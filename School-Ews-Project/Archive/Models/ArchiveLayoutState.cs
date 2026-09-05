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

public struct TextBar
{
    public Vector2 BoxPosition;
    public Vector2 BoxLengths;
    
    public Texture2D Texture;
    public Vector2 TextureSize;

    public Font TextFont;
    
    public string Text;

    public Vector2 ViewTextPixelPosition;
    public string ViewText;
    public float TextureToTextWhiteSpace;

    public int CursorTextOffSet;
    public int CursorTextPosition;
    public Vector2 CursorPixelPosition;
    public Vector2 CursorPixelLengths;

    public string BackgroundText;

    public int TextTypingWhiteSpaceRight;
    public int TextTypingWhiteSpaceLeft;
    public int CharSpacing;

    public bool IsTextBoxActive;

    public TextBar()
    {
        TextTypingWhiteSpaceRight = 1;
        TextTypingWhiteSpaceLeft = 1;
        CharSpacing = 0;
    }
}

public class ArchiveLayoutState
{
    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);

    public Texture2D[] ArchiveTextures { get; set; } = new Texture2D[0];

    public TextBar ArchiveNamingDataTypeBox { get; set; } = new TextBar();

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