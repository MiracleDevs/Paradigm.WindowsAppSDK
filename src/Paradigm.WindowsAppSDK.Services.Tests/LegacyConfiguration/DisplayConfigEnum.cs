using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Tests.LegacyConfiguration
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DisplayConfigEnum : int
    {
        Pixels,
        Percentage 
    }
}
