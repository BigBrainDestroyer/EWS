using System.Numerics;
using Raylib_cs;

namespace Program;

public static class Load
{
    public static Font LoadFontEx(string fontLocation, float fontSize)
    {
        Font newFont = Raylib.LoadFontEx(fontLocation, (int)fontSize, null, 0);
        Raylib.SetTextureFilter( newFont.Texture, TextureFilter.Bilinear);

        return newFont;
    }

    public static Texture2D LoadResizableTexture2D(string textureLocation)
    {
        Texture2D newTexture = Raylib.LoadTexture(textureLocation);

        Raylib.GenTextureMipmaps(ref newTexture);

        Raylib.SetTextureFilter(newTexture, TextureFilter.Trilinear);

        return newTexture;
    }
}
