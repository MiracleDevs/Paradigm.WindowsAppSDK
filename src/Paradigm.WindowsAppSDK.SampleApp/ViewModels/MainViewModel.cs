using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService Navigation { get; }

        public MainViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Navigation = serviceProvider.GetRequiredService<INavigationService>();
        }

        public async Task ExecuteActionAsync()
        {
            if (await Navigation.NavigateToAsync<TestViewModel>())
                LogService.Information("Executed navigation");
        }
    }
}