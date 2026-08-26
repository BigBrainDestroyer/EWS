using System.Numerics;

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
    Statistics,
    Editor
}

public class MainBoxesState
{   
    public Dictionary<MainColumnsName, Vector2> MainColumnsPercentage { get; set; } =
        new Dictionary<MainColumnsName, Vector2>{
            {MainColumnsName.Archive, new Vector2(12f, 100f)},
            {MainColumnsName.Visualize, new Vector2(48f, 100f)},
            {MainColumnsName.Editor, new Vector2(40f, 100f)}
        };

    public Dictionary<MainColumnsName, Vector2> MainColumnslengths { get; set; } =
        new Dictionary<MainColumnsName, Vector2>{
            {MainColumnsName.Archive, new Vector2()},
            {MainColumnsName.Visualize, new Vector2()},
            {MainColumnsName.Editor, new Vector2()}
        };

    public Dictionary<MainBoxNames, float> MainBoxPercentageWidths { get; } =
    new Dictionary<MainBoxNames, float>{
        {MainBoxNames.Archive   , 20f},
        {MainBoxNames.Maze      , 40f},
        {MainBoxNames.Statistics, 40f},
        {MainBoxNames.Editor    , 40f}
    };

    public Dictionary<MainBoxNames, float> MainBoxPercentageHeights { get; } =
    new Dictionary<MainBoxNames, float>{
        {MainBoxNames.Archive   , 100f},
        {MainBoxNames.Maze      , 70f},
        {MainBoxNames.Statistics, 30f},
        {MainBoxNames.Editor    , 100f}
    };

    public Dictionary<MainBoxNames, Vector2> MainBoxPixelLengths { get; set; } = 
    new Dictionary<MainBoxNames, Vector2>{
        {MainBoxNames.Archive   , new Vector2(0,0)},
        {MainBoxNames.Maze      , new Vector2(0,0)},
        {MainBoxNames.Statistics, new Vector2(0,0)},
        {MainBoxNames.Editor    , new Vector2(0,0)}
    };

    public Dictionary<MainBoxNames, Vector2> MainBoxPixelPositions { get; set; } =
    new Dictionary<MainBoxNames, Vector2>{
        {MainBoxNames.Archive   , new Vector2(0,0)},
        {MainBoxNames.Maze      , new Vector2(0,0)},
        {MainBoxNames.Statistics, new Vector2(0,0)},
        {MainBoxNames.Editor    , new Vector2(0,0)}
    };

    public Vector2 HeaderPercentages = new Vector2(100f, 10f);
    public Vector2 HeaderPixelLengths = new Vector2(100f, 10f);

    public float MainBoxesSpacing { get; set; } = 4f;
}