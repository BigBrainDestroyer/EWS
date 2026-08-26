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

        if(_state.maze.layout.mazeView.CommonDataCharHight > 0)
        {
            DrawCommonData();
        }
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
                if (wallState == WallState.Closed)
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
        var _layout = _state.maze.layout;
        var _font = _state.common.font;

        CommonDataNames[] boxNames = Enum.GetValues<CommonDataNames>();

        Vector2 textPosition =
            new Vector2(
                _layout.boxes.BoxPixelPositions[MazeViewBoxNames.CommonData].X + _layout.mazeView.CommonDataBoxPixelSpacing.X,
                _layout.boxes.BoxPixelPositions[MazeViewBoxNames.CommonData].Y
            );

        int counter = 0;

        foreach(CommonDataNames boxName in boxNames)
        {
            if(_layout.mazeView.CommonDataBoxState[boxName])
            {
                counter++;

                Raylib.DrawTextEx(
                    _font.EditorPanelText,
                    _layout.mazeView.CommonDataTitle[boxName],
                    textPosition,
                    _layout.mazeView.CommonDataCharHight,
                    _layout.mazeView.CommonDataCharSpacing,
                    Color.RayWhite
                );

                textPosition.Y += _layout.mazeView.CommonDataCharHight;

                Raylib.DrawTextEx(
                    _font.EditorPanelText,
                    _layout.mazeView.CommonDataInformation[boxName],
                    textPosition,
                    _layout.mazeView.CommonDataCharHight,
                    _layout.mazeView.CommonDataCharSpacing,
                    Color.Gray
                );

                textPosition.Y +=_layout.mazeView.CommonDataSingleBoxLengths.Y - _layout.mazeView.CommonDataCharHight;
            }
        }
    }

    
}