using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.PointsBuilder {
    internal class PointsBuilder {
        public PointsBuilder(Func<double, double> function, (double, double) limitX, (double, double) limitY, double step) {
            this.Function = function;
            this.LimitX   = limitX;
            this.LimitY   = limitY;
            this.Step     = step;
        }

        public ObservableCollection<Point> BuildPoints() {
            var points = new ObservableCollection<Point>();

            for (var x = this.LimitX.Item1; x - this.LimitX.Item2 <= double.Epsilon; x += this.Step) {
                var y = this.Function(x);
                if (y >= this.LimitY.Item1 && y <= this.LimitY.Item2) {
                    points.Add(new(x, y));
                }
            }

            return points;
        }

        public Func<double, double> Function { get; set; }
        public (double, double)     LimitX   { get; set; }
        public (double, double)     LimitY   { get; set; }
        public double               Step     { get; set; }
    }
}