
namespace Program;

public class HeaderController
{
    private readonly AppState _state;
    private readonly UpdateHeaderLayout _headerLayout;

    public HeaderController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _headerLayout = new UpdateHeaderLayout(_state);
    }

    public void Controller()
    {
        _headerLayout.Update();
    }
}