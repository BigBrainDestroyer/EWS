using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Program;

public class UpdateEditorPanelText
{
    private readonly AppState _state;

    public UpdateEditorPanelText(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void UpdatingPanelText()
    {
        var _input = _state.common.input;
        var _textPanel = _state.editor.layout.textPanel;

        if(_input.KeyboardCharInput.Count > 0 || Raylib.IsKeyPressed(KeyboardKey.Backspace) || Raylib.IsKeyPressed(KeyboardKey.Enter) ||Raylib.IsKeyPressed(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            (_textPanel.CursorTextPosition, _textPanel.PanelText, _textPanel.ViewText, _input.KeyboardCharInput) =
                UpdatingText(
                    _textPanel.CursorTextPosition,
                    _textPanel.PanelText,
                    _input.KeyboardCharInput,
                    _textPanel.GridColumnPositions,
                    _textPanel.GridRowPositions,
                    _textPanel.TextGridBoxesOffSetX,
                    _textPanel.TextGridBoxesOffSetY,
                    _textPanel.GridBoxesSpaceOffRowNumbers
                );
        }
        
        if(Raylib.IsWindowResized())
        {
            _textPanel.ViewText = UpdateViewText(
                _textPanel.PanelText, 
                _textPanel.GridColumnPositions,
                _textPanel.GridRowPositions,
                _textPanel.TextGridBoxesOffSetX,
                _textPanel.TextGridBoxesOffSetY,
                _textPanel.GridBoxesSpaceOffRowNumbers
            );
        }

        (_textPanel.CursorTextPosition, _textPanel.TextGridBoxesOffSetX, _textPanel.TextGridBoxesOffSetY) = 
            UpdateGridBoxesOffSet(
                _textPanel.ViewText,
                _textPanel.GridColumnPositions,
                _textPanel.GridRowPositions,
                _textPanel.CursorTextPosition,
                _textPanel.TextGridBoxesOffSetX,
                _textPanel.TextGridBoxesOffSetY,
                _textPanel.GridBoxesSpaceOffRowNumbers
            );
    }

    
    private (Vector2, List<string>, List<string>,List<int>) UpdatingText(Vector2 cursorTextPosition, List<string> panelText, List<int> chars, float[] gridColumnPositions, float[] gridRowPositions,int textGridBoxesOffSetX, int textGridBoxesOffSetY, int gridBoxesSpaceOffRowNumbers)
    {
        StringBuilder newString = new StringBuilder(panelText[(int)cursorTextPosition.Y]);

        if(Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            (cursorTextPosition, newString, panelText) = 
                UpdateEnterToText(cursorTextPosition, newString, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Backspace))
        {
            (cursorTextPosition, newString, panelText) = 
                UpdateBackspaceToString(cursorTextPosition, newString, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            cursorTextPosition = 
                UpdateUpToText(cursorTextPosition, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            cursorTextPosition = 
                UpdateDownToText(cursorTextPosition, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            cursorTextPosition = 
                UpdateLeftToText(cursorTextPosition, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            cursorTextPosition =
                UpdateRightToText(cursorTextPosition, panelText);
        }
        
        if(chars.Count > 0)
        {
            (cursorTextPosition, panelText, chars) =
                UpdateCharInputToText(cursorTextPosition, newString, panelText, chars);
        }
        
        List<string> viewText = UpdateViewText(panelText, gridColumnPositions, gridRowPositions, textGridBoxesOffSetX, textGridBoxesOffSetY, gridBoxesSpaceOffRowNumbers);

        return (cursorTextPosition, panelText, viewText, chars);
    }


    private (Vector2, StringBuilder, List<string>) UpdateEnterToText(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText)
    {
        string newRow = newString.ToString((int)cursorTextPosition.X, newString.Length - (int)cursorTextPosition.X);

        panelText[(int)cursorTextPosition.Y] = newString.ToString(0, (int)cursorTextPosition.X);

        cursorTextPosition.Y += 1;

        if(panelText.Count == cursorTextPosition.Y)
        {
            panelText.Add(newRow);
        }
        else
        {
            panelText.Insert((int)cursorTextPosition.Y, newRow);
        }

        cursorTextPosition.X = 0;

        newString = new StringBuilder(newRow);

        return (cursorTextPosition, newString, panelText);
    }

    
    private (Vector2, StringBuilder, List<string>) UpdateBackspaceToString(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText)
    {
        if(cursorTextPosition.X == 0 && cursorTextPosition.Y == 0)//weet niet zeker 1 of 0 maar dat zien we wel
        {
            return (cursorTextPosition, newString, panelText);
        }
        else if(cursorTextPosition.X == 0)
        {
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y - 1].Length;

            newString.Insert(0, panelText[(int)cursorTextPosition.Y - 1]);
            
            panelText.RemoveAt((int)cursorTextPosition.Y);

            cursorTextPosition.Y -= 1;

            panelText[(int)cursorTextPosition.Y] = newString.ToString();

            return (cursorTextPosition, newString, panelText);
        }

        cursorTextPosition.X -= 1;
        
        newString.Remove((int)cursorTextPosition.X, 1);
        
        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        return (cursorTextPosition, newString, panelText);
    }

    
    private Vector2 UpdateUpToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if((int)cursorTextPosition.Y == 0)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X > panelText[(int)cursorTextPosition.Y - 1].Length)
        {
            cursorTextPosition.Y -= 1;
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y].Length;

            return cursorTextPosition;
        }

        cursorTextPosition.Y -= 1;

        return cursorTextPosition;
    }

    
    private Vector2 UpdateDownToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if((int)cursorTextPosition.Y == panelText.Count - 1)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X > panelText[(int)cursorTextPosition.Y + 1].Length)
        {
            cursorTextPosition.Y += 1;
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y].Length;

            return cursorTextPosition;
        }

