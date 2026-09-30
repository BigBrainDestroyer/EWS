using Raylib_cs;
using System.Numerics;

namespace Program;

public class ArchiveState
{
    public ArchiveAddDataTypeMenu AddDataTypeMenu { get; set; } = new ArchiveAddDataTypeMenu();
    public ArchiveStorageNames?[] AddLegalDataTypeToStorage { get; set; } = new ArchiveStorageNames?[3];
    public ArchiveDataTypeBox[] DataTypeBoxes { get; set; } = new ArchiveDataTypeBox[0];
    public ArchiveEditDataType ArchiveEditDataType { get; set; } = new ArchiveEditDataType();

    public ArchiveDataTypeBox SingleArchiveDataTypeBox { get; set; } = 
        new ArchiveDataTypeBox(
            new Color(51, 59, 67, 255), 
            new Color(145, 155, 165, 255), 
            new Color(220, 223, 228, 255),
            new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()),
            "",
            "",
            ArchiveStorageNames.Folder,
            new RaylibText(),
            new Box()
        );
        
    public TextBar ArchiveNamingDataTypeTextBox { get; set; } = new TextBar();
    public BoolVector4 ArchiveNamingDataTypeTextBoxCornerStates { get; } = new BoolVector4(false, false, true, true);

    public ArchiveStorageNames TemporarilyData { get; set; }

    //Colors
    public Color BackgroundColor { get; } = new Color(51, 59, 67, 255);
    public Color DataTypeBoxesTextColor { get; } = new Color(220, 223, 228, 255);
    public Color DataTypeBoxesTextureColor { get; } = new Color(145, 155, 165, 255);
    
    //public Color DataTypeBoxesSelectedBoxTextureColor { get; } = new Color(220, 223, 228, 255);
    public Color DataTypeBoxesMouseOnBoxColor { get; } = Color.Red;
    public Color DataTypeBoxesLoadedFileColor { get; } = Color.Blue;
    public Color DataTypeBoxesEditDataTypeColor { get; } = Color.Yellow;
    public Color DataTypeBoxesCurrentStorageLocationColor { get; } = Color.SkyBlue;

    //NamingDataTypeColors
    public Color NamingDataTypeBackgroundColor { get; } = new Color(76, 84, 92, 255);
    public Color NamingDataTypeCursorColor { get; }  = new Color(95, 179, 179, 255);
    public Color NamingDataTypeTextColor { get; } = new Color(220, 223, 228, 255);
    public Color NamingDataTypeTextureColor { get; } = new Color(145, 155, 165, 255);

    //EditColors
    public Color EditDataTypesTextColor { get; } = new Color(220, 223, 228, 255);
    public Color EditDataHighLightBoxColor { get; } = Color.LightGray;
    public Color DeleteHighLightTextColor { get; } = Color.Red;
    public Color RenameHighLightTextColor { get; } = Color.Yellow;

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

    public Vector2 ArchiveDataTypeBoxPercentages { get; set; } = new Vector2(100f, 2.3f);

    public Vector2 ArchiveFunctionBarBoxPixelLengths { get; set; } = new Vector2();
    public Vector2 ArchiveFunctionBarBoxPosition { get; set; } = new Vector2();

    public Vector2 SearchBarPixelLengths { get; set; } = new Vector2();
    public Vector2 SearchBarPixelPosition { get; set; } = new Vector2();

    public Vector2 AddOnButtonPixelLengths { get; set; } = new Vector2();
    public Vector2 AddOnButtonPixelPosition { get; set; } = new Vector2();

    public Vector2 AddOnButtonTexturePosition { get; set; } = new Vector2();

    public Vector2 DataTypeBoxPercentage { get; } = new Vector2(100f, 2.5f);

    public string CurrentStorageDataLocation { get; set; } = Directory.GetCurrentDirectory() + "/" + "AppProjects";
    public string LoadedFileDirectory { get; set; } = "";
    public string ProjectsDataLocation { get; } = Directory.GetCurrentDirectory() + "/" + "AppProjects"; //Base
    public string PathSyntax { get; set; } = "";
    public string LinuxPathSyntax { get; } = "/";
    public string WindowsPathSyntax { get; } = "\\";

    public float ArchiveFunctionBarBoxPercentageWhiteSpace { get; } = 20f;
    public float ArchiveFunctionBarBoxPixelWhiteSpace { get; set; }

    public float ArchiveFunctionBarFunctionsPercentageWhiteSpace { get; } = 7f;
    public float ArchiveFunctionBarFunctionsPixelWhiteSpace { get; set; }

    public float ArchiveWhiteSpaceToFunctionBar { get; set; } = 4f;

    public float ArchiveDataTypeBoxTextSpacing { get; } = 0;

    public float DataTypeBoxTextureTextPercentageOffSet { get; set; } = 4f;

    public float DataTypeBoxCharLoadFontSize { get; set; } = 25f;
    
    public int TotalViewStorageCount { get; set; } = 0;
    public int ArchiveDataTypeBoxRowOffSet { get; set; } = 0;

    public bool ProjectsFolderHasChanged { get; set; }
}