using ShareData;
using System.Text.Json;

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

        UpdateShareFiles();
        
        UpdateShareAlgoData();
    }

    private void UpdateShareFiles()
    {
        var _mazeGenerator = _state.maze.generator;
        var _play = _state.playMaze;
        
        if (!_mazeGenerator.MazesAreDone)
        {
            return;
        }

        UpdateNewMazeToShareAlgoFile();
        
        UpdateNewMazeToShareMazeFile();
        
        //reset params
        if(_play.UpdatedNewMazeToShareAlgoFile && _play.UpdatedNewMazeToShareMazeFile)
        {
            _play.IsAlgoLoaded = false;
            _play.IsMazeLoaded = true;

            _mazeGenerator.MazesAreDone = false;
            _mazeGenerator.BuildingMazes = false;
            
            _play.UpdatedNewMazeToShareAlgoFile = false;
            _play.UpdatedNewMazeToShareMazeFile = false;

            ResetAlgoRunParams();

            Debug.WriteLine("Updating ShareFiles Is SuccessFull");
        }
    }

    private void ResetAlgoRunParams()
    {
        var _play = _state.playMaze;

        _play.MazeListPosition = 0;
        _play.SlotHistoryListPosition = 0;
    }

    private void UpdateNewMazeToShareAlgoFile()
    {
        var _mazeGenerator = _state.maze.generator;
        var _shareAlgoData = _state.shareAlgoData;
        var _play = _state.playMaze;

        if (_play.UpdatedNewMazeToShareAlgoFile)
        {
            return;
        }

        Debug.WriteLine("Resetting ShareAlgoFile");
        
        var shareAlgoObj = new ShareAlgoData();

        shareAlgoObj.CommonDataHistory = new CommonDataHistory();
        shareAlgoObj.AllMazes = _mazeGenerator.AllMazes;
        shareAlgoObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
        shareAlgoObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

        try
        {
            File.WriteAllText(
                ShareData.App.directoryInfo.ShareAlgoDataFilePath,
                JsonSerializer.Serialize(shareAlgoObj, ShareData.Json.JsonSerializeSettings())
            );   
        }
        catch
        {
            Debug.WriteLine("ShareAlgoFile Reset Is UnSuccessFull");
            return;
        }
        
        _shareAlgoData.CommonDataHistory = new CommonDataHistory();
        _shareAlgoData.AllMazes = shareAlgoObj.AllMazes;
        _shareAlgoData.MazeBeginPoints = shareAlgoObj.MazeBeginPoints;
        _shareAlgoData.MazeEndPoints = shareAlgoObj.MazeEndPoints;

        _play.UpdatedNewMazeToShareAlgoFile = true;
        
        Debug.WriteLine("ShareAlgoFile Reset Is SuccessFull");
    }

    private void UpdateNewMazeToShareMazeFile()
    {
        var _mazeGenerator = _state.maze.generator;
        var _shareAlgoData = _state.shareAlgoData;
        var _play = _state.playMaze;

        if (_play.UpdatedNewMazeToShareMazeFile)
        {
            return;
        }

        Debug.WriteLine("Updating New Maze To ShareMazeFile");
        
        var shareMazeObj = new ShareMazeData();

        shareMazeObj.AllMazes = _mazeGenerator.AllMazes;
        shareMazeObj.MazeBeginPoints = _mazeGenerator.MazeBeginPoints;
        shareMazeObj.MazeEndPoints = _mazeGenerator.MazeEndPoints;

        try
        {
            File.WriteAllText(
                ShareData.App.directoryInfo.ShareMazeDataFilePath,
                JsonSerializer.Serialize(shareMazeObj, ShareData.Json.JsonSerializeSettings())
            );   
        }
        catch
        {
            Debug.WriteLine("Update New Maze To ShareMazeFile Is UnSuccessFull");
            return;
        }
        
        _play.UpdatedNewMazeToShareMazeFile = true;
        
        Debug.WriteLine("Update New Maze To ShareMazeFile Is SuccessFull");
    }

    private void UpdateShareAlgoData()
    {
        var _play = _state.playMaze;

        if(!_play.IsAlgoRunningDone)
        {
            return;
        }

        Debug.WriteLine("Loading ShareAlgoData");

        var _shareAlgoData = _state.shareAlgoData;

        string jsonText = "";
        
        try
        {            
            jsonText = File.ReadAllText(ShareData.App.directoryInfo.ShareAlgoDataFilePath);
            
            Debug.WriteLine("Reading ShareAlgoData File Is SuccessFull");
        }   
        catch
        {
            Debug.WriteLine("Reading ShareAlgoData File Is UnSuccessFull");
            
            return;
        }

        if(string.IsNullOrWhiteSpace(jsonText))
        {
            Debug.WriteLine("AlgoShareData File Is Corrupt");

            UpdateAlgoRunningState();
            
            _play.IsAlgoLoaded = false;

            return;
        }

        try
        {
            if(JsonSerializer.Deserialize<ShareData.ShareAlgoData>(jsonText, ShareData.Json.JsonSerializeSettings()) == null)
            {   
                _shareAlgoData = new ShareAlgoData();
            }
            else
            {
                var shareObj = new ShareAlgoData();

                shareObj = JsonSerializer.Deserialize<ShareData.ShareAlgoData>(jsonText, ShareData.Json.JsonSerializeSettings());
                
                _shareAlgoData.CommonDataHistory = shareObj.CommonDataHistory;
                _shareAlgoData.AllMazes = shareObj.AllMazes;
                _shareAlgoData.MazeBeginPoints = shareObj.MazeBeginPoints;
                _shareAlgoData.MazeEndPoints = shareObj.MazeEndPoints;
            }
        }
        catch
        {
            Debug.WriteLine("AlgoShareData File Is Corrupt");

            UpdateAlgoRunningState();
            
            _play.IsAlgoLoaded = false;

            return;
        }

        (_play.IsAlgoLoaded, _play.IsMazeLoaded) = IsShareAlgoDataLegal.Check(_shareAlgoData);

        Debug.WriteLine($"IsAlgoLoaded: {_play.IsAlgoLoaded}");

        UpdateAlgoRunningState();
    }

    private void UpdateAlgoRunningState()
    {
        var _play = _state.playMaze;

        ResetAlgoRunParams();
        
        _play.IsAlgoRunningDone = false;
        _play.IsAlgoRunning = false;
    }
}
