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
        _state.common.font.EditorPanelText = Raylib.LoadFontEx("Resources/Font/Lilex-Regular.ttf", _state.editor.layout.textPanel.TextSize, null, 0);
        Raylib.SetTextureFilter(_state.common.font.EditorPanelText.Texture, TextureFilter.Bilinear);

        _state.common.font.EditorPanelRowNumbers = Raylib.LoadFontEx("Resources/Font/Lilex-ExtraLight.ttf", _state.editor.layout.textPanel.TextSize, null, 0);
        Raylib.SetTextureFilter(_state.common.font.EditorPanelRowNumbers.Texture, TextureFilter.Bilinear);

        _state.common.font.MazeCommonData = Raylib.LoadFontEx("Resources/Font/Lilex-ExtraLight.ttf", (int)_state.maze.layout.mazeView.CommonDataCharHight, null, 0);
        Raylib.SetTextureFilter(_state.common.font.MazeCommonData.Texture, TextureFilter.Bilinear);
    }
}