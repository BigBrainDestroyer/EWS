using Raylib_cs;

namespace Program;

public class DrawUi
{
    private readonly AppState _state;
    private readonly DrawDiagnostics _diagnostics;
    private readonly DrawEditorLayout _drawEditorLayout;
    private readonly DrawMazeLayout _drawMazeLayout;
    private readonly DrawCommonHeader _drawCommonHeader;

    public DrawUi(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _diagnostics = new DrawDiagnostics(_state);
        _drawEditorLayout = new DrawEditorLayout(_state);
        _drawMazeLayout = new DrawMazeLayout(_state);
        _drawCommonHeader = new DrawCommonHeader(_state);
    }
 
    public void Ui()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(_state.common.boxes.MainBoxesBackgroundColor);

        _drawCommonHeader.CommonHeader();

        _diagnostics.Draw();
        
        _drawEditorLayout.DrawEditor();

        _drawMazeLayout.Maze();

        Raylib.EndDrawing();
    }                           
}