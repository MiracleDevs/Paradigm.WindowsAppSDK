namespace Paradigm.WindowsAppSDK.Services.FileStorage
{
    /// <summary>
    /// Implements the file storage service to give access to the lower level storage api.
    /// </summary>
    /// <seealso cref="IFileStorageService" />
    public class FileStorageService : IFileStorageService
    {
        #region Properties

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        private FileStorageSettings? Settings { get; set; }

        /// <summary>
        /// Gets the local folder path.
        /// </summary>
        /// <value>
        /// The local folder path.
        /// </value>
        private string? LocalFolderPath => Settings?.LocalFolderPath;

        /// <summary>
        /// Gets the installation folder path.
        /// </summary>
        /// <value>
        /// The installation folder path.
        /// </value>
        private string? InstallationFolderPath => Settings?.InstallationFolderPath;

        /// <summary>
        /// Gets the local base URI.
        /// </summary>
        /// <value>
        /// The local base URI.
        /// </value>
        private string? LocalBaseUri => Settings?.LocalBaseUri;

        /// <summary>
        /// Gets the installation base URI.
        /// </summary>
        /// <value>
        /// The installation base URI.
        /// </value>
        private string? InstallationBaseUri => Settings?.InstallationBaseUri;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStorageService"/> class.
        /// </summary>
        public FileStorageService()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Initialize(FileStorageSettings settings)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// Reads the text from installation folder asynchronous.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">InstallationFolderPath</exception>
        public async Task<string> ReadTextFromInstallationFolderAsync(string path)
        {
            if (string.IsNullOrEmpty(InstallationFolderPath))
                throw new ArgumentNullException(nameof(InstallationFolderPath));

            return await File.ReadAllTextAsync(Path.Combine(InstallationFolderPath, path));
        }

        /// <summary>
        /// Reads the text from installation folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">InstallationFolderPath</exception>
        public string ReadTextFromInstallationFolder(string path)
        {
            if (string.IsNullOrEmpty(InstallationFolderPath))
                throw new ArgumentNullException(nameof(InstallationFolderPath));

            return File.ReadAllText(Path.Combine(InstallationFolderPath, path));
        }

        /// <summary>
        /// Reads the bytes from installation folder asynchronous.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">InstallationFolderPath</exception>
        public async Task<byte[]> ReadBytesFromInstallationFolderAsync(string path)
        {
            if (string.IsNullOrEmpty(InstallationFolderPath))
                throw new ArgumentNullException(nameof(InstallationFolderPath));

            return await File.ReadAllBytesAsync(Path.Combine(InstallationFolderPath, path));
        }

        /// <summary>
        /// Reads the bytes from installation folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">InstallationFolderPath</exception>
        public byte[] ReadBytesFromInstallationFolder(string path)
        {
            if (string.IsNullOrEmpty(InstallationFolderPath))
                throw new ArgumentNullException(nameof(InstallationFolderPath));

            return File.ReadAllBytes(Path.Combine(InstallationFolderPath, path));
        }

        /// <summary>
        /// Reads a text file asynchronous.
        /// </summary>
        /// <param name="path">The URI.</param>
        /// <returns>
        /// The text content.
        /// </returns>
        public async Task<string> ReadLocalTextAsync(string path)
        {
            if (string.IsNullOrEmpty(LocalFolderPath))
                throw new ArgumentNullException(nameof(LocalFolderPath));

            return await File.ReadAllTextAsync(Path.Combine(LocalFolderPath, path));
        }

        /// <summary>
        /// Reads a text file.
        /// </summary>
        /// <param name="path">The URI.</param>
        /// <returns>
        /// The text content.
        /// </returns>
        public string ReadLocalText(string path)
        {
            if (string.IsNullOrEmpty(LocalFolderPath))
                throw new ArgumentNullException(nameof(LocalFolderPath));

            return File.ReadAllText(Path.Combine(LocalFolderPath, path));
        }

        /// <summary>
        /// Checks if the file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            var filePath = GetFilePath(fileName);
            return File.Exists(filePath);
        }

        /// <summary>
        /// Saves the file asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        public async Task SaveFileAsync(string fileName, string content)
        {
            var filePath = GetFilePath(fileName);
            await File.WriteAllTextAsync(filePath, content);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        public void SaveFile(string fileName, string content)
        {
            var filePath = GetFilePath(fileName);
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// Copies the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>
        /// The new absolute path.
        /// </returns>
        public string CopyFile(string fileName, string newName)
        {
            var sourceFile = GetFilePath(fileName);
            var destFile = GetFilePath(newName);

            File.Copy(sourceFile, destFile, true);

            return destFile;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public void DeleteFile(string fileName)
        {
            File.Delete(GetFilePath(fileName));
        }

        /// <summary>
        /// Reads the file contents as a base64 string asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public async Task<string?> ReadAsBase64Async(string fileName)
        {
            var bytes = await this.ReadAsByteArrayAsync(fileName);
            return bytes == null ? await Task.FromResult((string?)null) : Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Reads the file contents as a base64 string.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public string? ReadAsBase64(string fileName)
        {
            var bytes = this.ReadAsByteArray(fileName);
            return bytes == null ? null : Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Reads the file contents as byte array asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public async Task<byte[]?> ReadAsByteArrayAsync(string fileName)
        {
            if (!this.FileExists(fileName))
                return default;

            return await File.ReadAllBytesAsync(GetFilePath(fileName));
        }

        /// <summary>
        /// Reads the file contents as byte array.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public byte[]? ReadAsByteArray(string fileName)
        {
            if (!this.FileExists(fileName))
                return default;

            return File.ReadAllBytes(GetFilePath(fileName));
        }

        /// <summary>
        /// Gets the stream for read.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public FileStream? GetStreamForRead(string fileName)
        {
            if (!this.FileExists(fileName))
                return default;

            return File.OpenRead(GetFilePath(fileName));
        }

        /// <summary>
        /// Saves the base64 file asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns></returns>
        public async Task<string> SaveBase64FileAsync(string fileName, string fileContent)
        {
            return await this.SaveByteArrayFileAsync(fileName, Convert.FromBase64String(fileContent));
        }

        /// <summary>
        /// Saves the base64 file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns></returns>
        public string SaveBase64File(string fileName, string fileContent)
        {
            return this.SaveByteArrayFile(fileName, Convert.FromBase64String(fileContent));
        }

        /// <summary>
        /// Saves the byte array file asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns></returns>
        public async Task<string> SaveByteArrayFileAsync(string fileName, byte[] fileContent)
        {
            var filePath = GetFilePath(fileName);
            await File.WriteAllBytesAsync(filePath, fileContent);

            return filePath;
        }

        /// <summary>
        /// Saves the byte array file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns></returns>
        public string SaveByteArrayFile(string fileName, byte[] fileContent)
        {
            var filePath = GetFilePath(fileName);
            File.WriteAllBytes(filePath, fileContent);

            return filePath;
        }

        /// <summary>
        /// Gets the files from folder.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// LocalFolderPath
        /// or
        /// InstallationFolderPath
        /// </exception>
        public List<string>? GetFilesFromFolder(string folderPath, bool useInstallationFolder)
        {
            var parentFolderPath = useInstallationFolder ? this.InstallationFolderPath : LocalFolderPath;

            if (string.IsNullOrEmpty(parentFolderPath))
                throw new ArgumentNullException(nameof(parentFolderPath));

            try
            {
                return Directory.GetFiles(Path.Combine(parentFolderPath, folderPath)).Select(x => Path.GetFileName(x)).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the local file URI.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="validateEmptyPath">if set to <c>true</c> [validate empty path].</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        /// <returns></returns>
        public Uri? GetLocalFileUri(string path, bool validateEmptyPath = false, bool useInstallationFolder = true)
        {
            if (string.IsNullOrEmpty(LocalBaseUri))
                throw new ArgumentNullException(nameof(LocalBaseUri));

            if (string.IsNullOrEmpty(InstallationBaseUri))
                throw new ArgumentNullException(nameof(InstallationBaseUri));

            if (validateEmptyPath && string.IsNullOrWhiteSpace(path))
                return default;

            var baseUri = useInstallationFolder ? InstallationBaseUri : LocalBaseUri;

            return new Uri($"{baseUri}/{path}", UriKind.Absolute);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private string GetFilePath(string fileName)
        {
            if (string.IsNullOrEmpty(LocalFolderPath))
                throw new ArgumentNullException(nameof(LocalFolderPath));

            var filePath = Path.IsPathRooted(fileName) ? fileName : Path.Combine(LocalFolderPath, fileName);
            CreateDirectoryIfNotExists(filePath);

            return filePath;
        }

        /// <summary>
        /// Creates the directory if not exists.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        private void CreateDirectoryIfNotExists(string filePath)
        {
            if (Settings is not null && !Settings.CreateDirectories) return;

            var directoryPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        #endregion
    }
}