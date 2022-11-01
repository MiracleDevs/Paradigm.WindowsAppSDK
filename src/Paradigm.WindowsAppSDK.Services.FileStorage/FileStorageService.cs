using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;

namespace Paradigm.WindowsAppSDK.Services.FileStorage
{
    /// <summary>
    /// Implements the file storage service to give access to the lower level storage api.
    /// </summary>
    /// <seealso cref="IFileStorageService" />
    public class FileStorageService : IFileStorageService
    {
        private const string InstallationRootFolderName = "Assets";
        #region Properties

        /// <summary>
        /// Gets the local folder.
        /// </summary>
        /// <value>
        /// The local folder.
        /// </value>
        private StorageFolder LocalFolder { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStorageService"/> class.
        /// </summary>
        public FileStorageService()
        {
            this.LocalFolder = ApplicationData.Current.LocalFolder;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads content from application URI.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public async Task<string> ReadContentFromApplicationUriAsync(string path)
        {
            return await ReadContentFromApplicationUriAsync(this.GetLocalFileUri(path));
        }

        /// <summary>
        /// Reads the content from application URI asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public async Task<string> ReadContentFromApplicationUriAsync(Uri uri)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            return await File.ReadAllTextAsync(file.Path);
        }

        /// <summary>
        /// Reads the bytes from application URI.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public async Task<byte[]> ReadBytesFromApplicationUriAsync(string path)
        {
            var uri = this.GetLocalFileUri(path);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            return (await FileIO.ReadBufferAsync(file)).ToArray();
        }

        /// <summary>
        /// Reads a text file from the installed location.
        /// </summary>
        /// <param name="path">The URI.</param>
        /// <returns>
        /// The text content.
        /// </returns>
        public async Task<string> ReadLocalTextAsync(string path)
        {
            return await FileIO.ReadTextAsync(await this.LocalFolder.GetFileAsync(path));
        }

        /// <summary>
        /// Gets the name of the local file from a relative path.
        /// </summary>
        /// <param name="path">The relative path.</param>
        /// <returns>
        /// The absolute local filename.
        /// </returns>
        public async Task<string> GetLocalFileNameAsync(string path)
        {
            return (await this.LocalFolder.GetFileAsync(path)).Path;
        }

        /// <summary>
        /// Gets the local file URI.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="validateEmptyPath">if set to <c>true</c> [validate empty].</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        /// <returns></returns>
        public Uri GetLocalFileUri(string path, bool validateEmptyPath = false, bool useInstallationFolder = true)
        {
            if (validateEmptyPath && string.IsNullOrWhiteSpace(path))
                return default;

            var baseUri = useInstallationFolder ? $"ms-appx:///{InstallationRootFolderName}" : "ms-appdata:///local";

            return new Uri($"{baseUri}/{path}", UriKind.Absolute);
        }

        /// <summary>
        /// Checks if the file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            var filePath = Path.IsPathRooted(fileName) ? fileName : Path.Combine(this.LocalFolder.Path, fileName);
            return File.Exists(filePath);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public async Task SaveFileAsync(string fileName, string content)
        {
            var file = this.FileExists(fileName)
                ? await this.LocalFolder.GetFileAsync(fileName)
                : await this.LocalFolder.CreateFileAsync(fileName);

            await FileIO.WriteTextAsync(file, content);
        }

        /// <summary>
        /// Copies the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>
        /// The new absolute path.
        /// </returns>
        public async Task<string> CopyFileAsync(string fileName, string newName)
        {
            var file = await this.LocalFolder.GetFileAsync(Path.GetFileName(fileName));
            var newFile = await file.CopyAsync(this.LocalFolder, newName);

            return newFile.Path;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public void DeleteFile(string fileName)
        {
            var filePath = Path.IsPathRooted(fileName) ? fileName : Path.Combine(this.LocalFolder.Path, fileName);
            File.Delete(filePath);
        }

        /// <summary>
        /// Reads the file contents as a base64 string.
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
        /// Reads the file contents as byte array.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// File content
        /// </returns>
        public async Task<byte[]> ReadAsByteArrayAsync(string fileName)
        {
            fileName = Path.IsPathRooted(fileName) ? Path.GetFileName(fileName) : fileName;

            if (!this.FileExists(fileName))
            {
                return default;
            }

            var file = await this.LocalFolder.GetFileAsync(fileName);
            var buffer = await FileIO.ReadBufferAsync(file);

            return buffer.ToArray();
        }

        /// <summary>
        /// Gets the stream for read.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamForReadAsync(string fileName)
        {
            fileName = Path.IsPathRooted(fileName) ? Path.GetFileName(fileName) : fileName;

            if (!this.FileExists(fileName))
            {
                return default;
            }

            var file = await this.LocalFolder.GetFileAsync(fileName);
            return await file.OpenStreamForReadAsync();
        }

        /// <summary>
        /// Saves the base64 file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns>
        /// The absolute path.
        /// </returns>
        public async Task<string> SaveBase64FileAsync(string fileName, string fileContent)
        {
            return await this.SaveByteArrayFileAsync(fileName, Convert.FromBase64String(fileContent));
        }

        /// <summary>
        /// Saves the byte array file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileContent">Content of the file.</param>
        /// <returns>
        /// The absolute path.
        /// </returns>
        public async Task<string> SaveByteArrayFileAsync(string fileName, byte[] fileContent)
        {
            fileName = Path.IsPathRooted(fileName) ? Path.GetFileName(fileName) : fileName;

            var file = this.FileExists(fileName)
                ? await this.LocalFolder.GetFileAsync(fileName)
                : await this.LocalFolder.CreateFileAsync(fileName);

            await FileIO.WriteBytesAsync(file, fileContent);
            return file.Path;
        }

        /// <summary>
        /// Gets the files from folder.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        /// <returns></returns>
        public async Task<List<string>> GetFilesFromFolderAsync(string folderPath, bool useInstallationFolder = false)
        {
            try
            {
                var folder = useInstallationFolder
                    ? await StorageFolder.GetFolderFromPathAsync(
                        Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path,
                        InstallationRootFolderName,
                        folderPath
                        ))
                    : await this.LocalFolder.GetFolderAsync(folderPath);

                var files = await folder.GetFilesAsync();
                return files.Select(x => x.Name).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Reads the file properties.
        /// Properties reference documentation: https://docs.microsoft.com/en-us/windows/win32/properties/props
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="propertiesToRetrieve">The properties to retrieve.</param>
        /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> ReadFilePropertiesAsync(string path, IEnumerable<string> propertiesToRetrieve, bool useInstallationFolder = false)
        {
            var uri = this.GetLocalFileUri(path, useInstallationFolder: useInstallationFolder);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            return (await file.Properties.RetrievePropertiesAsync(propertiesToRetrieve)).ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion
    }
}
