using Raylib_cs;

namespace Program;

public class DrawUi
{
    private readonly AppState _state;
    private readonly DrawDiagnostics _diagnostics;
    private readonly DrawEditorLayout _drawEditorLayout;

    public DrawUi(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _diagnostics = new DrawDiagnostics(_state);
        _drawEditorLayout = new DrawEditorLayout(_state);
    }
    
    public void Ui()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.RayWhite);
        
        _diagnostics.Draw();

        _drawEditorLayout.DrawEditor();

        Raylib.EndDrawing();
    }
}