
namespace ShareData;

internal class RunningMazeState
{
    public Maze currentMaze { get; set; } = new Maze();

    public IVector2 CurrentNavigatorCoordinates { get; set; } = new IVector2(1,1);

    public IVector2 currentMazeEndPoint { get; set; } = new IVector2(1,1);

    public int currentMazeNumber { get; set; } = 0;
}