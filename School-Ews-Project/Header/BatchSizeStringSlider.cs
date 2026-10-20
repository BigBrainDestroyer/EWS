
namespace Program;

public class BatchSizeStringSlider
{
    private readonly AppState _state;

    public BatchSizeStringSlider(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void StringSliderCore()
    {
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;
        
        if(int.Parse(_header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.Text.Text) != _generator.MazeBatchSize)
        {
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.Text = 
                new RaylibText(
                    $"{_generator.MazeBatchSize}", 
                    _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.Text.TextSize,
                    _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.Text.TextSpacing
                );
        }

        UpdateStringSliderAvailability();
    }

    private void UpdateStringSliderAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        if (_play.IsAlgoProgramPaused && !_play.IsAlgoRunning && !_generator.BuildingMazes && !_generator.MazesAreDone)
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BatchSize] = true;
        }
        else if(_generator.BuildingMazes)
        {   
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].TitleStringBox.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].TitleBoxColors.X;                
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.TextColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].TitleTextColors.X;

            
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].StringSliderBoxColors.X;        
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].StringSliderBox.TextColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].StringSliderTextColors.X;

                
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].UpTexture.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].UpClickFieldColors.Z;
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].UpTexture.TextureColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].TextureUpColors.Z;

                
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].DownTexture.BoxBackgroundColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].DownClickFieldColors.Z;                
            _header.HeaderStringSlider[DrawHeaderObjectNames.BatchSize].DownTexture.TextureColor =
                _header.HeaderStringSliderColors[DrawHeaderObjectNames.BatchSize].TextureDownColors.Z;
            
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BatchSize] = false;
        }
        else
        {
            _header.HeaderObjectAvailable[DrawHeaderObjectNames.BatchSize] = false;
        }
    }

    public void StringSliderClick(int addRequest)
    {
        var _generator = _state.maze.generator;
        
        if(_generator.MazeBatchSize > 1)
        {
            _generator.MazeBatchSize += addRequest;
        }
        else if(addRequest > 0)
        {
            _generator.MazeBatchSize += addRequest;
        }
    }
}