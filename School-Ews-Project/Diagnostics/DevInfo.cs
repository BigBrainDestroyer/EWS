using Raylib_cs;

namespace Program;

public class DevInfo
{
    private readonly AppState _state;
    private readonly DrawDiagnostics _draw;

    public DevInfo(AppState state)
    {
        state = _state ?? throw new ArgumentNullException(nameof(state));

        _draw = new DrawDiagnostics(_state);
    }
    
    public void GetDevInfo()
    {
        
    }
}