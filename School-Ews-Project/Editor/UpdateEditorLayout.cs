
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
        var _textPanel = _state.editor.layout.textPanel;
        var _mainBoxes = _state.common.boxes;

        if (_textPanel.CurrentLoadedFile.Length > 0)
        {   
            _updateEditorTextPanel.UpdateTextPanel();
        }
        else
        {
            _state.editor.layout.textPanel.FileNotLoadedText.Box =
                new CustomBox(
                    _textPanel.FileNotLoadedText.Box.CornerStates,
                    _mainBoxes.MainBoxPixelPositions[MainBoxNames.Editor],
                    _mainBoxes.MainBoxPixelLengts[MainBoxNames.Editor],
                    _mainBoxes.MainBoxesCornerRadius[MainBoxNames.Editor]
                );

            _textPanel.FileNotLoadedText = CalculateCenterTextInBox.Calculate(_textPanel.FileNotLoadedText);
        }
    }
}