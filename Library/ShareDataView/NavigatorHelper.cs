
namespace ShareData;

public static class NavigatorHelper
{
    //Je kan Doel einde van de maze krijgen 
    //Nieuwe positie aanvragen als legaal door
    //Je omgeving data krijgen 

    public static void GiveCurrentMazeFinalPath(MazeFinalPath finalPath)
    {
        App.commonData.MazeFinalPaths.Add(finalPath);

        AlgoSandbox.UpdateRunningMazeVariables();
    }
    
    public static bool IsCurrentMazeSolved()
    {
        if(App.runningMaze.CurrentMazeEndPoint.X == App.runningMaze.CurrentNavigatorCoordinates.X && App.runningMaze.CurrentMazeEndPoint.Y == App.runningMaze.CurrentNavigatorCoordinates.Y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static IVector2 CurrentMazeEndPoint()
    {
        return App.runningMaze.CurrentMazeEndPoint;
    }

    public static void RequestNewNavigatorPosition(Directions requestedDirection)
    {
        if(CheckGivenSlotWalls(App.runningMaze.CurrentMaze, App.runningMaze.CurrentNavigatorCoordinates)[(int)requestedDirection] == WallState.Open)
        {
            switch(requestedDirection)
            {
                case Directions.Up: 
                AddNewSearchedSlot(App.runningMaze.CurrentNavigatorCoordinates, App.runningMaze.CurrentMazeNumber);
                App.runningMaze.CurrentNavigatorCoordinates =
                    new IVector2(
                        App.runningMaze.CurrentNavigatorCoordinates.X, 
                        App.runningMaze.CurrentNavigatorCoordinates.Y - 1
                    ); break;

                case Directions.Right: 
                AddNewSearchedSlot(App.runningMaze.CurrentNavigatorCoordinates, App.runningMaze.CurrentMazeNumber);
                App.runningMaze.CurrentNavigatorCoordinates = 
                    new IVector2(
                        App.runningMaze.CurrentNavigatorCoordinates.X + 1, 
                        App.runningMaze.CurrentNavigatorCoordinates.Y
                    ) ; break; 

                case Directions.Down: 
                AddNewSearchedSlot(App.runningMaze.CurrentNavigatorCoordinates, App.runningMaze.CurrentMazeNumber);
                App.runningMaze.CurrentNavigatorCoordinates =
                    new IVector2(
                        App.runningMaze.CurrentNavigatorCoordinates.X, 
                        App.runningMaze.CurrentNavigatorCoordinates.Y + 1
                    ); break;

                case Directions.Left: 
                AddNewSearchedSlot(App.runningMaze.CurrentNavigatorCoordinates, App.runningMaze.CurrentMazeNumber);
                App.runningMaze.CurrentNavigatorCoordinates =
                    new IVector2(
                        App.runningMaze.CurrentNavigatorCoordinates.X - 1, 
                        App.runningMaze.CurrentNavigatorCoordinates.Y
                    ); break;
            }
        }
    }
    
    public static WallState[] GetSlotWallsData()
    {
        return CheckGivenSlotWalls(App.runningMaze.CurrentMaze, App.runningMaze.CurrentNavigatorCoordinates);
    }

    private static WallState[] CheckGivenSlotWalls(Maze currentMaze, IVector2 currentNavigatorCoordinates)
    {
        WallStates horizontal = new WallStates();
        WallStates vertical = new WallStates();

        horizontal = CheckWall(
            currentMaze.UsingXToGetRightArray, 
            currentNavigatorCoordinates.X, 
            currentNavigatorCoordinates.Y
        );

        vertical = CheckWall(
            currentMaze.UsingYToGetRightArray, 
            currentNavigatorCoordinates.Y, 
            currentNavigatorCoordinates.X
        );

        return new WallState[4] { horizontal.X, vertical.Y, horizontal.Y, vertical.X };
    }

    private static WallStates CheckWall(WallState[][] WallStates, int arrayNavigator, int newSlotNavigator)
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

    private static void AddNewSearchedSlot(IVector2 newDiscovertSlotCoordinates, int currentMazeNumber)
    {
        SlotDataVariables slotData =
            new SlotDataVariables(
                new SlotSearchedWithTime(
                        App.commonData.ProjectStopWatch.Elapsed.TotalSeconds,
                        newDiscovertSlotCoordinates
                    ),
                false,
                currentMazeNumber
            );
        
        if(App.commonData.SlotSearchedHistory.Any(maze => maze.SlotSearchedWithTime.SlotCoordinates.X == slotData.SlotSearchedWithTime.SlotCoordinates.X && maze.SlotSearchedWithTime.SlotCoordinates.Y == slotData.SlotSearchedWithTime.SlotCoordinates.Y && maze.MazeNumber == slotData.MazeNumber))
        {
            slotData.RepeatedSlotSearched = true;

            App.commonData.SlotSearchedHistory.Add(slotData);
        }
        else
        {
            App.commonData.SlotSearchedHistory.Add(slotData);
        }
    }
}