using Raylib_cs;
using ShareData;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace Program;

public static class StaticVariablesInitialize
{
    //FontPaths
    public static string FontLilexBoldPath { get; } = "Resources/Font/Lilex-Bold.ttf";
    public static string FontLilexSemiBoldPath { get; } = "Resources/Font/Lilex-Bold.ttf";
    public static string FontLilexMediumPath { get; } = "Resources/Font/Lilex-Medium.ttf";
    public static string FontLilexRegular { get; } = "Resources/Font/Lilex-Regular.ttf";
    public static string FontLilexExtraLightPath { get; } = "Resources/Font/Lilex-ExtraLight.ttf";

    //TexturePaths
    public static string TextureArrowUpPath { get; } = "Resources/Png/ArrowUp.png";
    public static string TextureArrowDownPath { get; } = "Resources/Png/ArrowDown.png";
    public static string TextureProjectFolderPath { get; } = "Resources/Png/ProjectFolder.png";
    public static string TextureFolderPath { get; } = "Resources/Png/Folder.png";
    public static string TextureFilePath { get; } = "Resources/Png/File.png";
    public static string TextureOpenProjectFolderPath { get; } = "Resources/Png/OpenProjectFolder.png";
    public static string TextureOpenFolderPath { get; } = "Resources/Png/OpenFolder.png";
    public static string TexturePlusSignPath { get; } = "Resources/Png/PlusSign.png";
    public static string TextureTerminalPath { get; } = "Resources/Png/Terminal.png";
    public static string TexturePauzePath { get; } = "Resources/Png/Pauze.png";
    public static string TexturePlayPath { get; } = "Resources/Png/Play.png";
    public static string TextureMazePath { get; } = "Resources/Png/Maze.png";

    //Textures
    public static Texture2D TextureArrowUp { get;  private set; }
    public static Texture2D TextureArrowDown { get;  private set; }
    public static Texture2D TextureProjectFolder { get;  private set; }
    public static Texture2D TextureFolder { get;  private set; }
    public static Texture2D TextureFile { get;  private set; }
    public static Texture2D TextureOpenProjectFolder { get;  private set; }
    public static Texture2D TextureOpenFolder { get;  private set; }
    public static Texture2D TexturePlusSign { get;  private set; }
    public static Texture2D TextureTerminal { get;  private set; }
    public static Texture2D TexturePauze { get;  private set; }
    public static Texture2D TexturePlay { get;  private set; }
    public static Texture2D TextureMaze { get;  private set; }

    public static void Init()
    {
        StaticVariablesInitialize.TextureArrowUp = Load.LoadResizableTexture2D(TextureArrowUpPath);
        StaticVariablesInitialize.TextureArrowDown = Load.LoadResizableTexture2D(TextureArrowDownPath);
        StaticVariablesInitialize.TextureProjectFolder = Load.LoadResizableTexture2D(TextureProjectFolderPath);
        StaticVariablesInitialize.TextureFolder = Load.LoadResizableTexture2D(TextureFolderPath);
        StaticVariablesInitialize.TextureFile = Load.LoadResizableTexture2D(TextureFilePath);
        StaticVariablesInitialize.TextureOpenProjectFolder = Load.LoadResizableTexture2D(TextureOpenProjectFolderPath);
        StaticVariablesInitialize.TextureOpenFolder = Load.LoadResizableTexture2D(TextureOpenFolderPath);
        StaticVariablesInitialize.TexturePlusSign = Load.LoadResizableTexture2D(TexturePlusSignPath);
        
        StaticVariablesInitialize.TextureTerminal = Load.LoadResizableTexture2D(TextureTerminalPath);
        StaticVariablesInitialize.TexturePauze = Load.LoadResizableTexture2D(TexturePauzePath);
        StaticVariablesInitialize.TexturePlay = Load.LoadResizableTexture2D(TexturePlayPath);
        StaticVariablesInitialize.TextureMaze = Load.LoadResizableTexture2D(TextureMazePath);
    }
}

public class Initialize
{
    private readonly AppState _state;
    private readonly UpdateArchiveStorage _archiveStorage;

    public Initialize(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _archiveStorage = new UpdateArchiveStorage(_state);
    }

    public void StartStopwatch()
    {
        _state.common.time.Stopwatch.Start();
    }

    public void Texture2D()
    {
        var _archive = _state.archive;

        //ArchiveTexture2DNames volgorde
        _archive.ArchiveTextures = 
            new Texture2D[] { 
                StaticVariablesInitialize.TextureProjectFolder,
                StaticVariablesInitialize.TextureFolder,
                StaticVariablesInitialize.TextureFile,
                StaticVariablesInitialize.TextureOpenProjectFolder, 
                StaticVariablesInitialize.TextureOpenFolder,
                StaticVariablesInitialize.TexturePlusSign 
            };
    }

    public void OsPlatform()
    {
        var _archive = _state.archive;  
    }

