
namespace Program;

public class OutsideTerminal
{
    private readonly AppState _state;

    public OutsideTerminal(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
}