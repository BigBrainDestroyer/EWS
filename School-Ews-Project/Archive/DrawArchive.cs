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

        DrawArchiveFunctionBar();
    }

    private void DrawArchiveBackground()
    {
        var _boxes = _state.common.boxes;
        var _layout = _state.archive.layout;

        Custom.DrawCustomRectangle(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Archive], 
            _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            _layout.BackgroundColor
        );
    }

    private void DrawArchiveFunctionBar()
    {
        var _layout = _state.archive.layout;

        Custom.DrawCustomRectangle(
            _layout.SearchBarPixelPosition,
            _layout.SearchBarPixelLengths,
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            Color.Red
        );

        Custom.DrawCustomRectangle(
            _layout.AddOnButtonPixelPosition,
            _layout.AddOnButtonPixelLengths,
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            Color.Blue
        );
    }
}