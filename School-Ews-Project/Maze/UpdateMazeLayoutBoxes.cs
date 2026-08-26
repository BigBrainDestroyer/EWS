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
        var _commonBoxes = _state.common.boxes;
        var _mazeBoxes = _state.maze.layout.boxes;
        
        MazeViewBoxNames[] boxNames = Enum.GetValues<MazeViewBoxNames>();

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

        CalculatePosition();
    }

    private Vector2 CalculatePixelLength(MazeViewBoxNames boxName, Vector2 childBoxPercentageLengths, Vector2 parentBoxLengths)
    {
        Vector2 childBoxPixelLengths = 
            new Vector2(
                (childBoxPercentageLengths.X / 100f) * parentBoxLengths.X,
                (childBoxPercentageLengths.Y / 100f) * parentBoxLengths.Y
            );

        if(boxName == MazeViewBoxNames.ParentBox || boxName == MazeViewBoxNames.CommonData)
        {
            childBoxPixelLengths.X = MathF.Round(childBoxPixelLengths.X);
        }
        
        return childBoxPixelLengths;
    }

    
    private void CalculatePosition()
    {
        var _mazeBoxes = _state.maze.layout.boxes;
        var _commonBoxes = _state.common.boxes;

        int whiteWidthSpace = ((int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze].X - (int)_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox].X)/2;
        int whiteHightSpace = ((int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze].Y - (int)_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox].Y)/2;

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Background] = _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze];
        
        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox] =
            new Vector2(
                _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze].X + whiteWidthSpace,
                _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze].Y + whiteHightSpace
            );

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Maze] = _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox];
        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.CommonData] =
            new Vector2(
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].X + _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.Maze].X, 
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].Y
            );
    }
}