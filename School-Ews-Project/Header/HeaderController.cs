
namespace Program;

public class HeaderController
{
    private readonly AppState _state;
    private readonly UpdateHeaderLayout _headerLayout;
    private readonly HeaderClick _click;
    private readonly UpdateRunFolder _runFolder;
    private readonly RunButtonCore _runButton;
    private readonly BuildMazeButtonCore _buildMaze;
    private readonly BuildButtonCore _buildButton;
    private readonly TerminalButtonCore _terminalButton;

    public HeaderController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _headerLayout = new UpdateHeaderLayout(_state);
        _click = new HeaderClick(_state);
        _runFolder = new UpdateRunFolder(_state);
        _runButton = new RunButtonCore(_state);
        _buildMaze = new BuildMazeButtonCore(_state);
        _buildButton = new BuildButtonCore(_state);
        _terminalButton = new TerminalButtonCore(_state);
    }

    public void Controller()
    {
        _runFolder.Update();
        
        _headerLayout.Update();

        _click.Click();

        _runButton.UpdateButtonAvailability();

        _buildMaze.UpdateButtonAvailability();

        _buildButton.UpdateButtonAvailability();

        _terminalButton.UpdateButtonAvailability();
    }
}