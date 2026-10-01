using Raylib_cs;
using System.Numerics;

namespace Program;
    
public class UpdateArchiveClicks
{
    private readonly AppState _state;
    private readonly UpdateArchiveDataTypeMenu _dataTypeMenu;
    private readonly UpdateCustomTextBar _customTextBar;
    private readonly UpdateArchiveStorage _archiveStorage;

    public UpdateArchiveClicks(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _dataTypeMenu = new UpdateArchiveDataTypeMenu(_state);
        _customTextBar = new UpdateCustomTextBar(_state);
        _archiveStorage = new UpdateArchiveStorage(_state);
    }

    public void ArchiveClicks()
    {
        var _archive = _state.archive;

        if(_archive.AddDataTypeMenu.IsMenuActive)
        {
            AddDataMenuClick();
        }
        
        AddOnButtonClick();
    }

    private void AddDataMenuClick()
    {
        var _archive = _state.archive;
        var _mainBoxes = _state.common.boxes;
        var _input = _state.common.input;

        if(!CheckField.MouseOnField(_archive.AddDataTypeMenu.BoxPosition, _archive.AddDataTypeMenu.BoxLengths, Raylib.GetMousePosition()) || !Raylib.IsMouseButtonPressed(MouseButton.Left)) return;

        ArchiveAddDataTypeMenu addDataTypeMenu = _archive.AddDataTypeMenu;

        foreach(ArchiveStorageNames? dataType in  _archive.AddDataTypeMenu.AddLegalDataTypeToStorage)
        {
            if(dataType != null)
            {
                if (CheckField.MouseOnField(addDataTypeMenu.BoxPosition, addDataTypeMenu.SingleBoxLengths, Raylib.GetMousePosition()))
                {
                    _archive.AddDataTypeMenu = _dataTypeMenu.UpdateArchiveTypeMenuActiveState(_archive.AddDataTypeMenu);

                    _archive.ArchiveNamingDataTypeTextBox = 
                        GetNewNamingDataTypeBox(
                            _archive.ArchiveTextures, 
                            dataType.Value, 
                            _archive.ArchiveNamingDataTypeTextBoxCornerStates, 
                            _mainBoxes.MainBoxesCornerRadius[MainBoxNames.Archive],
                            _archive.NamingDataTypeBackgroundColor,
                            _archive.NamingDataTypeTextureColor,
                            _archive.NamingDataTypeCursorColor,
                            _archive.NamingDataTypeTextColor
                        );

                    _archive.TemporarilyData = dataType.Value;
                }
                
                addDataTypeMenu.BoxPosition.Y += addDataTypeMenu.SingleBoxLengths.Y;
            }
        }
    }
    
    private void AddOnButtonClick()
    {
        var _archive = _state.archive;

        if(CheckField.MouseOnField(_archive.AddOnButtonPixelPosition, _archive.AddOnButtonPixelLengths, Raylib.GetMousePosition()) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            _archive.AddLegalDataTypeToStorage = GetLegalDataTypes(_archive.CurrentStorageDataLocation, _archive.ProjectsDataLocation, _archive.PathSyntax);

            _archive.AddDataTypeMenu = _dataTypeMenu.UpdateArchiveTypeMenuActiveState(_archive.AddDataTypeMenu);
        }
    }

    private ArchiveStorageNames?[] GetLegalDataTypes(string dataLocation, string projectDataLocation, string pathSyntax)
    {
        ArchiveStorageNames?[] newAddLegalDataTypeToStorage = new ArchiveStorageNames?[3];

        if(dataLocation != projectDataLocation)
        {
            newAddLegalDataTypeToStorage[(int)ArchiveStorageNames.Folder] = ArchiveStorageNames.Folder;
            newAddLegalDataTypeToStorage[(int)ArchiveStorageNames.File] = ArchiveStorageNames.File;
        }
        else
        {
            newAddLegalDataTypeToStorage[(int)ArchiveStorageNames.ProjectFolder] = ArchiveStorageNames.ProjectFolder;
        }

        return newAddLegalDataTypeToStorage;
    }

