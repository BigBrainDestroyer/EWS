using System.Numerics;

namespace Program;

public class UpdateLoadFile
{
    private readonly AppState _state;
    private readonly UpdateTextView _textView;

    public UpdateLoadFile(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _textView = new UpdateTextView(_state);
    }

    public void CheckLoadFileState()
    {
        var _archive = _state.archive;
        var _textPanel = _state.editor.layout.textPanel;

        if(_textPanel.CurrentLoadedFile != _archive.LoadedFileDirectory)
        {
            _textPanel.PanelText = LoadNewFile(_archive.LoadedFileDirectory);

            (_textPanel.CursorTextPosition, _textPanel.TextGridBoxesOffSetX, _textPanel.TextGridBoxesOffSetY) = ResetAllPanelTextViewParameters();

            _textView.UpdateView();

            _textPanel.CurrentLoadedFile = _archive.LoadedFileDirectory;
        }
    }

    private List<string> LoadNewFile(string loadedFileDirectory)
    {
        List<string> newEditorText = new List<string>();
        
        foreach(string textRow in File.ReadLines(loadedFileDirectory))
        {
            newEditorText.Add(textRow);
        }

        if(newEditorText.Count == 0)
        {
            newEditorText.Add("");
        }

        return newEditorText;
    }

    private (Vector2, int, int) ResetAllPanelTextViewParameters()
    {
        return (new Vector2(0, 0), 0, 0);
    }
}