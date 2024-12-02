using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Tests.Configuration;

[JsonSerializable(typeof(ObjectValueModel))]
internal partial class ObjectValueModelJsonContext : JsonSerializerContext
{
}