
namespace ShareData;

public static class DirectoryInfo
{
    public static string ShareDataFolderPath()
    {
        return App.directoryInfo.SharedDataFolderPath;
    }

    public static string ShareMazeDataFilePath()
    {
        return App.directoryInfo.ShareMazeDataFilePath;
    }

    public static string ShareAlgoDataFilePath()
    {
        return App.directoryInfo.ShareAlgoDataFilePath;
    }
}