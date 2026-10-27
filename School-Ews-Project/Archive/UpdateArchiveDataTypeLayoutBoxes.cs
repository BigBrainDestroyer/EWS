using Raylib_cs;
using System.Numerics;
using ShareData;

namespace Program;

public class UpdateArchiveDataTypeLayoutBoxes
{
    private readonly AppState _state;
    private readonly UpdateArchiveClicks _archiveClick;

    public UpdateArchiveDataTypeLayoutBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _archiveClick = new UpdateArchiveClicks(_state);
    }

    public void ArchiveDataTypeLayoutBoxes()
    {
        var _archive = _state.archive;

        if(_archive.DataTypeBoxFont.BaseSize != _archive.DataTypeBoxCharLoadFontSize)
        {
            _archive.DataTypeBoxFont = Load.UpdateMonoSpaceFontEx(
                _archive.DataTypeBoxFont,
                _archive.DataTypeBoxFontPath,
                (int)_archive.DataTypeBoxCharLoadFontSize,
                _archive.MaxCharHeightOffSet
            );
        }

        (_archive.DataTypeBoxes, _archive.TotalViewStorageCount) = UpdateArchiveDataTypeBox(
            _archive.DataTypeBoxFont,
            _archive.Storage,
            _archive.ArchiveTextures,
            _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Archive],
            _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Archive],
            _archive.DataTypeBoxPercentage,
            App.directoryInfo.PathSyntax,
            _archive.ArchiveWhiteSpaceToFunctionBar,
            _archive.ArchiveDataTypeBoxTextSpacing,
            _archive.DataTypeBoxTextureTextPercentageOffSet,
            _archive.ArchiveDataTypeBoxRowOffSet,
            _archive.BackgroundColor,
            _archive.DataTypeBoxesTextureColor,
            _archive.DataTypeBoxesTextColor
        );

        //check archive mouse interactions
        (_archive.Storage, _archive.DataTypeBoxes, _archive.ArchiveEditDataType, _archive.CurrentStorageDataLocation, _archive.LoadedFileDirectory) =
            _archiveClick.DataTypeBoxClick(
                _archive.Storage,
                _archive.DataTypeBoxes,
                _archive.ArchiveEditDataType,
                _archive.ArchiveNamingDataTypeTextBox,
                _archive.AddDataTypeMenu,
                _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Archive],
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Archive],
                _archive.ProjectsDataLocation,
                _archive.CurrentStorageDataLocation, 
                _archive.LoadedFileDirectory,
                App.directoryInfo.PathSyntax,
                _archive.DataTypeBoxesMouseOnBoxColor
            );
            
        _archive.DataTypeBoxes = 
            UpdateNewArchiveDataTypeBoxColors(
                _archive.DataTypeBoxes,
                _archive.ArchiveEditDataType.DataType,
                _archive.CurrentStorageDataLocation, 
                _archive.ArchiveEditDataType.DataTypePath,
                _archive.LoadedFileDirectory,
                App.directoryInfo.PathSyntax,
                _archive.DataTypeBoxesLoadedFileColor,
                _archive.DataTypeBoxesEditDataTypeColor,
                _archive.DataTypeBoxesCurrentStorageLocationColor
            );
    }

    private (ArchiveDataTypeBox[], int) UpdateArchiveDataTypeBox(Font textFont, Dictionary<string, ArchiveStorage> storage, Texture2D[] archiveTextures, Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, Vector2 percentages, string pathSyntax, float whiteSpaceToFunctionBar, float textSpacing, float textureTextPercentageOffSet, int archiveDataTypeBoxRowOffSet, Color boxColor, Color textureColor, Color textColor)
    {
        ArchiveDataTypeBox newSingleDataTypeBox =
            new ArchiveDataTypeBox(
                boxColor,
                textureColor,
                textColor,
                new RaylibTexture(new Texture2D(), new Rectangle(), new Rectangle()),
                "",
                "",
                ArchiveStorageNames.Folder,
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

        Vector2 newTextureSize =
            CalculateNewTextureSize(
                newSingleDataTypeBox.Box
            );

        int newCharSpace =
            CalculateNewCharSpace(
                newSingleDataTypeBox.Box,
                newSingleDataTypeBox.Text,
                textFont,
                newTextureSize,
                textureTextPercentageOffSet
            );

        int newDataViewBoxesCount =
            CalculateNewDataViewBoxesCount(
                parentBoxPixelLengths.Y,
                newSingleDataTypeBox.Box.PixelLengths.Y,
                whiteSpaceToFunctionBar
            );

        (ArchiveDataTypeBox[] newViewArchiveDataTypeBoxes, int totalViewStorageCount) =
            CalculateViewArchiveDataTypeBoxes(
                newSingleDataTypeBox.Text, 
                textFont, 
                storage, 
                newSingleDataTypeBox, 
                archiveTextures, 
                newTextureSize, 
                pathSyntax,  
                newCharSpace,
                archiveDataTypeBoxRowOffSet, 
                newDataViewBoxesCount
            );

        return (newViewArchiveDataTypeBoxes, totalViewStorageCount);
    }

    private Box CalculateBox(Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, Vector2 percentages, float whitePercentageSpaceToFunctionBar)
    {
        return new Box(
            CalculateDataTypeBoxPosition(parentBoxPixelPosition, whitePercentageSpaceToFunctionBar),
            CalculateDataTypeBoxLengths(parentBoxPixelLengths, percentages)
        );
    }

    private Vector2 CalculateDataTypeBoxLengths(Vector2 parentBoxPixelLengths, Vector2 percentages)
    {
        Vector2 newDataBoxLengths =
            new Vector2(
                parentBoxPixelLengths.X * (percentages.X / 100f),
                parentBoxPixelLengths.Y * (percentages.Y / 100f)
            );

        return newDataBoxLengths;
    }

    private Vector2 CalculateNewTextureSize(Box dataBoxSize)
    {
        return (dataBoxSize.PixelLengths.Y > dataBoxSize.PixelLengths.X) ?
        new Vector2(dataBoxSize.PixelLengths.X, dataBoxSize.PixelLengths.X) :
        new Vector2(dataBoxSize.PixelLengths.Y, dataBoxSize.PixelLengths.Y);
    }

    private Vector2 CalculateDataTypeBoxPosition(Vector2 parentBoxPixelPosition, float whitePercentageSpaceToFunctionBar)
    {
        Vector2 newDataTypePosition =
            new Vector2(
                parentBoxPixelPosition.X,
                parentBoxPixelPosition.Y + (whitePercentageSpaceToFunctionBar / 100f)
            );

        return newDataTypePosition;
    }

    private int CalculateNewCharSpace(Box newSingleDataTypeBox, RaylibText text, Font textFont, Vector2 newTextureSize, float textureTextPercentageOffSet)
    {
        //Als de textFont niet geladen is kan is Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X gelijk aan 0 dus int is maximaal
        
        return ((newSingleDataTypeBox.PixelLengths.X - newTextureSize.X - (textureTextPercentageOffSet / 100f * newSingleDataTypeBox.PixelLengths.X)) / Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X <= 0 ) ? 0 :
            (int)((newSingleDataTypeBox.PixelLengths.X - newTextureSize.X - ((textureTextPercentageOffSet/100f)*newSingleDataTypeBox.PixelLengths.X)) / Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X);
    }

    private RaylibText CalculateText(float boxHeight, float textSpacing)
    {
        return new RaylibText("", boxHeight, textSpacing);
    }

    private int CalculateNewDataViewBoxesCount(float parentBoxPixelLengthY, float newSingleDataTypeBoxLengthY, float whiteSpaceToFunctionBar)
    {
        return (int)((parentBoxPixelLengthY - whiteSpaceToFunctionBar) / newSingleDataTypeBoxLengthY);
    }

    private (ArchiveDataTypeBox[], int) CalculateViewArchiveDataTypeBoxes(RaylibText text, Font textFont, Dictionary<string, ArchiveStorage> storage, ArchiveDataTypeBox singleArchiveDataTypeBox, Texture2D[] archiveTextures, Vector2 textureSize, string pathSyntax, int charSpace, int archiveDataTypeBoxRowOffSet, int dataViewBoxesCount)
    {
        List<ArchiveDataTypeBox> newArchiveDataBoxes = new List<ArchiveDataTypeBox>();

        Dictionary<string, ArchiveStorage> viewStorage = new Dictionary<string, ArchiveStorage>();

        string[] storageKeys = storage.Keys.ToArray();
        string[] folderPath;
        string parentFolder;
        int lastPathNameLength;

        storageKeys.Sort(StringComparer.Ordinal);

        foreach (string folderName in storageKeys)
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
                lastPathNameLength = folderPath.Last().Length + pathSyntax.Length;

                parentFolder = folderName.Substring(0, folderName.Length - lastPathNameLength);

                if (storage.ContainsKey(parentFolder))
                {
                    if (storage[parentFolder].ViewFolderStorage)
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
        
        storageKeys.Sort(StringComparer.Ordinal);
        
        string viewText = "";

        int totalViewStorageCount = archiveDataTypeBoxRowOffSet;

        float heightIncrement = 0;
        
        foreach (string folderLocation in storageKeys)
        {
            if (dataViewBoxesCount <= 0) break;

            if(archiveDataTypeBoxRowOffSet <= 0)
            {
                int ArchiveStorageTexturePosition = (viewStorage[folderLocation].DataType == ArchiveStorageNames.ProjectFolder) ? (int)ArchiveStorageNames.ProjectFolder : (int)ArchiveStorageNames.Folder;

                ArchiveStorageTexturePosition += (viewStorage[folderLocation].ViewFolderStorage) ? 3 : 0;

                viewText = folderLocation.Split(pathSyntax).Last();

                viewText = (viewText.Length > charSpace) ? viewText.Substring(0, (int)charSpace) : viewText;

                newArchiveDataBoxes.Add(
                    new ArchiveDataTypeBox(
                        singleArchiveDataTypeBox.BoxColor,
                        singleArchiveDataTypeBox.TextureColor,
                        singleArchiveDataTypeBox.TextColor,
                        new RaylibTexture(archiveTextures[ArchiveStorageTexturePosition],
                            new Rectangle(0, 0, archiveTextures[ArchiveStorageTexturePosition].Width, archiveTextures[ArchiveStorageTexturePosition].Height),
                            new Rectangle(
                                singleArchiveDataTypeBox.Box.PixelPosition.X,
                                singleArchiveDataTypeBox.Box.PixelPosition.Y + heightIncrement,
                                textureSize.X,
                                textureSize.Y)
                        ),
                        viewText,
                        folderLocation,
                        (viewStorage[folderLocation].DataType == ArchiveStorageNames.ProjectFolder) ? ArchiveStorageNames.ProjectFolder : ArchiveStorageNames.Folder,
                        new RaylibText(folderLocation.Split(pathSyntax).Last(), text.TextSize, text.TextSpacing),
                        new Box(
                            new Vector2(
                                singleArchiveDataTypeBox.Box.PixelPosition.X,
                                singleArchiveDataTypeBox.Box.PixelPosition.Y + heightIncrement
                            ),
                            singleArchiveDataTypeBox.Box.PixelLengths
                        )
                    )
                );

                dataViewBoxesCount--;
                if (dataViewBoxesCount <= 0) break;

                heightIncrement += singleArchiveDataTypeBox.Box.PixelLengths.Y;

                totalViewStorageCount++;
            }
            else
            {
                archiveDataTypeBoxRowOffSet--;
            }

            if (viewStorage[folderLocation].ViewFolderStorage)
            {
                foreach (string fileName in viewStorage[folderLocation].FolderStorage)
                {
                    if (archiveDataTypeBoxRowOffSet <= 0)
                    {
                        viewText = fileName.Split(pathSyntax).Last();
                        viewText = (viewText.Length > charSpace) ? viewText.Substring(0, (int)charSpace) : viewText;

                        newArchiveDataBoxes.Add(
                            new ArchiveDataTypeBox(
                                singleArchiveDataTypeBox.BoxColor,
                                singleArchiveDataTypeBox.TextureColor,
                                singleArchiveDataTypeBox.TextColor,
                                new RaylibTexture(archiveTextures[(int)ArchiveTexture2DNames.File],
                                    new Rectangle(0, 0, archiveTextures[(int)ArchiveTexture2DNames.File].Width, archiveTextures[(int)ArchiveTexture2DNames.File].Height),
                                    new Rectangle(
                                        singleArchiveDataTypeBox.Box.PixelPosition.X,
                                        singleArchiveDataTypeBox.Box.PixelPosition.Y + heightIncrement,
                                        textureSize.X,
                                        textureSize.Y)
                                ),
                                viewText,
                                folderLocation,
                                ArchiveStorageNames.File,
                                new RaylibText(fileName.Split(pathSyntax).Last(), text.TextSize, text.TextSpacing),
                                new Box(
                                    new Vector2(
                                        singleArchiveDataTypeBox.Box.PixelPosition.X,
                                        singleArchiveDataTypeBox.Box.PixelPosition.Y + heightIncrement
                                    ),
                                    singleArchiveDataTypeBox.Box.PixelLengths
                                )
                            )
                        );
                        dataViewBoxesCount--;
                        if (dataViewBoxesCount <= 0) break;

                        heightIncrement += singleArchiveDataTypeBox.Box.PixelLengths.Y;

                        totalViewStorageCount++;
                    }
                    else
                    {
                        archiveDataTypeBoxRowOffSet--;
                    }
                }
            }
        }

        return (newArchiveDataBoxes.ToArray(), totalViewStorageCount);
    }

    private ArchiveDataTypeBox[] UpdateNewArchiveDataTypeBoxColors(ArchiveDataTypeBox[] currentArchiveDataTypeBoxes, ArchiveStorageNames currentEditDataType, string currentStorageDataLocation, string currentEditDirectory, string loadedFileDirectory, string pathSyntax, Color loadedFileColor, Color editDataTypeColor, Color currentStorageLocationColor)
    {
        foreach (ArchiveDataTypeBox dataTypeBox in currentArchiveDataTypeBoxes)
        {
            if (dataTypeBox.DataTypeName == ArchiveStorageNames.File)
            {
                if (dataTypeBox.Location + pathSyntax + dataTypeBox.Text.Text == loadedFileDirectory) dataTypeBox.BoxColor = loadedFileColor;

                if (dataTypeBox.Location + pathSyntax + dataTypeBox.Text.Text == currentEditDirectory && currentEditDataType == ArchiveStorageNames.File) dataTypeBox.BoxColor = editDataTypeColor;
            }
            else
            {
                if(dataTypeBox.Location == currentStorageDataLocation) dataTypeBox.BoxColor = currentStorageLocationColor;

                if(dataTypeBox.Location == currentEditDirectory && currentEditDataType != ArchiveStorageNames.File) dataTypeBox.BoxColor = editDataTypeColor;
            }
        }
        
        return currentArchiveDataTypeBoxes;
    }
}