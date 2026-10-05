using System.Text.Json;

namespace ShareData;

public static class AlgoSandbox
{
    private static readonly object _lock = new object();

    public static void Start()
    {
        DeserializeSharedMazeData();

        Task.Run(() =>
        {
            DataWatcher.Start();

            EndProgram();
        });

        while (!App.commonData.ProjectStopWatch.IsRunning)
        {
            Thread.Sleep(5);
        }

        UpdateRunningMazeVariables();
    }

    private static void DeserializeSharedMazeData()
    {
        string jsonText = File.ReadAllText(App.directoryInfo.ShareMazeDataFilePath);

        if(JsonSerializer.Deserialize<ShareMazeData>(jsonText, App.json.JsonSerializeSettings) == null)
        {
            EndProgram();
        }
        else
        {
            App.shareMazeData = JsonSerializer.Deserialize<ShareMazeData>(jsonText, App.json.JsonSerializeSettings) ?? throw new ArgumentNullException(nameof(App.shareMazeData));
        }            
    }
    public static void EndProgram()
    {
        lock(_lock)
        {
            WriteAlgoShareData();

            Environment.Exit(0);
        }
    }

    private static void WriteAlgoShareData()
    {
        var ShareObj = new ShareAlgoData();

        ShareObj.CommonDataHistory.CommonUsageDataHistory = App.commonData.CommonUsageDataHistory;
        ShareObj.CommonDataHistory.SlotSearchedHistory = App.commonData.SlotSearchedHistory;
        ShareObj.CommonDataHistory.TotalProjectStopWatchTotalTime = App.commonData.ProjectStopWatch;
        ShareObj.CommonDataHistory.MazeFinalPaths = App.commonData.MazeFinalPaths;


        File.WriteAllText(
            ShareData.DirectoryInfo.ShareAlgoDataFilePath(),
            JsonSerializer.Serialize(ShareObj, ShareData.Json.JsonSerializeSettings())
        );
    }

    internal static void UpdateRunningMazeVariables()
    { 
        if(App.commonData.MazeFinalPaths.Count() == 0)
        {
            App.runningMaze.CurrentMazeNumber = 0;
            App.runningMaze.CurrentMaze = App.shareMazeData.AllMazes[App.runningMaze.CurrentMazeNumber];
            App.runningMaze.CurrentNavigatorCoordinates = App.shareMazeData.MazeBeginPoints[App.runningMaze.CurrentMazeNumber];
            App.runningMaze.CurrentMazeEndPoint = App.shareMazeData.MazeEndPoints[App.runningMaze.CurrentMazeNumber];
        }

        if(App.runningMaze.CurrentMazeNumber + 1 < App.shareMazeData.AllMazes.Count())
        {
            App.runningMaze.CurrentMazeNumber += 1;
            App.runningMaze.CurrentMaze = App.shareMazeData.AllMazes[App.runningMaze.CurrentMazeNumber];
            App.runningMaze.CurrentNavigatorCoordinates = App.shareMazeData.MazeBeginPoints[App.runningMaze.CurrentMazeNumber];
            App.runningMaze.CurrentMazeEndPoint = App.shareMazeData.MazeEndPoints[App.runningMaze.CurrentMazeNumber];
        }
        else
        {
            App.commonData.AllMazesSolved = true;
        }
    }
}