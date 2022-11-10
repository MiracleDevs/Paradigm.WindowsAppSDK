using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace Paradigm.WindowsAppSDK.Xaml.Converters
{
    /// <summary>
    /// Converts from <see cref="bool"/> to a <see cref="Color"/>.
    /// </summary>
    /// <remarks>
    /// Two colors can be provided separated by a comma.
    /// The first color will be used for the true assignment, and the second
    /// for the false.
    /// </remarks>
    /// <seealso cref="IValueConverter" />
    public class ColorToSolidBrushConverter : IValueConverter
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
            if (value is Color color)
                return new SolidColorBrush(color);

            if (value is string colorString)
                return new SolidColorBrush((Color)XamlBindingHelper.ConvertValue(typeof(Color), colorString));

            return value;
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
            if (value is Color color)
                return color.ToString();

            return value;
        }
    }
}
