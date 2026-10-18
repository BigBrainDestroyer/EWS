using System.Diagnostics;

namespace Program;

public struct TerminalCommands
{
    public string TerminalAppName;

    public string StartTerminal;

    public string NewConsole;

    public string NewReference;

    public string RunProject;

    public string BuildProject;

    public string CloseTerminal;

    public TerminalCommands(string terminalAppName, string startTerminal, string newConsole, string newReference, string runProject, string buildProject, string closeTerminal)
    {
        TerminalAppName = terminalAppName;

        StartTerminal = startTerminal;

        NewConsole = newConsole;

        NewReference = newReference;

        RunProject = runProject;

        BuildProject = buildProject;

        CloseTerminal = closeTerminal;
    }
}

public class TerminalState
{
    public TerminalCommands LinuxTerminalCommands =
        new TerminalCommands(
            "bash", //Process Starting App
            "ptyxis --new-window --working-directory=\"$PWD\" -- bash",
            "dotnet new console --force",
            "dotnet add reference ", //Add Path
            "dotnet run",
            "ptyxis --new-window --working-directory=\"$PWD\" -- bash -c 'dotnet build; exec bash'",
            "exit"
        );

    public TerminalCommands WindowsTerminalCommands =
        new TerminalCommands(
            "cmd.exe", //Process Starting App
            "start \"\" /d \"%CD%\" cmd.exe /d",
            "dotnet new console --force",
            "dotnet add reference ", // Add Path
            "dotnet run", 
            "start \"\" /d \"%CD%\" cmd.exe /d /k dotnet build",
            "exit"
        );

    public string ProjectsDllReferenceFileLocation { get; set; } = "";
    public string DllProjectFolderName { get; } = "Library";
    public string DllProjectReferenceFileName { get; } = "Library.csproj";
}