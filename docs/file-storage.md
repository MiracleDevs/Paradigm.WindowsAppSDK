# File Storage Service

The `Paradigm.WindowsAppSDK.Services.FileStorage` package provides a comprehensive file system access service for Windows App SDK applications, simplifying file operations across different storage locations.

## Installation

```powershell
Install-Package Paradigm.WindowsAppSDK.Services.FileStorage
```

## Key Components

### IFileStorageService

The core interface defining methods for file operations across various storage locations.

Key features include:
- Reading and writing files
- Checking if files exist
- Listing files and directories
- Copying, moving, and deleting files
- Creating and deleting directories
- Support for both text and binary data
- Getting file information
- URI conversion

### FileStorageService

The implementation of `IFileStorageService` that handles all file operations.

### FileStorageSettings

Configuration settings for the file storage service, including:

```csharp
public class FileStorageSettings
{
    /// <summary>
    /// Gets or sets the application folder.
    /// </summary>
    public string ApplicationFolder { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether to create directory if not exists.
    /// </summary>
    public bool CreateDirectoryIfNotExists { get; set; } = true;
}
```

## Storage Locations

The file storage service supports multiple storage locations:

- **Local Application Data**: Storage for application data that is not backed up to the cloud
- **Roaming Application Data**: Storage for application data that is synchronized across devices
- **Temporary Application Data**: Storage for temporary files that may be deleted by the system
- **Local Cache**: Storage for cached data
- **Shared Local**: Storage for files that are local to the machine but can be accessed by all users
- **Current Installation**: The application's installation directory (read-only)
- **Current Working**: The current working directory
- **Custom**: Custom specified paths

## Usage

### Service Registration

Register the file storage service in your application's startup:

```csharp
services.AddSingleton<IFileStorageService, FileStorageService>();
```

### Basic File Operations

```csharp
// Get the service
var fileStorageService = ServiceLocator.Instance.GetRequiredService<IFileStorageService>();

// Write a text file to local application data
await fileStorageService.WriteTextFileAsync("config.json", jsonContent, StorageLocation.LocalApplicationData);

// Read a text file from local application data
var content = await fileStorageService.ReadTextFileAsync("config.json", StorageLocation.LocalApplicationData);

// Check if a file exists
bool exists = fileStorageService.FileExists("config.json", StorageLocation.LocalApplicationData);

// Delete a file
await fileStorageService.DeleteFileAsync("temp.txt", StorageLocation.TemporaryApplicationData);
```

### Directory Operations

```csharp
// Create a directory
await fileStorageService.CreateDirectoryAsync("logs", StorageLocation.LocalApplicationData);

// Check if a directory exists
bool exists = fileStorageService.DirectoryExists("logs", StorageLocation.LocalApplicationData);

// List files in a directory
var files = await fileStorageService.ListFilesAsync("logs", StorageLocation.LocalApplicationData);

// List subdirectories
var directories = await fileStorageService.ListDirectoriesAsync("data", StorageLocation.LocalApplicationData);

// Delete a directory
await fileStorageService.DeleteDirectoryAsync("temp", StorageLocation.TemporaryApplicationData);
```

### Binary File Operations

```csharp
// Write a binary file
byte[] data = GetBinaryData();
await fileStorageService.WriteBinaryFileAsync("image.png", data, StorageLocation.LocalApplicationData);

// Read a binary file
byte[] imageData = await fileStorageService.ReadBinaryFileAsync("image.png", StorageLocation.LocalApplicationData);
```

### File Information

```csharp
// Get file information
var fileInfo = await fileStorageService.GetFileInfoAsync("document.txt", StorageLocation.LocalApplicationData);
var size = fileInfo.Size;
var modified = fileInfo.DateModified;
```

### Working with URIs

```csharp
// Get a URI for a file (useful for loading images, etc.)
var uri = fileStorageService.GetLocalFileUri("image.png", StorageLocation.LocalApplicationData);
```

### Custom Paths

```csharp
// Work with a custom path
await fileStorageService.WriteTextFileAsync("data.txt", content, StorageLocation.Custom, "C:\\Data");
```

## Advanced Usage

### Configuring the Service

Configure the file storage service with custom settings:

```csharp
services.AddSingleton(new FileStorageSettings
{
    ApplicationFolder = "MyApp",
    CreateDirectoryIfNotExists = true
});
```

### Copy and Move Operations

```csharp
// Copy a file
await fileStorageService.CopyFileAsync(
    "source.txt", StorageLocation.LocalApplicationData,
    "destination.txt", StorageLocation.RoamingApplicationData);

// Move a file
await fileStorageService.MoveFileAsync(
    "source.txt", StorageLocation.TemporaryApplicationData,
    "destination.txt", StorageLocation.LocalApplicationData);
```

### Stream Operations

```csharp
// Read a file as a stream
using (var stream = await fileStorageService.OpenFileStreamAsync("document.txt", StorageLocation.LocalApplicationData))
{
    // Process the stream
}

// Write content to a stream
using (var stream = await fileStorageService.CreateFileStreamAsync("output.txt", StorageLocation.LocalApplicationData))
{
    // Write to the stream
}
``` 