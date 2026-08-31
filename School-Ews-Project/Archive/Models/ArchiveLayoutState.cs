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

public enum ArchiveStorageNames
{
    ProjectFolder,
    Folder,
    File
}

public struct ArchiveStorage
{
    public ArchiveStorageNames DataType;

    public string Name;

    public string DataLocation;

    public Dictionary<string[], ArchiveStorage>? FolderStorage;

    public ArchiveStorage(ArchiveStorageNames dataType, string name, string dataLocation, Dictionary<string[], ArchiveStorage>? folderStorage)
    {
        DataType = dataType;
        Name = name;
        DataLocation = dataLocation;
        FolderStorage = folderStorage;
    }
}

public class ArchiveLayoutState
{
    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);

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

    public Dictionary<string, ArchiveStorage> Storage { get; set; } = new Dictionary<string, ArchiveStorage>();

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