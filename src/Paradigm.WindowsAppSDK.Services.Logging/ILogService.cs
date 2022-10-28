using Paradigm.WindowsAppSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.Services.Logging
{
    public interface ILogService : IService
    {
        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <returns></returns>
        Task InitializeAsync(int logType);

        /// <summary>
        /// Sets the minimum type of the log.
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        void SetMinimumLogType(int logType);

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
