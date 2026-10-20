
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
        
        if(double.Parse(_header.HeaderStringSlider[DrawHeaderObjectNames.PlayBackSpeed].StringSliderBox.Text.Text) != _play.PlaySpeed)
        {
            _header.HeaderStringSlider[DrawHeaderObjectNames.PlayBackSpeed].StringSliderBox.Text = 
                new RaylibText(
                    $"{_play.PlaySpeed:0.######}", 
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

        if(_play.PlaySpeed <= 0.000001 && addRequest < 0)
        {
            return;
        }

        if (addRequest > 0)
        {
            if(_play.PlaySpeed < 0.000001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 10000000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed < 0.00001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 1000000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed < 0.0001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 100000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed < 0.001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 10000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed < 0.01)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 1000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 0.1)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 100, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 0.5)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 40, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 1)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 20, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed < 10)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 8, 6);
                return;
            }
            else if (_play.PlaySpeed < 20)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest, 8);
                return;
            }
            else
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest * 5, 8);
                return;
            }
        }
        else
        {
            if(_play.PlaySpeed <= 0.000001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 10000000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed <= 0.00001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 1000000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed <= 0.0001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 100000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed <= 0.001)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 10000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if(_play.PlaySpeed <= 0.01)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 1000, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 0.1)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 100, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 0.5)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 40, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 1)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 20, 8, MidpointRounding.AwayFromZero);
                return;
            }
            else if (_play.PlaySpeed <= 10)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest / 2, 8);
                return;
            }
            else if (_play.PlaySpeed <= 20)
            {
                _play.PlaySpeed = Math.Round(_play.PlaySpeed + (Double)addRequest, 8);
                return;
            }
            else
            {
                _play.PlaySpeed += Math.Round((Double)addRequest * 5, 8);
                return;
            }
        }
    }
}