using Raylib_cs;
using System.Numerics;

namespace Program;

public class HeaderState
{
    //Colors
    public Color HeaderBackgroundColor { get; set; } = new Color(18, 22, 26, 255);

    //Buttons
    public BoolVector4 CornerStates { get; } = new BoolVector4(true, true, true, true);
    public float ButtonCornerRadius { get; } = 10;

    public float HeaderStartObjectPercentageWidth { get; } = 10;
    public float HeaderDrawObjectsTotalWhiteSpacePercentageY { get; } = 15;

    public Dictionary<DrawHeaderObjectNames, float> HeaderObjectWidthPercentages { get; set; } =
        new Dictionary<DrawHeaderObjectNames, float>(){
            {DrawHeaderObjectNames.RunButton, },
            {DrawHeaderObjectNames.BuildButton, float 6)}
        };

    public Dictionary<DrawHeaderObjectNames, Box> HeaderObjectBoxes { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Box>(){
            {DrawHeaderObjectNames.RunButton, new Box()},
            {DrawHeaderObjectNames.BuildButton, new Box()}
        };

}