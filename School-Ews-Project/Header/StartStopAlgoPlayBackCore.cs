
namespace Program;

public class StartStopAlgoPlayBackButtonCore
{
    private readonly AppState _state;

    public StartStopAlgoPlayBackButtonCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void UpdateButtonAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        if(_play.IsAlgoLoaded && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.StartStopAlgoPlayBack] = true;
        }
        else
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.StartStopAlgoPlayBack] = false;
        }
        
        if(!_play.IsAlgoProgramPaused && _header.HeaderObjectAvailable[DrawHeaderObjectNames.StartStopAlgoPlayBack])
        {
            _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].BoxBackgroundColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.StartStopAlgoPlayBack].BoxColors.Y;

            _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].TextureColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.StartStopAlgoPlayBack].SymbolColors.Y;
        }
        else if(_header.HeaderObjectAvailable[DrawHeaderObjectNames.StartStopAlgoPlayBack])
        {
            _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].BoxBackgroundColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.StartStopAlgoPlayBack].BoxColors.X;

            _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].TextureColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.StartStopAlgoPlayBack].SymbolColors.X;            
        }
    }

    public void ButtonClick()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;
        var _time = _state.common.time;

        if(_play.IsAlgoLoaded && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _play.IsAlgoProgramPaused = !_play.IsAlgoProgramPaused;

            double currentPausedTime = 0;
            double currentLastPausedTime = 0;

            if(_play.IsAlgoProgramPaused)
            {
                currentLastPausedTime = _time.Stopwatch.Elapsed.TotalSeconds;
            }
            else if(_time.MazeRunStopwatch.LastTimePaused != 0)
            {
                currentPausedTime = _time.Stopwatch.Elapsed.TotalSeconds - _time.MazeRunStopwatch.LastTimePaused;
            }
            
            _time.MazeRunStopwatch =
                new MazeRunStopwatch(
                    _time.MazeRunStopwatch.StartTime,
                    _state.common.time.MazeRunStopwatch.TotalTime,
                    _time.MazeRunStopwatch.TotalPausedTime + currentPausedTime,
                    currentLastPausedTime,
                    _time.MazeRunStopwatch.MazeStopwatch,
                    _play.PlaySpeed,
                    !_play.IsAlgoProgramPaused,
                    _time.MazeRunStopwatch.ResetStopwatch
                );
            
            if(_play.IsAlgoProgramPaused)
            {
                _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].Texture =
                    new RaylibTexture(
                        StaticVariablesInitialize.TextureArrowUp,
                        _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].Texture.TextureUsage,
                        _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].Texture.TextureSize
                    );
            }
            else
            {
                _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].Texture =
                    new RaylibTexture(
                        StaticVariablesInitialize.TextureArrowDown,
                        _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].Texture.TextureUsage,
                        _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.StartStopAlgoPlayBack].Texture.TextureSize
                    );
            }
        }
    }
}