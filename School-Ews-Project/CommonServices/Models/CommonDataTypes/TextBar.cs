using System.Numerics;
using Raylib_cs;

namespace Program;

public class TextBar
{
    public Color BackgroundColor { get; set; }
    public Color TextureColor { get; set; }
    public Color CursorColor { get; set; }
    public Color TextColor { get; set; }
    
    public Font TextFont { get; set; }

    public CustomBox Box { get; set; }
    public RaylibTexture Texture2D { get; set; }

    public RaylibText Text { get; set; } = new RaylibText("", 0, 0);

    public Vector2 ViewTextPixelPosition { get; set; }
    public Box CursorBox { get; set; }
    
    public string ViewText { get; set; } = "";
    public string FontPath { get; set; } = "";

    public float TextureToTextWhiteSpace { get; set; }

    public int CursorTextPosition { get; set; }
    public int CursorTextOffSet { get; set; }
    public int TextTypingWhiteSpaceRight { get; set; } = 2;
    public int TextTypingWhiteSpaceLeft { get; set; } = 0;
    public int MaxCharHeightOffSet { get; set; } = 0;

    public bool IsCursorVisible { get; set; } = true;
    public bool IsBoxActive { get; set; }
}