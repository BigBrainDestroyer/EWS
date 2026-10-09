
namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal class RunningMazeState
{
    public Maze CurrentMaze { get; set; } = new Maze();

    public IVector2 CurrentNavigatorCoordinates { get; set; } = new IVector2(1,1);

    public IVector2 CurrentMazeEndPoint { get; set; } = new IVector2(1,1);

    public int CurrentMazeNumber { get; set; } = 0;
}