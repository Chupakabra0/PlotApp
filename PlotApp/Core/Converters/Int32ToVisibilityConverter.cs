using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PlotApp.Core.Converters {
    internal class Int32ToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (int)value == 0 ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (Visibility)value == Visibility.Collapsed ? 0 : 1;
    }
}