using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CenteredMessagebox;
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

        //used to store data to use in sunrise and sunset tooltips
        private List<DateTime> sunriseMarkers = new List<DateTime>();
        private List<DateTime> sunsetMarkers = new List<DateTime>();


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

            dtpStartTime.Value = DateTime.Parse("00:00:00");
            dtpEndTime.Value = DateTime.Parse("23:30:00");

            //tooltip event handler
            chartUsage.GetToolTipText += chartUsage_GetToolTipText;


        }

        //tooltip logic
        private void chartUsage_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            // Only show tooltips for data points
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint point = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];

                // Extract the DateTime from the X value
                DateTime dt = DateTime.FromOADate(point.XValue);

                // Extract kWh from Y value
                double kwh = point.YValues[0];

                // Extract cost (stored in Tag)
                double costPence = 0;
                if (point.Tag != null)
                    costPence = (double)point.Tag;

                if (rdoHalfHour.Checked)
                {
                    // Build tooltip text
                    e.Text =
                        "Date: " + dt.ToString("dd-MMM-yyyy") + Environment.NewLine +
                        "Time: " + dt.ToString("HH:mm") + Environment.NewLine +
                        "Usage: " + kwh.ToString("F3") + " kWh" + Environment.NewLine +
                        "Cost: £" + (costPence / 100.0).ToString("F4");
                }
                else if (rdoDaily.Checked)
                {
                    e.Text =
                        "Usage: " + kwh.ToString("F3") + " kWh" + Environment.NewLine +
                        "Cost: £" + (costPence / 100.0).ToString("F4");
                }

            }

            // Second: sunrise/sunset tooltip detection
            HitTestResult h = e.HitTestResult;

            if (h.ChartArea != null)
            {
                Axis axisX = h.ChartArea.AxisX;

                // Convert mouse pixel to chart X-value
                double mouseXValue = axisX.PixelPositionToValue(e.X);

                // Check sunrise markers
                foreach (var sr in sunriseMarkers)
                {
                    if (Math.Abs(mouseXValue - sr.ToOADate()) < 0.01) // ~15 minutes tolerance
                    {
                        e.Text = "Sunrise: " + sr.ToString("HH:mm");
                        return;
                    }
                }

                // Check sunset markers
                foreach (var ss in sunsetMarkers)
                {
                    if (Math.Abs(mouseXValue - ss.ToOADate()) < 0.01)
                    {
                        e.Text = "Sunset: " + ss.ToString("HH:mm");
                        return;
                    }
                }
            }
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
                MsgBox.Show($"Error loading years: {ex.Message}", "Error",
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
                MsgBox.Show($"Error loading months: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ConfigureChart()
        {
            chartUsage.Series.Clear();
            chartUsage.ChartAreas[0].AxisX.Title = "Time";
            chartUsage.ChartAreas[0].AxisY.Title = "kWh";
            chartUsage.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // rotate labels if needed
        }

        private void btnShowChart_Click(object sender, EventArgs e)
        {
            DateTime? sunrise;
            DateTime? sunset;

            List<DateTime> selectedDays = new List<DateTime>();

           
            try
            {

                if (allReadings == null || allReadings.Count == 0)
                {
                    MsgBox.Show("No readings loaded. Please select a year and month first.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Clear Sunrise and sunset tooltip data
                sunriseMarkers.Clear();
                sunsetMarkers.Clear();

                // FULL RESET – fixes blank chart bug
                chartUsage.Series.Clear();
                chartUsage.ChartAreas[0].AxisX.StripLines.Clear();
                chartUsage.ChartAreas[0].AxisY.StripLines.Clear();

                // Reset axis objects (critical!)
                chartUsage.ChartAreas[0].AxisX = new Axis();
                chartUsage.ChartAreas[0].AxisY = new Axis();

                // Force recalculation
                chartUsage.ChartAreas[0].AxisX.Minimum = double.NaN;
                chartUsage.ChartAreas[0].AxisX.Maximum = double.NaN;
                chartUsage.ChartAreas[0].RecalculateAxesScale();

                // Force redraw
                chartUsage.Invalidate();
                chartUsage.Update();


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

                    chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // rotate labels if needed

                    // Cost for whole month
                    var costResult = _costCalculator.CalculateCostForPeriod(allReadings);

                    lblCost.Text = "Total Cost: £" + (costResult.TotalCostPence / 100.0).ToString("F2");
                    lblkWh.Text = "Total kWh: " + costResult.TotalKWh.ToString("F3");

                    return; // Finished daily mode
                }

                // ---------------------------------------------------------
                // HALF-HOUR MODE (FILTERING)
                // ---------------------------------------------------------

                if (rdoHalfHour.Checked)
                {
                   // 1. Get selected days
                   // List<DateTime> selectedDays = new List<DateTime>();

                    foreach (var item in clbDays.CheckedItems)
                    {
                        DateTime d;
                        if (DateTime.TryParse(item.ToString(), out d))
                            selectedDays.Add(d);
                    }

                    if (selectedDays.Count == 0)
                    {
                        MsgBox.Show("Please select at least one day.", "No Days Selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool isSingleDay = selectedDays.Count == 1;

                    // 2. Get selected time range
                    TimeSpan startTime = dtpStartTime.Value.TimeOfDay;
                    TimeSpan endTime = dtpEndTime.Value.TimeOfDay;

                    if (endTime <= startTime)
                    {
                        MsgBox.Show("End time must be after start time.", "Invalid Time Range",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Filter readings
                    var filteredReadings = FilterReadings(allReadings, selectedDays, startTime, endTime);

                    if (filteredReadings.Count == 0)
                    {
                        MsgBox.Show("No readings match the selected filters.", "No Data",
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

                        if (dayReadings.Count > 0)
                        {
                            chartUsage.Series.Add(series);
                        }

                        if (dayReadings.Count == 0)
                            continue;

                        foreach (var shading_day in selectedDays)
                        {

                            // ---------------------------------------------------------
                            // SUNRISE / SUNSET
                            // ---------------------------------------------------------
                            sunrise = GetSunEventLocal(shading_day, true);
                            sunset = GetSunEventLocal(shading_day, false);

                            if (!sunrise.HasValue || !sunset.HasValue)
                                continue;

                            // Vertical markers
                            if (sunrise.HasValue)
                            {
                                AddVerticalLine(sunrise.Value, Color.Red);
                                sunriseMarkers.Add(sunrise.Value);
                            }

                            if (sunset.HasValue)
                            {
                                AddVerticalLine(sunset.Value, Color.Orange);
                                sunsetMarkers.Add(sunset.Value);
                            }


                            // ---------------------------------------------------------
                            // NIGHT BEFORE SUNRISE (00:00 → sunrise)
                            // ---------------------------------------------------------
                            AddNightBeforeSunrise(shading_day, sunrise.Value);

                            // ---------------------------------------------------------
                            // DAYLIGHT SHADING (sunrise → sunset)
                            // ---------------------------------------------------------
                            AddDaylightShading(sunrise.Value, sunset.Value);

                            // ---------------------------------------------------------
                            // NIGHT AFTER SUNSET (sunset → next sunrise)
                            // ---------------------------------------------------------
                            DateTime nextDay = shading_day.AddDays(1);
                            DateTime? nextSunrise = GetSunEventLocal(nextDay, true);

                            if (nextSunrise.HasValue)
                                AddNightShading(sunset.Value, nextSunrise.Value);

                            // ---------------------------------------------------------
                            // PEAK SHADING (weekdays only)
                            // ---------------------------------------------------------
                            if (shading_day.DayOfWeek != DayOfWeek.Saturday &&
                                shading_day.DayOfWeek != DayOfWeek.Sunday)
                            {
                                AddPeakShading(shading_day);
                            }
                        }

                    }

                    // 5. Cost for filtered period
                    var costResult = _costCalculator.CalculateCostForPeriod(filteredReadings);

                    lblCost.Text = "Total Cost: £" + (costResult.TotalCostPence / 100.0).ToString("F2");
                    lblkWh.Text = "Total kWh: " + costResult.TotalKWh.ToString("F3");

                    //smart X-axis formatting
                    Axis axisX = chartUsage.ChartAreas[0].AxisX;
                    axisX.IsLabelAutoFit = true;
                    axisX.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont | LabelAutoFitStyles.LabelsAngleStep30;



                    if (isSingleDay)
                    {
                        // Single day → show time only
                        axisX.LabelStyle.Format = "HH:mm";

                        // Optional: show the date once at the left
                        axisX.Title = selectedDays[0].ToString("dddd : dd MMM yyyy");

                        axisX.MajorGrid.Enabled = false;
                        axisX.MinorGrid.Enabled = false;
                    }
                    else
                    {
                        // Multiple days → show date at midnight only
                        axisX.IntervalType = DateTimeIntervalType.Days;
                        axisX.Interval = 1;
                        axisX.LabelStyle.Format = "dd MMM";

                        // Reduce clutter
                        axisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                        axisX.Interval = 1; // one label per day
                    }
                }

                if (rdoHalfHour.Checked)
                {
                    // ---------------------------------------------------------
                    // Day before shading as bars start away from y-axis
                    // ---------------------------------------------------------

                    AddYesterdayPaddingShade(selectedDays[0]);
                }

                chartUsage.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // rotate labels if needed

               

            }
            catch (Exception ex)
            {
                MsgBox.Show("Error generating chart:\n" + ex.Message, "Error",
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
                MsgBox.Show("Error loading tariff file:\n" + ex.Message,
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
                MsgBox.Show("Error saving tariff file:\n" + ex.Message,
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
                    MsgBox.Show("Tariff must have a name.", "Validation Error",
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

                MsgBox.Show("Tariff saved successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MsgBox.Show("Error saving tariff:\n" + ex.Message, "Error",
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

            MsgBox.Show("Tariff deleted.", "Deleted",
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
                //clbDays.Items.Add(d.ToString("dddd : dd MMM yyyy"));

                clbDays.Items.Add(d.ToString("yyyy-MM-dd"));
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

            // Load only the correct folder based on radio button
            LoadAvailableSources(monthFolder);

            // Load readings for selected source
            LoadSelectedSourceReadings();
        }


        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMonthReadings();
        }

        // Add shading only on half hourly charts to show the peak and off peak times.
        private void AddPeakShading(DateTime day)
        {
            DateTime start = day.Date.AddHours(7);
            DateTime end = day.Date.AddHours(19);

            StripLine strip = new StripLine();
            strip.BackColor = Color.FromArgb(40, Color.Gray); // light grey
                                                              // strip.BackColor = Color.FromArgb(40, Color.DarkGray); // dark grey
            strip.IntervalOffset = start.ToOADate();
            strip.StripWidth = end.ToOADate() - start.ToOADate();

            chartUsage.ChartAreas[0].AxisX.StripLines.Add(strip);
        }


        /////////////////
        /// Sunrise Sunset items
        private DateTime? GetSunEventLocal(DateTime day, bool isSunrise)
        {
            Sunriset sr = new Sunriset();

            double latitude = 52.2053;
            double longitude = 0.1218;

            double utcHours = sr.CalculateSunTime(day, latitude, longitude, isSunrise);

            if (double.IsNaN(utcHours))
                return null;

            int hour = (int)Math.Floor(utcHours);
            int minute = (int)Math.Floor((utcHours - hour) * 60);
            int second = (int)Math.Floor(((utcHours - hour) * 60 - minute) * 60);

            DateTime utcTime = new DateTime(
                day.Year, day.Month, day.Day,
                hour, minute, second,
                DateTimeKind.Utc);

            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(
                utcTime,
                TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));

            return localTime;
        }

        //Draw vertical lines
        private void AddVerticalLine(DateTime time, Color color)
        {
            StripLine line = new StripLine();
            line.BorderColor = color;
            line.BorderWidth = 2;
            line.BorderDashStyle = ChartDashStyle.Solid;

            line.IntervalOffset = time.ToOADate();
            line.StripWidth = 0.0001;

            chartUsage.ChartAreas[0].AxisX.StripLines.Add(line);
        }

        private void AddDaylightShading(DateTime sunrise, DateTime sunset)
        {
            StripLine strip = new StripLine();
            strip.BackColor = Color.FromArgb(40, Color.Yellow); // soft daylight
            strip.IntervalOffset = sunrise.ToOADate();
            strip.StripWidth = sunset.ToOADate() - sunrise.ToOADate();

            chartUsage.ChartAreas[0].AxisX.StripLines.Add(strip);
        }

        private void AddNightShading(DateTime sunset, DateTime nextSunrise)
        {
           //This is added from sunset to midnight only
            StripLine strip = new StripLine();
            strip.BackColor = Color.FromArgb(40, Color.DarkBlue);
            strip.IntervalOffset = sunset.ToOADate();
            //strip.StripWidth = nextSunrise.ToOADate() - sunset.ToOADate();
            //Extract date and set to midnight
            strip.StripWidth = DateTime.Parse(nextSunrise.ToShortDateString() + " 00:00:00").ToOADate() - sunset.ToOADate();

            chartUsage.ChartAreas[0].AxisX.StripLines.Add(strip);
        }

        private void AddNightBeforeSunrise(DateTime day, DateTime sunrise)
        {
            DateTime midnight = day.Date;

            StripLine strip = new StripLine();
            strip.BackColor = Color.FromArgb(40, Color.DarkBlue);
            strip.IntervalOffset = midnight.ToOADate();
            strip.StripWidth = sunrise.ToOADate() - midnight.ToOADate();

            chartUsage.ChartAreas[0].AxisX.StripLines.Add(strip);
        }

        private void AddYesterdayPaddingShade(DateTime day)
        {
            DateTime yesterday = day.AddDays(-1);
            DateTime start = yesterday.Date.AddHours(23).AddMinutes(00);
            DateTime end = day.Date;

            StripLine strip = new StripLine();
            strip.BackColor = Color.FromArgb(40, Color.DarkBlue);
            strip.IntervalOffset = start.ToOADate();
            strip.StripWidth = end.ToOADate() - start.ToOADate();

            chartUsage.ChartAreas[0].AxisX.StripLines.Add(strip);
        }

        ////////////////////
        /// Combobox items

        private void LoadAvailableSources(string monthFolder)
        {
            cmbDataSource.Items.Clear();

            string halfHourFolder = Path.Combine(monthFolder, "HalfHour");
            string dailyFolder = Path.Combine(monthFolder, "Daily");

            List<CsvSourceInfo> sources = new List<CsvSourceInfo>();

            if (rdoHalfHour.Checked)
            {
                AddSourcesFromFolder(halfHourFolder, sources);
            }
            else if (rdoDaily.Checked)
            {
                AddSourcesFromFolder(dailyFolder, sources);
            }

            foreach (var src in sources)
                cmbDataSource.Items.Add(src);

            if (cmbDataSource.Items.Count > 0)
                cmbDataSource.SelectedIndex = 0;
        }

        private void AddSourcesFromFolder(string folder, List<CsvSourceInfo> list)
        {
            if (!Directory.Exists(folder))
                return;

            var files = Directory.GetFiles(folder, "*.csv");

            foreach (var file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file);

                // Example: HouseE → Location = House, Type = Electricity
                string location = name.Substring(0, name.Length - 1);
                char typeChar = name[name.Length - 1];

                EnergyType type = typeChar == 'E'
                    ? EnergyType.Electricity
                    : EnergyType.Gas;

                list.Add(new CsvSourceInfo
                {
                    FilePath = file,
                    Location = location,
                    Type = type
                });
            }
        }

        private void LoadSelectedSourceReadings()
        {
            allReadings.Clear();

            if (cmbDataSource.SelectedItem is CsvSourceInfo src)
            {
                var readings = _csvLoader.LoadCsvFile(src.FilePath, src.Type);
                allReadings.AddRange(readings);

                if (rdoHalfHour.Checked)
                {
                    PopulateDayList(allReadings);
                }
                else
                {
                    clbDays.Items.Clear(); // daily mode does not use day selection
                }
            }
        }


        private void cmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedSourceReadings();
        }

        private void rdoHalfHour_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoHalfHour.Checked)
                LoadMonthReadings();
        }

        private void rdoDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDaily.Checked)
                LoadMonthReadings();
        }
    }

}
