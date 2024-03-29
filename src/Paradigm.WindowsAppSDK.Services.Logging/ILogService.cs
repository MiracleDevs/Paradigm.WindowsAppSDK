﻿using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Logging.Enums;

namespace Paradigm.WindowsAppSDK.Services.Logging
{
    public interface ILogService : IService
    {
        /// <summary>
        /// Gets the log file path.
        /// </summary>
        /// <value>
        /// The log file path.
        /// </value>
        string LogFilePath { get; }

        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void Initialize(LogSettings settings);

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
        void Error(string message);

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Error(Exception exception);
    }
}
