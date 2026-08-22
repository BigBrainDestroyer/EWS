using System.Numerics;
using System.Linq;

namespace Program;

public class MazeGenerator
{
    private readonly AppState _state;

    public MazeGenerator(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Generator()
    {
        var _generator = _state.maze.generator;
        
        _generator.CurrentMaze = 
            MakeCurrentMaze(
                _generator.MazeSize,
                _generator.MazeSeed
            );
    }

    
    private Maze MakeCurrentMaze(Vector2 mazeSize, int[] seed)
    {
        Maze newMaze;

        newMaze.X = FillArray((int)mazeSize.X, (int)mazeSize.Y);
        newMaze.Y = FillArray((int)mazeSize.Y, (int)mazeSize.X);

        newMaze = MazeMaker(newMaze, mazeSize, seed);

        return newMaze;
    }

    private int[][] FillArray(int arrayLength, int secondArrayLength)
    {
        int[][] newArray = new int[arrayLength][];
        int[] filledArray = new int[secondArrayLength];

        Array.Fill<int>(filledArray, 1);
        Array.Fill<Array>(newArray, filledArray);

        return newArray;
    }

    private Maze MazeMaker(Maze newMaze, Vector2 mazeSize, int[] seed)
    {
        MazeNavigator navigator;
        navigator.X = 0;
        navigator.Y = 0;
        
        seed = new int[500]; //Tijdelijk
        Array.Fill<int>(seed, 1); //Tijdelijk

        for (int i = 0; i < (int)mazeSize.X * (int)mazeSize.Y; i++)
        {
            if(navigator.X > mazeSize.X - 1 || navigator.Y > mazeSize.Y - 1)
            {
                Console.WriteLine("MazeMaker Loop X of Y is Gebroken");
                break;
            }

            int[] nearBySlots = UpdateNearBySlots();

            //Is er plek om mij heen?
        }

        return newMaze;
    }

    private int[] UpdateNearBySlots(MazeNavigator navigator, Maze newMaze, Vector2 mazeSize)
    {
        int[] nearBySlots = new int[];

        if(navigator.Y - 1 !< 0)
        {
            
        }

        if(navigator.X - 1 !< 0)
        {
            
        }

        if(navigator.Y - 1 !< 0)
        {
            
        }

        if(navigator.Y - 1 !< 0)
        {
            
        }
    }

    private int[] CheckNearBySlots(MazeNavigator navigator, Maze newMaze, Vector2 mazeSize)
    {
        Vector2 X = new Vector2();
        Vector2 Y = new Vector2();

        if(navigator.Y + 1 < mazeSize.Y)
        {
            if(navigator.X - 1 > 0)
            {
                Y =
                    new Vector2(
                        newMaze.Y[navigator.Y + 1][navigator.X - 1],
                        newMaze.Y[navigator.Y + 1][navigator.X]
                    );
            }
            else
            {
                Y =
                    new Vector2(
                        1,
                        newMaze.X[navigator.Y + 1][navigator.X]
                    );
            }

            if(navigator.Y - 1 > 0)
            {
                X =
                    new Vector2(
                        newMaze.X[navigator.X][navigator.Y - 1],
                        newMaze.X[navigator.X][navigator.Y]
                    );
            }
            else
            {
                X =
                    new Vector2(
                        1,
                        newMaze.X[navigator.Y + 1][navigator.X]
                    );
            }
        }

        if(navigator.X + 1 < mazeSize.X)
        {
            if(navigator.Y - 1 > 0)
            {
                X =
                    new Vector2(
                        newMaze.X[navigator.X + 1][navigator.Y - 1],
                        newMaze.X[navigator.X + 1][navigator.Y]
                    );
            }
            else
            {
                X =
                    new Vector2(
                        1,
                        newMaze.X[navigator.X + 1][navigator.Y]
                    );
            }

            if(navigator.X - 1 > 0)
            {
                Y =
                    new Vector2(
                        newMaze.Y[navigator.Y][navigator.X - 1],
                        newMaze.Y[navigator.Y][navigator.X]
                    );
            }
            else
            {
                Y =
                    new Vector2(
                        1,
                        newMaze.X[navigator.X + 1][navigator.Y]
                    );
            }
        }

        if(navigator.X - 1 !< 0)
        {
            
        }

        if(navigator.Y - 1 !< 0)
        {
            
        }

        int[] nearBySlots = new int[4] { (int)X.X, (int)X.Y, (int)Y.X, (int)Y.Y };
    }
    
}