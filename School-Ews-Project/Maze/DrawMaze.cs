using System.Numerics;
using Raylib_cs;

namespace Program;

public class DrawMazeLayout
{
    private readonly AppState _state;

    public DrawMazeLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Maze()
    {
        DrawMazeView();
    }

    private void DrawMazeView()
    {
        DrawBackground();

        DrawMaze();
    }

    private void DrawBackground()
    {
        var _boxes = _state.maze.layout.boxes;

        Raylib.DrawRectangleV(
            _boxes.BoxPixelPositions[MazeViewBoxNames.Background],
            _boxes.BoxPixelLengths[MazeViewBoxNames.Background],
            _boxes.BackgroundColor
        );
    }

    private void DrawMaze()
    {
        
    }
}