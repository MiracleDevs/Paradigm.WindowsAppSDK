using Paradigm.WindowsAppSDK.Services.Interfaces;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public interface INavigationService : IService
    {
        /// <summary>
        /// Activates the instance.
        /// </summary>
        void ActivateInstance();

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        void DeactivateInstance();

        /// <summary>
        /// Gets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        bool CanGoBack { get; }

        /// <summary>
        /// Gets a value indicating whether this instance can go forward.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        bool CanGoForward { get; }

        /// <summary>
        /// Gets the current navigable.
        /// </summary>
        /// <value>
        /// The current navigable.
        /// </value>
        INavigable CurrentNavigable { get; }

        /// <summary>
        /// Goes back to the previous navigable.
        /// </summary>
        Task<bool> GoBackAsync();

        /// <summary>
        /// Goes forward to the next navigable.
        /// </summary>
        Task<bool> GoForwardAsync();

        /// <summary>
        /// Navigates to a navigable element.
        /// </summary>
        /// <typeparam name="TNavigable">The type of the navigable.</typeparam>
        /// <returns></returns>
        Task<bool> NavigateToAsync<TNavigable>() where TNavigable : INavigable;

        /// <summary>
        /// Navigates to a navigable element.
        /// </summary>
        /// <param name="navigableType">Type of the navigable.</param>
        /// <returns></returns>
        Task<bool> NavigateToAsync(Type navigableType);

        /// <summary>
        /// Clears the back stack.
        /// </summary>
        void ClearBackStack();
    }
}