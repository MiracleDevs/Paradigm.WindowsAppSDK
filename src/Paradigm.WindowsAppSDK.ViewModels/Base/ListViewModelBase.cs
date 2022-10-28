namespace Paradigm.WindowsAppSDK.ViewModels.Base
{
    public abstract class ListViewModelBase : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewModelBase"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected ListViewModelBase(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
