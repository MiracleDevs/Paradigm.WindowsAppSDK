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
        /// Gets the log file path.
        /// </summary>
        /// <value>
        /// The log file path.
        /// </value>
        public string LogFilePath => GetFilePath();

        /// <summary>
        /// Gets or sets the log folder path.
        /// </summary>
        /// <value>
        /// The log folder path.
        /// </value>
        private string? LogFolderPath => Settings?.FolderPath;

        /// <summary>
        /// The log file name
        /// </summary>
        private string? LogFileName { get; set; }

        /// <summary>
        /// The maximum log file size
        /// </summary>
        private int MaxLogFileSize { get; set; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        private LogSettings? Settings { get; set; }

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
        /// <param name="settings">The settings.</param>
        /// <exception cref="ArgumentNullException">logFolderPath</exception>
        public void Initialize(LogSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.FolderPath))
                throw new ArgumentNullException(nameof(settings.FolderPath));

            Settings = settings;
            LogFileName = string.IsNullOrWhiteSpace(settings.FileName) ? DefaultLogFileName : settings.FileName;
            MaxLogFileSize = settings.FileMaxSize.GetValueOrDefault(DefaultLogFileMaxSize);
            CheckLogFileMaxSize();
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
            var message = ex.Message;
            var stackTrace = ex.StackTrace;

            while (ex.InnerException is not null)
            {
                ex = ex.InnerException;
                message = string.Concat(message, Environment.NewLine, ex.Message);
            }

            if (!string.IsNullOrWhiteSpace(stackTrace))
                message = string.Concat(message, stackTrace);

            LogText("ERROR", message, LogTypes.Error);
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
                if (logType < MinimumLogType)
                    return;

                CheckLogFileMaxSize();

                var result = $"[{DateTime.Now:O}][{type}] - {message}{Environment.NewLine}";
                File.AppendAllText(GetFilePath(), result);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to log line: {e.Message}");
            }
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

        /// <summary>
        /// Checks the maximum size of the log file.
        /// </summary>
        private void CheckLogFileMaxSize()
        {
            if (Settings is null) return;

            try
            {
                var filePath = GetFilePath();
                var fileInfo = new FileInfo(filePath);

                if (fileInfo.Exists && fileInfo.Length > MaxLogFileSize)
                {
                    if (!Settings.ArchivePreviousFile)
                    {
                        fileInfo.Delete();
                        return;
                    }

                    var fileDirectoryPath = Path.GetDirectoryName(filePath);
                    if (string.IsNullOrWhiteSpace(fileDirectoryPath))
                        return;

                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(LogFileName);
                    var existingLogFilesCount = Directory.GetFiles(fileDirectoryPath, $"{fileNameWithoutExtension}*").Length;
                    var archivedFileName = filePath.Replace(Path.GetFileName(filePath), $"{fileNameWithoutExtension}_{existingLogFilesCount}.txt");
                    File.Move(filePath, archivedFileName);
                }
            }
            catch
            {
                // ignore
            }
        }

        #endregion
    }
}