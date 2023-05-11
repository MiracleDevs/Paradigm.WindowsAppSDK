using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DisplayConfigEnum : int
    {
        Percentage,
        Pixels
    }
}
