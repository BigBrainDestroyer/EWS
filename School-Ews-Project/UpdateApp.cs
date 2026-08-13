
namespace Program;

public class UpdateApp
{
    private readonly AppState _state;
    private readonly CalculateMainBoxes _calculateMainBoxes;
    private readonly CalculateEditorLayout _EditorLayout;

    public UpdateApp(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _calculateMainBoxes = new CalculateMainBoxes(_state);
        _EditorLayout = new CalculateEditorLayout(_state);
    }

    public void App()
    {
        _calculateMainBoxes.UpdateMainBoxes();

        _EditorLayout.UpdateEditorLayout();
    }
}