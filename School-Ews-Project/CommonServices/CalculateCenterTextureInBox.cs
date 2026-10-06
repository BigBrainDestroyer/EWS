using System.Numerics;
using Raylib_cs;

namespace Program;

public static class CalculateCenterTextureInBox
{
    public static CenterTextureInBox Calculate(CenterTextureInBox currentCenterTextureInBox)
    {
        Vector2 whiteSpacePixelLengths = 
            CalculateTextureBorderWhiteSpacingLengths(
                currentCenterTextureInBox.Box.PixelLengths, 
                currentCenterTextureInBox.TextureBorderWhiteSpacingPercentageLengths
            );

        Vector2 texturePixelLengths = CalculateTexturePixelLengths(
            new Vector2(
                currentCenterTextureInBox.Box.PixelLengths.X - whiteSpacePixelLengths.X, 
                currentCenterTextureInBox.Box.PixelLengths.Y - whiteSpacePixelLengths.Y
            ));

        Vector2 texturePixelPosition = CalculateTexturePixelPosition(
            currentCenterTextureInBox.Box.PixelPosition,
            currentCenterTextureInBox.Box.PixelLengths,
            texturePixelLengths
        );

        currentCenterTextureInBox.Texture = 
            new RaylibTexture(
                currentCenterTextureInBox.Texture.Texture,
                new Rectangle(0, 0, 100, 100),
                new Rectangle(
                    texturePixelPosition.X, 
                    texturePixelPosition.Y, 
                    texturePixelLengths.X, 
                    texturePixelLengths.X
                )
            );

        return currentCenterTextureInBox;
    }

    private static Vector2 CalculateTextureBorderWhiteSpacingLengths(Vector2 parentBoxLengths, Vector2 whiteSpacePercentages)
    {
        return new Vector2(
            (whiteSpacePercentages.X/100f) * parentBoxLengths.X,
            (whiteSpacePercentages.Y/100f) * parentBoxLengths.Y
        );
    }

    private static Vector2 CalculateTexturePixelLengths(Vector2 textureUsableBoxPixelLengths)
    {
        return (textureUsableBoxPixelLengths.X < textureUsableBoxPixelLengths.Y) ? 
            new Vector2(textureUsableBoxPixelLengths.X, textureUsableBoxPixelLengths.X) : 
            new Vector2(textureUsableBoxPixelLengths.Y, textureUsableBoxPixelLengths.Y
            );
    }

    private static Vector2 CalculateTexturePixelPosition(Vector2 parentBoxPositions, Vector2 parentBoxLengths, Vector2 textureLengths)
    {
        return new Vector2(
            parentBoxPositions.X + ((parentBoxLengths.X - textureLengths.X)/2), 
            parentBoxPositions.Y + ((parentBoxLengths.Y - textureLengths.Y)/2)
        );
    }
}