using Paradigm.WindowsAppSDK.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// Provides an interface for a file storage service.
/// </summary>
public interface IFileStorageService : IService
{
    /// <summary>
    /// Reads content from application URI.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    Task<string> ReadContentFromApplicationUriAsync(string path);

    /// <summary>
    /// Reads the bytes from application URI.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    Task<byte[]> ReadBytesFromApplicationUriAsync(string path);

    /// <summary>
    /// Reads a text file.
    /// </summary>
    /// <param name="path">The URI.</param>
    /// <returns>The text content.</returns>
    Task<string> ReadLocalTextAsync(string path);

    /// <summary>
    /// Gets the name of the local file from a relative path.
    /// </summary>
    /// <param name="path">The relative path.</param>
    /// <returns>The absolute local filename.</returns>
    Task<string> GetLocalFileNameAsync(string path);

    /// <summary>
    /// Gets the local file URI.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="validateEmptyPath">if set to <c>true</c> [validate empty].</param>
    /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
    /// <returns></returns>
    Uri GetLocalFileUri(string path, bool validateEmptyPath = false, bool useInstallationFolder = true);

    /// <summary>
    /// Checks if the file exists.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    bool FileExists(string fileName);

    /// <summary>
    /// Saves the file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    Task SaveFileAsync(string fileName, string content);

    /// <summary>
    /// Copies the file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>The new absolute path.</returns>
    Task<string> CopyFileAsync(string fileName, string newName);

    /// <summary>
    /// Deletes the file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    void DeleteFile(string fileName);

    /// <summary>
    /// Reads the file contents as a base64 string.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File content</returns>
    Task<string> ReadAsBase64Async(string fileName);

    /// <summary>
    /// Reads the file contents as byte array.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File content</returns>
    Task<byte[]> ReadAsByteArrayAsync(string fileName);

    /// <summary>
    /// Gets the stream for read.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    Task<Stream> GetStreamForReadAsync(string fileName);

    /// <summary>
    /// Saves the base64 file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns>The absolute path.</returns>
    Task<string> SaveBase64FileAsync(string fileName, string fileContent);

    /// <summary>
    /// Saves the byte array file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns>The absolute path.</returns>
    Task<string> SaveByteArrayFileAsync(string fileName, byte[] fileContent);

    /// <summary>
    /// Gets the files from folder.
    /// </summary>
    /// <param name="folderPath">The folder path.</param>
    /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
    /// <returns></returns>
    Task<List<string>> GetFilesFromFolderAsync(string folderPath, bool useInstallationFolder = false);

    /// <summary>
    /// Reads the file properties.
    /// Properties reference documentation: https://docs.microsoft.com/en-us/windows/win32/properties/props
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="propertiesToRetrieve">The properties to retrieve.</param>
    /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
    /// <returns></returns>
    Task<Dictionary<string, object>> ReadFilePropertiesAsync(string path, IEnumerable<string> propertiesToRetrieve, bool useInstallationFolder = false);
}