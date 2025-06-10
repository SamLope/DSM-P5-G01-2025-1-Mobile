// Converters/DoubleToStringConverter.cs
using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Mobile.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return d.ToString(CultureInfo.InvariantCulture); // Garante formato consistente (usa ponto como decimal)
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            return null; // Retorna null para double? se a conversão falhar ou o string estiver vazio
        }
    }
}