using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.LegacyConfiguration
{
    public interface ILegacyConfigurationService : IService
    {
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="serializedContent">Content of the serialized.</param>
        /// <returns></returns>
        void Initialize(string serializedContent);

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