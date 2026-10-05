using System.Diagnostics;

namespace ShareData;

internal class CommonDataState
{
    public volatile bool AlgoDone = false;

    public volatile Stopwatch ProjectStopWatch = new Stopwatch();

    public volatile CommonDataHistory CommonDataHistory = new CommonDataHistory();

    public volatile List<SlotDataVariables> SlotSearchedHistory = new List<SlotDataVariables>();
}
