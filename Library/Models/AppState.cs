
namespace ShareData;

internal static class App
{
    public static DirectoryInfoState directoryInfo { get; set; } = new DirectoryInfoState();
    public static JsonState json { get; set; } = new JsonState();
    public static CommonDataState commonData { get; set; } = new CommonDataState();
}