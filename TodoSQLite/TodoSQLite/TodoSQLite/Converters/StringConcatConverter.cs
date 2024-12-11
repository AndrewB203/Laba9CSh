using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace TodoSQLite.Converters
{
    public class StringConcatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return value;

            var propertyInfo = value.GetType().GetProperty(parameter.ToString());
            if (propertyInfo == null)
                return value;

            var secondValue = propertyInfo.GetValue(value, null);
            return $"{value} {secondValue}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}