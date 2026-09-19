using System.Numerics;
using Raylib_cs;

namespace Program;

public class ArchiveDataTypeBox
{
    public Color BoxColor { get; set; }
    public Color TextColor { get; set; }
    public string ViewText { get; set; }
    public RaylibTexture Texture { get; set; }
    public RaylibText Text { get; set; }
    public Box Box { get; set; }

    public ArchiveDataTypeBox(Color boxColor, Color textColor, RaylibTexture texture, string viewText, RaylibText text, Box box)
    {
        BoxColor = boxColor;
        TextColor = textColor;
        ViewText = viewText;
        Texture = texture;
        Text = text;
        Box = box;
    }
}