using System.Numerics;
using Raylib_cs;

namespace Program;

public static class CalculateDigitCounter
{
    public static DigitsCounter Calculate(DigitsCounter currentDigitCounter)
    {
        currentDigitCounter = CalculateTextureBoxes(currentDigitCounter);

        currentDigitCounter = CalculateTitleBox(currentDigitCounter);

        currentDigitCounter = CalculateCounterBox(currentDigitCounter);

        return currentDigitCounter;
    }

    private static DigitsCounter CalculateTextureBoxes(DigitsCounter currentDigitCounter)
    {
        currentDigitCounter.CounterUpClickField =
            new CustomBox(
                currentDigitCounter.CounterUpClickField.CornerStates,
                
                ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                    new Vector2(
                        currentDigitCounter.Box.PixelPosition.X + (currentDigitCounter.Box.PixelLengths.X - (currentDigitCounter.Box.PixelLengths.Y / 2)),
                        currentDigitCounter.Box.PixelPosition.Y
                    ) 
                    : 
                    currentDigitCounter.Box.PixelPosition,
                    
                ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                    new Vector2((currentDigitCounter.Box.PixelLengths.Y/2), (currentDigitCounter.Box.PixelLengths.Y/2)) : 
                    new Vector2(currentDigitCounter.Box.PixelLengths.X, (currentDigitCounter.Box.PixelLengths.Y/2)),
                    
                currentDigitCounter.CounterUpClickField.Radius
            );

        currentDigitCounter.CounterDownClickField =
            new CustomBox(
                    currentDigitCounter.CounterDownClickField.CornerStates,
                    
                    ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                        new Vector2(
                            currentDigitCounter.Box.PixelPosition.X + (currentDigitCounter.Box.PixelLengths.X - (currentDigitCounter.Box.PixelLengths.Y / 2)),
                            currentDigitCounter.Box.PixelPosition.Y + currentDigitCounter.CounterUpClickField.PixelLengths.Y
                        ) 
                        : 
                        new Vector2(
                            currentDigitCounter.Box.PixelPosition.X,
                            currentDigitCounter.Box.PixelPosition.Y + currentDigitCounter.CounterUpClickField.PixelLengths.Y
                        ),
                        
                    ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                        new Vector2((currentDigitCounter.Box.PixelLengths.Y/2), (currentDigitCounter.Box.PixelLengths.Y/2)) : 
                        new Vector2(currentDigitCounter.Box.PixelLengths.X, (currentDigitCounter.Box.PixelLengths.Y/2)),
                        
                    currentDigitCounter.CounterDownClickField.Radius
                );

        currentDigitCounter.UpTexture = 
            new RaylibTexture(
                currentDigitCounter.UpTexture.Texture,
                new Rectangle(0, 0, 100, 100),
                new Rectangle(
                    currentDigitCounter.CounterUpClickField.PixelPosition.X,
                    currentDigitCounter.CounterUpClickField.PixelPosition.Y,
                    currentDigitCounter.CounterUpClickField.PixelPosition.X + currentDigitCounter.CounterUpClickField.PixelLengths.X,
                    currentDigitCounter.CounterUpClickField.PixelPosition.Y + currentDigitCounter.CounterUpClickField.PixelLengths.Y
                )
            );

        currentDigitCounter.DownTexture = 
            new RaylibTexture(
                currentDigitCounter.DownTexture.Texture,
                new Rectangle(0, 0, 100, 100),
                new Rectangle(
                    currentDigitCounter.CounterDownClickField.PixelPosition.X,
                    currentDigitCounter.CounterDownClickField.PixelPosition.Y,
                    currentDigitCounter.CounterDownClickField.PixelPosition.X + currentDigitCounter.CounterUpClickField.PixelLengths.X,
                    currentDigitCounter.CounterDownClickField.PixelPosition.Y + currentDigitCounter.CounterUpClickField.PixelLengths.Y
                )
            );

        return currentDigitCounter;
    }

    private static DigitsCounter CalculateTitleBox(DigitsCounter currentDigitCounter)
    {
        currentDigitCounter.TitleBox.Box =
            new CustomBox(
                currentDigitCounter.TitleBox.Box.CornerStates,
                currentDigitCounter.Box.PixelPosition,
                ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                    new Vector2(
                        currentDigitCounter.Box.PixelLengths.X - currentDigitCounter.CounterUpClickField.PixelLengths.X,
                        currentDigitCounter.Box.PixelLengths.X * (currentDigitCounter.TitleBoxHeightPercentage/100)
                    )
                    :
                    new Vector2(),
                currentDigitCounter.TitleBox.Box.Radius
            );

        currentDigitCounter.TitleBox = CalculateCenterTextInBox.Calculate(currentDigitCounter.TitleBox);

        return currentDigitCounter;
    }

    private static DigitsCounter CalculateCounterBox(DigitsCounter currentDigitCounter)
    {
        currentDigitCounter.TitleBox.Box =
            new CustomBox(
                currentDigitCounter.CounterBox.Box.CornerStates,
                new Vector2(
                    currentDigitCounter.TitleBox.Box.PixelPosition.X,
                    currentDigitCounter.TitleBox.Box.PixelPosition.Y + currentDigitCounter.TitleBox.Box.PixelLengths.Y
                ),
                ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                    new Vector2(
                        currentDigitCounter.Box.PixelLengths.X - currentDigitCounter.CounterUpClickField.PixelLengths.X,
                        currentDigitCounter.Box.PixelLengths.X - currentDigitCounter.TitleBox.Box.PixelLengths.Y
                    )
                    :
                    new Vector2(),
                currentDigitCounter.CounterBox.Box.Radius
            );

        currentDigitCounter.CounterBox = CalculateCenterTextInBox.Calculate(currentDigitCounter.TitleBox);

        return currentDigitCounter;
    }
}