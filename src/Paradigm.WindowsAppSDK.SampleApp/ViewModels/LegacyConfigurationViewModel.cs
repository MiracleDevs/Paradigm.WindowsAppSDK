using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.LegacyConfiguration;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class LegacyConfigurationViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private ILegacyConfigurationService Service { get; }

        public string StringValue1 => Service.GetString("stringValue1");

        public string StringValue2 => Service.GetString("stringValue2");

        public double? NumericValue1 => Service.GetDouble("numericValue1");

        public double? NumericValue2 => Service.GetDouble("numericValue2");

        public bool? BooleanValue1 => Service.GetBoolean("booleanValue1");

        public bool? BooleanValue2 => Service.GetBoolean("booleanValue2");

        public string ObjectValue => Service.GetObject<Models.ObjectPropertyModel>("objectValue")?.ToString();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LegacyConfigurationViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public LegacyConfigurationViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Service = serviceProvider.GetRequiredService<ILegacyConfigurationService>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            var configurationContent = ServiceProvider.GetRequiredService<IFileStorageService>().ReadTextFromInstallationFolder("Configuration\\config.json");
            Service.Initialize(configurationContent);
            OnAllPropertiesChanged();
        }

        #endregion
    }
}