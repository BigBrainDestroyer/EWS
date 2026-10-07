using Raylib_cs;

namespace Program;

public class HeaderClick
{
    private readonly AppState _state;
    private readonly UpdateRunFolder _runFolder;

    public HeaderClick(AppState state)
    {
        _state = state ?? throw new ArgumentNullException();
        _runFolder = new UpdateRunFolder(_state);
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
                    _header.HeaderStringSlider[objectName].UpTexture.TextureColor = _header.HeaderStringSliderColors[objectName].TextureUpColors.ReactionColor;
                    _header.HeaderStringSlider[objectName].UpTexture.TextureColor = _header.HeaderStringSliderColors[objectName].UpClickFieldColors.ReactionColor;

                    //_header.HeaderStringSlider[objectName].TitleStringBox.TextColor = _header.HeaderStringSliderColors[objectName].TitleTextColors.ReactionColor;
                    //_header.HeaderStringSlider[objectName].StringSliderBox.TextColor = _header.HeaderStringSliderColors[objectName].StringSliderTextColors.ReactionColor;
                    //_header.HeaderStringSlider[objectName].TitleStringBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].TitleBoxColors.ReactionColor;
                    //_header.HeaderStringSlider[objectName].StringSliderBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].StringSliderBoxColors.ReactionColor;

                    if(Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        StringSliderClick(objectName, 1);
                    }
                }
                else if(CheckField.MouseOnField(_header.HeaderStringSlider[objectName].DownTexture.Box.PixelPosition, _header.HeaderStringSlider[objectName].DownTexture.Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    // IS OP ARROW DOWN
                    _header.HeaderStringSlider[objectName].DownTexture.TextureColor = _header.HeaderStringSliderColors[objectName].TextureDownColors.ReactionColor;
                    _header.HeaderStringSlider[objectName].DownTexture.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].DownClickFieldColors.ReactionColor;

                    //_header.HeaderStringSlider[objectName].TitleStringBox.TextColor = _header.HeaderStringSliderColors[objectName].TitleTextColors.ReactionColor;
                    //_header.HeaderStringSlider[objectName].StringSliderBox.TextColor = _header.HeaderStringSliderColors[objectName].StringSliderTextColors.ReactionColor;
                    //_header.HeaderStringSlider[objectName].TitleStringBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].TitleBoxColors.ReactionColor;
                    //_header.HeaderStringSlider[objectName].StringSliderBox.BoxBackgroundColor = _header.HeaderStringSliderColors[objectName].StringSliderBoxColors.ReactionColor;

                    if(Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        StringSliderClick(objectName, -1);
                    }
                }
            }
            else if(_header.HeaderButtons.ContainsKey(objectName))
            {
                _header.HeaderButtons[objectName].TextColor = _header.HeaderButtonColors[objectName].SymbolColors.ReactionColor;
                _header.HeaderButtons[objectName].BoxBackgroundColor = _header.HeaderButtonColors[objectName].BoxColors.ReactionColor;

                if(objectName == DrawHeaderObjectNames.RunButton && CheckField.MouseOnField(_header.HeaderButtons[objectName].Box.PixelPosition, _header.HeaderButtons[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
                else if(objectName == DrawHeaderObjectNames.BuildButton && CheckField.MouseOnField(_header.HeaderButtons[objectName].Box.PixelPosition, _header.HeaderButtons[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
            }
            else if(_header.HeaderCenterTextureInBox.ContainsKey(objectName))
            {
                _header.HeaderCenterTextureInBox[objectName].TextureColor = _header.HeaderButtonColors[objectName].SymbolColors.ReactionColor;
                _header.HeaderCenterTextureInBox[objectName].BoxBackgroundColor = _header.HeaderButtonColors[objectName].BoxColors.ReactionColor;
                
                if(objectName == DrawHeaderObjectNames.TerminalRequest && CheckField.MouseOnField(_header.HeaderCenterTextureInBox[objectName].Box.PixelPosition, _header.HeaderCenterTextureInBox[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
                else if(objectName == DrawHeaderObjectNames.StartStopAlgoPlayBack && CheckField.MouseOnField(_header.HeaderCenterTextureInBox[objectName].Box.PixelPosition, _header.HeaderCenterTextureInBox[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
                else if(objectName == DrawHeaderObjectNames.BuildMaze && CheckField.MouseOnField(_header.HeaderCenterTextureInBox[objectName].Box.PixelPosition, _header.HeaderCenterTextureInBox[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
            }
        }
    }

    private void StringSliderClick(DrawHeaderObjectNames objectName, int addRequest)
    {
        if(objectName == DrawHeaderObjectNames.MazeSize)
        {
            
        }
        else if(objectName == DrawHeaderObjectNames.BatchSize)
        {
            
        }
        else if(objectName == DrawHeaderObjectNames.PlayBackSpeed)
        {
            
        }
        else if(objectName == DrawHeaderObjectNames.RunFolderName)
        {
            _runFolder.RunFolderNameHeaderObjectClickAction(addRequest);
        }
    }
}