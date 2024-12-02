using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Configuration;

[JsonSerializable(typeof(List<object>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
internal partial class ListObjectJsonContext : JsonSerializerContext
{
}