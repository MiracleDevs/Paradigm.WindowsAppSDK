using Microsoft.Extensions.DependencyInjection;
using Paradigm.WindowsAppSDK.SampleApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Paradigm.WindowsAppSDK.SampleApp.ViewModels
{
    public class FileStorageViewModel : SampleAppPageViewModelBase
    {
        #region Properties

        /// <summary>
        /// The text
        /// </summary>
        private string _text;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get => this._text;
            set => this.SetPropertyField(ref this._text, value);
        }

        /// <summary>
        /// Gets the saved files.
        /// </summary>
        /// <value>
        /// The saved files.
        /// </value>
        public ObservableCollection<string> SavedFiles { get; private set; }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private IFileStorageService Service { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStorageViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public FileStorageViewModel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            SavedFiles = new();
            Service = serviceProvider.GetRequiredService<IFileStorageService>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            LoadSavedFiles();
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        public void SaveFile()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            Service.SaveFile($"FileStorage\\{Guid.NewGuid()}.txt", Text);
            Text = string.Empty;
            LoadSavedFiles();
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void LoadFile(string filePath)
        {
            Text = Service.ReadLocalText(filePath);
        }

        /// <summary>
        /// Loads the sample file.
        /// </summary>
        public void LoadSampleFile()
        {
            Text = Service.ReadTextFromInstallationFolder("FileStorageSample\\sampleText.txt");
        }

        /// <summary>
        /// Clears the files.
        /// </summary>
        public void ClearFiles()
        {
            foreach (var file in SavedFiles)
            {
                Service.DeleteFile($"FileStorage\\{file}");
            }

            SavedFiles.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the saved files.
        /// </summary>
        private void LoadSavedFiles()
        {
            var savedFiles = Service.GetFilesFromFolder("FileStorage", false) ?? new List<string>();
            SavedFiles = new ObservableCollection<string>(savedFiles);
            OnPropertyChanged(nameof(SavedFiles));
        }

        #endregion
    }
}
