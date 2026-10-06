using Raylib_cs;

namespace Program;

public class HeaderClick
{
    private readonly AppState _state;

    public HeaderClick(AppState state)
    {
        _state = state ?? throw new ArgumentNullException();
    }

    public void Click()
    {
        CheckAllHeaderButtons();
    }

    public void CheckAllHeaderButtons()
    {
        var _boxes = _state.common.boxes;
        var _header = _state.HeaderState;

        if(!CheckField.MouseOnField(_boxes.HeaderPixelPosition, _boxes.HeaderPixelLengths, Raylib.GetMousePosition())) return;

        foreach(DrawHeaderObjectNames objectName in _header.HeaderObjectBoxes.Keys)
        {
            if(!CheckField.MouseOnField(_header.HeaderObjectBoxes[objectName].PixelPosition, _header.HeaderObjectBoxes[objectName].PixelLengths, Raylib.GetMousePosition())) continue;

            if(_header.HeaderStringSlider.ContainsKey(objectName))
            {
                if(CheckField.MouseOnField(_header.HeaderStringSlider[objectName].CounterUpClickField.PixelPosition, _header.HeaderStringSlider[objectName].CounterUpClickField.PixelLengths, Raylib.GetMousePosition()))
                {
                    // IS OP UP ARROW
                }
                else if(CheckField.MouseOnField(_header.HeaderStringSlider[objectName].CounterDownClickField.PixelPosition, _header.HeaderStringSlider[objectName].CounterDownClickField.PixelLengths, Raylib.GetMousePosition()))
                {
                    // IS OP ARROW DOWN
                }
            }
            else
            {
                if(objectName == DrawHeaderObjectNames.RunButton && CheckField.MouseOnField(_header.HeaderButtons[objectName].Box.PixelPosition, _header.HeaderButtons[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
                else if(objectName == DrawHeaderObjectNames.BuildButton && CheckField.MouseOnField(_header.HeaderButtons[objectName].Box.PixelPosition, _header.HeaderButtons[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
                {
                    
                }
                else if(objectName == DrawHeaderObjectNames.TerminalRequest && CheckField.MouseOnField(_header.HeaderCenterTextureInBox[objectName].Box.PixelPosition, _header.HeaderCenterTextureInBox[objectName].Box.PixelLengths, Raylib.GetMousePosition()))
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
}