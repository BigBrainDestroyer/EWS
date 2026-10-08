using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace ShareData;

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
    
    public static string SharedDataFolderPath()
    {
        string ewsFolderPath = Directory.GetCurrentDirectory();
        string[] currentDirectoryArray = Directory.GetCurrentDirectory().Split(State.PathSyntax);

        for (int i = Directory.GetCurrentDirectory().Split(State.PathSyntax).Length - 1; i > 0; i--)
        {
            ewsFolderPath = ewsFolderPath.Substring(0, ewsFolderPath.Length - currentDirectoryArray[i].Length);
        }

        string sharedDataFolderPath = Directory.GetCurrentDirectory().Substring(
            0,
            Directory.GetCurrentDirectory().Length - 
            Directory.GetCurrentDirectory().Split(State.PathSyntax).Last().Length 
        ) + State.ShareDataFolderName;

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