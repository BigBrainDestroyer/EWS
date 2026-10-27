using Raylib_cs;

namespace Program;

public class RaylibTexture
{
    public Texture2D Texture { get; set; }
    public Rectangle TextureUsage { get; set; }
    public Rectangle TextureSize { get; set; }

    public RaylibTexture(Texture2D texture, Rectangle textureUsage, Rectangle textureSize)
    {
        Texture = texture;
        TextureUsage = textureUsage;
        TextureSize = textureSize;
    }
}