using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class TestViewModel : SampleAppViewModelBase
    {
        private INavigationService Navigation { get; }
        public IFileStorageService FileStorageService { get; }

        public TestViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Navigation = serviceProvider.GetRequiredService<INavigationService>();
            FileStorageService = serviceProvider.GetRequiredService<IFileStorageService>();
        }

        public async Task ExecuteActionAsync()
        {
            var path = "testData.json";
            var completePath = Path.Combine("Test", path);

            if (!FileStorageService.FileExists(completePath))
            {
                throw new InvalidOperationException($"{path} file does not exist.");
            }
            else
            {
                var testFileContent = await FileStorageService.ReadContentFromApplicationUriAsync(completePath);
                
                LogService.Information(string.Join(Environment.NewLine, new[] { $"{completePath} content is", testFileContent }));

                var properties = await FileStorageService.ReadFilePropertiesAsync(completePath, Enumerable.Empty<string>());

                LogService.Information(string.Join(Environment.NewLine, (new[] { $"{completePath} properties are " }).ToList().Concat(properties.Select(kvp => $"{kvp.Key} = {kvp.Value}"))));

            }

            if (await Navigation.GoBackAsync())
                LogService.Information("Executed back navigation");
        }
    }
}