        cursorTextPosition.Y += 1;

        return cursorTextPosition;
    }

    
    private Vector2 UpdateLeftToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if(cursorTextPosition.X == 0 && cursorTextPosition.Y == 0)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X == 0)
        {
            cursorTextPosition.Y -= 1;
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y].Length;

            return cursorTextPosition;
        }

        cursorTextPosition.X -= 1;
        
        return cursorTextPosition;
    }

    
    private Vector2 UpdateRightToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if(cursorTextPosition.X == panelText[(int)cursorTextPosition.Y].Length && cursorTextPosition.Y == panelText.Count - 1)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X == panelText[(int)cursorTextPosition.Y].Length)
        {
            cursorTextPosition.Y += 1;
            cursorTextPosition.X = 0;

            return cursorTextPosition;
        }

        cursorTextPosition.X += 1;
        
        return cursorTextPosition;
    }

    
    private (Vector2, List<string>, List<int>) UpdateCharInputToText(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText, List<int> chars)
    {
        foreach(int character in chars)
        {
            newString.Insert((int)cursorTextPosition.X, (char)character);
            cursorTextPosition.X += 1;
        }

        chars = new List<int>();

        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        return (cursorTextPosition, panelText, chars);
    }

    private List<string> UpdateViewText(List<string> panelText, float[] gridColumnPositions, float[] gridRowPositions, int textGridBoxesOffSetX, int textGridBoxesOffSetY, int gridBoxesSpaceOffRowNumbers)
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

            newViewText.Add(newString.ToString());
        }

        return newViewText;
    }

    private (Vector2, int, int) UpdateGridBoxesOffSet(List<string> viewText, float[] gridColumnPositions, float[] gridRowPositions , Vector2 cursorTextPosition, int textGridBoxesOffSetX, int textGridBoxesOffSetY, int gridBoxesSpaceOffRowNumbers)
    {
        int newTextGridBoxesOffSetX = textGridBoxesOffSetX;
        int newTextGridBoxesOffSetY = textGridBoxesOffSetY;

        //eerst kijken of cusro in viewpanelstaat
        if((int)cursorTextPosition.X >= gridColumnPositions.Length - 1 - gridBoxesSpaceOffRowNumbers + textGridBoxesOffSetX)
        {
            newTextGridBoxesOffSetX = (int)cursorTextPosition.X - gridColumnPositions.Length + 1 + gridBoxesSpaceOffRowNumbers +1;
            Console.WriteLine(newTextGridBoxesOffSetX);
        }
        
        return (cursorTextPosition, newTextGridBoxesOffSetX, newTextGridBoxesOffSetY);
    }
}