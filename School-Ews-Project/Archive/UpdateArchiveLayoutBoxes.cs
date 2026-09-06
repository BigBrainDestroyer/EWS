using System.Numerics;

namespace Program;

public class UpdateArchiveLayoutBoxes
{
    private readonly AppState _state;

    public UpdateArchiveLayoutBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void ArchiveLayoutBoxes()
    {
        var _layout = _state.archive;
        var _boxes = _state.common.boxes;

        _layout.ArchiveBoxPixelLengths = CalculateArchiveLayoutBoxLengths(
            _layout.ArchiveBoxPixelPercentages,
            _boxes.MainBoxPixelLengts[MainBoxNames.Archive]
            );

        _layout.ArchiveBoxPixelPositions = CalculateArchiveLayoutBoxPositions(
            _layout.ArchiveBoxPixelLengths,
            _boxes.MainBoxPixelLengts[MainBoxNames.Archive],
            _boxes.MainBoxPixelPositions[MainBoxNames.Archive]
        );
    }

    private Dictionary<ArchiveBoxesNames,Vector2> CalculateArchiveLayoutBoxLengths(Dictionary<ArchiveBoxesNames,Vector2> BoxPercentages, Vector2 parentBoxLengths)
    {
        ArchiveBoxesNames[] boxNames = Enum.GetValues<ArchiveBoxesNames>();

        Dictionary<ArchiveBoxesNames, Vector2> newArchiveLayoutBoxLengths = new Dictionary<ArchiveBoxesNames, Vector2>();

        foreach(ArchiveBoxesNames boxName in boxNames)
        {
            newArchiveLayoutBoxLengths.Add(boxName,
                new Vector2(
                    (BoxPercentages[boxName].X/100f) * parentBoxLengths.X,
                    (BoxPercentages[boxName].Y/100f) * parentBoxLengths.Y
                ));
        }

        return newArchiveLayoutBoxLengths;
    }

    private Dictionary<ArchiveBoxesNames,Vector2> CalculateArchiveLayoutBoxPositions(Dictionary<ArchiveBoxesNames,Vector2> boxLengths, Vector2 parentBoxLengths, Vector2 parentBoxPositions)
    {
        ArchiveBoxesNames[] boxNames = Enum.GetValues<ArchiveBoxesNames>();

        Dictionary<ArchiveBoxesNames, Vector2> newArchiveLayoutBoxPositions = new Dictionary<ArchiveBoxesNames, Vector2>();

        float boxIncrement = 0;

        foreach(ArchiveBoxesNames boxName in boxNames)
        {
            newArchiveLayoutBoxPositions.Add(boxName,
                new Vector2(
                parentBoxPositions.X + ((parentBoxLengths.X - boxLengths[boxName].X)/2),
                parentBoxPositions.Y + boxIncrement)
            );

            boxIncrement += boxLengths[boxName].Y;
        }

        return newArchiveLayoutBoxPositions;
    }
}