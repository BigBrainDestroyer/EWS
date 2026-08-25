
namespace Program;

public class UpdateMazeLayoutMazeView
{
    private readonly AppState _state;

    public UpdateMazeLayoutMazeView(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void MazeView()
    {
        var _generator = _state.maze.generator;
        var _layout = _state.maze.layout.mazeView;
        
        _layout.CurrentMaze = UpdateCurrentMaze(_generator.AllMazes, _layout.AllMazePosition);
    }

    private Maze UpdateCurrentMaze(Maze[] allMazes, int allMazePosition)
    {
        Maze newCurrentMaze = allMazes[allMazePosition];

        return newCurrentMaze;
    }
}