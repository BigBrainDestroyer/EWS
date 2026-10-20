using Raylib_cs;

namespace Program;

public class HeaderClick
{
    private readonly AppState _state;
    private readonly RunFolderStringSliderCore _runFolder;
    private readonly RunButtonCore _runButton;
    private readonly BuildButtonCore _buildButton;
    private readonly TerminalButtonCore _terminalButton;
    private readonly BuildMazeButtonCore _buildMaze;
    private readonly StartStopAlgoPlayBackButtonCore _playBackButton;
    private readonly MazeSizeStringSliderCore _mazeSizeStringSlider;
    private readonly BatchSizeStringSlider _batchSizeStringSlider;

    public HeaderClick(AppState state)
    {
        _state = state ?? throw new ArgumentNullException();
        _runFolder = new RunFolderStringSliderCore(_state);
        _runButton = new RunButtonCore(_state);
        _buildButton = new BuildButtonCore(_state);
        _terminalButton = new TerminalButtonCore(_state);
        _buildMaze = new BuildMazeButtonCore(_state);
        _playBackButton = new StartStopAlgoPlayBackButtonCore(_state);
        _mazeSizeStringSlider = new MazeSizeStringSliderCore(_state);
        _batchSizeStringSlider = new BatchSizeStringSlider(_state);
    }

    public void Click()
    {
        CheckAllHeaderObjects();
    }

    public void CheckAllHeaderObjects()
    {
        var _boxes = _state.common.boxes;
        var _header = _state.HeaderState;

        if(!CheckField.MouseOnField(_boxes.HeaderPixelPosition, _boxes.HeaderPixelLengths, Raylib.GetMousePosition())) return;

        foreach(DrawHeaderObjectNames objectName in _header.HeaderObjectBoxes.Keys)
        {
            if(!CheckField.MouseOnField(_header.HeaderObjectBoxes[objectName].PixelPosition, _header.HeaderObjectBoxes[objectName].PixelLengths, Raylib.GetMousePosition())) continue;

            if(_header.HeaderStringSlider.ContainsKey(objectName))
            {
                if(CheckField.MouseOnField(_header.HeaderStringSlider[objectName].UpTexture.Box.PixelPosition, _header.HeaderStringSlider[objectName].UpTexture.Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    // IS OP UP ARROW
                    if(_header.HeaderObjectAvailable[objectName])
                    {
                        _header.HeaderStringSlider[objectName].UpTexture.TextureColor = _header.HeaderStringSliderColors[objectName].TextureUpColors.Y;
                        _header.HeaderStringSlider[objectName].UpTexture.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].UpClickFieldColors.Y;

                        _header.HeaderStringSlider[objectName].TitleStringBox.TextColor = _header.HeaderStringSliderColors[objectName].TitleTextColors.X;
                        _header.HeaderStringSlider[objectName].TitleStringBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].TitleBoxColors.X;
                        
                        _header.HeaderStringSlider[objectName].StringSliderBox.TextColor = _header.HeaderStringSliderColors[objectName].StringSliderTextColors.X;
                        _header.HeaderStringSlider[objectName].StringSliderBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].StringSliderBoxColors.X;
                    } 

                    if(Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        StringSliderClick(objectName, 1, _header.HeaderObjectAvailable[objectName]);
                    }
                }
                else if(CheckField.MouseOnField(_header.HeaderStringSlider[objectName].DownTexture.Box.PixelPosition, _header.HeaderStringSlider[objectName].DownTexture.Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    // IS OP ARROW DOWN
                    if(_header.HeaderObjectAvailable[objectName])
                    {
                        _header.HeaderStringSlider[objectName].DownTexture.TextureColor = _header.HeaderStringSliderColors[objectName].TextureDownColors.Y;
                        _header.HeaderStringSlider[objectName].DownTexture.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].DownClickFieldColors.Y;

                        _header.HeaderStringSlider[objectName].TitleStringBox.TextColor = _header.HeaderStringSliderColors[objectName].TitleTextColors.X;
                        _header.HeaderStringSlider[objectName].TitleStringBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].TitleBoxColors.X;
                        
                        _header.HeaderStringSlider[objectName].StringSliderBox.TextColor = _header.HeaderStringSliderColors[objectName].StringSliderTextColors.X;
                        _header.HeaderStringSlider[objectName].StringSliderBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].StringSliderBoxColors.X;
                    }

                    if(Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        StringSliderClick(objectName, -1, _header.HeaderObjectAvailable[objectName]);
                    }
                }
            }
            else if(_header.HeaderButtons.ContainsKey(objectName))
            {
                if(_header.HeaderObjectAvailable[objectName])
                {
                    _header.HeaderButtons[objectName].TextColor = _header.HeaderButtonColors[objectName].SymbolColors.Y;
                    _header.HeaderButtons[objectName].BoxBackgroundColor = _header.HeaderButtonColors[objectName].BoxColors.Y;
                }

                if(Raylib.IsMouseButtonPressed(MouseButton.Left) && CheckField.MouseOnField(_header.HeaderButtons[objectName].Box.PixelPosition, _header.HeaderButtons[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    HeaderButtonsClick(objectName, _header.HeaderObjectAvailable[objectName]);
                }
            }
            else if(_header.HeaderCenterTextureInBox.ContainsKey(objectName))
            {
                if(_header.HeaderObjectAvailable[objectName])
                {
                    _header.HeaderCenterTextureInBox[objectName].TextureColor = _header.HeaderButtonColors[objectName].SymbolColors.Y;
                    _header.HeaderCenterTextureInBox[objectName].BoxBackgroundColor = _header.HeaderButtonColors[objectName].BoxColors.Y;
                }
                
                if(Raylib.IsMouseButtonPressed(MouseButton.Left) && CheckField.MouseOnField(_header.HeaderCenterTextureInBox[objectName].Box.PixelPosition, _header.HeaderCenterTextureInBox[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    HeaderButtonsClick(objectName, _header.HeaderObjectAvailable[objectName]);
                }
            }
        }
    }

    private void StringSliderClick(DrawHeaderObjectNames objectName, int addRequest, bool isAvailable)
    {
        if(objectName == DrawHeaderObjectNames.MazeSize && isAvailable)
        {
            _mazeSizeStringSlider.StringSliderClick(addRequest);
        }
        else if(objectName == DrawHeaderObjectNames.BatchSize && isAvailable)
        {
            _batchSizeStringSlider.StringSliderClick(addRequest);
        }
        else if(objectName == DrawHeaderObjectNames.PlayBackSpeed && isAvailable)
        {
            
        }
        else if(objectName == DrawHeaderObjectNames.RunFolderName && isAvailable)
        {
            _runFolder.RunFolderNameHeaderObjectClickAction(addRequest);
        }
    }

    private void HeaderButtonsClick(DrawHeaderObjectNames objectName, bool isAvailable)
    {
        if(objectName == DrawHeaderObjectNames.RunButton && isAvailable)
        {
            _runButton.ButtonClick();
        }
        else if(objectName == DrawHeaderObjectNames.BuildButton && isAvailable)
        {
            _buildButton.ButtonCLick();
        }
        else if(objectName == DrawHeaderObjectNames.TerminalRequest && isAvailable)
        {
            _terminalButton.ButtonClick();
        }
        else if(objectName == DrawHeaderObjectNames.BuildMaze && isAvailable)
        {
            _buildMaze.ButtonClick();
        }
        else if(objectName == DrawHeaderObjectNames.StartStopAlgoPlayBack && isAvailable)
        {
            _playBackButton.ButtonClick();
        }
    }
}