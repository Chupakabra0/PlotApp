using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

using Point = PlotApp.MVVM.Models.Point.Point;

namespace PlotApp.MVVM.ViewModels.CreatePlotViewModel {
    internal class CreatePlotViewModel : BaseViewModel.BaseViewModel {
        public CreatePlotViewModel() {
            this.Points = new ObservableCollection<Point>(
                new List<Point> { new(1.0, 1.0), new(2.0, 2.0), new(3.0, 3.0) }
            );
        }

        public ObservableCollection<Point> Points { get; set; }
    }
}
