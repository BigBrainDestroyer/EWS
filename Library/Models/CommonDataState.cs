using System.Diagnostics;

namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal class CommonDataState
{
    public volatile bool AllMazesSolved = false;

    public volatile Stopwatch ProjectStopWatch = new Stopwatch();

    public volatile List<CommonUsageDataHistory> CommonUsageDataHistory = new List<CommonUsageDataHistory>();

    public volatile List<SlotDataVariables> SlotSearchedHistory = new List<SlotDataVariables>();

    public volatile List<MazeFinalPath> MazeFinalPaths = new List<MazeFinalPath>();
}
