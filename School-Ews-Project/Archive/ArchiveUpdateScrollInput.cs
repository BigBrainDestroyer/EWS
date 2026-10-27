
namespace Program;

public class ArchiveUpdateScrollInput
{
    private readonly AppState _state;

    public ArchiveUpdateScrollInput(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ScrollInput()
    {
        var _input = _state.common.input;
        var _mainBoxes = _state.common.boxes;
        var _archive = _state.archive;

        _archive.ArchiveDataTypeBoxRowOffSet = UpdateScrollInput(_mainBoxes.ActiveMainBox, _archive.ArchiveDataTypeBoxRowOffSet, (int)_input.MouseWheelInput, _archive.TotalViewStorageCount);
    }

    private int UpdateScrollInput(MainBoxNames activeMainBox, int currentDataTypeBoxRowOffSet, int mouseWheelInput, int viewStorageCount)
    {
        if(activeMainBox == MainBoxNames.Archive) currentDataTypeBoxRowOffSet -= mouseWheelInput;
        
        if(currentDataTypeBoxRowOffSet < 0)
        {
            currentDataTypeBoxRowOffSet = 0;
        }
        
        if(currentDataTypeBoxRowOffSet >= viewStorageCount)
        {
            currentDataTypeBoxRowOffSet = (viewStorageCount > 0)? viewStorageCount - 1 : viewStorageCount;
        }
        
        return currentDataTypeBoxRowOffSet;
    }
}