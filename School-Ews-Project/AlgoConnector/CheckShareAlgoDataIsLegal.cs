using ShareData;

namespace Program;

public static class IsShareAlgoDataLegal
{
    public static bool Check(ShareAlgoData shareAlgoData)
    {
        if(shareAlgoData.CommonDataHistory.ProjectTotalSeconds == 0)
        {
            return false;
        }

        if(shareAlgoData.AllMazes.Count() < 1)
        {
            return false;
        }

        if(shareAlgoData.AllMazes.Count() != shareAlgoData.MazeBeginPoints.Length || shareAlgoData.AllMazes.Count() != shareAlgoData.MazeEndPoints.Length)
        {
            return false;
        }

        int mazeSize = shareAlgoData.AllMazes[0].UsingXToGetRightArray.Count();

        foreach(Maze maze in shareAlgoData.AllMazes)
        {
            if(mazeSize != maze.UsingXToGetRightArray.Count() || mazeSize != maze.UsingYToGetRightArray.Count())
            {
                return false;
            }
        }

        return true;
    }
}
