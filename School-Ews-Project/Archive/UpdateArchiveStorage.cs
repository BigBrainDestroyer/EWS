using ShareData;

namespace Program;

public class UpdateArchiveStorage
{
    private readonly AppState _state;
    private readonly OutsideTerminal _terminal;

    public UpdateArchiveStorage(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _terminal = new OutsideTerminal(_state);
    }

    public (Dictionary<string, ArchiveStorage>, string) UpdateAddDataType(Dictionary<string, ArchiveStorage> currentStorage, ShareData.OsSystem osSystem, ArchiveStorageNames dataType, string dataLocation, string projectDataLocation, string name, string pathSyntax)
    {
        if (name.Length > 0)
        {
            if (dataType != ArchiveStorageNames.File && !currentStorage.ContainsKey(dataLocation + pathSyntax + name))
            {
                string directory = Directory.GetCurrentDirectory();

                if (Path.Exists(Path.Combine(dataLocation)))
                {
                    currentStorage.Add(
                            dataLocation + pathSyntax + name,
                            new ArchiveStorage(
                            new List<string>(),
                            dataType,
                            true
                        ));

                    directory = Path.Combine(directory, dataLocation, name);
                    Directory.CreateDirectory(directory);

                    dataLocation = dataLocation + pathSyntax + name;

                    if(dataType == ArchiveStorageNames.ProjectFolder)
                    {
                        _terminal.MakeProjectInNewProjectFolder(osSystem, dataLocation);
                    }
                }
                else
                {
                    dataLocation = projectDataLocation;
                }
            }
            else if (currentStorage.ContainsKey(dataLocation) && !currentStorage[dataLocation].FolderStorage.Contains(dataLocation + pathSyntax + name))
            {   
                ArchiveStorage newStorage = currentStorage[dataLocation];

                newStorage.FolderStorage.Add(dataLocation + pathSyntax + name);

                File.WriteAllText(dataLocation + pathSyntax + name, "");

                currentStorage[dataLocation] = newStorage;
            }
        }

        currentStorage = LoadStorage(currentStorage, projectDataLocation);
        
        return (currentStorage, dataLocation);
    }

    public Dictionary<string, ArchiveStorage> LoadStorage(Dictionary<string, ArchiveStorage> currentStorage, string projectDataLocation)
    {
        Dictionary<string, ArchiveStorage> newStorage = new Dictionary<string, ArchiveStorage>();

        if(!Directory.Exists(projectDataLocation))
        {
            Directory.CreateDirectory(projectDataLocation);
        }
        
        string[] ProjectFolders = Directory.GetDirectories(projectDataLocation);

        Array.Sort(ProjectFolders);

        foreach(string ProjectFolder in ProjectFolders)
        {
            if(currentStorage.ContainsKey(ProjectFolder))
            {
                newStorage.Add(
                    ProjectFolder,
                    new ArchiveStorage(
                    Directory.GetFiles(ProjectFolder).ToList(),
                    ArchiveStorageNames.ProjectFolder,
                    currentStorage[ProjectFolder].ViewFolderStorage
                ));
            }
            else
            {
                newStorage.Add(
                    ProjectFolder,
                    new ArchiveStorage(
                    Directory.GetFiles(ProjectFolder).ToList(),
                    ArchiveStorageNames.ProjectFolder,
                    false
                ));
            }

            newStorage = FolderLoop(currentStorage, newStorage,ProjectFolder);
        }

        foreach(string key in newStorage.Keys)
        {
            Console.WriteLine(key);
            foreach(string file in newStorage[key].FolderStorage)
            {
                Console.WriteLine(file);
            }
        }
        
        return newStorage;
    }

    public Dictionary<string, ArchiveStorage> FolderLoop(Dictionary<string, ArchiveStorage> currentStorage, Dictionary<string, ArchiveStorage> newStorage, string directory)
    {
        string[] folders = Directory.GetDirectories(directory);

        folders.Sort(StringComparer.Ordinal);

        foreach(string folder in folders)
        {
            if (currentStorage.ContainsKey(folder))
            {
                newStorage.Add(
                    folder,
                    new ArchiveStorage(
                    Directory.GetFiles(folder).ToList(),
                    ArchiveStorageNames.Folder,
                    currentStorage[folder].ViewFolderStorage
                ));
            }
            else
            {
                newStorage.Add(
                    folder,
                    new ArchiveStorage(
                    Directory.GetFiles(folder).ToList(),
                    ArchiveStorageNames.Folder,
                    false
                ));
            }

            FolderLoop(currentStorage, newStorage, folder);
        }

        return newStorage;
    }

    public void UpdateProjectsFolder()
    {
        var _archive = _state.archive;

        if(_archive.ProjectsFolderHasChanged)
        {
            _archive.ProjectsFolderHasChanged = false;

            _archive.Storage = LoadStorage(_archive.Storage, _archive.ProjectsDataLocation);

            _archive.ArchiveProjectFolderPaths = Directory.GetDirectories(_archive.ProjectsDataLocation);
        }
    }
} 