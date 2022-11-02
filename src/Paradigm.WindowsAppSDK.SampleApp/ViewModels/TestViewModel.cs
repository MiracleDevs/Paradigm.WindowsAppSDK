using Microsoft.ApplicationInsights.Channel;
using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.Services.Telemetry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class TestViewModel : SampleAppViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the navigation.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        private INavigationService Navigation { get; }

        /// <summary>
        /// Gets the telemetry.
        /// </summary>
        /// <value>
        /// The telemetry.
        /// </value>
        private ITelemetryService Telemetry { get; }

        /// <summary>
        /// Gets the file storage service.
        /// </summary>
        /// <value>
        /// The file storage service.
        /// </value>
        protected IFileStorageService FileStorageService { get; }

        /// <summary>
        /// Gets a value indicating whether [use local state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use local state]; otherwise, <c>false</c>.
        /// </value>
        protected virtual bool UseLocalState { get; } = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TestViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public TestViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Navigation = serviceProvider.GetRequiredService<INavigationService>();
            Telemetry = serviceProvider.GetRequiredService<ITelemetryService>();
            FileStorageService = serviceProvider.GetRequiredService<IFileStorageService>();
        }

        #endregion

        #region Public Methods

        public async Task GoBackAsync()
        {
            await ReadFolderContentAsync("Test", !this.UseLocalState);

            if (await Navigation.GoBackAsync())
                LogService.Information($"Executed back navigation from {this.GetType().FullName}");
        }

        public async Task SendTelemetryAsync()
        {
            Telemetry.Initialize(new TelemetrySettings("InstrumentationKey=761f753b-f1e0-4442-91c2-0bea33fdded6;IngestionEndpoint=https://eastus2-3.in.applicationinsights.azure.com/;LiveEndpoint=https://eastus2.livediagnostics.monitor.azure.com/"));
            Telemetry.AddExtraProperty("rac", "rac1");
            Telemetry.AddExtraProperty("storeId", "store1");
            Telemetry.TrackEvent("sampleAppTestEvent", new Dictionary<string, string>
            {
                ["test1"] = Guid.NewGuid().ToString(),
                ["test2"] = "Paradigm.WindowsAppSDK.SampleApp"
            });

            await Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reads the folder content asynchronous.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        protected virtual async Task ReadFolderContentAsync(string path, bool useInstallationFolder)
        {
            var fileNames = await FileStorageService.GetFilesFromFolderAsync(path, useInstallationFolder);

            if (fileNames == null)
                return;

            foreach (var name in fileNames)
            {
                var filePath = Path.Combine(path, name);
                var fileProperties = await FileStorageService.ReadFilePropertiesAsync(filePath, Enumerable.Empty<string>(), useInstallationFolder);

                LogService.Information(string.Join(Environment.NewLine, (new[] { $"{filePath} properties." }).ToList().Concat(fileProperties.Select(prop => $"{prop.Key} = {prop.Value}"))));
                var fileContent = await FileStorageService.ReadContentFromApplicationUriAsync(FileStorageService.GetLocalFileUri(filePath, true, useInstallationFolder));
                LogService.Information(string.Join(Environment.NewLine, new[] { $"{filePath} content.", fileContent }));
            }
        }

        #endregion
    }
}