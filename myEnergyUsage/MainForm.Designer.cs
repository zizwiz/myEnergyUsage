
namespace myEnergyUsage
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_close = new System.Windows.Forms.Button();
            this.chartUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ShowChart = new System.Windows.Forms.Button();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.btnSelectRoot = new System.Windows.Forms.Button();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblkWh = new System.Windows.Forms.Label();
            this.rdoHalfHour = new System.Windows.Forms.RadioButton();
            this.rdoDaily = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_charts = new System.Windows.Forms.TabPage();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.clbDays = new System.Windows.Forms.CheckedListBox();
            this.tab_costs = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNrab = new System.Windows.Forms.TextBox();
            this.txtStandingCharge = new System.Windows.Forms.TextBox();
            this.txtFlatRate = new System.Windows.Forms.TextBox();
            this.txtOffPeakRate = new System.Windows.Forms.TextBox();
            this.txtPeakRate = new System.Windows.Forms.TextBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbEnergyType = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lstTariffs = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.cmbDataSource = new System.Windows.Forms.ComboBox();
            this.lbl_month = new System.Windows.Forms.Label();
            this.lbl_year = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartUsage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_charts.SuspendLayout();
            this.tab_costs.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(1276, 586);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(104, 45);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // chartUsage
            // 
            chartArea1.Name = "ChartArea1";
            this.chartUsage.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartUsage.Legends.Add(legend1);
            this.chartUsage.Location = new System.Drawing.Point(226, 16);
            this.chartUsage.Name = "chartUsage";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartUsage.Series.Add(series1);
            this.chartUsage.Size = new System.Drawing.Size(1236, 531);
            this.chartUsage.TabIndex = 1;
            this.chartUsage.Text = "chart1";
            // 
            // ShowChart
            // 
            this.ShowChart.Location = new System.Drawing.Point(1031, 585);
            this.ShowChart.Name = "ShowChart";
            this.ShowChart.Size = new System.Drawing.Size(176, 45);
            this.ShowChart.TabIndex = 2;
            this.ShowChart.Text = "Show Chart";
            this.ShowChart.UseVisualStyleBackColor = true;
            this.ShowChart.Click += new System.EventHandler(this.btnShowChart_Click);
            // 
            // cmbYear
            // 
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(79, 30);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(141, 28);
            this.cmbYear.TabIndex = 3;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(79, 74);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(141, 28);
            this.cmbMonth.TabIndex = 4;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            // 
            // btnSelectRoot
            // 
            this.btnSelectRoot.Location = new System.Drawing.Point(824, 586);
            this.btnSelectRoot.Name = "btnSelectRoot";
            this.btnSelectRoot.Size = new System.Drawing.Size(177, 44);
            this.btnSelectRoot.TabIndex = 5;
            this.btnSelectRoot.Text = "Select Data Root";
            this.btnSelectRoot.UseVisualStyleBackColor = true;
            this.btnSelectRoot.Click += new System.EventHandler(this.btnSelectRoot_Click);
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(121, 31);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(57, 20);
            this.lblCost.TabIndex = 6;
            this.lblCost.Text = "lblCost";
            // 
            // lblkWh
            // 
            this.lblkWh.AutoSize = true;
            this.lblkWh.Location = new System.Drawing.Point(121, 57);
            this.lblkWh.Name = "lblkWh";
            this.lblkWh.Size = new System.Drawing.Size(56, 20);
            this.lblkWh.TabIndex = 7;
            this.lblkWh.Text = "lblkWh";
            // 
            // rdoHalfHour
            // 
            this.rdoHalfHour.AutoSize = true;
            this.rdoHalfHour.Checked = true;
            this.rdoHalfHour.Location = new System.Drawing.Point(6, 29);
            this.rdoHalfHour.Name = "rdoHalfHour";
            this.rdoHalfHour.Size = new System.Drawing.Size(112, 24);
            this.rdoHalfHour.TabIndex = 8;
            this.rdoHalfHour.TabStop = true;
            this.rdoHalfHour.Text = "Half Hourly";
            this.rdoHalfHour.UseVisualStyleBackColor = true;
            this.rdoHalfHour.CheckedChanged += new System.EventHandler(this.rdoHalfHour_CheckedChanged);
            // 
            // rdoDaily
            // 
            this.rdoDaily.AutoSize = true;
            this.rdoDaily.Location = new System.Drawing.Point(124, 29);
            this.rdoDaily.Name = "rdoDaily";
            this.rdoDaily.Size = new System.Drawing.Size(68, 24);
            this.rdoDaily.TabIndex = 9;
            this.rdoDaily.TabStop = true;
            this.rdoDaily.Text = "Daily";
            this.rdoDaily.UseVisualStyleBackColor = true;
            this.rdoDaily.CheckedChanged += new System.EventHandler(this.rdoDaily_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoDaily);
            this.groupBox1.Controls.Add(this.rdoHalfHour);
            this.groupBox1.Location = new System.Drawing.Point(20, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 74);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graph Type";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_charts);
            this.tabControl1.Controls.Add(this.tab_costs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1527, 703);
            this.tabControl1.TabIndex = 11;
            // 
            // tab_charts
            // 
            this.tab_charts.Controls.Add(this.groupBox3);
            this.tab_charts.Controls.Add(this.groupBox2);
            this.tab_charts.Controls.Add(this.label17);
            this.tab_charts.Controls.Add(this.lbl_year);
            this.tab_charts.Controls.Add(this.lbl_month);
            this.tab_charts.Controls.Add(this.cmbDataSource);
            this.tab_charts.Controls.Add(this.clbDays);
            this.tab_charts.Controls.Add(this.chartUsage);
            this.tab_charts.Controls.Add(this.btn_close);
            this.tab_charts.Controls.Add(this.btnSelectRoot);
            this.tab_charts.Controls.Add(this.ShowChart);
            this.tab_charts.Controls.Add(this.groupBox1);
            this.tab_charts.Controls.Add(this.cmbMonth);
            this.tab_charts.Controls.Add(this.cmbYear);
            this.tab_charts.Location = new System.Drawing.Point(4, 29);
            this.tab_charts.Name = "tab_charts";
            this.tab_charts.Padding = new System.Windows.Forms.Padding(3);
            this.tab_charts.Size = new System.Drawing.Size(1519, 670);
            this.tab_charts.TabIndex = 0;
            this.tab_charts.Text = "Charts";
            this.tab_charts.UseVisualStyleBackColor = true;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(212, 28);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(107, 26);
            this.dtpEndTime.TabIndex = 15;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(183, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 20);
            this.label16.TabIndex = 14;
            this.label16.Text = "to";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 20);
            this.label15.TabIndex = 13;
            this.label15.Text = "From";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(70, 28);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(107, 26);
            this.dtpStartTime.TabIndex = 12;
            // 
            // clbDays
            // 
            this.clbDays.CheckOnClick = true;
            this.clbDays.FormattingEnabled = true;
            this.clbDays.Location = new System.Drawing.Point(20, 234);
            this.clbDays.Name = "clbDays";
            this.clbDays.Size = new System.Drawing.Size(200, 326);
            this.clbDays.TabIndex = 11;
            // 
            // tab_costs
            // 
            this.tab_costs.Controls.Add(this.button1);
            this.tab_costs.Controls.Add(this.label14);
            this.tab_costs.Controls.Add(this.label13);
            this.tab_costs.Controls.Add(this.label12);
            this.tab_costs.Controls.Add(this.label11);
            this.tab_costs.Controls.Add(this.label10);
            this.tab_costs.Controls.Add(this.label9);
            this.tab_costs.Controls.Add(this.label8);
            this.tab_costs.Controls.Add(this.label7);
            this.tab_costs.Controls.Add(this.label6);
            this.tab_costs.Controls.Add(this.label5);
            this.tab_costs.Controls.Add(this.label4);
            this.tab_costs.Controls.Add(this.label3);
            this.tab_costs.Controls.Add(this.label2);
            this.tab_costs.Controls.Add(this.label1);
            this.tab_costs.Controls.Add(this.txtNrab);
            this.tab_costs.Controls.Add(this.txtStandingCharge);
            this.tab_costs.Controls.Add(this.txtFlatRate);
            this.tab_costs.Controls.Add(this.txtOffPeakRate);
            this.tab_costs.Controls.Add(this.txtPeakRate);
            this.tab_costs.Controls.Add(this.dtpTo);
            this.tab_costs.Controls.Add(this.dtpFrom);
            this.tab_costs.Controls.Add(this.cmbEnergyType);
            this.tab_costs.Controls.Add(this.txtName);
            this.tab_costs.Controls.Add(this.lstTariffs);
            this.tab_costs.Controls.Add(this.btnDelete);
            this.tab_costs.Controls.Add(this.btnSave);
            this.tab_costs.Controls.Add(this.btnAddNew);
            this.tab_costs.Location = new System.Drawing.Point(4, 29);
            this.tab_costs.Name = "tab_costs";
            this.tab_costs.Padding = new System.Windows.Forms.Padding(3);
            this.tab_costs.Size = new System.Drawing.Size(1519, 670);
            this.tab_costs.TabIndex = 1;
            this.tab_costs.Text = "Costs";
            this.tab_costs.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(553, 475);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 20);
            this.label14.TabIndex = 26;
            this.label14.Text = "p/kWh";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(343, 443);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 20);
            this.label13.TabIndex = 25;
            this.label13.Text = "p/day";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(283, 411);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "p/kWh";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(283, 382);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "p/kWh";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(283, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "p/kWh";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 475);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(339, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "Nuclear Regulated Asset Base charge (NRAB)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 443);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Standing Charge";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 411);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Flat Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 382);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "Off Peak";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Peak";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Energy Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Valid From";
            // 
            // txtNrab
            // 
            this.txtNrab.Location = new System.Drawing.Point(382, 472);
            this.txtNrab.Name = "txtNrab";
            this.txtNrab.Size = new System.Drawing.Size(165, 26);
            this.txtNrab.TabIndex = 12;
            // 
            // txtStandingCharge
            // 
            this.txtStandingCharge.Location = new System.Drawing.Point(172, 440);
            this.txtStandingCharge.Name = "txtStandingCharge";
            this.txtStandingCharge.Size = new System.Drawing.Size(165, 26);
            this.txtStandingCharge.TabIndex = 11;
            // 
            // txtFlatRate
            // 
            this.txtFlatRate.Location = new System.Drawing.Point(129, 408);
            this.txtFlatRate.Name = "txtFlatRate";
            this.txtFlatRate.Size = new System.Drawing.Size(148, 26);
            this.txtFlatRate.TabIndex = 10;
            // 
            // txtOffPeakRate
            // 
            this.txtOffPeakRate.Location = new System.Drawing.Point(129, 376);
            this.txtOffPeakRate.Name = "txtOffPeakRate";
            this.txtOffPeakRate.Size = new System.Drawing.Size(148, 26);
            this.txtOffPeakRate.TabIndex = 9;
            // 
            // txtPeakRate
            // 
            this.txtPeakRate.Location = new System.Drawing.Point(129, 344);
            this.txtPeakRate.Name = "txtPeakRate";
            this.txtPeakRate.Size = new System.Drawing.Size(148, 26);
            this.txtPeakRate.TabIndex = 8;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(368, 261);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(201, 26);
            this.dtpTo.TabIndex = 7;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(105, 261);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(201, 26);
            this.dtpFrom.TabIndex = 6;
            // 
            // cmbEnergyType
            // 
            this.cmbEnergyType.FormattingEnabled = true;
            this.cmbEnergyType.Items.AddRange(new object[] {
            "Electricity",
            "Gas"});
            this.cmbEnergyType.Location = new System.Drawing.Point(129, 152);
            this.cmbEnergyType.Name = "cmbEnergyType";
            this.cmbEnergyType.Size = new System.Drawing.Size(293, 28);
            this.cmbEnergyType.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(129, 77);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(294, 26);
            this.txtName.TabIndex = 4;
            // 
            // lstTariffs
            // 
            this.lstTariffs.FormattingEnabled = true;
            this.lstTariffs.ItemHeight = 20;
            this.lstTariffs.Location = new System.Drawing.Point(644, 75);
            this.lstTariffs.Name = "lstTariffs";
            this.lstTariffs.Size = new System.Drawing.Size(448, 244);
            this.lstTariffs.TabIndex = 3;
            this.lstTariffs.SelectedIndexChanged += new System.EventHandler(this.lstTariffs_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1083, 539);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(125, 42);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(925, 539);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 42);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(771, 539);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(125, 42);
            this.btnAddNew.TabIndex = 0;
            this.btnAddNew.Text = "New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // cmbDataSource
            // 
            this.cmbDataSource.FormattingEnabled = true;
            this.cmbDataSource.Location = new System.Drawing.Point(79, 200);
            this.cmbDataSource.Name = "cmbDataSource";
            this.cmbDataSource.Size = new System.Drawing.Size(141, 28);
            this.cmbDataSource.TabIndex = 16;
            this.cmbDataSource.SelectedIndexChanged += new System.EventHandler(this.cmbDataSource_SelectedIndexChanged);
            // 
            // lbl_month
            // 
            this.lbl_month.AutoSize = true;
            this.lbl_month.Location = new System.Drawing.Point(19, 77);
            this.lbl_month.Name = "lbl_month";
            this.lbl_month.Size = new System.Drawing.Size(54, 20);
            this.lbl_month.TabIndex = 17;
            this.lbl_month.Text = "Month";
            // 
            // lbl_year
            // 
            this.lbl_year.AutoSize = true;
            this.lbl_year.Location = new System.Drawing.Point(19, 33);
            this.lbl_year.Name = "lbl_year";
            this.lbl_year.Size = new System.Drawing.Size(43, 20);
            this.lbl_year.TabIndex = 18;
            this.lbl_year.Text = "Year";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(16, 203);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 20);
            this.label17.TabIndex = 19;
            this.label17.Text = "Meter";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.lblCost);
            this.groupBox2.Controls.Add(this.lblkWh);
            this.groupBox2.Location = new System.Drawing.Point(440, 566);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 96);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Totals";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 20);
            this.label18.TabIndex = 8;
            this.label18.Text = "Cost: ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 57);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(109, 20);
            this.label19.TabIndex = 9;
            this.label19.Text = "Energy Used: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpEndTime);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.dtpStartTime);
            this.groupBox3.Location = new System.Drawing.Point(20, 580);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(337, 84);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Time Selection";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1251, 539);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 45);
            this.button1.TabIndex = 27;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 703);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "myEnergyUsage";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartUsage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tab_charts.ResumeLayout(false);
            this.tab_charts.PerformLayout();
            this.tab_costs.ResumeLayout(false);
            this.tab_costs.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUsage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button ShowChart;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Button btnSelectRoot;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblkWh;
        private System.Windows.Forms.RadioButton rdoHalfHour;
        private System.Windows.Forms.RadioButton rdoDaily;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_charts;
        private System.Windows.Forms.TabPage tab_costs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNrab;
        private System.Windows.Forms.TextBox txtStandingCharge;
        private System.Windows.Forms.TextBox txtFlatRate;
        private System.Windows.Forms.TextBox txtOffPeakRate;
        private System.Windows.Forms.TextBox txtPeakRate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cmbEnergyType;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ListBox lstTariffs;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.CheckedListBox clbDays;
        private System.Windows.Forms.ComboBox cmbDataSource;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_year;
        private System.Windows.Forms.Label lbl_month;
        private System.Windows.Forms.Button button1;
    }
}

