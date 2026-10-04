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
        RunAlgo();
    }

    private void RunAlgo()
    {
        var _connect = _state.connector;
        
        if(Raylib.IsKeyPressed(KeyboardKey.RightAlt))//if(_connect.RunAlgo)
        {
            var ShareObj = new ShareMazeData();

            ShareObj.AllMazes = new ShareData.Maze[1];

            var JsonSettings = new JsonSerializerOptions();
            
            JsonSettings.Converters.Add(new JsonStringEnumConverter());

            JsonSettings.WriteIndented = true;
            JsonSettings.PropertyNameCaseInsensitive = true;

            File.WriteAllText(
                ShareData.DirectoryInfo.ShareMazeDataFilePath(),
                JsonSerializer.Serialize(ShareObj, JsonSettings)
            );
        }
    }
}