    public void InitializeProjectsDataLocation()
    {
        var _archive = _state.archive;

        string baseProjectPath = ShareData.App.directoryInfo.ProgramStorageFolderPath + App.directoryInfo.PathSyntax + _archive.ProjectFolderName;

        
        if(!Directory.Exists(baseProjectPath))
        {
            Directory.CreateDirectory(baseProjectPath);
        }

        _archive.ProjectsDataLocation = baseProjectPath;
        _archive.CurrentStorageDataLocation = baseProjectPath;
    }

    public void InitializeProjectsDllLocation()
    {
        var _archive = _state.archive;
        var _terminal = _state.common.terminal;

        string dllCsprojPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory,
            "..", "..", "..", "..",
            _terminal.DllProjectFolderName,
            _terminal.DllProjectReferenceFileName
        ));

        if(!File.Exists(dllCsprojPath))
        {
            Debug.WriteLine("Missing Library DLL Project Folder");

            Environment.Exit(0);
        }
        
        _terminal.ProjectsDllReferenceFileLocation = dllCsprojPath;
    }

    public void LoadStorage()
    {
        var _archive = _state.archive;

        _archive.Storage = _archiveStorage.LoadStorage(_archive.Storage, _archive.ProjectsDataLocation);

        _archive.ArchiveProjectFolderPaths = Directory.GetDirectories(_archive.ProjectsDataLocation);
    }

    public void LoadShareAlgoData()
    {
        var _shareAlgoData = _state.shareAlgoData;
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;

        Debug.WriteLine("Loading ShareAlgoData");

        string jsonText = "";
        
        try
        {   
            jsonText = File.ReadAllText(ShareData.App.directoryInfo.ShareAlgoDataFilePath);
            
            Debug.WriteLine("Reading ShareAlgoData File Is SuccessFull");
        }   
        catch
        {
            Debug.WriteLine("Reading ShareAlgoData File Is UnSuccessFull");
            
            return;
        }

        
        if(string.IsNullOrWhiteSpace(jsonText))
        {
            Debug.WriteLine("AlgoShareData File Is Corrupt");

            _play.IsAlgoLoaded = false;

            return;
        }
        
        try
        {
            if(JsonSerializer.Deserialize<ShareData.ShareAlgoData>(jsonText, ShareData.Json.JsonSerializeSettings()) == null)
            {   
                _shareAlgoData = new ShareAlgoData();
            }
            else
            {
                var shareObj = new ShareAlgoData();

                shareObj = JsonSerializer.Deserialize<ShareData.ShareAlgoData>(jsonText, ShareData.Json.JsonSerializeSettings());

                _shareAlgoData.CommonDataHistory = shareObj.CommonDataHistory;
                _shareAlgoData.AllMazes = shareObj.AllMazes;
                _shareAlgoData.MazeBeginPoints = shareObj.MazeBeginPoints;
                _shareAlgoData.MazeEndPoints = shareObj.MazeEndPoints;

                int shareAlgoMazeSize = _shareAlgoData.AllMazes.First().UsingXToGetRightArray.Length;
                int shareAlgoBatchSize = _shareAlgoData.AllMazes.Count();

                if (shareAlgoMazeSize >= 3)
                {
                    _generator.MazeSize = shareAlgoMazeSize;
                    _play.IsMazeLoaded = true;
                }
                else
                {
                    _play.IsMazeLoaded = false;
                    _generator.MazeSize = 3;
                }
                
                if (shareAlgoBatchSize >= 1)
                {
                    _generator.MazeBatchSize = shareAlgoBatchSize;
                }
                else
                {
                    _generator.MazeBatchSize = 1;
                }
            }
        }
        catch
        {
            Debug.WriteLine("AlgoShareData File Is Corrupt");

            _play.IsAlgoLoaded = false;

            return;
        }

        (_play.IsAlgoLoaded, _play.IsMazeLoaded) = IsShareAlgoDataLegal.Check(_shareAlgoData);

        Debug.WriteLine($"IsAlgoLoaded: {_play.IsAlgoLoaded}");
    }

    public void InitializeCommonDataState()
    {
        var _mazeView = _state.maze.layout.mazeView;

        if(App.directoryInfo.OsSystem == ShareData.OsSystem.Linux)
        {
            _mazeView.CommonDataBoxState[CommonDataNames.RamTotalUsage] = false;
        }
        else
        {
            _mazeView.CommonDataBoxState[CommonDataNames.RamTotalUsage] = true;
        }
    }
    
    public void FileWatchers()
    {
        var _archive = _state.archive;

        _archive.ProjectsFolderWatcher = new FileSystemWatcher(_archive.ProjectsDataLocation);

        _archive.ProjectsFolderWatcher.Changed += (a, b) => _archive.ProjectsFolderHasChanged = true;
        _archive.ProjectsFolderWatcher.Created += (a, b) => _archive.ProjectsFolderHasChanged = true;
        _archive.ProjectsFolderWatcher.Deleted += (a, b) => _archive.ProjectsFolderHasChanged = true;
        _archive.ProjectsFolderWatcher.Renamed += (a, b) => _archive.ProjectsFolderHasChanged = true;

        _archive.ProjectsFolderWatcher.IncludeSubdirectories = true;
        _archive.ProjectsFolderWatcher.EnableRaisingEvents = true;
    }
}