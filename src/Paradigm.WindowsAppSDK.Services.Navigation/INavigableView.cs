using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    /// <summary>
    /// Provides an interface for a navigable view.
    /// </summary>
    public interface INavigableView
    {
        /// <summary>
        /// Initializes the navigation.
        /// </summary>
        /// <param name="navigable">The navigable.</param>
        Task InitializeNavigationAsync(INavigable navigable);

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        Task DisposeAsync();
    }
}