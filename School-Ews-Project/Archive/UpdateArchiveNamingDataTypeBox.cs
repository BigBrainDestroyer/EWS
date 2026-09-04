using Raylib_cs;
using System.Numerics;

namespace Program;

public class UpdateArchiveNamingDataTypeBox
{
    private readonly AppState _state;

    public UpdateArchiveNamingDataTypeBox(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void UpdateNamingDataTypeBoxLayout()
    {
        var _layout = _state.archive.layout;
        var _core = _state.archive.core;
        var _input = _state.common.input;

        ArchiveNamingDataTypeBox newArchiveNamingDataTypeBox = new ArchiveNamingDataTypeBox();

        newArchiveNamingDataTypeBox.BoxPosition =
            CalculateBoxPosition(_layout.ArchiveBoxPixelPositions[ArchiveBoxesNames.Edit]);

        newArchiveNamingDataTypeBox.BoxLengths =
            CalculateBoxLengths(_layout.ArchiveBoxPixelLengths[ArchiveBoxesNames.Edit]);

        newArchiveNamingDataTypeBox.TextureSize = 
            CalculateTextureSize(newArchiveNamingDataTypeBox.BoxLengths);

        newArchiveNamingDataTypeBox.TextPosition = 
            CalculateTextPosition(newArchiveNamingDataTypeBox.BoxPosition, newArchiveNamingDataTypeBox.TextureSize, newArchiveNamingDataTypeBox.TextWhiteSpacing);
            
        newArchiveNamingDataTypeBox.TextSize =
            CalculateFontSize(newArchiveNamingDataTypeBox.BoxLengths);    
    }

    private Vector2 CalculateBoxPosition(Vector2 editBoxPosition)
    {
        return editBoxPosition;
    }

    private Vector2 CalculateBoxLengths(Vector2 editBoxLengths)
    {
        return editBoxLengths;
    }

    private float CalculateFontSize(Vector2 boxLengths)
    {
        return boxLengths.Y;
    }

    private Vector2 CalculateTextureSize(Vector2 boxLengths)
    {
        return (boxLengths.X < boxLengths.Y) ? new Vector2(boxLengths.X, boxLengths.X) : new Vector2(boxLengths.Y, boxLengths.Y);
    }

    private Vector2 CalculateTextPosition(Vector2 boxPosition, Vector2 textureSize, float textWhiteSpacing)
    {
        return new Vector2(boxPosition.X + (textureSize.X * (textWhiteSpacing/100f)), boxPosition.Y);
    }
    
    public ArchiveNamingDataTypeBox GetNewNamingDataTypeBox(Texture2D[] archiveTextures, ArchiveNamingDataTypeBox currentArchiveNamingDataTypeBox, ArchiveStorageNames dataType)
    {
        ArchiveNamingDataTypeBox newArchiveNamingDataTypeBox = new ArchiveNamingDataTypeBox();

        newArchiveNamingDataTypeBox.Texture = archiveTextures[(int)dataType];

        newArchiveNamingDataTypeBox.IsBoxActive = !currentArchiveNamingDataTypeBox.IsBoxActive;

        return newArchiveNamingDataTypeBox;
    }
}