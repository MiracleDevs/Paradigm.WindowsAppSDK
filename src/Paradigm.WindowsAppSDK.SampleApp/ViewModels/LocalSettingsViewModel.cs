using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.Models;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.LocalSettings;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class LocalSettingsViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private ILocalSettingsService Service { get; }

        /// <summary>
        /// Gets or sets the current local settings.
        /// </summary>
        /// <value>
        /// The current local settings.
        /// </value>
        public LocalSettingsModel? CurrentLocalSettings { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalSettingsViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public LocalSettingsViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Service = serviceProvider.GetRequiredService<ILocalSettingsService>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the local settings.
        /// </summary>
        public void LoadLocalSettings()
        {
            CurrentLocalSettings = Service.GetStoredSettings<LocalSettingsModel>() ?? new LocalSettingsModel();
            OnPropertyChanged(nameof(CurrentLocalSettings));
        }

        /// <summary>
        /// Saves the local settings.
        /// </summary>
        public void SaveLocalSettings()
        {
            Service.StoreSettings(CurrentLocalSettings);
        }

        /// <summary>
        /// Resets the local settings.
        /// </summary>
        public void ResetLocalSettings()
        {
            Service.ResetToDefaultSettings();
            LoadLocalSettings();
        }

        #endregion
    }
}