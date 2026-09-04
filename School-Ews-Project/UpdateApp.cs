
namespace Program;

public class UpdateApp
{
    private readonly AppState _state;
    private readonly CalculateMainBoxes _calculateMainBoxes;
    private readonly UpdateEditorLayout _editorLayout;
    private readonly MazeGenerator _mazeGenerator;
    private readonly UpdateMazeLayout _mazeLayout;
    private readonly UpdateArchiveLayout _archiveLayout;
    private readonly UpdateArchiveCore _archiveCore;
    private readonly UpdateActiveMainBox _activeMainBox;


    public UpdateApp(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _calculateMainBoxes = new CalculateMainBoxes(_state);
        _editorLayout = new UpdateEditorLayout(_state);
        _mazeGenerator = new MazeGenerator(_state);
        _mazeLayout = new UpdateMazeLayout(_state);
        _archiveLayout = new UpdateArchiveLayout(_state);
        _archiveCore = new UpdateArchiveCore(_state);
        _activeMainBox = new UpdateActiveMainBox(_state);
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

        _archiveCore.ArchiveCore();
        
        _archiveLayout.UpdateArchive();
    }
}