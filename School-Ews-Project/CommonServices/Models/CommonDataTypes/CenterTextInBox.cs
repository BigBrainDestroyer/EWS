using Raylib_cs;
using System.Numerics;

namespace Program;

public class CenterTextInBox
{
    public Font Font { get; set; }

    public Color BoxBackgroundColor { get; set; }
    public Color TextColor { get; set; }

    public CustomBox Box { get; set; }

    public RaylibText Text { get; set; }

    public Vector2 TextPosition { get; set; }

    public Vector2 TextBorderWhiteSpacingPercentageLengths { get; set; }

    public string FontPath { get; set; }
    
    public int MaxFontOffSet { get; } = 2;

    public CenterTextInBox(Font font, CustomBox box, RaylibText text, Vector2 textPosition, Vector2 textBorderWhiteSpacingPercentageLengths, string fontPath, Color boxBackgroundColor, Color textColor)
    {
        Font = font;
        BoxBackgroundColor = boxBackgroundColor;
        TextColor = textColor;
        Box = box;
        Text = text;
        TextPosition = textPosition;
        TextBorderWhiteSpacingPercentageLengths = textBorderWhiteSpacingPercentageLengths;
        FontPath = fontPath;
    }
}