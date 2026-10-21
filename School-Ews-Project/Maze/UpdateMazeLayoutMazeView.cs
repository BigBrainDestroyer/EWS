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
        var _play = _state.playMaze;
        var _time = _state.common.time;
        var _shareAlgoData = _state.shareAlgoData;

        UpdateAlgoRunData();

        _layout.mazeView.MazeSlotLengths = CalculateMazeSlotPixelLength(
                _layout.boxes.BoxPixelLengths[MazeViewBoxNames.Maze],
                new Vector2(_layout.mazeView.CurrentMaze.UsingYToGetRightArray.Count(), _layout.mazeView.CurrentMaze.UsingXToGetRightArray.Count())
            );

        (double usagePercentageCpu, double usagePercentageGpu, double usagePercentageRamMb) = 
            UpdateUsagePercentage(
                _time.MazeRunStopwatch,
                _shareAlgoData.CommonDataHistory.CommonUsageDataHistory,
                _play.CommonUsageDataListPosition,
                _play.IsAlgoLoaded
            );

        _layout.mazeView.CurrentCommonDataInformation =
            UpdateCurrentCommonDataInformation(
                _layout.mazeView.CurrentCommonDataInformation,
                _layout.mazeView.CommonDataInactiveInformation,
                _time.MazeRunStopwatch,
                usagePercentageCpu,
                usagePercentageGpu,
                usagePercentageRamMb,
                _shareAlgoData.AllMazes.Count(),
                _play.MazeListPosition,
                _play.SlotHistoryListPosition,
                _play.IsAlgoLoaded
            );

        _layout.mazeView.CommonDataBoxes =
            CalculateCommonDataBox(
                _layout.mazeView.CommonDataBoxes,
                _layout.mazeView.CurrentCommonDataInformation,
                _layout.mazeView.CommonDataBoxState, 
                _layout.boxes.BoxPixelPositions[MazeViewBoxNames.CommonData],
                _layout.boxes.BoxPixelLengths[MazeViewBoxNames.CommonData]
            );
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

    private (double, double, double) UpdateUsagePercentage(MazeRunStopwatch currentMazeRunStopWatch, List<CommonUsageDataHistory> commonUsageDataHistory, int commonUsageDataListPosition, bool isAlgoLoaded)
    {
        if(!isAlgoLoaded || commonUsageDataHistory.Count() == 0) return (0, 0, 0);

        for (int i = commonUsageDataListPosition; i < commonUsageDataHistory.Count(); i++)
        {
            if (currentMazeRunStopWatch.MazeStopwatch <= commonUsageDataHistory[i].StopWatchTime.X)
            {
                Console.WriteLine(i);
                
                commonUsageDataListPosition = i;
                break;
            }
        }

        return (
            commonUsageDataHistory[commonUsageDataListPosition].UsagePercentageCpu,
            commonUsageDataHistory[commonUsageDataListPosition].UsagePercentageGpu,
            commonUsageDataHistory[commonUsageDataListPosition].UsagePercentageRamMb
        );
    }
    
    private Dictionary<CommonDataNames, string> UpdateCurrentCommonDataInformation(Dictionary<CommonDataNames, string> currentDataInformation, Dictionary<CommonDataNames, string> commonDataInactiveInformation, MazeRunStopwatch mazeRunStopwatch, double cpuUsage, double gpuUsage, double ramUsage, int totalBatchSize, int currentMazeNumber, int totalSlotSearched, bool isAlgoLoaded)
    {
        if (!isAlgoLoaded) return commonDataInactiveInformation;

        currentDataInformation[CommonDataNames.State] = "Loaded";
        currentDataInformation[CommonDataNames.CoreProcessorUsagePercentage] = $"{cpuUsage:F3}%";
        currentDataInformation[CommonDataNames.GPU] = $"{gpuUsage:F3}%";
        currentDataInformation[CommonDataNames.RamTotalUsage] = $"{ramUsage:F3}";
        currentDataInformation[CommonDataNames.BatchState] = $"{currentMazeNumber + 1}/{totalBatchSize}";
        currentDataInformation[CommonDataNames.SlotSearched] = $"{totalSlotSearched}";
        currentDataInformation[CommonDataNames.Time] = $"{mazeRunStopwatch.MazeStopwatch:F60}";

        return currentDataInformation;
    }

    private Dictionary<CommonDataNames, InformationViewer> CalculateCommonDataBox(Dictionary<CommonDataNames, InformationViewer> currentCommonDataBoxes, Dictionary<CommonDataNames, string> currentCommonDataInformation, Dictionary<CommonDataNames, bool> commonDataState, Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths)
    {
        CommonDataNames[] boxNames = Enum.GetValues<CommonDataNames>();

        Vector2 newBoxLengths =
            new Vector2(
                parentBoxPixelLengths.X,
                parentBoxPixelLengths.Y / commonDataState.Values.Count(value => value)
            );

        float boxVerticalPositionIncrement = 0;

        foreach (CommonDataNames boxName in boxNames)
        {
            if (commonDataState[boxName] == true)
            {   
                currentCommonDataBoxes[boxName].InformationBox.Text = 
                    new RaylibText(
                        currentCommonDataInformation[boxName],
                        currentCommonDataBoxes[boxName].InformationBox.Text.TextSize,
                        currentCommonDataBoxes[boxName].InformationBox.Text.TextSpacing
                    );

                currentCommonDataBoxes[boxName] = 
                    CalculateInformationViewer.Calculate(
                        currentCommonDataBoxes[boxName], 
                        new Box(
                            new Vector2(
                                parentBoxPixelPosition.X, 
                                parentBoxPixelPosition.Y + boxVerticalPositionIncrement
                            ),
                            newBoxLengths
                        )
                    );

                boxVerticalPositionIncrement += newBoxLengths.Y;
            }
        }

        return currentCommonDataBoxes;
    }

    private void UpdateAlgoRunData()
    {
        var _generator = _state.maze.generator;
        var _play = _state.playMaze;
   
        if (_generator.BuildingMazes || _play.IsAlgoRunning || _play.IsAlgoProgramPaused || !_play.IsAlgoLoaded)
        {
            return;
        }

        UpdateCurrentMazeNavigatorSlots();

    }

    private void UpdateCurrentMazeNavigatorSlots()
    {
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;

        if(_play.SlotHistoryListPosition + 1 >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count())
        {
            _play.MazeListPosition = 0;

            _play.SlotHistoryListPosition = 0;

            NextMaze();

            _time.MazeRunStopwatch =
                new MazeRunStopwatch(
                    0,
                    0,
                    0,
                    0,
                    0,
                    _time.MazeRunStopwatch.PlaySpeed,
                    _time.MazeRunStopwatch.UpdateStopwatch,
                    true
                );
        }

        if(_play.SlotHistoryListPosition == 0)
        {
            NextMaze();
        }
        
        if(_time.MazeRunStopwatch.MazeStopwatch >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition + 1].SlotSearchedWithTime.Time)
        {
            for (int i = _play.SlotHistoryListPosition + 1; i < _shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count() && _time.MazeRunStopwatch.MazeStopwatch >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].SlotSearchedWithTime.Time; i++)
            {
                _play.SlotHistoryListPosition = i;
            }
            

            //_play.SlotHistoryListPosition += 1;

             Console.WriteLine(_time.MazeRunStopwatch.MazeStopwatch);

             
            Console.WriteLine(_shareAlgoData.MazeEndPoints.Count());
            Console.WriteLine(_play.SlotHistoryListPosition);
            Console.WriteLine(_shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count());
            Console.WriteLine(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].MazeNumber);

            Console.WriteLine();
            Console.WriteLine(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.X );
            Console.WriteLine(_shareAlgoData.MazeEndPoints[_play.MazeListPosition].X);
            Console.WriteLine(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.Y);
            Console.WriteLine(_shareAlgoData.MazeEndPoints[_play.MazeListPosition].Y);
            Console.WriteLine(_play.MazeListPosition);
            Console.WriteLine();

            
            if(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition - 1].MazeNumber != _shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].MazeNumber)//if(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.X == _shareAlgoData.MazeEndPoints[_play.MazeListPosition].X && _shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition].SlotSearchedWithTime.SlotCoordinates.Y == _shareAlgoData.MazeEndPoints[_play.MazeListPosition].Y)
            {
                 _play.MazeListPosition += 1;
                     
                 NextMaze();
            }
        }
    }

    private void NextMaze()
    {
        var _layout = _state.maze.layout;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;

        _layout.mazeView.CurrentMaze = UpdateCurrentMaze(_shareAlgoData.AllMazes, _play.MazeListPosition);
    }
    
    private Maze UpdateCurrentMaze(Maze[] allMazes, int mazeListPosition)
    {
        Maze newCurrentMaze = allMazes[mazeListPosition];

        return newCurrentMaze;
    }
}

