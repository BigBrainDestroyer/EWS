using ShareData;
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

        UpdateCurrentMazeLayout();

        _layout.mazeView.MazeSlotLengths = CalculateMazeSlotPixelLength(
                _layout.boxes.BoxPixelLengths[MazeViewBoxNames.Maze],
                _generator.MazeSize
            );

        (_layout.mazeView.CommonDataSingleBoxLengths, _layout.mazeView.AmountOfCommonDataBoxes) =
            CalculateCommonDataSingleBoxLengths(_layout.mazeView.CommonDataBoxState, _layout.boxes.BoxPixelLengths[MazeViewBoxNames.CommonData]);

        _layout.mazeView.CommonDataBoxPixelSpacing =
                CalculatingCommonDataBoxLengthsPixelSpacing(_layout.boxes.BoxPixelLengths[MazeViewBoxNames.CommonData], _layout.mazeView.CommonDataBoxPercentageSpacing);


        _layout.mazeView.CommonDataCharHeight =
            CalculatingCommonDataCharHeight(_layout.mazeView.CommonDataSingleBoxLengths, _layout.mazeView.CommonDataBoxPercentageSpacing, _layout.mazeView.AmountOfCommonDataBoxes);
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

    private (Vector2, int) CalculateCommonDataSingleBoxLengths(Dictionary<CommonDataNames, bool> commonDataState, Vector2 parentBoxLengths)
    {
        CommonDataNames[] boxNames = Enum.GetValues<CommonDataNames>();
        int amountOfCommonDataBoxes = 0;

        foreach (CommonDataNames boxName in boxNames)
        {
            if (commonDataState[boxName] == true)
            {
                amountOfCommonDataBoxes += 1;
            }
        }

        Vector2 newBoxLengths =
            new Vector2(
                parentBoxLengths.X,
                parentBoxLengths.Y / amountOfCommonDataBoxes
            );

        return (newBoxLengths, amountOfCommonDataBoxes);
    }

    private Vector2 CalculatingCommonDataBoxLengthsPixelSpacing(Vector2 commonDataSingleBoxLengths, Vector2 commonDataSingleBoxPercentageSpacing)
    {
        Vector2 newCommonDataBoxPixelSpacing =
            new Vector2(
                (commonDataSingleBoxPercentageSpacing.X / 100f) * commonDataSingleBoxLengths.X,
                (commonDataSingleBoxPercentageSpacing.X / 100f) * commonDataSingleBoxLengths.Y
            );

        return newCommonDataBoxPixelSpacing;
    }

    private float CalculatingCommonDataCharHeight(Vector2 commonDataSingleBoxLengths, Vector2 CommonDataBoxPixelSpacing, int amountOfCommonDataBoxes)
    {
        float newCharHeight = (commonDataSingleBoxLengths.Y - CommonDataBoxPixelSpacing.Y) / 2;

        return newCharHeight;
    }

    private void UpdateCurrentMazeLayout()
    {
        var _generator = _state.maze.generator;
        var _layout = _state.maze.layout;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;

        if (_generator.BuildingMazes || _play.IsAlgoRunning || _play.IsAlgoProgramPauzed || !_play.IsAlgoLoaded)
        {
            return;
        }

        if(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.X == _shareAlgoData.MazeEndPoints[_play.SlotHistoryListPosition].X && _shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.Y == _shareAlgoData.MazeEndPoints[_play.SlotHistoryListPosition].Y)
        {
            NextMaze();
        }

        _play.SlotHistoryListPosition += 1;
    }

    private void NextMaze()
    {
        var _layout = _state.maze.layout;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;

        _play.MazeListPosition += 1;

        _layout.mazeView.CurrentMaze = UpdateCurrentMaze(_shareAlgoData.AllMazes, _play.MazeListPosition);
    }
    
    private Maze UpdateCurrentMaze(Maze[] allMazes, int mazeListPosition)
    {

        Maze newCurrentMaze = allMazes[mazeListPosition];

        return newCurrentMaze;
    }
}