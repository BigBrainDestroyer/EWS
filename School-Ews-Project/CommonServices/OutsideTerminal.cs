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

        requestedFolderPath = UpdateRequestedFolderPathToOsSystemRequirements(requestedFolderPath);

        string projectsDllReferenceFileLocation = UpdateRequestedDllPathToOsSystemRequirements(_terminal.ProjectsDllReferenceFileLocation);

        ProcessStartInfo terminalParams = new ProcessStartInfo();

        terminalParams.FileName = (osSystem == ShareData.OsSystem.Linux) ? _terminal.LinuxTerminalAppName : _terminal.WindowsTerminalAppName;
        terminalParams.RedirectStandardInput = true;

        Process terminal = Process.Start(terminalParams) ?? throw new ArgumentNullException(nameof(terminal));

        if (terminal != null)
        {
            terminal.StandardInput.WriteLine(_terminal.CommandRequestedPath);
            terminal.StandardInput.WriteLine($"{_terminal.CommandRequestedPath} {requestedFolderPath}");
            terminal.StandardInput.WriteLine(_terminal.CommandNewConsole);
            terminal.StandardInput.WriteLine(_terminal.CommandNewReference + " " + projectsDllReferenceFileLocation);
            terminal.StandardInput.WriteLine(_terminal.CommandCloseTerminal);
            terminal.Dispose();
        }

        return;
    }

    private string UpdateRequestedFolderPathToOsSystemRequirements(string requestedFolderPath)
    {
        if (App.directoryInfo.OsSystem == OsSystem.Linux)
        {
            requestedFolderPath = requestedFolderPath.Replace("'", "'\\''");

            requestedFolderPath = "'" + requestedFolderPath + "'";
        }
        else if (App.directoryInfo.OsSystem == OsSystem.Windows)
        {
            requestedFolderPath = "\"" + requestedFolderPath + "\"";

            requestedFolderPath = "/d" + " " + requestedFolderPath;
        }

        return requestedFolderPath;
    }

    private string UpdateRequestedDllPathToOsSystemRequirements(string requestedDllPath)
    {
        if (App.directoryInfo.OsSystem == OsSystem.Linux)
        {
            requestedDllPath = requestedDllPath.Replace("'", "'\\''");

            requestedDllPath = "'" + requestedDllPath + "'";
        }
        else if (App.directoryInfo.OsSystem == OsSystem.Windows)
        {
            requestedDllPath = "\"" + requestedDllPath + "\"";
        }
        
        return requestedDllPath;
    }

    private void RunAlgoProject()
    {
        
    }
}