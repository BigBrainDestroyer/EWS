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
    public Color X;
    public Color Y;

    public ReactionColorCombinations(Color x, Color y)
    {
        X = x;
        Y = y;
    }
}

public struct TripleStateReactionColorCombinations
{
    public Color X;
    public Color Y;
    public Color Z;

    public TripleStateReactionColorCombinations(Color x, Color y, Color z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}

public struct StringSliderColors
{
    //Buttons
    public TripleStateReactionColorCombinations TextureUpColors;
    public TripleStateReactionColorCombinations TextureDownColors;
    public TripleStateReactionColorCombinations UpClickFieldColors;
    public TripleStateReactionColorCombinations DownClickFieldColors;

    //Strings
    public ReactionColorCombinations TitleTextColors;
    public ReactionColorCombinations StringSliderTextColors;
    public ReactionColorCombinations TitleBoxColors;
    public ReactionColorCombinations StringSliderBoxColors;

    public StringSliderColors(TripleStateReactionColorCombinations textureUpColors, TripleStateReactionColorCombinations textureDownColors, TripleStateReactionColorCombinations upClickFieldColors, TripleStateReactionColorCombinations downClickFieldColors, ReactionColorCombinations titleTextColors, ReactionColorCombinations stringSliderTextColors, ReactionColorCombinations titleBoxColors, ReactionColorCombinations stringSliderBoxColors)
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
    public TripleStateReactionColorCombinations SymbolColors;
    public TripleStateReactionColorCombinations BoxColors;

    public ButtonColors(TripleStateReactionColorCombinations symbolColor, TripleStateReactionColorCombinations boxColors)
    {
        SymbolColors = symbolColor;
        BoxColors = boxColors;
    }
}