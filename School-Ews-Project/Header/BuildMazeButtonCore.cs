
using Raylib_cs;
using ShareData;

namespace Program;

public class BuildMazeButtonCore
{
    private readonly AppState _state;
    private readonly OutsideTerminal _terminal;
    private readonly MazeGenerator _generate;

    public BuildMazeButtonCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _terminal = new OutsideTerminal(_state);
        _generate = new MazeGenerator(_state);
    }

    public void UpdateButtonAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        if (_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BuildMaze] = true;
        }
        else if(_generator.BuildingMazes)
        {
            _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.BuildMaze].BoxBackgroundColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.BuildMaze].BoxColors.Y;

            _header.HeaderCenterTextureInBox[DrawHeaderObjectNames.BuildMaze].TextureColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.BuildMaze].SymbolColors.Y;

            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BuildMaze] = false;
        }
        else
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BuildMaze] = false;
        }
    }

    public void ButtonClick()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;

        if(_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _generate.Generator();
        }
    }
}