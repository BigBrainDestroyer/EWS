
namespace Program;

public class PlayMaze
{
    public int MazeListPosition { get; set; } = 0;

    public int SlotHistoryListPosition { get; set; } = 0;

    public volatile bool IsAlgoProgramPaused = true;

    public volatile bool IsAlgoRunning = false;
    
    public volatile bool IsAlgoLoaded = false;
}