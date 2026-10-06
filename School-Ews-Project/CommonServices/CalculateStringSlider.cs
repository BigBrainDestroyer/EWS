using System.Numerics;
using Raylib_cs;

namespace Program;

public static class CalculateDigitCounter
{
    public static StringSlider Calculate(StringSlider currentDigitCounter)
    {
        currentDigitCounter = CalculateTextureBoxes(currentDigitCounter);

        currentDigitCounter = CalculateTitleBox(currentDigitCounter);

        currentDigitCounter = CalculateCounterBox(currentDigitCounter);

        return currentDigitCounter;
    }

    private static StringSlider CalculateTextureBoxes(StringSlider currentDigitCounter)
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

    private static StringSlider CalculateTitleBox(StringSlider currentDigitCounter)
    {
        currentDigitCounter.TitleStringBox.Box =
            new CustomBox(
                currentDigitCounter.TitleStringBox.Box.CornerStates,
                currentDigitCounter.Box.PixelPosition,
                ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                    new Vector2(
                        currentDigitCounter.Box.PixelLengths.X - currentDigitCounter.CounterUpClickField.PixelLengths.X,
                        currentDigitCounter.Box.PixelLengths.Y * (currentDigitCounter.TitleStringBoxHeightPercentage/100f)
                    )
                    :
                    new Vector2(),
                currentDigitCounter.TitleStringBox.Box.Radius
            );

        currentDigitCounter.TitleStringBox = CalculateCenterTextInBox.Calculate(currentDigitCounter.TitleStringBox);

        return currentDigitCounter;
    }

    private static StringSlider CalculateCounterBox(StringSlider currentDigitCounter)
    {
        currentDigitCounter.StringSliderBox.Box =
            new CustomBox(
                currentDigitCounter.StringSliderBox.Box.CornerStates,
                new Vector2(
                    currentDigitCounter.TitleStringBox.Box.PixelPosition.X,
                    currentDigitCounter.TitleStringBox.Box.PixelPosition.Y + currentDigitCounter.TitleStringBox.Box.PixelLengths.Y
                ),
                ((currentDigitCounter.Box.PixelLengths.Y / 2) < currentDigitCounter.Box.PixelLengths.X) ?
                    new Vector2(
                        currentDigitCounter.Box.PixelLengths.X - currentDigitCounter.CounterDownClickField.PixelLengths.X,
                        currentDigitCounter.Box.PixelLengths.Y - currentDigitCounter.TitleStringBox.Box.PixelLengths.Y
                    )
                    :
                    new Vector2(),
                currentDigitCounter.StringSliderBox.Box.Radius
            );

        currentDigitCounter.StringSliderBox = CalculateCenterTextInBox.Calculate(currentDigitCounter.StringSliderBox);

        return currentDigitCounter;
    }
}