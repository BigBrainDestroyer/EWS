
namespace Program;

public class UpdateArchiveLayout
{
    private readonly AppState _state;
    private readonly UpdateFunctionBox _functionBox;
    private readonly UpdateArchiveLayoutBoxes _archiveBoxes;
    private readonly UpdateArchiveDataTypeLayoutBoxes _dataBoxes;
    private readonly UpdateArchiveDataTypeMenu _dataTypeMenu;

    public UpdateArchiveLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _functionBox = new UpdateFunctionBox(_state);
        _archiveBoxes = new UpdateArchiveLayoutBoxes(_state);
        _dataBoxes = new UpdateArchiveDataTypeLayoutBoxes(_state);
        _dataTypeMenu = new UpdateArchiveDataTypeMenu(_state);
    }

    public void UpdateArchive()
    {        
        _archiveBoxes.ArchiveLayoutBoxes();

        _functionBox.FunctionBox();

        _dataBoxes.ArchiveDataTypeLayoutBoxes();

        _dataTypeMenu.UpdateDataTypeMenu();
    }
}