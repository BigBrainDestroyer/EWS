using Raylib_cs;

namespace Program;

public class RaylibTexture
{
    Texture2D Texture { get; set; }
    Rectangle TextureUsage { get; set; }
    Rectangle TextureSize { get; set; }

    public RaylibTexture(Texture2D texture, Rectangle textureUsage, Rectangle textureSize)
    {
        Texture = texture;
        TextureUsage = textureUsage;
        TextureSize = textureSize;
    }
}