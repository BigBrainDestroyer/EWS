
namespace Program;

public class EditorLayoutState
{
    public EditorBoxLayoutState boxes { get; set; } = new EditorBoxLayoutState();
    public EditorTextPanelLayoutState textPanel { get; set; } = new EditorTextPanelLayoutState();
}