using Raylib_cs;

namespace Program;

//flatpak run dev.zed.Zed -n "/home/bigbraindestroyer/Documents/GitHub/EWS/School-Ews-Project"

public class Program
{
    public static void Main()
    {
        var program = new Program();

        program.Window();

        var state = new AppState();
        var initialize = new Initialize(state);

        initialize.StartStopwatch();

        initialize.OsPlatform();

        initialize.InitializeProjectsDataLocation();

        initialize.InitializeProjectsDllLocation();
        
        initialize.FileWatcher();

        initialize.Fonts();

        initialize.Texture2D();

        initialize.LoadStorage();

        initialize.LoadShareAlgoData();

        var game = new ProgramLoop(state);
        game.Loop();

        Raylib.CloseWindow();
    }

    private void Window()
    {
        const int screenWidth = 800;
        const int screenHeight = 600; 

        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.SetConfigFlags(ConfigFlags.HighDpiWindow);
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.InitWindow(screenWidth, screenHeight, "EWS");

        Raylib.SetWindowMonitor(0);

        Raylib.SetExitKey(KeyboardKey.Null);

        Raylib.SetTargetFPS(240);
        Raylib.MaximizeWindow();

        Raylib.SetWindowMinSize(screenWidth, screenHeight);

        StaticVariablesInitialize.Init();
    }
}