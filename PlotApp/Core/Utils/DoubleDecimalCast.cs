namespace PlotApp.Core.Utils {
    internal static class DoubleDecimalCast {
        public static double CastToDouble(decimal x) {
            try {
                return x switch {
                    // yeah, we need exactly equality, not "epsilon check"
                    decimal.MaxValue => double.NaN,
                    decimal.MinValue => double.NaN,
                    _                => (double)x
                };
            }
            catch {
                return double.NaN;
            }
        }

        public static decimal CastToDecimal(double x) {
            var DECIMAL_EPSILON = new decimal(1, 0, 0, false, 27);

            try {
                return x switch {
                    // yeah, we need exactly equality, not "epsilon check"
                    double.Epsilon          => DECIMAL_EPSILON,
                    double.PositiveInfinity => decimal.MaxValue,
                    double.NegativeInfinity => decimal.MinValue,
                    _                       => (decimal)x
                };
            }
            catch {
                return decimal.MaxValue;
            }
        }
    }
}
