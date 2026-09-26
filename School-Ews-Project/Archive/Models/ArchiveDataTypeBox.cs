using Raylib_cs;

namespace Program;

public class ArchiveDataTypeBox
{
    public Color BoxColor { get; set; }
    public Color TextureColor { get; set; }
    public Color TextColor { get; set; }
    public string ViewText { get; set; }
    public string Location { get; set; }
    public RaylibTexture Texture { get; set; }
    public RaylibText Text { get; set; }
    public Box Box { get; set; }

    public ArchiveDataTypeBox(Color boxColor, Color textureColor, Color textColor, RaylibTexture texture, string viewText, string location, RaylibText text, Box box)
    {
        BoxColor = boxColor;
        TextureColor = textureColor;
        TextColor = textColor;
        ViewText = viewText;
        Location = location;
        Texture = texture;
        Text = text;
        Box = box;
    }
}
