using Raylib_cs;

namespace Program;

public class DrawUi
{
    private readonly AppState _state;
    private readonly DrawDiagnostics _diagnostics;
    private readonly DrawEditorLayout _drawEditorLayout;
    private readonly DrawMazeLayout _drawMazeLayout;
    private readonly DrawHeaderLayout _drawHeader;
    private readonly DrawArchive _drawArchive;

    public DrawUi(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _diagnostics = new DrawDiagnostics(_state);
        _drawEditorLayout = new DrawEditorLayout(_state);
        _drawMazeLayout = new DrawMazeLayout(_state);
        _drawHeader = new DrawHeaderLayout(_state);
        _drawArchive = new DrawArchive(_state);
    }
 
    public void Ui()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(_state.common.boxes.MainBoxesBackgroundColor);

        _drawHeader.Draw();

        _diagnostics.Draw();
        
        _drawEditorLayout.DrawEditor();

        _drawMazeLayout.Maze();

        _drawArchive.Archive();

        Raylib.EndDrawing();
    }                           
}