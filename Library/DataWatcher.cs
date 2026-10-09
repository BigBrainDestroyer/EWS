using System.Diagnostics;

namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal static class DataWatcher
{
    public static void Start()
    {
        Process projectProcess = Process.GetProcessById(Environment.ProcessId);

        while (!App.commonData.AllMazesSolved)
        {
            if (!App.commonData.ProjectStopWatch.IsRunning) App.commonData.ProjectStopWatch.Start();

            double firstTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;
            double firstCpuUsage = projectProcess.TotalProcessorTime.TotalSeconds;

            Thread.Sleep(100);

            projectProcess.Refresh();
            double secondTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;

            double totalTime = firstTime - secondTime;
            double cpuUsage = projectProcess.TotalProcessorTime.TotalSeconds - firstCpuUsage;

            double usagePercentageCpu = Math.Abs(cpuUsage / (Environment.ProcessorCount * totalTime)) * 100;

            if (App.directoryInfo.OsSystem == OsSystem.Windows)
            {
                App.commonData.CommonUsageDataHistory.Add(
                    new CommonUsageDataHistory(
                        new DVector2(firstTime, secondTime),
                        usagePercentageCpu,
                        projectProcess.WorkingSet64 / 1024.0 / 1024.0,
                        -1 //gpu niet goed goed mogelijk   
                ));
            }
            else
            {
                App.commonData.CommonUsageDataHistory.Add(
                    new CommonUsageDataHistory(
                        new DVector2(firstTime, secondTime),
                        usagePercentageCpu,
                        -1,
                        -1 //gpu niet goed goed mogelijk   
                ));
            }
        }

        App.commonData.ProjectStopWatch.Stop();
    }
}