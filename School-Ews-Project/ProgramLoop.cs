using Raylib_cs;

namespace Program;

public class ProgramLoop
{
    private readonly AppState _state;
    private readonly DrawUi _draw;
    private readonly UpdateApp _update;

    public ProgramLoop(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _update = new UpdateApp(_state);
        _draw = new DrawUi(_state);
    }
    
    public void Loop()
    {
        while(!Raylib.WindowShouldClose())
        {
            _update.App();

            _draw.Ui();
        }
    }
}
