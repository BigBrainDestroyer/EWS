//Update: Terminal kan hier wel gw gedrawd worden maar de states worden gewijzicht later

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
        DrawEditorTextPanelBackRound();
        
        if (_state.editor.layout.textPanel.CurrentLoadedFile.Length > 0)
        {
            DrawEditorTextPanel();

            DrawWriteOverTextBoxes();
        }
        else 
        {
            DrawLoadFile();
        }

        
    }

    private void DrawEditorTextPanel()
    {
        DrawTextPanelCursor();

        DrawTextMouseCursor();

        DrawPanelText();

        DrawTextRowNumbersBox();

        DrawRowNumbers();
    }

    private void DrawLoadFile()
    {
        var _textPanel = _state.editor.layout.textPanel;

        Custom.DrawCustomRectangle(
            _textPanel.FileNotLoadedText.Box.PixelPosition, 
            _textPanel.FileNotLoadedText.Box.PixelLengths,
            _textPanel.FileNotLoadedText.Box.Radius,
            _textPanel.FileNotLoadedText.Box.CornerStates,
            _textPanel.FileNotLoadedText.BoxBackgroundColor
        );

        Raylib.DrawTextEx(
            _textPanel.FileNotLoadedText.Font,
            _textPanel.FileNotLoadedText.Text.Text,
            _textPanel.FileNotLoadedText.TextPosition,
            _textPanel.FileNotLoadedText.Text.TextSize,
            _textPanel.FileNotLoadedText.Text.TextSpacing,
            _textPanel.FileNotLoadedText.TextColor
        );
    }


    private void DrawEditorTextPanelBackRound()
    {
        var _boxes = _state.common.boxes;

        Custom.DrawCustomRectangle(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Editor], 
            _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Editor],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Editor],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Editor],
            _state.editor.layout.textPanel.TextPanelBackgroundColor
        );
    }


    private void DrawTextPanelCursor()
    {
        if (_state.editor.layout.textPanel.CursorPosition.X != -1 || _state.editor.layout.textPanel.CursorPosition.Y != -1)
        {
            Raylib.DrawRectangle(
                (int)_state.editor.layout.textPanel.CursorPosition.X,
                (int)_state.editor.layout.textPanel.CursorPosition.Y,
                _state.editor.layout.textPanel.CursorWidth,
                _state.editor.layout.textPanel.TextSize,
                _state.editor.layout.textPanel.CursorColor
            );
        }
    }

    private void DrawTextMouseCursor()
    {
        var _layout = _state.editor.layout.textPanel;

        if (_layout.MouseCursorBox.IsCursorBoxActive)
        {
            Raylib.DrawRectangle(
                (int)_layout.MouseCursorBox.CursorBox.PixelPosition.X,
                (int)_layout.MouseCursorBox.CursorBox.PixelPosition.Y,
                (int)_layout.MouseCursorBox.CursorBox.PixelLengths.X,
                (int)_layout.MouseCursorBox.CursorBox.PixelLengths.Y,
                _layout.MouseCursorBox.CursorBoxColor
            );
        }
    }


    private void DrawPanelText()
    {
        var _panel = _state.editor.layout.textPanel;

        float maxBottomY = _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Archive].Y + _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Archive].Y;
        float RowIncrement = 0;

        for (int i = 0; _panel.TextPosition.Y + RowIncrement <= maxBottomY && i < _panel.ViewText.Count; i++)
        {
            float currentY = _panel.TextPosition.Y + RowIncrement;

            Raylib.DrawTextEx(
                _panel.EditorPanelTextFont,
                _state.editor.layout.textPanel.ViewText[i],
                new Vector2(_panel.TextPosition.X, currentY),
                _panel.TextSize,
                _panel.CharSpacing,
                _panel.TextColor
            );

            RowIncrement += _panel.TextSize;
        }
    }

    private void DrawTextRowNumbersBox()
    {
        Custom.DrawCustomRectangle(
            _state.editor.layout.textPanel.TextRowNumberBoxPositions,
            _state.editor.layout.textPanel.TextRowNumberBoxLengths,
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.editor.layout.textPanel.textRowNumberBoxCornerStates,
            _state.editor.layout.textPanel.TerminalBackgroundColor
        );
    }

    private void DrawRowNumbers()
    {
        var _panel = _state.editor.layout.textPanel;
        var _boxes = _state.common.boxes;

        int restoreBaseOffSet = 1;
        float maxBottomY = _boxes.MainBoxPixelPositions[MainBoxNames.Editor].Y + _boxes.MainBoxPixelLengts[MainBoxNames.Editor].Y;
        float RowIncrement = 0;

        Color numberColor = _panel.RowNumberColor;

        for (int i = _panel.TextGridBoxesOffSetY + restoreBaseOffSet; i <= _panel.PanelText.Count; i++)
        {
            if (_panel.RowNumbersPosition.Y + RowIncrement > maxBottomY)
            {
                break;
            }

            if (_panel.CursorTextPosition.Y == i - restoreBaseOffSet)
            {
                numberColor = _panel.ActiveRowNumberColor;
            }

            float currentY = _panel.RowNumbersPosition.Y + RowIncrement;

            Raylib.DrawTextEx(
                _panel.EditorPanelRowNumbersFont,
                $"{i}",
                new Vector2(_panel.RowNumbersPosition.X, currentY),
                _panel.TextSize,
                _panel.CharSpacing,
                numberColor
            );

            RowIncrement += _panel.TextSize;

            numberColor = _panel.RowNumberColor;
        }
    }

    private void DrawWriteOverTextBoxes()
    {
        var _textPanel = _state.editor.layout.textPanel;

        Raylib.DrawRectangleV(
            _textPanel.WriteOverBoxPositionHorizontalText,
            _textPanel.WriteOverBoxLengthsHorizontalText,
            _state.common.boxes.MainBoxesBackgroundColor
        );

        Raylib.DrawRectangleV(
            _textPanel.WriteOverBoxPositionVerticalText,
            _textPanel.WriteOverBoxLengthsVerticalText,
            _state.common.boxes.MainBoxesBackgroundColor
        );
    }
}