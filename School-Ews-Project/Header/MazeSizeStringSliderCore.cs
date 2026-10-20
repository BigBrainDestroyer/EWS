
namespace Program;

public class MazeSizeStringSliderCore
{
    private readonly AppState _state;

    public MazeSizeStringSliderCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void StringSliderCore()
    {
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;
        
        if(int.Parse(_header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text.Text) != _generator.MazeSize - 1)
        {
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text = 
                new RaylibText(
                    $"{_generator.MazeSize - 1}", 
                    _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text.TextSize,
                    _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.Text.TextSpacing
                );
        }

        StringSliderAvailability();
    }
    
    public void StringSliderAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        if (_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.MazeSize] = true;
        }
        else if(_generator.BuildingMazes)
        {   
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].TitleStringBox.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].TitleBoxColors.X;                
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.TextColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].TitleTextColors.X;

            
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].StringSliderBoxColors.X;        
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].StringSliderBox.TextColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].StringSliderTextColors.X;

                
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].UpTexture.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].UpClickFieldColors.Z;
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].UpTexture.TextureColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].TextureUpColors.Z;

                
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].DownTexture.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].DownClickFieldColors.Z;                
            _header.HeaderStringSlider[DrawHeaderObjectNames.MazeSize].DownTexture.TextureColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.MazeSize].TextureDownColors.Z;
            
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.MazeSize] = false;
        }
        else
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.MazeSize] = false;
        }
    }

    public void StringSliderClick(int addRequest)
    {
        var _generator = _state.maze.generator;
        
        if(_generator.MazeSize - 1 > 2)
        {
            _generator.MazeSize += addRequest;
        }
        else if(addRequest > 0)
        {
            _generator.MazeSize += addRequest;
        }
    }
}