using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Tests.Configuration;

[JsonSerializable(typeof(List<ObjectValueModel>))]
internal partial class ListObjectValueModelJsonContext : JsonSerializerContext
{
}