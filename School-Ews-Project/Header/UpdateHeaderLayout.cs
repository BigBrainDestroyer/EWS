using System.Numerics;

namespace Program;

public class UpdateHeaderLayout
{
    private readonly AppState _state;

    public UpdateHeaderLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Update()
    {
        var _header = _state.HeaderState;

        _header.HeaderObjectBoxes = CalculateHeaderObjectBoxes();
    }
    
    public Dictionary<DrawHeaderObjectNames, Box> CalculateHeaderObjectBoxes(Dictionary<DrawHeaderObjectNames, Vector2> headerObjectWidthPercentages, Vector2 parentBoxPositions, Vector2 parentBoxLength, float headerDrawObjectsTotalWhiteSpacePercentage, float headerStartObjectPercentageWidth)
    {
        Vector2 positionFirstObject = CalculatePositionY(parentBoxPositions, parentBoxLength, headerDrawObjectsTotalWhiteSpacePercentageY);

        Dictionary<DrawHeaderObjectNames, Box> newHeaderObjectBoxes =
            CalculateNewHeaderObjectBoxes(headerObjectWidthPercentages);

    }

    public float CalculatePositionY(Vector2 parentBoxPositions, Vector2 parentBoxLengths, float headerDrawObjectsTotalWhiteSpacePercentage)
    {
        if(parentBoxPositions.Y > parentBoxPositions.X)
        {
            return parentBoxPositions.Y + (((headerDrawObjectsTotalWhiteSpacePercentage/100) * parentBoxLengths.Y)/2);
        }
        
        return parentBoxPositions.Y + (((headerDrawObjectsTotalWhiteSpacePercentage/100) * parentBoxLengths.Y)/2);
    }

    public Dictionary<DrawHeaderObjectNames, Box> CalculateNewHeaderObjectBoxes(Dictionary<DrawHeaderObjectNames, Vector2> headerObjectWidthPercentages, Vector2 parentBoxPositions, Vector2 parentBoxLength, float positionY, float headerStartObjectPercentageWidth)
    {
        Dictionary<DrawHeaderObjectNames, Box> newHeaderObjectBoxes = new Dictionary<DrawHeaderObjectNames, Box>();

        DrawHeaderObjectNames[] headerObjectNames = Enum.GetValues<DrawHeaderObjectNames>();

        foreach(DrawHeaderObjectNames objectName in headerObjectNames)
        {           
            
            
            newHeaderObjectBoxes.Add(
                objectName,
                new Box(
                    
                )
            );
        }
    }
}