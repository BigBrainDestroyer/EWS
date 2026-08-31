using System.Numerics;
using Raylib_cs;

namespace Program;

public enum ArchiveStorageNames
{
    ProjectFolder,
    Folder,
    File
}

public struct ArchiveStorage
{
    public ArchiveStorageNames DataType;

    public string Name;

    public string DataLocation;

    public Dictionary<string[], ArchiveStorage>? FolderStorage;

    public ArchiveStorage(ArchiveStorageNames dataType, string name, string dataLocation, Dictionary<string[], ArchiveStorage>? folderStorage)
    {
        DataType = dataType;
        Name = name;
        DataLocation = dataLocation;
        FolderStorage = folderStorage;
    }
}

public class ArchiveCoreState
{
    public ArchiveStorageNames[] AddLegalDataTypeToStorage { get; set; } = new ArchiveStorageNames[3];

    public Dictionary<string, ArchiveStorage> Storage { get; set; } = new Dictionary<string, ArchiveStorage>();
    
    public string CurentStorageDataLocation { get; set; } = "AppProjects";
}