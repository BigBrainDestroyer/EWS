using System.Numerics;
using Raylib_cs;

namespace Program;

public enum MainColumnsName
{
    Archive,
    Visualize,
    Editor
}

public enum MainBoxNames
{
    Archive,
    Maze,
    Editor,
}

public class MainBoxesState
{
    public Color MainBoxesBackgroundColor { get; set; } = new Color(43, 46, 49, 255);

    public MainBoxNames ActiveMainBox { get; set; } = MainBoxNames.Editor;

    public Dictionary<MainColumnsName, Vector2> MainColumnsPercentage { get; set; } =
        new Dictionary<MainColumnsName, Vector2>{
            {MainColumnsName.Archive, new Vector2(13f, 100f)},
            {MainColumnsName.Visualize, new Vector2(48f, 100f)},
            {MainColumnsName.Editor, new Vector2(39f, 100f)}
        };

    public Dictionary<MainColumnsName, Vector2> MainColumnsPixelLengths { get; set; } =
        new Dictionary<MainColumnsName, Vector2>{
            {MainColumnsName.Archive, new Vector2()},
            {MainColumnsName.Visualize, new Vector2()},
            {MainColumnsName.Editor, new Vector2()}
        };

    public Dictionary<MainBoxNames, Vector2> MainBoxPercentages { get; set; } =
        new Dictionary<MainBoxNames, Vector2>{
            {MainBoxNames.Archive   , new Vector2(100f, 100f)},
            {MainBoxNames.Maze      , new Vector2(100f, 100f)},
            {MainBoxNames.Editor    , new Vector2(100f, 100f)},
        };

    public Dictionary<MainBoxNames, Vector2> MainBoxPixelLengts { get; set; } =
        new Dictionary<MainBoxNames, Vector2>{
            {MainBoxNames.Archive   , new Vector2(0,0)},
            {MainBoxNames.Maze      , new Vector2(0,0)},
            {MainBoxNames.Editor    , new Vector2(0,0)},
        };

    public Dictionary<MainBoxNames, Vector2> MainBoxPixelPositions { get; set; } =
    new Dictionary<MainBoxNames, Vector2>{
        {MainBoxNames.Archive   , new Vector2(0,0)},
        {MainBoxNames.Maze      , new Vector2(0,0)},
        {MainBoxNames.Editor    , new Vector2(0,0)},
    };

    public Dictionary<MainBoxNames, BoolVector4> MainBoxesCornerStates { get; set; } =
    new Dictionary<MainBoxNames, BoolVector4>{
        {MainBoxNames.Archive   , new BoolVector4(true, true, true, true)},
        {MainBoxNames.Maze      , new BoolVector4(true, true, true, true)},
        {MainBoxNames.Editor    , new BoolVector4(true, true, true, true)},
    };

    public Dictionary<MainBoxNames, float> MainBoxesCornerRadius { get; set; } =
    new Dictionary<MainBoxNames, float>{
        {MainBoxNames.Archive   , 5f},
        {MainBoxNames.Maze      , 5f},
        {MainBoxNames.Editor    , 5f},
    };

    public Vector2 HeaderPercentages { get; set; } = new Vector2(100f, 6f);
    public Vector2 HeaderPixelLengths { get; set;}= new Vector2();
    public Vector2 HeaderPixelPosition = new Vector2(0,0);

    public float MainBoxesPercentageSpacing { get; set; } = 1.35f;

    public float MainBoxesPixelSpacing { get; set; }
}