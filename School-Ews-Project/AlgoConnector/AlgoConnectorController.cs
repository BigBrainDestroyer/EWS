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
            var ShareObj = new ShareAlgoData();

            ShareObj.CommonDataHistory = new CommonDataHistory();
            ShareObj.AllMazes = _mazeGenerator.AllMazes;
            ShareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
            ShareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

            File.WriteAllText(
                ShareData.DirectoryInfo.ShareAlgoDataFilePath(),
                JsonSerializer.Serialize(ShareObj, ShareData.Json.JsonSerializeSettings())
            );

            _shareAlgoData = ShareObj;

            _mazeGenerator.MazesAreDone = false;
        }
    }
    
    private void RunAlgo()
    {
        var _connect = _state.connector;
        var _mazeGenerator = _state.maze.generator;
        
        if(Raylib.IsKeyPressed(KeyboardKey.RightAlt))//if(_connect.RunAlgo)
        {
            var ShareObj = new ShareMazeData();

            ShareObj.AllMazes = _mazeGenerator.AllMazes;
            ShareObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
            ShareObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

            File.WriteAllText(
                ShareData.DirectoryInfo.ShareMazeDataFilePath(),
                JsonSerializer.Serialize(ShareObj, ShareData.Json.JsonSerializeSettings())
            );
        }
    }

    
}