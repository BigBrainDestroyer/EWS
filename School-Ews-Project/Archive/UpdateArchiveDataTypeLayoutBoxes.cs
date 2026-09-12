using Raylib_cs;
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
        var _archive = _state.archive;

        _archive.SingleDataTypeBox = 
            UpdateArchiveDataTypeBox(
                _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Archive],
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Archive],
                _archive.ArchiveWhiteSpaceToFunctionBar 
            );


        //_archive.DataTypeBoxes =
           //UpdateDataTypeBoxes(
           //    _archive.Storage,
           //    _archive.ArchiveTextures,
           //    _archive.SingleDataTypeBox,
           //    _archive.PathSyntax
           //);
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

    private ArchiveDataTypeBox[] UpdateDataTypeBoxes(Dictionary<string, ArchiveStorage> currentStorage, Texture2D[] archiveTextures, ArchiveDataTypeBox singleArchiveDataTypeBox, string pathSyntax)
    {
        ArchiveDataTypeBox[] newArchiveDataTypeBoxes = new ArchiveDataTypeBox[currentStorage.Count];
        int counter = 0;

        //UpdateNogOpenFoldersErbij
        foreach(string key in currentStorage.Keys)
        {
            string name = key.Split(pathSyntax).Last();

            newArchiveDataTypeBoxes[counter] = singleArchiveDataTypeBox;
            newArchiveDataTypeBoxes[counter].Texture = archiveTextures[(int)currentStorage[key].DataType];
            newArchiveDataTypeBoxes[counter].Name = name;

            counter++;
            
            foreach(string filePaths in currentStorage[key].FolderStorage)
            {
                name = filePaths.Split(pathSyntax).Last();

                newArchiveDataTypeBoxes[counter] = singleArchiveDataTypeBox;
                newArchiveDataTypeBoxes[counter].Texture = archiveTextures[(int)ArchiveStorageNames.File];
                newArchiveDataTypeBoxes[counter].Name = name;
                
                counter++;
            }
        }

        return newArchiveDataTypeBoxes;
    }
}