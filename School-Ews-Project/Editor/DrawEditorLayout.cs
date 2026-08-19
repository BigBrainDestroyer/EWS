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
        Console.WriteLine($"{Raylib.GetScreenWidth()}");
        Console.WriteLine($"{Raylib.GetScreenHeight()}");

        DrawTextPanelCursor();

        DrawPanelText();

        DrawTextRowNumbersBox();
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
        var _panel = _state.editor.layout.textPanel;

        float maxBottomY = _state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.Terminal].Y;
        float RowIncrement = 0;

        for (int i = 0; _panel.TextPosition.Y + RowIncrement <= maxBottomY && i < _panel.ViewText.Count; i++)
        {
            float currentY = _panel.TextPosition.Y + RowIncrement;

            Raylib.DrawTextEx(
                _state.common.font.EditorTextPanel,
                _state.editor.layout.textPanel.ViewText[i],
                new Vector2 (_panel.TextPosition.X, currentY),
                _panel.TextSize,
                _panel.CharSpacing,
                _panel.TextColor
            );

            RowIncrement += _panel.TextSize;
        }
    }

    private void DrawTextRowNumbersBox()
    {
        Raylib.DrawRectangleV(
            _state.editor.layout.textPanel.TextRowNumberBoxPositions,
            _state.editor.layout.textPanel.TextRowNumberBoxLengths,
            Color.Red
        );
    }
}
