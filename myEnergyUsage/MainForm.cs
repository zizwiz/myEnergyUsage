using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<EnergyReading> allReadings;   
        private CsvLoader _csvLoader = new CsvLoader();
        private TariffStore _tariffStore;
        private TariffService _tariffService;
        private CostCalculator _costCalculator;

        private string _rootFolder;
        private string _tariffFilePath;

     
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

            _tariffFilePath = Path.Combine(Application.StartupPath, "tariffs.json"); //Get list from same location as exe
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
                string tooltip = $"Usage: {kwh:F3} kWh\nCost: £{costPence / 100.0:F2}";

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
            try
            {
                if (allReadings == null || allReadings.Count == 0)
                {
                    MessageBox.Show("No readings loaded. Please select a year and month first.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                chartUsage.Series.Clear();

                // ---------------------------------------------------------
                // DAILY MODE (NO FILTERING)
                // ---------------------------------------------------------
                if (rdoDaily.Checked)
                {
                    // One series for the whole month
                    var series = new Series("Daily Usage")
                    {
                        ChartType = SeriesChartType.Column,
                        XValueType = ChartValueType.DateTime,
                        YValueType = ChartValueType.Double
                    };

                    foreach (var r in allReadings.OrderBy(x => x.DateTimeUtc))
                    {
                        int idx = series.Points.AddXY(r.DateTimeUtc.Date, r.KWh);

                        double costPence = _costCalculator.CalculateCostForReading(r);
                        series.Points[idx].Tag = costPence;
                    }

                    chartUsage.Series.Add(series);

                    // Cost for whole month
                    var costResult = _costCalculator.CalculateCostForPeriod(allReadings);

                    lblCost.Text = "Total Cost: £" + (costResult.TotalCostPence / 100.0).ToString("F2");
                    lblkWh.Text = "Total kWh: " + costResult.TotalKWh.ToString("F3");

                    return; // Finished daily mode
                }

                // ---------------------------------------------------------
                // HALF-HOUR MODE (FILTERING)
                // ---------------------------------------------------------
                chartUsage.ChartAreas[0].AxisX.StripLines.Clear();

                if (rdoHalfHour.Checked)
                {
                    // 1. Get selected days
                    List<DateTime> selectedDays = new List<DateTime>();

                    foreach (var item in clbDays.CheckedItems)
                    {
                        DateTime d;
                        if (DateTime.TryParse(item.ToString(), out d))
                            selectedDays.Add(d);
                    }

                    if (selectedDays.Count == 0)
                    {
                        MessageBox.Show("Please select at least one day.", "No Days Selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2. Get selected time range
                    TimeSpan startTime = dtpStartTime.Value.TimeOfDay;
                    TimeSpan endTime = dtpEndTime.Value.TimeOfDay;

                    if (endTime <= startTime)
                    {
                        MessageBox.Show("End time must be after start time.", "Invalid Time Range",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Filter readings
                    var filteredReadings = FilterReadings(allReadings, selectedDays, startTime, endTime);

                    if (filteredReadings.Count == 0)
                    {
                        MessageBox.Show("No readings match the selected filters.", "No Data",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 4. Create one series per selected day
                    foreach (var day in selectedDays)
                    {
                        string seriesName = day.ToString("yyyy-MM-dd");

                        var dayReadings = filteredReadings
                            .Where(r => r.DateTimeUtc.Date == day)
                            .OrderBy(r => r.DateTimeUtc)
                            .ToList();

                        var series = new Series(seriesName)
                        {
                            ChartType = SeriesChartType.Column,
                            XValueType = ChartValueType.DateTime,
                            YValueType = ChartValueType.Double
                        };

                        foreach (var r in dayReadings)
                        {
                            int idx = series.Points.AddXY(r.DateTimeUtc, r.KWh);

                            double costPence = _costCalculator.CalculateCostForReading(r);
                            series.Points[idx].Tag = costPence;
                        }

                        chartUsage.Series.Add(series);

                        // ---------------------------------------------------------
                        // ADD PEAK PERIOD SHADING (WEEKDAYS ONLY)
                        // ---------------------------------------------------------
                        if (day.DayOfWeek != DayOfWeek.Saturday &&
                            day.DayOfWeek != DayOfWeek.Sunday)
                        {
                            AddPeakShading(day);
                        }
                    }

                    // 5. Cost for filtered period
                    var costResult = _costCalculator.CalculateCostForPeriod(filteredReadings);

                    lblCost.Text = "Total Cost: £" + (costResult.TotalCostPence / 100.0).ToString("F2");
                    lblkWh.Text = "Total kWh: " + costResult.TotalKWh.ToString("F3");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating chart:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /////
        /// Tariff info which is stored in JSON file

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

        //////////////////////
        /// CSV filtering functions
        ///
        private void PopulateDayList(List<EnergyReading> readings)
        {
            clbDays.Items.Clear();

            var days = readings
                .Select(r => r.DateTimeUtc.Date)
                .Distinct()
                .OrderBy(d => d);

            foreach (var d in days)
            {
               // show the date as dayname : day of month Month name Year.
                clbDays.Items.Add(d.ToString("dddd : dd MMM yyyy"));
            }
        }

        private List<EnergyReading> FilterReadings(
            List<EnergyReading> allReadings,
            List<DateTime> selectedDays,
            TimeSpan startTime,
            TimeSpan endTime)
        {
            var filtered = allReadings
                .Where(r => selectedDays.Contains(r.DateTimeUtc.Date))
                .Where(r =>
                {
                    var t = r.DateTimeUtc.TimeOfDay;
                    return t >= startTime && t <= endTime;
                })
                .OrderBy(r => r.DateTimeUtc)
                .ToList();

            return filtered;
        }

        private void LoadMonthReadings()
        {
            allReadings = new List<EnergyReading>();

            string year = cmbYear.SelectedItem.ToString();
            string month = cmbMonth.SelectedItem.ToString();

            string monthFolder = Path.Combine(_rootFolder, year, month);
            string halfHourFolder = Path.Combine(monthFolder, "HalfHour");

            if (!Directory.Exists(halfHourFolder))
            {
                clbDays.Items.Clear();
                return;
            }

            var csvFiles = Directory.GetFiles(halfHourFolder, "*.csv");

            foreach (var file in csvFiles)
            {
                EnergyType type = file.EndsWith("E.csv") ? EnergyType.Electricity : EnergyType.Gas;
                var readings = _csvLoader.LoadCsvFile(file, type);
                allReadings.AddRange(readings);
            }

            PopulateDayList(allReadings);
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMonthReadings();
        }

        // Add shading only on half hourly charts to show the peak and off peak times.
        private void AddPeakShading(DateTime day)
        {
            // Peak period: 07:00 → 19:00
            DateTime start = day.Date.AddHours(7);
            DateTime end = day.Date.AddHours(19);

            StripLine strip = new StripLine();
            strip.BackColor = Color.FromArgb(40, Color.Gray); // light grey transparent
            strip.IntervalOffset = start.ToOADate();
            strip.StripWidth = end.ToOADate() - start.ToOADate();

            // Add to chart area
            chartUsage.ChartAreas[0].AxisX.StripLines.Add(strip);
        }

    }

}
