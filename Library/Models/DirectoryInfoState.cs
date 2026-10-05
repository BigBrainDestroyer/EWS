
namespace ShareData;

internal class DirectoryInfoState
{
    public OsSystem OsSystem { get; } = Initialize.OsPlatform();

    public string PathSyntax { get; } = Initialize.PathSyntax();

    public string SharedDataFolderPath { get; } = Initialize.SharedDataFolderPath();

    public string ShareMazeDataFilePath { get; } = Initialize.ShareMazeDataFilePath();

    public string ShareAlgoDataFilePath { get; } = Initialize.ShareAlgoDataFilePath();
}