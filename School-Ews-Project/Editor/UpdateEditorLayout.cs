
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
        if (_state.editor.layout.textPanel.CurrentLoadedFile.Length > 0)
        {   
            _updateEditorTextPanel.UpdateTextPanel();
        }
    }
}