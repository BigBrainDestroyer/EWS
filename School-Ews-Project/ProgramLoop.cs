using Raylib_cs;

namespace Program;

public class ProgramLoop
{
    private readonly AppState _state;
    private readonly CalculateMainBoxes _calculateMainBoxes;
    private readonly DrawUi _draw;

    public ProgramLoop(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        
        _calculateMainBoxes = new CalculateMainBoxes(_state);
        _draw = new DrawUi(_state);
    }
    
    public void Loop()
    {
        while(!Raylib.WindowShouldClose())
        {
            _calculateMainBoxes.UpdateMainBoxes();
            
            _draw.Ui();
        }
    }
}
