using Microsoft.UI.Xaml.Data;
using System;

namespace Paradigm.WindowsAppSDK.Xaml.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null || value is not DateTime date)
                return default;

            //Date formats https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=net-7.0
            var format = parameter?.ToString() ?? "d";

            return date.ToString(format);
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var stringValue = value?.ToString();

            if (!string.IsNullOrWhiteSpace(stringValue) && DateTime.TryParse(stringValue, out var date))
                return date;

            return null;
        }
    }
}