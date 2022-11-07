using Paradigm.WindowsAppSDK.SampleApp.Messages;
using Paradigm.WindowsAppSDK.Services.MessageBus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MainViewModel : Base.SampleAppPageViewModelBase
    {
        public MainViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void RegisterServiceBusMessageHandlers()
        {
            base.RegisterServiceBusMessageHandlers();
            MessageBusRegistrationsHandler.Instance.RegisterMessageHandler<ContentFolderReadFinishedMessage>(this, OnContentReadFinishedAsync);
        }

        private async Task OnContentReadFinishedAsync(ContentFolderReadFinishedMessage arg)
        {
            await Task.Delay(arg.Delay);
            LogService.Debug($"Processing message : {arg.GetType()}. Id {arg.Guid} with delay : {arg.Delay}");
        }

        public override void Dispose()
        {
            LogService.Debug($"Unregistering {MessageBusRegistrationsHandler.Instance.GetRegisteredMessageHandlers(this).Count()} message registrations from {this.GetType().FullName}");
            MessageBusRegistrationsHandler.Instance.UnregisterMessageHandler<ContentFolderReadFinishedMessage>(this);
            LogService.Debug($"{MessageBusRegistrationsHandler.Instance.GetRegisteredMessageHandlers(this).Count()} message registrations from {this.GetType().FullName}");
        }

        public async Task ExecuteActionAsync()
        {
            if (await Navigation.NavigateToAsync<TestViewModel>())
                LogService.Information("Executed test navigation");
        }

        public async Task ExecuteLocalStateActionAsync()
        {
            if (await Navigation.NavigateToAsync<LocalStateTestViewModel>())
                LogService.Information("Executed local state test navigation");
        }
    }
}