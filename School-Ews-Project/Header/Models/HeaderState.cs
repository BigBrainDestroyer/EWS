using Raylib_cs;
using System.Numerics;

namespace Program;

public class HeaderState
{
    //Colors
    public Color HeaderBackgroundColor { get; } = new Color(18, 22, 26, 255);

    //Colors
    public Dictionary<DrawHeaderObjectNames, StringSliderColors> HeaderStringSliderColors { get; set; } =
        new Dictionary<DrawHeaderObjectNames, StringSliderColors>() {
            {DrawHeaderObjectNames.MazeSize, new StringSliderColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray)
            )},
            {DrawHeaderObjectNames.BatchSize, new StringSliderColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray)
            )},
            {DrawHeaderObjectNames.PlayBackSpeed, new StringSliderColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray)
            )},
            {DrawHeaderObjectNames.RunFolderName, new StringSliderColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.DarkGray, Color.DarkBlue),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray)
            )}
        };

    public Dictionary<DrawHeaderObjectNames, ButtonColors> HeaderButtonColors { get; set; } =
        new Dictionary<DrawHeaderObjectNames, ButtonColors>() {
            {DrawHeaderObjectNames.RunButton, new ButtonColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray) 
            )},
            {DrawHeaderObjectNames.BuildButton, new ButtonColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray) 
            )},
            {DrawHeaderObjectNames.TerminalRequest, new ButtonColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray) 
            )},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, new ButtonColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray) 
            )},
            {DrawHeaderObjectNames.BuildMaze, new ButtonColors(
                new ReactionColorCombinations(Color.Red, Color.LightGray),
                new ReactionColorCombinations(Color.DarkBlue, Color.DarkGray) 
            )}
        };

    public float TotalHeaderObjectVerticalPercentageWhiteSpace { get; } = 20;

    public Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables> HeaderObjectWidthPercentages { get; set; } =
        new Dictionary<DrawHeaderObjectNames, HeaderObjectPercentageWidthVariables>(){
            {DrawHeaderObjectNames.RunButton, new HeaderObjectPercentageWidthVariables(6.5f, 1)},
            {DrawHeaderObjectNames.BuildButton, new HeaderObjectPercentageWidthVariables(6.5f, 1)},
            {DrawHeaderObjectNames.TerminalRequest, new HeaderObjectPercentageWidthVariables(4f, 1)},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, new HeaderObjectPercentageWidthVariables(6.5f, 1)},
            {DrawHeaderObjectNames.BuildMaze, new HeaderObjectPercentageWidthVariables(4f, 1)},
            {DrawHeaderObjectNames.MazeSize, new HeaderObjectPercentageWidthVariables(9f, 1)},
            {DrawHeaderObjectNames.BatchSize, new HeaderObjectPercentageWidthVariables(9f, 1)},
            {DrawHeaderObjectNames.PlayBackSpeed, new HeaderObjectPercentageWidthVariables(9f, 1)},
            {DrawHeaderObjectNames.RunFolderName, new HeaderObjectPercentageWidthVariables(16f, 1)}
        };

    public Dictionary<DrawHeaderObjectNames, Box> HeaderObjectBoxes { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Box>() {
            {DrawHeaderObjectNames.RunButton, new Box()},
            {DrawHeaderObjectNames.BuildButton, new Box()},
            {DrawHeaderObjectNames.TerminalRequest, new Box()},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, new Box()},
            {DrawHeaderObjectNames.BuildMaze, new Box()},
            {DrawHeaderObjectNames.MazeSize, new Box()},
            {DrawHeaderObjectNames.BatchSize, new Box()},
            {DrawHeaderObjectNames.PlayBackSpeed, new Box()},
            {DrawHeaderObjectNames.RunFolderName, new Box()}
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
                Color.DarkBlue,
                Color.Red
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
                Color.DarkBlue,
                Color.Red
            )}
        };

    public Dictionary<DrawHeaderObjectNames, StringSlider> HeaderStringSlider { get; set; } =
        new Dictionary<DrawHeaderObjectNames, StringSlider>(){
            {DrawHeaderObjectNames.MazeSize, new StringSlider(   
                new CenterTextureInBox(//UpClickField
                    new CustomBox( 
                        new BoolVector4(false, true, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                    new Vector2(10, 10),
                    Color.Red, 
                    Color.Red
                ),

                new CenterTextureInBox(//DownpClickField
                    new CustomBox(
                        new BoolVector4(false, false, true, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowDown, new Rectangle(), new Rectangle()),
                    new Vector2(10, 10),
                    Color.Red, 
                    Color.Red
                ),
                
                new CenterTextInBox( //titleBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(true, false, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("MazeSize", 0, 0),
                    new Vector2(),
                    new Vector2(10,7),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                
                new CenterTextInBox( //StringSliderBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(false, false, false, true),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("counter", 0, 0),
                    new Vector2(),
                    new Vector2(10,4),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                42.5f
            )},

            {DrawHeaderObjectNames.BatchSize, new StringSlider(   
                new CenterTextureInBox(//UpClickField
                    new CustomBox( 
                        new BoolVector4(false, true, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                    new Vector2(10,10),
                    Color.Red, 
                    Color.Red
                ),

                new CenterTextureInBox(//DownpClickField
                    new CustomBox(
                        new BoolVector4(false, false, true, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowDown, new Rectangle(), new Rectangle()),
                    new Vector2(10,10),
                    Color.Red, 
                    Color.Red
                ),
                
                new CenterTextInBox( //titleBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(true, false, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("BatchSize", 0, 0),
                    new Vector2(),
                    new Vector2(10,7),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                
                new CenterTextInBox( //StringSliderBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(false, false, false, true),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("counter", 0, 0),
                    new Vector2(),
                    new Vector2(10,4),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                42.5f
            )},

            {DrawHeaderObjectNames.PlayBackSpeed, new StringSlider(   
                new CenterTextureInBox(//UpClickField
                    new CustomBox( 
                        new BoolVector4(false, true, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                    new Vector2(10,10),
                    Color.Red, 
                    Color.Red
                ),

                new CenterTextureInBox(//DownpClickField
                    new CustomBox(
                        new BoolVector4(false, false, true, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowDown, new Rectangle(), new Rectangle()),
                    new Vector2(10,10),
                    Color.Red, 
                    Color.Red
                ),
                
                new CenterTextInBox( //titleBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(true, false, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("PlayBackSpeed", 0, 0),
                    new Vector2(),
                    new Vector2(10,7),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                
                new CenterTextInBox( //StringSliderBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(false, false, false, true),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("counter", 0, 0),
                    new Vector2(),
                    new Vector2(10,4),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                42.5f
            )},

            {DrawHeaderObjectNames.RunFolderName, new StringSlider(   
                new CenterTextureInBox(//UpClickField
                    new CustomBox( 
                        new BoolVector4(false, true, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                    new Vector2(10,10),
                    Color.Red, 
                    Color.Red
                ),

                new CenterTextureInBox(//DownpClickField
                    new CustomBox(
                        new BoolVector4(false, false, true, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibTexture(StaticVariablesInitialize.TextureArrowDown, new Rectangle(), new Rectangle()),
                    new Vector2(10,10),
                    Color.Red, 
                    Color.Red
                ),
                
                new CenterTextInBox( //titleBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(false, false, false, false),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("", 0, 0),
                    new Vector2(),
                    new Vector2(10,7),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                
                new CenterTextInBox( //StringSliderBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(true, false, false, true),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("ProjectName", 0, 0),
                    new Vector2(),
                    new Vector2(10,4),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                0
            )},
        };

    public Dictionary<DrawHeaderObjectNames, CenterTextureInBox> HeaderCenterTextureInBox { get; set; } =
        new Dictionary<DrawHeaderObjectNames, CenterTextureInBox>() {
            {DrawHeaderObjectNames.TerminalRequest, new CenterTextureInBox(
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                new Vector2(10,10),
                Color.Blue,
                Color.Red
            )},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, new CenterTextureInBox(
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                new Vector2(40,10),
                Color.Blue,
                Color.Red
            )},
            {DrawHeaderObjectNames.BuildMaze, new CenterTextureInBox(
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                new RaylibTexture(StaticVariablesInitialize.TextureArrowUp, new Rectangle(), new Rectangle()),
                new Vector2(10,10),
                Color.Blue,
                Color.Red
            )}
        };
}