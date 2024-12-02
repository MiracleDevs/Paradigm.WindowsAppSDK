using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.Services.Tests.LocalSettings;

[JsonSerializable(typeof(SettingsModel))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(object))]
internal partial class SettingsModelJsonContext : JsonSerializerContext
{
}