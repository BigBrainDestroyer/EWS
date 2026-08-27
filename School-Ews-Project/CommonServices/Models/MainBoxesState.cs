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
    Editor,
    Terminal
}

public class MainBoxesState
{
    public Dictionary<MainColumnsName, Vector2> MainColumnsPercentage { get; set; } =
        new Dictionary<MainColumnsName, Vector2>{
            {MainColumnsName.Archive, new Vector2(12f, 100f)},
            {MainColumnsName.Visualize, new Vector2(48f, 100f)},
            {MainColumnsName.Editor, new Vector2(40f, 100f)}
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
            {MainBoxNames.Maze      , new Vector2(100f, 65f)},
            {MainBoxNames.Statistics, new Vector2(100f, 35f)},
            {MainBoxNames.Editor    , new Vector2(100f, 65f)},
            {MainBoxNames.Terminal  , new Vector2(100f, 35f)}
        };

    public Dictionary<MainBoxNames, Vector2> MainBoxPixelLengts { get; set; } =
        new Dictionary<MainBoxNames, Vector2>{
            {MainBoxNames.Archive   , new Vector2(0,0)},
            {MainBoxNames.Maze      , new Vector2(0,0)},
            {MainBoxNames.Statistics, new Vector2(0,0)},
            {MainBoxNames.Editor    , new Vector2(0,0)},
            {MainBoxNames.Terminal  , new Vector2(0,0)}
        };

    public Dictionary<MainBoxNames, Vector2> MainBoxPixelPositions { get; set; } =
    new Dictionary<MainBoxNames, Vector2>{
        {MainBoxNames.Archive   , new Vector2(0,0)},
        {MainBoxNames.Maze      , new Vector2(0,0)},
        {MainBoxNames.Statistics, new Vector2(0,0)},
        {MainBoxNames.Editor    , new Vector2(0,0)},
        {MainBoxNames.Terminal  , new Vector2(0,0)}
    };

    public Vector2 HeaderPercentages { get; set; } = new Vector2(100f, 6f);
    public Vector2 HeaderPixelLengths { get; set;}= new Vector2();
    public Vector2 HeaderPixelPosition = new Vector2(0,0);

    public float MainBoxesPercentageSpacing { get; set; } = 1.35f;

    public float MainBoxesPixelSpacing { get; set; }
}