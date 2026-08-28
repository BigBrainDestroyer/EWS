using Raylib_cs;

namespace Program;

public class DrawCommonHeader
{
    private readonly AppState _state;

    public DrawCommonHeader(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void CommonHeader()
    {
        DrawHeader();
    }

    private void DrawHeader()
    {
        var _boxes = _state.common.boxes;

        Raylib.DrawRectangleV(
            _boxes.HeaderPixelPosition,
            _boxes.HeaderPixelLengths,
           _boxes.HeaderBackgroundColor 
        );
    }  
}