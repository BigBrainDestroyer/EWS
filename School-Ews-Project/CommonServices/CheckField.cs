using System.Numerics;

namespace Program;

public class CheckField
{
    public static bool MouseOnField(Vector2 fieldPixelPosition, Vector2 fieldPixelLength, Vector2 currentMousePosition)
    {
        if(currentMousePosition.X < fieldPixelPosition.X || currentMousePosition.Y < fieldPixelPosition.Y || currentMousePosition.X > fieldPixelPosition.X + fieldPixelLength.X || currentMousePosition.Y > fieldPixelPosition.Y + fieldPixelLength.Y)
        {
            return false;
        }

        return true;
    }
}