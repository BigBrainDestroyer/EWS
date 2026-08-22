using System.Numerics;

namespace Program;

public struct Maze
{
    public int[][] X;
    public int[][] Y;
}

public struct MazeNavigator
{
    public int X;
    public int Y;
}

public class MazeGeneratorState
{
    public Maze CurrentMaze { get; set; }

    public Vector2 MazeSize = new Vector2(10, 10);

    public int MazeMakers { get; set; } = 1;

    public int[] MazeSeed { get; set; } = new int[0];



}