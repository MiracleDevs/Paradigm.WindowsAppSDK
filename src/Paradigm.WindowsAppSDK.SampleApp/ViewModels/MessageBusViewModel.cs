using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MessageBusViewModel : SampleAppPageViewModelBase
    {
        private List<ItemViewModel> Items { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MessageBusViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Items = new();
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

        /// <summary>
        /// Registers this instance.
        /// </summary>
        public void Register()
        {
            Items.Add(ServiceProvider.GetRequiredService<ItemViewModel>());
        }

        /// <summary>
        /// Unregisters this instance.
        /// </summary>
        public void Unregister()
        {
            Items.ForEach(x => x.UnregisterMessage());
        }
    }

    public class ItemViewModel : ViewModelBase
    {
        public ItemViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<SampleMessage>(this, OnSampleMessageReceivedAsync);
        }

        public void UnregisterMessage()
        {
            MessageBusRegistrationsHandler.Instance.UnregisterMessageHandler<SampleMessage>(this);
        }

        private async Task OnSampleMessageReceivedAsync(SampleMessage message)
        {
            await Task.CompletedTask;
        }
    }
}