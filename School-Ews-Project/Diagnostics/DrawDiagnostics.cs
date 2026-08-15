using Raylib_cs;

namespace Program;

public class DrawDiagnostics
{
    private readonly AppState _state;

    public DrawDiagnostics(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Draw()
    {
        if(_state.common.settings.DrawDiagnostics == true)
        {
            DrawMainBoxes();
        }
    }
    
    private void DrawMainBoxes()
    {
        Raylib.DrawRectangleV(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Archive], 
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Archive],
            Color.Yellow
        );

        Raylib.DrawRectangleV(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Maze], 
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Maze],
            Color.DarkGray
        );

        Raylib.DrawRectangleV(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Statistics], 
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Statistics],
            Color.DarkBlue
        );

        Raylib.DrawRectangleV(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Editor], 
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Editor],
            Color.Red
        );
    }
}