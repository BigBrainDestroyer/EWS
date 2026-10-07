using System.Numerics;
using Raylib_cs;

namespace Program;

public static class CalculateStringSlider
{
    public static StringSlider Calculate(StringSlider currentStringSlider)
    {
        currentStringSlider = CalculateTextureBoxes(currentStringSlider);

        if(currentStringSlider.TitleStringBoxHeightPercentage > 0)
        {
            currentStringSlider = CalculateTitleBox(currentStringSlider);
        }
        
        currentStringSlider = CalculateCounterBox(currentStringSlider);

        return currentStringSlider;
    }

    private static StringSlider CalculateTextureBoxes(StringSlider currentStringSlider)
    {
        currentStringSlider.UpTexture.Box =
            new CustomBox(
                currentStringSlider.UpTexture.Box.CornerStates,
                
                ((currentStringSlider.Box.PixelLengths.Y / 2) < currentStringSlider.Box.PixelLengths.X) ?
                    new Vector2(
                        currentStringSlider.Box.PixelPosition.X + (currentStringSlider.Box.PixelLengths.X - (currentStringSlider.Box.PixelLengths.Y / 2)),
                        currentStringSlider.Box.PixelPosition.Y
                    ) 
                    : 
                    currentStringSlider.Box.PixelPosition,
                    
                ((currentStringSlider.Box.PixelLengths.Y / 2) < currentStringSlider.Box.PixelLengths.X) ?
                    new Vector2((currentStringSlider.Box.PixelLengths.Y/2), (currentStringSlider.Box.PixelLengths.Y/2)) : 
                    new Vector2(currentStringSlider.Box.PixelLengths.X, (currentStringSlider.Box.PixelLengths.Y/2)),
                    
                currentStringSlider.UpTexture.Box.Radius
            );

        currentStringSlider.DownTexture.Box =
            new CustomBox(
                    currentStringSlider.DownTexture.Box.CornerStates,
                    
                    ((currentStringSlider.Box.PixelLengths.Y / 2) < currentStringSlider.Box.PixelLengths.X) ?
                        new Vector2(
                            currentStringSlider.Box.PixelPosition.X + (currentStringSlider.Box.PixelLengths.X - (currentStringSlider.Box.PixelLengths.Y / 2)),
                            currentStringSlider.Box.PixelPosition.Y + currentStringSlider.DownTexture.Box.PixelLengths.Y
                        ) 
                        : 
                        new Vector2(
                            currentStringSlider.Box.PixelPosition.X,
                            currentStringSlider.Box.PixelPosition.Y + currentStringSlider.DownTexture.Box.PixelLengths.Y
                        ),
                        
                    ((currentStringSlider.Box.PixelLengths.Y / 2) < currentStringSlider.Box.PixelLengths.X) ?
                        new Vector2((currentStringSlider.Box.PixelLengths.Y/2), (currentStringSlider.Box.PixelLengths.Y/2)) : 
                        new Vector2(currentStringSlider.Box.PixelLengths.X, (currentStringSlider.Box.PixelLengths.Y/2)),
                        
                    currentStringSlider.DownTexture.Box.Radius
                );

        currentStringSlider.UpTexture = CalculateCenterTextureInBox.Calculate(currentStringSlider.UpTexture);
        currentStringSlider.DownTexture = CalculateCenterTextureInBox.Calculate(currentStringSlider.DownTexture);

        return currentStringSlider;
    }

    private static StringSlider CalculateTitleBox(StringSlider currentStringSlider)
    {
        currentStringSlider.TitleStringBox.Box =
            new CustomBox(
                currentStringSlider.TitleStringBox.Box.CornerStates,
                currentStringSlider.Box.PixelPosition,
                ((currentStringSlider.Box.PixelLengths.Y / 2) < currentStringSlider.Box.PixelLengths.X) ?
                    new Vector2(
                        currentStringSlider.Box.PixelLengths.X - currentStringSlider.UpTexture.Box.PixelLengths.X,
                        currentStringSlider.Box.PixelLengths.Y * (currentStringSlider.TitleStringBoxHeightPercentage/100f)
                    )
                    :
                    new Vector2(),
                currentStringSlider.TitleStringBox.Box.Radius
            );

        currentStringSlider.TitleStringBox = CalculateCenterTextInBox.Calculate(currentStringSlider.TitleStringBox);

        return currentStringSlider;
    }

    private static StringSlider CalculateCounterBox(StringSlider currentStringSlider)
    {
        currentStringSlider.StringSliderBox.Box =
            new CustomBox(
                currentStringSlider.StringSliderBox.Box.CornerStates,
                new Vector2(
                    currentStringSlider.Box.PixelPosition.X,
                    currentStringSlider.Box.PixelPosition.Y + currentStringSlider.TitleStringBox.Box.PixelLengths.Y
                ),
                ((currentStringSlider.Box.PixelLengths.Y / 2) < currentStringSlider.Box.PixelLengths.X) ?
                    new Vector2(
                        currentStringSlider.Box.PixelLengths.X - currentStringSlider.DownTexture.Box.PixelLengths.X,
                        currentStringSlider.Box.PixelLengths.Y - currentStringSlider.TitleStringBox.Box.PixelLengths.Y
                    )
                    :
                    new Vector2(),
                currentStringSlider.StringSliderBox.Box.Radius
            );

        currentStringSlider.StringSliderBox = CalculateCenterTextInBox.Calculate(currentStringSlider.StringSliderBox);

        return currentStringSlider;
    }
}