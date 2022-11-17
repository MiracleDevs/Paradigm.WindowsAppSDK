using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.ApplicationInformation;
using System;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class ApplicationInformationViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the application information.
        /// </summary>
        /// <value>
        /// The application information.
        /// </value>
        private IApplicationInformationService ApplicationInformation { get; }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName => ApplicationInformation.GetApplicationName();

        /// <summary>
        /// Gets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public string PackageId => ApplicationInformation.GetPackageId();

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version => ApplicationInformation.GetVersion();

        /// <summary>
        /// Gets the name of the computer.
        /// </summary>
        /// <value>
        /// The name of the computer.
        /// </value>
        public string ComputerName => ApplicationInformation.GetComputerName();

        /// <summary>
        /// Gets the retail access code.
        /// </summary>
        /// <value>
        /// The retail access code.
        /// </value>
        public string RetailAccessCode => ApplicationInformation.GetRetailAccessCode();

        /// <summary>
        /// Gets the store identifier.
        /// </summary>
        /// <value>
        /// The store identifier.
        /// </value>
        public string StoreId => ApplicationInformation.GetStoreId();

        /// <summary>
        /// Gets the machine identifier.
        /// </summary>
        /// <value>
        /// The machine identifier.
        /// </value>
        public string MachineId => ApplicationInformation.GetMachineId();

        /// <summary>
        /// Gets the sku.
        /// </summary>
        /// <value>
        /// The sku.
        /// </value>
        public string Sku => ApplicationInformation.GetSku();

        /// <summary>
        /// Gets the visible bounds.
        /// </summary>
        /// <value>
        /// The visible bounds.
        /// </value>
        public string VisibleBounds
        {
            get
            {
                var visibleBounds = ApplicationInformation.GetVisibleBounds();
                return $"W{visibleBounds.Width} x H{visibleBounds.Height}";
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInformationViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ApplicationInformationViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            ApplicationInformation = serviceProvider.GetRequiredService<IApplicationInformationService>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            OnAllPropertiesChanged();
        }

        #endregion
    }
}