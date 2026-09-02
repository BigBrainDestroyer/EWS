using System.Numerics;
using Raylib_cs;

namespace Program;

public class DrawArchive
{
    private readonly AppState _state;

    public DrawArchive(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void Archive()
    {
        DrawArchiveBackground();

        DrawArchiveFunctionBar();
    }

    private void DrawArchiveBackground()
    {
        var _boxes = _state.common.boxes;
        var _layout = _state.archive.layout;

        Custom.DrawCustomRectangle(
            _state.common.boxes.MainBoxPixelPositions[MainBoxNames.Archive], 
            _state.common.boxes.MainBoxPixelLengts[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            _layout.BackgroundColor
        );
    }

    private void DrawArchiveFunctionBar()
    {
        var _layout = _state.archive.layout;

        if (_layout.AddDataTypeMenu.IsMenuActive)
        {
            DrawAddDataTypeMenu();
        }
        else
        {
            DrawSearchBar();            
        }

        DrawAddOnButton();
    }

    private void DrawAddDataTypeMenu()
    {
        var _layout = _state.archive.layout;
        var _commonBoxes = _state.common.boxes;

        ArchiveAddDataTypeMenu addDataTypeMenu = _layout.AddDataTypeMenu;

        Custom.DrawCustomRectangleOutlining(
            addDataTypeMenu.BoxPosition,
            addDataTypeMenu.BoxLengths,
            _commonBoxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _commonBoxes.MainBoxesCornerStates[MainBoxNames.Archive],
            0.4f,
            new Color(102, 153, 204),
            new Color(57, 66, 76)
        );

        foreach(ArchiveStorageNames? dataType in addDataTypeMenu.AddLegalDataTypeToStorage)
        {
            if(dataType != null)
            {
                Rectangle textureRectangleSize = 
                    new Rectangle(
                        0, 
                        0, 
                        _layout.ArchiveTextures[(int)dataType].Width, 
                        _layout.ArchiveTextures[(int)dataType].Height
                    );
        
                Rectangle rectangleDrawSize = 
                    new Rectangle(
                        addDataTypeMenu.Texture2DPosition.X, 
                        addDataTypeMenu.Texture2DPosition.Y, 
                        addDataTypeMenu.Texture2DSize.X, 
                        addDataTypeMenu.Texture2DSize.Y
                    );
                
                Raylib.DrawTexturePro(
                    _layout.ArchiveTextures[(int)dataType],
                    textureRectangleSize,
                    rectangleDrawSize,
                    new Vector2(0, 0),
                    0f,
                    new Color(216, 222, 233)
                );

                addDataTypeMenu.Texture2DPosition.Y += addDataTypeMenu.SingleBoxLengths.Y;
            }
        }
    }

    private void DrawSearchBar()
    {
        var _layout = _state.archive.layout;

        Custom.DrawCustomRectangleOutlining(
            _layout.SearchBarPixelPosition,
            _layout.SearchBarPixelLengths,
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            0.4f,
            new Color(102, 153, 204),
            new Color(57, 66, 76)
        );
    }

    private void DrawAddOnButton()
    {
        var _layout = _state.archive.layout;
        
        Custom.DrawCustomRectangleOutlining(
            _layout.AddOnButtonPixelPosition,
            _layout.AddOnButtonPixelLengths,
            _state.common.boxes.MainBoxesCornerRadius[MainBoxNames.Archive],
            _state.common.boxes.MainBoxesCornerStates[MainBoxNames.Archive],
            0.6f,
            new Color(102, 153, 204),
            new Color(57, 66, 76)
        );

        //test
        
        Rectangle sourceRectangle = 
            new Rectangle(
                0, 
                0, 
                _layout.ArchiveTextures[(int)ArchiveTexture2DNames.PlusSign].Width, 
                _layout.ArchiveTextures[(int)ArchiveTexture2DNames.PlusSign].Height
            );

        Rectangle rectangleDrawSize = 
            new Rectangle(
                _layout.AddOnButtonPixelPosition.X, 
                _layout.AddOnButtonPixelPosition.Y, 
                _layout.AddOnButtonPixelLengths.X, 
                _layout.AddOnButtonPixelLengths.Y
            );

        Raylib.DrawTexturePro(
            _layout.ArchiveTextures[(int)ArchiveTexture2DNames.PlusSign], 
            sourceRectangle,
            rectangleDrawSize,
            new Vector2(0, 0),
            0f,
            new Color(216, 222, 233)
        );
    }
}