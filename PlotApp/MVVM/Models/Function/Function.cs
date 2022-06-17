using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.MVVM.Models.Function {
    internal class Function {
        public Function() {
            
        }

        public string          Name   { get; set; } = string.Empty;
        public double          ScaleX { get; set; } = 1.0;
        public double          ScaleY { get; set; } = 1.0;
        public double          WrapX  { get; set; } = 0.0;
        public double          WrapY  { get; set; } = 0.0;
        public List<Point> Points { get; set; }
    }
}