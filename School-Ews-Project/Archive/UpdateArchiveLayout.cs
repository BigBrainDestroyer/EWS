using System.Numerics;

namespace Program;

public class UpdateArchiveLayout
{
    private readonly AppState _state;
    private readonly UpdateFunctionBox _functionBox;
    private readonly UpdateArchiveLayoutBoxes _archiveBoxes;

    public UpdateArchiveLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
        _functionBox = new UpdateFunctionBox(_state);
        _archiveBoxes = new UpdateArchiveLayoutBoxes(_state);
    }

    public void UpdateArchive()
    {
        _archiveBoxes.ArchiveLayoutBoxes();

        _functionBox.FunctionBox();
    }
}