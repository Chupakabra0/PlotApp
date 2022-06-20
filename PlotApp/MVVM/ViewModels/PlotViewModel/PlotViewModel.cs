using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.FunctionWrapper;
using PlotApp.MVVM.Models.Function;
using PlotApp.MVVM.Views.CreatePlotView;

using Point = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.PlotViewModel {
    internal class PlotViewModel : BaseViewModel.BaseViewModel {
        public PlotViewModel() {
            //this.Model.PlotView.InvalidatePlot();
        }

        public ICommand AddPlotCommand => 
            new RelayCommand(_ => {
                var window = new CreatePlotView();

                if (!window.ShowDialog() ?? false) {
                    var context = window.DataContext as CreatePlotViewModel.CreatePlotViewModel;
                    
                    var points = context?.Points ?? new();

                    var scaleX = context?.ScaleX ?? 1.0;
                    var scaleY = context?.ScaleY ?? 1.0;

                    var wrapX = context?.WrapX ?? 0.0;
                    var wrapY = context?.WrapY ?? 0.0;

                    this.Functions.Add(new FunctionWrapper(new Function(
                        new List<Point>(points), scaleX, scaleY, wrapX, wrapY, "Temp")
                    ));

                    this.Model.Series.Clear();

                    foreach (var f in this.Functions) {
                        this.Model.Series.Add(f.GetSeries());
                    }

                    this.Model.PlotView.InvalidatePlot();
                }
            });

        public ICommand DeletePlotCommand =>
            new RelayCommand(o => {
                var result = MessageBox.Show("Are you sure you want to delete {}?", "Delete", MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes) {
                    this.DeletePlot(o);
                }  
            });

        public PlotModel Model { get; set;  } = new PlotModel {
            PlotType = PlotType.Cartesian,
            Axes = {
                new LinearAxis {
                    Position               = AxisPosition.Bottom,
                    ExtraGridlines         = new double[] { 0 },
                    ExtraGridlineThickness = 1,
                    ExtraGridlineColor     = OxyColors.Black,
                    ExtraGridlineStyle     = LineStyle.Solid,
                    Title                  = "X"
                },
                new LinearAxis {
                    Position               = AxisPosition.Left,
                    ExtraGridlines         = new double[] { 0 },
                    ExtraGridlineThickness = 1,
                    ExtraGridlineColor     = OxyColors.Black,
                    ExtraGridlineStyle     = LineStyle.Solid,
                    Title                  = "Y"
                }
            }
        };

        public ObservableCollection<FunctionWrapper> Functions { get; set; } = new();

        private void DeletePlot(object o) {
            var index = this.Functions.IndexOf(o as FunctionWrapper);

            if (index > -1 && index < this.Functions.Count) {
                this.Functions.RemoveAt(index);
            }
        }
    }
}