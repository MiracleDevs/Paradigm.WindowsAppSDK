using System.Text.Json;
using Windows.Storage;

namespace Paradigm.WindowsAppSDK.Services.LocalSettings
{
    public class LocalSettingsService : ILocalSettingsService
    {
        private const string SettingsKey = "local-settings";

        /// <summary>
        /// Gets the stored settings.
        /// </summary>
        /// <returns></returns>
        public T GetStoredSettings<T>()
        {
            var storedSettings = ApplicationData.Current.LocalSettings.Values[SettingsKey]?.ToString();
            return !string.IsNullOrWhiteSpace(storedSettings) ? JsonSerializer.Deserialize<T>(storedSettings) : default;
        }

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void StoreSettings<T>(T settings)
        {
            if (settings == null)
                return;

            ApplicationData.Current.LocalSettings.Values[SettingsKey] = JsonSerializer.Serialize(settings);
        }

        /// <summary>
        /// Resets to default settings.
        /// </summary>
        public void ResetToDefaultSettings()
        {
            ApplicationData.Current.LocalSettings.Values.Clear();
        }
    }
}