using System.Numerics;
using Raylib_cs;

namespace Program;

public class UpdateEditorTextPanel
{
    private readonly AppState _state;
    private readonly UpdateEditorPanelText _updateEditorPanelText;

    public UpdateEditorTextPanel(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _updateEditorPanelText = new UpdateEditorPanelText(_state);
    }

    public void UpdateTextPanel()
    {
        (_state.editor.layout.textPanel.GridColumnPositions, _state.editor.layout.textPanel.GridRowPositions) = 
            CalculateTextGrid(
                _state.common.font.EditorTextPanel,
                (float)_state.editor.layout.textPanel.TextSize,
                (float)_state.editor.layout.textPanel.CharSpacing,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].X,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].Y
            );
        
        _updateEditorPanelText.UpdatingPanelText();

        _state.editor.layout.textPanel.CursorWidth =
            CalculateTextPanelCursorWidth(
                _state.editor.layout.textPanel.GridColumnPositions, 
                _state.editor.layout.textPanel.CursorTextPosition, 
                (int)_state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].X,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].X
            );

        _state.editor.layout.textPanel.CursorPosition = 
            UpdateCursorPosition(
                _state.editor.layout.boxes.BoxPixelPositions, 
                _state.editor.layout.textPanel.GridColumnPositions,
                _state.editor.layout.textPanel.GridRowPositions,
                _state.editor.layout.textPanel.CursorTextPosition
            );
    }

    private (float[], float[]) CalculateTextGrid(Font textFond, float textHeight, float charSpacing, int areaWidth, int areaHeight)
    {
        Vector2 charDimensions = Raylib.MeasureTextEx(textFond, "t", textHeight, charSpacing);
        
        float columnArrayLength = areaWidth / charDimensions.X;
        float[] columnPositions = new float[(int)columnArrayLength];

        float rowArrayLength = areaHeight / charDimensions.Y;
        float[] rowPositions = new float[(int)rowArrayLength];
        
        columnPositions = GridArrayLoop(columnPositions, charDimensions.X);
        rowPositions = GridArrayLoop(rowPositions, charDimensions.Y);
        
        return (columnPositions, rowPositions);
    }

    private float[] GridArrayLoop (float[] gridPositions, float increment)
    {
        float gridPosition = 0;
        for (int i = 0; i < gridPositions.Length; i++)
        {
            gridPositions[i] = gridPosition;

            gridPosition += increment;
        }

        return gridPositions;
    }

    private int CalculateTextPanelCursorWidth(float[] gridColumnPositions, Vector2 cursorTextPosition, int textPanelStartingPosition, int textPanelWidth)
    {
        float newCursorWidth = 0;

        if(cursorTextPosition.X == gridColumnPositions.Length)
        {
            newCursorWidth = MathF.Round(textPanelStartingPosition + textPanelWidth - textPanelStartingPosition + gridColumnPositions[(int)cursorTextPosition.X]);

            return (int)newCursorWidth;
        }

        newCursorWidth = MathF.Round(gridColumnPositions[(int)cursorTextPosition.X + 1] - gridColumnPositions[(int)cursorTextPosition.X]);

        return (int)newCursorWidth;
    }

    private Vector2 UpdateCursorPosition(Dictionary<EditorBoxNames,Vector2> boxPixelPositions, float[] gridColumnPositions, float[] gridRowPositions, Vector2 cursorTextPosition)
    {
        Vector2 newCursorPosition =
            new Vector2(
                boxPixelPositions[EditorBoxNames.TextPanel].X + gridColumnPositions[(int)cursorTextPosition.X],
                boxPixelPositions[EditorBoxNames.TextPanel].Y + gridRowPositions[(int)cursorTextPosition.Y]
            );

        return newCursorPosition;
    }
}