using System.Numerics;
using Raylib_cs;

namespace Program;

public class UpdateActiveMainBox
{
    private readonly AppState _state;

    public UpdateActiveMainBox(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ActiveMainBox()
    {
        var _commonBoxes = _state.common.boxes;

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsMouseButtonPressed(MouseButton.Right) || Raylib.IsMouseButtonPressed(MouseButton.Middle))
        {
            MainBoxNames[] mainBoxNames = Enum.GetValues<MainBoxNames>();

            Vector2 currentMousePosition = Raylib.GetMousePosition();

            foreach(MainBoxNames mainBox in mainBoxNames)
            {
                if(CheckField.MouseOnField( _commonBoxes.MainBoxPixelPositions[mainBox], _commonBoxes.MainBoxPixelLengts[mainBox], Raylib.GetMousePosition()))
                {
                    _commonBoxes.ActiveMainBox = mainBox;
                    break;
                }
            }
        }
    }
}