
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

        (_header.AllProjectFolderNames, _header.AllProjectFolderNamesListPosition) =
            UpdateAllProjectFolderNames(
                _archive.ArchiveProjectFolderPaths, 
                _header.AllProjectFolderNames, 
                _archive.PathSyntax, 
                _header.AllProjectFolderNamesListPosition
            );

        _header.HeaderStringSlider[DrawHeaderObjectNames.RunFolderName].StringSliderBox.Text = 
            UpdateRunFolderNameText(_header.HeaderStringSlider[DrawHeaderObjectNames.RunFolderName].StringSliderBox.Text, _header.AllProjectFolderNames, _header.AllProjectFolderNamesListPosition);
    }

    private (string[], int) UpdateAllProjectFolderNames(string[] projectFolderPaths, string[] allProjectFolderNames, string pathSyntax, int allProjectFolderNamesListPosition)
    {
        if (projectFolderPaths.Length != allProjectFolderNames.Length)
        {
            string selectedFolderName = "";

            if(allProjectFolderNamesListPosition < allProjectFolderNames.Length && allProjectFolderNames.Length != 0)
            {
                selectedFolderName = allProjectFolderNames[allProjectFolderNamesListPosition];
            }
            
            allProjectFolderNames = new string[projectFolderPaths.Length];

            
            for (int i = 0; projectFolderPaths.Length > i; i++)
            {
                allProjectFolderNames[i] = projectFolderPaths[i].Split(pathSyntax).Last();
            }
            
            int selectedFolderListPosition = allProjectFolderNames.IndexOf(selectedFolderName);

            if (selectedFolderListPosition != -1)
            {
                allProjectFolderNamesListPosition = selectedFolderListPosition;
            }
            else
            {
                allProjectFolderNamesListPosition = 0;
            }
        }
        else
        {
            for (int i = 0; projectFolderPaths.Length > i; i++)
            {
                if (allProjectFolderNames[i] != projectFolderPaths[i].Split(pathSyntax).Last())
                {
                    allProjectFolderNames[i] = projectFolderPaths[i].Split(pathSyntax).Last();
                }
            }
        }

        return (allProjectFolderNames, allProjectFolderNamesListPosition);
    }

    private RaylibText UpdateRunFolderNameText(RaylibText oldText, string[] allProjectFolderNames, int allProjectFolderNamesListPosition)
    {
        if(allProjectFolderNamesListPosition < allProjectFolderNames.Length && allProjectFolderNamesListPosition >= 0 && allProjectFolderNames.Length != 0 && allProjectFolderNames[allProjectFolderNamesListPosition] != null)
        {
            return new RaylibText(allProjectFolderNames[allProjectFolderNamesListPosition], oldText.TextSize, oldText.TextSpacing);
        }
        
        return new RaylibText("", oldText.TextSize, oldText.TextSpacing);
    }

    public void RunFolderNameHeaderObjectClickAction(int addRequest)
    {
        var _header = _state.HeaderState;
        
        if(_header.AllProjectFolderNamesListPosition + addRequest > _header.AllProjectFolderNames.Length - 1)
        {
            _header.AllProjectFolderNamesListPosition = 0;
        }
        else if(_header.AllProjectFolderNamesListPosition + addRequest < 0)
        {
            _header.AllProjectFolderNamesListPosition = _header.AllProjectFolderNames.Length - 1;
        }
        else
        {
            _header.AllProjectFolderNamesListPosition += addRequest;
        }
    }
}