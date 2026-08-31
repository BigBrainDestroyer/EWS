
namespace Program;

public class ArchiveState
{
    public ArchiveCoreState core { get; set; } = new ArchiveCoreState();
    public ArchiveLayoutState layout { get; set; } = new ArchiveLayoutState();
}