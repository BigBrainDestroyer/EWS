using System.Numerics;

namespace Program;

public class GenerateMazeSeed
{
    private readonly AppState _state;

    public GenerateMazeSeed(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Generate()
    {
        var _generator = _state.maze.generator;

        _generator.MazeSeedBach = 
            MakeSeed(
                _generator.MazeSize,
                _generator.MazeBachSize
            );
    }

    private int[][] MakeSeed(Vector2 mazeSize, int mazeBachSize)
    {
        int[][] newSeedBach = new int[mazeBachSize][];
        int seedLength = (int)mazeSize.X * (int)mazeSize.Y;

        for (int x = 0; x < newSeedBach.Length; x++)
        {
            newSeedBach[x] = new int[seedLength];

            for (int i = 0; i < seedLength; i++)
            {
                newSeedBach[x][i] = Random.Shared.Next(0, 4);
            }
        }

        return newSeedBach;
    }
}