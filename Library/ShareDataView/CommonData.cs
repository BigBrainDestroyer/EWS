using System.Diagnostics;

namespace ShareData;

public static class CommonData
{
    public static void StartCommonDataWatcher()
    {
        Task.Run(() =>
        {
            Process projectProcess = Process.GetProcessById(Environment.ProcessId);

            while(!App.commonData.AlgoDone)
            {
                if(!App.commonData.ProjectStopWatch.IsRunning) App.commonData.ProjectStopWatch.Start();

                double firstTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;
                double firstCpuUsage = projectProcess.TotalProcessorTime.TotalSeconds;

                Thread.Sleep(100);

                projectProcess.Refresh();
                double secondTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;
                
                double totalTime = firstTime - secondTime;
                double cpuUsage = projectProcess.TotalProcessorTime.TotalSeconds - firstCpuUsage;

                double cpuPercentageUsage = (cpuUsage / (Environment.ProcessorCount * totalTime)) * 100;

                App.commonData.CommonDataHistory.StopwatchTime.Add(new DVector2(firstTime, secondTime));
                App.commonData.CommonDataHistory.UsagePercentageCpu.Add(cpuPercentageUsage);
                
                if(App.directoryInfo.OsSystem == OsSystem.Windows)
                {
                    App.commonData.CommonDataHistory.UsagePercentageRamMb.Add(projectProcess.WorkingSet64 / 1024.0 / 1024.0);
                }
                
                Console.WriteLine(cpuPercentageUsage);
            }

            App.commonData.ProjectStopWatch.Stop();

            //eindig laatste van progama
            
            Environment.Exit(0);
        });

        while(!App.commonData.ProjectStopWatch.IsRunning)
        {
            Thread.Sleep(50);
        }
    }

    public static void AddNewSearchedSlot(IVector2 newDiscovertSlotCoordinates)
    {
        App.commonData.SlotSearchedHistoryWithTime.Add(
            new SlotSearchedWithTime(
                App.commonData.ProjectStopWatch.Elapsed.TotalSeconds,
                newDiscovertSlotCoordinates
            )
        );
    }

    public static void AllSlotSearchedCoordinates(IVector2 requestedSlot)
    {
        if(App.commonData.AllSlotSearchedCoordinates.Contains(requestedSlot))
        {
            return;
        }
        else
        {
            App.commonData.AllSlotSearchedCoordinates.Add(requestedSlot);
        }
    }
}