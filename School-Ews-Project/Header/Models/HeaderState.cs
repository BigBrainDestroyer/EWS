using Raylib_cs;
using System.Numerics;

namespace Program;

public class HeaderState
{
    //Colors
    public Color HeaderBackgroundColor { get; } = new Color(18, 22, 26, 255);

    //BackgroundColors

    
    //HighLightColors



    //Buttons
    public BoolVector4 CornerStates { get; } = new BoolVector4(true, true, true, true);

    public float TotalHeaderObjectVerticalPercentageWhiteSpace { get; } = 20;

    public Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables> HeaderObjectWidthPercentages { get; set; } =
        new Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables>(){
            {DrawHeaderObjectNames.RunButton, new HeaderObjectPercentageWidthVariables(6.5f, 1)},
            {DrawHeaderObjectNames.BuildButton, new HeaderObjectPercentageWidthVariables(6.5f, 1)}
        };
        
    public Dictionary<DrawHeaderObjectNames, Box> HeaderObjectBoxes { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Box>() {
            {DrawHeaderObjectNames.RunButton, new Box()},
            {DrawHeaderObjectNames.BuildButton, new Box()}
        };

    public Dictionary<DrawHeaderObjectNames, CenterTextInBox> HeaderButtons { get; set; } =
        new Dictionary<DrawHeaderObjectNames, CenterTextInBox>() { 
            {DrawHeaderObjectNames.RunButton, new CenterTextInBox( 
                new Font(),
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                new RaylibText("Run", 0, 0),
                new Vector2(),
                new Vector2(34,20),
                StaticVariablesInitialize.FontLilexBoldPath,
                true,
                Color.DarkBlue,
                Color.Red,
                Color.SkyBlue,
                Color.Orange
            )},
            {DrawHeaderObjectNames.BuildButton, new CenterTextInBox( 
                new Font(),
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                new RaylibText("Build", 0, 0),
                new Vector2(),
                new Vector2(10,20),
                StaticVariablesInitialize.FontLilexBoldPath,
                true,
                Color.DarkBlue,
                Color.Red,
                Color.SkyBlue,
                Color.Orange
            )}
        };

    //public Dictionary<DrawHeaderObjectNames, CenterTextInBox>
}