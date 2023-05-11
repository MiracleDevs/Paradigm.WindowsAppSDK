using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.Models
{
    public class ObjectPropertyModel
    {
        [JsonPropertyName("prop1")]
        public string Prop1 { get; set; }

        [JsonPropertyName("prop2")]
        public string Prop2 { get; set; }

        [JsonPropertyName("prop3")]
        public object Prop3 { get; set; }

        [JsonPropertyName("prop4")]
        public DisplayConfigEnum Prop4 { get; set; }
        public override string ToString()
        {
            return $"prop1 = {Prop1}, prop2 = {Prop2}, prop3 = {Prop3}, prop4 = {Prop4}";
        }
    }
}
