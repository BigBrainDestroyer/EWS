using System.Numerics;

namespace Program;

public enum CommonDataNames
{
    State,
    CoreProcessorUsagePercentage,
    GPU,
    RamTotalUsage,
    SlotSearched,
    Time
}

public class MazeLayoutMazeViewState
{
    public Maze CurrentMaze { get; set; } = new Maze(new WallState[0][], new WallState[0][]);

    public Dictionary<CommonDataNames, bool> CommonDataBoxState { get; set; } =
        new Dictionary<CommonDataNames, bool>{
            {CommonDataNames.State, true},
            {CommonDataNames.CoreProcessorUsagePercentage, true},
            {CommonDataNames.GPU, true},
            {CommonDataNames.RamTotalUsage, true},
            {CommonDataNames.SlotSearched, true},
            {CommonDataNames.Time, true}
        };

    public Dictionary<CommonDataNames, string> CommonDataTitle { get; } =
        new Dictionary<CommonDataNames, string>{
            {CommonDataNames.State, "State"},
            {CommonDataNames.CoreProcessorUsagePercentage, "CPU Usage"},
            {CommonDataNames.GPU, "GPU Usage"},
            {CommonDataNames.RamTotalUsage, "RAM Usage"},
            {CommonDataNames.SlotSearched, "SearchSlots"},
            {CommonDataNames.Time, "Time"}
        };

    public Dictionary<CommonDataNames, string> CommonDataInformation { get; set; } =
        new Dictionary<CommonDataNames, string>{
            {CommonDataNames.State, "Inactive"},
            {CommonDataNames.CoreProcessorUsagePercentage, "__%"},
            {CommonDataNames.GPU, "__%"},
            {CommonDataNames.RamTotalUsage, "___ / _GB"},
            {CommonDataNames.SlotSearched, "___"},
            {CommonDataNames.Time, "__:__"}
        };
        
    public Vector2 CommonDataSingleBoxLengths { get; set; } = new Vector2();
    
    public Vector2 MazeSlotLengths { get; set; } = new Vector2();

    public Vector2 CommonDataBoxPercentageSpacing { get; set; } = new Vector2(13f, 25f);

    public Vector2 CommonDataBoxPixelSpacing { get; set; } = new Vector2();

    public float LineThick { get; set; } = 3;

    public float CommonDataCharHeight { get; set; } = 10;

    public float CommonDataCharSpacing { get; set; } = 0;

    public int AllMazePosition { get; set; } = 0;

    public int AmountOfCommonDataBoxes { get; set; } = 5;
} 