    public (Dictionary<string, ArchiveStorage>, ArchiveDataTypeBox[], ArchiveEditDataType, string, string) DataTypeBoxClick(Dictionary<string, ArchiveStorage> currentStorage, ArchiveDataTypeBox[] archiveDataTypeBoxes, ArchiveEditDataType currentArchiveEditDataType, TextBar archiveNamingDataTypeTextBox, ArchiveAddDataTypeMenu addDataTypeMenu, Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, string projectsDataLocation, string currentStorageDataLocation, string loadedFileDirectory, string pathSyntax, Color mouseOnBoxColor)
    {
        if(!CheckField.MouseOnField(parentBoxPixelPosition, parentBoxPixelLengths, Raylib.GetMousePosition()) || CheckField.MouseOnField(addDataTypeMenu.BoxPosition, addDataTypeMenu.BoxLengths, Raylib.GetMousePosition()) && addDataTypeMenu.IsMenuActive || archiveNamingDataTypeTextBox.IsBoxActive) return (currentStorage, archiveDataTypeBoxes, currentArchiveEditDataType, currentStorageDataLocation, loadedFileDirectory);

        bool mouseIsOnDataBox = false;

        foreach(ArchiveDataTypeBox dataTypeBox in archiveDataTypeBoxes)
        {
            if(CheckField.MouseOnField(dataTypeBox.Box.PixelPosition, dataTypeBox.Box.PixelLengths, Raylib.GetMousePosition()))
            {
                if(Raylib.IsMouseButtonPressed(MouseButton.Left) && dataTypeBox.DataTypeName != ArchiveStorageNames.File)
                {
                    currentStorageDataLocation = dataTypeBox.Location;

                    currentStorage[dataTypeBox.Location] = 
                    new ArchiveStorage(
                        currentStorage[dataTypeBox.Location].FolderStorage,
                        currentStorage[dataTypeBox.Location].DataType,
                        !currentStorage[dataTypeBox.Location].ViewFolderStorage
                    );
                }
                else if(Raylib.IsMouseButtonPressed(MouseButton.Right) && dataTypeBox.DataTypeName != ArchiveStorageNames.File)
                {
                    currentArchiveEditDataType.DataTypePath = dataTypeBox.Location;
                    currentArchiveEditDataType.DataType = dataTypeBox.DataTypeName;
                    currentArchiveEditDataType.IsEditBoxActive = true;
                }
                else if(Raylib.IsMouseButtonPressed(MouseButton.Left) && dataTypeBox.DataTypeName == ArchiveStorageNames.File)
                {
                    currentStorageDataLocation = dataTypeBox.Location;
                    loadedFileDirectory = dataTypeBox.Location + pathSyntax + dataTypeBox.Text.Text;
                }
                else if((Raylib.IsMouseButtonPressed(MouseButton.Right) && dataTypeBox.DataTypeName == ArchiveStorageNames.File))
                {
                    currentArchiveEditDataType.DataTypePath = dataTypeBox.Location + pathSyntax + dataTypeBox.Text.Text;
                    currentArchiveEditDataType.DataType = dataTypeBox.DataTypeName;
                    currentArchiveEditDataType.IsEditBoxActive = true;
                }
                else
                {
                    dataTypeBox.BoxColor = mouseOnBoxColor;
                }

                mouseIsOnDataBox = true;
            }
        }

        if(!mouseIsOnDataBox && Raylib.IsMouseButtonPressed(MouseButton.Left) && !CheckField.MouseOnField(addDataTypeMenu.BoxPosition, addDataTypeMenu.BoxLengths, Raylib.GetMousePosition()) && !addDataTypeMenu.IsMenuActive)
        {
            currentStorageDataLocation = projectsDataLocation;
        }
        
        return (currentStorage, archiveDataTypeBoxes, currentArchiveEditDataType, currentStorageDataLocation, loadedFileDirectory);
    }
    
