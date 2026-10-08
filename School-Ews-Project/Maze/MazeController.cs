
namespace Program;

public class MazeController
{
    private readonly AppState _state;
    private readonly UpdateMazeLayout _mazeLayout;
    private readonly MazeGenerator _generator;

    public MazeController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _mazeLayout = new UpdateMazeLayout(_state);
        _generator = new MazeGenerator(_state);
    }

    public void Controller()
    {
        _mazeLayout.MazeLayout();
    }
}