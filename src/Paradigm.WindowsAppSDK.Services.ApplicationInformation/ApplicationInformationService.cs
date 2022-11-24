using Microsoft.UI.Windowing;
using System;
using System.Drawing;
using System.Linq;
using Windows.ApplicationModel;
using Windows.Networking.Connectivity;
using Windows.System.Profile;

namespace Paradigm.WindowsAppSDK.Services.ApplicationInformation
{
    /// <summary>
    /// Provides application information.
    /// </summary>
    /// <seealso cref="IApplicationInformationService" />
    public class ApplicationInformationService : IApplicationInformationService
    {
        /// <summary>
        /// Gets or sets the current window.
        /// </summary>
        /// <value>
        /// The current window.
        /// </value>
        private AppWindow CurrentWindow { get; set; }

        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <param name="windowId">The window identifier.</param>
        public void Initialize(Microsoft.UI.WindowId windowId)
        {
            CurrentWindow = AppWindow.GetFromWindowId(windowId);
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns></returns>
        public string GetApplicationName()
        {
            return Package.Current.DisplayName;
        }

        /// <summary>
        /// Gets the package identifier.
        /// </summary>
        /// <returns></returns>
        public string GetPackageId()
        {
            return Package.Current.Id.FullName;
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            var appVersion = Package.Current.Id.Version;
            return $"{appVersion.Major}.{appVersion.Minor}.{appVersion.Build}.{appVersion.Revision}";
        }

        /// <summary>
        /// Gets the name of the computer.
        /// </summary>
        /// <returns></returns>
        public string GetComputerName()
        {
            var hostNames = NetworkInformation.GetHostNames();
            var localName = hostNames.FirstOrDefault(name => name.DisplayName.Contains(".local"));
            return localName?.DisplayName.Replace(".local", string.Empty);
        }

        /// <summary>
        /// Gets the application visible bounds.
        /// </summary>
        /// <returns></returns>
        public Size GetVisibleBounds()
        {
            if (CurrentWindow == null)
                throw new InvalidOperationException("ApplicationInformation was not initialized");

            return new Size(CurrentWindow.Size.Width, CurrentWindow.Size.Height);
        }

        /// <summary>
        /// Gets the retail access code.
        /// </summary>
        /// <returns></returns>
        public string GetRetailAccessCode()
        {
            var retailAccessCode = default(string);

            try
            {
                if (RetailInfo.IsDemoModeEnabled)
                    retailAccessCode = RetailInfo.Properties[KnownRetailInfoProperties.RetailAccessCode]?.ToString();
            }
            catch
            {
                //ignore
            }

            return retailAccessCode;
        }

        /// <summary>
        /// Gets the store identifier.
        /// </summary>
        /// <returns></returns>
        public string GetStoreId()
        {
            var storeId = default(string);

            try
            {
                if (RetailInfo.IsDemoModeEnabled)
                    storeId = RetailInfo.Properties["StoreID"]?.ToString();
            }
            catch
            {
                //ignore
            }

            return storeId;
        }

        /// <summary>
        /// Gets the machine identifier.
        /// </summary>
        /// <returns></returns>
        public string GetMachineId()
        {
            var machineId = default(string);

            try
            {
                if (RetailInfo.IsDemoModeEnabled)
                    machineId = (RetailInfo.Properties["MachineId"]?.ToString() ?? RetailInfo.Properties["original_MachineId"]?.ToString())
                        ?.Replace("{", "")?.Replace("}", "")?.ToLowerInvariant();
            }
            catch
            {
                //ignore
            }

            return machineId;
        }

        /// <summary>
        /// Gets the sku.
        /// </summary>
        /// <returns></returns>
        public string GetSku()
        {
            var sku = default(string);

            try
            {
                if (RetailInfo.IsDemoModeEnabled)
                    sku = RetailInfo.Properties["SKU"]?.ToString();
            }
            catch
            {
                //ignore
            }

            return sku;
        }
    }
}