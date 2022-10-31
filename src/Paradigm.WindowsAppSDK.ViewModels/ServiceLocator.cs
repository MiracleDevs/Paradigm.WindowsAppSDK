using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.WindowsAppSDK.ViewModels
{
    public sealed class ServiceLocator
    {
        #region Singleton 

        /// <summary>
        /// The internal static thread-safe singleton instance.
        /// </summary>
        private static readonly Lazy<ServiceLocator> InternalInstance = new Lazy<ServiceLocator>(() => new ServiceLocator(), true);

        /// <summary>
        /// Gets the service locator singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static ServiceLocator Instance => InternalInstance.Value;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        /// <value>
        /// The service collection.
        /// </value>
        private IServiceCollection ServiceCollection { get; }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        public IServiceProvider? ServiceProvider { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceLocator"/> class from being created.
        /// </summary>
        private ServiceLocator()
        {
            ServiceCollection = new ServiceCollection();
        }

        #endregion

        #region User Configuration

        /// <summary>
        /// Configures this instance.
        /// </summary>
        public void Configure(Action<IServiceCollection> configure)
        {
            configure(ServiceCollection);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the required service from the service provider.
        /// </summary>
        /// <remarks>
        /// If the service is not properly registered the method will return null.
        /// </remarks>
        /// <typeparam name="T">The type or interface of the required service.</typeparam>
        /// <returns>The service instance.</returns>
        public T? GetService<T>()
        {
            if (ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider was not initialized");

            return ServiceProvider.GetService<T>();
        }

        /// <summary>
        /// Gets the required service from the service provider or fail if the service is not registered.
        /// </summary>
        /// <remarks>
        /// If the service is not properly registered, the method will throw <see cref="InvalidOperationException"/>
        /// </remarks>
        /// <exception cref="InvalidOperationException"></exception>
        /// <typeparam name="T">The type or interface of the required service.</typeparam>
        /// <returns>The service instance.</returns>
        public T GetRequiredService<T>() where T : notnull
        {
            if (ServiceProvider == null)
                throw new InvalidOperationException("ServiceProvider was not initialized");

            return ServiceProvider.GetRequiredService<T>();
        }

        #endregion
    }
}
