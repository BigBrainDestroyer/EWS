using Raylib_cs;
using System.Numerics;

namespace Program;

public static class CalculateCenterTextInBox
{
    public static CenterTextInBox Calculate(CenterTextInBox currentCenterTextInBox)
    {
        Vector2 whiteSpacePixelLengths = CalculateTextBorderWhiteSpacingLengths(currentCenterTextInBox.Box.PixelLengths, currentCenterTextInBox.TextBorderWhiteSpacingPercentageLengths);

        if(currentCenterTextInBox.Font.BaseSize == 0) currentCenterTextInBox.Font = Load.UpdateMonoSpaceFontEx(currentCenterTextInBox.Font, currentCenterTextInBox.FontPath, 10, currentCenterTextInBox.MaxFontOffSet);
        
        Vector2 totalTextSize =
            Raylib.MeasureTextEx(
                currentCenterTextInBox.Font,
                "t",
                currentCenterTextInBox.Box.PixelLengths.Y - whiteSpacePixelLengths.Y,
                currentCenterTextInBox.Text.TextSpacing
            );

        if (totalTextSize.X * currentCenterTextInBox.Text.Text.Length > currentCenterTextInBox.Box.PixelLengths.X - whiteSpacePixelLengths.X)
        {
            int charHeight = (int)((totalTextSize.Y / totalTextSize.X) * ((currentCenterTextInBox.Box.PixelLengths.X - whiteSpacePixelLengths.X)/ currentCenterTextInBox.Text.Text.Length));

            currentCenterTextInBox.Text = new RaylibText(currentCenterTextInBox.Text.Text, charHeight, currentCenterTextInBox.Text.TextSpacing);

            currentCenterTextInBox.Font = Load.UpdateMonoSpaceFontEx(currentCenterTextInBox.Font, currentCenterTextInBox.FontPath, (int)currentCenterTextInBox.Text.TextSize, currentCenterTextInBox.MaxFontOffSet);
            
            currentCenterTextInBox.TextPosition =
                new Vector2(
                    currentCenterTextInBox.Box.PixelPosition.X + (whiteSpacePixelLengths.X/2),
                    currentCenterTextInBox.Box.PixelPosition.Y + (whiteSpacePixelLengths.Y / 2) + ((currentCenterTextInBox.Box.PixelLengths.Y - whiteSpacePixelLengths.Y - charHeight)/2)
                );
        }
        else
        {
            currentCenterTextInBox.Text = new RaylibText(currentCenterTextInBox.Text.Text, totalTextSize.Y, currentCenterTextInBox.Text.TextSpacing);

            currentCenterTextInBox.Font = Load.UpdateMonoSpaceFontEx(currentCenterTextInBox.Font, currentCenterTextInBox.FontPath, (int)currentCenterTextInBox.Text.TextSize, currentCenterTextInBox.MaxFontOffSet);

            currentCenterTextInBox.TextPosition =
                new Vector2(
                    currentCenterTextInBox.Box.PixelPosition.X + (whiteSpacePixelLengths.X / 2) + ((currentCenterTextInBox.Box.PixelLengths.X - whiteSpacePixelLengths.X - (totalTextSize.X * currentCenterTextInBox.Text.Text.Length))/2),
                    currentCenterTextInBox.Box.PixelPosition.Y + (whiteSpacePixelLengths.Y/2)
                );
        }

        return currentCenterTextInBox;
    }

    private static Vector2 CalculateTextBorderWhiteSpacingLengths(Vector2 parentBoxLengths, Vector2 whiteSpacePercentages)
    {
        return new Vector2(
            (whiteSpacePercentages.X/100f) * parentBoxLengths.X,
            (whiteSpacePercentages.Y/100f) * parentBoxLengths.Y
        );
    }
}