using System.Diagnostics;

namespace ShareData;

public static class CommonData
{
    public static void CommonDataWatcher()
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

        ShareObj. = _state.maze.generator.AllMazes;

        File.WriteAllText(
            ShareData.DirectoryInfo.ShareMazeDataFilePath(),
            JsonSerializer.Serialize(ShareObj, ShareData.Json.JsonSerializeSettings())
        );
    }
}