
namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal static class App
{
    public static DirectoryInfoState directoryInfo { get; set; } = new DirectoryInfoState();
    public static JsonState json { get; set; } = new JsonState();
    public static CommonDataState commonData { get; set; } = new CommonDataState();
    public static RunningMazeState runningMaze { get; set; } = new RunningMazeState();
    public static ShareMazeData shareMazeData { get; set; } = new ShareMazeData();
}