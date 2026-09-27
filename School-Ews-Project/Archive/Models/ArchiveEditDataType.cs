using Raylib_cs;
using System.Numerics;

namespace Program;

public class ArchiveEditDataType
{
    public Color RenameTextColor { get; set; }
    public Color DeleteTextColor { get; set; }

    public Vector2 DrawTextPixelPosition { get; set; }

    public ArchiveStorageNames DataType { get; set; }
    public string DataTypePath { get; set; } = "";

    public string RenameText { get; } = "Rename";
    public string DeleteText { get; } = "Delete";

    public int TextHight { get; set; }
    
    public bool IsEditBoxActive { get; set; }
}