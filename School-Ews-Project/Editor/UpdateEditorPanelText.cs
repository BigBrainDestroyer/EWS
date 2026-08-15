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
            (_state.editor.layout.textPanel.CursorPosition, _state.editor.layout.textPanel.PanelText, _state.common.input.KeyboardCharInput) = 
                UpdatingText(_state.editor.layout.textPanel.CursorPosition, _state.editor.layout.textPanel.PanelText, _state.common.input.KeyboardCharInput);
        }
    }

    private (Vector2, List<string>, List<int>) UpdatingText(Vector2 cursorPosition, List<string> panelText, List<int> chars)
    {
        StringBuilder newString = new StringBuilder(panelText[(int)_state.editor.layout.textPanel.CursorPosition.Y]);

        if(Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            (cursorPosition, newString, panelText) = 
                UpdateSpaceToText(cursorPosition, newString, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            (cursorPosition, newString, panelText) = 
                UpdateEnterToText(cursorPosition, newString, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Backspace))
        {
            (cursorPosition, newString, panelText) = 
                UpdateBackspaceToString(cursorPosition, newString, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            cursorPosition = 
                UpdateUpToText(cursorPosition, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            cursorPosition = 
                UpdateDownToText(cursorPosition, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            cursorPosition = 
                UpdateLeftToText(cursorPosition, panelText);
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            cursorPosition =
                UpdateRightToText(cursorPosition, panelText);
        }
        
        if(chars.Count > 0)
        {
            (cursorPosition, panelText, chars) =
                UpdateCharInputToText(cursorPosition, newString, panelText, chars);
        }
        
        return (cursorPosition, panelText, chars);
    }

    private (Vector2, StringBuilder, List<string>) UpdateSpaceToText(Vector2 cursorPosition, StringBuilder newString, List<string> panelText)
    {
        newString.Insert((int)cursorPosition.X, " ");        
        panelText[(int)cursorPosition.Y] = newString.ToString();

        cursorPosition.X += 1;

        return (cursorPosition, newString, panelText);
    }

    private (Vector2, StringBuilder, List<string>) UpdateEnterToText(Vector2 cursorPosition, StringBuilder newString, List<string> panelText)
    {
        
        string newRow = newString.ToString((int)cursorPosition.X, newString.Length - (int)cursorPosition.X);

        panelText[(int)cursorPosition.Y] = newString.ToString(0, (int)cursorPosition.X);

        cursorPosition.Y += 1;

        if(panelText.Count < cursorPosition.Y)
        {
            panelText.Add(newString.ToString(0, newString.Length - (int)cursorPosition.X));
        }
        else
        {
            panelText.Insert((int)cursorPosition.Y, newString.ToString(0, newString.Length - (int)cursorPosition.X));
        }

        cursorPosition.X = 0;

        newString = new StringBuilder(panelText[(int)cursorPosition.Y]);

        return (cursorPosition, newString, panelText);
    }

    private (Vector2, StringBuilder, List<string>) UpdateBackspaceToString(Vector2 cursorPosition, StringBuilder newString, List<string> panelText)
    {
        if(cursorPosition.X == 0 && cursorPosition.Y == 0)//weet niet zeker 1 of 0 maar dat zien we wel
        {
            return (cursorPosition, newString, panelText);
        }
        else if(cursorPosition.X == 0)
        {
            cursorPosition.X = panelText[(int)cursorPosition.Y - 1].Length;

            newString.Insert(0, panelText[(int)cursorPosition.Y - 1]);
            
            panelText.RemoveAt((int)cursorPosition.Y);

            cursorPosition.Y -= 1;

            panelText[(int)cursorPosition.Y] = newString.ToString();

            return (cursorPosition, newString, panelText);
        }

        cursorPosition.X -= 1;
        
        newString.Remove((int)cursorPosition.X, 1);
        
        panelText[(int)cursorPosition.Y] = newString.ToString();

        return (cursorPosition, newString, panelText);
    }

    private Vector2 UpdateUpToText(Vector2 cursorPosition, List<string> panelText)
    {
        if((int)cursorPosition.Y == 0) //Ik weet niet of dit 1 of 0 moet zijn maar ik denk 1
        {
            return cursorPosition;
        }
        else if(panelText[(int)cursorPosition.Y].Length > panelText[(int)cursorPosition.Y - 1].Length)
        {
            cursorPosition.Y -= 1;
            cursorPosition.X = panelText[(int)cursorPosition.Y].Length;

            return cursorPosition;
        }

        cursorPosition.Y -= 1;

        return cursorPosition;
    }

    private Vector2 UpdateDownToText(Vector2 cursorPosition, List<string> panelText)
    {
        if((int)cursorPosition.Y == panelText.Count)
        {
            return cursorPosition;
        }
        else if(panelText[(int)cursorPosition.Y].Length > panelText[(int)cursorPosition.Y + 1].Length)
        {
            cursorPosition.Y += 1;
            cursorPosition.X = panelText[(int)cursorPosition.Y].Length;

            return cursorPosition;
        }

        cursorPosition.Y += 1;

        return cursorPosition;
    }

    private Vector2 UpdateLeftToText(Vector2 cursorPosition, List<string> panelText)
    {
        if(cursorPosition.X == 0 && cursorPosition.Y == 0)//Ik weet niet of Y 0 of 1 is
        {
            return cursorPosition;
        }
        else if(cursorPosition.X == 0)
        {
            cursorPosition.Y -= 1;
            cursorPosition.X = panelText[(int)cursorPosition.Y].Length;

            return cursorPosition;
        }

        cursorPosition.X -= 1;
        
        return cursorPosition;
    }

    private Vector2 UpdateRightToText(Vector2 cursorPosition, List<string> panelText)
    {
        if(cursorPosition.X == panelText[(int)cursorPosition.Y].Length && cursorPosition.Y == panelText.Count)
        {
            return cursorPosition;
        }
        else if(cursorPosition.X == panelText[(int)cursorPosition.Y].Length)
        {
            cursorPosition.Y += 1;
            cursorPosition.X = 0;

            return cursorPosition;
        }

        cursorPosition.X += 1;
        
        return cursorPosition;
    }

    private (Vector2, List<string>, List<int>) UpdateCharInputToText(Vector2 cursorPosition, StringBuilder newString, List<string> panelText, List<int> chars)
    {
        foreach(int character in chars)
        {
            newString.Append((char)character);
        }

        chars = new List<int>();

        panelText[(int)cursorPosition.Y] = newString.ToString();

        return (cursorPosition, panelText, chars);
    }
}