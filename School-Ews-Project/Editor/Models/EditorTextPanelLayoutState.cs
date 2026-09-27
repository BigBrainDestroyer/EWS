using System.Numerics;
using Raylib_cs;

namespace Program;

public class EditorTextPanelLayoutState
{
    public Color TextPanelBackgroundColor { get; set; } = new Color(51, 59, 67, 255);
    public Color TerminalBackgroundColor { get; set; } = new Color(51, 59, 67, 255);
    
    public Color CursorColor { get; set; } = new Color(95, 179, 179, 255);
    public Color TextColor { get; set; } = new Color(220, 223, 228, 255);
    public Color RowNumberColor { get; set; } = new Color(145, 155, 165, 255);
    public Color ActiveRowNumberColor { get; set; } = new Color(220, 223, 228, 255);

    public List<string> PanelText { get; set; } = new List<string>() {""};
    public List<string> ViewText { get; set; } = new List<string>() {""};

    public string CurrentLoadedFile { get; set; } = "";

    public float[] GridColumnPositions { get; set; } = new float[0];
    public float[] GridRowPositions { get; set; } = new float[0];

    public BoolVector4 textRowNumberBoxCornerStates { get; set; } = new BoolVector4(true, false, false, true);

    public Vector2 WriteOverBoxPositionHorizontalText { get; set; } = new Vector2();
    public Vector2 WriteOverBoxPositionVerticalText { get; set; } = new Vector2();
    public Vector2 WriteOverBoxLengthsHorizontalText { get; set; } = new Vector2();
    public Vector2 WriteOverBoxLengthsVerticalText { get; set; } = new Vector2();
    
    public Vector2 TextRowNumberBoxPositions { get; set; } = new Vector2(0,0);
    public Vector2 TextRowNumberBoxLengths { get; set; } = new Vector2(0,0);
    public Vector2 RowNumbersPosition { get; set; } = new Vector2(0, 0);
    public Vector2 TextPosition { get; set; } = new Vector2(0, 0);
    public Vector2 CursorPosition { get; set; } = new Vector2(0,0);
    public Vector2 CursorTextPosition { get; set; } = new Vector2(0,0);
    
    public string TapTextSpaces { get; set; } = "    ";//4

    public int ExtraGridBoxSpaceBetweenTextAndRowNumbers { get; set; } = 0;
    public int GridBoxesSpaceOffRowNumbers { get; set; } = 3;
    
    public int TextGridBoxesOffSetX { get; set; } = 0;
    public int TextGridBoxesOffSetY { get; set; } = 0;

    public int TextNavigationWhiteSpaceRight { get; set; } = 2;
    public int TextNavigationWhiteSpaceLeft { get; set; } = 1;
    public int TextNavigationWhiteSpaceDown { get; set; } = 1;
    public int TextNavigationWhiteSpaceUp { get; set; } = 1;

    public int TextHorizontalPixelOffSet { get; set; } = 0;
    public int TextVerticalPixelOffSet { get; set; } = 0;

    public int CursorWidth { get; set; }
    public int TextSize { get; set; } = 25;
    public int CharSpacing { get; set; } = 0;
}
