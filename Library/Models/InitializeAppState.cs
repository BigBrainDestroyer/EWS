
namespace ShareData;

internal static class State
{
    public static OsSystem OsSystem { get; } = Initialize.OsPlatform();
    public static string PathSyntax { get; } = Initialize.PathSyntax();

    public static string ShareDataFolderName { get; } = "ShareFolder";
    public static string ShareMazeDataFileName { get; } = "ShareMazeData.json";
    public static string ShareAlgoDataFileName { get; } = "ShareAlgoData.json";
}