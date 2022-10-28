using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Localization
{
    /// <summary>
    /// Provides an interface for a translation service.
    /// </summary>
    /// <seealso cref="IService" />
    public interface ILocalizationService : IService
    {
        /// <summary>
        /// Extracts the translation strings.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Dictionary<string, string> ExtractLocalizableStrings<TModel>(TModel model, string prefix);

        /// <summary>
        /// Applies the translations.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="translations">The translations.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        TModel ApplyLocalizableStrings<TModel>(TModel model, Dictionary<string, string> translations, string prefix);
    }
}