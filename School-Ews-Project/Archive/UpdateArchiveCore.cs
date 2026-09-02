using System.Text;
using Raylib_cs;

namespace Program;
    
public class UpdateArchiveCore
{
    private readonly AppState _state;
    private readonly UpdateArchiveDataTypeMenu _dataTypeMenu;

    public UpdateArchiveCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _dataTypeMenu = new UpdateArchiveDataTypeMenu(_state);
    }

    public void ArchiveCore()
    {
        var _layout = _state.archive.layout;

        if(_layout.AddDataTypeMenu.IsMenuActive)
        {
            AddDataMenuClick();
        }
        
        AddOnButtonClick();
    }

    private void AddDataMenuClick()
    {
        var _layout = _state.archive.layout;

        if(!CheckField.MouseOnField(_layout.AddDataTypeMenu.BoxPosition, _layout.AddDataTypeMenu.BoxLengths, Raylib.GetMousePosition()) || !Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            return;
        }

        ArchiveAddDataTypeMenu addDataTypeMenu = _layout.AddDataTypeMenu;

        foreach(ArchiveStorageNames? dataType in  _layout.AddDataTypeMenu.AddLegalDataTypeToStorage)
        {
            if(dataType != null)
            {
                if(CheckField.MouseOnField(addDataTypeMenu.BoxPosition, addDataTypeMenu.SingleBoxLengths, Raylib.GetMousePosition()))
                {
                    Console.WriteLine(dataType);

                    _layout.AddDataTypeMenu = _dataTypeMenu.UpdateArchiveTypeMenuActiveState(_layout.AddDataTypeMenu);
                }

                addDataTypeMenu.BoxPosition.Y += addDataTypeMenu.SingleBoxLengths.Y;
            }
        }
    }
    
    private void AddOnButtonClick()
    {
        var _layout = _state.archive.layout;
        var _core = _state.archive.core;

        if(CheckField.MouseOnField(_layout.AddOnButtonPixelPosition, _layout.AddOnButtonPixelLengths, Raylib.GetMousePosition()) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            _core.AddLegalDataTypeToStorage = GetLegalDataTypes(_core.CurentStorageDataLocation);

            _layout.AddDataTypeMenu = _dataTypeMenu.UpdateArchiveTypeMenuActiveState(_layout.AddDataTypeMenu);
        }
    }
    
    private ArchiveStorageNames?[] GetLegalDataTypes(string dataLocation)
    {
        ArchiveStorageNames?[] newAddLegalDataTypeToStorage = new ArchiveStorageNames?[3];

        StringBuilder newString = new StringBuilder();

        string[] dataLocationList = newString.ToString().Split('/');
        
        newAddLegalDataTypeToStorage[(int)ArchiveStorageNames.ProjectFolder] = ArchiveStorageNames.ProjectFolder;
        
        if(dataLocation != dataLocationList[0])
        {
            newAddLegalDataTypeToStorage[(int)ArchiveStorageNames.Folder] = ArchiveStorageNames.Folder;
            newAddLegalDataTypeToStorage[(int)ArchiveStorageNames.File] = ArchiveStorageNames.File;

            Console.WriteLine(dataLocationList[0]);
            Console.WriteLine(dataLocation);
        }

        return newAddLegalDataTypeToStorage;
    }
}