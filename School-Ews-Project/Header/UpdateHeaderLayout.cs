using System.Numerics;
using Raylib_cs;

namespace Program;

public class UpdateHeaderLayout
{
    private readonly AppState _state;

    public UpdateHeaderLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Update()
    {
        var _header = _state.HeaderState;
        var _boxes = _state.common.boxes;

        _header.HeaderObjectBoxes = CalculateHeaderObjectBoxes(_header.HeaderObjectWidthPercentages, _boxes.HeaderPixelPosition, _boxes.HeaderPixelLengths, _header.TotalHeaderObjectVerticalPercentageWhiteSpace);

        _header.HeaderButtons = CalculateHeaderButtons(new Font(), _header.HeaderButtons, _header.HeaderObjectBoxes);
    }
    
    private Dictionary<DrawHeaderObjectNames, Box> CalculateHeaderObjectBoxes(Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables> headerObjectWidthPercentages, Vector2 parentBoxPositions, Vector2 parentBoxLength, float totalHeaderObjectVerticalPercentageWhiteSpace)
    {
        float TotalHeaderObjectVerticalPixelWhiteSpace = CalculatePositionY(parentBoxLength, totalHeaderObjectVerticalPercentageWhiteSpace);

        Dictionary<DrawHeaderObjectNames, Box> newHeaderObjectBoxes =
            CalculateNewHeaderObjectBoxes(
                headerObjectWidthPercentages, 
                parentBoxPositions, 
                parentBoxLength, 
                TotalHeaderObjectVerticalPixelWhiteSpace
            );

        return newHeaderObjectBoxes;
    }

    private float CalculatePositionY(Vector2 parentBoxLengths, float TotalHeaderObjectVerticalPercentageWhiteSpace)
    {        
        return ((TotalHeaderObjectVerticalPercentageWhiteSpace/100) * parentBoxLengths.Y)/2;
    }

    private Dictionary<DrawHeaderObjectNames, Box> CalculateNewHeaderObjectBoxes(Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables> headerObjectWidthPercentages, Vector2 parentBoxPositions, Vector2 parentBoxLength, float totalHeaderObjectVerticalPixelWhiteSpace)
    {
        Dictionary<DrawHeaderObjectNames, Box> newHeaderObjectBoxes = new Dictionary<DrawHeaderObjectNames, Box>();

        DrawHeaderObjectNames[] headerObjectNames = Enum.GetValues<DrawHeaderObjectNames>();

        float objectPositionY = parentBoxPositions.Y + (totalHeaderObjectVerticalPixelWhiteSpace / 2);
        float totalObjectWidthPixelSpaceIncrement = 0;

        foreach(DrawHeaderObjectNames objectName in headerObjectNames)
        {

            float totalBoxPixelWidth = ((headerObjectWidthPercentages[objectName].TotalObjectWidthSpace / 100) * parentBoxLength.X);
            float totalBoxWhiteSpacePixelWidth = ((headerObjectWidthPercentages[objectName].TotalObjectWhiteSpaceWidth / 100) * parentBoxLength.X);
            
            newHeaderObjectBoxes.Add(
                objectName,
                new Box(
                    new Vector2(
                        parentBoxPositions.X + totalObjectWidthPixelSpaceIncrement + totalBoxWhiteSpacePixelWidth/2,
                        objectPositionY
                    ),
                    new Vector2(
                        totalBoxPixelWidth - totalBoxWhiteSpacePixelWidth,
                        parentBoxLength.Y - totalHeaderObjectVerticalPixelWhiteSpace
                    )
                )
            );

            totalObjectWidthPixelSpaceIncrement += totalBoxPixelWidth;
        }

        return newHeaderObjectBoxes;
    }

    private Dictionary<DrawHeaderObjectNames, Button> CalculateHeaderButtons(Font font, Dictionary<DrawHeaderObjectNames, Button> headerButtons, Dictionary<DrawHeaderObjectNames, Box> headerObjectBoxes)
    {
        foreach(DrawHeaderObjectNames headerButtonName in headerButtons.Keys)
        {
            headerButtons[headerButtonName].Font = _state.common.font.ArchiveDataBoxType;

            headerButtons[headerButtonName].Box = 
                new CustomBox(
                    headerButtons[headerButtonName].Box.CornerStates,
                    headerObjectBoxes[headerButtonName].PixelPosition,
                    headerObjectBoxes[headerButtonName].PixelLengths,
                    headerButtons[headerButtonName].Box.Radius
                );
                
            headerButtons[headerButtonName] = CalculateButton.CalculateButtonText(headerButtons[headerButtonName]);
        }

        return headerButtons;
    }
}