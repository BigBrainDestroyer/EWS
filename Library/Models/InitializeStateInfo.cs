
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
        string sharedDataFolderPath = Directory.GetCurrentDirectory().Substring(
            0,
            Directory.GetCurrentDirectory().Length - 
            Directory.GetCurrentDirectory().Split(State.pathSyntax).Last().Length 
        ) + State.ShareDataFolderName;

        if(!Directory.Exists(sharedDataFolderPath))
        {
            Directory.CreateDirectory(sharedDataFolderPath);
        }

        return sharedDataFolderPath;
    }

    public static string ShareMazeDataFilePath()
    {

        string filePath = SharedDataFolderPath() + State.pathSyntax + State.ShareMazeDataFileName;

        if(!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }

        return  filePath;
    }
}