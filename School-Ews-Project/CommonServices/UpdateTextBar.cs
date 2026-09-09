using Raylib_cs;
using System.Numerics;
using System.Text;

namespace Program;

public class UpdateCustomTextBar
{
    private readonly AppState _state;

    public UpdateCustomTextBar(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public TextBar UpdateTextBar(TextBar currentTextBar, List<int> chars, Vector2 boxPosition, Vector2 boxLengths)
    {
        TextBar newTextBar = currentTextBar;

        if(newTextBar.IsTextBoxActive)
        {
            StringBuilder newString = new StringBuilder(newTextBar.Text);

            newTextBar.CursorTextPosition = UpdateNavigationKeyBoardInput(newString, newTextBar.CursorTextPosition);
 
            (newString, newTextBar.CursorTextPosition) = UpdateBackSpace(newString, newTextBar.CursorTextPosition);
    
            (newString, newTextBar.CursorTextPosition) = UpdateNewCharsToText(chars , newString, newTextBar.CursorTextPosition, newTextBar.IsTextBoxActive);

            newTextBar.Text = newString.ToString();
        }

        newTextBar.BoxPosition = boxPosition;
        newTextBar.BoxLengths = boxLengths;

        int textAreaWidth = CalculateTextAreaWidth(newTextBar.BoxLengths, newTextBar.TextureSize, newTextBar.TextureToTextWhiteSpace);

        newTextBar.ViewText = 
            UpdateViewText(
                _state.common.font.EditorPanelText, 
                newTextBar.Text, 
                newTextBar.BoxLengths.Y, 
                newTextBar.CharSpacing, 
                newTextBar.CursorTextPosition, 
                textAreaWidth, 
                newTextBar.TextTypingWhiteSpaceRight, 
                newTextBar.TextTypingWhiteSpaceLeft
            );

        newTextBar.TextureSize = CalculateTextureSize(boxLengths);

        newTextBar.ViewTextPixelPosition = 
            CalculateViewTextPosition(
                boxPosition, 
                newTextBar.TextureSize, 
                newTextBar.TextureToTextWhiteSpace
            );

        return newTextBar;
    }

    private int UpdateNavigationKeyBoardInput(StringBuilder newString, int cursorTextPosition)
    {
        if(Raylib.IsKeyPressed(KeyboardKey.Left) && cursorTextPosition > 0)
        {
            cursorTextPosition -= 1;
        }

        if((Raylib.IsKeyPressed(KeyboardKey.Right) && cursorTextPosition < newString.Length))
        {
            cursorTextPosition += 1;
        }
        
        return cursorTextPosition;
    }

    private (StringBuilder, int) UpdateBackSpace(StringBuilder newString, int cursorTextPosition)
    {
        if(Raylib.IsKeyPressed(KeyboardKey.Backspace)  && cursorTextPosition > 0)
        {
            cursorTextPosition -= 1;
            newString.Remove(cursorTextPosition, 1);
        }

        return (newString, cursorTextPosition);
    }
    
    private (StringBuilder, int) UpdateNewCharsToText(List<int> chars, StringBuilder newString,  int cursorTextPosition, bool isTextBoxActive)
    {
        foreach(int character in chars)
        {
            newString.Insert((int)cursorTextPosition, (char)character);
            cursorTextPosition += 1;
        }

        return (newString, cursorTextPosition);
    }

    private int CalculateTextAreaWidth(Vector2 boxLengths, Vector2 textureSize, float textureToTextWhiteSpace)
    {
        return (int)boxLengths.X - (int)textureSize.X - (int)textureToTextWhiteSpace;
    }
    
    private string UpdateViewText(Font textFont, string text, float charSize, float charSpacing, int cursorTextPosition, int textAreaWidth, int textTypingWhiteSpaceRight, int textTypingWhiteSpaceLeft)
    {
        StringBuilder newViewText = new StringBuilder(text);

        Vector2 charDimensions = Raylib.MeasureTextEx(textFont, "t", charSize, charSpacing);

        float charColumnLength = (float)textAreaWidth/ charDimensions.X;

        int cursorTextOffSet = 0;

        if(cursorTextPosition - cursorTextOffSet >= charColumnLength - 1 - textTypingWhiteSpaceRight)
        {
            cursorTextOffSet = (int)MathF.Abs(charColumnLength - 1 - textTypingWhiteSpaceRight - cursorTextPosition);
        }
        else if(cursorTextPosition <= cursorTextOffSet + textTypingWhiteSpaceLeft && (int)cursorTextPosition >= textTypingWhiteSpaceLeft)
        {
            cursorTextOffSet = cursorTextPosition - textTypingWhiteSpaceLeft;
        }
        else if(cursorTextPosition <= cursorTextOffSet)
        {
            cursorTextOffSet = cursorTextPosition;
        }

        //Console.WriteLine(charColumnLength);
        //Console.WriteLine(cursorTextOffSet);
        //Console.WriteLine(cursorTextPosition);
        //Console.WriteLine(newViewText.Length);
        

        if(newViewText.Length >= cursorTextOffSet)
        {
            newViewText.Remove(0, cursorTextOffSet);

            //Console.WriteLine();
            //Console.WriteLine(newViewText.Length);
            //Console.WriteLine((int)charColumnLength);
            //Console.WriteLine(cursorTextOffSet);
            //Console.WriteLine(cursorTextOffSet);
            
            if(newViewText.Length >= (int)charColumnLength + cursorTextOffSet)
            {
                newViewText.Length = (int)charColumnLength;
            }
        }
        else
        {
            newViewText.Length = 0;
        }

        return newViewText.ToString();
    }

    private Vector2 CalculateTextureSize(Vector2 boxLengths)
    {
        return (boxLengths.X < boxLengths.Y) ? new Vector2(boxLengths.X, boxLengths.X) : new Vector2(boxLengths.Y, boxLengths.Y);
    }

    private Vector2 CalculateViewTextPosition(Vector2 boxPosition, Vector2 textureSize, float textureToTextWhiteSpace)
    {
        return new Vector2(boxPosition.X + textureSize.X + textureToTextWhiteSpace, boxPosition.Y);
    }
}