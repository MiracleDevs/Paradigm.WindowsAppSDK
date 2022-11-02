namespace Paradigm.WindowsAppSDK.ViewModels.Base
{
    public abstract class ListPageViewModelBase : PageViewModelBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPageViewModelBase"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected ListPageViewModelBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        #endregion
    }
}