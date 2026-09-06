using System.Numerics;

namespace Program;

public class UpdateArchiveDataTypeLayoutBoxes
{
    private readonly AppState _state;

    public UpdateArchiveDataTypeLayoutBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ArchiveDataTypeLayoutBoxes()
    {
        var _layout = _state.archive;

        _layout.DataTypeBox = 
            UpdateArchiveDataTypeBox(
                _layout.ArchiveBoxPixelPositions[ArchiveBoxesNames.Archive],
                _layout.ArchiveBoxPixelLengths[ArchiveBoxesNames.Archive],
                _layout.ArchiveWhiteSpaceToFunctionBar 
            );
    }

    private ArchiveDataTypeBox UpdateArchiveDataTypeBox(Vector2 parentBoxPosition, Vector2 parentBoxPixelLengths, float whiteSpaceToFunctionBar)
    {
        ArchiveDataTypeBox newDataTypeBox = new ArchiveDataTypeBox();

        newDataTypeBox.Length = 
            CalculateDataTypeBoxLengths(
                newDataTypeBox.Percentages, 
                parentBoxPixelLengths
            );

        newDataTypeBox.Position =
            CalculateDataTypeBoxPosition(
                parentBoxPosition,
                parentBoxPixelLengths,
                whiteSpaceToFunctionBar
            );

        return newDataTypeBox;
    }

    private Vector2 CalculateDataTypeBoxLengths(Vector2 percentages, Vector2 parentBoxPixelLengths)
    {
        Vector2 newDataBoxLengths =
            new Vector2(
                parentBoxPixelLengths.X * (percentages.X/100f),
                parentBoxPixelLengths.Y * (percentages.Y/100f)
            );

        return newDataBoxLengths;
    }

    private Vector2 CalculateDataTypeBoxPosition(Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths , float whiteSpaceToFunctionBar)
    {
        Vector2 newDataTypePosition =
            new Vector2(
                parentBoxPixelPosition.X,
                parentBoxPixelLengths.Y * (whiteSpaceToFunctionBar/100f)
            );

        return newDataTypePosition;
    }
}