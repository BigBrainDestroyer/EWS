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
                _header.HeaderButtons[buttonName].Box.PixelPosition,
                _header.HeaderButtons[buttonName].Box.PixelLengths,
                _header.HeaderButtons[buttonName].Box.Radius,
                _header.HeaderButtons[buttonName].Box.CornerStates,
                _header.HeaderButtons[buttonName].ButtonBackgroundColor
            );

            Raylib.DrawTextEx(
                _header.HeaderButtons[buttonName].Font,
                _header.HeaderButtons[buttonName].Text.Text,
                _header.HeaderButtons[buttonName].TextPosition,
                _header.HeaderButtons[buttonName].Text.TextSize,
                _header.HeaderButtons[buttonName].Text.TextSpacing,
                _header.HeaderButtons[buttonName].ButtonTextColor
            );
        }
    }
}