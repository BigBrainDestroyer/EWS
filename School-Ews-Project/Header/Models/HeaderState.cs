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
            {DrawHeaderObjectNames.BuildButton, new HeaderObjectPercentageWidthVariables(6.5f, 1)},
            {DrawHeaderObjectNames.MazeSize, new HeaderObjectPercentageWidthVariables(9f, 1)},
            {DrawHeaderObjectNames.BatchSize, new HeaderObjectPercentageWidthVariables(9f, 1)},
            {DrawHeaderObjectNames.PlayBackSpeed, new HeaderObjectPercentageWidthVariables(9f, 1)},
            {DrawHeaderObjectNames.RunFolderName, new HeaderObjectPercentageWidthVariables(16f, 1)}
        };

    public Dictionary<DrawHeaderObjectNames, Box> HeaderObjectBoxes { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Box>() {
            {DrawHeaderObjectNames.RunButton, new Box()},
            {DrawHeaderObjectNames.BuildButton, new Box()},
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
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), // UpTexture
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), //DownTexture
                
                new CustomBox( //counterUpClickField
                    new BoolVector4(false, true, false, false),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                
                new CustomBox( //counterDownClickField
                    new BoolVector4(false, false, true, false),
                    new Vector2(),
                    new Vector2(),
                    8
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
                
                new CenterTextInBox( //counterBox
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
                StaticVariablesInitialize.TextureArrowUp,
                StaticVariablesInitialize.TextureArrowDown,
                42.5f,
                Color.Red, //textureUpColor
                Color.Blue, //textureDownColor
                Color.Red, //textureUpBackgroundClickFieldColor
                Color.Red //textureDownBackgroundClickFieldColor
            )},

            {DrawHeaderObjectNames.BatchSize, new StringSlider(
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), // UpTexture
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), //DownTexture
                
                new CustomBox( //counterUpClickField
                    new BoolVector4(false, true, false, false),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                
                new CustomBox( //counterDownClickField
                    new BoolVector4(false, false, true, false),
                    new Vector2(),
                    new Vector2(),
                    8
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
                
                new CenterTextInBox( //counterBox
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
                StaticVariablesInitialize.TextureArrowUp,
                StaticVariablesInitialize.TextureArrowDown,
                42.5f,
                Color.Red, //textureUpColor
                Color.Blue, //textureDownColor
                Color.Red, //textureUpBackgroundClickFieldColor
                Color.Red //textureDownBackgroundClickFieldColor
            )},

            {DrawHeaderObjectNames.PlayBackSpeed, new StringSlider(
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), // UpTexture
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), //DownTexture
                
                new CustomBox( //counterUpClickField
                    new BoolVector4(false, true, false, false),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                
                new CustomBox( //counterDownClickField
                    new BoolVector4(false, false, true, false),
                    new Vector2(),
                    new Vector2(),
                    8
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
                
                new CenterTextInBox( //counterBox
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
                StaticVariablesInitialize.TextureArrowUp,
                StaticVariablesInitialize.TextureArrowDown,
                42.5f,
                Color.Red, //textureUpColor
                Color.Blue, //textureDownColor
                Color.Red, //textureUpBackgroundClickFieldColor
                Color.Red //textureDownBackgroundClickFieldColor
            )},

            {DrawHeaderObjectNames.RunFolderName, new StringSlider(
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), // UpTexture
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()), //DownTexture
                
                new CustomBox( //counterUpClickField
                    new BoolVector4(false, true, false, false),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                
                new CustomBox( //counterDownClickField
                    new BoolVector4(false, false, true, false),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                
                new CenterTextInBox( //titleBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("", 0, 0),
                    new Vector2(),
                    new Vector2(),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.Red,
                    Color.Red
                ),
                
                new CenterTextInBox( //counterBox
                    new Font(),
                    new CustomBox(
                        new BoolVector4(true, false, false, true),
                        new Vector2(),
                        new Vector2(),
                        8
                    ),
                    new RaylibText("ProjectNaam", 0, 0),
                    new Vector2(),
                    new Vector2(10,4),
                    StaticVariablesInitialize.FontLilexBoldPath,
                    Color.DarkBlue,
                    Color.Red
                ),
                StaticVariablesInitialize.TextureArrowUp,
                StaticVariablesInitialize.TextureArrowDown,
                0,
                Color.Red, //textureUpColor
                Color.Blue, //textureDownColor
                Color.Red, //textureUpBackgroundClickFieldColor
                Color.Red //textureDownBackgroundClickFieldColor
            )}
        };
}