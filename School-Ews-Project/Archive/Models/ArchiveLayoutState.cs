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

public class ArchiveLayoutState
{
    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);

    public Dictionary<ArchiveBoxesNames, Vector2> ArchiveBoxPixelPercentages { get; } =
        new Dictionary<ArchiveBoxesNames, Vector2>{
            { ArchiveBoxesNames.Header, new Vector2(100f, 15f)},
            { ArchiveBoxesNames.Archive, new Vector2(100f, 75f)},
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
    public float ArchiveFunctionBarBoxPercentageWhiteSpace { get; } = 10f;
    public float ArchiveFunctionBarBoxPixelWhiteSpace { get; set; }
    public Vector2 ArchiveFunctionBarBoxPixelLengths { get; set; }
    public Vector2 ArchiveFunctionBarBoxPosition { get; set; }

    public float ArchiveFunctionBarFunctionsPercentageWhiteSpace { get; } = 5f;
    public float ArchiveFunctionBarFunctionsPixelWhiteSpace { get; set; }

    public Vector2 SearchBarPixelLengths { get; set; }
    public Vector2 SearchBarPixelPosition { get; set; }

    public Vector2 AddOnButtonPixelLengths { get; set; }
    public Vector2 AddOnButtonPixelPosition { get; set; }
}