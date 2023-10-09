using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Tests.LegacyConfiguration
{
    internal class ObjectValueModel
    {
        [JsonPropertyName("prop1")]
        public string? Prop1 { get; set; }

        [JsonPropertyName("prop2")]
        public string? Prop2 { get; set; }

        [JsonPropertyName("prop3")]
        public DisplayConfigEnum Prop3 { get; set; }

        [JsonPropertyName("prop4")]
        public List<string>? Prop4 { get; set; }
    }
}