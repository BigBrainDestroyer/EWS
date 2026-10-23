using ShareData;

namespace Program;

public class Program()
{
    public static void Main()
    {
        ShareData.AlgoSandbox.Start();

        TestAlgo();
    }

    public struct CrossWay
    {
        public IVector2 Coordinates { get; set; }
        public WallState[] LegalWays { get; set; }
        public int WayIn { get; set; }

        public CrossWay(IVector2 coordinates, WallState[] legalWays, int wayIn)
        {
            Coordinates = coordinates;
            LegalWays = legalWays;
            WayIn = wayIn;
        }
    }
    
    private static void TestAlgo()
    {
        WallState[] currentWallState = new WallState[4];
        List<CrossWay> crossWayList = new List<CrossWay>();
        List<IVector2> ExploredRoad = new List<IVector2>() { NavigatorHelper.GetCurrentNavigatorCoordinates() };

        IVector2 oldPath = NavigatorHelper.GetCurrentNavigatorCoordinates();

        bool followOldPath = false;
        
        while(true)
        {
            currentWallState = ShareData.NavigatorHelper.GetSlotWallsData();
            
            if(followOldPath)
            {
                if(!NavigatorHelper.GetCurrentNavigatorCoordinates().Equals(crossWayList.Last().Coordinates))
                {
                    oldPath = ExploredRoad.Last();

                    ExploredRoad.RemoveAt(ExploredRoad.Count() - 1);

                    if (ExploredRoad.Last().X == oldPath.X)
                    {
                        if (ExploredRoad.Last().Y > oldPath.Y)
                        {
                            ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Down);
                        }
                        else
                        {
                            ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Up);
                        }
                    }
                    else
                    {
                        if (ExploredRoad.Last().X > oldPath.X)
                        {
                            ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Right);
                        }
                        else
                        {
                            ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Left);
                        }
                    }
                }
                else
                {
                    int counter = 0;
                    List<int> moveList = new List<int>();

                    if(crossWayList.Last().LegalWays.Count(x => x == WallState.Open) <= 1 && !NavigatorHelper.CurrentMazeBeginPoint().Equals(NavigatorHelper.GetCurrentNavigatorCoordinates))
                    {
                        oldPath = ExploredRoad.Last();
    
                        ExploredRoad.RemoveAt(ExploredRoad.Count() - 1);
    
                        if (ExploredRoad.Last().X == oldPath.X)
                        {
                            if (ExploredRoad.Last().Y > oldPath.Y)
                            {
                                ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Down);
                            }
                            else
                            {
                                ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Up);
                            }
                        }
                        else
                        {
                            if (ExploredRoad.Last().X > oldPath.X)
                            {
                                ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Right);
                            }
                            else
                            {
                                ShareData.NavigatorHelper.RequestNewNavigatorPosition(Directions.Left);
                            }
                        }

                        crossWayList.RemoveAt(crossWayList.Count() - 1);
                    }
                    else
                    {
                        oldPath = NavigatorHelper.GetCurrentNavigatorCoordinates();

                        foreach(WallState wallState in crossWayList.Last().LegalWays)
                        {
                            if(wallState == WallState.Open)
                            {
                                moveList.Add(counter);
                            }
            
                            counter++;
                        }

                        if(moveList.First() == crossWayList.Last().WayIn)
                        {
                            ShareData.NavigatorHelper.RequestNewNavigatorPosition((Directions)moveList[1]);
                            
                            crossWayList.Last().LegalWays[moveList[1]] = WallState.Closed;
                        }
                        else
                        {
                            ShareData.NavigatorHelper.RequestNewNavigatorPosition((Directions)moveList.First());

                            crossWayList.Last().LegalWays[moveList.First()] = WallState.Closed;
                        }

                        ExploredRoad.Add(NavigatorHelper.GetCurrentNavigatorCoordinates());
    
                        followOldPath = false;
                    }
                }

                if(ShareData.NavigatorHelper.IsCurrentMazeSolved())
                {
                    Console.WriteLine("Maze Solved");
                    
                    ShareData.NavigatorHelper.GiveCurrentMazeFinalPath(ExploredRoad.ToArray());
    
                    currentWallState = new WallState[4];
                    crossWayList = new List<CrossWay>();
                    ExploredRoad = new List<IVector2>() { NavigatorHelper.GetCurrentNavigatorCoordinates() };
            
                    oldPath = NavigatorHelper.GetCurrentNavigatorCoordinates();
            
                    followOldPath = false;
                }
                
                continue;
            }

            if(currentWallState.Count(x => x == WallState.Open) > 1)
            {                
                int counter = 0;
                List<int> moveList = new List<int>();
    
                foreach(WallState wallState in currentWallState)
                {
                    if(wallState == WallState.Open)
                    {
                        moveList.Add(counter);
                    }
    
                    counter++;
                }

                int wayIn = 0;


                if (ExploredRoad.Last().X == oldPath.X)
                {
                    if (ExploredRoad.Last().Y > oldPath.Y)
                    {
                        wayIn = (int)Directions.Up;
                    }
                    else
                    {
                        wayIn = (int)Directions.Down;
                    }
                }
                else
                {
                    if (ExploredRoad.Last().X > oldPath.X)
                    {
                        wayIn = (int)Directions.Left;
                    }
                    else
                    {
                        wayIn = (int)Directions.Right;
                    }
                }

                if(NavigatorHelper.CurrentMazeBeginPoint().Equals(NavigatorHelper.GetCurrentNavigatorCoordinates))
                {
                    crossWayList.Add(
                        new CrossWay(
                            NavigatorHelper.GetCurrentNavigatorCoordinates(),
                            currentWallState,
                            -1
                        )
                    );
                }
                else
                {
                    crossWayList.Add(
                        new CrossWay(
                            NavigatorHelper.GetCurrentNavigatorCoordinates(),
                            currentWallState,
                            wayIn
                        )
                    );
                }

                oldPath = NavigatorHelper.GetCurrentNavigatorCoordinates();
                
                if(moveList.First() == crossWayList.Last().WayIn)
                {
                    currentWallState[moveList[1]] = WallState.Closed;
                    
                    ShareData.NavigatorHelper.RequestNewNavigatorPosition((Directions)moveList[1]);
                    
                    crossWayList.Last().LegalWays[moveList[1]] = WallState.Closed;
                }
                else
                {
                    currentWallState[moveList.First()] = WallState.Closed;
                    
                    ShareData.NavigatorHelper.RequestNewNavigatorPosition((Directions)moveList.First());

                    crossWayList.Last().LegalWays[moveList.First()] = WallState.Closed;
                }
                
                ExploredRoad.Add(NavigatorHelper.GetCurrentNavigatorCoordinates());
            }
            else
            { 
                int counter = 0;
                List<int> moveList = new List<int>();
    
                foreach(WallState wallState in currentWallState)
                {
                    if(wallState == WallState.Open)
                    {
                        moveList.Add(counter);
                    }
    
                    counter++;
                }
                
                ShareData.NavigatorHelper.RequestNewNavigatorPosition((Directions)moveList.First());

                if(oldPath.Equals(NavigatorHelper.GetCurrentNavigatorCoordinates()))
                {
                    followOldPath = true;

                    ExploredRoad.RemoveAt(ExploredRoad.Count() - 1);
                }
                else
                {
                    oldPath = ExploredRoad.Last();
                    
                    ExploredRoad.Add(NavigatorHelper.GetCurrentNavigatorCoordinates());
                }
            }

            if(ShareData.NavigatorHelper.IsCurrentMazeSolved())
            {
                Console.WriteLine("Maze Solved");
                
                ShareData.NavigatorHelper.GiveCurrentMazeFinalPath(ExploredRoad.ToArray());

                currentWallState = new WallState[4];
                crossWayList = new List<CrossWay>();
                ExploredRoad = new List<IVector2>() { NavigatorHelper.GetCurrentNavigatorCoordinates() };
        
                oldPath = NavigatorHelper.GetCurrentNavigatorCoordinates();
        
                followOldPath = false;
            }
        }
    }
}