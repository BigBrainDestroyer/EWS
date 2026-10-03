using Raylib_cs;

namespace Program;

public class UpdateNamingDataTypeTextBox
{
    private readonly AppState _state;
    private readonly UpdateCustomTextBar _customTextBar;
    private readonly UpdateArchiveStorage _archiveStorage;
    private readonly ArchiveCalculateEditDataType _editDataType;

    public UpdateNamingDataTypeTextBox(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _customTextBar = new UpdateCustomTextBar(_state);
        _archiveStorage = new UpdateArchiveStorage(_state);
        _editDataType = new ArchiveCalculateEditDataType(_state);
    }

    public void NamingDataTypeTextBox()
    {
        var _archive = _state.archive;

        if(_archive.ArchiveNamingDataTypeTextBox.IsBoxActive)
        {
            UpdateNamingDataTypeTextBoxLayout();
            CheckForCriticalInput();
        }
    }

    private void UpdateNamingDataTypeTextBoxLayout()
    {
        var _archive = _state.archive;
        var _mainBoxes = _state.common.boxes;
        var _input = _state.common.input;

        _archive.ArchiveNamingDataTypeTextBox =
            _customTextBar.UpdateTextBar(
                _archive.ArchiveNamingDataTypeTextBox,
                _input.KeyboardCharInput,
                _archive.ArchiveBoxPixelPositions[ArchiveBoxesNames.Edit],
                _archive.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit]
            );
    }

    private void CheckForCriticalInput()
    {
        var _archive = _state.archive;

        TextBar newArchiveNamingDataTypeTextBox = _archive.ArchiveNamingDataTypeTextBox;

        if (Raylib.IsKeyPressed(KeyboardKey.Enter) && newArchiveNamingDataTypeTextBox.IsBoxActive == true)
        {
            if(!_archive.ArchiveEditDataType.RenamingActive)
            {
                (_archive.Storage, _archive.CurrentStorageDataLocation) =
                    _archiveStorage.UpdateAddDataType(
                        _archive.Storage,
                        _archive.OsSystem,
                        _archive.TemporarilyData,
                        _archive.CurrentStorageDataLocation,
                        _archive.ProjectsDataLocation,
                        _archive.ArchiveNamingDataTypeTextBox.Text.Text,
                        _archive.PathSyntax
                    );
            }
            else
            {
                _archive.Storage = 
                    _editDataType.RenameDataTypeAction(
                        _archive.Storage,
                        _archive.ArchiveEditDataType.DataType,
                        _archive.ArchiveEditDataType.DataTypePath,
                        _archive.ArchiveNamingDataTypeTextBox.Text.Text,
                        _archive.PathSyntax,
                        _archive.ProjectsDataLocation
                    );

                _archive.ArchiveEditDataType.DataTypePath = "";
                    
                _archive.ArchiveEditDataType.RenamingActive = false;
            }

            newArchiveNamingDataTypeTextBox.IsBoxActive = false;
        }
        else if(newArchiveNamingDataTypeTextBox.IsBoxActive == true)
        {
            if ((Raylib.IsKeyPressed(KeyboardKey.Escape) || Raylib.IsMouseButtonPressed(MouseButton.Left) && !CheckField.MouseOnField(newArchiveNamingDataTypeTextBox.Box.PixelPosition, newArchiveNamingDataTypeTextBox.Box.PixelLengths, Raylib.GetMousePosition()) && !CheckField.MouseOnField(_archive.AddDataTypeMenu.BoxPosition, _archive.AddDataTypeMenu.BoxLengths, Raylib.GetMousePosition())|| Raylib.IsMouseButtonPressed(MouseButton.Right)))
            {
                newArchiveNamingDataTypeTextBox.IsBoxActive = false;
            }
        }

        _archive.ArchiveNamingDataTypeTextBox = newArchiveNamingDataTypeTextBox;
    }
}
