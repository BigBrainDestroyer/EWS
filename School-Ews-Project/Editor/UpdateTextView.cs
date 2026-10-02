using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Program;

public class UpdateTextView
{
    private readonly AppState _state;

    public UpdateTextView(AppState  state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void IsMouseWheelActive()
    {
        var _input = _state.common.input;
        var _textPanel = _state.editor.layout.textPanel;
        
        if(_input.MouseWheelInput != 0)
        {
            (_textPanel.TextGridBoxesOffSetX, _textPanel.TextGridBoxesOffSetY) = UpdateScrollWheelOffSet(_textPanel.PanelText, _textPanel.TextGridBoxesOffSetX, _textPanel.TextGridBoxesOffSetY);

            _textPanel.ViewText = 
                UpdateViewText(
                    _textPanel.PanelText, 
                    _textPanel.GridColumnPositions,
                    _textPanel.GridRowPositions,
                    _textPanel.TextGridBoxesOffSetX,
                    _textPanel.TextGridBoxesOffSetY,
                    _textPanel.GridBoxesSpaceOffRowNumbers
                );
        }
    }

    public void UpdateView()
    {
        var _textPanel = _state.editor.layout.textPanel;

        (_textPanel.CursorTextPosition, _textPanel.TextGridBoxesOffSetX, _textPanel.TextGridBoxesOffSetY) = 
            UpdateGridBoxesOffSet(
                _textPanel.ViewText,
                _textPanel.GridColumnPositions,
                _textPanel.GridRowPositions,
                _textPanel.CursorTextPosition,
                _textPanel.TextGridBoxesOffSetX,
                _textPanel.TextGridBoxesOffSetY,
                _textPanel.GridBoxesSpaceOffRowNumbers,
                _textPanel.TextNavigationWhiteSpaceRight,
                _textPanel.TextNavigationWhiteSpaceLeft,
                _textPanel.TextNavigationWhiteSpaceDown,
                _textPanel.TextNavigationWhiteSpaceUp
            );
        
        _textPanel.ViewText = UpdateViewText(
            _textPanel.PanelText, 
            _textPanel.GridColumnPositions,
            _textPanel.GridRowPositions,
            _textPanel.TextGridBoxesOffSetX,
            _textPanel.TextGridBoxesOffSetY,
            _textPanel.GridBoxesSpaceOffRowNumbers
        );
    }

    public List<string> UpdateViewText(List<string> panelText, float[] gridColumnPositions, float[] gridRowPositions, int textGridBoxesOffSetX, int textGridBoxesOffSetY, int gridBoxesSpaceOffRowNumbers)
    {
        StringBuilder newString;
        List<string> newViewText = new List<string>();

        for (int i = textGridBoxesOffSetY, x =  0; x < gridRowPositions.Length; i++, x++)
        {
            if(i == panelText.Count)
            {
                break;
            }

            newString = new StringBuilder(panelText[i]);

            if(newString.Length >= textGridBoxesOffSetX)
            {
                newString.Remove(0, textGridBoxesOffSetX);

                if(newString.Length >= gridColumnPositions.Length + textGridBoxesOffSetX - gridBoxesSpaceOffRowNumbers)
                {
                    newString.Length =  gridColumnPositions.Length - gridBoxesSpaceOffRowNumbers;
                }
            }
            else
            {
                newString.Length = 0;
            }

            newViewText.Add(newString.ToString());
        }

        return newViewText;
    }
    
    private (Vector2, int, int) UpdateGridBoxesOffSet(List<string> viewText, float[] gridColumnPositions, float[] gridRowPositions , Vector2 cursorTextPosition, int textGridBoxesOffSetX, int textGridBoxesOffSetY, int gridBoxesSpaceOffRowNumbers, int textTypingWhiteSpaceRight, int textTypingWhiteSpaceLeft, int textTypingWhiteSpaceDown,  int textTypingWhiteSpaceUp)
    {
        int newTextGridBoxesOffSetX = textGridBoxesOffSetX;
        int newTextGridBoxesOffSetY = textGridBoxesOffSetY;

        if((int)cursorTextPosition.X - textGridBoxesOffSetX >= gridColumnPositions.Length - 1 - textTypingWhiteSpaceRight - gridBoxesSpaceOffRowNumbers)
        {
            int gridColumnTextLength = gridColumnPositions.Length - gridBoxesSpaceOffRowNumbers;

            newTextGridBoxesOffSetX = Math.Abs(gridColumnTextLength - 1 - textTypingWhiteSpaceRight - (int)cursorTextPosition.X);
        }
        else if((int)cursorTextPosition.X <= textGridBoxesOffSetX + textTypingWhiteSpaceLeft && (int)cursorTextPosition.X >= textTypingWhiteSpaceLeft)
        {
            newTextGridBoxesOffSetX = (int)cursorTextPosition.X - textTypingWhiteSpaceLeft;
        }
        else if((int)cursorTextPosition.X <= textGridBoxesOffSetX)
        {
            newTextGridBoxesOffSetX = (int)cursorTextPosition.X;
        }

        if((int)cursorTextPosition.Y <= textGridBoxesOffSetY + textTypingWhiteSpaceUp && (int)cursorTextPosition.Y >= textTypingWhiteSpaceUp)
        {
            newTextGridBoxesOffSetY = (int)cursorTextPosition.Y - textTypingWhiteSpaceUp;
        }
        else if((int)cursorTextPosition.Y <= textGridBoxesOffSetY)
        {
            newTextGridBoxesOffSetY = (int)cursorTextPosition.Y;
        }
        else if((int)cursorTextPosition.Y - textGridBoxesOffSetY >= gridRowPositions.Length - 1 - textTypingWhiteSpaceDown)
        {
            newTextGridBoxesOffSetY = Math.Abs(gridRowPositions.Length - 1 - textTypingWhiteSpaceDown - (int)cursorTextPosition.Y);
        }

        return (cursorTextPosition, newTextGridBoxesOffSetX, newTextGridBoxesOffSetY);
    }

    public (int, int) UpdateScrollWheelOffSet(List<string> panelText, int currentTextGridBoxesOffSetX, int currentTextGridBoxesOffSetY)
    {
        var _input = _state.common.input;
        var _textPanel = _state.editor.layout.textPanel;

        int newTextGridBoxesOffSet;

        if (_input.MouseWheelInput != 0)
        {
            if(Raylib.IsKeyDown(KeyboardKey.LeftShift))
            {
                newTextGridBoxesOffSet = currentTextGridBoxesOffSetX;
                newTextGridBoxesOffSet -= (int)_input.MouseWheelInput;

                if(newTextGridBoxesOffSet < 0 || newTextGridBoxesOffSet >= panelText.Max(String => String.Length))
                {
                    return (currentTextGridBoxesOffSetX, currentTextGridBoxesOffSetY);
                }
                else
                {
                    return (newTextGridBoxesOffSet, currentTextGridBoxesOffSetY);
                }
            }
            else
            {
                newTextGridBoxesOffSet = currentTextGridBoxesOffSetY;
                newTextGridBoxesOffSet -= (int)_input.MouseWheelInput;

                if (newTextGridBoxesOffSet < 0 || newTextGridBoxesOffSet >= panelText.Count)
                {
                    return (currentTextGridBoxesOffSetX, currentTextGridBoxesOffSetY);
                }
                else
                {
                    return (currentTextGridBoxesOffSetX, newTextGridBoxesOffSet);
                }
            } 
        }

        return (currentTextGridBoxesOffSetX, currentTextGridBoxesOffSetY);
    }
}