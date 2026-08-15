using System.Numerics;
using System.Text;
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
                (float)_state.editor.layout.textPanel.charSpacing,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].X,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].Y
            );

        _updateEditorPanelText.UpdatingPanelText();
    }

    private (float[], float[]) CalculateTextGrid(Font textFond, float textHeight, float charSpacing, int areaWidth, int areaHeight)
    {
        Vector2 charDimensions = Raylib.MeasureTextEx(textFond, "T", textHeight, charSpacing);
        
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
}