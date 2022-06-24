using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.Core.FunctionType;
using PlotApp.Core.FunctionWrapper;
using PlotApp.MVVM.Models.Function;
using PlotApp.MVVM.Views.CreatePlotView;

using Point = PlotApp.MVVM.Models.Dot.Point;

namespace PlotApp.MVVM.ViewModels.PlotViewModel {
    internal class PlotViewModel : BaseViewModel.BaseViewModel {
        public PlotViewModel() {
            this.DrawCenter();
        }

        public ICommand AddPlotCommand => 
            new RelayCommand(_ => {
                var window = new CreatePlotView();

                if (window.ShowDialog() ?? false) {
                    var context = window.DataContext as CreatePlotViewModel.CreatePlotViewModel;
                    
                    var points = context?.Points ?? new();

                    var scaleX = context?.ScaleX ?? 1.0M;
                    var scaleY = context?.ScaleY ?? 1.0M;

                    var wrapX = context?.WrapX ?? 0.0M;
                    var wrapY = context?.WrapY ?? 0.0M;

                    var tension = context?.Tension ?? 0.0M;
                    var type    = context?.Type    ?? FunctionType.Point;

                    var name  = context?.Name ?? string.Empty;

                    this.Functions.Add(new FunctionWrapper(new Function(
                        new List<Point>(points), scaleX, scaleY, wrapX, wrapY, name), type, tension
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
                             plot.Function.Points, plot.ScaleX, plot.ScaleY,
                             plot.WrapX, plot.WrapY, plot.Tension, plot.Type, plot.Name
                        )
                    };

                    if (window.ShowDialog() ?? false) {
                        var context = window.DataContext as CreatePlotViewModel.CreatePlotViewModel;

                        var points = context?.Points ?? new();

                        var scaleX = context?.ScaleX ?? 1.0M;
                        var scaleY = context?.ScaleY ?? 1.0M;

                        var wrapX = context?.WrapX ?? 0.0M;
                        var wrapY = context?.WrapY ?? 0.0M;

                        var tension = context?.Tension ?? 0.0M;
                        var type    = context?.Type    ?? FunctionType.Point;

                        var name = context?.Name ?? string.Empty;

                        this.EditPlot(plot, new FunctionWrapper(new Function(
                            new List<Point>(points), scaleX, scaleY, wrapX, wrapY, name), type, tension
                        ));

                        this.UpdateAllPlots();
                    }
                }
                else {
                    throw new Exception("Unknown object");
                }
            });

        public ICommand HelpCommand =>
            new RelayCommand(_ => {
                var path = Path.Combine(Environment.CurrentDirectory, @"Guide\guide.pdf");
                var process = new Process {
                    StartInfo = new ProcessStartInfo {
                        UseShellExecute = true,
                        FileName = path
                    }
                };

                process.Start();
            });

        public PlotModel Model { get; set;  } = new PlotModel {
            PlotType = PlotType.Cartesian,
            Axes = {
                new LinearAxis {
                    Position               = AxisPosition.Bottom,
                    ExtraGridlines         = new[]{ 0.0 },
                    ExtraGridlineThickness = 3,
                    ExtraGridlineColor     = OxyColors.Black,
                    ExtraGridlineStyle     = LineStyle.Solid,
                    Title                  = "X",
                    MajorGridlineColor     = OxyColors.LightGray,
                    MajorGridlineStyle     = LineStyle.Dot,
                    MajorGridlineThickness = 1
                },
                new LinearAxis {
                    Position               = AxisPosition.Left,
                    ExtraGridlines         = new[]{ 0.0 },
                    ExtraGridlineThickness = 3,
                    ExtraGridlineColor     = OxyColors.Black,
                    ExtraGridlineStyle     = LineStyle.Solid,
                    Title                  = "Y",
                    MajorGridlineColor     = OxyColors.LightGray,
                    MajorGridlineStyle     = LineStyle.Dot,
                    MajorGridlineThickness = 1
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
                this.Model.Series.Add(f.GetLineSeries());
            }
            this.DrawCenter();

            this.Model.PlotView.InvalidatePlot();
        }

        private void DrawCenter() {
            this.Model.Series.Add(this.center_);
        }

        private readonly LineSeries center_ = new() {
            Points     = { new DataPoint(0, 0) },
            LineStyle  = LineStyle.None,
            MarkerType = MarkerType.Circle,
            Color      = OxyColors.Red,
            Title      = "O"
        };
    }
}
