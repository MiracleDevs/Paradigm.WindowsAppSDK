using Paradigm.WindowsAppSDK.Services.FileStorage;

namespace Paradigm.WindowsAppSDK.Services.Tests.FileStorage
{
    public abstract class FileStorageTestsBase
    {
        protected string LocalFolderPath { get; set; }

        protected string InstallationFolderPath { get; set; }

        protected IFileStorageService Sut { get; private set; }

        public FileStorageTestsBase()
        {
            this.LocalFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileStorage", "LocalState");
            this.InstallationFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileStorage");
        }

        [SetUp]
        public virtual void Setup()
        {
            this.Sut = new FileStorageService();
            this.Sut.Initialize(new FileStorageSettings
            {
                LocalFolderPath = this.LocalFolderPath,
                InstallationFolderPath = this.InstallationFolderPath
            });
        }
    }
}
