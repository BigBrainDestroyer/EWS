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
        if(_state.common.settings.DrawDiagnostics == false)
        {
            DrawMainBoxes();
        }
    }
    
    private void DrawMainBoxes()
    {
        Custom.DrawCustomRectangle(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Archive], 
            _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            Color.Yellow
        );

        Custom.DrawCustomRectangle(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Maze], 
            _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Maze],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Maze],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Maze],
            Color.DarkGray
        );

        Custom.DrawCustomRectangle(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Editor], 
            _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Editor],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Editor],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Editor],
            Color.Red
        );
    }
}