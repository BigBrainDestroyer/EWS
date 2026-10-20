
namespace Program;

public class HeaderController
{
    private readonly AppState _state;
    private readonly UpdateHeaderLayout _headerLayout;
    private readonly HeaderClick _click;
    private readonly RunFolderStringSliderCore _runFolder;
    private readonly RunButtonCore _runButton;
    private readonly BuildMazeButtonCore _buildMaze;
    private readonly BuildButtonCore _buildButton;
    private readonly TerminalButtonCore _terminalButton;
    private readonly StartStopAlgoPlayBackButtonCore _playBackButton;

    public HeaderController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _headerLayout = new UpdateHeaderLayout(_state);
        _click = new HeaderClick(_state);
        _runFolder = new RunFolderStringSliderCore(_state);
        _runButton = new RunButtonCore(_state);
        _buildMaze = new BuildMazeButtonCore(_state);
        _buildButton = new BuildButtonCore(_state);
        _terminalButton = new TerminalButtonCore(_state);
        _playBackButton = new StartStopAlgoPlayBackButtonCore(_state);
    }

    public void Controller()
    {
        _runFolder.StringSliderCore();

        _headerLayout.Update();

        _click.Click();

        _playBackButton.UpdateButtonAvailability();

        _runButton.UpdateButtonAvailability();

        _buildMaze.UpdateButtonAvailability();

        _buildButton.UpdateButtonAvailability();

        _terminalButton.UpdateButtonAvailability();
    }
}