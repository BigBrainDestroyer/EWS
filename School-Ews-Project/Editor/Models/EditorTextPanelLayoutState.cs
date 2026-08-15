using System.Numerics;

namespace Program;

public class EditorTextPanelLayoutState
{
    public List<string> PanelText { get; set; } = new List<string>();
    
    public float[] GridColumnPositions { get; set; } = new float[0];
    public float[] GridRowPositions { get; set; } = new float[0];

    public Vector2 CursorPosition { get; set; } = new Vector2(0,0);
    
    public int TextSize { get; set; } = 8;
    public int charSpacing { get; set; } = 0;
}