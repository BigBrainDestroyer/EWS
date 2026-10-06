using Raylib_cs;
using System.Numerics;

namespace Program;

public class Button
{
    public Font Font { get; set; }

    public Color ButtonBackgroundColor { get; set; }
    public Color ButtonTextColor { get; set; }
    public Color ButtonHighLightBackgroundColor { get; }
    public Color ButtonHighLightTextColor { get; set; }

    public CustomBox Box { get; set; }

    public RaylibText Text { get; set; }

    public Vector2 TextPosition { get; set; }

    public Vector2 TextBorderWhiteSpacingPercentageLengths { get; set; }
}