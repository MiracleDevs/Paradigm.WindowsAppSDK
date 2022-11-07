using Paradigm.WindowsAppSDK.SampleApp.Messages;
using System;
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
            
        }

        private async Task OnContentReadFinishedAsync(ContentFolderReadFinishedMessage arg)
        {
            await Task.Delay(arg.Delay);
            LogService.Debug($"Processing message : {arg.GetType()}. Id {arg.Guid} with delay : {arg.Delay}");
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