namespace Paradigm.WindowsAppSDK.Services.Dialog
{
    public interface IDialog
    {
        /// <summary>
        /// Gets a value indicating whether this instance can confirm.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can confirm; otherwise, <c>false</c>.
        /// </value>
        bool CanConfirm { get; }

        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        Task InitializeAsync(object? arguments);

        /// <summary>
        /// Called when [confirm].
        /// </summary>
        /// <returns></returns>
        Task OnConfirmAsync();
    }
}