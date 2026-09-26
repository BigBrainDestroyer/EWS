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
        var _input = _state.common.input;

        if(!CheckField.MouseOnField(_archive.AddDataTypeMenu.BoxPosition, _archive.AddDataTypeMenu.BoxLengths, Raylib.GetMousePosition()) || !Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            return;
        }

        ArchiveAddDataTypeMenu addDataTypeMenu = _archive.AddDataTypeMenu;

        foreach(ArchiveStorageNames? dataType in  _archive.AddDataTypeMenu.AddLegalDataTypeToStorage)
        {
            if(dataType != null)
            {
                if (CheckField.MouseOnField(addDataTypeMenu.BoxPosition, addDataTypeMenu.SingleBoxLengths, Raylib.GetMousePosition()))
                {
                    _archive.AddDataTypeMenu = _dataTypeMenu.UpdateArchiveTypeMenuActiveState(_archive.AddDataTypeMenu);

                    _archive.ArchiveNamingDataTypeTextBox = GetNewNamingDataTypeBox(_archive.ArchiveTextures, dataType.Value);

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

    public (Dictionary<string, ArchiveStorage>, ArchiveDataTypeBox[], string, string, string) DataTypeBoxClick(Dictionary<string, ArchiveStorage> currentStorage, ArchiveDataTypeBox[] archiveDataTypeBoxes,Vector2 parentBoxPixelPosition, Vector2 parentBoxPixelLengths, string currentStorageDataLocation, string currentEditDirectory, string loadedFileDirectory, Color mouseOnBoxColor)
    {
        if(!CheckField.MouseOnField(parentBoxPixelPosition, parentBoxPixelLengths, Raylib.GetMousePosition())) return (currentStorage, archiveDataTypeBoxes, currentStorageDataLocation, currentEditDirectory, loadedFileDirectory);

        foreach(ArchiveDataTypeBox dataTypeBox in archiveDataTypeBoxes)
        {
            if(CheckField.MouseOnField(dataTypeBox.Box.PixelPosition, dataTypeBox.Box.PixelLengths, Raylib.GetMousePosition()))
            {
                if(Raylib.IsMouseButtonPressed(MouseButton.Left) && dataTypeBox.DataTypeName != ArchiveStorageNames.File)
                {
                    //als file sla current geopende file op als laad nieuwe file zet selected file dataLocation
                    currentStorageDataLocation = dataTypeBox.Location;
                    //Console.WriteLine("test");

                    currentStorage[dataTypeBox.Location] = 
                    new ArchiveStorage(
                        currentStorage[dataTypeBox.Location].FolderStorage,
                        currentStorage[dataTypeBox.Location].DataType,
                        !currentStorage[dataTypeBox.Location].ViewFolderStorage
                    );
                }
                else if(Raylib.IsMouseButtonPressed(MouseButton.Right) && dataTypeBox.DataTypeName != ArchiveStorageNames.File)
                {
                    currentEditDirectory = dataTypeBox.Location;
                }
                else if(Raylib.IsMouseButtonPressed(MouseButton.Left) && dataTypeBox.DataTypeName == ArchiveStorageNames.File)
                {
                    loadedFileDirectory = dataTypeBox.Location + dataTypeBox.Text;
                }
                else if((Raylib.IsMouseButtonPressed(MouseButton.Right) && dataTypeBox.DataTypeName == ArchiveStorageNames.File))
                {
                    currentEditDirectory = dataTypeBox.Location + dataTypeBox.Text;
                }
                else
                {
                    dataTypeBox.BoxColor = mouseOnBoxColor;
                }
            }
        }
        
        return (currentStorage, archiveDataTypeBoxes, currentStorageDataLocation, currentEditDirectory, loadedFileDirectory);
    }
    
    //nog een goede plek
    public TextBar GetNewNamingDataTypeBox(Texture2D[] archiveTextures, ArchiveStorageNames dataType)
    {
        TextBar newTextBar = new TextBar();

        newTextBar.Texture = archiveTextures[(int)dataType];

        newTextBar.IsTextBoxActive = true;

        newTextBar.CornerStates = new BoolVector4(false, false, true, true);

        return newTextBar;
    }
}