
namespace Program;

public class AppState
{
    public CommonServicesState common { get; set; } = new CommonServicesState();
    public HeaderState HeaderState { get; set; } = new HeaderState();
    public EditorState editor { get; set; } = new EditorState();
    public MazeState maze { get; set; } = new MazeState();
    public ArchiveState archive { get; set; } = new ArchiveState();
    public AlgoConnectorState connector { get; set; } = new AlgoConnectorState();
}