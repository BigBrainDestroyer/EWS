using System.Numerics;
using Raylib_cs;

namespace Program;

public class EditorTextPanelLayoutState
{
    public Color CursorColor { get; set; } = new Color(95, 179, 179, 255);
    public Color TextColor { get; set; } = new Color(220, 223, 228, 255);

    public List<string> PanelText { get; set; } = new List<string>() {""};

    public float[] RownumbersColumnGrid { get; set; } = new float[0];
    public float[] RownumbersRowGrid { get; set; } = new float[0];
    public float[] GridColumnPositions { get; set; } = new float[0];
    public float[] GridRowPositions { get; set; } = new float[0];

    public Vector2 TextPosition { get; set; } = new Vector2(0, 0);
    public Vector2 CursorPosition { get; set; } = new Vector2(0,0);
    public Vector2 CursorTextPosition { get; set; } = new Vector2(0,0);

    public int RowNumbersGridSpacing { get; set; } = 1;
    public int RowNumbersGridSpace { get; set; } = 3;
    public int TextHorizontalPixelOffSet { get; set; } = 0;
    public int TextVerticalPixelOffSet { get; set; } = 0;
    public int CursorWidth { get; set; }
    public int TextSize { get; set; } = 25;
    public int CharSpacing { get; set; } = 0;
}