using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.FunctionModifiers {
    internal interface IFunctionModifier {
        public List<Point>        Modify(List<Point> points);
        public IFunctionModifier? NextModifier { get; set; }
    }
}