using Microsoft.UI.Xaml;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        #region Properties

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        private IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the navigation views.
        /// </summary>
        /// <value>
        /// The navigation views.
        /// </value>
        private Dictionary<Type, Type> NavigationViews { get; }

        /// <summary>
        /// Gets the navigables.
        /// </summary>
        /// <value>
        /// The navigables.
        /// </value>
        private Dictionary<Type, Type> Navigables { get; }

        /// <summary>
        /// Gets or sets the current window.
        /// </summary>
        /// <value>
        /// The current window.
        /// </value>
        private Window CurrentWindow { get; set; }

        /// <summary>
        /// Gets the current navigable.
        /// </summary>
        /// <value>
        /// The current navigable.
        /// </value>
        public INavigable CurrentNavigable { get; private set; }

        /// <summary>
        /// Gets the current navigable view.
        /// </summary>
        /// <value>
        /// The current navigable view.
        /// </value>
        public INavigableView CurrentNavigableView { get; private set; }

        public bool CanGoBack => throw new NotImplementedException();

        public bool CanGoForward => throw new NotImplementedException();

        #endregion

        public void ActivateInstance()
        {
            throw new NotImplementedException();
        }

        public void ClearBackStack()
        {
            throw new NotImplementedException();
        }

        public void DeactivateInstance()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GoForwardAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> NavigateToAsync<TNavigable>() where TNavigable : INavigable
        {
            throw new NotImplementedException();
        }

        public Task<bool> NavigateToAsync(Type navigableType)
        {
            throw new NotImplementedException();
        }
    }
}
