using Paradigm.WindowsAppSDK.Services.Dialog;

namespace Paradigm.WindowsAppSDK.Services.Tests.Dialog
{
    internal class TestDialogViewModel : IDialog
    {
        public bool CanConfirm => true;

        public async Task InitializeAsync(object? arguments)
        {
            await Task.CompletedTask;
        }

        public async Task OnConfirmAsync()
        {
            await Task.CompletedTask;
        }
    }
}