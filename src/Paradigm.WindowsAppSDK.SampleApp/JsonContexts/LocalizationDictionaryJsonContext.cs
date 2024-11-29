using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Paradigm.WindowsAppSDK.SampleApp.JsonContexts;

[JsonSerializable(typeof(Dictionary<string, string>))]
internal partial class LocalizationDictionaryJsonContext : JsonSerializerContext
{
}