﻿using Paradigm.WindowsAppSDK.Services.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Paradigm.WindowsAppSDK.Services.Configuration
{
    public interface IConfigurationService : IService
    {
        /// <summary>
        /// Adds the provided configuration content to the configurations dictionary.
        /// </summary>
        /// <param name="serializedContent">Content of the serialized.</param>
        /// <param name="overwriteExistingKeys">if set to <c>true</c> [overwrite existing keys].</param>
        void AddConfigurationContent(string serializedContent, bool overwriteExistingKeys = false);

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

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="jsonTypeInfo">The json type information.</param>
        /// <returns></returns>
        T? GetObject<T>(string key, JsonTypeInfo<T> jsonTypeInfo) where T : class;
    }
}