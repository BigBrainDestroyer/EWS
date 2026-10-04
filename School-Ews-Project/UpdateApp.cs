
namespace Program;

public class UpdateApp
{
    private readonly AppState _state;
    private readonly UpdateActiveMainBox _activeMainBox;
    private readonly CalculateMainBoxes _calculateMainBoxes;
    private readonly UpdateEditorLayout _editorLayout;
    private readonly MazeGenerator _mazeGenerator;
    private readonly UpdateMazeLayout _mazeLayout;
    private readonly UpdateArchive _archive;
    private readonly AlgoConnectorController _connectorController;

    public UpdateApp(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _activeMainBox = new UpdateActiveMainBox(_state);
        _calculateMainBoxes = new CalculateMainBoxes(_state);
        _editorLayout = new UpdateEditorLayout(_state);
        _mazeGenerator = new MazeGenerator(_state);
        _mazeLayout = new UpdateMazeLayout(_state);
        _archive = new UpdateArchive(_state);
        _connectorController = new AlgoConnectorController(_state);
    }
    
    public void App()
    {
        _activeMainBox.ActiveMainBox();

        _calculateMainBoxes.UpdateMainBoxes();

        _archive.Archive();

        _editorLayout.EditorLayout();

        if(_state.maze.generator.AllMazes.Length < 1)
        {
            _mazeGenerator.Generator();
        }

        _connectorController.Controller();

        _mazeLayout.MazeLayout();
    }
}