using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using PlotApp.Core.Commands.RelayCommand;
using PlotApp.MVVM.Views.CreatePlotView;


namespace PlotApp.MVVM.ViewModels.PlotViewModel {
    internal class PlotViewModel : BaseViewModel.BaseViewModel {
        public PlotViewModel() {
            this.Model.Series.Add(this.Area);
            //this.Model.PlotView.InvalidatePlot();
        }

        public ICommand AddPlotCommand => 
            new RelayCommand(_ => {
                var window = new CreatePlotView();

                if (!window.ShowDialog() ?? false) {
                    var points = (window.DataContext as CreatePlotViewModel.CreatePlotViewModel)?.Points ?? new();

                    foreach (var point in points) {
                        this.Area.Points.Add(new(point.X, point.Y));
                    }

                    //this.Model.Series.Add(this.Area);
                    this.Model.PlotView.InvalidatePlot();

                    //this.Area.Points.Clear();

                    MessageBox.Show("Ready");
                }
            });

        public PlotModel Model { get; set; } = new PlotModel {
            Series = { new AreaSeries() {
                Points = { new DataPoint(0, 0) }
            } },
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

        public FunctionSeries Area { get; set; } = new();
    }
}