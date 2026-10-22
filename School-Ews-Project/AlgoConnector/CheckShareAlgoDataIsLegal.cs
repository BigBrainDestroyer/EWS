using ShareData;

namespace Program;

public static class IsShareAlgoDataLegal
{
    public static (bool, bool) Check(ShareAlgoData shareAlgoData)
    {
        bool isAlgoDataLegal = true;
        bool isMazeDataLegal = true;

        if (shareAlgoData.CommonDataHistory.ProjectTotalSeconds == 0)
        {
            isAlgoDataLegal = false;
        }

        if(shareAlgoData.AllMazes.Count() < 1)
        {
            return (false, false);
        }

        if(shareAlgoData.AllMazes.Count() != shareAlgoData.MazeBeginPoints.Length || shareAlgoData.AllMazes.Count() != shareAlgoData.MazeEndPoints.Length)
        {
            return (false, false);
        }

        int mazeSize = shareAlgoData.AllMazes[0].UsingXToGetRightArray.Count();

        foreach(Maze maze in shareAlgoData.AllMazes)
        {
            if(mazeSize != maze.UsingXToGetRightArray.Count() || mazeSize != maze.UsingYToGetRightArray.Count())
            {
                return (false, false);
            }
        }

        if(shareAlgoData.CommonDataHistory.MazeFinalPaths.Any(final => final.SlotFinalPathCoordinates != null && final.SlotFinalPathCoordinates.Any(c => c.X > mazeSize - 1 && c.X < 1) && final.SlotFinalPathCoordinates.Any(c => c.Y > mazeSize - 1 && c.Y < 1)))
        {
            return (false, false);
        }

        return (isAlgoDataLegal, isMazeDataLegal);
    }
}
