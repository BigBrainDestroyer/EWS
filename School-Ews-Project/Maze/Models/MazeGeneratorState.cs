using ShareData;
using System.Numerics;

namespace Program;

public class MazeGeneratorState
{
    public Maze[] AllMazes { get; set; } = new Maze[0];

    public int[][] MazeSeedBach { get; set; } = new int[0][];

    public IVector2[] MazeBeginPoints { get; set; } = new IVector2[0];

    public IVector2[] MazeEndPoints { get; set; } = new IVector2[0];

    public Vector2 MazeSize { get; set; } = new Vector2(7, 7); // boxes length als 4 dan 3 breed dus -1

    public int MazeBachSize { get; set; } = 1;

    //bools
    public volatile bool MazesAreDone = false;
    public volatile bool BuildingMazes = false;
}