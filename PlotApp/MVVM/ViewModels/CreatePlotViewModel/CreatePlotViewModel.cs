using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.DataManipulators.IDataManipulator;
using PlotApp.MVVM.Views.FillFunctionView;

using Point = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.CreatePlotViewModel {
    internal class CreatePlotViewModel : BaseViewModel.BaseViewModel {
        public CreatePlotViewModel() {
            
        }

        public CreatePlotViewModel(List<Point> points, double scaleX, double scaleY, double wrapX, double wrapY, string name = "") {
            this.Points = new(points);
            this.ScaleX = scaleX;
            this.ScaleY = scaleY;
            this.WrapX = wrapX;
            this.WrapY = wrapY;
            this.Name = name;
        }

        public ObservableCollection<Point> Points { get; set; } = new();

        public double ScaleX { get; set; } = 1.0;
        public double ScaleY { get; set; } = 1.0;
        public double WrapX  { get; set; } = 0.0;
        public double WrapY  { get; set; } = 0.0;
        public string Name   { get; set; } = string.Empty;

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

                if (!window.ShowDialog() ?? false) {
                    var points = (window.DataContext as FillFunctionViewModel.FillFunctionViewModel)?.Points ?? new();

                    foreach (var point in points) {
                        this.Points.Add(point);
                    }

                    MessageBox.Show("Ready");
                }
            });

        private readonly IDataManipulator dataManipulator_ = new JsonDataManipulator();
    }
}
