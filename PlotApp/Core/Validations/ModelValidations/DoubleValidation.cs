using System.Globalization;
using System.Text.RegularExpressions;

namespace PlotApp.Core.Validations.ModelValidations {
    public static class DoubleValidation {
        public static bool IsDouble(string str) {
            var a = !string.Empty.Equals(str);
            var b = double.PositiveInfinity.ToString(CultureInfo.InvariantCulture).Equals(str);
            var c = double.NegativeInfinity.ToString(CultureInfo.InvariantCulture).Equals(str);
            var d = Regex.IsMatch(str, $@"(^|^-)(\d+)(\.(\d+)$|\.$|$)");

            return a && (b || c || d);
        }
    }
}
