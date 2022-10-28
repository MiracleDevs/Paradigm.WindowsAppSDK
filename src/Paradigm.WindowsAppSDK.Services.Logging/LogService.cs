using System.Threading.Tasks;
using System;
using Windows.Storage;
using Paradigm.WindowsAppSDK.Services.Logging.Enums;
using System.IO;

namespace Paradigm.WindowsAppSDK.Services.Logging
{
    public class LogService : ILogService
    {
        #region Properties

        /// <summary>
        /// The log file name
        /// </summary>
        private const string LogFileName = "log.txt";

        /// <summary>
        /// The maximum log file size
        /// </summary>
        private const int MaxLogFileSize = 1024 * 1024 * 20;

        /// <summary>
        /// Gets or sets the minimum type of the log.
        /// </summary>
        /// <value>The minimum type of the log.</value>
        private LogTypes MinimumLogType { get; set; }

        /// <summary>
        /// Gets the local folder.
        /// </summary>
        /// <value>
        /// The local folder.
        /// </value>
        private StorageFolder LocalFolder { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        public LogService()
        {
            LocalFolder = ApplicationData.Current.TemporaryFolder;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        public async Task InitializeAsync(int logType)
        {
            var fileName = GetFileName();

            // checks if the log file exists, and if its larger than 20mb then deletes it to start again.
            if (File.Exists(fileName))
            {
                var file = await GetFileAsync();
                var properties = await file.GetBasicPropertiesAsync();

                if (properties.Size > MaxLogFileSize)
                    await file.DeleteAsync();
            }
            this.MinimumLogType = (LogTypes)logType;
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Trace(string message)
        {
            LogText("TRACE", message, LogTypes.Trace);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            LogText("DEBUG", message, LogTypes.Debug);
        }

        /// <summary>
        /// Informs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Information(string message)
        {
            LogText("INFO ", message, LogTypes.Info);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(string message)
        {
            LogText("WARN ", message, LogTypes.Warning);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Error(string message, Exception ex = null)
        {
            LogText("ERROR", message, LogTypes.Error);
        }

        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Error(Exception ex)
        {
            LogText("ERROR", ex.Message, LogTypes.Error);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Logs the text.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        private void LogText(string type, string message, LogTypes logType)
        {
            try
            {
                if (logType < this.MinimumLogType)
                    return;

                var result = $"[{DateTime.Now:O}][{type}] - {message}{Environment.NewLine}";
                AppendText(result);
                System.Diagnostics.Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to log line: {e.Message}");
            }
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="content">The content.</param>
        private void AppendText(string content)
        {
            File.AppendAllText(GetFileName(), content);
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            return Path.IsPathRooted(LogFileName) ? LogFileName : Path.Combine(LocalFolder.Path, LogFileName);
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <returns></returns>
        private async Task<StorageFile> GetFileAsync()
        {
            return await LocalFolder.GetFileAsync(LogFileName);
        }

        /// <summary>
        /// Sets the minimum type of the log.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        public void SetMinimumLogType(int logType)
        {
            this.MinimumLogType = (LogTypes)logType;
        }

        #endregion
    }
}
