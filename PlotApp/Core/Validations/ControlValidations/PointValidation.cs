using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.Validations.ControlValidations {
    internal class PointValidation : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            var point = (value as BindingGroup)?.Items[0] as Point;

            if (point?.X == null || point?.Y == null) {
                return new ValidationResult(false, "Coordinate value cannot be null");
            }

            return ValidationResult.ValidResult;
        }
    }
}
