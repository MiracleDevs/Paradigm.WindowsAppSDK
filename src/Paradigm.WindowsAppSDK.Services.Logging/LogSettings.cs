namespace Paradigm.WindowsAppSDK.Services.Logging
{
    public class LogSettings
    {
        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <value>
        /// The folder path.
        /// </value>
        public string FolderPath { get; }

        /// <summary>
        /// Gets the maximum size of the file.
        /// </summary>
        /// <value>
        /// The maximum size of the file.
        /// </value>
        public int? FileMaxSize { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string? FileName { get; }

        /// <summary>
        /// Gets the archive previous file.
        /// </summary>
        /// <value>
        /// The archive previous file.
        /// </value>
        public bool ArchivePreviousFile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogSettings"/> class.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="fileMaxSize">Maximum size of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="archivePreviousFile">if set to <c>true</c> [archive previous file].</param>
        public LogSettings(string folderPath, int? fileMaxSize = default, string? fileName = default, bool archivePreviousFile = false)
        {
            FolderPath = folderPath;
            FileMaxSize = fileMaxSize;
            FileName = fileName;
            ArchivePreviousFile = archivePreviousFile;
        }
    }
}