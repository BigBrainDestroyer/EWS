using Raylib_cs;

namespace Program;

public class StringSlider
{
    public CustomBox Box { get; set; }

    public CenterTextureInBox UpTexture { get; set; }
    public CenterTextureInBox DownTexture { get; set; }
    
    public CenterTextInBox TitleStringBox { get; set; }
    public CenterTextInBox StringSliderBox { get; set; }

    public float TitleStringBoxHeightPercentage { get; set; }
    
    public StringSlider(CenterTextureInBox upTexture, CenterTextureInBox downTexture, CenterTextInBox titleStringBox, CenterTextInBox stringSliderBox, float titleStringBoxHeightPercentage)
    {
        UpTexture = upTexture;
        DownTexture = downTexture;
        TitleStringBox = titleStringBox;
        StringSliderBox = stringSliderBox;
        TitleStringBoxHeightPercentage = titleStringBoxHeightPercentage;
    }
}