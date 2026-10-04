
namespace ShareData;


public struct Maze
{
    public WallState[][] UsingXToGetRightArray { get; set; }
    public WallState[][] UsingYToGetRightArray { get; set; }

    public Maze(WallState[][] X, WallState[][] Y)
    {
        UsingXToGetRightArray = X;
        UsingYToGetRightArray = Y;
    }
}

public struct IVector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public IVector2(int x, int y)
    {
        X = x;
        Y = y;
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