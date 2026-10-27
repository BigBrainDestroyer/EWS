
namespace Program;

public class MazeLayoutState
{
    public MazeLayoutBoxesState boxes { get; set; } = new MazeLayoutBoxesState();
    public MazeLayoutMazeViewState mazeView { get; set; } = new MazeLayoutMazeViewState();
}