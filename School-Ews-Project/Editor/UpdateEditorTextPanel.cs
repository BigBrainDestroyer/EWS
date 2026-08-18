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
        _updateEditorPanelText.UpdatingPanelText();

        _state.editor.layout.textPanel.RowNumbersGridSpace = UpdateRowNumbersGridSpace(_state.editor.layout.textPanel.PanelText.Count, _state.editor.layout.textPanel.RowNumbersGridSpacing);

        (_state.editor.layout.textPanel.GridColumnPositions, _state.editor.layout.textPanel.GridRowPositions) = 
            CalculateTextGrid(
                _state.common.font.EditorTextPanel,
                (float)_state.editor.layout.textPanel.TextSize,
                (float)_state.editor.layout.textPanel.CharSpacing,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].X,
                (int)_state.editor.layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].Y
            );

        _state.editor.layout.textPanel.TextPosition = 
            UpdateTextPosition(
                _state.editor.layout.textPanel.GridColumnPositions, 
                _state.editor.layout.textPanel.RowNumbersGridSpace,
                _state.editor.layout.textPanel.CursorPosition,
                _state.editor.layout.textPanel.TextHorizontalPixelOffSet,
                _state.editor.layout.textPanel.TextVerticalPixelOffSet,
                (int)_state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].X,
                (int)_state.editor.layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].Y
            );

        _state.editor.layout.textPanel.CursorWidth =
            CalculateTextPanelCursorWidth( 
                _state.editor.layout.textPanel.CursorTextPosition 
            );

        _state.editor.layout.textPanel.CursorPosition = 
            UpdateCursorPosition(
              _state.editor.layout.boxes.BoxPixelPositions, 
                _state.editor.layout.textPanel.GridColumnPositions,
                _state.editor.layout.textPanel.GridRowPositions,
                _state.editor.layout.textPanel.CursorPosition,
                _state.editor.layout.textPanel.CursorTextPosition
            );
    }

    
    private int UpdateRowNumbersGridSpace(int amountRowNumbers, int rowNumbersGridSpacing)
    {
        int whiteSpace = 2;
        float gridSpace = 0;
        
        for (float i = amountRowNumbers; i >= 10; gridSpace++)
        {
            i = i * 0.1f;
        }

        gridSpace += rowNumbersGridSpacing + whiteSpace;

        return (int)gridSpace;
    }

    
    private (float[], float[]) CalculateTextGrid(Font textFond, float textHeight, float charSpacing, int areaWidth, int areaHeight)
    {
        Vector2 charDimensions = Raylib.MeasureTextEx(textFond, "t", textHeight, charSpacing);

        float columnArrayLength = MathF.Ceiling(areaWidth / charDimensions.X);
        float[] columnPositions = new float[(int)columnArrayLength];

        float rowArrayLength = MathF.Ceiling(areaHeight / charDimensions.Y);
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

    
    private Vector2 UpdateTextPosition(float[] gridColumnPositions, int rowNumbersGridSpace, Vector2 cursorTextPosition, int textPixelOffSetColumn, int textPixelOffSetRow, int panelTextStartingPositionX, int panelTextStartingPositionY)
    {
        if(gridColumnPositions.Length < rowNumbersGridSpace)
        {   
            return new Vector2();
        }
        
        Vector2 newTextPosition = 
            new Vector2(
                panelTextStartingPositionX - textPixelOffSetColumn,
                panelTextStartingPositionY - textPixelOffSetRow
            );

        for (int i = 0; i < rowNumbersGridSpace; i ++)
        {
            newTextPosition.X += gridColumnPositions[i];
        }
        
        return newTextPosition;
    }

    
    private int CalculateTextPanelCursorWidth(Vector2 cursorTextPosition)
    {
        Vector2 newCursorWidth = 
            Raylib.MeasureTextEx(
                _state.common.font.EditorTextPanel, 
                " ", 
                _state.editor.layout.textPanel.TextSize, 
                _state.editor.layout.textPanel.CharSpacing
            );
     
        newCursorWidth.X = MathF.Round(newCursorWidth.X);

        return (int)newCursorWidth.X;
    }

    
    private Vector2 UpdateCursorPosition(Dictionary<EditorBoxNames,Vector2> boxPixelPositions, float[] gridColumnPositions, float[] gridRowPositions, Vector2 cursorPosition, Vector2 cursorTextPosition)
    {
        if(cursorTextPosition.X >= gridColumnPositions.Length || cursorTextPosition.Y >= gridRowPositions.Length)
        {
            return cursorPosition;
        }
        
        cursorPosition =
            new Vector2(
                boxPixelPositions[EditorBoxNames.TextPanel].X + gridColumnPositions[(int)cursorTextPosition.X],
                boxPixelPositions[EditorBoxNames.TextPanel].Y + gridRowPositions[(int)cursorTextPosition.Y]
            );

        return cursorPosition;
    }
}