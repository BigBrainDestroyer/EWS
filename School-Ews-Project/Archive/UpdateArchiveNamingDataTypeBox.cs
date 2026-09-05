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

       
        
    }
    
    public TextBar GetNewNamingDataTypeBox(Texture2D[] archiveTextures, ArchiveStorageNames dataType)
    {
        TextBar newTextBar = new TextBar();

        newTextBar.Texture = archiveTextures[(int)dataType];

        newTextBar.IsTextBoxActive = true;

        return newTextBar;
    }
}