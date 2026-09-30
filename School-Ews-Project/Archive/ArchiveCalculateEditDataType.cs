using System.Numerics;
using Raylib_cs;

namespace Program;

public class ArchiveCalculateEditDataType 
{
    private readonly AppState _state;

    public ArchiveCalculateEditDataType(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void EditDataType()
    {
        CalculateEditLayout();
    }

    private void CalculateEditLayout()
    {
        var _archive = _state.archive;
        var _mainBoxes = _state.common.boxes;

        int totalTextCharLength = _archive.ArchiveEditDataType.RenameText.Length + _archive.ArchiveEditDataType.DeleteText.Length;

        (Vector2 totalTextSize, _archive.ArchiveEditDataType.CharHeight) =
            CalculateNewCharHeight(
                _archive.ArchiveEditDataType.TextFont,
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit],
                _archive.ArchiveEditDataType.CharSpacing,
                totalTextCharLength
            );

        _archive.ArchiveEditDataType.WhiteSpacePixelLengthBetweenText =
            CalculatingWhiteSpaceBetweenText(
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit],
                totalTextSize,
                _archive.ArchiveEditDataType.WhiteSpacePercentageBetweenText,
                totalTextCharLength
            );

        _archive.ArchiveEditDataType.DrawTextPixelPosition = 
            CalculateTextDrawPosition(
                _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Edit],
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit],
                totalTextSize,
                _archive.ArchiveEditDataType.WhiteSpacePixelLengthBetweenText,
                totalTextCharLength
            );

        (_archive.ArchiveEditDataType.DeleteClickField, _archive.ArchiveEditDataType.RenameClickField) =
            CalculatingEditActionClickField(
                _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Edit],
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit],
                _archive.ArchiveEditDataType.DeleteClickField.CornerStates,
                _archive.ArchiveEditDataType.RenameClickField.CornerStates,
                _mainBoxes.MainBoxesCornerRadius[MainBoxNames.Archive]
            );
    }

    public (Vector2, float) CalculateNewCharHeight(Font textFont, Vector2 parentBoxLengths, float charSpacing, int totalTextCharLength)
    {
        Vector2 totalTextSize = Raylib.MeasureTextEx(textFont, "t", parentBoxLengths.Y, charSpacing);

        if(totalTextSize.X * totalTextCharLength < parentBoxLengths.X)
        {
            return (totalTextSize, parentBoxLengths.Y);
        }
        else
        {
            float charHeight = (totalTextSize.Y / totalTextSize.X) * (parentBoxLengths.X / (totalTextCharLength + 1));

            return (Raylib.MeasureTextEx(textFont, "t", charHeight, charSpacing), 
                charHeight);
        }
    }

    public float CalculatingWhiteSpaceBetweenText(Vector2 parentBoxLengths, Vector2 totalTextSize, float whiteSpaceWidthPercentage, int totalTextCharLength)
    {
        float totalTextPixelWidth = totalTextSize.X * totalTextCharLength;
        float whiteSpacePixelWidth = (whiteSpaceWidthPercentage/100f) * parentBoxLengths.X;

        if(whiteSpacePixelWidth >= parentBoxLengths.X - totalTextPixelWidth)
        {
            return whiteSpacePixelWidth;
        }
        else
        {
            return parentBoxLengths.X - totalTextPixelWidth;
        }
    }

    public Vector2 CalculateTextDrawPosition(Vector2 parentBoxPosition, Vector2 parentBoxLengths, Vector2 totalTextSize, float whiteSpacePixelLengthBetweenText, int totalTextCharLength)
    {
        return new Vector2(
            parentBoxPosition.X + ((parentBoxLengths.X - whiteSpacePixelLengthBetweenText - (totalTextSize.X * totalTextCharLength)) / 2),
            parentBoxPosition.Y + ((parentBoxPosition.Y - totalTextSize.Y) / 2)
        );
    }

    public (CustomBox, CustomBox) CalculatingEditActionClickField(Vector2 parentBoxPosition, Vector2 parentBoxLengths, BoolVector4 DeleteActionCornerStates, BoolVector4 RenameActionCornerState, float parentBoxCornerRadius)
    {
        Vector2 newBoxLengths = new Vector2(parentBoxLengths.X / 2, parentBoxLengths.Y);

        return(
            new CustomBox(
                DeleteActionCornerStates,
                parentBoxPosition,
                newBoxLengths,
                parentBoxCornerRadius
            ),
            new CustomBox(                
            RenameActionCornerState,
            new Vector2(parentBoxPosition.X + newBoxLengths.X, parentBoxPosition.Y),
            newBoxLengths,
            parentBoxCornerRadius)
        );
    }
}