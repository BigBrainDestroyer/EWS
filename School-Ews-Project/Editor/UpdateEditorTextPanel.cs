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
        var _layout = _state.editor.layout;

        _updateEditorPanelText.UpdatingPanelText();

        _layout.textPanel.GridBoxesSpaceOffRowNumbers =
            UpdateGridBoxesSpaceOffRowNumbers(
                _layout.textPanel.PanelText.Count,
                _layout.textPanel.ExtraGridBoxSpaceBetweenTextAndRowNumbers
            );

        (_layout.textPanel.GridColumnPositions, _layout.textPanel.GridRowPositions) =
            CalculateTextGrid(
                _state.common.font.EditorTextPanel,
                (float)_layout.textPanel.TextSize,
                (float)_layout.textPanel.CharSpacing,
                (int)_layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].X,
                (int)_layout.boxes.BoxPixelLengths[EditorBoxNames.TextPanel].Y
            );

            (_layout.textPanel.TextRowNumberBoxPositions, _layout.textPanel.TextRowNumberBoxLengths) =
                CalculateTextRowNumbersBox(
                    _layout.boxes.BoxPixelPositions,
                    _layout.boxes.BoxPixelLengths,
                    _layout.textPanel.GridColumnPositions,
                    _layout.textPanel.RowNumbersTotalGridSpace
                );

        _layout.textPanel.TextPosition =
            UpdateTextPosition(
                _layout.textPanel.GridColumnPositions,
                _layout.textPanel.GridBoxesSpaceOffRowNumbers,
                _layout.textPanel.CursorPosition,
                _layout.textPanel.TextHorizontalPixelOffSet,
                _layout.textPanel.TextVerticalPixelOffSet,
                (int)_layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].X,
                (int)_layout.boxes.BoxPixelPositions[EditorBoxNames.TextPanel].Y
            );

        _layout.textPanel.CursorWidth =
            CalculateTextPanelCursorWidth(
                _layout.textPanel.CursorTextPosition
            );

        _layout.textPanel.CursorPosition =
            UpdateCursorPosition(
              _layout.boxes.BoxPixelPositions,
                _layout.textPanel.GridColumnPositions,
                _layout.textPanel.GridRowPositions,
                _layout.textPanel.CursorPosition,
                _layout.textPanel.CursorTextPosition
            );
    }


    private int UpdateGridBoxesSpaceOffRowNumbers(int amountRowNumbers, int extraGridBoxSpaceBetweenTextAndRowNumbers)
    {
        int whiteSpace = 2;
        float gridSpace = 0;

        for (float i = amountRowNumbers; i >= 10; gridSpace++)
        {
            i = i * 0.1f;
        }

        gridSpace += extraGridBoxSpaceBetweenTextAndRowNumbers + whiteSpace;

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


    private (Vector2, Vector2) CalculateTextRowNumbersBox(Dictionary<EditorBoxNames, Vextor2> parentBoxPositions, Dictionary<EditorBoxNames, Vextor2> parentBoxLengthsLengths, float[] gridColumnPositions, int amountOfColumnGridSpace)
    {
        Vector2 newBoxPositions = parentBoxPositions[EditorBoxNames.TextPanel];

        Vector2 newBoxLengths =
            new Vector2(
                0,
                parentBoxLengths[EditorBoxNames.TextPanel].Y
            );

        for(int i = 0; i == amountOfColumnGridSpace; i++)
        {
            boxLengths.X += gridColumnPositions;
        }

        return (boxPositions, boxLengths);
    }


    private Vector2 UpdateTextPosition(float[] gridColumnPositions, int GridBoxesSpaceOffRowNumbers, Vector2 cursorTextPosition, int textPixelOffSetColumn, int textPixelOffSetRow, int panelTextStartingPositionX, int panelTextStartingPositionY)
    {
        if(gridColumnPositions.Length < GridBoxesSpaceOffRowNumbers)
        {
            return new Vector2();
        }

        Vector2 newTextPosition =
            new Vector2(
                panelTextStartingPositionX - textPixelOffSetColumn,
                panelTextStartingPositionY - textPixelOffSetRow
            );

        for (int i = 0; i < GridBoxesSpaceOffRowNumbers; i ++)
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
