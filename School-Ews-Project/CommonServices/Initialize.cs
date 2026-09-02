using Raylib_cs;

namespace Program;

public class Initialize
{
    private readonly AppState _state;

    public Initialize(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
    
    public void Window()
    {
        const int screenWidth = 800;
        const int screenHeight  = 600; 

        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.InitWindow(screenWidth, screenHeight, "EWS");

        Raylib.SetWindowMonitor(0);

        Raylib.SetTargetFPS(240);
        Raylib.MaximizeWindow();

        Raylib.SetWindowMinSize(_state.common.settings.MinimalScreenWidth, _state.common.settings.MinimalScreenHeight);
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
                _state.editor.layout.textPanel.TextSize
            );
    }

    public void Texture2D()
    {
        var _archiveLayout = _state.archive.layout;

        //ArchiveTexture2DNames volgorde
        _archiveLayout.ArchiveTextures = 
            new Texture2D[] { 
                Load.LoadResizableTexture2D("Resources/Png/PlusSign.png"),
                Load.LoadResizableTexture2D("Resources/Png/ProjectFolder.png"), 
                Load.LoadResizableTexture2D("Resources/Png/Folder.png"),
                Load.LoadResizableTexture2D("Resources/Png/File.png"),
                Load.LoadResizableTexture2D("Resources/Png/OpenProjectFolder.png"),
                Load.LoadResizableTexture2D("Resources/Png/OpenFolder.png")
            };
    }
}