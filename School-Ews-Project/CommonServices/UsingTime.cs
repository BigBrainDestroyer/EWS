using Raylib_cs;
using System.Diagnostics;

namespace Program;

public static class CalculateUsingTime
{
    public static Dictionary<KeyboardKey, KeyboardKeyTimeDown> UpdateKeyboardKeyTimeDown(Dictionary<KeyboardKey, KeyboardKeyTimeDown> currentKeyboardKeyTimeDown, Stopwatch currentStopwatch)
    {
        foreach(KeyboardKey key in currentKeyboardKeyTimeDown.Keys)
        {
            if(Raylib.IsKeyDown(key))
            {
                if(currentKeyboardKeyTimeDown[key].StartDownTime == 0)
                {
                    currentKeyboardKeyTimeDown[key] = new KeyboardKeyTimeDown(currentStopwatch.Elapsed.TotalSeconds, 0);
                }
                else
                {
                    currentKeyboardKeyTimeDown[key] = new KeyboardKeyTimeDown(currentKeyboardKeyTimeDown[key].StartDownTime, currentStopwatch.Elapsed.TotalSeconds - currentKeyboardKeyTimeDown[key].StartDownTime);
                }
            }
            else if(currentKeyboardKeyTimeDown[key].StartDownTime != 0)
            {
                currentKeyboardKeyTimeDown[key] = new KeyboardKeyTimeDown(0, 0);
            }
        }
        
        return currentKeyboardKeyTimeDown;
    }

    public static FileChangeTimer UpdateFileChangeTimer(FileChangeTimer currentFileChangeTimer, Stopwatch currentStopwatch, bool fileHasChanged)
    {
        if (currentFileChangeTimer.StartTime == 0 && fileHasChanged)
        {
            currentFileChangeTimer = 
                new FileChangeTimer(
                    currentStopwatch.Elapsed.TotalSeconds, 
                    0, 
                    currentFileChangeTimer.ReactionTime, 
                    fileHasChanged
                );
        }
        else if (currentFileChangeTimer.StartTime != 0 && fileHasChanged)
        {
            currentFileChangeTimer = 
                new FileChangeTimer(
                    currentFileChangeTimer.StartTime, 
                    currentStopwatch.Elapsed.TotalSeconds - currentFileChangeTimer.StartTime, 
                    currentFileChangeTimer.ReactionTime, 
                    fileHasChanged
                );
        }
        else if(currentFileChangeTimer.StartTime != 0)
        {
            currentFileChangeTimer = 
                new FileChangeTimer(
                    0,
                    0,
                    currentFileChangeTimer.ReactionTime, 
                    fileHasChanged
                );
        }

        return currentFileChangeTimer;
    }

    public static MazeRunStopwatch MazePlaySpeedStopWatch(Stopwatch currentStopwatch, MazeRunStopwatch currentMazeRunStopwatch, double playSpeed)
    {
        if(currentMazeRunStopwatch.ResetStopwatch)
        {
            currentMazeRunStopwatch = 
                new MazeRunStopwatch(
                    0, 
                    0, 
                    0,
                    0, 
                    playSpeed,
                    currentMazeRunStopwatch.PlaySpeed, 
                    currentMazeRunStopwatch.UpdateStopwatch, 
                    false
                );
        }

        if(currentMazeRunStopwatch.UpdateStopwatch && currentMazeRunStopwatch.StartTime == 0)
        {
            currentMazeRunStopwatch = 
                new MazeRunStopwatch(
                    currentStopwatch.Elapsed.TotalSeconds, 
                    0, 
                    0,
                    0,
                    0,
                    playSpeed,
                    currentMazeRunStopwatch.UpdateStopwatch, 
                    currentMazeRunStopwatch.ResetStopwatch
                );
        }
        else if(currentMazeRunStopwatch.UpdateStopwatch)
        {
            currentMazeRunStopwatch = 
                new MazeRunStopwatch(
                    currentMazeRunStopwatch.StartTime, 
                    currentStopwatch.Elapsed.TotalSeconds - currentMazeRunStopwatch.StartTime - currentMazeRunStopwatch.TotalPausedTime, 
                    currentMazeRunStopwatch.TotalPausedTime,
                    currentMazeRunStopwatch.LastTimePaused,
                    currentMazeRunStopwatch.MazeStopwatch + ((currentStopwatch.Elapsed.TotalSeconds - currentMazeRunStopwatch.StartTime - currentMazeRunStopwatch.TotalPausedTime - currentMazeRunStopwatch.TotalTime) * playSpeed), 
                    playSpeed, 
                    currentMazeRunStopwatch.UpdateStopwatch, 
                    currentMazeRunStopwatch.ResetStopwatch
                );
        }

        return currentMazeRunStopwatch;
    }
}