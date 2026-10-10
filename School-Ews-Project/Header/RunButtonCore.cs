
namespace Program;

public class RunButtonCore
{
    private readonly AppState _state;

    public RunButtonCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ButtonCore()
    {
        
    }
}