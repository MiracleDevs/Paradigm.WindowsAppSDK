using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System;
using System.Linq;

namespace Paradigm.WindowsAppSDK.Xaml.UserControls
{
    public sealed class NavigationRootFrame : Frame, INavigationFrame
    {
        #region Properties

        /// <summary>
        /// Gets or sets the navigated action.
        /// </summary>
        /// <value>
        /// The navigated action.
        /// </value>
        public Action<object, NavigationFrameEventArgs> OnNavigated { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationRootFrame"/> class.
        /// </summary>
        public NavigationRootFrame()
        {
            this.Navigated += OnFrameNavigated;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Navigated -= OnFrameNavigated;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates the specified source page type.
        /// </summary>
        /// <param name="sourcePageType">Type of the source page.</param>
        /// <param name="value">The value.</param>
        public new void Navigate(Type sourcePageType, object value)
        {
            this.Navigate(sourcePageType, value, new SuppressNavigationTransitionInfo());
        }

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        public void ClearBackStack()
        {
            this.BackStack.Clear();
        }

        /// <summary>
        /// Lasts the type of the forward stack source page.
        /// </summary>
        /// <returns></returns>
        public Type LastForwardStackSourcePageType()
        {
            return this.ForwardStack.Last().SourcePageType;
        }

        /// <summary>
        /// Lasts the type of the back stack source page.
        /// </summary>
        /// <returns></returns>
        public Type LastBackStackSourcePageType()
        {
            return this.BackStack.Last().SourcePageType;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Called when [frame navigated].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            this.OnNavigated(sender, new NavigationFrameEventArgs(e.Content as INavigableView));
        }

        #endregion
    }
}