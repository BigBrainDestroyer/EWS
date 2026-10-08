using System.Diagnostics;

namespace ShareData;

//ShareAlgoData Word aan het einde gevuld en direct geschreven naar file

public class ShareAlgoData
{
    public CommonDataHistory CommonDataHistory { get; set; } = new CommonDataHistory();

    public Maze[] AllMazes { get; set; } = new Maze[0];

    public IVector2[] MazeEndPoints { get; set; } = new IVector2[0];

    public IVector2[] MazeBeginPoints { get; set; } = new IVector2[0];
}

public class CommonDataHistory
{
    public Stopwatch TotalProjectStopWatchTotalTime { get; set; } = new Stopwatch();
    public List<CommonUsageDataHistory> CommonUsageDataHistory  { get; set; } = new List<CommonUsageDataHistory>();
    public List<SlotDataVariables> SlotSearchedHistory { get; set; } = new List<SlotDataVariables>();
    public List<MazeFinalPath> MazeFinalPaths { get; set; } = new List<MazeFinalPath>();
}