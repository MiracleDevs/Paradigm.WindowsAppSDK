using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Logging.Enums;
using System;

namespace Paradigm.WindowsAppSDK.Services.Logging
{
    public interface ILogService : IService
    {

        /// <summary>
        /// Initializes the specified log type.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <param name="logFolderPath">The log folder path.</param>
        /// <param name="logFileMaxSize">Maximum size of the log file.</param>
        /// <param name="logFileName">Name of the log file.</param>
        void Initialize(LogTypes logType, string logFolderPath, int? logFileMaxSize = null, string logFileName = null);

        /// <summary>
        /// Sets the minimum type of the log.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        void SetMinimumLogType(LogTypes logType);

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Trace(string message);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Informs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Information(string message);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        void Error(string message, Exception ex = default);

        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void Error(Exception ex);
    }
}
