using System;

namespace PlotApp.Core.Formulas {
    internal class SofiaFormula : IFormula {
        public double Calculate(double x) {
            return x switch {
                >  2 and <  3  => Math.Exp(Math.Sqrt(2.0 * x + 2.0)) * (1.0 / 9.0 + Math.Log(Math.Sqrt(3.0 * x))) / (6.5 - x),
                > -2 and <= 2  => x * x,
                _              => x - 6.0
            };
        }
    }
}
