
namespace Program;

public class UpdateEditorLayout
{
    private readonly AppState _state;
    private readonly UpdateEditorTextPanel _updateEditorTextPanel;

    public UpdateEditorLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _updateEditorTextPanel = new UpdateEditorTextPanel(_state);
    }

    public void EditorLayout()
    {
        _updateEditorTextPanel.UpdateTextPanel();
    }
}