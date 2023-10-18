namespace Paradigm.WindowsAppSDK.Services.Dialog
{
    public interface IDialogView : IDisposable
    {
        /// <summary>
        /// Gets the dialog.
        /// </summary>
        /// <value>
        /// The dialog.
        /// </value>
        IDialog? Dialog { get; }

        /// <summary>
        /// Initializes the dialog view.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns></returns>
        Task InitializeAsync(IDialog dialog);

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <returns></returns>
        Task<bool?> ShowAsync();

        /// <summary>
        /// Hides this instance.
        /// </summary>
        void Hide();
    }
}