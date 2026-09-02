using System.Numerics;
using System.Text;
using Raylib_cs;

namespace Program;
    
public class UpdateArchiveCore
{
    private readonly AppState _state;

    public UpdateArchiveCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ArchiveCore()
    {
        AddOnButtonClick();
    }

    private void AddOnButtonClick()
    {
        var _layout = _state.archive.layout;
        var _core = _state.archive.core;

        if(CheckField.MouseOnField(_layout.AddOnButtonPixelPosition, _layout.AddOnButtonPixelLengths, Raylib.GetMousePosition()) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            _core.AddLegalDataTypeToStorage = GetLegalDataTypes(_core.CurentStorageDataLocation);
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
        }

        return newAddLegalDataTypeToStorage;
    }
}