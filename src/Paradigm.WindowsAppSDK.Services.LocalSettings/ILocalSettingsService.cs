using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.LocalSettings
{
    public interface ILocalSettingsService : IService
    {
        /// <summary>
        /// Gets the stored settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetStoredSettings<T>();

        /// <summary>
        /// Stores the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void StoreSettings<T>(T settings);

        /// <summary>
        /// Resets to default settings.
        /// </summary>
        void ResetToDefaultSettings();
    }
}
