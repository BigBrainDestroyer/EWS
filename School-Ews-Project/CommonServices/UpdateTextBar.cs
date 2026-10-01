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
        if (currentTextBar.IsBoxActive)
        {
            StringBuilder newString = new StringBuilder(currentTextBar.Text.Text);

            currentTextBar.CursorTextPosition = UpdateNavigationKeyBoardInput(newString, currentTextBar.CursorTextPosition);

            (newString, currentTextBar.CursorTextPosition) = UpdateBackSpace(newString, currentTextBar.CursorTextPosition);

            (newString, currentTextBar.CursorTextPosition) = UpdateNewCharsToText(chars, newString, currentTextBar.CursorTextPosition, currentTextBar.IsBoxActive);

            currentTextBar.Text = new RaylibText(newString.ToString(), boxLengths.Y, currentTextBar.Text.TextSpacing);
        }

        currentTextBar.Box = new CustomBox(currentTextBar.Box.CornerStates, boxPosition, boxLengths, currentTextBar.Box.Radius);

        currentTextBar.Texture2D.TextureSize = CalculateTextureSize(currentTextBar.Box.PixelPosition, currentTextBar.Box.PixelLengths);
        currentTextBar.Texture2D.TextureUsage = CalculateTextureUsage(currentTextBar.Texture2D.Texture);

        int textAreaWidth = CalculateTextAreaWidth(currentTextBar.Box.PixelLengths, new Vector2(currentTextBar.Texture2D.TextureSize.Width, currentTextBar.Texture2D.TextureSize.Height), currentTextBar.TextureToTextWhiteSpace);
        
        (currentTextBar.ViewText, currentTextBar.CursorTextOffSet) =
            UpdateViewText(
                _state.common.font.EditorPanelText,
                currentTextBar.Text.Text,
                currentTextBar.Box.PixelLengths.Y,
                currentTextBar.Text.TextSpacing,
                currentTextBar.CursorTextPosition,
                textAreaWidth,
                currentTextBar.TextTypingWhiteSpaceRight,
                currentTextBar.TextTypingWhiteSpaceLeft
            );

        currentTextBar.ViewTextPixelPosition =
            CalculateViewTextPosition(
                boxPosition,
                new Vector2(currentTextBar.Texture2D.TextureSize.Width, currentTextBar.Texture2D.TextureSize.Height),
                currentTextBar.TextureToTextWhiteSpace
            );

        if(currentTextBar.IsCursorVisible)
        {
            currentTextBar.CursorBox =
                CalculateCursorBox(
                    _state.common.font.EditorPanelText,
                    currentTextBar.Text,
                    currentTextBar.ViewTextPixelPosition,
                    currentTextBar.ViewText.Length,
                    currentTextBar.CursorTextPosition,
                    currentTextBar.CursorTextOffSet
                );
        }

        return currentTextBar;
    }

    private int UpdateNavigationKeyBoardInput(StringBuilder newString, int cursorTextPosition)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Left) && cursorTextPosition > 0)
        {
            cursorTextPosition -= 1;
        }

        if ((Raylib.IsKeyPressed(KeyboardKey.Right) && cursorTextPosition < newString.Length))
        {
            cursorTextPosition += 1;
        }

        return cursorTextPosition;
    }

    private (StringBuilder, int) UpdateBackSpace(StringBuilder newString, int cursorTextPosition)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && cursorTextPosition > 0)
        {
            cursorTextPosition -= 1;
            newString.Remove(cursorTextPosition, 1);
        }

        return (newString, cursorTextPosition);
    }

    private (StringBuilder, int) UpdateNewCharsToText(List<int> chars, StringBuilder newString, int cursorTextPosition, bool isTextBoxActive)
    {
        foreach (int character in chars)
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

    private (string, int) UpdateViewText(Font textFont, string text, float charSize, float charSpacing, int cursorTextPosition, int textAreaWidth, int textTypingWhiteSpaceRight, int textTypingWhiteSpaceLeft)
    {
        StringBuilder newViewText = new StringBuilder(text);

        Vector2 charDimensions = Raylib.MeasureTextEx(textFont, "t", charSize, charSpacing);

        float charColumnLength = (float)textAreaWidth / charDimensions.X;

        int cursorTextOffSet = 0;

        if (cursorTextPosition - cursorTextOffSet >= charColumnLength - 1 - textTypingWhiteSpaceRight)
        {
            cursorTextOffSet = (int)MathF.Abs(charColumnLength - 1 - textTypingWhiteSpaceRight - cursorTextPosition);
        }
        else if (cursorTextPosition <= cursorTextOffSet + textTypingWhiteSpaceLeft && (int)cursorTextPosition >= textTypingWhiteSpaceLeft)
        {
            cursorTextOffSet = cursorTextPosition - textTypingWhiteSpaceLeft;
        }
        else if (cursorTextPosition <= cursorTextOffSet)
        {
            cursorTextOffSet = cursorTextPosition;
        }

        if (newViewText.Length >= cursorTextOffSet)
        {
            newViewText.Remove(0, cursorTextOffSet);

            if (newViewText.Length >= (int)charColumnLength + cursorTextOffSet - cursorTextPosition)
            {
                newViewText.Length = (int)charColumnLength;
            }
        }
        else
        {
            newViewText.Length = 0;
        }

        return (newViewText.ToString(), cursorTextOffSet);
    }

    private Rectangle CalculateTextureSize(Vector2 boxPosition, Vector2 boxLengths)
    {
        return (boxLengths.X < boxLengths.Y) ? new Rectangle(boxPosition.X, boxPosition.Y, boxLengths.X, boxLengths.X) : new Rectangle(boxPosition.X, boxPosition.Y, boxLengths.Y, boxLengths.Y);
    }

    private Rectangle CalculateTextureUsage(Texture2D texture)
    {
        return new Rectangle(0,0, texture.Width, texture.Height);
    }

    private Vector2 CalculateViewTextPosition(Vector2 boxPosition, Vector2 textureSize, float textureToTextWhiteSpace)
    {
        return new Vector2(boxPosition.X + textureSize.X + textureToTextWhiteSpace, boxPosition.Y);
    }

    private Box CalculateCursorBox(Font textFont, RaylibText text, Vector2 textPosition, int viewTextLength, int cursorTextPosition, int cursorOffSet)
    {
        return new Box(
            new Vector2(
                textPosition.X + (Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X * (cursorTextPosition - cursorOffSet)),
                textPosition.Y
            ),
            Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing)
        );
    }
}