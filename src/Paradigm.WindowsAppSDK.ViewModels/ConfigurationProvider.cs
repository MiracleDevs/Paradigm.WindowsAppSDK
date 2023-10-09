using Microsoft.Extensions.Configuration;

namespace Paradigm.WindowsAppSDK.ViewModels
{
    public class ConfigurationProvider
    {
        /// <summary>
        /// The configuration root
        /// </summary>
        private IConfigurationRoot? _configurationRoot;

        /// <summary>
        /// Initializes the specified base path.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="files">The files tuples (fileName, optional).</param>
        public void Initialize(string basePath, List<Tuple<string, bool>> files)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath);

            files.ForEach(x => builder.AddJsonFile(x.Item1, optional: x.Item2));

            _configurationRoot = builder.Build();
        }

        /// <summary>
        /// Gets the configuration value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">ConfigurationProvider was not initialized</exception>
        public T? GetConfigurationValue<T>(string key)
        {
            if (_configurationRoot is null)
                throw new InvalidOperationException("ConfigurationProvider was not initialized");

            return _configurationRoot.GetSection(key).Get<T>();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">ConfigurationProvider was not initialized</exception>
        public T GetConfiguration<T>() where T : new()
        {
            if (_configurationRoot is null)
                throw new InvalidOperationException("ConfigurationProvider was not initialized");

            var configurationObject = new T();
            _configurationRoot.Bind(configurationObject);
            return configurationObject;
        }
    }
}