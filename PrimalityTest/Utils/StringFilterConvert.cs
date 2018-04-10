using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace PrimalityTest.Utils
{
    /// <summary>
    /// Filter uesr input data
    /// </summary>
    public class StringFilterConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                return Regex.Replace(input, @"[^\d]*", ""); /// Delete all non-digital characters
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                return Regex.Replace(input, @"[^\d]*", ""); /// Delete all non-digital characters
            }
            return "0";
        }

    }
}
