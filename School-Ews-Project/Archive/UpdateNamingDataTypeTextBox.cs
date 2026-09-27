using Raylib_cs;

namespace Program;

public class UpdateNamingDataTypeTextBox
{
    private readonly AppState _state;
    private readonly UpdateCustomTextBar _customTextBar;
    private readonly UpdateArchiveStorage _archiveStorage;

    public UpdateNamingDataTypeTextBox(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _customTextBar = new UpdateCustomTextBar(_state);
        _archiveStorage = new UpdateArchiveStorage(_state);
    }

    public void NamingDataTypeTextBox()
    {
        var _archive = _state.archive;

        if(_archive.ArchiveNamingDataTypeTextBox.IsTextBoxActive)
        {
            UpdateNamingDataTypeTextBoxLayout();
            CheckForCriticalInput();
        }
    }

    private void UpdateNamingDataTypeTextBoxLayout()
    {
        var _archive = _state.archive;
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

        if(Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            (_archive.Storage, _archive.CurrentStorageDataLocation) =
                _archiveStorage.UpdateAddDataType(
                    _archive.Storage,
                    _archive.TemporarilyData,
                    _archive.CurrentStorageDataLocation,
                    _archive.ProjectsDataLocation,
                    _archive.ArchiveNamingDataTypeTextBox.Text,
                    _archive.PathSyntax
                );

            newArchiveNamingDataTypeTextBox.IsTextBoxActive = false;
        }
        else if(newArchiveNamingDataTypeTextBox.IsTextBoxActive == true)
        {
            if ((Raylib.IsKeyPressed(KeyboardKey.Escape) || Raylib.IsMouseButtonPressed(MouseButton.Left) && !CheckField.MouseOnField(newArchiveNamingDataTypeTextBox.BoxPosition, newArchiveNamingDataTypeTextBox.BoxLengths, Raylib.GetMousePosition()) && !CheckField.MouseOnField(_archive.AddDataTypeMenu.BoxPosition, _archive.AddDataTypeMenu.BoxLengths, Raylib.GetMousePosition())|| Raylib.IsMouseButtonPressed(MouseButton.Right)))
            {
                newArchiveNamingDataTypeTextBox.IsTextBoxActive = false;
            }
        }

        _archive.ArchiveNamingDataTypeTextBox = newArchiveNamingDataTypeTextBox;
    }
}
