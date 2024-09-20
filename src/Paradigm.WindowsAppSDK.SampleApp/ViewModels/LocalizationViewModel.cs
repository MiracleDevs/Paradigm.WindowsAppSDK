using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.Models;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using Paradigm.WindowsAppSDK.Services.Localization;
using System;
using System.Collections.Generic;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class LocalizationViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the localizable text sample1.
        /// </summary>
        /// <value>
        /// The localizable text sample1.
        /// </value>
        public string LocalizableTextSample1 => Model.LocalizableTextSample1 ?? "-";

        /// <summary>
        /// Gets the localizable text sample2.
        /// </summary>
        /// <value>
        /// The localizable text sample2.
        /// </value>
        public string LocalizableTextSample2 => Model.LocalizableTextSample2 ?? "-";

        /// <summary>
        /// Gets the localizable text sample3.
        /// </summary>
        /// <value>
        /// The localizable text sample3.
        /// </value>
        public string LocalizableTextSample3 => Model.LocalizableTextSample3 ?? "-";

        /// <summary>
        /// Gets the non localizable text.
        /// </summary>
        /// <value>
        /// The non localizable text.
        /// </value>
        public string? NonLocalizableText => Model.NonLocalizableText;

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public List<string> Languages { get; }

        /// <summary>
        /// The selected language
        /// </summary>
        private string? _selectedLanguage;

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        /// <value>
        /// The selected language.
        /// </value>
        public string? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetPropertyField(ref _selectedLanguage, value) && _selectedLanguage is not null)
                    LoadLocalizationFile($"LocalizationSample/{_selectedLanguage.Replace("-", string.Empty)}.json", true);
            }
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private ILocalizationService Localization { get; }

        /// <summary>
        /// Gets the file storage.
        /// </summary>
        /// <value>
        /// The file storage.
        /// </value>
        private IFileStorageService FileStorage { get; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        private LocalizableModel Model { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public LocalizationViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Localization = serviceProvider.GetRequiredService<ILocalizationService>();
            FileStorage = serviceProvider.GetRequiredService<IFileStorageService>();
            Model = new LocalizableModel { NonLocalizableText = "This text is not localizable and won't be translated." };
            Languages = new List<string> { "en-US", "es-ES", "fr-FR" };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Exports the localization file.
        /// </summary>
        public void ExportLocalizationFile(string filePath)
        {
            var export = Localization.ExtractLocalizableStrings(Model, "sample");
            FileStorage.SaveFile(filePath, System.Text.Json.JsonSerializer.Serialize(export));
        }

        /// <summary>
        /// Loads the localization file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        public void LoadLocalizationFile(string filePath, bool useInstallationFolder = false)
        {
            var content = useInstallationFolder ? FileStorage.ReadTextFromInstallationFolder(filePath) : FileStorage.ReadLocalText(filePath);
            if (string.IsNullOrWhiteSpace(content))
                return;

            var translations = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(content);
            if (translations is not null)
                Localization.ApplyLocalizableStrings(Model, translations, "sample");

            OnAllPropertiesChanged();
        }

        #endregion
    }
}