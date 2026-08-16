using Raylib_cs;
using System.Numerics;

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

        DrawEditorTextPanel();
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

    private void DrawEditorTextPanel()
    {
        DrawTextPanelCursor();

        DrawPanelText();
    }

    private void DrawTextPanelCursor()
    {
        Raylib.DrawRectangle(
            (int)_state.editor.layout.textPanel.CursorPosition.X,
            (int)_state.editor.layout.textPanel.CursorPosition.Y,
            _state.editor.layout.textPanel.CursorWidth,
            _state.editor.layout.textPanel.TextSize,
            _state.editor.layout.textPanel.CursorColor
        );
    }

    private void DrawPanelText()
    {
        int counter = 0;

        foreach(string text in _state.editor.layout.textPanel.PanelText)
        {
            Raylib.DrawTextEx(
                _state.common.font.EditorTextPanel,
                text,
                new Vector2(_state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].X, _state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].Y + _state.editor.layout.textPanel.GridRowPositions[counter]),
                _state.editor.layout.textPanel.TextSize,
                _state.editor.layout.textPanel.CharSpacing,
                _state.editor.layout.textPanel.TextColor
            );

            counter++;
        }
    }
}