using System.Diagnostics;

namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal static class DataWatcher
{
    public static void Start()
    {
        Process projectProcess = Process.GetProcessById(Environment.ProcessId);

        if (!App.commonData.ProjectStopWatch.IsRunning) App.commonData.ProjectStopWatch.Start();
        
        while (!App.commonData.AllMazesSolved)
        {
            double firstTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;
            double firstCpuUsage = projectProcess.TotalProcessorTime.TotalSeconds;

            Thread.Sleep(1);

            projectProcess.Refresh();
            double secondTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;

            double totalTime = secondTime - firstTime;
            double cpuUsage = projectProcess.TotalProcessorTime.TotalSeconds - firstCpuUsage;

            double usagePercentageCpu = cpuUsage / (Environment.ProcessorCount * totalTime) * 100;

            App.commonData.CommonUsageDataHistory.Add(
                new CommonUsageDataHistory(
                    new DVector2(firstTime, secondTime),
                    usagePercentageCpu,
                    GC.GetTotalMemory(true) / 1024.0 / 1024.0,
                    -1 //gpu niet goed goed mogelijk   
            ));            
        }

        App.commonData.ProjectStopWatch.Stop();
    }
}