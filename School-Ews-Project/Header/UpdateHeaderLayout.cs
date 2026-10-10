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

        (_header.HeaderButtons, _header.HeaderStringSlider, _header.HeaderCenterTextureInBox) = CalculateObjects(_header.HeaderButtons, _header.HeaderStringSlider, _header.HeaderCenterTextureInBox, _header.HeaderObjectBoxes, _header.HeaderStringSliderColors, _header.HeaderButtonColors, _header.HeaderObjectAvailable);
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

    private (Dictionary<DrawHeaderObjectNames, CenterTextInBox>, Dictionary<DrawHeaderObjectNames, StringSlider>, Dictionary<DrawHeaderObjectNames, CenterTextureInBox>) CalculateObjects(Dictionary<DrawHeaderObjectNames, CenterTextInBox> headerButtons, Dictionary<DrawHeaderObjectNames, StringSlider> headerStringSlider, Dictionary<DrawHeaderObjectNames, CenterTextureInBox> headerCenterTextureInBox, Dictionary<DrawHeaderObjectNames, Box> headerObjectBoxes, Dictionary<DrawHeaderObjectNames, StringSliderColors> headerStringSliderColors, Dictionary<DrawHeaderObjectNames, ButtonColors> headerButtonColors, Dictionary<DrawHeaderObjectNames, bool> headerObjectAvailable)
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

                if(headerObjectAvailable[headerObjectName])
                {
                    headerButtons[headerObjectName].TextColor = headerButtonColors[headerObjectName].SymbolColors.X;
                    headerButtons[headerObjectName].BoxBackgroundColor = headerButtonColors[headerObjectName].BoxColors.X;
                }
                else
                {
                    headerButtons[headerObjectName].TextColor = headerButtonColors[headerObjectName].SymbolColors.Z;
                    headerButtons[headerObjectName].BoxBackgroundColor = headerButtonColors[headerObjectName].BoxColors.Z;
                }     
                    
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

                if(headerObjectAvailable[headerObjectName])
                {
                    headerStringSlider[headerObjectName].UpTexture.TextureColor = headerStringSliderColors[headerObjectName].TextureUpColors.X;
                    headerStringSlider[headerObjectName].UpTexture.BoxBackgroundColor = headerStringSliderColors[headerObjectName].UpClickFieldColors.X;
                    headerStringSlider[headerObjectName].DownTexture.TextureColor = headerStringSliderColors[headerObjectName].TextureDownColors.X;
                    headerStringSlider[headerObjectName].DownTexture.BoxBackgroundColor = headerStringSliderColors[headerObjectName].DownClickFieldColors.X;
                    headerStringSlider[headerObjectName].TitleStringBox.TextColor = headerStringSliderColors[headerObjectName].TitleTextColors.X;
                    headerStringSlider[headerObjectName].StringSliderBox.TextColor = headerStringSliderColors[headerObjectName].StringSliderTextColors.X;
                    headerStringSlider[headerObjectName].TitleStringBox.BoxBackgroundColor = headerStringSliderColors[headerObjectName].TitleBoxColors.X;
                    headerStringSlider[headerObjectName].StringSliderBox.BoxBackgroundColor = headerStringSliderColors[headerObjectName].StringSliderBoxColors.X;
                }
                else
                {
                    headerStringSlider[headerObjectName].UpTexture.TextureColor = headerStringSliderColors[headerObjectName].TextureUpColors.Z;
                    headerStringSlider[headerObjectName].UpTexture.BoxBackgroundColor = headerStringSliderColors[headerObjectName].UpClickFieldColors.Z;
                    headerStringSlider[headerObjectName].DownTexture.TextureColor = headerStringSliderColors[headerObjectName].TextureDownColors.Z;
                    headerStringSlider[headerObjectName].DownTexture.BoxBackgroundColor = headerStringSliderColors[headerObjectName].DownClickFieldColors.Z;
                    headerStringSlider[headerObjectName].TitleStringBox.TextColor = headerStringSliderColors[headerObjectName].TitleTextColors.Y;
                    headerStringSlider[headerObjectName].StringSliderBox.TextColor = headerStringSliderColors[headerObjectName].StringSliderTextColors.Y;
                    headerStringSlider[headerObjectName].TitleStringBox.BoxBackgroundColor = headerStringSliderColors[headerObjectName].TitleBoxColors.Y;
                    headerStringSlider[headerObjectName].StringSliderBox.BoxBackgroundColor = headerStringSliderColors[headerObjectName].StringSliderBoxColors.Y;
                }
                    
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

                if(headerObjectAvailable[headerObjectName])
                {
                    headerCenterTextureInBox[headerObjectName].TextureColor = headerButtonColors[headerObjectName].SymbolColors.X;
                    headerCenterTextureInBox[headerObjectName].BoxBackgroundColor = headerButtonColors[headerObjectName].BoxColors.X;
                }
                else
                {
                    headerCenterTextureInBox[headerObjectName].TextureColor = headerButtonColors[headerObjectName].SymbolColors.Z;
                    headerCenterTextureInBox[headerObjectName].BoxBackgroundColor = headerButtonColors[headerObjectName].BoxColors.Z;
                }                  
                
                headerCenterTextureInBox[headerObjectName] = CalculateCenterTextureInBox.Calculate(headerCenterTextureInBox[headerObjectName]);
            }
        }

        return (headerButtons, headerStringSlider, headerCenterTextureInBox);
    }
}