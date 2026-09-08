using Raylib_cs;

namespace Program;

public class UpdateNamingDataTypeTextBox
{
    private readonly AppState _state;
    private readonly UpdateCustomTextBar _customTextBar;


    public UpdateNamingDataTypeTextBox(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _customTextBar = new UpdateCustomTextBar(_state);
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
            newArchiveNamingDataTypeTextBox.IsTextBoxActive = false;
            _archive.ArchiveNamingDataTypeTextBox = newArchiveNamingDataTypeTextBox;
            
            if(_archive.ArchiveNamingDataTypeTextBox.Text.Length == 0)
            {
                return;
            }
        }
    }
}
