using System.Numerics;
using Raylib_cs;

namespace Program;

public class MazeLayoutMazeViewState
{
    public Maze CurrentMaze { get; set; }

    public Vector2 MazeSlotLengths { get; set; } = new Vector2();

    public float LineThick { get; set; } = 4;

    public int AllMazePosition { get; set; } = 0;
}