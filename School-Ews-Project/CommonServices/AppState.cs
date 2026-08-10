namespace Program;

public enum MainBoxNames
{
    Archive,
    Maze,
    Statistics,
    Editor
}

public class AppState
{
    public CommonServices common {get; set;} = new CommonServices();
}