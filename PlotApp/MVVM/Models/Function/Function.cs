using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.MVVM.Models.Function {
    internal class Function {
        public Function(IEnumerable<Point> points, decimal scaleX, decimal scaleY, decimal wrapX, decimal wrapY, string name = "") {
            this.Points = new List<Point>(points);
            this.ScaleX = scaleX;
            this.ScaleY = scaleY;
            this.WrapX  = wrapX;
            this.WrapY  = wrapY;
            this.Name   = name;
        }

        public string      Name   { get; set; } = string.Empty;
        public decimal      ScaleX { get; set; } = 1.0M;
        public decimal      ScaleY { get; set; } = 1.0M;
        public decimal      WrapX  { get; set; } = 0.0M;
        public decimal      WrapY  { get; set; } = 0.0M;
        public List<Point> Points { get; set; } = new ();
    }
}