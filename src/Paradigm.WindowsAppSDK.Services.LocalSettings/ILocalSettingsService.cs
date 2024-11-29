using Paradigm.WindowsAppSDK.Services.Interfaces;
using System.Text.Json.Serialization.Metadata;

namespace Paradigm.WindowsAppSDK.Services.LocalSettings
{
    public interface ILocalSettingsService : IService
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="settingsContainer">The settings container.</param>
        void Initialize(IDictionary<string, object> settingsContainer);

        /// <summary>
        /// Gets the stored settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T? GetStoredSettings<T>();

        /// <summary>
        /// Gets the stored settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonTypeInfo">The json type information.</param>
        /// <returns></returns>
        T? GetStoredSettings<T>(JsonTypeInfo<T> jsonTypeInfo);

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void StoreSettings<T>(T settings);

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings">The settings.</param>
        /// <param name="jsonTypeInfo">The json type information.</param>
        void StoreSettings<T>(T settings, JsonTypeInfo<T> jsonTypeInfo);

        /// <summary>
        /// Resets to default settings.
        /// </summary>
        void ResetToDefaultSettings();
    }
}