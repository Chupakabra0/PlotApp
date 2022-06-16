using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using PlotApp.MVVM.Models.Dot;

namespace PlotApp.Core.Validations {
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
