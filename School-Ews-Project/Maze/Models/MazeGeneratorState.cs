using ShareData;
using System.Numerics;

namespace Program;

public class MazeGeneratorState
{
    public Maze[] AllMazes { get; set; } = new Maze[0];

    public int[][] MazeSeedBach { get; set; } = new int[0][];

    IVector2[] MazeEndPoints { get; set; } = new IVector2[0];

    public Vector2 MazeSize = new Vector2(30, 30); // boxes length als 4 dan 3 breed dus -1

    public volatile bool ReadingAllMazes = false;

    //public int MazeMakers { get; set; } = 1;
    
    public int MazeBachSize { get; set; } = 10;
}