using System.Diagnostics;

namespace ShareData;

public class ShareAlgoData
{
    public CommonDataHistory CommonDataHistory = new CommonDataHistory();
}

public class CommonDataHistory
{
    public Stopwatch TotalProjectStopWatchTotalTime { get; set; } = new Stopwatch();
    public List<CommonUsageDataHistory> CommonUsageDataHistory  { get; set; } = new List<CommonUsageDataHistory>();
    public List<SlotDataVariables> SlotSearchedHistory { get; set; } = new List<SlotDataVariables>();
    public List<MazeFinalPath> MazeFinalPaths { get; set; } = new List<MazeFinalPath>();//DEZE NOG
}