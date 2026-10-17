
namespace Program;

public class BuildMazeButton
{
    private readonly AppState _state;

    public BuildMazeButton(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void UpdateButtonAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        if(_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BuildMaze] = true;
        }
        else if(_generator.BuildingMazes)
        {
            _header.HeaderButtons[DrawHeaderObjectNames.BuildMaze].BoxBackgroundColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.BuildMaze].BoxColors.Y;

            _header.HeaderButtons[DrawHeaderObjectNames.BuildMaze].TextColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.BuildMaze].SymbolColors.Y;
        }
        else
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BuildMaze] = false;
        }
    }
}