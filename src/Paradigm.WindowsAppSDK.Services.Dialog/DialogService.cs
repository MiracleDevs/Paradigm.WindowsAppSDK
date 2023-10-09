using Microsoft.Extensions.DependencyInjection;

namespace Paradigm.WindowsAppSDK.Services.Dialog
{
    public class DialogService : IDialogService
    {
        #region Properties

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        private IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the dialog views.
        /// </summary>
        /// <value>
        /// The dialog views.
        /// </value>
        private Dictionary<Type, Type> DialogViews { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public DialogService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            DialogViews = new();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers a dialog element and its paired view.
        /// </summary>
        /// <typeparam name="TDialogView">The type of the dialog view.</typeparam>
        /// <typeparam name="TDialog">The type of the dialog.</typeparam>
        public void Register<TDialogView, TDialog>()
            where TDialogView : IDialogView
            where TDialog : IDialog
        {
            DialogViews.Add(typeof(TDialog), typeof(TDialogView));
        }

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
        /// <exception cref="System.Exception">
        /// The dialog element '{dialogType.Name}' is not registered in the dialog service.
        /// or
        /// The dialog element '{dialogType.Name}' view is null.
        /// </exception>
        public async Task<bool?> OpenAsync<TDialog>(object? arguments) where TDialog : IDialog
        {
            var dialogType = typeof(TDialog);

            // 1. check if the dialog element is registered.
            if (!DialogViews.ContainsKey(dialogType))
                throw new Exception($"The dialog element '{dialogType.Name}' is not registered in the dialog service.");

            // 2. instantiate the dialog element from the service provider.
            var dialog = (IDialog)ServiceProvider.GetRequiredService(dialogType);

            // 3. instantiate the registered dialog view
            var dialogView = Activator.CreateInstance(DialogViews[dialogType]) as IDialogView;
            if (dialogView is null)
                throw new Exception($"The dialog element '{dialogType.Name}' view is null.");

            // 4. initialize the dialog view with the dialog element
            await dialog.InitializeAsync(arguments);
            await dialogView.InitializeAsync(dialog);

            // 5. show the dialog and return the result
            return await dialogView.ShowAsync();
        }

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
        /// <exception cref="System.Exception">
        /// The dialog element '{dialogType.Name}' is not registered in the dialog service.
        /// or
        /// The dialog element '{dialogType.Name}' view is null.
        /// </exception>
        public async Task<bool?> OpenAsync<TDialog>(TDialog dialog) where TDialog : IDialog
        {
            var dialogType = typeof(TDialog);

            // 1. check if the dialog element is registered.
            if (!DialogViews.ContainsKey(dialogType))
                throw new Exception($"The dialog element '{dialogType.Name}' is not registered in the dialog service.");

            // 2. instantiate the registered dialog view
            var dialogView = Activator.CreateInstance(DialogViews[dialogType]) as IDialogView;
            if (dialogView is null)
                throw new Exception($"The dialog element '{dialogType.Name}' view is null.");

            // 3. initialize the dialog view with the dialog element
            await dialogView.InitializeAsync(dialog);

            // 4. show the dialog and return the result
            return await dialogView.ShowAsync();
        }

        #endregion
    }
}