using Raylib_cs;

namespace Program;

public struct HeaderObjectPercentageWidthVariables
{
    public float TotalObjectWidthSpace { get; set; }
    public float TotalObjectWhiteSpaceWidth { get; set; }

    public HeaderObjectPercentageWidthVariables(float totalObjectWidthSpace, float totalObjectWhiteSpaceWidth)
    {
        TotalObjectWidthSpace = totalObjectWidthSpace;
        TotalObjectWhiteSpaceWidth = totalObjectWhiteSpaceWidth;
    }
}

public struct ReactionColorCombinations
{
    public Color NormalColor;
    public Color ReactionColor;

    public ReactionColorCombinations(Color normalColor, Color reactionColor)
    {
        NormalColor = normalColor;
        ReactionColor = reactionColor;
    }
}

public struct StringSliderColors
{
    public ReactionColorCombinations TextureUpColors;
    public ReactionColorCombinations TextureDownColors;
    public ReactionColorCombinations UpClickFieldColors;
    public ReactionColorCombinations DownClickFieldColors;
    public ReactionColorCombinations TitleTextColors;
    public ReactionColorCombinations StringSliderTextColors;
    public ReactionColorCombinations TitleBoxColors;
    public ReactionColorCombinations StringSliderBoxColors;

    public StringSliderColors(ReactionColorCombinations textureUpColors, ReactionColorCombinations textureDownColors, ReactionColorCombinations upClickFieldColors, ReactionColorCombinations downClickFieldColors, ReactionColorCombinations titleTextColors, ReactionColorCombinations stringSliderTextColors, ReactionColorCombinations titleBoxColors, ReactionColorCombinations stringSliderBoxColors)
    {
        TextureUpColors = textureUpColors;
        TextureDownColors = textureDownColors;
        UpClickFieldColors = upClickFieldColors;
        DownClickFieldColors = downClickFieldColors;
        TitleTextColors = titleTextColors;
        StringSliderTextColors = stringSliderTextColors;
        TitleBoxColors = titleBoxColors;
        StringSliderBoxColors = stringSliderBoxColors;
    }
}

public struct ButtonColors
{
    public ReactionColorCombinations SymbolColors;
    public ReactionColorCombinations BoxColors;

    public ButtonColors(ReactionColorCombinations symbolColor, ReactionColorCombinations boxColors)
    {
        SymbolColors = symbolColor;
        BoxColors = boxColors;
    }
}