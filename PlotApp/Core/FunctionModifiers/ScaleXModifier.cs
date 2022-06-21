using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.FunctionModifiers {
    internal class ScaleXModifier : IFunctionModifier {
        public ScaleXModifier(decimal scaleX, IFunctionModifier? nextModifier = null) {
            this.ScaleX       = scaleX;
            this.NextModifier = nextModifier;
        }

        public List<Point> Modify(List<Point> points) {
            foreach (var point in points) {
                point.X *= this.ScaleX;
            }

            return this.NextModifier?.Modify(points) ?? points;
        }

        public IFunctionModifier? NextModifier { get; set; }
        public decimal             ScaleX       { get; set; }
    }
}