    //nog een goede plek vinden
    public TextBar GetNewNamingDataTypeBox(Texture2D[] archiveTextures, ArchiveStorageNames dataType, BoolVector4 cornerStates, float cornerRadius, Color backgroundColor, Color textureColor, Color cursorColor, Color textColor)
    {
        TextBar newTextBar = new TextBar();

        newTextBar.Texture2D = new RaylibTexture(archiveTextures[(int)dataType], new Rectangle(), new Rectangle());

        newTextBar.IsBoxActive = true;

        newTextBar.Box = new CustomBox(cornerStates, new Vector2(0,0), new Vector2(0,0), cornerRadius);

        newTextBar.BackgroundColor = backgroundColor;
        newTextBar.TextureColor = textureColor;
        newTextBar.CursorColor = cursorColor;
        newTextBar.TextColor = textColor;

        return newTextBar;
    }

    public ArchiveEditDataType EditDataTypeActionClick(ArchiveEditDataType archiveEditDataType, Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, Color highLightBoxColor, Color highLightDeleteColor, Color highLightRenamingColor)
    {
        //Canceld
        if(!CheckField.MouseOnField(parentBoxPixelPosition, parentBoxPixelLengths, Raylib.GetMousePosition()) && Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsKeyPressed(KeyboardKey.Escape) || archiveEditDataType.DataTypePath.Length == 0)
        {
            archiveEditDataType.DataTypePath = "";
            archiveEditDataType.IsEditBoxActive = false;
            archiveEditDataType.RenamingActive = false;
            archiveEditDataType.DeleteActive = false;

            return archiveEditDataType;
        }

        if(!CheckField.MouseOnField(parentBoxPixelPosition, parentBoxPixelLengths, Raylib.GetMousePosition()) || !archiveEditDataType.IsEditBoxActive) return archiveEditDataType;

        if(CheckField.MouseOnField(archiveEditDataType.DeleteClickField.PixelPosition, archiveEditDataType.DeleteClickField.PixelLengths, Raylib.GetMousePosition()))
        {
            if(Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                archiveEditDataType.DeleteActive = true;
                archiveEditDataType.IsEditBoxActive = false;

                return archiveEditDataType;
            }
            else
            {
                archiveEditDataType.DeleteTextColor = highLightDeleteColor;
                archiveEditDataType.DeleteBoxColor = highLightBoxColor;
                
                return archiveEditDataType;
            }
        }
        else if(CheckField.MouseOnField(archiveEditDataType.RenameClickField.PixelPosition, archiveEditDataType.RenameClickField.PixelLengths, Raylib.GetMousePosition()))
        {
            if(Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                archiveEditDataType.RenamingActive = true;
                archiveEditDataType.IsEditBoxActive = false;

                //Uitzondering 
                var _archive = _state.archive;
                var _mainBoxes = _state.common.boxes;

                _archive.ArchiveNamingDataTypeTextBox = 
                    GetNewNamingDataTypeBox(
                        _archive.ArchiveTextures,
                        archiveEditDataType.DataType,//dataType.Value, 
                        _archive.ArchiveNamingDataTypeTextBoxCornerStates,
                        _mainBoxes.MainBoxesCornerRadius[MainBoxNames.Archive],
                        _archive.NamingDataTypeBackgroundColor,
                        _archive.RenamingDataTypeTextureColor,
                        _archive.NamingDataTypeCursorColor,
                        _archive.RenamingDataTypeTextColor
                    );
                
                return archiveEditDataType;
            }
            else
            {
                archiveEditDataType.RenameTextColor = highLightRenamingColor;
                archiveEditDataType.RenameBoxColor = highLightBoxColor;

                return archiveEditDataType;
            }
        }

        return archiveEditDataType;
    }

    
}