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

        DrawHeaderCounters();
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

    public void DrawHeaderCounters()
    {
        var _header = _state.HeaderState;

        _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].Box = _header.HeaderButtons[DrawHeaderObjectNames.BuildButton].Box;

        _header.DigitsCounters[DrawHeaderObjectNames.MazeSize] = CalculateDigitCounter.Calculate(_header.DigitsCounters[DrawHeaderObjectNames.MazeSize]);


        Custom.DrawCustomRectangle(
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Box.PixelPosition,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Box.PixelLengths,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Box.Radius,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Box.CornerStates,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.ButtonBackgroundColor
        );

        Raylib.DrawTextEx(
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Font,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Text.Text,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.TextPosition,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Text.TextSize,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.Text.TextSpacing,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TitleStringBox.ButtonTextColor
        );

        Custom.DrawCustomRectangle(
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Box.PixelPosition,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Box.PixelLengths,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Box.Radius,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Box.CornerStates,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.ButtonBackgroundColor
        );

        Raylib.DrawTextEx(
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Font,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text.Text,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.TextPosition,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text.TextSize,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text.TextSpacing,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].StringSliderBox.ButtonTextColor
        );

        
        Custom.DrawCustomRectangle(
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterUpClickField.PixelPosition,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterUpClickField.PixelLengths,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterUpClickField.Radius,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterUpClickField.CornerStates,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TextureDownBackgroundClickFieldColor
        );

        Custom.DrawCustomRectangle(
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterDownClickField.PixelPosition,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterDownClickField.PixelLengths,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterDownClickField.Radius,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].CounterDownClickField.CornerStates,
            _header.DigitsCounters[DrawHeaderObjectNames.MazeSize].TextureUpBackgroundClickFieldColor
        );
        
    }
}