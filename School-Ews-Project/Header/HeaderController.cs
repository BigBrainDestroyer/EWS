using Raylib_cs;

namespace Program;

public class HeaderController
{
    private readonly AppState _state;

    public HeaderController(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Controller()
    {
        UpdateLayout()
    }
}