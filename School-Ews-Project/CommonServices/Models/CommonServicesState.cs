
namespace Program;

public class CommonServicesState
{
    public SettingsState settings { get; set; } = new SettingsState();
    public MainBoxesState boxes {get; set;} = new MainBoxesState();
    public InputState input { get; set; } = new InputState();
    public FontState font { get; set; } = new FontState();
    public TimeState time { get; set; } = new TimeState();
    public TerminalState terminal { get; set; } = new TerminalState();
}