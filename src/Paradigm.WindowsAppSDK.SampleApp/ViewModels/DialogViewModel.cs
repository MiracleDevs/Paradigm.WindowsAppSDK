using Paradigm.WindowsAppSDK.SampleApp.Models;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Dialogs;
using Paradigm.WindowsAppSDK.Services.Dialog;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class DialogViewModel : SampleAppPageViewModelBase
    {
        public string? DialogResult { get; private set; }

        private IDialogService DialogService { get; }

        public DialogViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            DialogService = GetRequiredService<IDialogService>();
        }

        public async Task OpenInformationDialogAsync()
        {
            await DialogService.OpenAsync<SampleDialogViewModel>(new DialogModel
            {
                Title = "Information dialog",
                Paragraph = "Content saved succesfully"
            });

            DialogResult = default;
            OnPropertyChanged(nameof(DialogResult));
        }

        public async Task OpenConfirmationDialogAsync()
        {
            var result = await DialogService.OpenAsync<SampleDialogViewModel>(new DialogModel
            {
                Title = "Confirmation dialog",
                Paragraph = "Are you sure you want to confirm the operation?",
                ConfirmOptionText = "Yes",
                CancelOptionText = "Cancel"
            });

            DialogResult = !result.HasValue
                ? "Operation canceled!"
                : result.Value ? "Operation confirmed!" : default;

            OnPropertyChanged(nameof(DialogResult));
        }

        public async Task OpenThreeOptionsDialogAsync()
        {
            var dialogViewModel = GetRequiredService<SampleDialogViewModel>();
            dialogViewModel.Title = "3-options dialog";
            dialogViewModel.Content = "You have unsaved changes, do yo want to save before leaving?";
            dialogViewModel.ConfirmOptionText = "Yes";
            dialogViewModel.DenyOptionText = "No";
            dialogViewModel.CancelOptionText = "Cancel";

            var result = await DialogService.OpenAsync(dialogViewModel);

            DialogResult = !result.HasValue
                ? "Operation canceled!"
                : result.Value ? "Operation confirmed!" : "Operation denied!";

            OnPropertyChanged(nameof(DialogResult));
        }
    }
}