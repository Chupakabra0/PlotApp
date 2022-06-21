using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.DataManipulators.IDataManipulator;
using PlotApp.Core.Validations.ModelValidations;
using PlotApp.MVVM.Views.FillFunctionView;

using Point = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.CreatePlotViewModel {
    internal class CreatePlotViewModel : BaseViewModel.BaseViewModel {
        public CreatePlotViewModel() {
            
        }

        public CreatePlotViewModel(List<Point> points, double scaleX, double scaleY, double wrapX, double wrapY, double tension, string name) {
            this.Points       = new(points);
            this.ScaleXString = scaleX.ToString(CultureInfo.InvariantCulture);
            this.ScaleYString = scaleY.ToString(CultureInfo.InvariantCulture);
            this.WrapXString  = wrapX.ToString(CultureInfo.InvariantCulture);
            this.WrapYString  = wrapY.ToString(CultureInfo.InvariantCulture);
            this.Name         = name;
            this.Tension      = (decimal)tension;
        }

        public ObservableCollection<Point> Points { get; set; } = new();

        public string ScaleXString { get; set; } = "1.0";
        public string ScaleYString { get; set; } = "1.0";
        public string WrapXString  { get; set; } = "0.0";
        public string WrapYString  { get; set; } = "0.0";
        public string Name         { get; set; } = "Plot";
        public decimal Tension      { get; set; } = (decimal)0.0;

        public ICommand SaveToFileCommand =>
            new RelayCommand(_ => {
                var dialog = new SaveFileDialog {
                    Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
                };

                if (dialog.ShowDialog() ?? false) {
                    var s = this.dataManipulator_.DataSerialize(this.Points);
                    using var sw = new StreamWriter(dialog.FileName);
                    sw.Write(s);
                }

                MessageBox.Show("Ready");
            });

        public ICommand AddFromFileCommand =>
            new RelayCommand(_ => {
                var dialog = new OpenFileDialog() {
                    Filter = "json files (*.json)|*.json|All files (*.*)|*.*"
                };

                if (dialog.ShowDialog() ?? false) {
                    using var sr = new StreamReader(dialog.FileName);
                    var data = sr.ReadToEnd();
                    var points = this.dataManipulator_.DataDeserialize(data);
                        
                    foreach (var point in points) {
                        Points.Add(point);
                    }
                }

                MessageBox.Show("Ready");
            });

        public ICommand FillFunctionCommand =>
            new RelayCommand(_ => {
                var window = new FillFunctionView();

                if (window.ShowDialog() ?? false) {
                    var points = (window.DataContext as FillFunctionViewModel.FillFunctionViewModel)?.Points ?? new();

                    foreach (var point in points) {
                        this.Points.Add(point);
                    }

                    MessageBox.Show("Ready");
                }
            });

        public ICommand DoneCommand => new RelayCommand(null, _ => this.CheckAllStrings());

        public double ScaleX => double.Parse(this.ScaleXString, CultureInfo.InvariantCulture);
        public double ScaleY => double.Parse(this.ScaleYString, CultureInfo.InvariantCulture);
        public double WrapX  => double.Parse(this.WrapXString,  CultureInfo.InvariantCulture);
        public double WrapY  => double.Parse(this.WrapYString,  CultureInfo.InvariantCulture);

        private readonly IDataManipulator dataManipulator_ = new JsonDataManipulator();

        private bool CheckAllStrings() =>
            DoubleValidation.IsDouble(this.ScaleXString) && DoubleValidation.IsDouble(this.ScaleYString)
            && DoubleValidation.IsDouble(this.WrapXString) && DoubleValidation.IsDouble(this.WrapYString)
            && !this.Name.Equals(string.Empty);
    }
}
