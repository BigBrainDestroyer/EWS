using Raylib_cs;
using System.Numerics;

namespace Program;

public class CalculateButton
{
    //AANAMEN
    // HEEFT AL EEN BASIS:
    // TEXTSPACING//
    // CORNERRADUIS//
    // TEXT//
    // COLORS//
    // CORNERSTATES//

    public Button CalculateButtonText(Button currentButton)
    {
        Vector2 whiteSpacePixelLengths = CalculateTextBorderWhiteSpacingLengths(currentButton.Box.PixelLengths, currentButton.TextBorderWhiteSpacingPercentageLengths);

        Vector2 totalTextSize =
            Raylib.MeasureTextEx(
                currentButton.Font,
                "t",
                currentButton.Box.PixelLengths.Y - whiteSpacePixelLengths.Y,
                currentButton.Text.TextSpacing
            );

        if (totalTextSize.X * currentButton.Text.Text.Length> currentButton.Box.PixelLengths.X - whiteSpacePixelLengths.X)
        {
            int charHeight = (int)((totalTextSize.Y / totalTextSize.X) * ((currentButton.Box.PixelLengths.X - whiteSpacePixelLengths.X)/ currentButton.Text.Text.Length));

            currentButton.Text = new RaylibText(currentButton.Text.Text, charHeight, currentButton.Text.TextSpacing);

            currentButton.TextPosition =
                new Vector2(
                    currentButton.Box.PixelPosition.X + (whiteSpacePixelLengths.X/2),
                    currentButton.Box.PixelPosition.Y + (whiteSpacePixelLengths.Y / 2) + ((currentButton.Box.PixelLengths.Y - whiteSpacePixelLengths.Y + charHeight)/2)
                );
        }
        else
        {
            currentButton.Text = new RaylibText(currentButton.Text.Text, totalTextSize.Y, currentButton.Text.TextSpacing);
            
            currentButton.TextPosition =
                new Vector2(
                    currentButton.Box.PixelPosition.X + (whiteSpacePixelLengths.X / 2) + ((currentButton.Box.PixelLengths.X - whiteSpacePixelLengths.X + (totalTextSize.X * currentButton.Text.Text.Length))/2),
                    currentButton.Box.PixelPosition.Y + (whiteSpacePixelLengths.Y/2)
                );
        }

        return currentButton;
    }

    private Vector2 CalculateTextBorderWhiteSpacingLengths(Vector2 parentBoxLengths, Vector2 whiteSpacePercentages)
    {
        return new Vector2(
            (whiteSpacePercentages.X/100f) * parentBoxLengths.X,
            (whiteSpacePercentages.Y/100f) * parentBoxLengths.Y
        );
    }
}