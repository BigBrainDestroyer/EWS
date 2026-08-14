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
        if(Raylib.GetCharPressed() == 0)
        {
            return new List<int>();
        }

        List<int> newKeyboardInput = new List<int>();

        while(Raylib.GetCharPressed() != 0)
        {
            newKeyboardInput.Add(Raylib.GetCharPressed());            
        }

        return newKeyboardInput;
    }
}