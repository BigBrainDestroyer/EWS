using System.Numerics;
using Raylib_cs;

namespace Program;

public enum MazeViewBoxNames
{
    Background,
    ParentBox,
    Maze,
    CommonData
}

public class MazeLayoutBoxesState
{ 
    public Dictionary<MazeViewBoxNames, Vector2> BoxPercentageLengths { get; } =
        new Dictionary<MazeViewBoxNames, Vector2>{
            {MazeViewBoxNames.Background, new Vector2(100f, 100f)},
            {MazeViewBoxNames.ParentBox , new Vector2(90f, 90f)},
            {MazeViewBoxNames.Maze      , new Vector2(100f, 100f)}
        };

    public Dictionary<MazeViewBoxNames, Vector2> BoxPixelPositions { get; set; } =
    new Dictionary<MazeViewBoxNames, Vector2>{
        {MazeViewBoxNames.Background, new Vector2(0,0)},
        {MazeViewBoxNames.ParentBox , new Vector2(0,0)},
        {MazeViewBoxNames.CommonData, new Vector2(0,0)},
        {MazeViewBoxNames.Maze      , new Vector2(0,0)}
    };
    
    public Dictionary<MazeViewBoxNames, Vector2> BoxPixelLengths { get; set; } = 
    new Dictionary<MazeViewBoxNames, Vector2>{
        {MazeViewBoxNames.Background, new Vector2(0,0)},
        {MazeViewBoxNames.ParentBox , new Vector2(0,0)},
        {MazeViewBoxNames.CommonData, new Vector2(0,0)},
        {MazeViewBoxNames.Maze      , new Vector2(0,0)}
    };

    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);
}