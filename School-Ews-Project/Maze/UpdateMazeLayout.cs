using System.Numerics;

namespace Program;

public class UpdateMazeLayout
{
    private readonly AppState _state;
    private readonly UpdateMazeLayoutBoxes _mazeLayoutBoxes;
    private readonly UpdateMazeLayoutMazeView _layoutMazeView;

    public UpdateMazeLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _mazeLayoutBoxes = new UpdateMazeLayoutBoxes(_state);

        _layoutMazeView = new UpdateMazeLayoutMazeView(_state);
    }

    public void MazeLayout()
    {
        _mazeLayoutBoxes.MazeLayoutBoxes();

        _layoutMazeView.MazeView();
    }    
}