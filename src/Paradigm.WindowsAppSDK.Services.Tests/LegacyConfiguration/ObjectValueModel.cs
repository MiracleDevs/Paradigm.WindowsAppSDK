using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Tests.LegacyConfiguration
{
    internal class ObjectValueModel
    {
        [JsonPropertyName("prop1")]
        public string? Prop1 { get; set; }

        [JsonPropertyName("prop2")]
        public string? Prop2 { get; set; }
    }
}
