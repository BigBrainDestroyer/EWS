using System.Numerics;
using Raylib_cs;

namespace Program;

public class ArchiveCalculateEditDataType 
{
    private readonly AppState _state;
    private readonly UpdateArchiveClicks _archiveClick;
    private readonly UpdateArchiveStorage _archiveStorage;

    public ArchiveCalculateEditDataType(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _archiveClick = new UpdateArchiveClicks(_state);
        _archiveStorage = new UpdateArchiveStorage(_state);
    }

    public void EditDataType()
    {
        var _archive = _state.archive;

        CalculateEditLayout();

        _archiveClick.EditDataTypeActionClick(
            _archive.ArchiveEditDataType,
            _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Edit],
            _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit],
            _archive.EditDataHighLightBoxColor,
            _archive.DeleteHighLightTextColor, 
            _archive.RenameHighLightTextColor
        );

        if(_archive.ArchiveEditDataType.DeleteActive)
        {
            _archive.Storage = 
                DeletingDataTypeAction(
                    _archive.Storage,
                    _archive.ArchiveEditDataType.DataType,
                    _archive.ArchiveEditDataType.DataTypePath,
                    _archive.ProjectsDataLocation
                );

            _archive.ArchiveEditDataType.DataTypePath = "";

            _archive.ArchiveEditDataType.DeleteActive = false;
        }
    }

    private void CalculateEditLayout()
    {
        var _archive = _state.archive;
        var _mainBoxes = _state.common.boxes;


        UpdateTextFont();

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
            CalculatingEditActionClickFieldBox(
                _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Edit],
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit],
                _archive.ArchiveEditDataType.DeleteClickField.CornerStates,
                _archive.ArchiveEditDataType.RenameClickField.CornerStates,
                _mainBoxes.MainBoxesCornerRadius[MainBoxNames.Archive]
            );

        _archive.ArchiveEditDataType.DeleteBoxColor = _archive.BackgroundColor;
        _archive.ArchiveEditDataType.RenameBoxColor = _archive.BackgroundColor;
        _archive.ArchiveEditDataType.DeleteTextColor = _archive.EditDataTypesTextColor;
        _archive.ArchiveEditDataType.RenameTextColor = _archive.EditDataTypesTextColor;
    }

    private void UpdateTextFont()
    {
        var _archive = _state.archive;

        _archive.ArchiveEditDataType.TextFont =
            Load.UpdateMonoSpaceFontEx(
                _archive.ArchiveEditDataType.TextFont,
                _archive.ArchiveEditDataType.FontPath,
                _archive.ArchiveEditDataType.CharHeight,
                _archive.ArchiveEditDataType.MaxFontHeightOffSet
            );
    }

    private (Vector2, int) CalculateNewCharHeight(Font textFont, Vector2 parentBoxLengths, float charSpacing, int totalTextCharLength)
    {
        Vector2 totalTextSize = Raylib.MeasureTextEx(textFont, "t", parentBoxLengths.Y, charSpacing);

        if(totalTextSize.X * totalTextCharLength < parentBoxLengths.X)
        {
            return (totalTextSize, (int)parentBoxLengths.Y);
        }
        else
        {
            int charHeight = (int)((totalTextSize.Y / totalTextSize.X) * (parentBoxLengths.X / (totalTextCharLength + 1)));

            return (Raylib.MeasureTextEx(textFont, "t", charHeight, charSpacing), 
                charHeight);
        }
    }

    private float CalculatingWhiteSpaceBetweenText(Vector2 parentBoxLengths, Vector2 totalTextSize, float whiteSpaceWidthPercentage, int totalTextCharLength)
    {
        float totalTextPixelWidth = totalTextSize.X * totalTextCharLength;
        float whiteSpacePixelWidth = (whiteSpaceWidthPercentage/100f) * parentBoxLengths.X;

        if(whiteSpacePixelWidth <= parentBoxLengths.X - totalTextPixelWidth)
        {
            return whiteSpacePixelWidth;
        }
        else
        {
            return parentBoxLengths.X - totalTextPixelWidth;
        }
    }

    private Vector2 CalculateTextDrawPosition(Vector2 parentBoxPosition, Vector2 parentBoxLengths, Vector2 totalTextSize, float whiteSpacePixelLengthBetweenText, int totalTextCharLength)
    {
        return new Vector2(
            parentBoxPosition.X + ((parentBoxLengths.X - whiteSpacePixelLengthBetweenText - (totalTextSize.X * totalTextCharLength)) / 2),
            parentBoxPosition.Y + ((parentBoxLengths.Y - totalTextSize.Y) / 2)
        ); 
    }

    private (CustomBox, CustomBox) CalculatingEditActionClickFieldBox(Vector2 parentBoxPosition, Vector2 parentBoxLengths, BoolVector4 DeleteActionCornerStates, BoolVector4 RenameActionCornerState, float parentBoxCornerRadius)
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

    public Dictionary<string, ArchiveStorage> DeletingDataTypeAction(Dictionary<string, ArchiveStorage> currentStorage, ArchiveStorageNames dataType, string dataTypeCurrentPath, string projectDataLocation)
    {
        if(dataType != ArchiveStorageNames.File && Directory.Exists(dataTypeCurrentPath))
        {
            Directory.Delete(dataTypeCurrentPath, true);
        }
        else if(File.Exists(dataTypeCurrentPath))
        {
            File.Delete(dataTypeCurrentPath);
        }

        currentStorage = _archiveStorage.LoadStorage(currentStorage, projectDataLocation);

        return currentStorage;
    }
    
    public Dictionary<string, ArchiveStorage> RenameDataTypeAction(Dictionary<string, ArchiveStorage> currentStorage, ArchiveStorageNames dataType, string dataTypeCurrentPath, string newName, string pathSyntax, string projectDataLocation)
    {
        currentStorage = _archiveStorage.LoadStorage(currentStorage, projectDataLocation);

        string newPath = dataTypeCurrentPath.Substring(0, dataTypeCurrentPath.Length - dataTypeCurrentPath.Split(pathSyntax).Last().Length) + newName;

        if(dataType != ArchiveStorageNames.File && Directory.Exists(dataTypeCurrentPath) && !Directory.Exists(newPath))
        {
            ArchiveStorage archiveDataTypeState = currentStorage[dataTypeCurrentPath];

            currentStorage.Remove(dataTypeCurrentPath);
            currentStorage.Add(
                newPath,
                archiveDataTypeState
            );

            Directory.Move(dataTypeCurrentPath, newPath);
        }
        else if(File.Exists(dataTypeCurrentPath) && !File.Exists(newPath))
        {
            File.Move(dataTypeCurrentPath, newPath);
        }

        currentStorage = _archiveStorage.LoadStorage(currentStorage, projectDataLocation);

        return currentStorage;
    }
}