
namespace Program;

public class HeaderClick
{
    private readonly AppState _state;

    public HeaderClick(AppState state)
    {
        _state = state ?? throw new ArgumentNullException();
    }

    public void Click()
    {
        
    }
}