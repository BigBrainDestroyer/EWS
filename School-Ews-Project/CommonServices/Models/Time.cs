using Raylib_cs;
using System.Diagnostics;

namespace Program;

public struct KeyboardKeyTimeDown
{
    public double StartDownTime;
    public double TotalDownTime;

    public KeyboardKeyTimeDown(double startDownTime, double totalDownTime)
    {
        StartDownTime = startDownTime;
        TotalDownTime = totalDownTime;
    }
}

public struct FileChangeTimer
{
    public double StartTime;
    public double TotalTime;
    public double ReactionTime;
    public bool FileHasChanged;

    public FileChangeTimer(double startTime, double totalTime, double reactionTime, bool fileHasChanged)
    {
        StartTime = startTime;
        TotalTime = totalTime;
        ReactionTime = reactionTime;
        FileHasChanged = fileHasChanged;
    }
}

public struct MazeRunStopwatch
{
    public double StartTime;
    public double TotalTime;
    public double TotalPausedTime;
    public double LastTimePaused;
    public double MazeStopwatch;
    public double PlaySpeed;
    public bool UpdateStopwatch;
    public bool ResetStopwatch;

    public MazeRunStopwatch(double startTime, double totalTime, double totalPausedTime, double lastTimePaused, double mazeStopWatch, double playSpeed, bool updateStopWatch, bool resetStopwatch)
    {
        StartTime = startTime;
        TotalTime = totalTime;
        TotalPausedTime = totalPausedTime;
        LastTimePaused = lastTimePaused;
        MazeStopwatch = mazeStopWatch;
        PlaySpeed = playSpeed;
        UpdateStopwatch = updateStopWatch;
        ResetStopwatch = resetStopwatch;
    }
}

public class TimeState
{
    public Stopwatch Stopwatch = new Stopwatch();

    public Dictionary<KeyboardKey, KeyboardKeyTimeDown> KeyboardKeyTimeDown =
        new Dictionary<KeyboardKey, KeyboardKeyTimeDown>{
            {KeyboardKey.Backspace, new KeyboardKeyTimeDown()},
            {KeyboardKey.Enter, new KeyboardKeyTimeDown()},
            {KeyboardKey.Up, new KeyboardKeyTimeDown()},
            {KeyboardKey.Right, new KeyboardKeyTimeDown()},
            {KeyboardKey.Down, new KeyboardKeyTimeDown()},
            {KeyboardKey.Left, new KeyboardKeyTimeDown()}
        };
    
    public FileChangeTimer FileChangeTimer { get; set; } = new FileChangeTimer(0, 0, 0.65, false);

    public MazeRunStopwatch MazeRunStopwatch { get; set; } = new MazeRunStopwatch(0, 0, 0, 0, 0, 0, false, false);
}