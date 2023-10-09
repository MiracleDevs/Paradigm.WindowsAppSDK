using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.Xaml.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class LocalizationPage : INavigableView
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        private LocalizationViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationPage"/> class.
        /// </summary>
        public LocalizationPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public async Task DisposeAsync()
        {
            this.ViewModel.Dispose();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the navigation.
        /// </summary>
        /// <param name="navigable">The navigable.</param>
        public async Task InitializeNavigationAsync(INavigable navigable)
        {
            ViewModel = (LocalizationViewModel)navigable;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Called when [export tapped].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.UI.Xaml.Input.TappedRoutedEventArgs"/> instance containing the event data.</param>
        private async void OnExportTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary,
                SuggestedFileName = "export"
            };

            savePicker.FileTypeChoices.Add("JSON file", new[] { ".json" });

            App.MainWindow.InitializeTarget(savePicker);

            var file = await savePicker.PickSaveFileAsync();
            if (file is null)
                return;

            CachedFileManager.DeferUpdates(file);
            ViewModel.ExportLocalizationFile(file.Path);
            await CachedFileManager.CompleteUpdatesAsync(file);
        }

        /// <summary>
        /// Called when [pick tapped].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.UI.Xaml.Input.TappedRoutedEventArgs"/> instance containing the event data.</param>
        private async void OnPickTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker 
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };

            openPicker.FileTypeFilter.Add(".json");

            App.MainWindow.InitializeTarget(openPicker);

            var file = await openPicker.PickSingleFileAsync();
            if (file is null)
                return;

            ViewModel.LoadLocalizationFile(file.Path);
        }
    }
}