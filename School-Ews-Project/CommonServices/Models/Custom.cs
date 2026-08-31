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

                Console.WriteLine(counter);

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
}