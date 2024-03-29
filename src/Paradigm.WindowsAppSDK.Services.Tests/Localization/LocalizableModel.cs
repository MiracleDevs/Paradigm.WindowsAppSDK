﻿using Paradigm.WindowsAppSDK.Services.Localization;

namespace Paradigm.WindowsAppSDK.Services.Tests.Localization
{
    internal class LocalizableModel
    {
        public string? Id { get; set; }

        [Localizable]
        public string? Heading { get; set; }

        [Localizable]
        public string? Paragraph { get; set; }

        public IList<LocalizableChildModel>? Items { get; internal set; }

        public IDictionary<string, LocalizableChildModel>? AllItems { get; internal set; }
    }
}