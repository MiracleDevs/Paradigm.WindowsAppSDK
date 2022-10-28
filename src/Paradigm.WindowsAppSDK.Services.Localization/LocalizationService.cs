using System.Collections;
using System.Reflection;

namespace Paradigm.WindowsAppSDK.Services.Localization
{
    /// <summary>
    /// Extracts all the strings and translatable values from a layout to a dictionary,
    /// and apply back those values over a layout instance.
    /// </summary>
    /// <seealso cref="ILocalizationService" />
    public class LocalizationService : ILocalizationService
    {
        #region Public Methods

        /// <summary>
        /// Extracts the translation strings.
        /// </summary>
        /// <typeparam name="TModel">The type of the layout.</typeparam>
        /// <param name="model">The layout.</param>
        /// <param name="prefix"></param>
        /// <returns>A dictionary of key values.</returns>
        public Dictionary<string, string> ExtractLocalizableStrings<TModel>(TModel model, string prefix)
        {
            var strings = new Dictionary<string, string>();
            this.ExtractStrings(model, prefix, strings);

            return strings;
        }

        /// <summary>
        /// Applies the translations.
        /// </summary>
        /// <typeparam name="TModel">The type of the layout.</typeparam>
        /// <param name="model">The layout.</param>
        /// <param name="strings">The strings.</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public TModel ApplyLocalizableStrings<TModel>(TModel model, Dictionary<string, string> strings, string prefix)
        {
            this.ApplyStrings(model, strings, prefix);
            return model;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Extracts the strings.
        /// </summary>
        /// <param name="model">The layout.</param>
        /// <param name="name">The name.</param>
        /// <param name="strings">The strings.</param>
        private void ExtractStrings(object model, string name, Dictionary<string, string> strings)
        {
            if (model == null)
                return;

            var type = model.GetType().GetTypeInfo();
            var properties = type.GetProperties();

            if (model is IList collection && type != typeof(string))
            {
                for (var i = 0; i < collection.Count; i++)
                {
                    var item = collection[i];
                    ExtractStrings(item, $"{name}.{i}", strings);
                }
            }
            else if (model is IDictionary dictionary && type != typeof(string))
            {
                foreach (var key in dictionary.Keys)
                {
                    var item = dictionary[key];
                    ExtractStrings(item, $"{name}.{key}", strings);
                }
            }
            else
            {
                foreach (var property in properties)
                {
                    var propertyName = $"{name}.{property.Name}";

                    if (property.PropertyType == typeof(string) && property.GetCustomAttribute(typeof(LocalizableAttribute)) == null)
                        continue;

                    if (property.PropertyType == typeof(char))
                        continue;

                    if (property.GetCustomAttribute(typeof(LocalizableAttribute)) != null)
                        strings.Add(propertyName, property.GetValue(model) as string);
                    else
                        ExtractStrings(property.GetValue(model), propertyName, strings);
                }
            }
        }

        /// <summary>
        /// Applies the strings.
        /// </summary>
        /// <param name="model">The layout.</param>
        /// <param name="strings">The strings.</param>
        /// <param name="prefix">The prefix.</param>
        private void ApplyStrings(object model, Dictionary<string, string> strings, string prefix)
        {
            foreach (var localizableKey in strings.Keys)
            {
                if (!localizableKey.StartsWith(prefix))
                    continue;

                var localizableValue = strings[localizableKey];
                var keyParts = localizableKey.Split('.');
                var childModel = model;

                for (var index = 1; index < keyParts.Length; index++)
                {
                    if (childModel == null)
                        continue;

                    var keyPart = keyParts[index];

                    if (int.TryParse(keyPart, out var itemIndex) && childModel is IList list)
                    {
                        if (itemIndex >= list.Count)
                            continue;

                        if (index == keyParts.Length - 1)
                            list[itemIndex] = localizableValue;
                        else
                            childModel = list[itemIndex];
                    }
                    else if (keyPart is string && childModel is IDictionary dictionary)
                    {
                        var item = dictionary[keyPart];
                        if (item == null)
                            continue;

                        if (index == keyParts.Length - 1)
                            item = localizableValue;
                        else
                            childModel = item;
                    }
                    else
                    {
                        var property = childModel.GetType().GetTypeInfo().GetProperties().FirstOrDefault(x => x.Name == keyPart);

                        if (property == null)
                            continue;

                        if (index == keyParts.Length - 1)
                            property.SetValue(childModel, localizableValue);
                        else
                            childModel = property.GetValue(childModel);
                    }
                }
            }
        }

        #endregion
    }
}