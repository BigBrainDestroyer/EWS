using ShareData;
using System.Numerics;

namespace Program;

public class MazeGeneratorState
{
    public Maze[] AllMazes { get; set; } = new Maze[0];

    public int[][] MazeSeedBach { get; set; } = new int[0][];

    public IVector2[] MazeBeginPoints { get; set; } = new IVector2[0];

    public IVector2[] MazeEndPoints { get; set; } = new IVector2[0];

    public Vector2 MazeSize = new Vector2(4, 4); // boxes length als 4 dan 3 breed dus -1

    public int MazeBachSize { get; set; } = 10;


    //bools
    public volatile bool MazesAreDone = false;
    public volatile bool BuildingMazes = false;
}