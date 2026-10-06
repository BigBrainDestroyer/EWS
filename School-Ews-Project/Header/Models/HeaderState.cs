using Raylib_cs;
using System.Numerics;

namespace Program;

public class HeaderState
{
    //Colors
    public Color HeaderBackgroundColor { get; } = new Color(18, 22, 26, 255);

    //BackgroundColors
    Dictionary<DrawHeaderObjectNames, Color> HeaderObjectBackgroundColors { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Color>() {
            {DrawHeaderObjectNames.RunButton, Color.DarkBlue},
            {DrawHeaderObjectNames.BuildButton, Color.DarkBlue},
            {DrawHeaderObjectNames.TerminalRequest, Color.DarkBlue},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, Color.DarkBlue},
            {DrawHeaderObjectNames.BuildMaze, Color.DarkBlue},
            {DrawHeaderObjectNames.MazeSize, Color.DarkBlue},
            {DrawHeaderObjectNames.BatchSize, Color.DarkBlue},
            {DrawHeaderObjectNames.PlayBackSpeed, Color.DarkBlue},
            {DrawHeaderObjectNames.RunFolderName, Color.DarkBlue}
        };

    Dictionary<DrawHeaderObjectNames, Color> HeaderObjectSymbolColors { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Color>() {
            {DrawHeaderObjectNames.RunButton, Color.Red},
            {DrawHeaderObjectNames.BuildButton, Color.Red},
            {DrawHeaderObjectNames.TerminalRequest, Color.Red},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, Color.Red},
            {DrawHeaderObjectNames.BuildMaze, Color.Red},
            {DrawHeaderObjectNames.MazeSize, Color.Red},
            {DrawHeaderObjectNames.BatchSize, Color.Red},
            {DrawHeaderObjectNames.PlayBackSpeed, Color.Red},
            {DrawHeaderObjectNames.RunFolderName, Color.Red}
        };

    //HighLightColors
    Dictionary<DrawHeaderObjectNames, Color> HeaderObjectBackgroundHighLightColors { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Color>() {
            {DrawHeaderObjectNames.RunButton, Color.Red},
            {DrawHeaderObjectNames.BuildButton, Color.Red},
            {DrawHeaderObjectNames.TerminalRequest, Color.Red},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, Color.Red},
            {DrawHeaderObjectNames.BuildMaze, Color.Red},
            {DrawHeaderObjectNames.MazeSize, Color.Red},
            {DrawHeaderObjectNames.BatchSize, Color.Red},
            {DrawHeaderObjectNames.PlayBackSpeed, Color.Red},
            {DrawHeaderObjectNames.RunFolderName, Color.Red}
        };
    
    Dictionary<DrawHeaderObjectNames, Color> HeaderObjectSymbolHighLightColors { get; set; } =
        new Dictionary<DrawHeaderObjectNames, Color>() {
            {DrawHeaderObjectNames.RunButton, Color.Red},
            {DrawHeaderObjectNames.BuildButton, Color.Red},
            {DrawHeaderObjectNames.TerminalRequest, Color.Red},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, Color.Red},
            {DrawHeaderObjectNames.BuildMaze, Color.Red},
            {DrawHeaderObjectNames.MazeSize, Color.Red},
            {DrawHeaderObjectNames.BatchSize, Color.Red},
            {DrawHeaderObjectNames.PlayBackSpeed, Color.Red},
            {DrawHeaderObjectNames.RunFolderName, Color.Red}
        };

    Dictionary<DrawHeaderObjectNames, bool> IsHighLigthColorVisible { get; set; } =
        new Dictionary<DrawHeaderObjectNames, bool>() {
            {DrawHeaderObjectNames.RunButton, false},
            {DrawHeaderObjectNames.BuildButton, false},
            {DrawHeaderObjectNames.TerminalRequest, false},
            {DrawHeaderObjectNames.StartStopAlgoPlayBack, false},
            {DrawHeaderObjectNames.BuildMaze, false},
            {DrawHeaderObjectNames.MazeSize, false},
            {DrawHeaderObjectNames.BatchSize, false},
            {DrawHeaderObjectNames.PlayBackSpeed, false},
            {DrawHeaderObjectNames.RunFolderName, false}
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

    public Dictionary<DrawHeaderObjectNames, CenterTextureInBox> HeaderCenterTextureInBox { get; set; } =
        new Dictionary<DrawHeaderObjectNames, CenterTextureInBox>() {
            {DrawHeaderObjectNames.TerminalRequest, new CenterTextureInBox(
                new CustomBox(
                    new BoolVector4(true, true, true, true),
                    new Vector2(),
                    new Vector2(),
                    8
                ),
                new RaylibTexture(
                    new Texture2D(),
                    new Rectangle(),
                    new Rectangle()
                ),
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
                new RaylibTexture(
                    new Texture2D(),
                    new Rectangle(),
                    new Rectangle()
                ),
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
                new RaylibTexture(
                    new Texture2D(),
                    new Rectangle(),
                    new Rectangle()
                ),
                new Vector2(10,10),
                Color.Blue,
                Color.Red
            )}
        };
}