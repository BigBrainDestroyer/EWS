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
        if(_state.common.input.KeyboardCharInput.Count > 0 || Raylib.IsKeyPressed(KeyboardKey.Backspace) || Raylib.IsKeyPressed(KeyboardKey.Enter) ||Raylib.IsKeyPressed(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            (_state.editor.layout.textPanel.CursorTextPosition, _state.editor.layout.textPanel.PanelText, _state.common.input.KeyboardCharInput) = 
                UpdatingText(_state.editor.layout.textPanel.CursorTextPosition, _state.editor.layout.textPanel.PanelText, _state.common.input.KeyboardCharInput);
        }
    }

    private (Vector2, List<string>, List<int>) UpdatingText(Vector2 cursorTextPosition, List<string> panelText, List<int> chars)
    {
        StringBuilder newString = new StringBuilder(panelText[(int)cursorTextPosition.Y]);

        if(Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            (cursorTextPosition, newString, panelText) = 
                UpdateSpaceToText(cursorTextPosition, newString, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Enter))
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
        
        return (cursorTextPosition, panelText, chars);
    }

    private (Vector2, StringBuilder, List<string>) UpdateSpaceToText(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText)
    {
        newString.Insert((int)cursorTextPosition.X, " ");        
        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        cursorTextPosition.X += 1;

        return (cursorTextPosition, newString, panelText);
    }

    private (Vector2, StringBuilder, List<string>) UpdateEnterToText(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText)
    {
        
        string newRow = newString.ToString((int)cursorTextPosition.X, newString.Length - (int)cursorTextPosition.X);

        panelText[(int)cursorTextPosition.Y] = newString.ToString(0, (int)cursorTextPosition.X);

        cursorTextPosition.Y += 1;

        if(panelText.Count < cursorTextPosition.Y)
        {
            panelText.Add(newString.ToString(0, newString.Length - (int)cursorTextPosition.X));
        }
        else
        {
            panelText.Insert((int)cursorTextPosition.Y, newString.ToString(0, newString.Length - (int)cursorTextPosition.X));
        }

        cursorTextPosition.X = 0;

        newString = new StringBuilder(panelText[(int)cursorTextPosition.Y]);

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
        if((int)cursorTextPosition.Y == 0) //Ik weet niet of dit 1 of 0 moet zijn maar ik denk 1
        {
            return cursorTextPosition;
        }
        else if(panelText[(int)cursorTextPosition.Y].Length > panelText[(int)cursorTextPosition.Y - 1].Length)
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
        if((int)cursorTextPosition.Y == panelText.Count)
        {
            return cursorTextPosition;
        }
        else if(panelText[(int)cursorTextPosition.Y].Length > panelText[(int)cursorTextPosition.Y + 1].Length)
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
        if(cursorTextPosition.X == 0 && cursorTextPosition.Y == 0)//Ik weet niet of Y 0 of 1 is
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
        if(cursorTextPosition.X == panelText[(int)cursorTextPosition.Y].Length && cursorTextPosition.Y == panelText.Count)
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
            newString.Append((char)character);
        }

        chars = new List<int>();

        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        return (cursorTextPosition, panelText, chars);
    }
}