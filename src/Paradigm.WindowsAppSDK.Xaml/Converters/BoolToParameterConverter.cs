using Microsoft.UI.Xaml.Data;
using System;
using System.ComponentModel;
using System.Linq;

namespace Paradigm.WindowsAppSDK.Xaml.Converters
{
    /// <summary>
    /// If the value is true, this converter will return the value of the parameter, null otherwise.
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public class BoolToParameterConverter : IValueConverter
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
            if (value is bool b)
            {
                if (parameter is string s)
                {
                    var parameters = s.Split('|').Select(x => ConvertParameterToType(x, targetType)).ToArray();

                    return b
                        ? parameters[0]
                        : parameters.Length > 1 ? parameters[1] : null;
                }

                return b ? parameter : null;
            }

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
            return null;
        }

        /// <summary>
        /// Converts the type of the parameter to.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        private object ConvertParameterToType(object value, Type targetType)
        {
            var converter = TypeDescriptor.GetConverter(targetType);
            return converter.CanConvertFrom(typeof(string)) ? converter.ConvertFrom(value) : value;
        }
    }
}
