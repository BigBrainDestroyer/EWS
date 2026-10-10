using System.Diagnostics;

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
        if(!Directory.Exists(requestedFolderPath))
        {
            return;
        }
        
        var _terminal = _state.common.terminal;

        ProcessStartInfo terminalParams = new ProcessStartInfo();

        terminalParams.FileName = (osSystem == ShareData.OsSystem.Linux) ? _terminal.LinuxTerminalAppName : _terminal.WindowsTerminalAppName;
        terminalParams.RedirectStandardInput = true;

        Process terminal = Process.Start(terminalParams);

        if(terminal != null)
        {
            terminal.StandardInput.WriteLine(_terminal.CommandRequestedPath);
            terminal.StandardInput.WriteLine(_terminal.CommandRequestedPath + " " + requestedFolderPath);
            terminal.StandardInput.WriteLine(_terminal.CommandNewConsole);
            terminal.StandardInput.WriteLine(_terminal.CommandNewReference + " " + _terminal.ProjectsDllReferenceFileLocation);
            terminal.StandardInput.WriteLine(_terminal.CommandCloseTerminal);
            terminal.Dispose();
        }

        return;
    }
}