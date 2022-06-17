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
                    var points = (window.DataContext as CreatePlotViewModel.CreatePlotViewModel)?.Points ?? new();
                    
                    this.Functions.Add(new FunctionWrapper(
                        new Function(new List<Point>(points), 1, 1, 0, 0)
                    ));

                    this.Model.Series.Clear();

                    foreach (var f in this.Functions) {
                        this.Model.Series.Add(f.GetSeries());
                    }

                    this.Model.PlotView.InvalidatePlot();
                }
            });

        public PlotModel Model { get; set; } = new PlotModel {
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
    }
}