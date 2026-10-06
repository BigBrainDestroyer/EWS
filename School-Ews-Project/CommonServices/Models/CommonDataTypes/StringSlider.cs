using Raylib_cs;

namespace Program;

public class StringSlider
{
    public Color TextureUpColor { get; set; }
    public Color TextureDownColor{ get; set; }
    public Color TextureUpBackgroundClickFieldColor { get; set; }
    public Color TextureDownBackgroundClickFieldColor { get; set; }

    public CustomBox Box { get; set; }
    
    public RaylibTexture UpTexture { get; set; }
    public RaylibTexture DownTexture { get; set; }
    
    public CustomBox CounterUpClickField { get; set; }//
    public CustomBox CounterDownClickField { get; set; }//
    
    public CenterTextInBox TitleStringBox { get; set; }
    public CenterTextInBox StringSliderBox { get; set; }

    public string UpTexturePath { get; set; }
    public string DownTexturePath { get; set; }

    public float TitleStringBoxHeightPercentage { get; set; }
    
    public StringSlider(RaylibTexture upTexture, RaylibTexture downTexture, CustomBox SlideUpClickField, CustomBox counterDownClickField, CenterTextInBox titleStringBox, CenterTextInBox stringSliderBox, string UpTexturePath, string DownTexturePath, float titleStringBoxHeightPercentage, Color textureUpColor, Color textureDownColor, Color textureUpBackgroundClickFieldColor, Color textureDownBackgroundClickFieldColor)
    {
        TextureUpColor = textureUpColor;
        TextureDownColor = textureDownColor;
        TextureUpBackgroundClickFieldColor = textureUpBackgroundClickFieldColor;
        TextureDownBackgroundClickFieldColor = textureDownBackgroundClickFieldColor;

        UpTexture = upTexture;
        DownTexture = downTexture;
        CounterUpClickField = SlideUpClickField;
        CounterDownClickField = counterDownClickField;
        TitleStringBox = titleStringBox;
        StringSliderBox = stringSliderBox;
        TitleStringBoxHeightPercentage = titleStringBoxHeightPercentage;
    }
}