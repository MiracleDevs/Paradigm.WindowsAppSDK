using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.Logging;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class LoggingViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// The log message
        /// </summary>
        private const string LogMessage = "Message logged from Paradigm.WindowsAppSDK SampleApp";

        /// <summary>
        /// Gets the log file text.
        /// </summary>
        /// <value>
        /// The log file text.
        /// </value>
        public string LogFileText { get; private set; }

        /// <summary>
        /// Gets the file storage service.
        /// </summary>
        /// <value>
        /// The file storage service.
        /// </value>
        private IFileStorageService FileStorageService { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public LoggingViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            FileStorageService = serviceProvider.GetRequiredService<IFileStorageService>();
            LogService.SetMinimumLogType(Services.Logging.Enums.LogTypes.Trace);
            ReloadLogFileText();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs the trace message.
        /// </summary>
        public void LogTraceMessage()
        {
            LogService.Trace(LogMessage);
            ReloadLogFileText();
        }

        /// <summary>
        /// Logs the debug message.
        /// </summary>
        public void LogDebugMessage()
        {
            LogService.Debug(LogMessage);
            ReloadLogFileText();
        }

        /// <summary>
        /// Logs the information message.
        /// </summary>
        public void LogInformationMessage()
        {
            LogService.Information(LogMessage);
            ReloadLogFileText();
        }

        /// <summary>
        /// Logs the warning message.
        /// </summary>
        public void LogWarningMessage()
        {
            LogService.Warning(LogMessage);
            ReloadLogFileText();
        }

        /// <summary>
        /// Logs the error message.
        /// </summary>
        public void LogErrorMessage()
        {
            LogService.Error(LogMessage);
            ReloadLogFileText();
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        public void LogException()
        {
            LogService.Error(new ApplicationException(LogMessage));
            ReloadLogFileText();
        }

        /// <summary>
        /// Clears the logs.
        /// </summary>
        public void ClearLogs()
        {
            FileStorageService.DeleteFile(LogService.LogFilePath);
            LogFileText = string.Empty;
            OnPropertyChanged(nameof(LogFileText));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reloads the log file text.
        /// </summary>
        private void ReloadLogFileText()
        {
            LogFileText = FileStorageService.ReadLocalText(LogService.LogFilePath);
            OnPropertyChanged(nameof(LogFileText));
        }

        #endregion
    }
}