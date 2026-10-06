using Raylib_cs;
using System.Numerics;

namespace Program;

public class CenterTextInBox
{
    public Font Font;

    public Color ButtonBackgroundColor { get; set; }
    public Color ButtonTextColor { get; set; }
    public Color ButtonHighLightBackgroundColor { get; set; }
    public Color ButtonHighLightTextColor { get; set; }

    public CustomBox Box { get; set; }

    public RaylibText Text { get; set; }

    public Vector2 TextPosition { get; set; }

    public Vector2 TextBorderWhiteSpacingPercentageLengths { get; set; }

    public string FontPath { get; set; }
    public int MaxFontOffSet { get; } = 2;

    public bool IsActive { get; set; }

    public CenterTextInBox(Font font, CustomBox box, RaylibText text, Vector2 textPosition, Vector2 textBorderWhiteSpacingPercentageLengths, string fontPath, bool isActive, Color buttonBackgroundColor, Color buttonTextColor, Color buttonHighLightBackgroundColor, Color buttonHighLightTextColor)
    {
        Font = font;
        ButtonBackgroundColor = buttonBackgroundColor;
        ButtonTextColor = buttonTextColor;
        ButtonHighLightBackgroundColor = buttonHighLightBackgroundColor;
        ButtonHighLightTextColor = ButtonHighLightTextColor;
        Box = box;
        Text = text;
        TextPosition = textPosition;
        TextBorderWhiteSpacingPercentageLengths = textBorderWhiteSpacingPercentageLengths;
        FontPath = fontPath;
        IsActive = isActive;
    }
}