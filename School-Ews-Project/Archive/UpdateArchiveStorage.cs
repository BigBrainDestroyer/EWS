using System.Linq;

namespace Program;

public class UpdateArchiveStorage
{
    private readonly AppState _state;

    public UpdateArchiveStorage(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public (Dictionary<string, ArchiveStorage>, string) UpdateAddDataType(Dictionary<string, ArchiveStorage> currentStorage, ArchiveStorageNames dataType, string dataLocation, string name)
    {
        if (dataType != ArchiveStorageNames.File && name.Length > 0)
        {
            currentStorage.Add(
                    dataLocation + "/" + name,
                    new ArchiveStorage(
                    new List<string>(),
                    dataType,
                    false
                ));

            string directory = Directory.GetCurrentDirectory();

            if(Path.Exists(Path.Combine(directory, dataLocation)))
            {
                directory = Path.Combine(directory, dataLocation, name);
                Directory.CreateDirectory(directory);

                dataLocation = dataLocation + "/" + name;

                Console.WriteLine(directory);
                Console.WriteLine(dataLocation);
                Console.WriteLine("Ja werkt");
            }
            else
            {
                Console.WriteLine(directory);
                Console.WriteLine(dataLocation);
                Console.WriteLine("Oeps Ging Greatly WRONG");
                //Update Hole Directory
            }
        }
        else if (name.Length > 0)
        {
            ArchiveStorage newStorage = currentStorage[dataLocation];

            newStorage.FolderStorage.Add(name);

            File.WriteAllText(dataLocation + "/" + name, "");

            currentStorage[dataLocation] = newStorage;
        }
        else if(dataType != ArchiveStorageNames.File)
        {
            currentStorage.Remove(dataLocation);
        }

        return (currentStorage, dataLocation);
    }

    public Dictionary<string, ArchiveStorage> LoadStorage(string dataLocation)
    {
        Dictionary<string, ArchiveStorage> newStorage = new Dictionary<string, ArchiveStorage>();

        string[] dataLocationList = dataLocation.Split("/");

        string directory = Directory.GetCurrentDirectory();

        string[] ProjectFolders = Directory.GetDirectories(directory + "/" + dataLocationList[0]);

        foreach(string ProjectFolder in ProjectFolders)
        {
            newStorage.Add(
                ProjectFolder,
                new ArchiveStorage(
                Directory.GetFiles(ProjectFolder).ToList(),
                ArchiveStorageNames.ProjectFolder,
                false
            ));

            newStorage = FolderLoop(newStorage, ProjectFolder);
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

    public Dictionary<string, ArchiveStorage> FolderLoop(Dictionary<string, ArchiveStorage> newStorage, string directory)
    {
        string[] folders = Directory.GetDirectories(directory);

        foreach(string folder in folders)
        {
            newStorage.Add(
                folder,
                new ArchiveStorage(
                Directory.GetFiles(folder).ToList(),
                ArchiveStorageNames.Folder,
                false
            ));

            newStorage = FolderLoop(newStorage, folder);
        }

        return newStorage;
    }
}

//Updates
// directory weeten up te daten [X] smal universal update alles moet hele directory krijgen dat is denk ik handiger
// bestanden weeten te tekenen
// user input te mengen 
// files schrijven
// cancel manier files of bestanden 
