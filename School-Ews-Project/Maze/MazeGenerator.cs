using System.Numerics;

namespace Program;


public class MazeGenerator
{
    private readonly AppState _state;
    private readonly GenerateMazeSeed _seed;

    public MazeGenerator(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _seed = new GenerateMazeSeed(_state);
    }

    public void Generator() // 0 Up 1 Right 2 Down 3 Left
    {
        var _generator = _state.maze.generator;

        _generator.AllMazes = new Maze[_generator.MazeBachSize];
        _generator.ReadingAllMazes = false;

        Task.Run(() =>
        {
            _seed.Generate();

            int mazeLength = _generator.AllMazes.Length;
            int seedBachLength = _generator.MazeSeedBach.Length;

            Parallel.For(0, mazeLength, (i, loop) =>
            {
                if (i == seedBachLength)
                {
                    loop.Break();
                }

                _generator.AllMazes[i] =
                    MakeMaze(
                        _generator.MazeSize,
                        _generator.MazeSeedBach[i]
                    );     
            });

            _generator.ReadingAllMazes = true;
        });
    }

    private Maze MakeMaze( Vector2 mazeSize, int[] seed)
    {
        Maze newMaze;

        newMaze.UsingXToGetRightArray = FillArray((int)mazeSize.X, (int)mazeSize.Y);
        newMaze.UsingYToGetRightArray = FillArray((int)mazeSize.Y, (int)mazeSize.X);

        newMaze = MazeMaker(newMaze, mazeSize, seed);

        return newMaze;
    }

    private WallState[][] FillArray(int arrayLength, int secondArrayLength)
    {
        WallState[][] newArray = new WallState[arrayLength][];

        for (int i = 0; i < arrayLength; i++)
        {
            newArray[i] = new WallState[secondArrayLength];

            Array.Fill<WallState>(newArray[i], WallState.Closed);
        }

        return newArray;
    }

    private Maze MazeMaker(Maze newMaze, Vector2 mazeSize, int[] seed)
    {
        int seedPosition = 0;
        bool lostNavigator = false;

        MazeNavigator navigator;
        navigator.X = 1;
        navigator.Y = 1;

        for (int i = 0; i < (int)mazeSize.X * (int)mazeSize.Y; i++)
        {
            if (navigator.X > mazeSize.X - 1 || navigator.Y > mazeSize.Y - 1)
            {
                break;
            }
            else if (seed.Length - 1 < seedPosition)
            {
                seedPosition = 0;
            }

            lostNavigator = false;

            bool[] currentNavigatorSlotOptions = UpdateNearBySlotsState(navigator, newMaze, mazeSize, lostNavigator);

            if(Array.IndexOf(currentNavigatorSlotOptions, true) == -1)
            {
                MazeNavigator oldNavigator;
                oldNavigator.X = navigator.X;
                oldNavigator.Y = navigator.Y;

                navigator = HelpLostNavigatorSlot(navigator, newMaze, seed, mazeSize, seedPosition);

                if(oldNavigator.X == navigator.X && oldNavigator.Y == navigator.Y)
                {
                    break;
                }
                else
                {
                    lostNavigator = true;
                    currentNavigatorSlotOptions = UpdateNearBySlotsState(navigator, newMaze, mazeSize, lostNavigator);
                }
            }

            (navigator, newMaze) = UpdateNavigator(navigator, newMaze, currentNavigatorSlotOptions, seed, seedPosition);

            seedPosition++;
        }

        return newMaze;
    }

    private (MazeNavigator, Maze) UpdateNavigator(MazeNavigator navigator, Maze newMaze, bool[] currentNavigatorSlotOptions, int[] seed, int seedPosition)
    {
        int currentSeedNumber = seed[seedPosition];

        int index = FindIndex(currentNavigatorSlotOptions, currentSeedNumber);

        switch(index)
        {
            case (int)Directions.Up: newMaze.UsingXToGetRightArray[navigator.X][navigator.Y - 1] = WallState.Open;
                navigator.Y -= 1; break;
            case (int)Directions.Right: newMaze.UsingYToGetRightArray[navigator.Y][navigator.X] = WallState.Open;
                navigator.X += 1; break;
            case (int)Directions.Down: newMaze.UsingXToGetRightArray[navigator.X][navigator.Y] = WallState.Open;
                navigator.Y += 1; break;
            case (int)Directions.Left: newMaze.UsingYToGetRightArray[navigator.Y][navigator.X - 1] = WallState.Open;
                navigator.X -= 1; break;
        }

        return (navigator, newMaze);
    }

