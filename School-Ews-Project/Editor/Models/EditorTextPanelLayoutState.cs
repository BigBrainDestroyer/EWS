using System.Numerics;
using Raylib_cs;

namespace Program;

public class EditorTextPanelLayoutState
{
    public Color CursorColor { get; set; } = new Color(95, 179, 179, 255);

    public List<string> PanelText { get; set; } = new List<string>() {""};
    
    public float[] GridColumnPositions { get; set; } = new float[0];
    public float[] GridRowPositions { get; set; } = new float[0];

    public Vector2 CursorPosition { get; set; } = new Vector2(0,0);

    public int CursorWidth { get; set; }
    public int TextSize { get; set; } = 20;
    public int CharSpacing { get; set; } = 0;
}