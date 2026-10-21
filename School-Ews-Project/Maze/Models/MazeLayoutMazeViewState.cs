using ShareData;
using System.Numerics;
using Raylib_cs;

namespace Program;

public enum CommonDataNames
{
    State,
    CoreProcessorUsagePercentage,
    GPU,
    RamTotalUsage,
    BatchState,
    SlotSearched,
    Time
}

public class MazeLayoutMazeViewState
{
    public Maze CurrentMaze { get; set; } = new Maze(new WallState[0][], new WallState[0][]);

    public Dictionary<CommonDataNames, InformationViewer> CommonDataBoxes =
        new Dictionary<CommonDataNames, InformationViewer>()
        {
            {CommonDataNames.State,
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(true, true, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("State", 0, 0),
                        new Vector2(),
                        new Vector2(34,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(10,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )},
            {CommonDataNames.CoreProcessorUsagePercentage, 
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("CPU Usage", 0, 0),
                        new Vector2(),
                        new Vector2(34,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(10,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )},
            {CommonDataNames.RamTotalUsage, 
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("RAM Usage", 0, 0),
                        new Vector2(),
                        new Vector2(34,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(10,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )},
            {CommonDataNames.GPU, 
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("RAM Usage", 0, 0),
                        new Vector2(),
                        new Vector2(34,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(10,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )},
            {CommonDataNames.BatchState, 
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("BatchState", 0, 0),
                        new Vector2(),
                        new Vector2(6,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(10,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )},
            {CommonDataNames.SlotSearched, 
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("TotalSlots", 0, 0),
                        new Vector2(),
                        new Vector2(6,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(10,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )},
            {CommonDataNames.Time, 
                new InformationViewer(
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, false, false),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("Time", 0, 0),
                        new Vector2(),
                        new Vector2(34,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    new CenterTextInBox(
                        new Font(),
                        new CustomBox(
                            new BoolVector4(false, false, true, true),
                            new Vector2(),
                            new Vector2(),
                            8
                        ),
                        new RaylibText("", 0, 0),
                        new Vector2(),
                        new Vector2(34,20),
                        StaticVariablesInitialize.FontLilexBoldPath,
                        Color.DarkBlue,
                        Color.Red
                    ),
                    57.5f
                )}
        };

    public Dictionary<CommonDataNames, bool> CommonDataBoxState { get; set; } =
        new Dictionary<CommonDataNames, bool>{
            {CommonDataNames.State, true},
            {CommonDataNames.CoreProcessorUsagePercentage, true},
            {CommonDataNames.GPU, false},
            {CommonDataNames.RamTotalUsage, true},
            {CommonDataNames.BatchState, true},
            {CommonDataNames.SlotSearched, true},
            {CommonDataNames.Time, true}
        };

    public Dictionary<CommonDataNames, string> CommonDataTitle { get; } =
        new Dictionary<CommonDataNames, string>{
            {CommonDataNames.State, "State"},
            {CommonDataNames.CoreProcessorUsagePercentage, "CPU Usage"},
            {CommonDataNames.GPU, "GPU Usage"},
            {CommonDataNames.RamTotalUsage, "RAM Usage"},
            {CommonDataNames.BatchState, "BatchState"},
            {CommonDataNames.SlotSearched, "SearchSlots"},
            {CommonDataNames.Time, "Time"}
        };

    public Dictionary<CommonDataNames, string> CommonDataInactiveInformation { get; set; } =
        new Dictionary<CommonDataNames, string>{
            {CommonDataNames.State, "Inactive"},
            {CommonDataNames.CoreProcessorUsagePercentage, "___%"},
            {CommonDataNames.GPU, "___%"},
            {CommonDataNames.RamTotalUsage, "___ / _GB"},
            {CommonDataNames.BatchState, "___"},
            {CommonDataNames.SlotSearched, "___"},
            {CommonDataNames.Time, "__:__"}
        };

    public Dictionary<CommonDataNames, string> CurrentCommonDataInformation { get; set; } =
        new Dictionary<CommonDataNames, string>{
            {CommonDataNames.State, ""},
            {CommonDataNames.CoreProcessorUsagePercentage, ""},
            {CommonDataNames.GPU, ""},
            {CommonDataNames.RamTotalUsage, ""},
            {CommonDataNames.BatchState, ""},
            {CommonDataNames.SlotSearched, ""},
            {CommonDataNames.Time, ""}
        };
    
    public Vector2 CommonDataSingleBoxLengths { get; set; } = new Vector2();
    
    public Vector2 MazeSlotLengths { get; set; } = new Vector2();

    public Vector2 CommonDataBoxPercentageSpacing { get; set; } = new Vector2(13f, 25f);

    public Vector2 CommonDataBoxPixelSpacing { get; set; } = new Vector2();

    public float LineThick { get; set; } = 3;

    public float CommonDataCharHeight { get; set; } = 10;

    public float CommonDataCharSpacing { get; set; } = 0;

    public int AmountOfCommonDataBoxes { get; set; } = 5;
} 
