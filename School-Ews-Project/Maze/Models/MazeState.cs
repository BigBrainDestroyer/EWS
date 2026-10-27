
namespace Program;

public class MazeState
{
    public MazeLayoutState layout { get; set; } = new MazeLayoutState();
    public MazeGeneratorState generator { get; set; } = new MazeGeneratorState();
}