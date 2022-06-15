using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotApp.MVVM.Models.Point {
    internal class Point {
        public Point() {
            this.X = null;
            this.Y = null;
        }

        public Point(double x, double y) {
            this.X = x;
            this.Y = y;
        }

        public double? X { get; set; }
        public double? Y { get; set; }
    }
}
