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

        //updatenavigatorplay enz action
        UpdateMazeNavigator();

        //UpdateMaze
        UpdateCurrentLayoutMaze();

        _layout.mazeView.MazeSlotLengths = CalculateMazeSlotPixelLength(
                _layout.boxes.BoxPixelLengths[MazeViewBoxNames.Maze],
                new Vector2(_layout.mazeView.CurrentMaze.UsingYToGetRightArray.Count(), _layout.mazeView.CurrentMaze.UsingXToGetRightArray.Count())
            );

        (_layout.mazeView.MazeSlotsAreToSmall, _layout.mazeView.LineThick) =
            CalculateMazeLineThick(
                _layout.mazeView.MazeSlotLengths,
                _layout.mazeView.BaseLineThick,
                _layout.mazeView.MaximumLineThickToMazeSlotPercentage
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

    private void UpdateMazeNavigator()
    {
        var _generator = _state.maze.generator;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;

        if (_generator.BuildingMazes || _play.IsAlgoRunning || _play.IsAlgoProgramPaused || !_play.IsAlgoLoaded)
        {
            return;
        }

        bool isReturnTrue = UpdateStopwatches();

        if(isReturnTrue) return;

        IsAlgoDone();
        
        UpdateListPositions();
    }

    private bool UpdateStopwatches()
    {
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;

        if(_time.MazeRunFinalPathStopWatch.MazeStopwatch >= _shareAlgoData.CommonDataHistory.MazeFinalPaths[_play.MazeListPosition].SlotFinalPathCoordinates.Length * _play.FinalMazeQueSpeed + _play.FinalPathHolePathShowTime)
        {
            _play.ShowCurrentMazeFinalPath = false;
            _play.CurrentMazeFinalPathDone = true;
        }

        if(!_play.ShowCurrentMazeFinalPath && !_time.MazeRunStopwatch.UpdateStopwatch && _play.CurrentMazeFinalPathDone)
        {
            _time.MazeRunStopwatch = 
                new MazeRunStopwatch(
                    _time.MazeRunStopwatch.StartTime,
                    _state.common.time.MazeRunStopwatch.TotalTime,
                    _time.MazeRunStopwatch.TotalPausedTime + _time.MazeRunFinalPathStopWatch.TotalTime + _time.MazeRunFinalPathStopWatch.TotalPausedTime,
                    _time.MazeRunFinalPathStopWatch.LastTimePaused,
                    _time.MazeRunStopwatch.MazeStopwatch,
                    _time.MazeRunStopwatch.PlaySpeed,
                    !_play.IsAlgoProgramPaused,
                    _time.MazeRunStopwatch.ResetStopwatch
                );

            _time.MazeRunFinalPathStopWatch = 
                new MazeRunStopwatch(
                    0,
                    0,
                    0,
                    0,
                    0,
                    _time.MazeRunStopwatch.PlaySpeed,
                    false,
                    true
                );

            return false;
        }

        if(_play.ShowCurrentMazeFinalPath)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateListPositions()
    {
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;
        
        if(_time.MazeRunStopwatch.MazeStopwatch >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory[_play.SlotHistoryListPosition + 1].SlotSearchedWithTime.Time)
        {
            for (int i = _play.SlotHistoryListPosition + 1; i < _shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count() && _time.MazeRunStopwatch.MazeStopwatch >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].SlotSearchedWithTime.Time; i++)
            {
                if(_shareAlgoData.CommonDataHistory.SlotSearchedHistory[i - 1].MazeNumber != _shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].MazeNumber || i + 1 >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count())                
                {
                    if(!_play.CurrentMazeFinalPathDone)
                    {
                        _play.ShowCurrentMazeFinalPath = true;

                        _play.SlotHistoryListPosition = i - 1;

                        double timePausedIncrement = (_time.MazeRunStopwatch.MazeStopwatch - _shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].SlotSearchedWithTime.Time) / _time.MazeRunStopwatch.PlaySpeed;

                        if(timePausedIncrement < 0) timePausedIncrement = 0;

                        _time.MazeRunStopwatch = 
                            new MazeRunStopwatch(
                                _time.MazeRunStopwatch.StartTime,
                                _state.common.time.MazeRunStopwatch.TotalTime - timePausedIncrement,
                                _time.MazeRunStopwatch.TotalPausedTime + timePausedIncrement,
                                _time.MazeRunStopwatch.LastTimePaused,
                                _shareAlgoData.CommonDataHistory.SlotSearchedHistory[i].SlotSearchedWithTime.Time,
                                _time.MazeRunStopwatch.PlaySpeed,
                                false,
                                _time.MazeRunStopwatch.ResetStopwatch
                            );
            
                        _time.MazeRunFinalPathStopWatch = 
                            new MazeRunStopwatch(
                                0,
                                0,
                                0,
                                0,
                                0,
                                _time.MazeRunStopwatch.PlaySpeed,
                                true,
                                false
                            );
                    }
                    else
                    {
                        _play.SlotHistoryListPosition = i;
                        _play.CurrentMazeFinalPathDone = false;
                        
                        if(i >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count() - 1)
                        {
                            IsAlgoDone();
                        }
                        else
                        {
                            _play.MazeListPosition += 1;
                        }
                    }

                    break;
                }
                else
                {
                    _play.SlotHistoryListPosition = i;
                }
            }
        }
    }
    
    private void UpdateCurrentLayoutMaze()
    {
        var _mazeView = _state.maze.layout.mazeView;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;

        if(!_play.IsMazeLoaded || !_play.IsAlgoLoaded)
        {
            CalculateLoadScreen();
            return;
        }

        if(_play.IsAlgoLoaded && _mazeView.CurrentLoadedLayoutMazeListPosition != _play.MazeListPosition)
        {
            _mazeView.CurrentMaze = _shareAlgoData.AllMazes[_play.MazeListPosition];
            _mazeView.CurrentLoadedLayoutMazeListPosition = _play.MazeListPosition;
        }
        else if(_mazeView.CurrentMaze.UsingXToGetRightArray.Length == 0 && _mazeView.CurrentMaze.UsingYToGetRightArray.Length == 0 && _shareAlgoData.AllMazes.Length > 0)
        {
            _mazeView.CurrentMaze = _shareAlgoData.AllMazes[0];
        }
        else if(!_state.maze.generator.BuildingMazes && _mazeView.NeedToUpdateNewToMaze)
        {
            _mazeView.CurrentMaze = _shareAlgoData.AllMazes[0];

            _mazeView.NeedToUpdateNewToMaze = false;
        }
    }

    private void CalculateLoadScreen()
    {
        var _generator = _state.maze.generator;
        var _mazeView = _state.maze.layout.mazeView;
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;

        if(_play.IsAlgoRunning)
        {
            
        }
        
        if(_generator.BuildingMazes)
        {
            //BuildingMazes Screen
        }
        else
        {
            //BuildMaze Screen
        }
    }

    private void IsAlgoDone()
    {
        var _play = _state.playMaze;
        var _shareAlgoData = _state.shareAlgoData;
        var _time = _state.common.time;

        if (_play.SlotHistoryListPosition + 1 >= _shareAlgoData.CommonDataHistory.SlotSearchedHistory.Count())
        {
            _play.ShowCurrentMazeFinalPath = false;
            _play.CurrentMazeFinalPathDone = false;
            
            _play.MazeListPosition = 0;
    
            _play.SlotHistoryListPosition = 0;
    
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
    
            _time.MazeRunFinalPathStopWatch =
                new MazeRunStopwatch(
                    0,
                    0,
                    0,
                    0,
                    0,
                    _time.MazeRunStopwatch.PlaySpeed,
                    false,
                    true
                );
        }
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

    private (bool, float) CalculateMazeLineThick(Vector2 mazeSlotLengths, float baseLineThick, float maximumLineThickToMazeSlotPercentage)
    {
        float smallestSlotLength = (mazeSlotLengths.X < mazeSlotLengths.Y) ? mazeSlotLengths.X : mazeSlotLengths.Y;

        float maxLineThick = (maximumLineThickToMazeSlotPercentage/100f) * smallestSlotLength;
        
        if(baseLineThick > maxLineThick)
        {
            bool isMaxLineToSmall = false;

            if(maxLineThick < 1f)
            {
                if(smallestSlotLength < 3)
                {
                    isMaxLineToSmall = true;
                }
                else
                {
                    return (isMaxLineToSmall, 1);
                }
            }
            
            return (isMaxLineToSmall, maxLineThick);
        }

        return (false, baseLineThick);
    }

    private (double, double, double) UpdateUsagePercentage(MazeRunStopwatch currentMazeRunStopWatch, List<CommonUsageDataHistory> commonUsageDataHistory, int commonUsageDataListPosition, bool isAlgoLoaded)
    {
        if(!isAlgoLoaded || commonUsageDataHistory.Count() == 0) return (0, 0, 0);

        for (int i = commonUsageDataListPosition; i < commonUsageDataHistory.Count(); i++)
        {
            if (currentMazeRunStopWatch.MazeStopwatch <= commonUsageDataHistory[i].StopWatchTime.X)
            {   
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
        currentDataInformation[CommonDataNames.Time] = $"{mazeRunStopwatch.MazeStopwatch:F6}";

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
}

