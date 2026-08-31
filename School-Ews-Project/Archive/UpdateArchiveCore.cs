using System.Numerics;
using Raylib_cs;

namespace Program;

public class UpdateArchiveCore
{
    private readonly AppState _state;

    public UpdateArchiveCore(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ArchiveCore()
    {
        AddOnButtonClick();
    }

    private void AddOnButtonClick()
    {
        var _layout = _state.archive.layout;

        if(CheckField.MouseOnField(_layout.AddOnButtonPixelPosition, _layout.AddOnButtonPixelLengths, Raylib.GetMousePosition()) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Console.WriteLine("WERKT GOED");
        }
    }
}