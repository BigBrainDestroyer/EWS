using Raylib_cs;

namespace Program;

public class DrawHeaderLayout
{
    private readonly AppState _state;

    public DrawHeaderLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Draw()
    {
        DrawHeaderBackground();
    }

    public void DrawHeaderBackground()
    {
        var _boxes = _state.common.boxes;
        var _header = _state.HeaderState;

        Raylib.DrawRectangleV(
            _boxes.HeaderPixelPosition,
            _boxes.HeaderPixelLengths,
            _header.HeaderBackgroundColor
        );
    }
}