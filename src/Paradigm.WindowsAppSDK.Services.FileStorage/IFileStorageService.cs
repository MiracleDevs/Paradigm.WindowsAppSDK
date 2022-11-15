using Paradigm.WindowsAppSDK.Services.FileStorage;
using Paradigm.WindowsAppSDK.Services.Interfaces;

/// <summary>
/// Provides an interface for a file storage service.
/// </summary>
public interface IFileStorageService : IService
{
    /// <summary>
    /// Initializes the instance.
    /// </summary>
    /// <param name="settings">The settings.</param>
    void Initialize(FileStorageSettings settings);

    /// <summary>
    /// Reads the text from installation folder asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    Task<string> ReadTextFromInstallationFolderAsync(string path);

    /// <summary>
    /// Reads the text from installation folder.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    string ReadTextFromInstallationFolder(string path);

    /// <summary>
    /// Reads the bytes from installation folder asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    Task<byte[]> ReadBytesFromInstallationFolderAsync(string path);

    /// <summary>
    /// Reads the bytes from installation folder.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    byte[] ReadBytesFromInstallationFolder(string path);

    /// <summary>
    /// Reads a text file asynchronous.
    /// </summary>
    /// <param name="path">The URI.</param>
    /// <returns>The text content.</returns>
    Task<string> ReadLocalTextAsync(string path);

    /// <summary>
    /// Reads a text file.
    /// </summary>
    /// <param name="path">The URI.</param>
    /// <returns>The text content.</returns>
    string ReadLocalText(string path);

    /// <summary>
    /// Checks if the file exists.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    bool FileExists(string fileName);

    /// <summary>
    /// Saves the file asynchronous.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    Task SaveFileAsync(string fileName, string content);

    /// <summary>
    /// Saves the file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="content">The content.</param>
    void SaveFile(string fileName, string content);

    /// <summary>
    /// Copies the file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>The new absolute path.</returns>
    string CopyFile(string fileName, string newName);

    /// <summary>
    /// Deletes the file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    void DeleteFile(string fileName);

    /// <summary>
    /// Reads the file contents as a base64 string asynchronous.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File content</returns>
    Task<string?> ReadAsBase64Async(string fileName);

    /// <summary>
    /// Reads the file contents as a base64 string.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File content</returns>
    string? ReadAsBase64(string fileName);

    /// <summary>
    /// Reads the file contents as byte array asynchronous.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File content</returns>
    Task<byte[]> ReadAsByteArrayAsync(string fileName);

    /// <summary>
    /// Reads the file contents as byte array.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File content</returns>
    byte[] ReadAsByteArray(string fileName);

    /// <summary>
    /// Gets the stream for read.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    FileStream GetStreamForRead(string fileName);

    /// <summary>
    /// Saves the base64 file asynchronous.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns></returns>
    Task<string> SaveBase64FileAsync(string fileName, string fileContent);

    /// <summary>
    /// Saves the base64 file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns></returns>
    string SaveBase64File(string fileName, string fileContent);

    /// <summary>
    /// Saves the byte array file asynchronous.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns></returns>
    Task<string> SaveByteArrayFileAsync(string fileName, byte[] fileContent);

    /// <summary>
    /// Saves the byte array file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileContent">Content of the file.</param>
    /// <returns></returns>
    string SaveByteArrayFile(string fileName, byte[] fileContent);

    /// <summary>
    /// Gets the files from folder.
    /// </summary>
    /// <param name="folderPath">The folder path.</param>
    /// <param name="useInstallationFolder">if set to <c>true</c> [use installation folder].</param>
    /// <returns></returns>
    List<string>? GetFilesFromFolder(string folderPath, bool useInstallationFolder);
}