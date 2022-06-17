using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using PlotApp.Core.FunctionModifiers;
using PlotApp.MVVM.Models.Dot;
using PlotApp.MVVM.Models.Function;

namespace PlotApp.Core.FunctionToSeriesCast
{
    internal class FunctionToSeriesCast {
        public FunctionToSeriesCast(Function function) {
            this.Function = function;
            this.modifier_ = new ScaleXModifier(this.Function.ScaleX,
                new ScaleYModifier(this.Function.ScaleY,
                    new WrapXModifier(this.Function.WrapX,
                        new WrapYModifier(this.Function.WrapY))));
        }

        public FunctionSeries GetSeries() {
            var series = new FunctionSeries();
            var points = this.PointConstuct(this.Function.Points);

            foreach (var point in points) {
                series.Points.Add(new(point.X, point.Y));
            }

            return series;
        }

        public Function Function { get; set; }

        private List<Point> PointConstuct(IEnumerable<Point> points) {
            var result = new List<Point>(points);
            this.modifier_.Modify(result);

            return result;
        }

        private IFunctionModifier modifier_;
    }
}
