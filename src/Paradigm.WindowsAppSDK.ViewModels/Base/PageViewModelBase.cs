using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.ViewModels.Base
{
    public abstract class PageViewModelBase : ViewModelBase, INavigable
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PageViewModelBase"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected PageViewModelBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        #endregion

        #region Public Methods

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
    }
}