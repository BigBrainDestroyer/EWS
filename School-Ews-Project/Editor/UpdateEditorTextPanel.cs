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
        var _boxes = _state.common.boxes;
        
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
                (int)_boxes.MainBoxPixelLengts[MainBoxNames.Editor].X,
                (int)_boxes.MainBoxPixelLengts[MainBoxNames.Editor].Y
            );



        (_layout.textPanel.WriteOverBoxPositionHorizontalText, _layout.textPanel.WriteOverBoxPositionVerticalText) =
            CalculateWriteOverBoxPositions(
                _boxes.MainBoxPixelPositions[MainBoxNames.Editor],
                _boxes.MainBoxPixelLengts[MainBoxNames.Editor]
            );

        (_layout.textPanel.WriteOverBoxLengthsHorizontalText, _layout.textPanel.WriteOverBoxLengthsVerticalText) =
            CalculateWriteOverBoxLengths(
                _boxes.MainBoxPixelLengts[MainBoxNames.Editor],
                _boxes.MainBoxesPixelSpacing
            );

        (_layout.textPanel.TextRowNumberBoxPositions, _layout.textPanel.TextRowNumberBoxLengths) =
                CalculateTextRowNumbersBox(
                    _boxes.MainBoxPixelPositions[MainBoxNames.Editor],
                    _boxes.MainBoxPixelLengts[MainBoxNames.Editor],
                    _layout.textPanel.GridColumnPositions,
                    _layout.textPanel.GridBoxesSpaceOffRowNumbers
                );

        _layout.textPanel.TextPosition =
            UpdateTextPosition(
                _layout.textPanel.TextRowNumberBoxLengths,
                _layout.textPanel.TextHorizontalPixelOffSet,
                _layout.textPanel.TextVerticalPixelOffSet,
                (int)_boxes.MainBoxPixelPositions[MainBoxNames.Editor].X,
                (int)_boxes.MainBoxPixelPositions[MainBoxNames.Editor].Y
            );

        _layout.textPanel.RowNumbersPosition =
            UpdateRowNumberTextPosition(
                _layout.textPanel.GridColumnPositions,
                _layout.textPanel.GridBoxesSpaceOffRowNumbers,
                _layout.textPanel.TextVerticalPixelOffSet,
                (int)_boxes.MainBoxPixelPositions[MainBoxNames.Editor].X,
                (int)_boxes.MainBoxPixelPositions[MainBoxNames.Editor].Y
            );

        _layout.textPanel.CursorWidth =
            CalculateTextPanelCursorWidth(
                _layout.textPanel.CursorTextPosition
            );

        _layout.textPanel.CursorPosition =
            UpdateCursorPosition(
                _boxes.MainBoxPixelPositions[MainBoxNames.Editor],
                _layout.textPanel.GridColumnPositions,
                _layout.textPanel.GridRowPositions,
                _layout.textPanel.CursorPosition,
                _layout.textPanel.CursorTextPosition,
                _layout.textPanel.TextRowNumberBoxLengths,
                _layout.textPanel.TextGridBoxesOffSetX,
                _layout.textPanel.TextGridBoxesOffSetY
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


    private (float[], float[]) CalculateTextGrid(Font textFont, float textHeight, float charSpacing, int areaWidth, int areaHeight)
    {
        Vector2 charDimensions = Raylib.MeasureTextEx(textFont, "t", textHeight, charSpacing);

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

    private (Vector2, Vector2) CalculateWriteOverBoxPositions(Vector2 parentBoxPosition, Vector2 parentBoxLengths)
    {
        Vector2 newWriteOverBoxPositionsHorizontal =
            new Vector2(
                parentBoxPosition.X,
                parentBoxPosition.Y + parentBoxLengths.Y
            );

        Vector2 newWriteOverBoxPositionsVertical =
            new Vector2(
                parentBoxPosition.X + parentBoxLengths.X,
                parentBoxPosition.Y
            );

        return (newWriteOverBoxPositionsHorizontal, newWriteOverBoxPositionsVertical);
    }

    private (Vector2, Vector2) CalculateWriteOverBoxLengths(Vector2 parentBoxLengths, float mainBoxSpacing)
    {
        Vector2 newWriteOverBoxLengthsHorizontal =
            new Vector2(
                parentBoxLengths.X + mainBoxSpacing,
                mainBoxSpacing
            );

        Vector2 newWriteOverBoxLengthsVertical =
            new Vector2(
                mainBoxSpacing,
                parentBoxLengths.Y
            );

        return (newWriteOverBoxLengthsHorizontal, newWriteOverBoxLengthsVertical);
    }

    private (Vector2, Vector2) CalculateTextRowNumbersBox(Vector2 parentBoxPositions, Vector2 parentBoxLengths, float[] gridColumnPositions, int amountOfColumnGridSpace)
    {
        Vector2 newBoxPositions = parentBoxPositions;
        Vector2 newBoxLengths;

        if(amountOfColumnGridSpace >= gridColumnPositions.Length)
        {
            newBoxLengths =
                new Vector2(
                    parentBoxLengths.X,
                    parentBoxLengths.Y
                );

            return (newBoxPositions, newBoxLengths);
        }
        
        newBoxLengths =
            new Vector2(
                gridColumnPositions[amountOfColumnGridSpace] -1, //Raylib base spacing between chars 
                parentBoxLengths.Y
            );

        return (newBoxPositions, newBoxLengths);
    }


    private Vector2 UpdateTextPosition(Vector2 textRowNumberBoxLengths, int textPixelOffSetColumn, int textPixelOffSetRow, int panelTextStartingPositionX, int panelTextStartingPositionY)
    {
        Vector2 newTextPosition =
            new Vector2(
                panelTextStartingPositionX + textRowNumberBoxLengths.X - textPixelOffSetColumn,
                panelTextStartingPositionY - textPixelOffSetRow
            );

        return newTextPosition;
    }

    private Vector2 UpdateRowNumberTextPosition(float[] gridColumnPositions, int gridBoxesSpaceOffRowNumbers, int textPixelOffSetRow, int panelTextStartingPositionX, int panelTextStartingPositionY)
    {
        if(gridColumnPositions.Length < gridBoxesSpaceOffRowNumbers)
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


    private Vector2 UpdateCursorPosition(Vector2 boxPixelPositions, float[] gridColumnPositions, float[] gridRowPositions, Vector2 cursorPosition, Vector2 cursorTextPosition, Vector2 textRowNumberBoxLengths, int textGridBoxesOffSetX, int textGridBoxesOffSetY)
    {
        if(cursorTextPosition.X - textGridBoxesOffSetX >= gridColumnPositions.Length || cursorTextPosition.Y - textGridBoxesOffSetY>= gridRowPositions.Length || cursorTextPosition.Y < textGridBoxesOffSetY || cursorTextPosition.X < textGridBoxesOffSetX)
        {
            return new Vector2(-1,-1);
        }

        int cursorColumnPosition = (int)cursorTextPosition.X - textGridBoxesOffSetX;
        int cursorRowPosition = (int)cursorTextPosition.Y - textGridBoxesOffSetY;

        cursorPosition =
            new Vector2(
                boxPixelPositions.X + gridColumnPositions[cursorColumnPosition] + textRowNumberBoxLengths.X,
                boxPixelPositions.Y + gridRowPositions[cursorRowPosition]
            );

        return cursorPosition;
    }
}
