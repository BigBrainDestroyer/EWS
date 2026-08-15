using Raylib_cs;

namespace Program;

public class Program
{
    public static void Main()
    {
        var state = new AppState();
        
        var initialize = new Initialize(state);
        initialize.Window();

        initialize.Fonts();

        var game = new ProgramLoop(state);
        game.Loop();

        Raylib.CloseWindow();
    }
}