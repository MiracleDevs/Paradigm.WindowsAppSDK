using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace Paradigm.WindowsAppSDK.Xaml.Converters
{
    /// <summary>
    /// Converts from a <see cref="object"/> to a <see cref="Visibility"/>.
    /// </summary>
    /// <remarks>
    /// If the value is not null, returns <see cref="Visibility.Visible"/>, <see cref="Visibility.Collapsed"/> otherwise.
    /// </remarks>
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public class ObjectToVisibilityConverter : IValueConverter
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
            if (parameter is not null && parameter is string s && s == "negate")
                return value is not null ? Visibility.Collapsed : Visibility.Visible;

            return value is not null ? Visibility.Visible : Visibility.Collapsed;
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
            return null;
        }
    }
}