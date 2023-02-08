using Paradigm.WindowsAppSDK.Services.Dialog;

namespace Paradigm.WindowsAppSDK.Services.Tests.Dialog
{
    internal class TestDialogView : IDialogView
    {
        public IDialog Dialog { get; private set; }

        public void Dispose()
        {
        }

        public async Task InitializeAsync(IDialog dialog)
        {
            Dialog = dialog;
            await Task.CompletedTask;
        }

        public Task<bool?> ShowAsync()
        {
            return Task.FromResult<bool?>(true);
        }
    }
}