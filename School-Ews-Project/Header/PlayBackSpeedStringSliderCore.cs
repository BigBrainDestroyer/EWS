
namespace Program;

public class PlayBackSpeedStringSliderCore
{
    private readonly AppState _state;

    public PlayBackSpeedStringSliderCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void StringSliderCore()
    {
        var _play = _state.playMaze;
        var _header = _state.HeaderState;
        
        if(float.Parse(_header.HeaderStringSlider[DrawHeaderObjectNames.PlayBackSpeed].StringSliderBox.Text.Text) != _play.PlaySpeed)
        {
            _header.HeaderStringSlider[DrawHeaderObjectNames.PlayBackSpeed].StringSliderBox.Text = 
                new RaylibText(
                    $"{_play.PlaySpeed}", 
                    _header.HeaderStringSlider[DrawHeaderObjectNames.PlayBackSpeed].StringSliderBox.Text.TextSize,
                    _header.HeaderStringSlider[DrawHeaderObjectNames.PlayBackSpeed].StringSliderBox.Text.TextSpacing
                );
        }

        UpdateStringSliderAvailability();
    }

    private void UpdateStringSliderAvailability()
    {
        var _play = _state.playMaze;
        var _generator = _state.maze.generator;
        var _header = _state.HeaderState;

        _header.HeaderObjectAvailable[DrawHeaderObjectNames.PlayBackSpeed] = true;
    }

    public void StringSliderClick(int addRequest)
    {
        var _play = _state.playMaze;

        if(_play.PlaySpeed <= 0.0001 && addRequest < 0)
        {
            return;
        }

        if (addRequest > 0)
        {
            if(_play.PlaySpeed < 0.001f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 10000, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed < 0.01f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 1000, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 0.1f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 100, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 0.5f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 40, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 1)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 20, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 10)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 2, 5);
                return;
            }
            else if (_play.PlaySpeed < 20)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest, 5);
                return;
            }
            else
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest * 5, 5);
                return;
            }
        }
        else
        {
            if(_play.PlaySpeed <= 0.001f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 10000, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed <= 0.01f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 1000, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 0.1f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 100, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 0.5f)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 40, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 1)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 20, 5, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 10)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest / 2, 5);
                return;
            }
            else if (_play.PlaySpeed <= 20)
            {
                _play.PlaySpeed = MathF.Round(_play.PlaySpeed + (float)addRequest, 5);
                return;
            }
            else
            {
                _play.PlaySpeed += MathF.Round((float)addRequest * 5, 5);
                return;
            }
        }
    }
}