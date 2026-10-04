
namespace ShareData;

internal static class State
{
    public static OsSystem osSystem { get; } = Initialize.OsPlatform();
    public static string pathSyntax { get; } = Initialize.PathSyntax();

    public static string ShareDataFolderName { get; } = "ShareFolder";
    public static string ShareMazeDataFileName { get; } = "ShareMazeData.txt";
}