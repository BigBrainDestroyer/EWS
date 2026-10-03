using Raylib_cs;
using System.Runtime.InteropServices;

namespace Program;

public class Initialize
{
    private readonly AppState _state;
    private readonly UpdateArchiveStorage _archiveStorage;

    public Initialize(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _archiveStorage = new UpdateArchiveStorage(_state);
    }
    
    public void Window()
    {
        const int screenWidth = 800;
        const int screenHeight  = 600; 

        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.HighDpiWindow);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.InitWindow(screenWidth, screenHeight, "EWS");

        Raylib.SetWindowMonitor(0);

        Raylib.SetExitKey(KeyboardKey.Null);

        Raylib.SetTargetFPS(240);
        Raylib.MaximizeWindow();

        Raylib.SetWindowMinSize(_state.common.settings.MinimalScreenWidth, _state.common.settings.MinimalScreenHeight);
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
                Load.LoadResizableTexture2D("Resources/Png/ProjectFolder.png"), 
                Load.LoadResizableTexture2D("Resources/Png/Folder.png"),
                Load.LoadResizableTexture2D("Resources/Png/File.png"),
                Load.LoadResizableTexture2D("Resources/Png/OpenProjectFolder.png"),
                Load.LoadResizableTexture2D("Resources/Png/OpenFolder.png"),
                Load.LoadResizableTexture2D("Resources/Png/PlusSign.png")
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

    public void LoadStorage()
    {
        var _archive = _state.archive;

        _archive.Storage = _archiveStorage.LoadStorage(_archive.Storage, _archive.ProjectsDataLocation);
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