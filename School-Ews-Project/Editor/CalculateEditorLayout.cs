using System.Numerics;

namespace Program;

public class CalculateEditorLayout
{
    private readonly AppState _state;
    
    public CalculateEditorLayout(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void UpdateEditorLayout()
    {
        UpdateEditorLayoutBoxes();
    }

    public void UpdateEditorLayoutBoxes()
    {
        EditorBoxNames[] boxNames = Enum.GetValues<EditorBoxNames>();
        double leftOverPixels = 0;
        
        foreach(EditorBoxNames boxName in boxNames)
        {
            (int boxPixelHeight, leftOverPixels) = 
                CalculatePixelHeightLength(
                    _state.editor.layout.boxes.BoxPercentageHeights[boxName], 
                    (int)_state.common.boxes.MainBoxPixelLengths[MainBoxNames.Editor].Y,
                    leftOverPixels
                );

            _state.editor.layout.boxes.BoxPixelLengths[boxName] = 
                new Vector2(
                    _state.common.boxes.MainBoxPixelLengths[MainBoxNames.Editor].X,
                    boxPixelHeight
                );
        }

        leftOverPixels = Math.Round(leftOverPixels);

        foreach(EditorBoxNames boxName in boxNames)
        {
            if(leftOverPixels > 0)
            {
                _state.editor.layout.boxes.BoxPixelLengths[boxName] = 
                    new Vector2(
                        _state.editor.layout.boxes.BoxPixelLengths[boxName].X, 
                        _state.editor.layout.boxes.BoxPixelLengths[boxName].Y + 1
                    );
                
                leftOverPixels -= 1;
            }
            else
            {
                break;
            }
        }

        CalculatePosition(boxNames);
    }

    public (int, double) CalculatePixelHeightLength(float childBoxPercentageHeight, int parentBoxHeight, double leftOverPixels)
    {
        float childBoxPixelHeight = (childBoxPercentageHeight / 100f) * (float)parentBoxHeight;
        leftOverPixels += childBoxPixelHeight - (int)childBoxPixelHeight;

        return ((int)childBoxPixelHeight, leftOverPixels);
    }

    public void CalculatePosition(EditorBoxNames[] boxNames)
    {
        int boxHeightPixel = 0;

        foreach(EditorBoxNames boxName in boxNames)
        {
            _state.editor.layout.boxes.BoxPixelPositions[boxName] = new Vector2(_state.common.boxes.MainBoxPixelPositions[MainBoxNames.Editor].X, boxHeightPixel);

            boxHeightPixel += (int)_state.editor.layout.boxes.BoxPixelLengths[boxName].Y; 
        }
    }
}