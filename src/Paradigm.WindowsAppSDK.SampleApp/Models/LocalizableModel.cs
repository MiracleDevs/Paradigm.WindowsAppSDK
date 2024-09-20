using Paradigm.WindowsAppSDK.Services.Localization;

namespace Paradigm.WindowsAppSDK.SampleApp.Models
{
    public class LocalizableModel
    {
        [Localizable]
        public string? LocalizableTextSample1 { get; set; }

        [Localizable]
        public string? LocalizableTextSample2 { get; set; }

        [Localizable]
        public string? LocalizableTextSample3 { get; set; }

        public string? NonLocalizableText { get; set; }
    }
}