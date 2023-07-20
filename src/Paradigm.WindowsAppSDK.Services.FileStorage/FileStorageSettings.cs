namespace Paradigm.WindowsAppSDK.Services.FileStorage
{
    public class FileStorageSettings
    {
        /// <summary>
        /// Gets the local folder path.
        /// </summary>
        /// <value>
        /// The local folder path.
        /// </value>
        public string? LocalFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the installation folder path.
        /// </summary>
        /// <value>
        /// The installation folder path.
        /// </value>
        public string? InstallationFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the local base URI.
        /// </summary>
        /// <value>
        /// The local base URI.
        /// </value>
        public string? LocalBaseUri { get; set; }

        /// <summary>
        /// Gets or sets the installation base URI.
        /// </summary>
        /// <value>
        /// The installation base URI.
        /// </value>
        public string? InstallationBaseUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [create directories].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [create directories]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateDirectories { get; set; }
    }
}
