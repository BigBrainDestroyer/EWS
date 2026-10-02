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

                    Console.WriteLine(currentKeyboardKeyTimeDown[key].TotalDownTime);
                }
            }
            else if(currentKeyboardKeyTimeDown[key].StartDownTime != 0)
            {
                currentKeyboardKeyTimeDown[key] = new KeyboardKeyTimeDown(0, 0);
            }
        }
        
        return currentKeyboardKeyTimeDown;
    }
}