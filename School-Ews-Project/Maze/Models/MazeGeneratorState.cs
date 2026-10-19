using ShareData;
using System.Numerics;

namespace Program;

public class MazeGeneratorState
{
    public volatile Maze[] AllMazes = new Maze[0];

    public volatile int[][] MazeSeedBach = new int[0][];

    public volatile IVector2[] MazeBeginPoints = new IVector2[0];

    public volatile IVector2[] MazeEndPoints = new IVector2[0];

    public Vector2 MazeSize { get; set; } = new Vector2(4, 4); // boxes length als 4 dan 3 breed dus -1

    public volatile int MazeBachSize = 1;

    //bools
    public volatile bool MazesAreDone = false;
    public volatile bool BuildingMazes = false;
}