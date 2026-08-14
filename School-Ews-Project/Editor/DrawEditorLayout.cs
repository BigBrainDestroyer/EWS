using Raylib_cs;

namespace Program;

public class DrawEditorLayout
{
    private readonly AppState _state;

    public DrawEditorLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void DrawEditor()
    {
        DrawEditorLayoutBoxes();
    }
    
    private void DrawEditorLayoutBoxes()
    {
        Raylib.DrawRectangleV(
            _state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.Header], 
            _state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.Header], 
            _state.editor.layout.boxes.BoxLayoutColors[EditorBoxNames.Header]
        );

        Raylib.DrawRectangleV(
            _state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel], 
            _state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel], 
            _state.editor.layout.boxes.BoxLayoutColors[EditorBoxNames.TextPanel]
        );

        Raylib.DrawRectangleV(
            _state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.Terminal], 
            _state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.Terminal], 
            _state.editor.layout.boxes.BoxLayoutColors[EditorBoxNames.Terminal]
        );
    }
}