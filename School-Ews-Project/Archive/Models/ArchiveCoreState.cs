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
    public Dictionary<string, ArchiveStorage>? FolderStorage;
    public ArchiveStorageNames DataType;
    public string Name;
    public string DataLocation;
    public bool ViewFolderStorage;

    public ArchiveStorage(Dictionary<string, ArchiveStorage>? folderStorage, ArchiveStorageNames dataType, string name, string dataLocation, bool viewFolderStorage)
    {
        FolderStorage = folderStorage;
        DataType = dataType;
        Name = name;
        DataLocation = dataLocation;
        ViewFolderStorage = viewFolderStorage; 
    }
}

public class ArchiveCoreState
{
    public ArchiveStorageNames?[] AddLegalDataTypeToStorage { get; set; } = new ArchiveStorageNames?[3];

    public Dictionary<string, ArchiveStorage> Storage { get; set; } = new Dictionary<string, ArchiveStorage>();
    
    public string CurentStorageDataLocation { get; set; } = "AppProjects";
}