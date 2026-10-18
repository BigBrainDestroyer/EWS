
namespace Program;

public class BuildButtonCore
{
    private readonly AppState _state;
    private readonly OutsideTerminal _terminal;
    
    public BuildButtonCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _terminal = new OutsideTerminal(_state);
    }

    public void ButtonCLick()
    {
        var _archive = _state.archive;
        var _header = _state.HeaderState;

        if (_archive.ArchiveProjectFolderPaths.Length > 0)
        {
            if (_header.AllProjectFolderNamesListPosition < _archive.ArchiveProjectFolderPaths.Length && _header.AllProjectFolderNamesListPosition >= 0 && _archive.ArchiveProjectFolderPaths.Length != 0 && _archive.ArchiveProjectFolderPaths[_header.AllProjectFolderNamesListPosition] != null)
            {
                _terminal.BuildAlgoProject(ShareData.App.directoryInfo.OsSystem, _archive.ArchiveProjectFolderPaths[_header.AllProjectFolderNamesListPosition]);
            }
        }
    }
}