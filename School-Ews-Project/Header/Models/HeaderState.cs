using Raylib_cs;
using System.Numerics;

namespace Program;

public class HeaderState
{
    //Colors
    public Color HeaderBackgroundColor { get; } = new Color(18, 22, 26, 255);

    //BackgroundColors
    public Color RunButtonBackgroundColor { get; } = Color.DarkBlue;
    public Color RunButtonTextColor { get; } = Color.Red;

    public Color BuildButtonBackgroundColor { get; } = Color.DarkBlue;
    public Color BuildButtonTextColor { get; } = Color.Red;
    
    //HighLightColors
    public Color RunButtonHighLightBackgroundColor { get; } = Color.SkyBlue;
    public Color RunButtonHighLightTextColor { get; } = Color.Orange;

    public Color BuildButtonHighLightBackgroundColor { get; } = Color.SkyBlue;
    public Color BuildButtonHighLightTextColor { get; } = Color.Orange;


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
        new Dictionary<DrawHeaderObjectNames, Box>() {
            {DrawHeaderObjectNames.RunButton, new Box()},
            {DrawHeaderObjectNames.BuildButton, new Box()}
        };

    public Dictionary<DrawHeaderObjectNames, Button> HeaderButtons { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Button>() { 
            {DrawHeaderObjectNames.RunButton, new Button( 
                new Font(),
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    10
                ),
                new RaylibText("Run", 0, 0),
                new Vector2(),
                new Vector2(5,10),
                Color.DarkBlue,
                Color.Red,
                Color.SkyBlue,
                Color.Orange
            )},
            {DrawHeaderObjectNames.BuildButton, new Button( 
                new Font(),
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    10
                ),
                new RaylibText("Build", 0, 0),
                new Vector2(),
                new Vector2(5,10),
                Color.DarkBlue,
                Color.Red,
                Color.SkyBlue,
                Color.Orange
            )}
        };
}