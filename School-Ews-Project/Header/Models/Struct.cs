
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