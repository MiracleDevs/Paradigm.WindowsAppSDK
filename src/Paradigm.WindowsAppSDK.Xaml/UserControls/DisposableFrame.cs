using Microsoft.UI.Xaml.Controls;
using System;

namespace Paradigm.WindowsAppSDK.Xaml.UserControls
{
    public class DisposableFrame : Frame, IDisposable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            (Content as IDisposable)?.Dispose();
        }
    }
}