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

public class InputState
{
    public List<int> KeyboardCharInput { get; set; } = new List<int>();
    public float MouseWheelInput { get; set; } = 0;
}