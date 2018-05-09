using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using WinFormsApp.Model;
using WinFormsApp.Properties;
using WinFormsApp.Services;

namespace WinFormsApp
{
    public partial class FormMain : Form
    {
        private const int LearningWindowSize = 10;
        private readonly DataService _dataService;
        private readonly Dictionary<string, EmployeeAnalyseService> _employeeAnalyseServices;
        private readonly Brush _employeeBrush;
        private readonly Brush _notEmployeeBrush;
        private readonly SynchronizationContext _synchronizationContext;
        private EmployeeAnalyseService _currentEmployeeAnalyseService;
        private int _selectedEmployeeIndex;

        public FormMain()
        {
            InitializeComponent();
            _selectedEmployeeIndex = -1;
            _dataService = new DataService(Settings.Default.DataPath);
            _employeeAnalyseServices = new Dictionary<string, EmployeeAnalyseService>();
            _employeeBrush = new SolidColorBrush(Colors.WhiteSmoke);
            _notEmployeeBrush = new SolidColorBrush(Colors.AntiqueWhite);
            _currentEmployeeAnalyseService = null;
            _synchronizationContext = SynchronizationContext.Current;
        }

        public void EmployeeAnalyseServiceOnChangeCallback(object data)
        {
            var package = data as EmployeeDataPackageDto;

            if (package == null || package.EmployeeId != _currentEmployeeAnalyseService?.Data.EmployeeId)
                return;

            var anomaliesValues = package.AnomaliesDataSequence.Select(d => d.Value).ToArray();

            for (var i = 0; i < cartesianChartMain.Series[1].Values.Count; i++)
            {
                cartesianChartMain.Series[1].Values[i] = anomaliesValues[i];
                _currentEmployeeAnalyseService.Data.AnomaliesDataSequence[i].Value = anomaliesValues[i];
            }

            var analysingMasterValues = package.AnalysingDataSequenceMaster.Select(d => d.Value).ToArray();
            var toAdd = cartesianChartAnomaly.Series[0].Values.Count == 0;

            for (var i = 0; i < analysingMasterValues.Length; i++)
            {
                if(toAdd)
                    cartesianChartAnomaly.Series[0].Values.Add(analysingMasterValues[i]);
                else
                    cartesianChartAnomaly.Series[0].Values[i] = analysingMasterValues[i];
            }

            var analysingPredictedValues = package.AnalysingDataSequencePredicted.Select(d => d.Value).ToArray();
            toAdd = cartesianChartAnomaly.Series[1].Values.Count == 0;

            for (var i = 0; i < analysingPredictedValues.Length; i++)
            {
                if(toAdd)
                    cartesianChartAnomaly.Series[1].Values.Add(analysingPredictedValues[i]);
                else
                    cartesianChartAnomaly.Series[1].Values[i] = analysingPredictedValues[i];
            }

            _dataService.SaveEmployeeData(_currentEmployeeAnalyseService.Data);
        }

        public void EmployeeAnalyseServiceOnCompleteCallback(object employeeId)
        {
            if (!(employeeId is string employeeIdString))
                return;

            _employeeAnalyseServices.Remove(employeeIdString);

            if (_currentEmployeeAnalyseService?.Data.EmployeeId.ToString() != employeeIdString)
                return;

            InitCartesianChartAnomaly();
            //LoadCartesianChartAnomalySeriesMasterData(_currentEmployeeAnalyseService.Data);
            buttonAnalyse.Enabled = !_currentEmployeeAnalyseService.IsInProgress;
        }

        private void listBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ((ListBox)sender).SelectedIndex;

            if (index == _selectedEmployeeIndex)
                return;

            _selectedEmployeeIndex = index;
            OnSelectedEmployeeChanged();
        }

        private void LoadData()
        {
            var items = _dataService.GetList();
            listBoxEmployees.Items.Clear();
            listBoxEmployees.Items.AddRange(items);
        }

        private void OnSelectedEmployeeChanged()
        {
            var employeeData = GetSelectedEmployeeData();
            DisconnectCurrentEmployeeAnalyseService();
            ConnectCurrentEmployeeAnalyseService(employeeData);
        }

        private void DisconnectCurrentEmployeeAnalyseService()
        {
            buttonAnalyse.Enabled = false;
            _currentEmployeeAnalyseService = null;
        }

