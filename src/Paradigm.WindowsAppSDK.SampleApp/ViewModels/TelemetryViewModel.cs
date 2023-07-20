using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.Telemetry;
using Paradigm.WindowsAppSDK.ViewModels;
using System;
using System.Collections.Generic;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class TelemetryViewModel : SampleAppPageViewModelBase
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private ITelemetryService Service { get; }

        /// <summary>
        /// Gets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        public string StatusMessage { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TelemetryViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public TelemetryViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Service = serviceProvider.GetRequiredService<ITelemetryService>();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            var connString = GetRequiredService<ConfigurationProvider>().GetConfigurationValue<string>("TelemetryConnectionString");

            Service.Initialize(new TelemetrySettings(connString));
            Service.AddExtraProperty("extraProp1", "extraProp1Value");
            Service.AddExtraProperty("extraProp2", "extraProp2Value");
        }

        /// <summary>
        /// Registers the event1.
        /// </summary>
        public void RegisterEvent1()
        {
            Service.TrackEvent("sample event1", new Dictionary<string, string>
            {
                { "prop1", "prop1Value" },
                { "prop2", "prop2Value" }
            });

            UpdateStatusMessage("'sample event1' sent.");
        }

        /// <summary>
        /// Registers the event2.
        /// </summary>
        public void RegisterEvent2()
        {
            Service.TrackEvent("sample event2", default);
            Service.TrackEvent("sample event2", default);

            UpdateStatusMessage("'sample event2' sent.");
        }

        /// <summary>
        /// Registers the exception.
        /// </summary>
        /// <exception cref="ABI.System.Exception">sample</exception>
        public void RegisterException()
        {
            try
            {
                throw new Exception("sample exception");
            }
            catch (Exception ex)
            {
                Service.TrackException(ex);
                UpdateStatusMessage("'sample exception' sent.");
            }
        }

        /// <summary>
        /// Updates the status message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void UpdateStatusMessage(string message)
        {
            StatusMessage = message;
            OnPropertyChanged(nameof(StatusMessage));
        }
    }
}