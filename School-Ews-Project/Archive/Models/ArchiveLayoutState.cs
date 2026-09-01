using System.Numerics;
using Raylib_cs;

namespace Program;

public enum ArchiveBoxesNames
{
    Header,
    Archive,
    Edit
}

public enum ArchiveFunctionNames
{
    SearchBar,
    AddButton
}

public struct ArchiveDataTypeBox
{
    public Vector2 Percentages;
    public Vector2 Position;
    public Vector2 Length;
    public ArchiveStorageNames DataType;
    public string Name;

    public ArchiveDataTypeBox(Vector2 position, Vector2 length, ArchiveStorageNames dataType, string name)
    {
        Percentages = new Vector2(100f, 3f);
        Position = position;
        Length = length;
        DataType = dataType;
        Name = name;
    }
}

public struct ArchiveAddDataTypeMenu
{
    public ArchiveStorageNames[] AddLegalDataTypeToStorage;
    public Image[] Svg; 
    public string[] Text;
    public Vector2 SvgPosition;
    public Vector2 SvgSize;
    public float FontSize;

    public ArchiveAddDataTypeMenu(ArchiveStorageNames[] addLegalDataTypeStorage, Image[] svg, Vector2 svgPosition, Vector2 svgSize, float fontSize)
    {
        AddLegalDataTypeToStorage = addLegalDataTypeStorage;
        Svg = svg;
        Text = new string[] {"ProjectFolder", "Folder", "File"};
        SvgPosition = svgPosition;
        SvgSize = svgSize;
        FontSize = fontSize;
    }
}

public class ArchiveLayoutState
{
    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);

    public Image[] AddDataTypeSvg { get; set; } = new Image[3]; 
    
    public ArchiveDataTypeBox DataTypeBox { get; set; } = new ArchiveDataTypeBox();

    public Dictionary<ArchiveBoxesNames, Vector2> ArchiveBoxPixelPercentages { get; } =
        new Dictionary<ArchiveBoxesNames, Vector2>{
            { ArchiveBoxesNames.Header, new Vector2(100f, 7f)},
            { ArchiveBoxesNames.Archive, new Vector2(90f, 75f)},
            { ArchiveBoxesNames.Edit, new Vector2(90f, 10f)}
        };

    public Dictionary<ArchiveBoxesNames, Vector2> ArchiveBoxPixelPositions { get; set; } =
        new Dictionary<ArchiveBoxesNames, Vector2>{
            { ArchiveBoxesNames.Header, new Vector2()},
            { ArchiveBoxesNames.Archive, new Vector2()},
            { ArchiveBoxesNames.Edit, new Vector2()}
        };

    public Dictionary<ArchiveBoxesNames, Vector2> ArchiveBoxPixelLengths { get; set; } =
        new Dictionary<ArchiveBoxesNames, Vector2>{
            { ArchiveBoxesNames.Header, new Vector2()},
            { ArchiveBoxesNames.Archive, new Vector2()},
            { ArchiveBoxesNames.Edit, new Vector2()}
        };

    public Vector2 ArchiveFunctionBarBoxPixelLengths { get; set; } = new Vector2();
    public Vector2 ArchiveFunctionBarBoxPosition { get; set; } = new Vector2();

    public Vector2 SearchBarPixelLengths { get; set; } = new Vector2();
    public Vector2 SearchBarPixelPosition { get; set; } = new Vector2();

    public Vector2 AddOnButtonPixelLengths { get; set; } = new Vector2();
    public Vector2 AddOnButtonPixelPosition { get; set; } = new Vector2();

    public Vector2 ArchiveBoxLengths { get; set; } = new Vector2();

    public float ArchiveFunctionBarBoxPercentageWhiteSpace { get; } = 20f;
    public float ArchiveFunctionBarBoxPixelWhiteSpace { get; set; }

    public float ArchiveFunctionBarFunctionsPercentageWhiteSpace { get; } = 7f;
    public float ArchiveFunctionBarFunctionsPixelWhiteSpace { get; set; }

    public float ArchiveWhiteSpaceToFunctionBar { get; set; } = 4f;
}