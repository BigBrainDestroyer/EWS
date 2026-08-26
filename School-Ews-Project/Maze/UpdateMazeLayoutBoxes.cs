using System.Numerics;

namespace Program;

public class UpdateMazeLayoutBoxes
{
    private readonly AppState _state;

    public UpdateMazeLayoutBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void MazeLayoutBoxes()
    {
        UpdateMazeViewBoxes();
    }

    private void UpdateMazeViewBoxes()
    {
        var _mazeBoxes = _state.maze.layout.boxes;
        var _mazeView = _state.maze.layout.mazeView;
        var _commonBoxes = _state.common.boxes;
        
        MazeViewBoxNames[] boxNames = Enum.GetValues<MazeViewBoxNames>();
        Vector2 lengthDifference = new Vector2();

        foreach(MazeViewBoxNames boxName in boxNames)
        {
            Vector2 parentBox = (boxName == MazeViewBoxNames.Maze || boxName == MazeViewBoxNames.CommonData) ? _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox] : _commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze];
            
            (_mazeBoxes.BoxPixelLengths[boxName]) = 
                CalculatePixelLength(
                    boxName,
                    _mazeBoxes.BoxPercentageLengths[boxName], 
                    parentBox
                );
        }

        (_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.Maze], _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.CommonData], lengthDifference) = 
            UpdateChildBoxLength(
                _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.Maze], 
                _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.CommonData],
                _mazeView.LineThick
            );

        CalculatePosition(lengthDifference);
    }

    private Vector2 CalculatePixelLength(MazeViewBoxNames boxName, Vector2 childBoxPercentageLengths, Vector2 parentBoxLengths)
    {
        Vector2 childBoxPixelLengths = 
            new Vector2(
                (childBoxPercentageLengths.X / 100f) * parentBoxLengths.X,
                (childBoxPercentageLengths.Y / 100f) * parentBoxLengths.Y
            );
        
        return childBoxPixelLengths;
    }

    private (Vector2, Vector2, Vector2) UpdateChildBoxLength(Vector2 mazeBoxLengths, Vector2 commonBoxLengths, float lineThick)
    {
        Vector2 lengthDifference = new Vector2(0,0);

        if(mazeBoxLengths.X > mazeBoxLengths.Y)
        {
            lengthDifference.X = (MathF.Abs(mazeBoxLengths.Y - mazeBoxLengths.X));

            mazeBoxLengths.X = mazeBoxLengths.Y;

            commonBoxLengths.Y = mazeBoxLengths.Y;
        }
        else
        {
            lengthDifference.Y = (MathF.Abs(mazeBoxLengths.X - mazeBoxLengths.Y));

            mazeBoxLengths.Y = mazeBoxLengths.X;

            commonBoxLengths.Y = mazeBoxLengths.X;
        }

        commonBoxLengths.X -= MathF.Round(lineThick / 2);
        commonBoxLengths.Y += MathF.Round(lineThick);
        
        return (mazeBoxLengths, commonBoxLengths, lengthDifference);
    }
    
    private void CalculatePosition(Vector2 lengthDifference)
    {
        var _mazeBoxes = _state.maze.layout.boxes;
        var _mazeView = _state.maze.layout.mazeView;
        var _commonBoxes = _state.common.boxes;

        int whiteWidthSpace = ((int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze].X - (int)_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox].X)/2;
        int whiteHightSpace = ((int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze].Y - (int)_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox].Y)/2;

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Background] = _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze];
        
        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox] =
            new Vector2(
                _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze].X + whiteWidthSpace,
                _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze].Y + whiteHightSpace
            );

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Maze] =
            new Vector2(
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].X + (lengthDifference.X/2),
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].Y + (lengthDifference.Y/2)
            );
        

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.CommonData] =
            new Vector2(
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Maze].X + _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.Maze].X + (MathF.Round(_mazeView.LineThick/2)), 
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].Y + (lengthDifference.Y/2) - (MathF.Round(_mazeView.LineThick/2))
            );
    }
}