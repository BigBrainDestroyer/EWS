using System.Numerics;
using Raylib_cs;

namespace Program;

public class DrawArchive
{
    private readonly AppState _state;

    public DrawArchive(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Archive()
    {
        DrawArchiveBackground();
    }

    private void DrawArchiveBackground()
    {
        var _boxes = _state.common.boxes;
        var _layout = _state.archive.layout;

        Raylib.DrawRectangleV(
            _boxes.MainBoxPixelPositions[MainBoxNames.Archive],
            _boxes.MainBoxPixelLengts[MainBoxNames.Archive],
            _layout.BackgroundColor
        );
    }
}