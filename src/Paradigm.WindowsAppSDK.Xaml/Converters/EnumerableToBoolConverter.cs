using Microsoft.UI.Xaml.Data;
using System;
using System.Collections;

namespace Paradigm.WindowsAppSDK.Xaml.Converters
{
    /// <summary>
    /// Converts from a <see cref="IEnumerable"/> to a <see cref="bool"/>, checking if value is null or empty.
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public class EnumerableToBoolConverter : IValueConverter
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
            var hasItems = false;

            if (value != null && value is IEnumerable)
                hasItems = EnumerableCount(value as IEnumerable) > 0;

            var negate = parameter != null && !string.IsNullOrWhiteSpace(parameter.ToString()) && parameter.ToString() == "negate";

            return negate ? !hasItems : hasItems;
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
        /// Enumerables the count.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static int EnumerableCount(IEnumerable source)
        {
            int c = 0;
            var e = source.GetEnumerator();

            while (e.MoveNext())
                c++;

            return c;
        }
    }
}