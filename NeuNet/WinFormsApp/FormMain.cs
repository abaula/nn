using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using WinFormsApp.Properties;
using WinFormsApp.Services;

namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        private int _selectedEmployeeIndex;
        private readonly DataService _dataService;
        private readonly Random _rnd;

        public FormMain()
        {
            InitializeComponent();
            _selectedEmployeeIndex = -1;
            _dataService = new DataService(Settings.Default.DataPath);
            _rnd = new Random();
        }

        private void listBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ((ListBox)sender).SelectedIndex;

            if (index == _selectedEmployeeIndex)
                return;

            _selectedEmployeeIndex = index;
            OnSelectedEmployeeChanged();
        }

        private void OnSelectedEmployeeChanged()
        {
            cartesianChartMain.Series.Clear();


            var values = new List<double>();
            for(var i = 0; i < 60; i++)
                values.Add(-10.0 * _rnd.NextDouble() + 10.0);


            cartesianChartMain.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double>(values) // {4, 6, 5, 2, 7, 3, 2}
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> {6, 7, 3, 4, 6},
                    //PointGeometry = DefaultGeometries.Triangle,
                    PointGeometrySize = 5
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> {5, 2, 8, 3},
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                }
            };

            cartesianChartMain.AxisX.Clear();
            cartesianChartMain.AxisX.Add(new Axis
            {
                Title = "Month",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
            });

            cartesianChartMain.AxisY.Clear();
            cartesianChartMain.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("C")
            });

            cartesianChartMain.LegendLocation = LegendLocation.Top;
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true
            };

            var result = dialog.ShowDialog(this);

            if (result != DialogResult.OK)
                return;

            UseWaitCursor = true;
            _dataService.Load(dialog.FileName);
            UseWaitCursor = false;
        }

        private void cartesianChartMain_DataClick(object sender, ChartPoint chartPoint)
        {

            cartesianChartMain.Series[0].Values.Add(-10.0 * _rnd.NextDouble() + 10.0);
        }

        private void buttonAnalyse_Click(object sender, EventArgs e)
        {

        }
    }
}
