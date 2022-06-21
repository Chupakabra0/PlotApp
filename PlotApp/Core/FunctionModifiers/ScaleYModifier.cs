using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.FunctionModifiers {
    internal class ScaleYModifier : IFunctionModifier {
        public ScaleYModifier(decimal scaleY, IFunctionModifier? nextModifier = null) {
            this.ScaleY       = scaleY;
            this.NextModifier = nextModifier;
        }

        public List<Point> Modify(List<Point> points) {
            foreach (var point in points) {
                point.Y *= this.ScaleY;
            }

            return this.NextModifier?.Modify(points) ?? points;
        }

        public IFunctionModifier? NextModifier { get; set; }
        public decimal             ScaleY       { get; set; }
    }
}
