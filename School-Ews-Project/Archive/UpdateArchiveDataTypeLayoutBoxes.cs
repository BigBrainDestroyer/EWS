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
        var _font = _state.common.font;
        
        _archive.DataTypeBoxes = UpdateArchiveDataTypeBox(
            _font.ArchiveDataBoxType,
            _archive.Storage,
            _archive.ArchiveTextures,
            _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Archive],
            _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Archive],
            _archive.DataTypeBoxPercentage,
            _archive.PathSyntax,
            _archive.ArchiveWhiteSpaceToFunctionBar,
            _archive.ArchiveDataTypeBoxTextSpacing,
            _archive.DataTypeBoxTextureTextPercentageOffSet,
            _archive.ArchiveDataTypeBoxRowOffSet,
            _archive.BackgroundColor,
            _archive.DataTypeBoxesTextureColor,
            _archive.DataTypeBoxesTextColor
        );
    }

    private ArchiveDataTypeBox[] UpdateArchiveDataTypeBox(Font textFont, Dictionary<string, ArchiveStorage> storage, Texture2D[] archiveTextures, Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, Vector2 percentages, string pathSyntax, float whiteSpaceToFunctionBar, float textSpacing, float textureTextPercentageOffSet, int archiveDataTypeBoxRowOffSet, Color boxColor, Color textureColor, Color textColor)
    {
        ArchiveDataTypeBox newSingleDataTypeBox =
            new ArchiveDataTypeBox(
                boxColor,
                textureColor,
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

        ArchiveDataTypeBox[] newViewArchiveDataTypeBoxes =
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

        return newViewArchiveDataTypeBoxes;
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
        //Console.WriteLine($"newSingleDataTypeBox.PixelLengths.X {newSingleDataTypeBox.PixelLengths.X}");
        //Console.WriteLine($"newTextureSize.X {newTextureSize.X}");
        //Console.WriteLine($"textureTextPercentageOffSet {textureTextPercentageOffSet}");
        //Console.WriteLine($"Raylib.MeasureTextEx(textFont, t, text.TextSize, text.TextSpacing).X {Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X}");

        //Als de textFont niet geladen is kan is Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X gelijk aan 0 dus int is maximaal
        
        return ((newSingleDataTypeBox.PixelLengths.X - newTextureSize.X - (textureTextPercentageOffSet / 100f * newSingleDataTypeBox.PixelLengths.X)) / Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X <= 0 ) ? 0 :
            (int)((newSingleDataTypeBox.PixelLengths.X - newTextureSize.X - ((textureTextPercentageOffSet/100f)*newSingleDataTypeBox.PixelLengths.X)) / Raylib.MeasureTextEx(textFont, "t", text.TextSize, text.TextSpacing).X);
    }

    private RaylibText CalculateText(float boxHight, float textSpacing)
    {
        return new RaylibText("", boxHight, textSpacing);
    }

    private int CalculateNewDataViewBoxesCount(float parentBoxPixelLengthY, float newSingleDataTypeBoxLengthY, float whiteSpaceToFunctionBar)
    {
        return (int)((parentBoxPixelLengthY - whiteSpaceToFunctionBar) / newSingleDataTypeBoxLengthY);
    }

    private ArchiveDataTypeBox[] CalculateViewArchiveDataTypeBoxes(RaylibText text, Font textFont, Dictionary<string, ArchiveStorage> storage, ArchiveDataTypeBox singleArchiveDataTypeBox, Texture2D[] archiveTextures, Vector2 textureSize, string pathSyntax, int charSpace, int archiveDataTypeBoxRowOffSet, int dataViewBoxesCount)
    {
        List<ArchiveDataTypeBox> newArchiveDataBoxes = new List<ArchiveDataTypeBox>();

        Dictionary<string, ArchiveStorage> viewStorage = new Dictionary<string, ArchiveStorage>();

        string[] storageKeys = storage.Keys.ToArray();
        string[] folderPath;
        string parentFolder;
        int lastPathNameLength;

        storageKeys.Sort();

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
                lastPathNameLength = folderPath.Last().Length + 1;

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

        storageKeys.Sort();

        //foreach(string storagekey in storageKeys)
        //{
        //    Console.WriteLine(storagekey);
        //}
        //storageKeyNames worden goed geinitialiseerd
        
        string viewText = "";

        int counter = 0;

        
        foreach (string folderName in storageKeys)
        {
            if (counter >= archiveDataTypeBoxRowOffSet)
            {
                break;
            }

            //Console.WriteLine($"counter: {counter}"); // Verwijderd volgens mij goed 

            viewStorage.Remove(folderName);
        }

        storageKeys = viewStorage.Keys.ToArray();
        
        storageKeys.Sort();

        foreach (string folderName in storageKeys)
        {
            if (dataViewBoxesCount <= 0) break;
            
            int ArchiveStorageTexturePosition = (viewStorage[folderName].DataType == ArchiveStorageNames.ProjectFolder) ? (int)ArchiveStorageNames.ProjectFolder : (int)ArchiveStorageNames.Folder;
            ArchiveStorageTexturePosition += (viewStorage[folderName].ViewFolderStorage) ? 3 : 0;

            viewText = folderName.Split(pathSyntax).Last();

            viewText = (viewText.Length > charSpace) ? viewText.Substring(0, (int)charSpace) : viewText;

            //Console.WriteLine(folderName);
            //Console.WriteLine(viewText);

            newArchiveDataBoxes.Add(
                new ArchiveDataTypeBox(
                    singleArchiveDataTypeBox.BoxColor,
                    singleArchiveDataTypeBox.TextureColor,
                    singleArchiveDataTypeBox.TextColor,
                    new RaylibTexture(archiveTextures[ArchiveStorageTexturePosition],
                        new Rectangle(0, 0, archiveTextures[ArchiveStorageTexturePosition].Width, archiveTextures[ArchiveStorageTexturePosition].Height),
                        new Rectangle(
                            singleArchiveDataTypeBox.Box.PixelPosition.X,
                            singleArchiveDataTypeBox.Box.PixelPosition.Y,
                            textureSize.X,
                            textureSize.Y)
                    ),
                    viewText,
                    new RaylibText(folderName.Split(pathSyntax).Last(), text.TextSize, text.TextSpacing),
                    singleArchiveDataTypeBox.Box
                )
            );

            dataViewBoxesCount--;

            if (dataViewBoxesCount <= 0) break;

            foreach (string fileName in viewStorage[folderName].FolderStorage)
            {
                //Console.WriteLine(folderName);
                //Console.WriteLine(fileName.Split(pathSyntax).Last());

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
                                singleArchiveDataTypeBox.Box.PixelPosition.Y,
                                textureSize.X,
                                textureSize.Y)
                        ),
                        viewText,
                        new RaylibText(fileName.Split(pathSyntax).Last(), text.TextSize, text.TextSpacing),
                        singleArchiveDataTypeBox.Box
                    )
                );

                dataViewBoxesCount--;

                if (dataViewBoxesCount <= 0) break;
            }
        }

        return newArchiveDataBoxes.ToArray();
    }
}