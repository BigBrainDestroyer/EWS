using System.Numerics;
using System.Diagnostics;


namespace Program;

public class UpdateLoadFile
{
    private readonly AppState _state;
    private readonly UpdateTextView _textView;
    private readonly UpdateEditorTextPanel _updateEditorTextPanel;

    public UpdateLoadFile(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _textView = new UpdateTextView(_state);
        _updateEditorTextPanel = new UpdateEditorTextPanel(_state);
    }

    public void CheckLoadFileState()
    {
        var _archive = _state.archive;
        var _textPanel = _state.editor.layout.textPanel;
        var _input = _state.common.input;
        var _time = _state.common.time;

        if(_textPanel.CurrentLoadedFile != _archive.LoadedFileDirectory)
        {
            Console.WriteLine("JA hij komt hier");
            Console.WriteLine(_archive.LoadedFileDirectory);

            (_input.FileChangeTimer, _textPanel.CurrentLoadedFile) = UpdateFileText(_textPanel.PanelText, _input.FileChangeTimer, _textPanel.CurrentLoadedFile);
            
            (_textPanel.PanelText, _archive.LoadedFileDirectory) = LoadNewFile(_archive.LoadedFileDirectory);

            Console.WriteLine(_textPanel.PanelText.Count());

            (_textPanel.CursorTextPosition, _textPanel.TextGridBoxesOffSetX, _textPanel.TextGridBoxesOffSetY) = ResetAllPanelTextViewParameters();

            _updateEditorTextPanel.UpdateTextPanel();
            _textView.UpdateView();

            Console.WriteLine("crazy shit");
            Console.WriteLine(_archive.LoadedFileDirectory);
            _textPanel.CurrentLoadedFile = _archive.LoadedFileDirectory;
        }

        _input.FileChangeTimer = UpdateFileChangeTimerTimer(_input.FileChangeTimer, _time.Stopwatch);

        if (_input.FileChangeTimer.TotalTime >= _input.FileChangeTimer.ReactionTime && _input.FileChangeTimer.FileHasChanged)
        {
            (_input.FileChangeTimer, _textPanel.CurrentLoadedFile) = 
                UpdateFileText(_textPanel.PanelText, _input.FileChangeTimer, _textPanel.CurrentLoadedFile);
        }
    }

    private (List<string>, string) LoadNewFile(string loadedFileDirectory)
    {
        List<string> newEditorText = new List<string>();

        if(File.Exists(loadedFileDirectory))
        {
            foreach (string textRow in File.ReadLines(loadedFileDirectory))
            {
                newEditorText.Add(textRow);
            }
        }
        else
        {
            loadedFileDirectory = "";
        }

        if(newEditorText.Count == 0)
        {
            newEditorText.Add("");
        }

        return (newEditorText, loadedFileDirectory);
    }

    private (Vector2, int, int) ResetAllPanelTextViewParameters()
    {
        return (new Vector2(0, 0), 0, 0);
    }

    private FileChangeTimer UpdateFileChangeTimerTimer(FileChangeTimer fileChangeTimer, Stopwatch stopwatch)
    {
        fileChangeTimer = CalculateUsingTime.UpdateFileChangeTimer(fileChangeTimer, stopwatch, fileChangeTimer.FileHasChanged);

        return fileChangeTimer;
    }

    private (FileChangeTimer, string) UpdateFileText(List<string> panelText, FileChangeTimer fileChangeTimer, string requestedDirectoryDirectory)
    {
        fileChangeTimer = new FileChangeTimer(0, 0, fileChangeTimer.ReactionTime, false);

        if(File.Exists(requestedDirectoryDirectory))
        {
            Console.WriteLine("LoadedFileUpdate");
            File.WriteAllLines(requestedDirectoryDirectory, panelText);
        }
        else
        {
            requestedDirectoryDirectory = "";
        }

        return (fileChangeTimer, requestedDirectoryDirectory);
    }
}