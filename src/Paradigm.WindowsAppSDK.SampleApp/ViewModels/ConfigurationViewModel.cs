using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.JsonContexts;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.Configuration;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class ConfigurationViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// The service
        /// </summary>
        private readonly IConfigurationService _service;

        public string? StringValue1 => _service.GetString("stringValue1");

        public string? StringValue2 => _service.GetString("stringValue2");

        public double? NumericValue1 => _service.GetDouble("numericValue1");

        public double? NumericValue2 => _service.GetDouble("numericValue2");

        public bool? BooleanValue1 => _service.GetBoolean("booleanValue1");

        public bool? BooleanValue2 => _service.GetBoolean("booleanValue2");

        public string? ObjectValue => _service.GetObject("objectValue", ObjectPropertyModelJsonContext.Default.ObjectPropertyModel)?.ToString();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ConfigurationViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _service = serviceProvider.GetRequiredService<IConfigurationService>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            var configurationContent = ServiceProvider.GetRequiredService<IFileStorageService>().ReadTextFromInstallationFolder("Configuration\\config.json");
            _service.AddConfigurationContent(configurationContent);
            OnAllPropertiesChanged();
        }

        #endregion
    }
}