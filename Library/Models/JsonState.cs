using System.Text.Json;

namespace ShareData;

internal class JsonState
{
    public JsonSerializerOptions JsonSerializeSettings { get; } = Initialize.JsonSerializeSettings();
}