﻿using Paradigm.WindowsAppSDK.SampleApp.ViewModels;
using Paradigm.WindowsAppSDK.Services.Interfaces;
using Paradigm.WindowsAppSDK.Services.Navigation;
using System.Threading.Tasks;

namespace Paradigm.WindowsAppSDK.SampleApp.Views.Pages
{
    public sealed partial class ConfigurationPage : INavigableView
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        private ConfigurationViewModel? ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationPage"/> class.
        /// </summary>
        public ConfigurationPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public async Task DisposeAsync()
        {
            this.ViewModel?.Dispose();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Initializes the navigation.
        /// </summary>
        /// <param name="navigable">The navigable.</param>
        public async Task InitializeNavigationAsync(INavigable navigable)
        {
            this.ViewModel = (ConfigurationViewModel)navigable;
            this.ViewModel.Initialize();
            await Task.CompletedTask;
        }
    }
}