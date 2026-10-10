
namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal class DirectoryInfoState
{
    public OsSystem OsSystem { get; } = Initialize.OsPlatform();

    public string PathSyntax { get; } = Initialize.PathSyntax();

    public string DllPath => Initialize.DllPath();

    public string ProgramStorageFolderPath => Initialize.ProgramStorageFolderPath();

    public string SharedDataFolderPath => Initialize.SharedDataFolderPath();

    public string ShareMazeDataFilePath => Initialize.ShareMazeDataFilePath();

    public string ShareAlgoDataFilePath => Initialize.ShareAlgoDataFilePath();
}