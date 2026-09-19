using System.Numerics;

namespace Program;

public struct Box
{
    public Vector2 PixelPosition;
    public Vector2 PixelLengths;

    public Box(Vector2 pixelPosition, Vector2 pixelLengths)
    {
        PixelPosition = pixelPosition;
        PixelLengths = pixelLengths;
    }
}