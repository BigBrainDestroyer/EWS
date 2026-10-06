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

        DrawHeaderStringSlider();
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

    public void DrawHeaderStringSlider()
    {
        var _header = _state.HeaderState;

        foreach (DrawHeaderObjectNames headerObjectName in _header.HeaderStringSlider.Keys)
        {

            Custom.DrawCustomRectangle(
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.Radius,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.CornerStates,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.ButtonBackgroundColor
            );

            Raylib.DrawTextEx(
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Font,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Text.Text,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.TextPosition,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Text.TextSize,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Text.TextSpacing,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.ButtonTextColor
            );

            Custom.DrawCustomRectangle(
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.Radius,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.CornerStates,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.ButtonBackgroundColor
            );

            Raylib.DrawTextEx(
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Font,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Text.Text,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.TextPosition,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Text.TextSize,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Text.TextSpacing,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.ButtonTextColor
            );

            Custom.DrawCustomRectangle(
                _header.HeaderStringSlider[headerObjectName].CounterUpClickField.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].CounterUpClickField.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].CounterUpClickField.Radius,
                _header.HeaderStringSlider[headerObjectName].CounterUpClickField.CornerStates,
                _header.HeaderStringSlider[headerObjectName].TextureDownBackgroundClickFieldColor
            );

            Custom.DrawCustomRectangle(
                _header.HeaderStringSlider[headerObjectName].CounterDownClickField.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].CounterDownClickField.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].CounterDownClickField.Radius,
                _header.HeaderStringSlider[headerObjectName].CounterDownClickField.CornerStates,
                _header.HeaderStringSlider[headerObjectName].TextureUpBackgroundClickFieldColor
            );
        }
    }
}