using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SampleAppPageViewModelBase" />
    public class MainWindowViewModel: SampleAppPageViewModelBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MainWindowViewModel(IServiceProvider serviceProvider): base(serviceProvider)
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registers the service bus message handlers.
        /// </summary>
        public override void RegisterServiceBusMessageHandlers()
        {
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<ContentFolderReadFinishedMessage>(this, OnContentReadFinishedAsync);
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<LocalStateContentFolderReadFinishedMessage>(this, OnLocalStateContentReadFinishedAsync);

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
