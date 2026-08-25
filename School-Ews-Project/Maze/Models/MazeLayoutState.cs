using System.Numerics;
using Raylib_cs;

namespace Program;

public class MazeLayoutState
{
    public MazeLayoutBoxesState boxes { get; set; } = new MazeLayoutBoxesState();
    public MazeLayoutMazeViewState mazeView { get; set; } = new MazeLayoutMazeViewState();
}