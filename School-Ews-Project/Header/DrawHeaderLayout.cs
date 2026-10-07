using System.Numerics;
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

    private void DrawHeaderBackground()
    {
        var _boxes = _state.common.boxes;
        var _header = _state.HeaderState;

        Raylib.DrawRectangleV(
            _boxes.HeaderPixelPosition,
            _boxes.HeaderPixelLengths,
            _header.HeaderBackgroundColor
        );
    }

    private void DrawHeaderObjects()
    {
        DrawHeaderButtons();

        DrawHeaderCenterTextureInBox();

        DrawHeaderStringSlider();
    }

    private void DrawHeaderButtons()
    {
        var _header = _state.HeaderState;
        
        foreach(DrawHeaderObjectNames buttonName in _header.HeaderButtons.Keys)
        {
            Custom.DrawCustomRectangle(
                _header.HeaderButtons[buttonName].Box.PixelPosition,
                _header.HeaderButtons[buttonName].Box.PixelLengths,
                _header.HeaderButtons[buttonName].Box.Radius,
                _header.HeaderButtons[buttonName].Box.CornerStates,
                _header.HeaderButtons[buttonName].BoxBackgroundColor
            );

            Raylib.DrawTextEx(
                _header.HeaderButtons[buttonName].Font,
                _header.HeaderButtons[buttonName].Text.Text,
                _header.HeaderButtons[buttonName].TextPosition,
                _header.HeaderButtons[buttonName].Text.TextSize,
                _header.HeaderButtons[buttonName].Text.TextSpacing,
                _header.HeaderButtons[buttonName].TextColor
            );
        }
    }

    private void DrawHeaderCenterTextureInBox()
    {
        var _header = _state.HeaderState;
        
        foreach(DrawHeaderObjectNames headerObjectName in _header.HeaderCenterTextureInBox.Keys)
        {
            Custom.DrawCustomRectangle(
                _header.HeaderCenterTextureInBox[headerObjectName].Box.PixelPosition,
                _header.HeaderCenterTextureInBox[headerObjectName].Box.PixelLengths,
                _header.HeaderCenterTextureInBox[headerObjectName].Box.Radius,
                _header.HeaderCenterTextureInBox[headerObjectName].Box.CornerStates,
                _header.HeaderCenterTextureInBox[headerObjectName].BoxBackgroundColor
            );

            Raylib.DrawTexturePro(
                _header.HeaderCenterTextureInBox[headerObjectName].Texture.Texture,
                _header.HeaderCenterTextureInBox[headerObjectName].Texture.TextureUsage,
                _header.HeaderCenterTextureInBox[headerObjectName].Texture.TextureSize,
                new Vector2(),
                0f,
                _header.HeaderCenterTextureInBox[headerObjectName].TextureColor
            );
        }
    }

    private void DrawHeaderStringSlider()
    {
        var _header = _state.HeaderState;

        foreach (DrawHeaderObjectNames headerObjectName in _header.HeaderStringSlider.Keys)
        {
            Custom.DrawCustomRectangle( //UpBackground
                _header.HeaderStringSlider[headerObjectName].UpTexture.Box.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].UpTexture.Box.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].UpTexture.Box.Radius,
                _header.HeaderStringSlider[headerObjectName].UpTexture.Box.CornerStates,
                _header.HeaderStringSlider[headerObjectName].UpTexture.BoxBackgroundColor
            );

            Custom.DrawCustomRectangle( //DownBackground
                _header.HeaderStringSlider[headerObjectName].DownTexture.Box.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].DownTexture.Box.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].DownTexture.Box.Radius,
                _header.HeaderStringSlider[headerObjectName].DownTexture.Box.CornerStates,
                _header.HeaderStringSlider[headerObjectName].DownTexture.BoxBackgroundColor
            );

            Custom.DrawCustomRectangle( //TitleBackground
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.Radius,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Box.CornerStates,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.BoxBackgroundColor
            );

            Custom.DrawCustomRectangle( //StringSliderBackground
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.PixelPosition,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.PixelLengths,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.Radius,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Box.CornerStates,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.BoxBackgroundColor
            );

            Raylib.DrawTextEx( 
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Font,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Text.Text,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.TextPosition,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Text.TextSize,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.Text.TextSpacing,
                _header.HeaderStringSlider[headerObjectName].TitleStringBox.TextColor
            );

            Raylib.DrawTextEx(
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Font,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Text.Text,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.TextPosition,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Text.TextSize,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.Text.TextSpacing,
                _header.HeaderStringSlider[headerObjectName].StringSliderBox.TextColor
            );

            Raylib.DrawTexturePro(
                _header.HeaderStringSlider[headerObjectName].UpTexture.Texture.Texture,
                _header.HeaderStringSlider[headerObjectName].UpTexture.Texture.TextureUsage,
                _header.HeaderStringSlider[headerObjectName].UpTexture.Texture.TextureSize,
                new Vector2(),
                0f,
                _header.HeaderStringSlider[headerObjectName].UpTexture.TextureColor
            );

            Raylib.DrawTexturePro(
                _header.HeaderStringSlider[headerObjectName].DownTexture.Texture.Texture,
                _header.HeaderStringSlider[headerObjectName].DownTexture.Texture.TextureUsage,
                _header.HeaderStringSlider[headerObjectName].DownTexture.Texture.TextureSize,
                new Vector2(),
                0f,
                _header.HeaderStringSlider[headerObjectName].DownTexture.TextureColor
            );

            Console.WriteLine(_header.HeaderStringSlider[headerObjectName].UpTexture);
        }
    }
}