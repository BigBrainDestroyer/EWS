
namespace Program;

public class EditorTextPanelLayoutState
{
    public float[] GridColumnPositions { get; set; } = new float[0];
    public float[] GridRowPositions { get; set; } = new float[0];

    public int TextSize { get; set; } = 8;
    public int charSpacing { get; set; } = 0;

    public List<string> PanelText { get; set; } = new List<string>();
}