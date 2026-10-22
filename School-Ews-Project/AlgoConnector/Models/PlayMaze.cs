
namespace Program;

public class PlayMaze
{   
    public double PlaySpeed { get; set; } = 1;
    
    public int MazeListPosition { get; set; } = 0;

    public int SlotHistoryListPosition { get; set; } = 0;

    public int CommonUsageDataListPosition { get; set; } = 0;

    public double FinalMazeQueSpeed { get; } = 0.250;

    public double FinalPathHolePathShowTime { get; } = 1.5;

    public volatile bool IsAlgoProgramPaused = true;

    public volatile bool IsAlgoRunning = false;

    public volatile bool IsAlgoRunningDone = false;

    public volatile bool IsAlgoLoaded = false;

    public volatile bool IsMazeLoaded = false;

    public volatile bool UpdatedNewMazeToShareAlgoFile = false;
    
    public volatile bool UpdatedNewMazeToShareMazeFile = false;

    public volatile bool ShowCurrentMazeFinalPath = false;

    public volatile bool CurrentMazeFinalPathDone = false;
}