
namespace ShareData;

public class ShareMazeData
{    
    public Maze[] AllMazes { get; set; } = new Maze[0];

    public IVector2[] MazeEndPoints { get; set; } = new IVector2[0];

    public IVector2[] MazeBeginPoints { get; set; } = new IVector2[0];
}