
namespace Program;

public class UpdateApp
{
    private readonly AppState _state;
    private readonly UpdateActiveMainBox _activeMainBox;
    private readonly CalculateMainBoxes _calculateMainBoxes;
    private readonly UpdateEditorLayout _editorLayout;
    private readonly UpdateArchive _archive;
    private readonly AlgoConnectorController _connectorController;
    private readonly MazeController _mazeController;
    private readonly HeaderController _headerController;

    public UpdateApp(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _activeMainBox = new UpdateActiveMainBox(_state);
        _calculateMainBoxes = new CalculateMainBoxes(_state);
        _editorLayout = new UpdateEditorLayout(_state);
        _mazeController = new MazeController(_state);
        _archive = new UpdateArchive(_state);
        _connectorController = new AlgoConnectorController(_state);
        _headerController = new HeaderController(_state);
    }
    
    public void App()
    {
        _activeMainBox.ActiveMainBox();
        
        _calculateMainBoxes.UpdateMainBoxes();

        _headerController.Controller();

        _archive.Archive();

        _editorLayout.EditorLayout();

        _connectorController.Controller();

        _mazeController.Controller();
    }
}