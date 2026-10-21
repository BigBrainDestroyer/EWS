using ShareData;
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
        var _play = _state.playMaze;

        DrawBackground();

        if (_play.IsMazeLoaded)
        {
            DrawMazeBackground();

            if(_play.IsAlgoLoaded)
            {
                DrawNavigatorHistory();
                DrawNavigator();
            }
            
            DrawMaze();

            DrawCommonData();
        }
        else
        {
            //mazeLoadScreen
        }
    }

    private void DrawBackground()
    {
        var _boxes = _state.maze.layout.boxes;

        Custom.DrawCustomRectangle(
            _boxes.BoxPixelPositions[MazeViewBoxNames.Background], 
            _boxes.BoxPixelLengths[MazeViewBoxNames.Background],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Maze],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Maze],
            _boxes.BackgroundColor
        );
    }
    
    private void DrawMazeBackground()
    {
        var _boxes = _state.maze.layout.boxes;
        
        Raylib.DrawRectangleV(
            _boxes.BoxPixelPositions[MazeViewBoxNames.Maze],
            _boxes.BoxPixelLengths[MazeViewBoxNames.Maze],
            Color.Black
        );
    }
    
    private void DrawNavigatorHistory()
    {
        var _boxes = _state.maze.layout.boxes;
        var _generator = _state.maze.generator;
        var _mazeView = _state.maze.layout.mazeView;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        
        Vector2 startPosition = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze];

        Color navigatorPathColor = new Color((int)Color.RayWhite.A, Color.RayWhite.B, Color.RayWhite.G, 7);

        for (int i = _play.SlotHistoryListPosition; i >= 0; i--)
        {   
            if (_shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].MazeNumber == _play.MazeListPosition)
            {
                Raylib.DrawRectangleV(
                  new Vector2(
                      startPosition.X + (_mazeView.MazeSlotLengths.X * (_shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].SlotSearchedWithTime.SlotCoordinates.X -1)) ,
                      startPosition.Y + (_mazeView.MazeSlotLengths.Y * (_shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].SlotSearchedWithTime.SlotCoordinates.Y -1))
                  ),
                  _mazeView.MazeSlotLengths,
                  navigatorPathColor
                );
            }
            else
            {
                break;
            }
        }
    }

    private void DrawNavigator()
    {
        var _boxes = _state.maze.layout.boxes;
        var _generator = _state.maze.generator;
        var _mazeView = _state.maze.layout.mazeView;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        
        Vector2 startPosition = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze];

        Color navigatorColor = Color.DarkBlue;
        
        Raylib.DrawRectangleV(
          new Vector2(
              startPosition.X + (_mazeView.MazeSlotLengths.X * (_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.X -1)) ,
              startPosition.Y + (_mazeView.MazeSlotLengths.Y * (_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.Y -1))
          ),
          _mazeView.MazeSlotLengths,
          navigatorColor
        );
    }

    private void DrawMaze()
    {
        var _boxes = _state.maze.layout.boxes;
        var _generator = _state.maze.generator;
        var _mazeView = _state.maze.layout.mazeView;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;

        Vector2 startPosition = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze];

        for (int i = 1; i < _mazeView.CurrentMaze.UsingYToGetRightArray.Length; i++)
        {
            foreach (WallState wallState in _mazeView.CurrentMaze.UsingYToGetRightArray[i])
            {
                if ( wallState == WallState.Closed)
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

        startPosition = _boxes.BoxPixelPositions[MazeViewBoxNames.Maze];
            
        for (int i = 1; i < _mazeView.CurrentMaze.UsingXToGetRightArray.Length; i++)
        {
            foreach (WallState wallState in _mazeView.CurrentMaze.UsingXToGetRightArray[i])
            {
                if (wallState == WallState.Closed)
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
    }

    private void DrawCommonData()
    {
        var _mazeView = _state.maze.layout.mazeView;
        var _font = _state.common.font;

        CommonDataNames[] boxNames = Enum.GetValues<CommonDataNames>();

        foreach(CommonDataNames boxName in boxNames)
        {
            if (_mazeView.CommonDataBoxState[boxName])
            {
                Raylib.DrawTextEx(
                    _mazeView.CommonDataBoxes[boxName].TitleBox.Font,
                    _mazeView.CommonDataBoxes[boxName].TitleBox.Text.Text,
                    _mazeView.CommonDataBoxes[boxName].TitleBox.TextPosition,
                    _mazeView.CommonDataBoxes[boxName].TitleBox.Text.TextSize,
                    _mazeView.CommonDataBoxes[boxName].TitleBox.Text.TextSpacing,
                    _mazeView.CommonDataBoxes[boxName].TitleBox.TextColor
                );

                Raylib.DrawTextEx(
                    _mazeView.CommonDataBoxes[boxName].InformationBox.Font,
                    _mazeView.CommonDataBoxes[boxName].InformationBox.Text.Text,
                    _mazeView.CommonDataBoxes[boxName].InformationBox.TextPosition,
                    _mazeView.CommonDataBoxes[boxName].InformationBox.Text.TextSize,
                    _mazeView.CommonDataBoxes[boxName].InformationBox.Text.TextSpacing,
                    _mazeView.CommonDataBoxes[boxName].InformationBox.TextColor
                );
            }
        }
    }
}