using Microsoft.UI;
using Microsoft.UI.Xaml;
using System;

namespace Paradigm.WindowsAppSDK.Xaml.Extensions
{
    public static class WindowExtensions
    {
        /// <summary>
        /// Gets the window handle.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns></returns>
        public static IntPtr GetWindowHandle(this Window window)
        {
            return WinRT.Interop.WindowNative.GetWindowHandle(window);
        }

        /// <summary>
        /// Gets the window identifier.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns></returns>
        public static WindowId GetWindowId(this Window window)
        {
            var windowHandle = window.GetWindowHandle();
            return Win32Interop.GetWindowIdFromWindow(windowHandle);
        }

        /// <summary>
        /// Initializes the target.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="target">The target.</param>
        public static void InitializeTarget(this Window window, object target)
        {
            WinRT.Interop.InitializeWithWindow.Initialize(target, window.GetWindowHandle());
        }
    }
}