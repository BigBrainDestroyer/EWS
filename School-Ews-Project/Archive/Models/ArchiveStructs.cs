using Raylib_cs;
using System.Numerics;

namespace Program;

public struct ArchiveStorage
{
    public Dictionary<string, ArchiveStorage>? FolderStorage;
    public ArchiveStorageNames DataType;
    public string Name;
    public string DataLocation;
    public bool ViewFolderStorage;

    public ArchiveStorage(Dictionary<string, ArchiveStorage>? folderStorage, ArchiveStorageNames dataType, string name, string dataLocation, bool viewFolderStorage)
    {
        FolderStorage = folderStorage;
        DataType = dataType;
        Name = name;
        DataLocation = dataLocation;
        ViewFolderStorage = viewFolderStorage; 
    }
}

public struct ArchiveDataTypeBox
{
    public ArchiveStorageNames DataType;
    
    public Vector2 Percentages = new Vector2(100f, 3f);
    public Vector2 Position;
    public Vector2 Length;

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
    public BoolVector4 CornerStates ;
    public Font TextFont;
    public Color TextColor;
    public Texture2D Texture;
    
    public Vector2 BoxPosition;
    public Vector2 BoxLengths;
    public Vector2 TextureSize;
    public Vector2 ViewTextPixelPosition;
    public Vector2 CursorPixelLengths;
    public Vector2 CursorPixelPosition;
    
    public string Text;
    public string ViewText;
    public string BackgroundText;
    
    public float TextureToTextWhiteSpace;
    
    public int CursorTextPosition;
    public int CursorTextOffSet;
    public int TextTypingWhiteSpaceRight;
    public int TextTypingWhiteSpaceLeft;
    public int CharSpacing;
    
    public bool IsTextBoxActive;

    public TextBar()
    {
        TextColor = Color.RayWhite;
        Text = "";
        ViewText = "";
        BackgroundText = "";
        TextTypingWhiteSpaceRight = 0;
        TextTypingWhiteSpaceLeft = 0;
        CharSpacing = 0;
    }
}