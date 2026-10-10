using ShareData;

namespace Program;

public class Program()
{
    public static void Main()
    {
        ShareData.AlgoSandbox.Start();

        TestAlgo();
    }

    private static void TestAlgo()
    {
        WallState[] currentWallState = new WallState[4];

        while(true)
        {
              currentWallState = ShareData.NavigatorHelper.GetSlotWallsData();

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

            Random rndm = new Random();

            int direction = rndm.Next(0, moveList.Count);

            ShareData.NavigatorHelper.RequestNewNavigatorPosition((Directions)moveList[direction]);

            Thread.Sleep(1);

            if(ShareData.NavigatorHelper.IsCurrentMazeSolved())
            {
                Console.WriteLine("Maze Solved");

                ShareData.NavigatorHelper.GiveCurrentMazeFinalPath(new MazeFinalPath());
            }
        }
    }
}