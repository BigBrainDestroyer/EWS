
namespace Program;

public class UpdateMazeLayout
{
    private readonly AppState _state;
    private readonly UpdateMazeLayoutBoxes _mazeLayoutBoxes;

    public UpdateMazeLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _mazeLayoutBoxes = new UpdateMazeLayoutBoxes(_state);
    }

    public void MazeLayout()
    {

    }    
}