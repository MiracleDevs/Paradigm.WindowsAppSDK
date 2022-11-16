namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public class NavigationFrameEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public INavigableView Content { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFrameEventArgs"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public NavigationFrameEventArgs(INavigableView content)
        {
            this.Content = content;
        }

        #endregion
    }
}