using System.Numerics;

namespace Program;

public enum WallState
{
    Open,
    Closed,
    OutsideMaze
}

public enum Directions
{
    Up,
    Right,
    Down,
    Left
}

public struct Maze
{
    public WallState[][] UsingXToGetRightArray;
    public WallState[][] UsingYToGetRightArray;

    public Maze(WallState[][] X, WallState[][] Y)
    {   
        UsingXToGetRightArray = X;
        UsingYToGetRightArray = Y;
    }
}

public struct MazeNavigator
{
    public int X;
    public int Y;
}

public struct WallStates
{
    public WallState X;
    public WallState Y;
}

public class MazeGeneratorState
{
    public Maze[] AllMazes { get; set; } = new Maze[0];

    public int[][] MazeSeedBach { get; set; } = new int[0][];

    Vector2[] MazeEndPoints { get; set; } = new Vector2[0];

    public Vector2 MazeSize = new Vector2(20, 20);

    public volatile bool ReadingAllMazes = false;

    public int MazeMakers { get; set; } = 1;
    
    public int MazeBachSize { get; set; } = 1;   
}