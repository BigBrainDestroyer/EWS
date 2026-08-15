using Raylib_cs;

namespace Program;

public class ProgramLoop
{
    private readonly AppState _state;
    private readonly InputController _input;
    private readonly UpdateApp _update;
    private readonly DrawUi _draw;

    public ProgramLoop(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _input = new InputController(_state);
        _draw = new DrawUi(_state);
        _update = new UpdateApp(_state);
    }
    
    public void Loop()
    {
        while(!Raylib.WindowShouldClose())
        {
            _input.GetInput();

            _update.App();

            _draw.Ui();
        }
    }
}
