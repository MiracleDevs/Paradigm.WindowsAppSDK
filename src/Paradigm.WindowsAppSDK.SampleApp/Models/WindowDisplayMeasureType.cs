using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WindowDisplayMeasureType : int
    {
        Percentage,
        Pixels
    }
}
