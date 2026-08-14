
namespace Program;

public class EditorState
{
    public EditorLayoutState layout { get; set; } = new EditorLayoutState();
    public EditorCoreState core { get; set; } = new EditorCoreState();
}