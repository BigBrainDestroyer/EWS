using System.Text.Json;

namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal class JsonState
{
    public JsonSerializerOptions JsonSerializeSettings { get; } = Initialize.JsonSerializeSettings();
}