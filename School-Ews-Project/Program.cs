using Raylib_cs;

namespace Program;

public class Program
{
    public static void Main()
    {
        var state = new AppState();
        var initialize = new Initialize(state);
        
        initialize.Window();

        initialize.FileWatcher();

        initialize.Fonts();

        initialize.Texture2D();

        initialize.OsPlatform();
        
        initialize.LoadStorage();

        var game = new ProgramLoop(state);
        game.Loop();

        Raylib.CloseWindow();
    }
}