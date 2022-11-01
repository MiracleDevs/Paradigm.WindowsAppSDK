using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MainViewModel : SampleAppViewModelBase
    {
        private INavigationService Navigation { get; }

        public MainViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Navigation = serviceProvider.GetRequiredService<INavigationService>();
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