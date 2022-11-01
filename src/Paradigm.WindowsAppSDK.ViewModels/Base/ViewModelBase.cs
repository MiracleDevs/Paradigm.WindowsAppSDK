using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Paradigm.WindowsAppSDK.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INavigable, IDisposable
    {
        #region Properties

        /// <summary>
        /// Event executed when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the application service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <exception cref="ArgumentNullException">serviceProvider</exception>
        protected ViewModelBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Determines whether this instance [can navigate to] the specified navigable element.
        /// </summary>
        /// <param name="navigable">The navigable element.</param>
        /// <returns></returns>
        public async Task<bool> CanNavigateTo(INavigable navigable)
        {
            await Task.CompletedTask;
            return true;
        }

        /// <summary>
        /// Determines whether this instance [can navigate from] the specified navigable element.
        /// </summary>
        /// <param name="navigable">The navigable element.</param>
        /// <returns></returns>
        public async Task<bool> CanNavigateFrom(INavigable navigable)
        {
            await Task.CompletedTask;
            return true;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> event for a given property.
        /// </summary>
        /// <param name="propertyName">The name of the property changing its value.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Invokes the <see cref="PropertyChanged"/> for all the properties.
        /// </summary>
        protected void OnAllPropertiesChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        /// <summary>
        /// Sets the field value and reports a check if the value changed to invoke the changing event.
        /// </summary>
        /// <typeparam name="T">The field type.</typeparam>
        /// <param name="field">The field reference.</param>
        /// <param name="value">The new value.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns></returns>
        protected bool SetPropertyField<T>(ref T field, T value, [CallerMemberName] string fieldName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(fieldName);
            return true;
        }

        /// <summary>
        /// Gets the required service from the service provider.
        /// </summary>
        /// <remarks>
        /// If the service is not properly registered the method will return null.
        /// </remarks>
        /// <typeparam name="T">The type or interface of the required service.</typeparam>
        /// <returns>The service instance.</returns>
        protected T? GetService<T>()
        {
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
        protected T GetRequiredService<T>() where T : notnull
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        #endregion
    }
}
