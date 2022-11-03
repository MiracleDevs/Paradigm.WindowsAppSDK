using Paradigm.WindowsAppSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.Services.MainThreadDispatcher
{
    /// <summary>
    /// Provides a way of executing methods in the main thread.
    /// </summary>
    public interface IMainThreadDispatcherService : IService
    {
        /// <summary>
        /// Runs the function on the main thread.
        /// </summary>
        /// <param name="function">The function.</param>
        Task RunAsync(Func<Task> function);

        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        void Run(Action action);
    }
}