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

    public void UpdateMainBoxes()
    {
        if(_state.common.boxes.MainBoxPixelLengths != null && _state.common.boxes.MainBoxPixelLengths.Count() > 0)
        {   
            int[] pixelWidths = CalculatePixelLength(
                new float[]{_state.common.boxes.MainBoxProcentageWidths[MainBoxNames.Archive],
                    _state.common.boxes.MainBoxProcentageWidths[MainBoxNames.Maze],
                    _state.common.boxes.MainBoxProcentageWidths[MainBoxNames.Statistics],
                    _state.common.boxes.MainBoxProcentageWidths[MainBoxNames.Editor]}, 
                    Raylib.GetScreenWidth());

            int[] pixelHeights = CalculatePixelLength(
               new float[]{_state.common.boxes.MainBoxProcentageHeights[MainBoxNames.Archive],
                    _state.common.boxes.MainBoxProcentageHeights[MainBoxNames.Maze],
                    _state.common.boxes.MainBoxProcentageHeights[MainBoxNames.Statistics],
                    _state.common.boxes.MainBoxProcentageHeights[MainBoxNames.Editor]}, 
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
        float leftOverPixels = 0;
        int counter = 0;

        foreach(float piece in pieces)
        {
            float pieceLength = (piece/100) * (float)screenLength;
            newPieceLengths[counter] = (int)pieceLength;

            leftOverPixels += pieceLength - (int)pieceLength;

            counter += 1;
        }

        if(screenLength == Raylib.GetScreenHeight())
        {
            for(int r = 0, x = 0; x < leftOverPixels; r++)
            {
                if (pieces[r] < 99f)
                {
                    newPieceLengths[r] += 1;

                    x += 1;
                }
            }
        }
        else
        {
            for(int i = 0; i < leftOverPixels; i++)
            {
                if(newPieceLengths.Length > i && newPieceLengths[i] == newPieceLengths[i + 1])
                {
                    newPieceLengths[i] += 1;
                    newPieceLengths[i + 1] += 1;
                }
                else if(i > 0 && newPieceLengths[i] == newPieceLengths[i - 1])
                {
                    newPieceLengths[i] += 1;
                    newPieceLengths[i - 1] += 1;
                }
                else
                {
                    newPieceLengths[i] += 1;
                }
            }
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