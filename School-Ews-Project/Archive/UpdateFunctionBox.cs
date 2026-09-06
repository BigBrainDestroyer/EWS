using System.Numerics;

namespace Program;

public class UpdateFunctionBox
{
    private readonly AppState _state;

    public UpdateFunctionBox(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void FunctionBox()
    {
        var _layout = _state.archive;
        var _boxes = _state.common.boxes;

        _layout.ArchiveFunctionBarBoxPixelWhiteSpace =
            CalculateArchiveFunctionBarBoxWhiteSpace(
                _layout.ArchiveBoxPixelLengths[ArchiveBoxesNames.Header],
                _layout.ArchiveFunctionBarBoxPercentageWhiteSpace
            );

        _layout.ArchiveFunctionBarBoxPixelLengths =
            CalculateArchiveFunctionBarBoxPixelLength(
                _layout.ArchiveBoxPixelLengths[ArchiveBoxesNames.Header],
                _layout.ArchiveFunctionBarBoxPixelWhiteSpace
            );

        _layout.ArchiveFunctionBarBoxPosition =
            CalculateArchiveFunctionBarBoxPositions(
                _layout.ArchiveBoxPixelPositions[ArchiveBoxesNames.Header],
                _layout.ArchiveFunctionBarBoxPixelWhiteSpace
            );

        _layout.ArchiveFunctionBarFunctionsPixelWhiteSpace =
            CalculateArchiveFunctionBarFunctionsPixelWhiteSpace(
                _layout.ArchiveFunctionBarBoxPixelLengths,
                _layout.ArchiveFunctionBarFunctionsPercentageWhiteSpace
            );

        _layout.AddOnButtonPixelLengths =
            CalculateAddOnButtonPixelLengths(
                _layout.ArchiveFunctionBarBoxPixelLengths
            );

        _layout.SearchBarPixelLengths =
            CalculateSearchBarPixelLengths(
                _layout.ArchiveFunctionBarBoxPixelLengths,
                _layout.AddOnButtonPixelLengths,
                _layout.ArchiveFunctionBarFunctionsPixelWhiteSpace
            );

        _layout.SearchBarPixelPosition =
            CalculateSearchBarPixelPosition(
                _layout.ArchiveFunctionBarBoxPosition
            );

        _layout.AddOnButtonPixelPosition =
            CalculateAddOnButtonPosition(
                _layout.SearchBarPixelPosition,
                _layout.SearchBarPixelLengths.X,
                _layout.ArchiveFunctionBarFunctionsPixelWhiteSpace
            );
    }

    private float CalculateArchiveFunctionBarBoxWhiteSpace(Vector2 parentBoxLengths, float percentageWhiteSpace)
    {
        float newArchiveFunctionBarBoxWhiteSpace = (parentBoxLengths.X < parentBoxLengths.Y) ? parentBoxLengths.X * (percentageWhiteSpace/100f) : parentBoxLengths.Y * (percentageWhiteSpace/100f);

        return newArchiveFunctionBarBoxWhiteSpace;
    }

    private Vector2 CalculateArchiveFunctionBarBoxPixelLength(Vector2 parentBoxLengths, float pixelWhiteSpace)
    {
        Vector2 newArchiveFunctionBarBoxPixelLength =
            new Vector2(
                parentBoxLengths.X - (pixelWhiteSpace * 2),
                parentBoxLengths.Y - (pixelWhiteSpace * 2)
            );

        return newArchiveFunctionBarBoxPixelLength;
    }

    private Vector2 CalculateArchiveFunctionBarBoxPositions(Vector2 parentBoxPosition, float pixelWhiteSpace)
    {
        Vector2 newArchiveFunctionBarBoxPositions =
            new Vector2(
                parentBoxPosition.X + pixelWhiteSpace,
                parentBoxPosition.Y + pixelWhiteSpace
            );

        return newArchiveFunctionBarBoxPositions;
    }

    private float CalculateArchiveFunctionBarFunctionsPixelWhiteSpace(Vector2 parentBoxLengths, float percentageWhiteSpace)
    {
        float newArchiveFunctionBarFunctionsPixelWhiteSpace = parentBoxLengths.X * (percentageWhiteSpace / 100f);

        return newArchiveFunctionBarFunctionsPixelWhiteSpace;
    }

    private Vector2 CalculateAddOnButtonPixelLengths(Vector2 parentBoxLengths)
    {
        float newAddOnButtonLength = (parentBoxLengths.X < parentBoxLengths.Y) ? parentBoxLengths.X : parentBoxLengths.Y;

        Vector2 newAddOnButtonPixelLengths =
            new Vector2(
                newAddOnButtonLength,
                newAddOnButtonLength
            );

        return newAddOnButtonPixelLengths;
    }

    private Vector2 CalculateSearchBarPixelLengths(Vector2 parentBoxLengths, Vector2 addOnButtonLength, float pixelWhiteSpace)
    {
        Vector2 newSearchBarPixelLengths =
            new Vector2(
                parentBoxLengths.X - addOnButtonLength.X - pixelWhiteSpace,
                parentBoxLengths.Y
            );

        return newSearchBarPixelLengths;
    }

    private Vector2 CalculateSearchBarPixelPosition(Vector2 parentBoxPosition)
    {
        return parentBoxPosition;
    }

    private Vector2 CalculateAddOnButtonPosition(Vector2 searchBarPixelPosition, float searchBarWidth, float pixelWhiteSpace)
    {
        Vector2 newAddOnButtonPosition =
            new Vector2(
                searchBarPixelPosition.X + searchBarWidth + pixelWhiteSpace,
                searchBarPixelPosition.Y
            );

        return newAddOnButtonPosition;
    }

}