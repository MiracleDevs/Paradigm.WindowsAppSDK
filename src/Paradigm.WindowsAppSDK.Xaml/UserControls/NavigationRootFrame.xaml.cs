using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System;
using System.Linq;

namespace Paradigm.WindowsAppSDK.Xaml.UserControls
{
    public sealed partial class NavigationRootFrame : UserControl, INavigationFrame
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoBack => this.RootFrame.CanGoBack;

        /// <summary>
        /// Gets a value indicating whether this instance can go forward.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoForward => this.RootFrame.CanGoForward;

        /// <summary>
        /// Gets or sets the navigated action.
        /// </summary>
        /// <value>
        /// The navigated action.
        /// </value>
        public Action<object, NavigationFrameEventArgs> Navigated { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationRootFrame"/> class.
        /// </summary>
        public NavigationRootFrame()
        {
            this.InitializeComponent();
            this.RootFrame.Navigated += OnFrameNavigated;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.RootFrame.Navigated -= OnFrameNavigated;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Navigates the specified source page type.
        /// </summary>
        /// <param name="sourcePageType">Type of the source page.</param>
        /// <param name="value">The value.</param>
        public void Navigate(Type sourcePageType, object value)
        {
            this.RootFrame.Navigate(sourcePageType, value, new SuppressNavigationTransitionInfo());
        }

        /// <summary>
        /// Goes the forward.
        /// </summary>
        public void GoForward()
        {
            this.RootFrame.GoForward();
        }

        /// <summary>
        /// Goes back.
        /// </summary>
        public void GoBack()
        {
            this.RootFrame.GoBack();
        }

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        public void ClearBackStack()
        {
            this.RootFrame.BackStack.Clear();
        }

        /// <summary>
        /// Lasts the type of the forward stack source page.
        /// </summary>
        /// <returns></returns>
        public Type LastForwardStackSourcePageType()
        {
            return this.RootFrame.ForwardStack.Last().SourcePageType;
        }

        /// <summary>
        /// Lasts the type of the back stack source page.
        /// </summary>
        /// <returns></returns>
        public Type LastBackStackSourcePageType()
        {
            return this.RootFrame.BackStack.Last().SourcePageType;
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
            this.Navigated(sender, new NavigationFrameEventArgs(e.Content as INavigableView));
        }

        #endregion
    }
}