using System.Text.Json;

namespace ShareData;

public static class Json
{
    public static JsonSerializerOptions JsonSerializeSettings()
    {
        return App.json.JsonSerializeSettings;
    }
}