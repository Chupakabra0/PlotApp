using System.Collections.Generic;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.FunctionModifiers {
    internal class WrapYModifier : IFunctionModifier {
        public WrapYModifier(decimal wrapY, IFunctionModifier? nextModifier = null) {
            this.WrapY        = wrapY;
            this.NextModifier = nextModifier;
        }

        public List<Point> Modify(List<Point> points) {
            foreach (var point in points) {
                point.X += this.WrapY;
            }

            return this.NextModifier?.Modify(points) ?? points;
        }

        public IFunctionModifier? NextModifier { get; set; }
        public decimal             WrapY        { get; set; }
    }
}
