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
        double leftOverWidthsPixels = 0;
        double leftOverHeightsPixels = 0;

        foreach(MazeViewBoxNames boxName in boxNames)
        {
            (_mazeBoxes.BoxPixelLengths[boxName], leftOverWidthsPixels, leftOverHeightsPixels) = 
                CalculatePixelLength(
                    _mazeBoxes.BoxPercentageLengths[boxName], 
                    _commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze],
                    leftOverWidthsPixels,
                    leftOverHeightsPixels
                );
        }
        
        leftOverWidthsPixels  = Math.Round(leftOverWidthsPixels);
        leftOverHeightsPixels = Math.Round(leftOverHeightsPixels);
        
        foreach(MazeViewBoxNames boxName in boxNames)
        {
            bool noLeftOverPixels = true;

            if(leftOverWidthsPixels > 0)
            {
                _mazeBoxes.BoxPixelLengths[boxName] = 
                    new Vector2(
                        _mazeBoxes.BoxPixelLengths[boxName].X + 1, 
                        _mazeBoxes.BoxPixelLengths[boxName].Y
                    );
                
                leftOverWidthsPixels -= 1;

                noLeftOverPixels = false;
            }

            if(leftOverHeightsPixels > 0)
            {
                _mazeBoxes.BoxPixelLengths[boxName] = 
                    new Vector2(
                        _mazeBoxes.BoxPixelLengths[boxName].X,
                        _mazeBoxes.BoxPixelLengths[boxName].Y + 1
                    );
                
                leftOverHeightsPixels -= 1;

                noLeftOverPixels = false;
            }

            if(noLeftOverPixels)
            {
                break;
            }
        }

        CalculatePosition();
    }

    private (Vector2, double, double) CalculatePixelLength(Vector2 childBoxPercentageLengths, Vector2 parentBoxLengths, double leftOverWidthPixels, double leftOverHightPixels)
    {
        Vector2 childBoxPixelLengths = 
            new Vector2(
                (childBoxPercentageLengths.X / 100f) * parentBoxLengths.X,
                (childBoxPercentageLengths.Y / 100f) * parentBoxLengths.Y
            );
        
        leftOverWidthPixels += childBoxPixelLengths.X - childBoxPixelLengths.X;
        leftOverHightPixels += childBoxPixelLengths.Y - childBoxPixelLengths.Y;

        return (childBoxPixelLengths, leftOverWidthPixels, leftOverHightPixels);
    }

    
    private void CalculatePosition()
    {
        var _mazeBoxes = _state.maze.layout.boxes;
        var _commonBoxes = _state.common.boxes;

        int whiteWidthSpace = (int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze].X - (int)_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox].X;
        int whiteHightSpace = (int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Maze].Y - (int)_mazeBoxes.BoxPixelLengths[MazeViewBoxNames.ParentBox].Y;

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Background] = _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze];
        
        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox] =
            new Vector2(
                _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze].X + whiteWidthSpace,
                _commonBoxes.MainBoxPixelPositions[MainBoxNames.Maze].Y + whiteHightSpace
            );

        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Maze] = _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox];
        _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.Maze] =
            new Vector2(
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].X + _mazeBoxes.BoxPixelLengths[MazeViewBoxNames.Maze].X, 
                _mazeBoxes.BoxPixelPositions[MazeViewBoxNames.ParentBox].Y
            );
    }
}