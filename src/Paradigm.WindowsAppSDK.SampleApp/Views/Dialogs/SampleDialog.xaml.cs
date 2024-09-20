using Microsoft.UI.Xaml.Controls;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Dialogs;
using Paradigm.WindowsAppSDK.Services.Dialog;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Dialogs
{
    public sealed partial class SampleDialog : IDialogView
    {
        /// <summary>
        /// Gets the dialog.
        /// </summary>
        /// <value>
        /// The dialog.
        /// </value>
        public IDialog? Dialog { get; private set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        private SampleDialogViewModel? ViewModel => Dialog as SampleDialogViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDialog"/> class.
        /// </summary>
        public SampleDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ViewModel?.Dispose();
        }

        /// <summary>
        /// Initializes the dialog view.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        public async Task InitializeAsync(IDialog dialog)
        {
            Dialog = dialog;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <returns></returns>
        public async Task<bool?> ShowAsync()
        {
            if (App.MainWindow is null)
                return false;

            App.MainWindow.LayoutGrid.Children.Add(this);
            ContentDialogControl.Closed += (s, e) => App.MainWindow.LayoutGrid.Children.Remove(ContentDialogControl);
            var result = await ContentDialogControl.ShowAsync();

            if (result == ContentDialogResult.None)
                return null;

            if (result == ContentDialogResult.Secondary && Dialog is not null)
            {
                await Dialog.OnConfirmAsync();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void Hide()
        {
            ContentDialogControl.Hide();
        }
    }
}