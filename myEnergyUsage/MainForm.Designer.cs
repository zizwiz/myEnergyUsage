
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
            this.tab_graphs = new System.Windows.Forms.TabPage();
            this.tab_costs = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.chartUsage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_graphs.SuspendLayout();
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
            this.chartUsage.Location = new System.Drawing.Point(266, 16);
            this.chartUsage.Name = "chartUsage";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartUsage.Series.Add(series1);
            this.chartUsage.Size = new System.Drawing.Size(1108, 531);
            this.chartUsage.TabIndex = 1;
            this.chartUsage.Text = "chart1";
            this.chartUsage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartUsage_MouseMove);
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
            this.cmbYear.Location = new System.Drawing.Point(45, 33);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(163, 28);
            this.cmbYear.TabIndex = 3;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbMonth
            // 
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(45, 88);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(160, 28);
            this.cmbMonth.TabIndex = 4;
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
            this.lblCost.Location = new System.Drawing.Point(41, 305);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(57, 20);
            this.lblCost.TabIndex = 6;
            this.lblCost.Text = "lblCost";
            // 
            // lblkWh
            // 
            this.lblkWh.AutoSize = true;
            this.lblkWh.Location = new System.Drawing.Point(42, 343);
            this.lblkWh.Name = "lblkWh";
            this.lblkWh.Size = new System.Drawing.Size(56, 20);
            this.lblkWh.TabIndex = 7;
            this.lblkWh.Text = "lblkWh";
            // 
            // rdoHalfHour
            // 
            this.rdoHalfHour.AutoSize = true;
            this.rdoHalfHour.Checked = true;
            this.rdoHalfHour.Location = new System.Drawing.Point(27, 52);
            this.rdoHalfHour.Name = "rdoHalfHour";
            this.rdoHalfHour.Size = new System.Drawing.Size(112, 24);
            this.rdoHalfHour.TabIndex = 8;
            this.rdoHalfHour.TabStop = true;
            this.rdoHalfHour.Text = "Half Hourly";
            this.rdoHalfHour.UseVisualStyleBackColor = true;
            // 
            // rdoDaily
            // 
            this.rdoDaily.AutoSize = true;
            this.rdoDaily.Location = new System.Drawing.Point(27, 82);
            this.rdoDaily.Name = "rdoDaily";
            this.rdoDaily.Size = new System.Drawing.Size(68, 24);
            this.rdoDaily.TabIndex = 9;
            this.rdoDaily.TabStop = true;
            this.rdoDaily.Text = "Daily";
            this.rdoDaily.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoDaily);
            this.groupBox1.Controls.Add(this.rdoHalfHour);
            this.groupBox1.Location = new System.Drawing.Point(45, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 128);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Graph Type";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_graphs);
            this.tabControl1.Controls.Add(this.tab_costs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1424, 690);
            this.tabControl1.TabIndex = 11;
            // 
            // tab_graphs
            // 
            this.tab_graphs.Controls.Add(this.chartUsage);
            this.tab_graphs.Controls.Add(this.btn_close);
            this.tab_graphs.Controls.Add(this.btnSelectRoot);
            this.tab_graphs.Controls.Add(this.lblkWh);
            this.tab_graphs.Controls.Add(this.ShowChart);
            this.tab_graphs.Controls.Add(this.groupBox1);
            this.tab_graphs.Controls.Add(this.lblCost);
            this.tab_graphs.Controls.Add(this.cmbMonth);
            this.tab_graphs.Controls.Add(this.cmbYear);
            this.tab_graphs.Location = new System.Drawing.Point(4, 29);
            this.tab_graphs.Name = "tab_graphs";
            this.tab_graphs.Padding = new System.Windows.Forms.Padding(3);
            this.tab_graphs.Size = new System.Drawing.Size(1416, 657);
            this.tab_graphs.TabIndex = 0;
            this.tab_graphs.Text = "Graphs";
            this.tab_graphs.UseVisualStyleBackColor = true;
            // 
            // tab_costs
            // 
            this.tab_costs.Location = new System.Drawing.Point(4, 29);
            this.tab_costs.Name = "tab_costs";
            this.tab_costs.Padding = new System.Windows.Forms.Padding(3);
            this.tab_costs.Size = new System.Drawing.Size(1416, 657);
            this.tab_costs.TabIndex = 1;
            this.tab_costs.Text = "Costs";
            this.tab_costs.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 690);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "myEnergyUsage";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartUsage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tab_graphs.ResumeLayout(false);
            this.tab_graphs.PerformLayout();
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
        private System.Windows.Forms.TabPage tab_graphs;
        private System.Windows.Forms.TabPage tab_costs;
    }
}

