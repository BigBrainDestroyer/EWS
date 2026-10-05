using System.Numerics;

namespace ShareData;

public static class NavigatorHelper
{
    public static WallState[] GetSlotWallDataState(Maze currentMaze, IVector2 currentNavigatorCoordinates)
    {
        AddNewSearchedSlot(currentMaze, currentNavigatorCoordinates);

        return CheckGivenSlotWalls(currentMaze, currentNavigatorCoordinates);
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
        
        if(App.commonData.SlotSearchedHistory.Contains(slotData))
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