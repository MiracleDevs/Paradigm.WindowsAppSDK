using Paradigm.WindowsAppSDK.Services.Interfaces;
using System.Text.Json;

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

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        T? GetObject<T>(string key, JsonSerializerOptions? options = null) where T : class;
    }
}