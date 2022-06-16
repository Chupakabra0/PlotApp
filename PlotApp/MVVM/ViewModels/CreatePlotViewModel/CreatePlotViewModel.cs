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
using Point = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.CreatePlotViewModel {
    internal class CreatePlotViewModel : BaseViewModel.BaseViewModel {
        public CreatePlotViewModel() {
            this.Points = new ObservableCollection<Point>(
                new List<Point> { new(1.0, 1.0), new(2.0, 2.0), new(3.0, 3.0) }
            );
            this.dataManipulator_ = new JsonDataManipulator();
        }

        public ObservableCollection<Point> Points { get; set; }

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

        private readonly IDataManipulator dataManipulator_;
    }
}
