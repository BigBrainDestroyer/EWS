
namespace Program;

public class UpdateEditorLayout
{
    private readonly AppState _state;
    private readonly UpdateEditorTextPanel _updateEditorTextPanel;
    private readonly UpdateEditorLayoutBoxes _updateEditorLayoutBoxes;

    public UpdateEditorLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _updateEditorLayoutBoxes = new UpdateEditorLayoutBoxes(_state);
        _updateEditorTextPanel = new UpdateEditorTextPanel(_state);
    }

    public void EditorLayout()
    {
        _updateEditorLayoutBoxes.EditorLayoutBoxes();

        _updateEditorTextPanel.UpdateTextPanel();
    }


}