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

        DrawCommonData();
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
        var _boxes = _state.maze.layout.boxes;
        var _generator = _state.maze.generator;
        var _mazeView = _state.maze.layout.mazeView;
        
        Raylib.DrawRectangleV(
            _boxes.BoxPixelPositions[MazeViewBoxNames.Maze],
            _boxes.BoxPixelLengths[MazeViewBoxNames.Maze],
            Color.Black
        );

        Vector2 startPosition = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze];

        for (int i = 1; i < _mazeView.CurrentMaze.UsingYToGetRightArray.Length; i++)
        {
            foreach (WallState wallState in _mazeView.CurrentMaze.UsingYToGetRightArray[i])
            {
                if ((int)wallState == 1)
                {
                    Raylib.DrawLineEx(
                        new Vector2(
                            startPosition.X - (_mazeView.LineThick/2),
                            startPosition.Y
                        ),
                        new Vector2(
                            startPosition.X + _mazeView.MazeSlotLengths.X + (_mazeView.LineThick/2),
                            startPosition.Y
                        ),
                        _mazeView.LineThick,
                        Color.Pink
                    );
                }

                startPosition.Y += _mazeView.MazeSlotLengths.Y;
            }

            startPosition.X += _mazeView.MazeSlotLengths.X;
            startPosition.Y = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze].Y;
        }

        startPosition = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze];
            
        for (int i = 1; i < _mazeView.CurrentMaze.UsingXToGetRightArray.Length; i++)
        {
            foreach (WallState wallState in _mazeView.CurrentMaze.UsingXToGetRightArray[i])
            {
                if ((int)wallState == 1)
                {
                    Raylib.DrawLineEx(
                        new Vector2(
                            startPosition.X,
                            startPosition.Y - (_mazeView.LineThick/2)
                        ),
                        new Vector2(
                            startPosition.X,
                            startPosition.Y + _mazeView.MazeSlotLengths.Y + (_mazeView.LineThick/2)
                        ),
                        _mazeView.LineThick,
                        Color.Pink
                    );
                }

                startPosition.X += _mazeView.MazeSlotLengths.X;
            }

            startPosition.X = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze].X;
            startPosition.Y += _mazeView.MazeSlotLengths.Y;    
        }
    }

    private void DrawCommonData()
    {
        var _boxes = _state.maze.layout.boxes;
        
        Raylib.DrawRectangleV(
            _boxes.BoxPixelPositions[MazeViewBoxNames.CommonData],
            _boxes.BoxPixelLengths[MazeViewBoxNames.CommonData],
            Color.Red
        );
    }
}