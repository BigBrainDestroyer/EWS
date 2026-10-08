using Raylib_cs;
using ShareData;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    //Textures
    public static Texture2D TextureArrowUp { get;  private set; }
    public static Texture2D TextureArrowDown { get;  private set; }
    public static Texture2D TextureProjectFolder { get;  private set; }
    public static Texture2D TextureFolder { get;  private set; }
    public static Texture2D TextureFile { get;  private set; }
    public static Texture2D TextureOpenProjectFolder { get;  private set; }
    public static Texture2D TextureOpenFolder { get;  private set; }
    public static Texture2D TexturePlusSign { get;  private set; }

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

    public void Fonts()
    {
        _state.common.font.EditorPanelText = 
            Load.LoadFontEx(
                "Resources/Font/Lilex-Regular.ttf",
                _state.editor.layout.textPanel.TextSize
            );

        _state.common.font.EditorPanelRowNumbers = 
            Load.LoadFontEx(
                "Resources/Font/Lilex-ExtraLight.ttf", 
                _state.editor.layout.textPanel.TextSize
            );

        _state.common.font.MazeCommonData = 
            Load.LoadFontEx(
                "Resources/Font/Lilex-ExtraLight.ttf", 
                _state.maze.layout.mazeView.CommonDataCharHeight
            );

        _state.common.font.ArchiveDataBoxType = 
            Load.LoadFontEx(
                "Resources/Font/Lilex-ExtraLight.ttf", 
                _state.archive.DataTypeBoxCharLoadFontSize
            );
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

        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _archive.OsSystem = OsSystem.Linux;
            _archive.PathSyntax = _archive.LinuxPathSyntax;
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _archive.OsSystem = OsSystem.Windows;
            _archive.PathSyntax = _archive.WindowsPathSyntax;
        }
    }

    public void InitializeProjectsDataLocation()
    {
        var _archive = _state.archive;

        string baseProjectPath =
        Directory.GetCurrentDirectory().Substring(
            0,
            Directory.GetCurrentDirectory().Length - Directory.GetCurrentDirectory().Split(_archive.PathSyntax).Last().Length - _archive.PathSyntax.Length
        ) + _archive.PathSyntax + _archive.ProjectFolderName;       
    
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
        
        _terminal.ProjectsDllReferenceFileLocation =
            _archive.ProjectsDataLocation.Substring(
                0,
                _archive.ProjectsDataLocation.Length -
                _archive.ProjectsDataLocation.Split(_archive.PathSyntax).Last().Length
            ) + _terminal.DllProjectFolderName + _archive.PathSyntax + _terminal.DllProjectReferenceFileName ;
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

        bool stop = false;

        string jsonText = "";

        while(stop == false)
        {
            try
            {
                jsonText = File.ReadAllText(ShareData.DirectoryInfo.ShareAlgoDataFilePath());

                if(string.IsNullOrWhiteSpace(jsonText))
                {
                    jsonText = "";
                }
                else
                {
                    var shareObj = new ShareAlgoData();

                    shareObj = JsonSerializer.Deserialize<ShareData.ShareAlgoData>(jsonText, ShareData.Json.JsonSerializeSettings());

                    if (shareObj != null)
                    {
                        _shareAlgoData.CommonDataHistory = new CommonDataHistory();
                        _shareAlgoData.AllMazes = shareObj.AllMazes;
                        _shareAlgoData.MazeBeginPoints = shareObj.MazeBeginPoints;
                        _shareAlgoData.MazeEndPoints = shareObj.MazeEndPoints;
                    }
                    else
                    {
                        _shareAlgoData = new ShareAlgoData();
                    }
                }
                
                stop = true;
            }
            catch
            {
                Thread.Sleep(100);
            }
        }

        if(_shareAlgoData.CommonDataHistory.TotalProjectStopWatchTotalTime.Elapsed.TotalSeconds != 0)
        {
            _play.IsAlgoLoaded = true;
        }
        else
        {
            _play.IsAlgoLoaded = false;
        }
    }

    public void FileWatcher()
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