using System.Numerics;

namespace Program;

public class UpdateEditorLayoutBoxes
{
    private readonly AppState _state;

    public UpdateEditorLayoutBoxes(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void EditorLayoutBoxes()
    {
        var _editorBoxes = _state.editor.layout.boxes;
        var _commonBoxes = _state.common.boxes;

        EditorBoxNames[] boxNames = Enum.GetValues<EditorBoxNames>();
        double leftOverPixels = 0;
        
        foreach(EditorBoxNames boxName in boxNames)
        {
            (int boxPixelHeight, leftOverPixels) = 
                CalculatePixelHeightLength(
                    _editorBoxes.BoxPercentageHeights[boxName], 
                    (int)_commonBoxes.MainBoxPixelLengths[MainBoxNames.Editor].Y,
                    leftOverPixels
                );

            _editorBoxes.BoxPixelLengths[boxName] = 
                new Vector2(
                    _commonBoxes.MainBoxPixelLengths[MainBoxNames.Editor].X,
                    boxPixelHeight
                );
        }

        leftOverPixels = Math.Round(leftOverPixels);

        foreach(EditorBoxNames boxName in boxNames)
        {
            if(leftOverPixels > 0)
            {
                _editorBoxes.BoxPixelLengths[boxName] = 
                    new Vector2(
                        _editorBoxes.BoxPixelLengths[boxName].X, 
                        _editorBoxes.BoxPixelLengths[boxName].Y + 1
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

    
    private (int, double) CalculatePixelHeightLength(float childBoxPercentageHeight, int parentBoxHeight, double leftOverPixels)
    {
        float childBoxPixelHeight = (childBoxPercentageHeight / 100f) * (float)parentBoxHeight;
        leftOverPixels += childBoxPixelHeight - (int)childBoxPixelHeight;

        return ((int)childBoxPixelHeight, leftOverPixels);
    }

    
    private void CalculatePosition(EditorBoxNames[] boxNames)
    {
        int boxHeightPixel = 0;

        foreach(EditorBoxNames boxName in boxNames)
        {
            _state.editor.layout.boxes.BoxPixelPositions[boxName] = 
                new Vector2(
                    _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Editor].X,
                    boxHeightPixel
                );

            boxHeightPixel += (int)_state.editor.layout.boxes.BoxPixelLengths[boxName].Y; 
        }
    }
}