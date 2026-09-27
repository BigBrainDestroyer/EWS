
namespace Program;

public class ArchiveCalculateEditDataType 
{
    private readonly AppState _state;

    public ArchiveCalculateEditDataType(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void EditDataType()
    {
        CalculateEditLayout();
    }

    private void CalculateEditLayout()
    {
        
    }
}