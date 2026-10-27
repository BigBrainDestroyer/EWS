using Raylib_cs;
using System.Numerics;

namespace Program;

public class CalculateMainBoxes
{
    private readonly AppState _state;

    public CalculateMainBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    } 

    public void  UpdateMainBoxes()
    {
        var _mainBoxes = _state.common.boxes;

        _mainBoxes.HeaderPixelLengths =
            CalculateHeaderPixelLengths(_mainBoxes.HeaderPercentages);

        (_mainBoxes.MainColumnsPixelLengths, _mainBoxes.MainBoxesPixelSpacing) =
            CalculateMainColumnLengths(_mainBoxes.MainColumnsPercentage, _mainBoxes.HeaderPixelLengths, _mainBoxes.MainBoxesPercentageSpacing);

        _mainBoxes.MainBoxPixelLengts =
            CalculateMainBoxLengths(_mainBoxes.MainColumnsPixelLengths, _mainBoxes.MainBoxesPixelSpacing);

        _mainBoxes.MainBoxPixelPositions =
            CalculateMainBoxPositions(_mainBoxes.MainBoxPixelLengts, _mainBoxes.HeaderPixelLengths, _mainBoxes.MainBoxesPixelSpacing);
    }

    private Vector2 CalculateHeaderPixelLengths(Vector2 HeaderPercentages)
    {
        Vector2 newHeaderPixelLengths =
            new Vector2(
                MathF.Round(Raylib.GetScreenWidth()  * (HeaderPercentages.X / 100f)),
                MathF.Round(Raylib.GetScreenHeight() * (HeaderPercentages.Y / 100f))
            );

        return newHeaderPixelLengths;
    }

    private (Dictionary<MainColumnsName, Vector2>, float)CalculateMainColumnLengths(Dictionary<MainColumnsName, Vector2> mainColumnsPercentage, Vector2 headerPixelLengths, float mainBoxesSpacing)
    {
        MainColumnsName[] columnNames = Enum.GetValues<MainColumnsName>();

        Dictionary<MainColumnsName, Vector2> newMainColumnLengths = new Dictionary<MainColumnsName, Vector2>();

        float mainBoxPixelSpacing = MathF.Round((Raylib.GetScreenHeight() - headerPixelLengths.Y) * (mainBoxesSpacing/100f));

        Vector2 leftOverWindowPixelLengths = 
            new Vector2(
                Raylib.GetScreenWidth() - (mainBoxPixelSpacing * mainColumnsPercentage.Count),
                (Raylib.GetScreenHeight() - headerPixelLengths.Y) - (mainBoxPixelSpacing * 2) 
            );

        foreach(MainColumnsName columnName in columnNames)
        {
            newMainColumnLengths.Add(
                columnName, 
                new Vector2(
                    leftOverWindowPixelLengths.X * (mainColumnsPercentage[columnName].X/100f),
                    leftOverWindowPixelLengths.Y * (mainColumnsPercentage[columnName].Y/100f)
                )
            );
        }

        return (newMainColumnLengths, mainBoxPixelSpacing);
    }

    private Dictionary<MainBoxNames, Vector2> CalculateMainBoxLengths(Dictionary<MainColumnsName, Vector2> mainColumnsPixelLengths, float mainBoxPixelSpacing)
    {
        Dictionary<MainBoxNames, Vector2> newMainBoxPixelLengths = new Dictionary<MainBoxNames, Vector2>();

        newMainBoxPixelLengths.Add(MainBoxNames.Archive, mainColumnsPixelLengths[MainColumnsName.Archive]);
        newMainBoxPixelLengths.Add(MainBoxNames.Maze, mainColumnsPixelLengths[MainColumnsName.Visualize]);
        newMainBoxPixelLengths.Add(MainBoxNames.Editor, new Vector2(mainColumnsPixelLengths[MainColumnsName.Editor].X - mainBoxPixelSpacing, mainColumnsPixelLengths[MainColumnsName.Editor].Y));
        
        return newMainBoxPixelLengths;
    }

    private Dictionary<MainBoxNames, Vector2> CalculateMainBoxPositions(Dictionary<MainBoxNames, Vector2> mainBoxPixelLengths, Vector2 headerPixelLengths, float mainBoxPixelSpacing)
    {
        Dictionary<MainBoxNames, Vector2> newMainBoxPixelPositions= new Dictionary<MainBoxNames, Vector2>();

        newMainBoxPixelPositions.Add(MainBoxNames.Archive,
            new Vector2(
                mainBoxPixelSpacing,
                headerPixelLengths.Y + mainBoxPixelSpacing
            ));

        newMainBoxPixelPositions.Add(MainBoxNames.Maze,
            new Vector2(
                newMainBoxPixelPositions[MainBoxNames.Archive].X + mainBoxPixelLengths[MainBoxNames.Archive].X + mainBoxPixelSpacing,
                headerPixelLengths.Y + mainBoxPixelSpacing
            ));

        newMainBoxPixelPositions.Add(MainBoxNames.Editor,
            new Vector2(
                newMainBoxPixelPositions[MainBoxNames.Maze].X + mainBoxPixelLengths[MainBoxNames.Maze].X + mainBoxPixelSpacing,
                headerPixelLengths.Y + mainBoxPixelSpacing
            ));
        
        return newMainBoxPixelPositions;
    }
}