using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.LegacyConfiguration
{
    public interface ILegacyConfigurationService : IService
    {
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string? GetString(string key);

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool? GetBoolean(string key);

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        double? GetDouble(string key);
    }
}