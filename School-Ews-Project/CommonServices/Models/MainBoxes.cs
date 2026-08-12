using System.Numerics;

namespace Program;

public class MainBoxes
{
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
}