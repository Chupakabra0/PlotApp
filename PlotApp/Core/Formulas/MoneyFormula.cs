using System;

namespace PlotApp.Core.Formulas {
    internal class MoneyFormula : IFormula {
        public double Calculate(double x) {
            return x switch {
                >  1 and <  2 => (Math.Sqrt(8.0 * x) * Math.Sin(5 * x * x) - 4.3)
                                 / (Math.Pow(x, 1.0 / 3.0) + Math.Exp(2.0 * x) + Math.Abs(Math.Sin(2.0 * x))),
                > -3 and <= 1 => Math.Pow(x, -2.0) - 2.0,
                _             => Math.Pow(x, -2.0) - 4.0
            };
        }
    }
}