    private MazeNavigator HelpLostNavigatorSlot(MazeNavigator navigator, Maze newMaze, int[] seed, Vector2 mazeSize, int seedPosition)
    {
        List<int> historyIncrement = new List<int>();
        bool[] currentNavigatorSlotOptions = new bool[4];

        bool searchDistantCorners = false;
        bool currentIncrementIsX = true;

        int EndForLoop = (int)mazeSize.X * (int)mazeSize.Y;
        int currentIncrement = 0;
        int varyingIncrement = 0;
        int index = -1;

        for (int i = 0; i < EndForLoop; i++)
        {
            currentIncrement += 1;
            historyIncrement.Add(currentIncrement);

            searchDistantCorners = false;

            (currentNavigatorSlotOptions, currentIncrementIsX , varyingIncrement) = SearchDistantSlots(navigator, newMaze, mazeSize, historyIncrement, searchDistantCorners, currentIncrement);
            index = Array.IndexOf(currentNavigatorSlotOptions, true);

            if(index != -1)
            {
                break;
            }
            
            searchDistantCorners = true;

            (currentNavigatorSlotOptions, currentIncrementIsX , varyingIncrement) = SearchDistantSlots(navigator, newMaze, mazeSize, historyIncrement, searchDistantCorners, currentIncrement);
            index = Array.IndexOf(currentNavigatorSlotOptions, true);

            if(index != -1)
            {
                break;
            }
        }

        int currentSeedNumber = seed[seedPosition];

        if(index != -1)
        {
            index = FindIndex(currentNavigatorSlotOptions, currentSeedNumber);

            navigator = FindRightLostNavigatorInstructions(navigator, searchDistantCorners, currentIncrementIsX, index, currentIncrement, varyingIncrement);
        }

        return navigator;
    }

    private MazeNavigator FindRightLostNavigatorInstructions(MazeNavigator navigator, bool searchDistantCorners, bool currentIncrementIsX, int index, int currentIncrement, int varyingIncrement)
    {
        if(!searchDistantCorners)
        {
            switch(index)
            {
                case (int)Directions.Up:    navigator.Y -= currentIncrement; break;
                case (int)Directions.Right: navigator.X += currentIncrement; break;
                case (int)Directions.Down:  navigator.Y += currentIncrement; break;
                case (int)Directions.Left:  navigator.X -= currentIncrement; break;
            }
        }
        else if(searchDistantCorners)
        {
            int incrementX;
            int incrementY;

            if(currentIncrementIsX)
            {
                incrementX = currentIncrement;
                incrementY = varyingIncrement;
            }
            else
            {
                incrementX = varyingIncrement;
                incrementY = currentIncrement;
            }

            switch(index)
            {
                case (int)Directions.Up:    navigator.X += incrementX; navigator.Y -= incrementY; break;
                case (int)Directions.Right: navigator.X += incrementX; navigator.Y += incrementY; break;
                case (int)Directions.Down:  navigator.X -= incrementX; navigator.Y += incrementY; break;
                case (int)Directions.Left:  navigator.X -= incrementX; navigator.Y -= incrementY; break;
            }
        }

        return navigator;
    }

    private (bool[], bool, int) SearchDistantSlots(MazeNavigator navigator, Maze newMaze, Vector2 mazeSize, List<int> historyIncrement, bool searchDistantCorners, int currentIncrement)
    {
        bool[] currentNavigatorSlotOptions = new bool[4];

        int varyingIncrement = 0;
        bool currentIncrementIsX = true;

        if(!searchDistantCorners)
        {
            currentNavigatorSlotOptions = SearchDistantLinearBoxes(navigator, newMaze, mazeSize, currentIncrement);
        }
        else
        {
            for (int i = 0; i < historyIncrement.Count; i++)
            {
                varyingIncrement = historyIncrement[i];

                currentNavigatorSlotOptions = SearchDistanceCorners(navigator, newMaze, mazeSize, currentIncrement, varyingIncrement);

                if(currentNavigatorSlotOptions.Contains(true))
                {
                    currentIncrementIsX = true;
                    break;
                }

                currentNavigatorSlotOptions = SearchDistanceCorners(navigator, newMaze, mazeSize, varyingIncrement, currentIncrement);

                if(currentNavigatorSlotOptions.Contains(true))
                {
                    currentIncrementIsX = false;
                    break;
                }
            }
        }

        return (currentNavigatorSlotOptions, currentIncrementIsX ,varyingIncrement);
    }

