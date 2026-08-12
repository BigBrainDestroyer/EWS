using Raylib_cs;

namespace Program;

public class DrawUi
{
    private readonly AppState _state;
    private readonly DrawDiagnostics _draw;

    public DrawUi(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
    
    public void Ui()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);

        var _draw = new DrawDiagnostics(_state);
        _draw.DrawMainBoxes();

        Raylib.EndDrawing();
    }
}