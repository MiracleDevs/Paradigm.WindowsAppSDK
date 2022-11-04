using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.MessageBus.Extensions;
using System;
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
        protected override void RegisterServiceBusMessageHandlers()
        {
            this.RegisterMessage<ContentFolderReadFinishedMessage>(OnContentReadFinishedAsync);
            this.RegisterMessage<LocalStateContentFolderReadFinishedMessage>(OnLocalStateContentReadFinishedAsync);

            LogService.Debug($"Registered {this.MessageBusConsumerRegistrations.Count} message handlers");
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
