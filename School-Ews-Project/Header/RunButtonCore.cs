using Raylib_cs;
using ShareData;

namespace Program;

public class RunButtonCore
{
    private readonly AppState _state;
    private readonly OutsideTerminal _terminal;

    public RunButtonCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _terminal = new OutsideTerminal(_state);
    }

    public void UpdateButtonAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        if (_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.RunButton] = true;
        }
        else if(_play.IsAlgoRunning)
        {
            _header.HeaderButtons[DrawHeaderObjectNames.RunButton].BoxBackgroundColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.RunButton].BoxColors.Y;

            _header.HeaderButtons[DrawHeaderObjectNames.RunButton].TextColor = 
                _header.HeaderButtonColors[DrawHeaderObjectNames.RunButton].SymbolColors.Y;

            _header.HeaderObjectAvailable[DrawHeaderObjectNames.RunButton] = false;
        }
        else
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.RunButton] = false;
        }
    }

    public void ButtonClick()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _archive = _state.archive;
        var _header = _state.HeaderState;

        if(_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone && _archive.ArchiveProjectFolderPaths.Length > 0)
        {
            if(_header.AllProjectFolderNamesListPosition < _archive.ArchiveProjectFolderPaths.Length && _header.AllProjectFolderNamesListPosition >= 0 && _archive.ArchiveProjectFolderPaths.Length != 0 && _archive.ArchiveProjectFolderPaths[_header.AllProjectFolderNamesListPosition] != null)
            {
                _terminal.RunAlgoProject(ShareData.App.directoryInfo.OsSystem, _archive.ArchiveProjectFolderPaths[_header.AllProjectFolderNamesListPosition]);
            }
        }
    }
}