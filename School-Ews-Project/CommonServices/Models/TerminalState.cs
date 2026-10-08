using System.Diagnostics;

namespace Program;

public class TerminalState
{
    public Dictionary<string, Process> TerminalProcesses { get; set; } = new Dictionary<string, Process>();

    //Linux
    public string LinuxTerminalAppName { get; } = "bash";
    
    
    //Windows
    public string WindowsTerminalAppName { get; } = "cmd.exe";

    //Universal
    public string CommandCloseTerminal { get; } = "exit";
    public string CommandRequestedPath { get; } = "cd";
    public string CommandNewConsole { get; } = "dotnet new console --force";
    public string CommandNewReference { get; } = "dotnet add reference";


    public string ProjectsDllReferenceFileLocation { get; set; } = "";
    public string DllProjectFolderName { get; } = "Library";
    public string DllProjectReferenceFileName { get; } = "Library.csproj";
}