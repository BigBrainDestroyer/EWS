using Raylib_cs;

namespace Program;

//flatpak run dev.zed.Zed -n "/home/bigbraindestroyer/Documents/GitHub/EWS/School-Ews-Project"

public class Program
{
    public static void Main()
    {
        var state = new AppState();
        var initialize = new Initialize(state);

        initialize.Window();

        initialize.StartStopwatch();

        initialize.OsPlatform();

        initialize.InitializeProjectsDataLocation();

        initialize.InitializeProjectsDllLocation();
        
        initialize.FileWatcher();

        initialize.Fonts();

        initialize.Texture2D();

        initialize.LoadStorage();

        var game = new ProgramLoop(state);
        game.Loop();

        Raylib.CloseWindow();
    }
}