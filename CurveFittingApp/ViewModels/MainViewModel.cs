using CurveFittingApp.Models;
using CurveFittingApp.Services;
using CurveFittingApp.Utils;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using OxyPlot.Series;
using Microsoft.Win32;

namespace CurveFittingApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataFileService _dataFileService;
        private readonly ICurveFittingService _curveFittingService;

        // Parameterless constructor
        public MainViewModel() : this(new DataFileService(), new CurveFittingService())
        {
        }
        public MainViewModel(IDataFileService dataFileService, ICurveFittingService curveFittingService)
        {
            _dataFileService = dataFileService;
            _curveFittingService = curveFittingService;
            //LoadDataCommand = new RelayCommand(OnLoadData);
            LoadDataCommand = new RelayCommand(_ => OnLoadData());
            FitCurveCommand = new RelayCommand(OnFitCurve, CanFitCurve);
            FittingModels = new List<string> { "Linear", "Exponential", "Power" };
            PlotModel = new PlotModel();
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private List<DataPointModel> _dataPoints;
        public List<DataPointModel> DataPoints
        {
            get => _dataPoints;
            set => SetProperty(ref _dataPoints, value);
        }

        private List<string> _fittingModels;
        public List<string> FittingModels
        {
            get => _fittingModels;
            set => SetProperty(ref _fittingModels, value);
        }

        private string _selectedFittingModel;
        public string SelectedFittingModel
        {
            get => _selectedFittingModel;
            set => SetProperty(ref _selectedFittingModel, value);
        }

        private Tuple<double, double> _fittedCoefficients;


        public Tuple<double, double> FittedCoefficients
        {
            get => _fittedCoefficients;
            set => SetProperty(ref _fittedCoefficients, value);
           
        }
       
        private PlotModel _plotModel;
        public PlotModel PlotModel
        {
            get => _plotModel;
            set => SetProperty(ref _plotModel, value);
        }

        public ICommand LoadDataCommand { get; }
        public ICommand FitCurveCommand { get; }

        private void OnLoadData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                DefaultExt = ".csv",
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    DataPoints = _dataFileService.LoadDataFromFile(filePath);
                    FilePath = filePath;

                    if (DataPoints.Count == 0)
                    {
                        MessageBox.Show("No data points loaded. Please check the file format.", "Data Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    UpdatePlot();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "File Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnFitCurve(object _)
        {
            if (DataPoints == null || DataPoints.Count == 0) return;

            try
            {
                switch (SelectedFittingModel)
                {
                    case "Linear":
                        FittedCoefficients = _curveFittingService.FitLinear(DataPoints);             
                        break;
                    case "Exponential":
                        FittedCoefficients = _curveFittingService.FitExponential(DataPoints);
                        break;
                    case "Power":
                        FittedCoefficients = _curveFittingService.FitPowerFunction(DataPoints);
                        break;
                }

                UpdatePlot();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fitting curve: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanFitCurve(object _)
        {
            return DataPoints != null && DataPoints.Count > 0 && !string.IsNullOrEmpty(SelectedFittingModel);
        }

        private void UpdatePlot()
        {
            PlotModel.Series.Clear();
            if (DataPoints != null && DataPoints.Count > 0)
            {
                var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle, MarkerSize = 4 };
                foreach (var point in DataPoints)
                {
                    scatterSeries.Points.Add(new ScatterPoint(point.X, point.Y));
                }
                PlotModel.Series.Add(scatterSeries);
            }

            if (FittedCoefficients != null)
            {
                var lineSeries = new LineSeries();
                double minX = double.MaxValue;
                double maxX = double.MinValue;

                if (DataPoints != null)
                {
                    foreach (var point in DataPoints)
                    {
                        minX = Math.Min(minX, point.X);
                        maxX = Math.Max(maxX, point.X);
                    }
                }

                int numPoints = 100;
                double deltaX = (maxX - minX) / (numPoints - 1);
                double x = minX;

                for (int i = 0; i < numPoints; i++)
                {
                    double y = 0.0;
                    switch (SelectedFittingModel)
                    {
                        case "Linear":
                            y = FittedCoefficients.Item1 * x + FittedCoefficients.Item2;
                            break;
                        case "Exponential":
                            y = FittedCoefficients.Item1 * Math.Exp(FittedCoefficients.Item2 * x);
                            break;
                        case "Power":
                            y = FittedCoefficients.Item1 * Math.Pow(x, FittedCoefficients.Item2);
                            break;
                    }

                    lineSeries.Points.Add(new DataPoint(x, y));
                    x += deltaX;
                }

                PlotModel.Series.Add(lineSeries);
            }

            PlotModel.InvalidatePlot(true);
        }
    }
}
