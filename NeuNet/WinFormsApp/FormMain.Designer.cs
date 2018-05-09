namespace WinFormsApp
{
    partial class FormMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabEmployee = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBoxEmployees = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cartesianChartMain = new LiveCharts.WinForms.CartesianChart();
            this.cartesianChartAnomaly = new LiveCharts.WinForms.CartesianChart();
            this.pieChartVectors = new LiveCharts.WinForms.PieChart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAnalyse = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonLoadData = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControl1
            //
            this.tabControl1.Controls.Add(this.tabEmployee);
            this.tabControl1.Controls.Add(this.tabStatistics);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(964, 573);
            this.tabControl1.TabIndex = 0;
            //
            // tabEmployee
            //
            this.tabEmployee.Controls.Add(this.splitContainer1);
            this.tabEmployee.Location = new System.Drawing.Point(4, 22);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmployee.Size = new System.Drawing.Size(956, 547);
            this.tabEmployee.TabIndex = 0;
            this.tabEmployee.Text = "Employees";
            this.tabEmployee.UseVisualStyleBackColor = true;
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.Controls.Add(this.listBoxEmployees);
            this.splitContainer1.Panel1MinSize = 50;
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(950, 541);
            this.splitContainer1.SplitterDistance = 76;
            this.splitContainer1.TabIndex = 0;
            //
            // listBoxEmployees
            //
            this.listBoxEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEmployees.FormattingEnabled = true;
            this.listBoxEmployees.Items.AddRange(new object[] {
            "0001",
            "0002",
            "0003"});
            this.listBoxEmployees.Location = new System.Drawing.Point(0, 0);
            this.listBoxEmployees.Name = "listBoxEmployees";
            this.listBoxEmployees.Size = new System.Drawing.Size(76, 541);
            this.listBoxEmployees.TabIndex = 0;
            this.listBoxEmployees.SelectedIndexChanged += new System.EventHandler(this.listBoxEmployees_SelectedIndexChanged);
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cartesianChartMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cartesianChartAnomaly, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pieChartVectors, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 83);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(870, 388);
            this.tableLayoutPanel1.TabIndex = 2;
            //
            // cartesianChartMain
            //
            this.tableLayoutPanel1.SetColumnSpan(this.cartesianChartMain, 2);
            this.cartesianChartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChartMain.Location = new System.Drawing.Point(3, 3);
            this.cartesianChartMain.Name = "cartesianChartMain";
            this.cartesianChartMain.Size = new System.Drawing.Size(864, 188);
            this.cartesianChartMain.TabIndex = 0;
            this.cartesianChartMain.Text = "cartesianChart1";
            //
            // cartesianChartAnomaly
            //
            this.cartesianChartAnomaly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChartAnomaly.Location = new System.Drawing.Point(3, 197);
            this.cartesianChartAnomaly.Name = "cartesianChartAnomaly";
            this.cartesianChartAnomaly.Size = new System.Drawing.Size(429, 188);
            this.cartesianChartAnomaly.TabIndex = 1;
            this.cartesianChartAnomaly.Text = "cartesianChart1";
            //
            // pieChartVectors
            //
            this.pieChartVectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pieChartVectors.Location = new System.Drawing.Point(438, 197);
            this.pieChartVectors.Name = "pieChartVectors";
            this.pieChartVectors.Size = new System.Drawing.Size(429, 188);
            this.pieChartVectors.TabIndex = 2;
            this.pieChartVectors.Text = "pieChart1";
            //
            // panel2
            //
            this.panel2.Controls.Add(this.buttonAnalyse);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 471);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(870, 70);
            this.panel2.TabIndex = 1;
            //
            // buttonAnalyse
            //
            this.buttonAnalyse.Location = new System.Drawing.Point(4, 26);
            this.buttonAnalyse.Name = "buttonAnalyse";
            this.buttonAnalyse.Size = new System.Drawing.Size(75, 23);
            this.buttonAnalyse.TabIndex = 0;
            this.buttonAnalyse.Text = "Analyse";
            this.buttonAnalyse.UseVisualStyleBackColor = true;
            this.buttonAnalyse.Click += new System.EventHandler(this.buttonAnalyse_Click);
            //
            // panel1
            //
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(870, 83);
            this.panel1.TabIndex = 0;
            //
            // tabStatistics
            //
            this.tabStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(956, 547);
            this.tabStatistics.TabIndex = 1;
            this.tabStatistics.Text = "Statistics";
            this.tabStatistics.UseVisualStyleBackColor = true;
            //
            // tabPageSettings
            //
            this.tabPageSettings.Controls.Add(this.panel3);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(956, 547);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            //
            // panel3
            //
            this.panel3.Controls.Add(this.buttonLoadData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(950, 31);
            this.panel3.TabIndex = 0;
            //
            // buttonLoadData
            //
            this.buttonLoadData.Location = new System.Drawing.Point(5, 3);
            this.buttonLoadData.Name = "buttonLoadData";
            this.buttonLoadData.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadData.TabIndex = 0;
            this.buttonLoadData.Text = "ReLoad data";
            this.buttonLoadData.UseVisualStyleBackColor = true;
            this.buttonLoadData.Click += new System.EventHandler(this.buttonLoadData_Click);
            //
            // FormMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 573);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FormMain";
            this.Text = "Neu";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabEmployee.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabEmployee;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBoxEmployees;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LiveCharts.WinForms.CartesianChart cartesianChartMain;
        private LiveCharts.WinForms.CartesianChart cartesianChartAnomaly;
        private LiveCharts.WinForms.PieChart pieChartVectors;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonLoadData;
        private System.Windows.Forms.Button buttonAnalyse;
    }
}

