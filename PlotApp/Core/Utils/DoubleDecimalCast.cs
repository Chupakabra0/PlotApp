using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotApp.Core.Utils
{
    internal static class DoubleDecimalCast {
        public static double CastToDouble(decimal x) {
            return (double)x;
        }

        public static decimal CastToDecimal(double x) {
            var DECIMAL_EPSILON = new decimal(1, 0, 0, false, 27);

            // yeah, we need exactly equality, not "epsilon check"

            if (x == double.Epsilon) {
                // decimal epsilon
                return DECIMAL_EPSILON;
            }

            if (x == double.PositiveInfinity) {
                // decimal infinity
                return 1M / DECIMAL_EPSILON;
            }

            if (x == double.NegativeInfinity) {
                // decimal -infinity
                return -1M / DECIMAL_EPSILON;
            }

            return (decimal)x;
        }
    }
}
