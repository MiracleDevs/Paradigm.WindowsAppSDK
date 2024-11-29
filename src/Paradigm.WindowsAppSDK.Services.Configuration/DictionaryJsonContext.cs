using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Configuration;

[JsonSerializable(typeof(Dictionary<string, object>))]
internal partial class DictionaryJsonContext : JsonSerializerContext
{
}