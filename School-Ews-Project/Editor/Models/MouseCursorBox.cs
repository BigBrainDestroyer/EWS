using Raylib_cs;

namespace Program;

public class MouseCursorBox
{
    public Color CursorBoxColor { get; }
    public Box CursorBox { get; set; }
    public bool IsCursorBoxActive { get; set; }

    public MouseCursorBox(Box cursorBox, bool isCursorBoxActive, Color cursorColor)
    {
        CursorBoxColor = cursorColor;
        CursorBox = cursorBox;
        IsCursorBoxActive = isCursorBoxActive;
    }
}