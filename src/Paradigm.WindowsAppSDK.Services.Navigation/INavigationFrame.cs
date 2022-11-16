namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    /// <summary>
    /// Provides an interface for a navigation frame.
    /// </summary>
    public interface INavigationFrame : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        bool CanGoBack { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can go forward.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        bool CanGoForward { get; }

        /// <summary>
        /// Gets or sets the navigated action.
        /// </summary>
        /// <value>
        /// The navigated action.
        /// </value>
        Action<object, NavigationFrameEventArgs> OnNavigated { get; set; }

        /// <summary>
        /// Navigates the specified source page type.
        /// </summary>
        /// <param name="sourcePageType">Type of the source page.</param>
        /// <param name="value">The value.</param>
        void Navigate(Type sourcePageType, object value);

        /// <summary>
        /// Goes the forward.
        /// </summary>
        void GoForward();

        /// <summary>
        /// Goes back.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        void ClearBackStack();

        /// <summary>
        /// Lasts the type of the back stack source page.
        /// </summary>
        /// <returns></returns>
        Type LastBackStackSourcePageType();

        /// <summary>
        /// Lasts the type of the forward stack source page.
        /// </summary>
        /// <returns></returns>
        Type LastForwardStackSourcePageType();
    }
}