using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;

namespace Paradigm.WindowsAppSDK.Xaml.Converters
{
    /// <summary>
    /// Converts from a <see cref="bool"/> to a <see cref="ZoomMode"/>.
    /// </summary>
    /// <remarks>
    /// If the value it's true, returns <see cref="ZoomMode.Enabled"/>, <see cref="ZoomMode.Disabled"/> otherwise.
    /// </remarks>
    /// <seealso cref="IValueConverter" />
    public class BoolToZoomModeConverter : IValueConverter
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
                return value is bool b && !b ? ZoomMode.Enabled : ZoomMode.Disabled;

            return value is bool b1 && b1 ? ZoomMode.Enabled : ZoomMode.Disabled;
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
            if (value is ZoomMode v)
                return v == ZoomMode.Enabled && parameter is not null ? parameter : null;

            return value;
        }
    }
}
