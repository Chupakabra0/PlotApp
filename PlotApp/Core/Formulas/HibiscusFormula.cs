using System;

namespace PlotApp.Core.Formulas {
    internal class HibiscusFormula : IFormula {
        public double Calculate(double x) {
            return x switch {
                > -4 and < -2 => 6.0 * x - 2.0 / Math.Tan(0.7 * x) + Math.Log(Math.Cos(2.0 * x)) / (2.0 * x + 4.0),
                >  3 and <= 5 => Math.Sqrt(x),
                _ => x + 4.0
            };
        }
    }
}
