
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

            File.WriteAllText(dataLocation + "/"+name, "");

            currentStorage[dataLocation] = newStorage;
        }
        else if(dataType != ArchiveStorageNames.File)
        {
            currentStorage.Remove(dataLocation);
        }

        return (currentStorage, dataLocation);
    }
}
