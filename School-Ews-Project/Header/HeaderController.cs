
namespace Program;

public class HeaderController
{
    private readonly AppState _state;
    private readonly UpdateHeaderLayout _headerLayout;
    private readonly HeaderClick _click;

    public HeaderController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _headerLayout = new UpdateHeaderLayout(_state);
        _click = new HeaderClick(_state);
    }

    public void Controller()
    {
        _headerLayout.Update();

        _click.Click();
    }
}