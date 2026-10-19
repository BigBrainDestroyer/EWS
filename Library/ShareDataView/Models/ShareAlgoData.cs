using System.Diagnostics;

namespace ShareData;

//ShareAlgoData Word aan het einde gevuld en direct geschreven naar file

public class ShareAlgoData
{
    public volatile CommonDataHistory CommonDataHistory = new CommonDataHistory();

    public volatile Maze[] AllMazes = new Maze[0];

    public volatile IVector2[] MazeEndPoints = new IVector2[0];

    public volatile IVector2[] MazeBeginPoints = new IVector2[0];
}

public class CommonDataHistory
{
    public Double ProjectTotalSeconds = 0;
    public List<CommonUsageDataHistory> CommonUsageDataHistory  { get; set; } = new List<CommonUsageDataHistory>();
    public List<SlotDataVariables> SlotSearchedHistory { get; set; } = new List<SlotDataVariables>();
    public List<MazeFinalPath> MazeFinalPaths { get; set; } = new List<MazeFinalPath>();
}