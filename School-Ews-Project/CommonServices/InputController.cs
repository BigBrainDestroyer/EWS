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
        _state.common.input.KeyboardCharInput = GetKeyboardInput();
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
}