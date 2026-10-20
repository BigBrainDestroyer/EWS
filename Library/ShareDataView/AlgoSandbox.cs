using System.Text.Json;
using System.Diagnostics;

namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

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
        var shareObj = new ShareMazeData();
        bool stop = false;
        string jsonText = "";

        while (stop == false)
        {
            try
            {
                jsonText = File.ReadAllText(App.directoryInfo.ShareMazeDataFilePath);

                if(!string.IsNullOrWhiteSpace(jsonText))
                {
                    shareObj = JsonSerializer.Deserialize<ShareMazeData>(jsonText, App.json.JsonSerializeSettings) ?? throw new ArgumentNullException(nameof(App.shareMazeData));

                    if(shareObj != null)
                    {
                        stop = true;
                    }
                    else
                    {
                        EndProgram();
                    }
                }
                else
                { 
                    EndProgram();
                }
            }
            catch
            {
                Thread.Sleep(100);
            }
        }

        if(shareObj.AllMazes.Count() > 0)
        {
            App.shareMazeData.AllMazes = shareObj.AllMazes;
            App.shareMazeData.MazeBeginPoints = shareObj.MazeBeginPoints;
            App.shareMazeData.MazeEndPoints = shareObj.MazeEndPoints;

            App.commonData.SlotSearchedHistory.Add(
                new SlotDataVariables(
                    new SlotSearchedWithTime(
                        0,
                        App.shareMazeData.MazeBeginPoints[0]
                    ),
                    false,
                    0
                )
            );
        }
        else
        {       
            EndProgram();
        }
    }

    public static void EndProgram()
    {
        lock(_lock)
        {
            WriteAlgoShareData();

            StackTrace stacks = new StackTrace();

            foreach (StackFrame stack in stacks.GetFrames())
            {
                var methode = stack.GetMethod();

                Console.WriteLine(methode);
            }

            Environment.Exit(0);
        }
    }

    private static void WriteAlgoShareData()
    {
        var ShareObj = new ShareAlgoData();

        ShareObj.AllMazes = App.shareMazeData.AllMazes;
        ShareObj.MazeBeginPoints = App.shareMazeData.MazeBeginPoints;
        ShareObj.MazeEndPoints = App.shareMazeData.MazeEndPoints;
        ShareObj.CommonDataHistory.CommonUsageDataHistory = App.commonData.CommonUsageDataHistory;
        ShareObj.CommonDataHistory.SlotSearchedHistory = App.commonData.SlotSearchedHistory;
        ShareObj.CommonDataHistory.ProjectTotalSeconds = App.commonData.ProjectStopWatch.Elapsed.TotalSeconds;
        ShareObj.CommonDataHistory.MazeFinalPaths = App.commonData.MazeFinalPaths;

        bool stop = true;

        while(stop)
        {
            try
            {
                File.WriteAllText(
                    App.directoryInfo.ShareAlgoDataFilePath,
                    JsonSerializer.Serialize(ShareObj, ShareData.Json.JsonSerializeSettings())
                );

                stop = false;
            }
            catch
            {
                Thread.Sleep(500);
            }
        }
            
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
        else if(App.runningMaze.CurrentMazeNumber + 1 < App.shareMazeData.AllMazes.Count())
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