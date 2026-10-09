
namespace Program;

public class PlayMaze
{
    public int MazeListPosition { get; set; } = 0;

    public int SlotHistoryListPosition { get; set; } = 0;

    public bool IsAlgoProgramPauzed { get; set; } = false;

    public bool IsAlgoRunning { get; set; } = false;
    
    public bool IsAlgoLoaded { get; set; } = false;
}