using System.Numerics;

namespace Program;

public struct CustomBox
{
    public BoolVector4 CornerStates;
    public Vector2 PixelPosition;
    public Vector2 PixelLengths;
    public float Radius;

    public CustomBox(BoolVector4 cornerStates , Vector2 pixelPosition, Vector2 pixelLengths, float radius)
    {
        CornerStates = cornerStates;
        PixelPosition = pixelPosition;
        PixelLengths = pixelLengths;
        Radius = radius;
    }
}