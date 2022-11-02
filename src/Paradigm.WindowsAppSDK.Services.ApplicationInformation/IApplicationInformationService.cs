using Paradigm.WindowsAppSDK.Services.Interfaces;
using System.Drawing;

namespace Paradigm.WindowsAppSDK.Services.ApplicationInformation
{
    /// <summary>
    /// Provides an interface for an application information service.
    /// </summary>
    public interface IApplicationInformationService : IService
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns></returns>
        string GetApplicationName();

        /// <summary>
        /// Gets the package identifier.
        /// </summary>
        /// <returns></returns>
        string GetPackageId();

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <returns></returns>
        string GetVersion();

        /// <summary>
        /// Gets the name of the computer.
        /// </summary>
        /// <returns></returns>
        string GetComputerName();

        /// <summary>
        /// Gets the application visible bounds.
        /// </summary>
        /// <returns></returns>
        Size GetVisibleBounds();

        /// <summary>
        /// Gets the retail access code.
        /// </summary>
        /// <returns></returns>
        string GetRetailAccessCode();

        /// <summary>
        /// Gets the store identifier.
        /// </summary>
        /// <returns></returns>
        string GetStoreId();

        /// <summary>
        /// Gets the machine identifier.
        /// </summary>
        /// <returns></returns>
        string GetMachineId();

        /// <summary>
        /// Gets the sku.
        /// </summary>
        /// <returns></returns>
        string GetSku();
    }
}