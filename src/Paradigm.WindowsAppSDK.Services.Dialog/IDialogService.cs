using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Dialog
{
    public interface IDialogService : IService
    {
        /// <summary>
        /// Registers a dialog element and its paired view.
        /// </summary>
        /// <typeparam name="TDialogView">The type of the dialog view.</typeparam>
        /// <typeparam name="TDialog">The type of the dialog.</typeparam>
        void Register<TDialogView, TDialog>()
            where TDialogView : IDialogView
            where TDialog : IDialog;

        /// <summary>
        /// Creates an instance of the IDialog element and opens its paired IDialogView.
        /// </summary>
        /// <typeparam name="TDialog">The type of the dialog.</typeparam>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// - true if the dialog prompt is confirmed.
        /// - false if the dialog prompt is denied.
        /// - null if the dialog is cancelled.
        /// </returns>
        Task<bool?> OpenAsync<TDialog>(object? arguments) where TDialog : IDialog;

        /// <summary>
        /// Opens the IDialog element paired IDialogView.
        /// </summary>
        /// <typeparam name="TDialog">The type of the dialog.</typeparam>
        /// <param name="dialog">The dialog.</param>
        /// <returns>
        /// - true if the dialog prompt is confirmed.
        /// - false if the dialog prompt is denied.
        /// - null if the dialog is cancelled.
        /// </returns>
        Task<bool?> OpenAsync<TDialog>(TDialog dialog) where TDialog : IDialog;
    }
}