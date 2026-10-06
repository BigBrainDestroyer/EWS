using Raylib_cs;

namespace Program;

public class HeaderState
{
    //Colors
    public Color HeaderBackgroundColor { get; set; } = new Color(18, 22, 26, 255);

    //Buttons
    public BoolVector4 CornerStates { get; } = new BoolVector4(true, true, true, true);
    public float ButtonCornerRadius { get; } = 10;

    public float TotalHeaderObjectVerticalPercentageWhiteSpace { get; } = 10;

    public Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables> HeaderObjectWidthPercentages { get; set; } =
        new Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables>(){
            {DrawHeaderObjectNames.RunButton, new HeaderObjectPercentageWidthVariables(8, 2)},
            {DrawHeaderObjectNames.BuildButton, new HeaderObjectPercentageWidthVariables(8, 2)}
        };
        
    public Dictionary<DrawHeaderObjectNames, Box> HeaderObjectBoxes { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Box>(){
            {DrawHeaderObjectNames.RunButton, new Box()},
            {DrawHeaderObjectNames.BuildButton, new Box()}
        };
}