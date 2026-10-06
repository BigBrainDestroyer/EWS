using System.Numerics;
using Raylib_cs;

namespace Program;

public struct BoolVector4
{
    public bool X;
    public bool Y;
    public bool Z;
    public bool W;

    public bool[] Array;

    public BoolVector4(bool x, bool y, bool z, bool w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;

        Array = new bool[4] {x, y, z, w};
    }
}

public static class Custom
{
    public static void DrawCustomRectangle(Vector2 position, Vector2 size, float radius, BoolVector4 boolVector4, Color color)
    {
        if(size.X < radius*2 || size.Y < radius*2)
        {
            Raylib.DrawRectangleV(position, size, color);
            return;
        }
        
        int counter = 0;

        Vector2 childBoxSize = new Vector2();
        
        Vector2 currentRectanglePosition = 
            new Vector2(
                position.X + radius, 
                position.Y + radius
            );

        Vector2 currentCirclePosition = 
            new Vector2(
                position.X + radius, 
                position.Y + radius
            );

        Vector2 mainBoxSize =
            new Vector2(
                size.X - radius - radius,
                size.Y - radius - radius
            );
            
        Raylib.DrawRectangleV(currentRectanglePosition, mainBoxSize, color);
        
        foreach(bool cornerState in boolVector4.Array)
        {
            if(cornerState == true)
            {
                switch(counter)
                {
                    case 0: 
                        currentRectanglePosition = new Vector2(position.X + radius, position.Y);
                        currentCirclePosition = new Vector2(position.X + radius, position.Y + radius);
                        childBoxSize = new Vector2(mainBoxSize.X, radius);
                        break;
                    case 1: 
                        currentRectanglePosition = new Vector2(position.X + size.X - radius, position.Y + radius);
                        currentCirclePosition = new Vector2(position.X + size.X - radius, position.Y + radius);
                        childBoxSize = new Vector2(radius, mainBoxSize.Y);
                        break;
                    case 2: 
                        currentRectanglePosition = new Vector2(position.X + radius, position.Y + size.Y - radius);
                        currentCirclePosition = new Vector2(position.X + size.X - radius, position.Y + size.Y - radius);
                        childBoxSize = new Vector2(mainBoxSize.X, radius);
                        break;
                    case 3: 
                        currentRectanglePosition = new Vector2(position.X, position.Y + radius); 
                        currentCirclePosition = new Vector2(position.X + radius, position.Y + size.Y - radius);
                        childBoxSize = new Vector2(radius, mainBoxSize.Y);
                        break;
                }

                Raylib.DrawRectangleV(currentRectanglePosition, childBoxSize, color);
                Raylib.DrawCircleV(currentCirclePosition, radius, color);
            }
            else
            {
                switch(counter)
                {
                    case 0: 
                        currentRectanglePosition = new Vector2(position.X, position.Y);
                        childBoxSize = new Vector2(mainBoxSize.X + radius, radius);
                        break;
                    case 1: 
                        currentRectanglePosition = new Vector2(position.X + size.X - radius, position.Y);
                        childBoxSize = new Vector2(radius, mainBoxSize.Y + radius);
                        break;
                    case 2: 
                        currentRectanglePosition = new Vector2(position.X + radius, position.Y + size.Y - radius);
                        childBoxSize = new Vector2(mainBoxSize.X + radius, radius);
                        break;
                    case 3: 
                        currentRectanglePosition = new Vector2(position.X, position.Y + radius); 
                        childBoxSize = new Vector2(radius, mainBoxSize.Y + radius);
                        break;
                }

                Raylib.DrawRectangleV(currentRectanglePosition, childBoxSize, color);
            }

            counter++;
        }

        currentRectanglePosition = new Vector2(position.X + radius, position.Y + radius);
        Raylib.DrawRectangleV(currentRectanglePosition, mainBoxSize, color);
    }

    public static void DrawCustomRectangleOutlining(Vector2 position, Vector2 size, float radius, BoolVector4 boolVector4, float outlineSize, Color outlineColor, Color rectangleColor)
    {
        DrawCustomRectangle(position, size, radius, boolVector4, outlineColor);

        float totalOutlineSize = outlineSize * 2;

        Vector2 newPosition =
            new Vector2(
                position.X + outlineSize,
                position.Y + outlineSize
            );

        Vector2 newSize =
            new Vector2(
                size.X - totalOutlineSize,
                size.Y - totalOutlineSize
            );

        DrawCustomRectangle(newPosition, newSize, radius, boolVector4, rectangleColor);
    }
}