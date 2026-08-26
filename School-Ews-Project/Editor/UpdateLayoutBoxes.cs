using System.Numerics;

namespace Program;

public class UpdateEditorLayoutBoxes
{
    private readonly AppState _state;

    public UpdateEditorLayoutBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void EditorLayoutBoxes()
    {
        var _editorBoxes = _state.editor.layout.boxes;
        var _commonBoxes = _state.common.boxes;

        EditorBoxNames[] boxNames = Enum.GetValues<EditorBoxNames>();

        _editorBoxes.BoxPixelLengths[EditorBoxNames.TextPanel] = _commonBoxes.MainBoxPixelLengts[MainBoxNames.Editor];

        _editorBoxes.BoxPixelLengths[EditorBoxNames.Terminal] = _commonBoxes.MainBoxPixelLengts[MainBoxNames.Terminal];
        
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        var _common = _state.common.boxes;
        var _editor = _state.editor.layout.boxes;

        _editor.BoxPixelPositions[EditorBoxNames.TextPanel] = _common.MainBoxPixelPositions[MainBoxNames.Editor];

        _editor.BoxPixelPositions[EditorBoxNames.Terminal] = _common.MainBoxPixelPositions[MainBoxNames.Terminal];
    }
}