
namespace Program;

public class UpdateArchiveStorage
{
    private readonly AppState _state;

    public UpdateArchiveStorage(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }


}