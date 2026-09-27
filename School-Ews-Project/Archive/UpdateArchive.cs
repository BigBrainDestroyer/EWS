
namespace Program;

public class UpdateArchive
{
    private readonly AppState _state;
    private readonly UpdateArchiveLayout _archiveLayout;
    private readonly UpdateArchiveClicks _archiveClicks;
    private readonly UpdateNamingDataTypeTextBox _namingDataTypeBox;
    private readonly UpdateArchiveStorage _archiveStorage;
    private readonly UpdateArchiveDataTypeLayoutBoxes _dataTypeLayoutBoxes;
    private readonly UpdateLoadFile _loadFile;

    public UpdateArchive(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _archiveLayout = new UpdateArchiveLayout(_state);
        _archiveClicks = new UpdateArchiveClicks(_state);
        _namingDataTypeBox = new UpdateNamingDataTypeTextBox(_state);
        _archiveStorage = new UpdateArchiveStorage(_state);
        _dataTypeLayoutBoxes = new UpdateArchiveDataTypeLayoutBoxes(_state);
        _loadFile = new UpdateLoadFile(_state);
    }

    public void Archive()
    {
        _archiveClicks.ArchiveClicks();
        
        _namingDataTypeBox.NamingDataTypeTextBox();
        
        _archiveStorage.UpdateProjectsFolder();

        _archiveLayout.UpdateArchive();

        _loadFile.CheckLoadFileState();

        _archiveStorage.UpdateProjectsFolder();
    }
}