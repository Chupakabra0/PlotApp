using System;
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

                    this.UpdateAllPlots();
                }
            });

        public ICommand DeletePlotCommand =>
            new RelayCommand(o => {
                if (o is FunctionWrapper plot) {
                    var result = MessageBox.Show("Are you sure you want to delete {}?", "Delete",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

                    if (result == MessageBoxResult.Yes) {
                        this.DeletePlot(plot);
                    }

                    this.UpdateAllPlots();
                }
                else {
                    throw new Exception("Unknown object");
                }
            });

        public ICommand EditPlotCommand =>
            new RelayCommand(o => {
                if (o is FunctionWrapper plot) {
                    var window = new CreatePlotView {
                        DataContext = new CreatePlotViewModel.CreatePlotViewModel(
                             plot.Function.Points, plot.ScaleX, plot.ScaleY, plot.WrapX, plot.WrapY, plot.Name
                        )
                    };

                    if (!window.ShowDialog() ?? false) {
                        var context = window.DataContext as CreatePlotViewModel.CreatePlotViewModel;

                        var points = context?.Points ?? new();

                        var scaleX = context?.ScaleX ?? 1.0;
                        var scaleY = context?.ScaleY ?? 1.0;

                        var wrapX = context?.WrapX ?? 0.0;
                        var wrapY = context?.WrapY ?? 0.0;

                        var name = context?.Name ?? string.Empty;

                        this.EditPlot(plot, new FunctionWrapper(new Function(
                            new List<Point>(points), scaleX, scaleY, wrapX, wrapY, name)
                        ));

                        this.UpdateAllPlots();
                    }
                }
                else {
                    throw new Exception("Unknown object");
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
                    Title                  = "X",
                    MajorGridlineColor     = OxyColors.LightGray,
                    MajorGridlineStyle     = LineStyle.Dot
                },
                new LinearAxis {
                    Position               = AxisPosition.Left,
                    ExtraGridlines         = new double[] { 0 },
                    ExtraGridlineThickness = 1,
                    ExtraGridlineColor     = OxyColors.Black,
                    ExtraGridlineStyle     = LineStyle.Solid,
                    Title                  = "Y",
                    MajorGridlineColor     = OxyColors.LightGray,
                    MajorGridlineStyle     = LineStyle.Dot
                }
            }
        };

        public ObservableCollection<FunctionWrapper> Functions { get; set; } = new();

        private void DeletePlot(FunctionWrapper o) {
            var index = this.Functions.IndexOf(o);

            if (index > -1 && index < this.Functions.Count) {
                this.Functions.RemoveAt(index);
            }
        }

        private void EditPlot(FunctionWrapper dest, FunctionWrapper source) {
            var index = this.Functions.IndexOf(dest);

            if (index > -1 && index < this.Functions.Count) {
                this.Functions[index] = source;
            }
        }

        private void UpdateAllPlots() {
            this.Model.Series.Clear();

            foreach (var f in this.Functions) {
                this.Model.Series.Add(f.GetSeries());
            }

            this.Model.PlotView.InvalidatePlot();
        }
    }
}
