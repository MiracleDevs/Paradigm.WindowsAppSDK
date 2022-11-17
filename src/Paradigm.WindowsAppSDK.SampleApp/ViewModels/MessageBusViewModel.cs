using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MessageBusViewModel : SampleAppPageViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MessageBusViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// Toggles the message viewer.
        /// </summary>
        public async Task ToggleMessageViewerAsync()
        {
            await MessageBusService.SendAsync(new ToggleDisplayMessagesViewerMessage());
        }

        /// <summary>
        /// Sends the sample1 message.
        /// </summary>
        public async Task SendSample1MessageAsync()
        {
            await MessageBusService.SendAsync(new SampleMessage { Text = "Sample 1 message received" });
        }

        /// <summary>
        /// Sends the sample2 message asynchronous.
        /// </summary>
        public async Task SendSample2MessageAsync()
        {
            await MessageBusService.SendAsync(new SampleMessage { Text = "Sample 2 message received" });
        }
    }
}