    private bool[] SearchDistantLinearBoxes(MazeNavigator navigator, Maze newMaze, Vector2 mazeSize, int currentIncrement)
    {
        WallState[][] allNearWallStates= new WallState[4][];
        bool[][] checkNearSlotStates = new bool[4][];
        bool[] currentNavigatorSlotOptions = new bool[4];

        MazeNavigator checkNavigator;

        if(navigator.Y - currentIncrement >= 0)
        {
            allNearWallStates[(int)Directions.Up] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X, navigator.Y - currentIncrement);

            checkNavigator.X = navigator.X;
            checkNavigator.Y = navigator.Y - currentIncrement;

            checkNearSlotStates[(int)Directions.Up] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Up] =
                    new WallState[]{
                        WallState.OutsideMaze,
                        WallState.OutsideMaze,
                        WallState.OutsideMaze,
                        WallState.OutsideMaze
                    };
        }

        if(navigator.X + currentIncrement < mazeSize.X)
        {
            allNearWallStates[(int)Directions.Right] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X + currentIncrement, navigator.Y);

            checkNavigator.X = navigator.X + currentIncrement;
            checkNavigator.Y = navigator.Y;

            checkNearSlotStates[(int)Directions.Right] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Right] =
                    new WallState[]{
                        WallState.OutsideMaze,
                        WallState.OutsideMaze,
                        WallState.OutsideMaze,
                        WallState.OutsideMaze
                    };
        }

        if(navigator.Y + currentIncrement < mazeSize.Y)
        {
            allNearWallStates[(int)Directions.Down] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X, navigator.Y + currentIncrement);

            checkNavigator.X = navigator.X;
            checkNavigator.Y = navigator.Y + currentIncrement;

            checkNearSlotStates[(int)Directions.Down] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Down] =
                    new WallState[]{
                        WallState.OutsideMaze,
                        WallState.OutsideMaze,
                        WallState.OutsideMaze,
                        WallState.OutsideMaze
                    };
        }

        if(navigator.X - currentIncrement >= 0)
        {
            allNearWallStates[(int)Directions.Left] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X - currentIncrement, navigator.Y);

            checkNavigator.X = navigator.X - currentIncrement;
            checkNavigator.Y = navigator.Y;

            checkNearSlotStates[(int)Directions.Left] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Left] =
                new WallState[]{
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze
                };
        }

        for (int i = 0; i < currentNavigatorSlotOptions.Length; i++)
        {
            if(!allNearWallStates[i].Contains(WallState.Open) && !allNearWallStates[i].Contains(WallState.OutsideMaze) && checkNearSlotStates[i].Contains(true))
            {
                currentNavigatorSlotOptions[i] = true;
            }
            else
            {
                currentNavigatorSlotOptions[i] = false;
            }
        }

        return currentNavigatorSlotOptions;
    }

    private bool[] SearchDistanceCorners(MazeNavigator navigator, Maze newMaze,Vector2 mazeSize, int incrementX, int incrementY)
    {
        WallState[][] allNearWallStates = new WallState[4][];
        bool[] currentNavigatorSlotOptions = new bool[4];
        bool[][] checkNearSlotStates = new bool[4][];

        MazeNavigator checkNavigator;

        if(navigator.Y - incrementY >= 0 && navigator.X + incrementX < mazeSize.X)
        {
            allNearWallStates[(int)Directions.Up] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X + incrementX, navigator.Y - incrementY);

            checkNavigator.X = navigator.X + incrementX;
            checkNavigator.Y = navigator.Y - incrementY;

            checkNearSlotStates[(int)Directions.Up] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Up] =
                new WallState[]{
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze
                };
        }

        if(navigator.X + incrementX < mazeSize.X && navigator.Y + incrementY < mazeSize.Y)
        {
            allNearWallStates[(int)Directions.Right] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X + incrementX, navigator.Y + incrementY);

            checkNavigator.X = navigator.X + incrementX;
            checkNavigator.Y = navigator.Y + incrementY;

            checkNearSlotStates[(int)Directions.Right] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Right] =
                new WallState[]{
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze
                };
        }

        if(navigator.Y + incrementY < mazeSize.Y && navigator.X - incrementX >= 0)
        {
            allNearWallStates[(int)Directions.Down] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X - incrementX, navigator.Y + incrementY);

            checkNavigator.X = navigator.X - incrementX;
            checkNavigator.Y = navigator.Y + incrementY;

            checkNearSlotStates[(int)Directions.Down] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Down] =
                new WallState[]{
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze
                };
        }

        if (navigator.X - incrementX >= 0 && navigator.Y - incrementY >= 0)
        {
            allNearWallStates[(int)Directions.Left] = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X - incrementX, navigator.Y - incrementY);

            checkNavigator.X = navigator.X - incrementX;
            checkNavigator.Y = navigator.Y - incrementY;

            checkNearSlotStates[(int)Directions.Left] = UpdateNearBySlotsState(checkNavigator, newMaze, mazeSize, true);
        }
        else
        {
            allNearWallStates[(int)Directions.Left] =
                new WallState[]{
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze,
                    WallState.OutsideMaze
                };
        }

        for (int i = 0; i < currentNavigatorSlotOptions.Length; i++)
        {
            if(!allNearWallStates[i].Contains(WallState.Open) && !allNearWallStates[i].Contains(WallState.OutsideMaze) && checkNearSlotStates[i].Contains(true))
            {
                currentNavigatorSlotOptions[i] = true;
            }
            else
            {
                currentNavigatorSlotOptions[i] = false;
            }
        }

        return currentNavigatorSlotOptions;
    }

    private bool[] UpdateNearBySlotsState(MazeNavigator navigator, Maze newMaze, Vector2 mazeSize, bool lostNavigator)// ik ga er nu van uit dat als een slot een 0 heeft dat je hem niet meer mag enteren
    {
        WallState[] givenSlotState = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X, navigator.Y);
        WallState[][] allNearSlotStates = new WallState[4][];
        bool[] currentNavigatorSlotOptions = new bool[4];

        for (int i = 0; i < givenSlotState.Length; i++)
        {
            if(givenSlotState[i] == WallState.Closed)
            {
                currentNavigatorSlotOptions[i] = true;
            }
            else
            {
                currentNavigatorSlotOptions[i] = false;
            }
        }

        if(navigator.Y - 1 >= 0)
        {
            givenSlotState = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X, navigator.Y - 1);

            allNearSlotStates[(int)Directions.Up] = givenSlotState;
        }
        else
        {
            allNearSlotStates[(int)Directions.Up] = new WallState[]{WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze};
        }

        if(navigator.X + 1 < mazeSize.X)
        {
            givenSlotState = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X + 1, navigator.Y);

            allNearSlotStates[(int)Directions.Right] = givenSlotState;
        }
        else
        {
            allNearSlotStates[(int)Directions.Right] = new WallState[]{WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze};
        }

        if(navigator.Y + 1 < mazeSize.Y)
        {
            givenSlotState = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X, navigator.Y + 1);

            allNearSlotStates[(int)Directions.Down] = givenSlotState;
        }
        else
        {
            allNearSlotStates[(int)Directions.Down] = new WallState[]{WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze};
        }

        if(navigator.X - 1 >= 0)
        {
            givenSlotState = CheckGivenSlotWalls(newMaze, mazeSize, navigator.X - 1, navigator.Y);

            allNearSlotStates[(int)Directions.Left] = givenSlotState;
        }
        else
        {
            allNearSlotStates[(int)Directions.Left] = new WallState[]{WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze, WallState.OutsideMaze};
        }

        for (int i = 0; i < currentNavigatorSlotOptions.Length; i++)
        {
                if(!allNearSlotStates[i].Contains(WallState.OutsideMaze) && !allNearSlotStates[i].Contains(WallState.Open) && currentNavigatorSlotOptions[i] == true && !lostNavigator || !allNearSlotStates[i].Contains(WallState.OutsideMaze) && lostNavigator && currentNavigatorSlotOptions[i] == true && allNearSlotStates[i].Contains(WallState.Open))
            {
                currentNavigatorSlotOptions[i] = true;
            }
            else
            {
                currentNavigatorSlotOptions[i] = false;
            }
        }

        return currentNavigatorSlotOptions;
    }

    private WallState[] CheckGivenSlotWalls(Maze newMaze, Vector2 mazeSize, int newSlotNavigatorX, int newSlotNavigatorY)
    {
        WallStates horizontal = new WallStates();
        WallStates vertical = new WallStates();

        horizontal = CheckWall(newMaze.UsingXToGetRightArray, newSlotNavigatorX, newSlotNavigatorY);
        vertical = CheckWall(newMaze.UsingYToGetRightArray, newSlotNavigatorY, newSlotNavigatorX);

        return new WallState[4] { horizontal.X, vertical.Y, horizontal.Y, vertical.X };
    }

    private WallStates CheckWall(WallState[][] WallStates, int arrayNavigator, int newSlotNavigator)
    {
        WallStates newWallState = new WallStates();

        if(newSlotNavigator - 1 >= 0)
        {
            if(newSlotNavigator < WallStates[arrayNavigator].Length)
            {
                newWallState.X = WallStates[arrayNavigator][newSlotNavigator - 1];
                newWallState.Y = WallStates[arrayNavigator][newSlotNavigator];
            }
            else
            {
                newWallState.X = WallStates[arrayNavigator][newSlotNavigator - 1];
                newWallState.Y = WallState.OutsideMaze;
            }
        }
        else if(arrayNavigator - 1 >= 0)
        {
            newWallState.X = WallState.OutsideMaze;
            newWallState.Y = WallStates[arrayNavigator][newSlotNavigator];
        }

        return newWallState;
    }

    private int FindIndex(bool[] array, int currentSeedNumber)
    {
        int index = 0;
        int counter = 0;

        for (int i = 0; i < array.Length; i++)
        {
            bool result = array[i];

            if (result == true)
            {
                if (counter == currentSeedNumber)
                {
                    index = i;
                    break;
                }   

                counter += 1;
            }

            if (i == array.Length - 1)
            {
                index = Array.LastIndexOf(array, true);
            }
        }

        return index;
    }
}
