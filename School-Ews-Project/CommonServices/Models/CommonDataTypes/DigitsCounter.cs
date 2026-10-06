using Raylib_cs;

namespace Program;

public class DigitsCounter
{
    public Color TextureUpColor { get; set; }
    public Color TextureDownColor{ get; set; }
    public Color TextureUpHighLightColor { get; set; }
    public Color TextureDownHighLightColor { get; set; }
    public Color TextureUpBackgroundClickFieldColor { get; set; }
    public Color TextureDownBackgroundClickFieldColor { get; set; }
    public Color TextureUpBackgroundClickFieldHighLightColor { get; set; }
    public Color TextureDownBackgroundClickFieldHighLightColor { get; set; }

    public CustomBox Box { get; set; }
    
    public RaylibTexture UpTexture { get; set; }
    public RaylibTexture DownTexture { get; set; }
    
    public CustomBox CounterUpClickField { get; set; }//
    public CustomBox CounterDownClickField { get; set; }//
    
    public CenterTextInBox TitleBox { get; set; }
    public CenterTextInBox CounterBox { get; set; }

    public float TitleBoxHeightPercentage { get; set; }

    public int Counter { get; set; }

    public DigitsCounter(RaylibTexture upTexture, RaylibTexture downTexture, CustomBox counterUpClickField, CustomBox counterDownClickField, CenterTextInBox titleBox, CenterTextInBox counterBox, int counter, Color textureUpColor, Color textureDownColor, Color textureUpHighLightColor, Color textureDownHighLightColor, Color textureUpBackgroundClickFieldColor, Color textureDownBackgroundClickFieldColor, Color textureUpBackgroundClickFieldHighLightColor, Color textureDownBackgroundClickFieldHighLightColor)
    {
        TextureUpColor = textureUpColor;
        TextureDownColor = textureDownColor;
        TextureUpHighLightColor = textureUpHighLightColor;
        TextureDownHighLightColor = textureDownHighLightColor;
        TextureUpBackgroundClickFieldColor = textureUpBackgroundClickFieldColor;
        TextureDownBackgroundClickFieldColor = textureDownBackgroundClickFieldColor;
        TextureUpBackgroundClickFieldHighLightColor = textureUpBackgroundClickFieldHighLightColor;
        TextureDownBackgroundClickFieldHighLightColor = textureDownBackgroundClickFieldHighLightColor;

        UpTexture = upTexture;
        DownTexture = downTexture;
        CounterUpClickField = counterUpClickField;
        CounterDownClickField = counterDownClickField;
        TitleBox = titleBox;
        CounterBox = counterBox;
        Counter = counter;
    }
}