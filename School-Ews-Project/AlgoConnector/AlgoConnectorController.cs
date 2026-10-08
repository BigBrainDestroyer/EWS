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
                ShareData.DirectoryInfo.ShareAlgoDataFilePath(),
                JsonSerializer.Serialize(shareObj, ShareData.Json.JsonSerializeSettings())
            );

            _shareAlgoData.CommonDataHistory = new CommonDataHistory();
            _shareAlgoData.AllMazes = shareObj.AllMazes;
            _shareAlgoData.MazeBeginPoints = shareObj.MazeBeginPoints;
            _shareAlgoData.MazeEndPoints = shareObj.MazeEndPoints;

            _mazeGenerator.BuildingMazes = false;
            _mazeGenerator.MazesAreDone = false;
        }
    }
    
    private void RunAlgo()
    {
        var _mazeGenerator = _state.maze.generator;
        
        if(Raylib.IsKeyPressed(KeyboardKey.RightAlt))//if(_connect.RunAlgo)
        {
            var shareObj = new ShareMazeData();

            shareObj.AllMazes = _mazeGenerator.AllMazes;
            shareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
            shareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

            File.WriteAllText(
                ShareData.DirectoryInfo.ShareMazeDataFilePath(),
                JsonSerializer.Serialize(shareObj, ShareData.Json.JsonSerializeSettings())
            );
        }
    }

    
}