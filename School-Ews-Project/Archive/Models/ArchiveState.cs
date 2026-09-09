using Raylib_cs;
using System.Numerics;
using System.IO;

namespace Program;

public class ArchiveState
{
    public ArchiveAddDataTypeMenu AddDataTypeMenu { get; set; } = new ArchiveAddDataTypeMenu();
    public ArchiveStorageNames?[] AddLegalDataTypeToStorage { get; set; } = new ArchiveStorageNames?[3];
    public ArchiveDataTypeBox DataTypeBox { get; set; } = new ArchiveDataTypeBox();  
    public TextBar ArchiveNamingDataTypeTextBox { get; set; } = new TextBar();
    public ArchiveStorageNames TemporarilyData;


    public Color BackgroundColor { get; set; } = new Color(51, 59, 67, 255);
    
    public Texture2D[] ArchiveTextures { get; set; } = new Texture2D[0];

    public Dictionary<string, ArchiveStorage> Storage { get; set; } = new Dictionary<string, ArchiveStorage>();

    public Dictionary<ArchiveBoxesNames, Vector2> ArchiveBoxPixelPercentages { get; } =
        new Dictionary<ArchiveBoxesNames, Vector2>{
            { ArchiveBoxesNames.Header, new Vector2(100f, 7f)},
            { ArchiveBoxesNames.Archive, new Vector2(90f, 90.7f)},
            { ArchiveBoxesNames.Edit, new Vector2(100f, 2.3f)}
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

    public string CurentStorageDataLocation { get; set; } = "AppProjects";
    
    public float ArchiveFunctionBarBoxPercentageWhiteSpace { get; } = 20f;
    public float ArchiveFunctionBarBoxPixelWhiteSpace { get; set; }

    public float ArchiveFunctionBarFunctionsPercentageWhiteSpace { get; } = 7f;
    public float ArchiveFunctionBarFunctionsPixelWhiteSpace { get; set; }

    public float ArchiveWhiteSpaceToFunctionBar { get; set; } = 4f;
}