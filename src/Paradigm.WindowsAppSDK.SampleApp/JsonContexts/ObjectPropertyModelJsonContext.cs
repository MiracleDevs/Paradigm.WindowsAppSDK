using Paradigm.WindowsAppSDK.SampleApp.Models;
using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.JsonContexts;

[JsonSerializable(typeof(ObjectPropertyModel))]
internal partial class ObjectPropertyModelJsonContext : JsonSerializerContext
{
}