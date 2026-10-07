
namespace Program;

public class UpdateRunFolder
{
    private readonly AppState _state;

    public UpdateRunFolder(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Update()
    {
        var _header = _state.HeaderState;
        var _archive = _state.archive;

        _header.AllProjectFolderNames = UpdateAllProjectFolderNames(_archive.ArchiveProjectFolderPaths, _header.AllProjectFolderNames, _archive.PathSyntax);
    }

    private string[] UpdateAllProjectFolderNames(string[] projectFolderPaths, string[] allProjectFolderNames, string pathSyntax)
    {      
        if(projectFolderPaths.Length != allProjectFolderNames.Length)
        {
            allProjectFolderNames = new string[projectFolderPaths.Length];

            for (int i = 0; projectFolderPaths.Length < i; i++)
            {
                allProjectFolderNames[i] = projectFolderPaths[i].Split(pathSyntax).Last();
            }
        }
        else
        {
            for (int i = 0; projectFolderPaths.Length < i; i++)
            {
                if(allProjectFolderNames[i] != projectFolderPaths[i].Split(pathSyntax).Last())
                {
                    allProjectFolderNames[i] = projectFolderPaths[i].Split(pathSyntax).Last();
                }
            }
        }

        return allProjectFolderNames;
    }
}