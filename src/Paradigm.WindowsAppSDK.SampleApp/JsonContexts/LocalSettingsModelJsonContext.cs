using Paradigm.WindowsAppSDK.SampleApp.Models;
using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.JsonContexts;

[JsonSerializable(typeof(LocalSettingsModel))]
internal partial class LocalSettingsModelJsonContext : JsonSerializerContext
{
}