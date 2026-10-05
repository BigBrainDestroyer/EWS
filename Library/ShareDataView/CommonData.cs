using System.Diagnostics;
using System.Text.Json;

namespace ShareData;

public static class CommonData
{
    public static void CommonDataWatcher()
    {
        Task.Run(() =>
        {
            Process projectProcess = Process.GetProcessById(Environment.ProcessId);

            while (!App.commonData.AlgoDone)
            {
                if (!App.commonData.ProjectStopWatch.IsRunning) App.commonData.ProjectStopWatch.Start();

                double firstTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;
                double firstCpuUsage = projectProcess.TotalProcessorTime.TotalSeconds;

                Thread.Sleep(100);

                projectProcess.Refresh();
                double secondTime = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;

                double totalTime = firstTime - secondTime;
                double cpuUsage = projectProcess.TotalProcessorTime.TotalSeconds - firstCpuUsage;

                double usagePercentageCpu = (cpuUsage / (Environment.ProcessorCount * totalTime)) * 100;

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

            //eindig laatste van progama
            WriteAlgoShareData();

            Environment.Exit(0);
        });

        while(!App.commonData.ProjectStopWatch.IsRunning)
        {
            Thread.Sleep(5);
        }
    }

    private static void WriteAlgoShareData()
    {
        var ShareObj = new ShareAlgoData();

        ShareObj.CommonDataHistory.CommonUsageDataHistory = App.commonData.CommonUsageDataHistory;
        ShareObj.CommonDataHistory.SlotSearchedHistory = App.commonData.SlotSearchedHistory;
        ShareObj.CommonDataHistory.TotalProjectStopWatchTotalTime = App.commonData.ProjectStopWatch;

        //MOET NOG MAZEFINAL PATH DOEN MAAR KAN OOK LATER
        
        
        File.WriteAllText(
            ShareData.DirectoryInfo.ShareAlgoDataFilePath(),
            JsonSerializer.Serialize(ShareObj, ShareData.Json.JsonSerializeSettings())
        );
    }
}