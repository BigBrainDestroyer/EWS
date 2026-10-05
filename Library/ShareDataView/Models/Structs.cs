
namespace ShareData;

public struct DVector2
{
    double X { get; set; }
    double Y { get; set; }

    public DVector2(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public struct BoolVector4
{
    public bool X;
    public bool Y;
    public bool Z;
    public bool W;

    public bool[] Array;

    public BoolVector4(bool x, bool y, bool z, bool w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;

        Array = new bool[4] {x, y, z, w};
    }
}

public struct SlotSearchedWithTime
{
    double Time { get; set; }
    IVector2 SlotCoordinates { get; set; }

    public SlotSearchedWithTime(double time, IVector2 slotCoordinates)
    {
        Time = time;
        SlotCoordinates = slotCoordinates;
    }
}

public struct SlotDataVariables
{
    public SlotSearchedWithTime SlotSearchedWithTime { get; set; }
    public bool RepeatedSlotSearched { get; set; }
    public int MazeNumber { get; set; }

    public SlotDataVariables(SlotSearchedWithTime slotSearchedWithTime, bool repeatedSlotSearched, int mazeNumber)
    {
        SlotSearchedWithTime = slotSearchedWithTime;
        RepeatedSlotSearched = repeatedSlotSearched;
        MazeNumber = mazeNumber;
    }
}

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