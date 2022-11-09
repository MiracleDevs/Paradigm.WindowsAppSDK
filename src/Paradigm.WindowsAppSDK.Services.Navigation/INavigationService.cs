using Paradigm.WindowsAppSDK.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.Services.Navigation
{
    public interface INavigationService : IService
    {
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
        /// Initializes the instance.
        /// </summary>
        /// <param name="frame">The frame.</param>
        void Initialize(INavigationFrame frame);

        /// <summary>
        /// Registers a navigable element and its paired view.
        /// </summary>
        /// <typeparam name="TNavigableView">The type of the page.</typeparam>
        /// <typeparam name="TNavigable">The type of the navigable.</typeparam>
        void Register<TNavigableView, TNavigable>() where TNavigableView : INavigableView where TNavigable : INavigable;

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