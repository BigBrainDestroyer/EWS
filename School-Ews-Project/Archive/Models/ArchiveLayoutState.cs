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

public enum ArchiveTexture2DNames
{
    ProjectFolder,
    Folder,
    File,
    OpenProjectFolder,
    OpenFolder,
    PlusSign
}

public struct ArchiveDataTypeBox
{
    public Vector2 Percentages = new Vector2(100f, 3f);
    public Vector2 Position;
    public Vector2 Length;
    public ArchiveStorageNames DataType;
    public string Name;

    public ArchiveDataTypeBox(Vector2 position, Vector2 length, ArchiveStorageNames dataType, string name)
    {
        Position = position;
        Length = length;
        DataType = dataType;
        Name = name;
    }
}

public struct ArchiveAddDataTypeMenu
{
    public ArchiveStorageNames?[] AddLegalDataTypeToStorage;

    public string[] Text = new string[] {
        "NewProject", 
        "NewFolder", 
        "NewFile"
    };

    public Vector2 BoxPosition;
    public Vector2 BoxLengths;

    public Vector2 SingleBoxLengths;

    public Vector2 Texture2DPosition;
    public Vector2 Texture2DSize;
    public float FontSize;
    public int LegalDataTypeCount;
    public bool IsMenuActive = false;

    public ArchiveAddDataTypeMenu(ArchiveStorageNames?[] addLegalDataTypeStorage, Vector2 boxPosition, Vector2 boxLengths, Vector2 singleBoxLengths, Vector2 texture2DPosition, Vector2 texture2DSize, bool isMenuActive)
    {
        AddLegalDataTypeToStorage = addLegalDataTypeStorage;
        BoxPosition = boxPosition;
        BoxLengths = boxLengths;
        SingleBoxLengths = singleBoxLengths;
        Texture2DPosition = texture2DPosition;
        Texture2DSize = texture2DSize;
        IsMenuActive = isMenuActive;
    }
}

public struct ArchiveNamingDataTypeBox
{
    public Texture2D Texture;
    public Vector2 TextureSize;
    public Vector2 TextPosition;
    public Vector2 BoxPosition;
    public Vector2 BoxLengths;
    public string Name;
    public float TextSize;
    public float TextWhiteSpacing = 50f;
    public int CursorPosition = 0;
    public bool IsBoxActive = false;

    public ArchiveNamingDataTypeBox(Texture2D texture, Vector2 textureSize, Vector2 boxPosition, Vector2 boxLengths, Vector2 textPosition, string name, float textSize, int cursorPosition, bool isBoxActive)
    {
        Texture = texture;
        TextureSize = textureSize;
        BoxPosition = boxPosition;
        BoxLengths = boxLengths;
        TextPosition = textPosition;
        Name = name;
        TextSize = textSize;
        CursorPosition = cursorPosition;
        IsBoxActive = isBoxActive;
    }
}



public class ArchiveLayoutState
{
    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);

    public Texture2D[] ArchiveTextures { get; set; } = new Texture2D[0];

    public ArchiveNamingDataTypeBox ArchiveNamingDataTypeBox { get; set; } = new ArchiveNamingDataTypeBox();

    public ArchiveAddDataTypeMenu AddDataTypeMenu { get; set; } = new ArchiveAddDataTypeMenu();

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

    public Vector2 AddOnButtonTexturePosition { get; set; } = new Vector2();

    public float ArchiveFunctionBarBoxPercentageWhiteSpace { get; } = 20f;
    public float ArchiveFunctionBarBoxPixelWhiteSpace { get; set; }

    public float ArchiveFunctionBarFunctionsPercentageWhiteSpace { get; } = 7f;
    public float ArchiveFunctionBarFunctionsPixelWhiteSpace { get; set; }

    public float ArchiveWhiteSpaceToFunctionBar { get; set; } = 4f;
}