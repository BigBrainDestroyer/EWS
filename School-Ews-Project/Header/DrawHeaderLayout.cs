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

        DrawHeaderObjects();
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

    public void DrawHeaderObjects()
    {
        DrawHeaderButtons();
    }

    public void DrawHeaderButtons()
    {
        var _header = _state.HeaderState;

        foreach(DrawHeaderObjectNames buttonName in _header.HeaderButtons.Keys)
        {
            Custom.DrawCustomRectangle(
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Box.PixelPosition,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Box.PixelLengths,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Box.Radius,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Box.CornerStates,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].ButtonBackgroundColor
            );

            Raylib.DrawTextEx(
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Font,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Text.Text,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].TextPosition,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Text.TextSize,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].Text.TextSpacing,
                _header.HeaderButtons[DrawHeaderObjectNames.RunButton].ButtonTextColor
            );
        }
    }
}