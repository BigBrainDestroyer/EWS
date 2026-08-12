
namespace Program;

public class CalculateEditorLayout
{
    private readonly AppState _state;
    
    public CalculateEditorLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void LayoutBoxes()
    {
        
    }
}