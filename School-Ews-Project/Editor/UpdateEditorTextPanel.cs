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
                _state.common.font.EditorPanelText,
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
                    _layout.textPanel.GridBoxesSpaceOffRowNumbers
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

        _layout.textPanel.RowNumbersPosition =
            UpdateRowNumberTextPosition(
                _layout.textPanel.GridColumnPositions,
                _layout.textPanel.GridBoxesSpaceOffRowNumbers,
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
                _layout.textPanel.CursorTextPosition,
                _layout.textPanel.TextRowNumberBoxLengths
            );
    }


    private int UpdateGridBoxesSpaceOffRowNumbers(int amountRowNumbers, int extraGridBoxSpaceBetweenTextAndRowNumbers)
    {
        int whiteSpace = 3;
        float gridSpace = 0;

        for (float i = amountRowNumbers; i >= 10; gridSpace++)
        {
            i = i * 0.1f;
        }

        gridSpace += extraGridBoxSpaceBetweenTextAndRowNumbers ;

        return (int)gridSpace + whiteSpace;
    }


    private (float[], float[]) CalculateTextGrid(Font textFond, float textHeight, float charSpacing, int areaWidth, int areaHeight)
    {
        Vector2 charDimensions = Raylib.MeasureTextEx(textFond, "t", textHeight, charSpacing);

        float columnArrayLength = MathF.Ceiling((float)areaWidth / charDimensions.X);
        float[] columnPositions = new float[(int)columnArrayLength];

        float rowArrayLength = MathF.Ceiling((float)areaHeight / charDimensions.Y);
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


    private (Vector2, Vector2) CalculateTextRowNumbersBox(Dictionary<EditorBoxNames, Vector2> parentBoxPositions, Dictionary<EditorBoxNames, Vector2> parentBoxLengths, float[] gridColumnPositions, int amountOfColumnGridSpace)
    {
        Vector2 newBoxPositions = parentBoxPositions[EditorBoxNames.TextPanel];

        Vector2 newBoxLengths =
            new Vector2(
                -1, //Raylib base spacing between chars 
                parentBoxLengths[EditorBoxNames.TextPanel].Y
            );

        for(int i = 0; i < amountOfColumnGridSpace; i++)
        {
            if(i >= gridColumnPositions.Length -1)
            {
                break;
            }
            
            newBoxLengths.X += gridColumnPositions[i];
        }

        return (newBoxPositions, newBoxLengths);
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
            newTextPosition.X += (int)gridColumnPositions[i];
        }

        return newTextPosition;
    }

    private Vector2 UpdateRowNumberTextPosition(float[] gridColumnPositions, int GridBoxesSpaceOffRowNumbers, int textPixelOffSetRow, int panelTextStartingPositionX, int panelTextStartingPositionY)
    {
        if(gridColumnPositions.Length < GridBoxesSpaceOffRowNumbers)
        {
            return new Vector2();
        }

        int gridBoxesWhiteSpaceLeft = 2;

        Vector2 newNumberPosition =
            new Vector2(
                panelTextStartingPositionX,
                panelTextStartingPositionY - textPixelOffSetRow
            );

        for (int i = 0; i < gridBoxesWhiteSpaceLeft; i ++)
        {
            newNumberPosition.X += (int)gridColumnPositions[i];
        }

        return newNumberPosition;
    }

    private int CalculateTextPanelCursorWidth(Vector2 cursorTextPosition)
    {
        Vector2 newCursorWidth =
            Raylib.MeasureTextEx(
                _state.common.font.EditorPanelText,
                " ",
                _state.editor.layout.textPanel.TextSize,
                _state.editor.layout.textPanel.CharSpacing
            );

        newCursorWidth.X = MathF.Round(newCursorWidth.X);

        return (int)newCursorWidth.X;
    }


    private Vector2 UpdateCursorPosition(Dictionary<EditorBoxNames,Vector2> boxPixelPositions, float[] gridColumnPositions, float[] gridRowPositions, Vector2 cursorPosition, Vector2 cursorTextPosition, Vector2 textRowNumberBoxLengths)
    {
        if(cursorTextPosition.X >= gridColumnPositions.Length || cursorTextPosition.Y >= gridRowPositions.Length)
        {
            return cursorPosition;
        }

        cursorPosition =
            new Vector2(
                boxPixelPositions[EditorBoxNames.TextPanel].X + gridColumnPositions[(int)cursorTextPosition.X] + textRowNumberBoxLengths.X,
                boxPixelPositions[EditorBoxNames.TextPanel].Y + gridRowPositions[(int)cursorTextPosition.Y]
            );

        return cursorPosition;
    }
}
