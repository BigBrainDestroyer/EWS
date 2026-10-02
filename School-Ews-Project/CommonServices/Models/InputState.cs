using Raylib_cs;

namespace Program;

public enum KeyboardKeyNames
{ 
    Backspace,
    Enter,
    Up,
    Right,
    Down,
    Left
}

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

public class InputState
{
    public List<int> KeyboardCharInput { get; set; } = new List<int>();
    public float MouseWheelInput { get; set; } = 0;

    public Dictionary<KeyboardKey, KeyboardKeyTimeDown> KeyboardKeyTimeDown =
        new Dictionary<KeyboardKey, KeyboardKeyTimeDown>{
            {KeyboardKey.Backspace, new KeyboardKeyTimeDown()},
            {KeyboardKey.Enter, new KeyboardKeyTimeDown()},
            {KeyboardKey.Up, new KeyboardKeyTimeDown()},
            {KeyboardKey.Right, new KeyboardKeyTimeDown()},
            {KeyboardKey.Down, new KeyboardKeyTimeDown()},
            {KeyboardKey.Left, new KeyboardKeyTimeDown()}
        };
}