using System.Diagnostics;
using ShareData;

namespace Program;

public class OutsideTerminal
{
    private readonly AppState _state;

    public OutsideTerminal(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void MakeProjectInNewProjectFolder(ShareData.OsSystem osSystem, string requestedFolderPath)
    {
        if (!Directory.Exists(requestedFolderPath))
        {
            return;
        }

        var _terminal = _state.common.terminal;

        TerminalCommands commands = (osSystem == ShareData.OsSystem.Linux) ? _terminal.LinuxTerminalCommands : _terminal.WindowsTerminalCommands;
        
        string projectsDllReferenceFileLocation = UpdateRequestedPathToOsSystemRequirements(_terminal.ProjectsDllReferenceFileLocation);

        ProcessStartInfo terminalParams = new ProcessStartInfo();

        terminalParams.FileName = commands.TerminalAppName;
        terminalParams.WorkingDirectory = requestedFolderPath;
        terminalParams.RedirectStandardInput = true;
        terminalParams.CreateNoWindow = true;
        
        Process terminal = Process.Start(terminalParams) ?? throw new ArgumentNullException(nameof(terminal));

        if (terminal != null)
        {
            terminal.StandardInput.WriteLine(commands.NewConsole);
            terminal.StandardInput.WriteLine(commands.NewReference + projectsDllReferenceFileLocation);
            terminal.StandardInput.WriteLine(commands.CloseTerminal);
            terminal.Dispose();
        }

        return;
    }

    private string UpdateRequestedPathToOsSystemRequirements(string requestedPath)
    {
        if (App.directoryInfo.OsSystem == OsSystem.Linux)
        {
            requestedPath = requestedPath.Replace("'", "'\\''");

            requestedPath = "'" + requestedPath + "'";
        }
        else if (App.directoryInfo.OsSystem == OsSystem.Windows)
        {
            requestedPath = "\"" + requestedPath + "\"";
        }

        return requestedPath;
    }

    public void RunAlgoProject(ShareData.OsSystem osSystem, string requestedFolderPath)
    {
        if (!Directory.Exists(requestedFolderPath))
        {
            return;
        }

        var _terminal = _state.common.terminal;
        var _play = _state.playMaze;

        TerminalCommands commands = (osSystem == ShareData.OsSystem.Linux) ? _terminal.LinuxTerminalCommands : _terminal.WindowsTerminalCommands;
        
        ProcessStartInfo terminalParams = new ProcessStartInfo();

        terminalParams.FileName = commands.TerminalAppName;
        terminalParams.WorkingDirectory = requestedFolderPath;
        terminalParams.RedirectStandardInput = true;
        terminalParams.CreateNoWindow = true;

        Process terminal = Process.Start(terminalParams) ?? throw new ArgumentNullException(nameof(terminal));

        if (terminal != null)
        {
            terminal.StandardInput.WriteLine(commands.RunProject);
            terminal.StandardInput.WriteLine(commands.CloseTerminal);

            _play.IsAlgoRunning = true;
            
            RunningProject(terminal);
        }

        return;
    }

    private async Task RunningProject(Process terminal)
    {
        var _terminal = _state.common.terminal;
        var _play = _state.playMaze;
        
        while(!terminal.HasExited)
        {   
            await Task.Delay(200);
            Debug.WriteLine("AlgoIsRunning");
        }

        Debug.WriteLine("AlgoIsDone");

        terminal.Dispose();

        _play.IsAlgoRunningDone = true;
    }

    public void BuildAlgoProject(ShareData.OsSystem osSystem, string requestedFolderPath)
    {
        if (!Directory.Exists(requestedFolderPath))
        {
            return;
        }

        var _terminal = _state.common.terminal;
        var _play = _state.playMaze;

        TerminalCommands commands = (osSystem == ShareData.OsSystem.Linux) ? _terminal.LinuxTerminalCommands : _terminal.WindowsTerminalCommands;
        
        ProcessStartInfo terminalParams = new ProcessStartInfo();

        terminalParams.FileName = commands.TerminalAppName;
        terminalParams.WorkingDirectory = requestedFolderPath;
        terminalParams.RedirectStandardInput = true;
        terminalParams.CreateNoWindow = true;

        Process terminal = Process.Start(terminalParams) ?? throw new ArgumentNullException(nameof(terminal));

        if (terminal != null)
        {
            terminal.StandardInput.WriteLine(commands.BuildProject);
            terminal.Dispose();
        }

        return;
    }

    public void RequestVisualTerminal(ShareData.OsSystem osSystem, string requestedFolderPath)
    {
        if (!Directory.Exists(requestedFolderPath))
        {
            return;
        }

        var _terminal = _state.common.terminal;
        var _play = _state.playMaze;

        TerminalCommands commands = (osSystem == ShareData.OsSystem.Linux) ? _terminal.LinuxTerminalCommands : _terminal.WindowsTerminalCommands;
        
        ProcessStartInfo terminalParams = new ProcessStartInfo();

        terminalParams.FileName = commands.TerminalAppName;
        terminalParams.WorkingDirectory = requestedFolderPath;
        terminalParams.RedirectStandardInput = true;
        terminalParams.CreateNoWindow = true;

        Process terminal = Process.Start(terminalParams) ?? throw new ArgumentNullException(nameof(terminal));

        if (terminal != null)
        {
            terminal.StandardInput.WriteLine(commands.StartTerminal);
            terminal.Dispose();
        }

        return;
    }
}