        private void ConnectCurrentEmployeeAnalyseService(EmployeeDataDto employeeData)
        {
            var employeeKey = employeeData.EmployeeId.ToString();

            if (_employeeAnalyseServices.ContainsKey(employeeKey))
                _currentEmployeeAnalyseService = _employeeAnalyseServices[employeeKey];
            else
                _currentEmployeeAnalyseService = new EmployeeAnalyseService(employeeData, employeeData.MasterDataSequence.Count, LearningWindowSize, _synchronizationContext, EmployeeAnalyseServiceOnChangeCallback, EmployeeAnalyseServiceOnCompleteCallback);

            buttonAnalyse.Enabled = !_currentEmployeeAnalyseService.IsInProgress;
            LoadSelectedEmployeeChartsData(_currentEmployeeAnalyseService.Data);
        }

        private void LoadSelectedEmployeeChartsData(EmployeeDataDto employeeData)
        {
            InitCartesianChartMain(employeeData.IsEmployee);
            InitCartesianChartAnomaly();
            LoadCartesianChartMainSeriesRawData(employeeData);
            //LoadCartesianChartAnomalySeriesMasterData(employeeData);
        }

        private void LoadCartesianChartMainSeriesRawData(EmployeeDataDto employeeData)
        {
            foreach (var value in employeeData.MasterDataSequence.Select(d => d.Value))
                cartesianChartMain.Series[0].Values.Add(value);

            foreach (var value in employeeData.AnomaliesDataSequence.Select(d => d.Value))
                cartesianChartMain.Series[1].Values.Add(value);

            foreach (var dateTime in employeeData.MasterDataSequence.Select(d => d.Key.Date))
                cartesianChartMain.AxisX[0].Labels.Add(dateTime.ToString("dd/MM/yyyy"));
        }

        //private void LoadCartesianChartAnomalySeriesMasterData(EmployeeDataDto employeeData)
        //{
        //    for (var i = 0; i < LearningWindowSize; i++)
        //    {
        //        var item = employeeData.MasterDataSequence[i];
        //        cartesianChartAnomaly.Series[0].Values.Add(item.Value);
        //        cartesianChartAnomaly.Series[1].Values.Add(double.NaN);
        //        cartesianChartAnomaly.AxisX[0].Labels.Add(item.Key.Date.ToString("dd/MM/yyyy"));
        //    }
        //}

        private void InitCartesianChartAnomaly()
        {
            cartesianChartAnomaly.Series.Clear();

            cartesianChartAnomaly.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Raw",
                    Values = new ChartValues<double>(),
                },
                new LineSeries
                {
                    Title = "Predicted",
                    Values = new ChartValues<double>()
                }
            };

            cartesianChartAnomaly.AxisX.Clear();
            cartesianChartAnomaly.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = new List<string>()
            });

            cartesianChartAnomaly.AxisY.Clear();
            cartesianChartAnomaly.AxisY.Add(new Axis
            {
                Title = "Dev (h)",
                LabelFormatter = value => value.ToString("F")
            });

            cartesianChartAnomaly.LegendLocation = LegendLocation.Top;
        }

        private void InitCartesianChartMain(bool isEmployee)
        {
            cartesianChartMain.Background = isEmployee ? _employeeBrush : _notEmployeeBrush;
            cartesianChartMain.Series.Clear();

            cartesianChartMain.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Raw",
                    Values = new ChartValues<double>(),
                },
                new LineSeries
                {
                    Title = "Anomaly",
                    Values = new ChartValues<double>()
                }
            };

            cartesianChartMain.AxisX.Clear();
            cartesianChartMain.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = new List<string>()
            });

            cartesianChartMain.AxisY.Clear();
            cartesianChartMain.AxisY.Add(new Axis
            {
                Title = "Dev (h)",
                LabelFormatter = value => value.ToString("F")
            });

            cartesianChartMain.LegendLocation = LegendLocation.Top;
        }

        private EmployeeDataDto GetSelectedEmployeeData()
        {
            var item = listBoxEmployees.Items[_selectedEmployeeIndex];
            return _dataService.GetEmployee(item.ToString());
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
            Update();
            _dataService.ReLoad(dialog.FileName);
            LoadData();
            UseWaitCursor = false;
        }

        private void buttonAnalyse_Click(object sender, EventArgs e)
        {
            buttonAnalyse.Enabled = false;

            if (!_employeeAnalyseServices.ContainsKey(_currentEmployeeAnalyseService.Data.EmployeeId.ToString()))
                _employeeAnalyseServices.Add(_currentEmployeeAnalyseService.Data.EmployeeId.ToString(), _currentEmployeeAnalyseService);

            _currentEmployeeAnalyseService.Start();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
