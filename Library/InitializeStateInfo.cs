using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace ShareData;

[EditorBrowsable(EditorBrowsableState.Never)]

internal static class Initialize
{
    public static OsSystem OsPlatform()
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OsSystem.Linux;
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OsSystem.Windows;
        }

        return OsSystem.Linux;
    }

    public static string PathSyntax()
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "/";
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "\\";
        }

        return "";
    }

    public static string ProgramStorageFolderPath()
    {
        string programStorageFolderPath = 
            Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData) + 
                State.PathSyntax + 
                State.ProgramStorageFolderName;

        if(!Directory.Exists(programStorageFolderPath))
        {
            Directory.CreateDirectory(programStorageFolderPath);
        }

        return programStorageFolderPath;
    }
    
    public static string SharedDataFolderPath()
    {
        string sharedDataFolderPath = 
                App.directoryInfo.ProgramStorageFolderPath + 
                State.PathSyntax + 
                State.ShareDataFolderName;

        if(!Directory.Exists(sharedDataFolderPath))
        {
            Directory.CreateDirectory(sharedDataFolderPath);
        }

        return sharedDataFolderPath;
    }

    public static string ShareMazeDataFilePath()
    {
        string filePath = SharedDataFolderPath() + State.PathSyntax + State.ShareMazeDataFileName;

        if(!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            File.WriteAllText(filePath, "{}");
        }

        return  filePath;
    }

    public static string ShareAlgoDataFilePath()
    {
        string filePath = SharedDataFolderPath() + State.PathSyntax + State.ShareAlgoDataFileName;

        if(!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            File.WriteAllText(filePath, "{}");
        }

        return  filePath;
    }

    public static JsonSerializerOptions JsonSerializeSettings()
    {
        JsonSerializerOptions jsonSerializeSettings = new JsonSerializerOptions();

        //Settings
        jsonSerializeSettings.Converters.Add(new JsonStringEnumConverter());
        jsonSerializeSettings.WriteIndented = true;
        jsonSerializeSettings.PropertyNameCaseInsensitive = true;

        return jsonSerializeSettings;
    }
}