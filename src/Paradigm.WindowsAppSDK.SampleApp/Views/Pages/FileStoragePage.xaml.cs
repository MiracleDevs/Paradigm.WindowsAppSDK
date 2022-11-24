using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using Paradigm.WindowsAppSDK.Xaml.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class FileStoragePage : INavigableView
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        private FileStorageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStoragePage"/> class.
        /// </summary>
        public FileStoragePage()
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
            ViewModel = (FileStorageViewModel)navigable;
            ViewModel.Initialize();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Called when [pick file tapped].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.UI.Xaml.Input.TappedRoutedEventArgs"/> instance containing the event data.</param>
        private async void OnPickFileTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();

            App.MainWindow.InitializeTarget(filePicker);

            filePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            filePicker.FileTypeFilter.Add(".txt");

            var file = await filePicker.PickSingleFileAsync();
            if (file != null)
                ViewModel.LoadFile(file.Path);
        }

        /// <summary>
        /// Called when [selection changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnSelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            var fileName = e.AddedItems.FirstOrDefault()?.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            ViewModel.LoadFile($"FileStorage\\{fileName}");
        }
    }
}
