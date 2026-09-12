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

        //DrawArchiveDataTypeBoxes();

        DrawArchiveNamingDataTypeTextBox();
    }

    private void DrawArchiveBackground()
    {
        var _boxes = _state.common.boxes;
        var _layout = _state.archive;

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
        var _layout = _state.archive;

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

    //private void DrawArchiveDataTypeBoxes()
    //{
    //    var _archive = _state.archive;
    //
    //    float positionIncrementY = 0;
    //
    //    foreach(ArchiveDataTypeBox dataTypeBox in _archive.DataTypeBoxes)
    //    {
    //        
    //    }
    //}

    private void DrawAddDataTypeMenu()
    {
        var _layout = _state.archive;
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
        var _layout = _state.archive;

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
        var _layout = _state.archive;
        
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

    private void DrawArchiveNamingDataTypeTextBox()
    {
        var _layout = _state.archive;
        var _commonBoxes = _state.common.boxes;

        if(_layout.ArchiveNamingDataTypeTextBox.IsTextBoxActive)
        {
            Custom.DrawCustomRectangle(
                _layout.ArchiveNamingDataTypeTextBox.BoxPosition,
                _layout.ArchiveNamingDataTypeTextBox.BoxLengths,
                _commonBoxes.MainBoxesCornerRadius[MainBoxNames.Archive],
                _layout.ArchiveNamingDataTypeTextBox.CornerStates,
                _layout.BackgroundColor
            );

            Raylib.DrawTextEx(
                _state.common.font.EditorPanelText,
                _layout.ArchiveNamingDataTypeTextBox.ViewText,
                _layout.ArchiveNamingDataTypeTextBox.ViewTextPixelPosition,
                _layout.ArchiveNamingDataTypeTextBox.BoxLengths.Y,
                _layout.ArchiveNamingDataTypeTextBox.CharSpacing,
                Color.RayWhite
            );

            Rectangle sourceRectangle = 
                new Rectangle(
                    0, 
                    0, 
                    _layout.ArchiveNamingDataTypeTextBox.Texture.Width, 
                    _layout.ArchiveNamingDataTypeTextBox.Texture.Height
                );
    
            Rectangle rectangleDrawSize = 
                new Rectangle(
                    _layout.ArchiveNamingDataTypeTextBox.BoxPosition.X, 
                    _layout.ArchiveNamingDataTypeTextBox.BoxPosition.Y, 
                    _layout.ArchiveNamingDataTypeTextBox.TextureSize.X, 
                    _layout.ArchiveNamingDataTypeTextBox.TextureSize.Y
                );
    
            Raylib.DrawTexturePro(
                _layout.ArchiveNamingDataTypeTextBox.Texture, 
                sourceRectangle,
                rectangleDrawSize,
                new Vector2(0, 0),
                0f,
                new Color(216, 222, 233)
            );
        }
    }
}