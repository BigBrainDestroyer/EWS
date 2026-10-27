
namespace Program;

public class UpdateStopwatches
{
    private readonly AppState _state;

    public UpdateStopwatches(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Update()
    {
        var _input = _state.common.input;
        var _time = _state.common.time;
        var _play = _state.playMaze;
        
        _time.KeyboardKeyTimeDown = CalculateUsingTime.UpdateKeyboardKeyTimeDown(_time.KeyboardKeyTimeDown, _time.Stopwatch);

        _time.FileChangeTimer = CalculateUsingTime.UpdateFileChangeTimer(_time.FileChangeTimer, _time.Stopwatch, _time.FileChangeTimer.FileHasChanged);

        _time.MazeRunStopwatch = CalculateUsingTime.MazePlaySpeedStopWatch(_time.Stopwatch, _time.MazeRunStopwatch, _play.PlaySpeed, _play.ShowCurrentMazeFinalPath);

        _time.MazeRunFinalPathStopWatch = CalculateUsingTime.MazeFinalPathStopWatch(_time.Stopwatch, _time.MazeRunFinalPathStopWatch, _play.PlaySpeed, _play.FinalMazeQueSpeed, _play.ShowCurrentMazeFinalPath);
    }
}