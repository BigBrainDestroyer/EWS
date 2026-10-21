using System.Numerics;

namespace Program;

public static class CalculateInformationViewer
{
    public static InformationViewer Calculate(InformationViewer currentInformationViewer, Box box)
    {
        currentInformationViewer.TitleBox.Box =
            new CustomBox(
                currentInformationViewer.TitleBox.Box.CornerStates,
                box.PixelPosition,
                new Vector2(
                    box.PixelLengths.X, 
                    (currentInformationViewer.TitleBoxVerticalPercentage/100f) * box.PixelLengths.Y
                ),
                currentInformationViewer.TitleBox.Box.Radius
            );

        currentInformationViewer.TitleBox = 
            CalculateCenterTextInBox.Calculate(currentInformationViewer.TitleBox);

        currentInformationViewer.InformationBox.Box =
            new CustomBox(
                currentInformationViewer.TitleBox.Box.CornerStates,
                new Vector2(
                    box.PixelPosition.X, 
                    box.PixelPosition.Y + currentInformationViewer.TitleBox.Box.PixelLengths.Y
                ),
                new Vector2(
                    box.PixelLengths.X, 
                    box.PixelLengths.Y - currentInformationViewer.TitleBox.Box.PixelLengths.Y
                ),
                currentInformationViewer.TitleBox.Box.Radius
            );
           
        currentInformationViewer.InformationBox = 
            CalculateCenterTextInBox.Calculate(currentInformationViewer.InformationBox);
            
        return currentInformationViewer;
    }
}