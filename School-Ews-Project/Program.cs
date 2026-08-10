using Raylib_cs;

namespace Program;

public class Program
{
    public static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight  = 600; 

        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);        
        Raylib.InitWindow(screenWidth, screenHeight, "EWS");
        Raylib.SetTargetFPS(61);
        Thread.Sleep(1000);
        Raylib.MaximizeWindow();

        var state = new AppState();

        var Game = new ProgramLoop(state);
        Game.Loop();

        Raylib.CloseWindow();
    }
}