
namespace Program;

public class PlayMaze
{   
    public int MazeListPosition { get; set; } = 0;

    public int SlotHistoryListPosition { get; set; } = 0;

    public volatile bool IsAlgoProgramPaused = true;

    public volatile bool IsAlgoRunning = false;

    public volatile bool IsAlgoRunningDone = false;

    public volatile bool IsAlgoLoaded = false;

    public volatile bool UpdatedNewMazeToShareAlgoFile = false;
    
    public volatile bool UpdatedNewMazeToShareMazeFile = false;
}