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
        var _mazeGenerator = _state.maze.generator;
        var _play = _state.playMaze;

        if (_mazeGenerator.MazesAreDone && !_play.WritingMazeDataToFile)
        {
            _play.WritingMazeDataToFile = true;

            UpdateNewMazeToShareAlgoFile();
        }

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
        }

    }

    private async Task UpdateNewMazeToShareAlgoFile()
    {
        var _mazeGenerator = _state.maze.generator;
        var _shareAlgoData = _state.shareAlgoData;
        var _play = _state.playMaze;

        var shareObj = new ShareAlgoData();

        shareObj.CommonDataHistory = new CommonDataHistory();
        shareObj.AllMazes = _mazeGenerator.AllMazes;
        shareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
        shareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

        bool fileHasBeenWritten = false;

        while(!fileHasBeenWritten)
        {
            try
            {
                File.WriteAllText(
                    ShareData.App.directoryInfo.ShareAlgoDataFilePath,
                    JsonSerializer.Serialize(shareObj, ShareData.Json.JsonSerializeSettings())
                );

                fileHasBeenWritten = true;
            }
            catch
            {
                await Task.Delay(200);
            }
        }

        _shareAlgoData.CommonDataHistory = new CommonDataHistory();
        _shareAlgoData.AllMazes = shareObj.AllMazes;
        _shareAlgoData.MazeBeginPoints = shareObj.MazeBeginPoints;
        _shareAlgoData.MazeEndPoints = shareObj.MazeEndPoints;

        
        _mazeGenerator.BuildingMazes = false;
        _mazeGenerator.MazesAreDone = false;
        _play.WritingMazeDataToFile = false;

        Console.WriteLine("Done");
    }

    private void RunAlgo()
    {
        var _play = _state.playMaze;
        var _mazeGenerator = _state.maze.generator;

        if(_play.IsAlgoRunningDone)
        {
            var shareObj = new ShareMazeData();

            shareObj.AllMazes = _mazeGenerator.AllMazes;
            shareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
            shareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

            File.WriteAllText(
                ShareData.App.directoryInfo.ShareMazeDataFilePath,
                JsonSerializer.Serialize(shareObj, ShareData.Json.JsonSerializeSettings())
            );

            //Hier checks over of het wel legale data is

            Console.WriteLine("Done Running");

            _play.IsAlgoRunningDone = false;
            _play.IsAlgoRunning = false;
        }
    }
}
