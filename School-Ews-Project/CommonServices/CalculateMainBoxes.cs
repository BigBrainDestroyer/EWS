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

    public void  updateMainBoxes()
    {
        var _mainBoxes = _state.common.boxes;

        _mainBoxes.HeaderPixelLengths =
            CalculatingHeaderPixelLengths(_mainBoxes.HeaderPercentages);

        _mainBoxes.MainColumnslengths =
            CalculateMainColumnLengths(_mainBoxes.MainColumnsPercentage, _mainBoxes.HeaderPixelLengths, _mainBoxes.MainBoxesSpacing);
    }

    private Vector2 CalculatingHeaderPixelLengths(Vector2 HeaderPercentages)
    {
        Vector2 newHeaderPixelLengths =
            new Vector2(
                MathF.Round(Raylib.GetScreenWidth()  * (HeaderPercentages.X / 100f)),
                MathF.Round(Raylib.GetScreenHeight() * (HeaderPercentages.Y / 100f))
            );

        return newHeaderPixelLengths;
    }

    private Dictionary<MainColumnsName, Vector2> CalculateMainColumnLengths(Dictionary<MainColumnsName, Vector2> mainColumnsPercentage, Vector2 headerPixelLengths, float mainBoxesSpacing)
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

        return newMainColumnLengths;
    }
    
    public void UpdateMainBoxes()
    {
        if(_state.common.boxes.MainBoxPixelLengths != null && _state.common.boxes.MainBoxPixelLengths.Count() > 0)
        {   
            int[] pixelWidths = CalculatePixelLength(
                new float[]{_state.common.boxes.MainBoxPercentageWidths[MainBoxNames.Archive],
                    _state.common.boxes.MainBoxPercentageWidths[MainBoxNames.Maze],
                    _state.common.boxes.MainBoxPercentageWidths[MainBoxNames.Statistics],
                    _state.common.boxes.MainBoxPercentageWidths[MainBoxNames.Editor]}, 
                    Raylib.GetScreenWidth());

            int[] pixelHeights = CalculatePixelLength(
               new float[]{_state.common.boxes.MainBoxPercentageHeights[MainBoxNames.Archive],
                    _state.common.boxes.MainBoxPercentageHeights[MainBoxNames.Maze],
                    _state.common.boxes.MainBoxPercentageHeights[MainBoxNames.Statistics],
                    _state.common.boxes.MainBoxPercentageHeights[MainBoxNames.Editor]}, 
                    Raylib.GetScreenHeight());
            
            if (pixelHeights != null && pixelWidths != null && pixelHeights.Length > 0 && pixelWidths.Length > 0)
            {
                MainBoxNames[] boxName = (MainBoxNames[])Enum.GetValues(typeof(MainBoxNames));

                for (int i = 0; i < boxName.Length; i++)
                {
                    _state.common.boxes.MainBoxPixelLengths[boxName[i]] = new Vector2(pixelWidths[i], pixelHeights[i]);
                }
                
                CalculateMainBoxPositions();
            }
        }       
    }

    private int[] CalculatePixelLength(float[] pieces, int screenLength)
    {
        if(pieces == null || pieces.Length == 0)
        {
            return new int[0];
        }
        
        int[] newPieceLengths = new int[pieces.Length];
        double leftOverPixels = 0;
        int counter = 0;

        foreach(float piece in pieces)
        {
            float pieceLength = (piece/100) * (float)screenLength;
            newPieceLengths[counter] = (int)pieceLength;

            leftOverPixels += pieceLength - (int)pieceLength;

            counter += 1;
        }

        counter = 0;
        
        leftOverPixels = Math.Ceiling(leftOverPixels);

        if(leftOverPixels == 2)
        {
            leftOverPixels = 3;
        }

        foreach(float piece in pieces)
        {
            if(piece < 100f && leftOverPixels > 0)
            {
                newPieceLengths[counter] += 1;
                leftOverPixels -= 1;
            }

            counter += 1;
        }

        return newPieceLengths;
    }

    private void CalculateMainBoxPositions()
    {
        _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Archive] = 
        new Vector2(
            0, 
            0
        );

        _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Maze] = 
        new Vector2(
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Archive].X, 
            0
        );

        _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Statistics] = 
        new Vector2(
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Archive].X, 
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Maze].Y
        );

        _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Editor] =
        new Vector2(
            _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Archive].X + _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Maze].X, 
            0
        );
    }
}