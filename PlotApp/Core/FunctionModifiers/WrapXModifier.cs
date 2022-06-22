using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.FunctionModifiers {
    internal class WrapXModifier : IFunctionModifier {
        public WrapXModifier(decimal wrapX, IFunctionModifier? nextModifier = null) {
            this.WrapX        = wrapX;
            this.NextModifier = nextModifier;
        }

        public List<Point> Modify(List<Point> points) {
            foreach (var point in points) {
                point.X += this.WrapX;
            }

            return this.NextModifier?.Modify(points) ?? points;
        }

        public IFunctionModifier? NextModifier { get; set; }
        public decimal WrapX { get; set; }
    }
}
