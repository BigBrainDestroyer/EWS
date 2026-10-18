using ShareData;
using Raylib_cs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Program;

public class AlgoConnectorController
{
    private readonly AppState _state;

    public AlgoConnectorController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Controller()
    {
        UpdateNewMazeToShareAlgoFile();

        RunAlgo();

        if(Raylib.IsKeyPressed(KeyboardKey.RightAlt))
        {
            _state.playMaze.IsAlgoProgramPaused = !_state.playMaze.IsAlgoProgramPaused;
            
            _state.common.time.MazeRunStopwatch = 
                new MazeRunStopwatch(
                    _state.common.time.MazeRunStopwatch.StartTime,
                    _state.common.time.MazeRunStopwatch.TotalTime,
                    _state.common.time.MazeRunStopwatch.MazeStopwatch,
                    1,
                    true,
                    _state.common.time.MazeRunStopwatch.ResetStopwatch
                );

            Console.WriteLine("kasjdf;lja s;fjas;dlfja;lsdjfa;l dsjf;lasjdf;lk asjfklasjf;klasj dflkjasdf jasfjasldkfjasjff");
        }
        if(Raylib.IsKeyPressed(KeyboardKey.LeftAlt))
        {
            //_generate.Generator();
        }

    }

    private void UpdateNewMazeToShareAlgoFile()
    {
        var _mazeGenerator = _state.maze.generator;
        var _shareAlgoData = _state.shareAlgoData;

        if(_mazeGenerator.MazesAreDone)
        {
            var shareObj = new ShareAlgoData();

            shareObj.CommonDataHistory = new CommonDataHistory();
            shareObj.AllMazes = _mazeGenerator.AllMazes;
            shareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
            shareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

            File.WriteAllText(
                ShareData.App.directoryInfo.ShareAlgoDataFilePath,
                JsonSerializer.Serialize(shareObj, ShareData.Json.JsonSerializeSettings())
            );

            _shareAlgoData.CommonDataHistory = new CommonDataHistory();
            _shareAlgoData.AllMazes = shareObj.AllMazes;
            _shareAlgoData.MazeBeginPoints = shareObj.MazeBeginPoints;
            _shareAlgoData.MazeEndPoints = shareObj.MazeEndPoints;

            _mazeGenerator.BuildingMazes = false;
            _mazeGenerator.MazesAreDone = false;

            Console.WriteLine("Done");
        }
    }
    
    private void RunAlgo()
    {
        var _play = _state.playMaze;
        var _mazeGenerator = _state.maze.generator;
        
        if(_mazeGenerator.MazesAreDone)//net voor runnign algo
        {
            var shareObj = new ShareMazeData();

            shareObj.AllMazes = _mazeGenerator.AllMazes;
            shareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
            shareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

            File.WriteAllText(
                ShareData.App.directoryInfo.ShareMazeDataFilePath,
                JsonSerializer.Serialize(shareObj, ShareData.Json.JsonSerializeSettings())
            );

            Console.WriteLine("Done Running");
        }
    }
}