using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Logging
{
    public class LogService : ILogService
    {
        #region Consts

        private const string DefaultLogFileName = "log.txt";

        private const int DefaultLogFileMaxSize = 1024 * 1024 * 20; //20MB

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the log folder path.
        /// </summary>
        /// <value>
        /// The log folder path.
        /// </value>
        private string? LogFolderPath { get; set; }

        /// <summary>
        /// The log file name
        /// </summary>
        private string? LogFileName { get; set; }

        /// <summary>
        /// The maximum log file size
        /// </summary>
        private int MaxLogFileSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum type of the log.
        /// </summary>
        /// <value>The minimum type of the log.</value>
        private LogTypes MinimumLogType { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="logFolderPath">The log folder path.</param>
        /// <param name="logFileMaxSize">Maximum size of the log file.</param>
        /// <param name="logFileName">Name of the log file.</param>
        /// <exception cref="ArgumentNullException">logFolderPath</exception>
        public void Initialize(string logFolderPath, int? logFileMaxSize = default, string? logFileName = default)
        {
            if (string.IsNullOrWhiteSpace(logFolderPath))
                throw new ArgumentNullException(nameof(logFolderPath));

            LogFolderPath = logFolderPath;
            LogFileName = string.IsNullOrWhiteSpace(logFileName) ? DefaultLogFileName : logFileName;
            MaxLogFileSize = logFileMaxSize.GetValueOrDefault(DefaultLogFileMaxSize);

            // checks if the log file exists, and if its larger than 20mb then deletes it to start again.
            var fileInfo = new FileInfo(GetFilePath());
            if (fileInfo.Exists && fileInfo.Length > MaxLogFileSize)
                fileInfo.Delete();
        }

        /// <summary>
        /// Sets the minimum type of the log.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        public void SetMinimumLogType(LogTypes logType)
        {
            this.MinimumLogType = logType;
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
        public void Error(string message)
        {
            LogText("ERROR", message, LogTypes.Error);
        }

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="ex"></param>
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
            if (string.IsNullOrWhiteSpace(LogFolderPath))
                throw new ArgumentNullException(nameof(LogFolderPath));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            try
            {
                if (logType < this.MinimumLogType)
                    return;
                
                var result = $"[{DateTime.Now:O}][{type}] - {message}{Environment.NewLine}";
                AppendText(result);
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
            File.AppendAllText(GetFilePath(), content);
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
        {
            if (string.IsNullOrWhiteSpace(LogFolderPath) || string.IsNullOrWhiteSpace(LogFileName))
                return string.Empty;

            return Path.IsPathRooted(LogFileName) ? LogFileName : Path.Combine(LogFolderPath, LogFileName);
        }

        #endregion
    }
}