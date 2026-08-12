
namespace Program;

public class AppState
{
    public CommonServices common { get; set; } = new CommonServices();
    public Editor editor { get; set; } = new Editor();
}