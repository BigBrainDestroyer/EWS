
namespace Program;

public class UpdateApp
{
    private readonly AppState _state;
    private readonly CalculateMainBoxes _calculateMainBoxes;
    private readonly UpdateEditorLayout _editorLayout;
    private readonly MazeGenerator _mazeGenerator;
    private readonly UpdateMazeLayout _mazeLayout;
    private readonly UpdateArchiveLayout _archiveLayout;
    private readonly UpdateArchiveClicks _archiveClicks;
    private readonly UpdateActiveMainBox _activeMainBox;
    private readonly UpdateNamingDataTypeTextBox _namingDataTypeBox;

    public UpdateApp(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _calculateMainBoxes = new CalculateMainBoxes(_state);
        _editorLayout = new UpdateEditorLayout(_state);
        _mazeGenerator = new MazeGenerator(_state);
        _mazeLayout = new UpdateMazeLayout(_state);
        _archiveLayout = new UpdateArchiveLayout(_state);
        _archiveClicks = new UpdateArchiveClicks(_state);
        _activeMainBox = new UpdateActiveMainBox(_state);
        _namingDataTypeBox = new UpdateNamingDataTypeTextBox(_state);
    }
    
    public void App()
    {
        _activeMainBox.ActiveMainBox();

        _calculateMainBoxes.UpdateMainBoxes();

        _editorLayout.EditorLayout();

        if(_state.maze.generator.AllMazes.Length < 1)
        {
            _mazeGenerator.Generator();
        }

        _mazeLayout.MazeLayout();

        _archiveClicks.ArchiveClicks();

        _namingDataTypeBox.NamingDataTypeTextBox();

        _archiveLayout.UpdateArchive();
    }
}