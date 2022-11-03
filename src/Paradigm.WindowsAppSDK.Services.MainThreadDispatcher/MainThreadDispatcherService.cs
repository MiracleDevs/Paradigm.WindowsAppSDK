using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Paradigm.WindowsAppSDK.Services.MainThreadDispatcher
{
    /// <summary>
    /// Provides a way to execute functions in the main thread.
    /// </summary>
    /// <seealso cref="IMainThreadDispatcherService" />
    public class MainThreadDispatcherService : IMainThreadDispatcherService
    {
        #region Public Methods

        /// <summary>
        /// Runs the function on the main thread.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public async Task RunAsync(Func<Task> function)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await function());
        }

        /// <summary>
        /// Runs the specified action on the main thread.
        /// </summary>
        /// <param name="action">The action.</param>
        public async void Run(Action action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        #endregion
    }
}