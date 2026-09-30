using Raylib_cs;
using System.Numerics;

namespace Program;

public class ArchiveEditDataType
{
    public Color RenameTextColor { get; set; }
    public Color DeleteTextColor { get; set; }

    public Font TextFont { get; set; }

    public CustomBox DeleteClickField { get; set; } = new CustomBox(new BoolVector4(false, false, false, true), new Vector2(0,0), new Vector2(0,0), 0);
    public CustomBox RenameClickField { get; set; } = new CustomBox(new BoolVector4(false, false, true, false), new Vector2(0,0), new Vector2(0,0), 0);

    public Vector2 DrawTextPixelPosition { get; set; }

    public ArchiveStorageNames DataType { get; set; }
    public string DataTypePath { get; set; } = "";

    public string DeleteText { get; } = "Delete";
    public string RenameText { get; } = "Rename";

    public float WhiteSpacePercentageBetweenText { get; } = 20f;
    public float WhiteSpacePixelLengthBetweenText { get; set; }
    public float CharHeight { get; set; }
    public float CharSpacing { get; set; }
    public bool IsEditBoxActive { get; set; }
}

//Implementatie
// Juiste Data Typen Achterhalen 
// Juiste Actie Met Het DataTypen AchterHalen
// Uitvoeren Van die Actie
// 
// Visuals 
// Kleur Verandering 
// Text Is Box Active