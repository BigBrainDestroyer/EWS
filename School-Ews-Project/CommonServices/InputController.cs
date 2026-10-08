using Raylib_cs;

namespace Program;

public class InputController
{
    private AppState _state;

    public InputController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
    
    public void GetInput()
    {
        var _input = _state.common.input;
        var _time = _state.common.time;

        _input.KeyboardCharInput = GetKeyboardInput();

        _input.MouseWheelInput = GetMouseWheelInput();
    }

    public List<int> GetKeyboardInput()
    {
        int Char = Raylib.GetCharPressed();
        List<int> newKeyboardInput = new List<int>();

        while(Char != 0)
        {   
            newKeyboardInput.Add(Char);

            Char = Raylib.GetCharPressed();
        }

        return newKeyboardInput;
    }

    public float GetMouseWheelInput()
    {
        return Raylib.GetMouseWheelMove();
    }
}