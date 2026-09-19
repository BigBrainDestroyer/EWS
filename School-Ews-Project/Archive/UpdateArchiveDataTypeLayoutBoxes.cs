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

    private ArchiveDataTypeBox[] UpdateArchiveDataTypeBox(Dictionary<string, ArchiveStorage> storage, Texture2D[] archiveTextures, Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, Vector2 percentages, float whiteSpaceToFunctionBar, float textSpacing, int archiveDataTypeBoxYOffSet, Color boxColor, Color textColor)
    {
        ArchiveDataTypeBox newSingleDataTypeBox = 
            new ArchiveDataTypeBox(
                boxColor, 
                textColor, 
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()),
                "",
                new RaylibText(),
                new Box()
            );

        newSingleDataTypeBox.Box = 
            CalculateBox(
                parentBoxPixelPosition,
                parentBoxPixelLengths,
                percentages,
                whiteSpaceToFunctionBar
            );

        newSingleDataTypeBox.Text =
            CalculateText(
                newSingleDataTypeBox.Box.PixelLengths.Y,
                textSpacing
            );

        ArchiveDataTypeBox[] newViewArchiveDataTypeBoxes =
            CalculateViewArchiveDataTypeBoxes(
                storage,
                archiveTextures,
                archiveDataTypeBoxYOffSet
            );

        return newViewArchiveDataTypeBoxes;
    }

    private Box CalculateBox(Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, Vector2 percentages, float whiteSpaceToFunctionBar)
    {
        return new Box(
            CalculateDataTypeBoxPosition(parentBoxPixelPosition, parentBoxPixelLengths, whiteSpaceToFunctionBar),
            CalculateDataTypeBoxLengths(parentBoxPixelLengths, percentages)
        );
    }
    
    private Vector2 CalculateDataTypeBoxLengths(Vector2 parentBoxPixelLengths, Vector2 percentages)
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

    private RaylibText CalculateText(float boxHight, float textSpacing)
    {
        return new RaylibText("", boxHight, textSpacing);
    }

    private ArchiveDataTypeBox[] CalculateViewArchiveDataTypeBoxes(Dictionary<string, ArchiveStorage> storage, ArchiveDataTypeBox singleArchiveDataTypeBox,Texture2D[] archiveTextures, string pathSyntax,int archiveDataTypeBoxYOffSet)
    {
        List<ArchiveDataTypeBox> newArchiveDataBoxes = new List<ArchiveDataTypeBox>();

        Dictionary<string, ArchiveStorage> viewStorage = new Dictionary<string, ArchiveStorage>();
        
        string[] storageKeys = storage.Keys.ToArray();
        string[] folderPath;
        string parentFolder;
        int lastPathNameLength;

        storageKeys.Sort();

        foreach(string folderName in storageKeys)
        {
            if (storage[folderName].DataType == ArchiveStorageNames.ProjectFolder)
            {
                viewStorage.Add(
                    folderName,
                    storage[folderName]
                );
            }
            else
            {
                folderPath = folderName.Split(pathSyntax);
                lastPathNameLength = folderPath.Last().Length + 1;

                parentFolder = folderName.Substring(0, folderName.Length - lastPathNameLength);

                if(storage.ContainsKey(parentFolder))
                {
                    if(storage[parentFolder].ViewFolderStorage) 
                    {
                        viewStorage.Add(
                            folderName,
                            storage[folderName]
                        );
                    }
                    else
                    {
                        storage[folderName] = 
                            new ArchiveStorage(
                                storage[folderName].FolderStorage,
                                storage[folderName].DataType,
                                false
                            );
                    }
                }
            }
        }

        storageKeys = viewStorage.Keys.ToArray();

        storageKeys.Sort();

        foreach(string folderName in storageKeys)
        {
            
            
            newArchiveDataBoxes.Add(
                    new ArchiveDataTypeBox(
                        singleArchiveDataTypeBox.BoxColor, 
                        singleArchiveDataTypeBox.TextColor, 
                        new RaylibTexture((viewStorage[folderName].DataType == ArchiveStorageNames.ProjectFolder) ?  :, new Rectangle(), new Rectangle(),
                        "",
                        new RaylibText(),
                        new Box()
                    );
            );
        }
    }
    
    //private ArchiveDataTypeBox[] UpdateDataTypeBoxes(Dictionary<string, ArchiveStorage> currentStorage, Texture2D[] archiveTextures, ArchiveDataTypeBox singleArchiveDataTypeBox, string pathSyntax)
    //{
        //ArchiveDataTypeBox[] newArchiveDataTypeBoxes = new ArchiveDataTypeBox[currentStorage.Count];
        //int counter = 0;
        //
        ////UpdateNogOpenFoldersErbij
        //foreach(string key in currentStorage.Keys)
        //{
        //    string name = key.Split(pathSyntax).Last();
        //
        //    newArchiveDataTypeBoxes[counter] = singleArchiveDataTypeBox;
        //    newArchiveDataTypeBoxes[counter].Texture = archiveTextures[(int)currentStorage[key].DataType];
        //    newArchiveDataTypeBoxes[counter].Name = name;
        //
        //    counter++;
        //    
        //    foreach(string filePaths in currentStorage[key].FolderStorage)
        //    {
        //        name = filePaths.Split(pathSyntax).Last();
//
//        newArchiveDataTypeBoxes[counter] = singleArchiveDataTypeBox;
//        newArchiveDataTypeBoxes[counter].Texture = archiveTextures[(int)ArchiveStorageNames.File];
//        newArchiveDataTypeBoxes[counter].Name = name;
//        
//        counter++;
        //}
    //}
    //
    //return newArchiveDataTypeBoxes;
    //}
}