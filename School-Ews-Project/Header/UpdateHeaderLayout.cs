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

        (_header.HeaderButtons, _header.HeaderStringSlider, _header.HeaderCenterTextureInBox) = CalculateObjects(_header.HeaderButtons, _header.HeaderStringSlider, _header.HeaderCenterTextureInBox, _header.HeaderObjectBoxes, _header.HeaderStringSliderColors, _header.HeaderButtonColors);
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
        return ((TotalHeaderObjectVerticalPercentageWhiteSpace/100) * parentBoxLengths.Y);
    }

    private Dictionary<DrawHeaderObjectNames, Box> CalculateNewHeaderObjectBoxes(Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables> headerObjectWidthPercentages, Vector2 parentBoxPositions, Vector2 parentBoxLength, float totalHeaderObjectVerticalPixelWhiteSpace)
    {
        Dictionary<DrawHeaderObjectNames, Box> newHeaderObjectBoxes = new Dictionary<DrawHeaderObjectNames, Box>();

        DrawHeaderObjectNames[] headerObjectNames = Enum.GetValues<DrawHeaderObjectNames>();

        float objectPositionY = parentBoxPositions.Y + (totalHeaderObjectVerticalPixelWhiteSpace/2);
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

    private (Dictionary<DrawHeaderObjectNames, CenterTextInBox>, Dictionary<DrawHeaderObjectNames, StringSlider>, Dictionary<DrawHeaderObjectNames, CenterTextureInBox>) CalculateObjects(Dictionary<DrawHeaderObjectNames, CenterTextInBox> headerButtons, Dictionary<DrawHeaderObjectNames, StringSlider> headerStringSlider, Dictionary<DrawHeaderObjectNames, CenterTextureInBox> headerCenterTextureInBox, Dictionary<DrawHeaderObjectNames, Box> headerObjectBoxes, Dictionary<DrawHeaderObjectNames, StringSliderColors> headerStringSliderColors, Dictionary<DrawHeaderObjectNames, ButtonColors> headerButtonColors)
    {
        foreach(DrawHeaderObjectNames headerObjectName in headerObjectBoxes.Keys)
        { 
            if(headerButtons.ContainsKey(headerObjectName))
            {
                headerButtons[headerObjectName].Box = 
                    new CustomBox(
                        headerButtons[headerObjectName].Box.CornerStates,
                        headerObjectBoxes[headerObjectName].PixelPosition,
                        headerObjectBoxes[headerObjectName].PixelLengths,
                        headerButtons[headerObjectName].Box.Radius
                    );

                headerButtons[headerObjectName].TextColor = headerButtonColors[headerObjectName].SymbolColors.NormalColor;
                headerButtons[headerObjectName].BoxBackgroundColor = headerButtonColors[headerObjectName].BoxColors.NormalColor;
                    
                headerButtons[headerObjectName] = CalculateCenterTextInBox.Calculate(headerButtons[headerObjectName]);
            }
            else if(headerStringSlider.ContainsKey(headerObjectName))
            {
                headerStringSlider[headerObjectName].Box =
                    new CustomBox(
                        headerStringSlider[headerObjectName].Box.CornerStates,
                        headerObjectBoxes[headerObjectName].PixelPosition,
                        headerObjectBoxes[headerObjectName].PixelLengths,
                        headerStringSlider[headerObjectName].Box.Radius
                    );

                headerStringSlider[headerObjectName].UpTexture.TextureColor = headerStringSliderColors[headerObjectName].TextureUpColors.NormalColor;
                headerStringSlider[headerObjectName].UpTexture.BoxBackgroundColor = headerStringSliderColors[headerObjectName].UpClickFieldColors.NormalColor;
                headerStringSlider[headerObjectName].DownTexture.TextureColor = headerStringSliderColors[headerObjectName].TextureDownColors.NormalColor;
                headerStringSlider[headerObjectName].DownTexture.BoxBackgroundColor = headerStringSliderColors[headerObjectName].DownClickFieldColors.NormalColor;
                headerStringSlider[headerObjectName].TitleStringBox.TextColor = headerStringSliderColors[headerObjectName].TitleTextColors.NormalColor;
                headerStringSlider[headerObjectName].StringSliderBox.TextColor = headerStringSliderColors[headerObjectName].StringSliderTextColors.NormalColor;
                headerStringSlider[headerObjectName].TitleStringBox.BoxBackgroundColor = headerStringSliderColors[headerObjectName].TitleBoxColors.NormalColor;
                headerStringSlider[headerObjectName].StringSliderBox.BoxBackgroundColor = headerStringSliderColors[headerObjectName].StringSliderBoxColors.NormalColor;
                
                headerStringSlider[headerObjectName] = CalculateStringSlider.Calculate(headerStringSlider[headerObjectName]);
            }
            else if(headerCenterTextureInBox.ContainsKey(headerObjectName))
            {
                headerCenterTextureInBox[headerObjectName].Box =
                    new CustomBox(
                        headerCenterTextureInBox[headerObjectName].Box.CornerStates,
                        headerObjectBoxes[headerObjectName].PixelPosition,
                        headerObjectBoxes[headerObjectName].PixelLengths,
                        headerCenterTextureInBox[headerObjectName].Box.Radius
                    );

                headerCenterTextureInBox[headerObjectName].TextureColor = headerButtonColors[headerObjectName].SymbolColors.NormalColor;
                headerCenterTextureInBox[headerObjectName].BoxBackgroundColor = headerButtonColors[headerObjectName].BoxColors.NormalColor;
                
                headerCenterTextureInBox[headerObjectName] = CalculateCenterTextureInBox.Calculate(headerCenterTextureInBox[headerObjectName]);
            }
        }

        return (headerButtons, headerStringSlider, headerCenterTextureInBox);
    }
}