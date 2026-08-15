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
        if(_state.common.input.KeyboardCharInput.Count > 0 || Raylib.IsKeyPressed(KeyboardKey.Backspace) || Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            _state.editor.layout.textPanel.PanelText = 
            UpdatingText(_state.editor.layout.textPanel.PanelText, _state.common.input.KeyboardCharInput);
        }        
    }

    private List<string> UpdatingText(List<string> panelText, List<int> chars)
    {
        StringBuilder newString = new StringBuilder(panelText[(int)_state.editor.layout.textPanel.CursorPosition.Y]);

        if(Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            (_state.editor.layout.textPanel.CursorPosition, newString, panelText) = 
                UpdateSpaceToText(_state.editor.layout.textPanel.CursorPosition, newString, panelText);
        }
        
        if(Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            (_state.editor.layout.textPanel.CursorPosition, newString, panelText) = 
                UpdateEnterToText(_state.editor.layout.textPanel.CursorPosition, newString, panelText);
        }

        if(Raylib.IsKeyPressed(KeyboardKey.Backspace))
        {
            (_state.editor.layout.textPanel.CursorPosition, newString, panelText) = 
                UpdateBackspaceToString(_state.editor.layout.textPanel.CursorPosition, newString, panelText);
        }

        return panelText;
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
        if(cursorPosition.X == 0 && cursorPosition.Y == 1)//weet niet zeker 1 of 0 maar dat zien we wel
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
}