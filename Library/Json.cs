using System.Text.Json;

namespace ShareData;

internal static class Json
{
    public static JsonSerializerOptions JsonSerializeSettings()
    {
        return App.json.JsonSerializeSettings;
    }
}