using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MainWindowViewModel : SampleAppPageViewModelBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MainWindowViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates to page.
        /// </summary>
        public async Task NavigateToPageAsync(string typeName)
        {
            var type = typeof(MainWindowViewModel).Assembly.GetType($"Paradigm.WindowsAppSDK.SampleApp.ViewModels.{typeName}ViewModel");
            await Navigation.NavigateToAsync(type);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registers the service bus message handlers.
        /// </summary>
        public override void RegisterServiceBusMessageHandlers()
        {
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<ContentFolderReadFinishedMessage>(this, this.ServiceProvider, OnContentReadFinishedAsync);
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<LocalStateContentFolderReadFinishedMessage>(this, this.ServiceProvider, OnLocalStateContentReadFinishedAsync);

            LogService.Debug($"Registered {MessageBusRegistrationsHandler.Instance.GetRegisteredMessageHandlers(this).Count()} message handlers");
        }

        /// <summary>
        /// Called when [local state content read finished asynchronous].
        /// </summary>
        /// <param name="arg">The argument.</param>
        private async Task OnLocalStateContentReadFinishedAsync(LocalStateContentFolderReadFinishedMessage arg)
        {
            await Task.Delay(arg.Delay);
            LogService.Debug($"Processing message : {arg.GetType()}. Id {arg.Guid} with delay : {arg.Delay}");
        }


        /// <summary>
        /// Called when [content read finished asynchronous].
        /// </summary>
        /// <param name="arg">The argument.</param>
        private async Task OnContentReadFinishedAsync(ContentFolderReadFinishedMessage arg)
        {
            await Task.Delay(arg.Delay);
            LogService.Debug($"Processing message : {arg.GetType()}. Id {arg.Guid} with delay : {arg.Delay}");
        }

        #endregion
    }
}