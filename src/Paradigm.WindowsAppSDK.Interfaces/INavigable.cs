using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.Services.Interfaces
{
    /// <summary>
    /// Provides navigation aware methods for both the navigator and the navigable elements.
    /// </summary>
    public interface INavigable
    {
        /// <summary>
        /// Determines whether this instance [can navigate to] the specified navigable element.
        /// </summary>
        /// <param name="navigable">The navigable element.</param>
        Task<bool> CanNavigateTo(INavigable navigable);

        /// <summary>
        /// Determines whether this instance [can navigate from] the specified navigable element.
        /// </summary>
        /// <param name="navigable">The navigable element.</param>
        Task<bool> CanNavigateFrom(INavigable navigable);
    }
}