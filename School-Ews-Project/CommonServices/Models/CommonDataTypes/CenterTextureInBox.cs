
using Raylib_cs;
using System.Numerics;

namespace Program;

public class CenterTextureInBox
{
    public Color BoxBackgroundColor { get; set; }
    public Color TextureColor { get; set; }

    public CustomBox Box { get; set; }

    public RaylibTexture Texture { get; set; }

    public Vector2 TextureBorderWhiteSpacingPercentageLengths { get; set; }

    public CenterTextureInBox(CustomBox box, RaylibTexture texture, Vector2 textBorderWhiteSpacingPercentageLengths, Color boxBackgroundColor, Color textureColor)
    {
        BoxBackgroundColor = boxBackgroundColor;
        TextureColor = textureColor;
        Box = box;
        Texture = texture;
        TextureBorderWhiteSpacingPercentageLengths = textBorderWhiteSpacingPercentageLengths;
    }
}