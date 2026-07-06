using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using myEnergyUsage.Models;
using Newtonsoft.Json;

namespace myEnergyUsage
{
    public partial class MainForm : Form
    {
        private string _rootFolder;
        private readonly CsvLoader _csvLoader = new CsvLoader();
        private CostCalculator _costCalculator;
        private TariffService _tariffService;
        private string _tariffFilePath;
        private TariffStore _tariffStore;

      
        public MainForm()
        {
            InitializeComponent();

            // Load tariffs from JSON or wherever you store them
            // List<Tariff> tariffs = LoadTariffsFromJson();

            var tariffs = new List<Tariff>(); // empty list for now

            _tariffService = new TariffService(tariffs);
            _costCalculator = new CostCalculator(_tariffService);
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            Text += " : v" + Assembly.GetExecutingAssembly().GetName().Version; // put in the version number

            _tariffFilePath = Path.Combine(Application.StartupPath, "tariffs.json");

            _tariffStore = LoadTariffsFromJson(_tariffFilePath);

            _tariffService = new TariffService(_tariffStore.Tariffs);

            _costCalculator = new CostCalculator(_tariffService);

            LoadTariffList();
            }

        private void btn_close_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnSelectRoot_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select root folder containing Year/Month/HalfHour/Daily";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _rootFolder = dialog.SelectedPath;
                    LoadYears();
                }
            }
        }

        private void LoadYears()
        {
            cmbYear.Items.Clear();

            if (string.IsNullOrEmpty(_rootFolder) || !Directory.Exists(_rootFolder))
                return;

            try
            {
                var yearDirs = Directory.GetDirectories(_rootFolder)
                                        .Select(Path.GetFileName)
                                        .OrderBy(y => y);

                foreach (var year in yearDirs)
                {
                    cmbYear.Items.Add(year);
                }

                if (cmbYear.Items.Count > 0) cmbYear.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading years: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMonths();
        }

        private void LoadMonths()
        {
            cmbMonth.Items.Clear();

            if (cmbYear.SelectedItem == null)
                return;

            var yearFolder = Path.Combine(_rootFolder, cmbYear.SelectedItem.ToString());

            if (!Directory.Exists(yearFolder))
                return;

            try
            {
                var monthDirs = Directory.GetDirectories(yearFolder)
                                         .Select(Path.GetFileName)
                                         .OrderBy(m => m);

                foreach (var month in monthDirs)
                {
                    cmbMonth.Items.Add(month);
                }

                if (cmbMonth.Items.Count > 0) cmbMonth.SelectedIndex = 0;
                ConfigureChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading months: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ConfigureChart()
        {
            chartUsage.Series.Clear();
            chartUsage.ChartAreas[0].AxisX.Title = "Time";
            chartUsage.ChartAreas[0].AxisY.Title = "kWh";
            chartUsage.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // rotate labels if needed
        }

        private void ShowHalfHourlyDay(IEnumerable<EnergyReading> readingsForDay, string seriesName)
        {
            var series = new Series(seriesName)
            {
                ChartType = SeriesChartType.Column, // bar chart
                XValueType = ChartValueType.DateTime,
                YValueType = ChartValueType.Double
            };

            foreach (var r in readingsForDay.OrderBy(x => x.DateTimeUtc))
            {
                // Add point: X = time, Y = kWh
                int pointIndex = series.Points.AddXY(r.DateTimeUtc, r.KWh);

                // Store cost in Tag for tooltip
                double costPence = _costCalculator.CalculateCostForReading(r);
                series.Points[pointIndex].Tag = costPence;
            }

            chartUsage.Series.Add(series);
        }

        private void chartUsage_MouseMove(object sender, MouseEventArgs e)
        {
            var hit = chartUsage.HitTest(e.X, e.Y);

            if (hit.ChartElementType == ChartElementType.DataPoint)
            {
                var point = hit.Series.Points[hit.PointIndex];

                double kwh = point.YValues[0];
                double costPence = 0.0;

                if (point.Tag is double c)
                    costPence = c;

                // Show tooltip: kWh and cost
                string tooltip = $"Usage: {kwh:F3} kWh\nCost: {costPence / 100.0:F2} £";

                toolTip1.SetToolTip(chartUsage, tooltip);
            }
            else
            {
                toolTip1.SetToolTip(chartUsage, string.Empty);
            }
        }

        private void ShowAllSundaysInMonth(List<EnergyReading> allHalfHourlyReadings)
        {
            var sundays = allHalfHourlyReadings
                .Where(r => r.DateTimeUtc.DayOfWeek == DayOfWeek.Sunday)
                .GroupBy(r => r.DateTimeUtc.Date);

            foreach (var dayGroup in sundays)
            {
                string seriesName = dayGroup.Key.ToString("yyyy-MM-dd");
                ShowHalfHourlyDay(dayGroup, seriesName);
            }
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            if (cmbYear.SelectedItem == null || cmbMonth.SelectedItem == null)
            {
                MessageBox.Show("Please select a year and month.", "Missing selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validate selections
                if (cmbYear.SelectedItem == null || cmbMonth.SelectedItem == null)
                {
                    MessageBox.Show("Please select both a year and a month.", "Missing selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Determine selected folders
                string year = cmbYear.SelectedItem.ToString();
                string month = cmbMonth.SelectedItem.ToString();

                string monthFolder = Path.Combine(_rootFolder, year, month);

                if (!Directory.Exists(monthFolder))
                {
                    MessageBox.Show("The selected month folder does not exist.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Determine whether user wants HalfHour or Daily
                bool isHalfHour = rdoHalfHour.Checked;   // your radio button
                bool isDaily = rdoDaily.Checked;

                string dataFolder = isHalfHour
                    ? Path.Combine(monthFolder, "HalfHour")
                    : Path.Combine(monthFolder, "Daily");

                if (!Directory.Exists(dataFolder))
                {
                    MessageBox.Show("The selected data type folder does not exist.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Load all CSV files in the folder
                var csvFiles = Directory.GetFiles(dataFolder, "*.csv");

                if (csvFiles.Length == 0)
                {
                    MessageBox.Show("No CSV files found in this folder.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Prepare list for all readings
                List<EnergyReading> allReadings = new List<EnergyReading>();

                foreach (var file in csvFiles)
                {
                    // Determine energy type from filename (HouseE.csv → Electricity)
                    EnergyType type = file.EndsWith("E.csv", StringComparison.OrdinalIgnoreCase)
                        ? EnergyType.Electricity
                        : EnergyType.Gas;

                    // Load readings from this file
                    var readings = _csvLoader.LoadCsvFile(file, type);

                    // Add to master list
                    allReadings.AddRange(readings);
                }

                if (allReadings.Count == 0)
                {
                    MessageBox.Show("CSV files were found, but no valid readings were loaded.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Clear chart before drawing new data
                chartUsage.Series.Clear();

                // Example: show all readings for the month as one series
                // You can later replace this with filtering (e.g., Sundays only, specific days, etc.)
                var series = new Series("Usage")
                {
                    ChartType = SeriesChartType.Column,
                    XValueType = ChartValueType.DateTime,
                    YValueType = ChartValueType.Double
                };

                foreach (var r in allReadings.OrderBy(x => x.DateTimeUtc))
                {
                    int idx = series.Points.AddXY(r.DateTimeUtc, r.KWh);

                    // Store cost in Tag for tooltip
                    double costPence = _costCalculator.CalculateCostForReading(r);
                    series.Points[idx].Tag = costPence;
                }

                chartUsage.Series.Add(series);

                // Calculate total cost for the displayed period
                var costResult = _costCalculator.CalculateCostForPeriod(allReadings);

                double totalCostPounds = costResult.TotalCostPence / 100.0;

                // Show cost to user
                lblCost.Text = "Total Cost: £" + totalCostPounds.ToString("F2");

                // Optional: show total kWh
                lblkWh.Text = "Total kWh: " + costResult.TotalKWh.ToString("F3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating chart:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /////
        /// Tarriff info which is stored in JSON file

        public TariffStore LoadTariffsFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // No file yet → return empty store
                return new TariffStore();
            }

            try
            {
                string json = File.ReadAllText(filePath);
                TariffStore store = JsonConvert.DeserializeObject<TariffStore>(json);

                if (store == null)
                    return new TariffStore();

                return store;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tariff file:\n" + ex.Message,
                    "Tariff Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return new TariffStore();
            }
        }

        public void SaveTariffsToJson(string filePath, TariffStore store)
        {
            try
            {
                string json = JsonConvert.SerializeObject(store, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving tariff file:\n" + ex.Message,
                    "Tariff Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadTariffList()
        {
            lstTariffs.Items.Clear();

            foreach (var t in _tariffStore.Tariffs)
            {
                lstTariffs.Items.Add(t.Name);
            }
        }

        private void lstTariffs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTariffs.SelectedItem == null)
                return;

            string name = lstTariffs.SelectedItem.ToString();

            Tariff selected = _tariffStore.Tariffs
                .FirstOrDefault(t => t.Name == name);

            if (selected == null)
                return;

            txtName.Text = selected.Name;
            cmbEnergyType.SelectedIndex = selected.Type == EnergyType.Electricity ? 0 : 1;
            dtpFrom.Value = selected.EffectiveFrom;
            dtpTo.Value = selected.EffectiveTo;

            txtPeakRate.Text = selected.DayRatePencePerKWh.ToString();
            txtOffPeakRate.Text = selected.NightRatePencePerKWh.ToString();
            txtFlatRate.Text = selected.FlatRatePencePerKWh.ToString();
            txtStandingCharge.Text = selected.StandingChargePencePerDay.ToString();
            txtNrab.Text = selected.NrabPencePerKWh.ToString();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            lstTariffs.ClearSelected();

            txtName.Text = "";
            cmbEnergyType.SelectedIndex = -1;
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;

            txtPeakRate.Text = "0";
            txtOffPeakRate.Text = "0";
            txtFlatRate.Text = "0";
            txtStandingCharge.Text = "0";
            txtNrab.Text = "0";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate name
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Tariff must have a name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Determine energy type
                EnergyType type = cmbEnergyType.SelectedIndex == 0
                    ? EnergyType.Electricity
                    : EnergyType.Gas;

                // Try to find existing tariff
                Tariff existing = _tariffStore.Tariffs
                    .FirstOrDefault(t => t.Name == txtName.Text);

                if (existing == null)
                {
                    existing = new Tariff();
                    _tariffStore.Tariffs.Add(existing);
                }

                // Update fields
                existing.Name = txtName.Text;
                existing.Type = type;
                existing.EffectiveFrom = dtpFrom.Value;
                existing.EffectiveTo = dtpTo.Value;

                double temp;

                double.TryParse(txtPeakRate.Text, out temp);
                existing.DayRatePencePerKWh = temp;

                double.TryParse(txtOffPeakRate.Text, out temp);
                existing.NightRatePencePerKWh = temp;

                double.TryParse(txtFlatRate.Text, out temp);
                existing.FlatRatePencePerKWh = temp;

                double.TryParse(txtStandingCharge.Text, out temp);
                existing.StandingChargePencePerDay = temp;

                double.TryParse(txtNrab.Text, out temp);
                existing.NrabPencePerKWh = temp;

                // Save JSON
                SaveTariffsToJson(_tariffFilePath, _tariffStore);

                // Refresh list
                LoadTariffList();

                MessageBox.Show("Tariff saved successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving tariff:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstTariffs.SelectedItem == null)
                return;

            string name = lstTariffs.SelectedItem.ToString();

            Tariff t = _tariffStore.Tariffs.FirstOrDefault(x => x.Name == name);

            if (t == null)
                return;

            _tariffStore.Tariffs.Remove(t);

            SaveTariffsToJson(_tariffFilePath, _tariffStore);

            LoadTariffList();

            MessageBox.Show("Tariff deleted.", "Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

}
