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
        /// Gets the local folder path.
        /// </summary>
        /// <value>
        /// The local folder path.
        /// </value>
        private string? LocalFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the installation folder path.
        /// </summary>
        /// <value>
        /// The installation folder path.
        /// </value>
        private string? InstallationFolderPath { get; set; }

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
        /// <param name="localFolderPath">The local folder path.</param>
        /// <param name="installationFolderPath">The installation folder path.</param>
        public void Initialize(string localFolderPath, string installationFolderPath)
        {
            this.LocalFolderPath = localFolderPath;
            this.InstallationFolderPath = installationFolderPath;
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
        public async Task<string> ReadAsBase64Async(string fileName)
        {
            var bytes = await this.ReadAsByteArrayAsync(fileName);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Reads the file contents as a base64 string.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public string ReadAsBase64(string fileName)
        {
            var bytes = this.ReadAsByteArray(fileName);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Reads the file contents as byte array asynchronous.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public async Task<byte[]> ReadAsByteArrayAsync(string fileName)
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
        public byte[] ReadAsByteArray(string fileName)
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
        public FileStream GetStreamForRead(string fileName)
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
            
            if (string.IsNullOrEmpty(LocalFolderPath) && !useInstallationFolder)
                throw new ArgumentNullException(nameof(LocalFolderPath));

            if (string.IsNullOrEmpty(InstallationFolderPath) && useInstallationFolder)
                throw new ArgumentNullException(nameof(InstallationFolderPath));

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

            return Path.IsPathRooted(fileName) ? fileName : Path.Combine(LocalFolderPath, fileName);
        }

        #endregion
    }
}