using Paradigm.WindowsAppSDK.Services.Localization;

namespace Paradigm.WindowsAppSDK.Services.Tests.Localization
{
    internal class LocalizableChildModel
    {
        public string? Id { get; set; }
        [Localizable]

        public string? Name { get; set; }
        [Localizable]
        public string? Description { get; set; }
    }
}