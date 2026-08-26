using System.Numerics;

namespace Program;

public class UpdateMazeLayoutMazeView
{
    private readonly AppState _state;

    public UpdateMazeLayoutMazeView(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void MazeView()
    {
        var _generator = _state.maze.generator;
        var _layout = _state.maze.layout;
        
        _layout.mazeView.CurrentMaze = UpdateCurrentMaze(_generator.AllMazes, _layout.mazeView.AllMazePosition);

        (_layout.boxes.BoxPixelLengths[MazeViewBoxNames.Maze], _layout.boxes.BoxPixelPositions[MazeViewBoxNames.Maze]) = 
            UpdateMazeLengthsAndPosition(
                _layout.boxes.BoxPixelLengths[MazeViewBoxNames.Maze], 
                _layout.boxes.BoxPixelPositions[MazeViewBoxNames.Maze]
            );

        _layout.mazeView.MazeSlotLengths = CalculateMazeSlotPixelLength(
                _layout.boxes.BoxPixelLengths[MazeViewBoxNames.Maze],
                _generator.MazeSize
            );
    }

    private Maze UpdateCurrentMaze(Maze[] allMazes, int allMazePosition)
    {
        Maze newCurrentMaze = allMazes[allMazePosition];

        return newCurrentMaze;
    }

    private (Vector2, Vector2) UpdateMazeLengthsAndPosition(Vector2 boxPixelLengths, Vector2 boxPosition)
    {
        int lengthDifference = 0;

        if(boxPixelLengths.X > boxPixelLengths.Y)
        {
            lengthDifference = (int)(MathF.Abs(boxPixelLengths.Y - boxPixelLengths.X));

            boxPixelLengths.X = boxPixelLengths.Y;

            boxPosition.X += lengthDifference / 2;
        }
        else
        {
            lengthDifference = (int)(MathF.Abs(boxPixelLengths.X - boxPixelLengths.Y));

            boxPixelLengths.Y = boxPixelLengths.X;

            boxPosition.Y += lengthDifference / 2;
        }
        
        return (boxPixelLengths, boxPosition);
    }

    private Vector2 CalculateMazeSlotPixelLength(Vector2 mazeBoxLengths, Vector2 mazeSize)
    {
        Vector2 newSlotPixelLengths = 
            new Vector2(
                mazeBoxLengths.X / (mazeSize.X - 1),
                mazeBoxLengths.Y / (mazeSize.Y - 1)
            );

        return newSlotPixelLengths;
    }
}