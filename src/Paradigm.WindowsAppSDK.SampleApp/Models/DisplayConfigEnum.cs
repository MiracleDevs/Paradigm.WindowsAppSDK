using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter<DisplayConfigEnum>))]
    public enum DisplayConfigEnum : int
    {
        Percentage,
        Pixels
    }
}
