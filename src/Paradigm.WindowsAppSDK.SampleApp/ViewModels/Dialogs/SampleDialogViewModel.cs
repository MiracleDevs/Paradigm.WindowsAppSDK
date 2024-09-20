using Paradigm.WindowsAppSDK.SampleApp.Models;
using Paradigm.WindowsAppSDK.Services.Dialog;
using Paradigm.WindowsAppSDK.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels.Dialogs
{
    public class SampleDialogViewModel : ViewModelBase, IDialog
    {
        #region Properties

        public virtual bool CanConfirm => true;

        public string? Title { get; set; }

        public string? ConfirmOptionText { get; set; }

        public string? DenyOptionText { get; set; }

        public string? CancelOptionText { get; set; }

        public object? Content { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDialogViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public SampleDialogViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            CancelOptionText = "Close";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public virtual async Task InitializeAsync(object? arguments)
        {
            if (arguments is not null && arguments is DialogModel dialogModel)
            {
                Title = dialogModel.Title;
                Content = dialogModel.Paragraph;
                ConfirmOptionText = dialogModel.ConfirmOptionText;
                CancelOptionText = dialogModel.CancelOptionText ?? CancelOptionText;
                DenyOptionText = dialogModel.DenyOptionText;
                OnAllPropertiesChanged();
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Called when [confirm].
        /// </summary>
        public virtual async Task OnConfirmAsync()
        {
            await Task.CompletedTask;
        }

        #endregion
    }
}