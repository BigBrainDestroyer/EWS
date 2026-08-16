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

        Raylib.SetTargetFPS(60);
        Raylib.MaximizeWindow();
    }

    public void Fonts()
    {
        _state.common.font.EditorTextPanel = Raylib.LoadFontEx("Resources/Lilex-Medium.ttf", 32, null, 0);
        Raylib.SetTextureFilter(_state.common.font.EditorTextPanel.Texture, TextureFilter.Bilinear);
    }
}