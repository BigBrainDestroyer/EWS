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

        DrawArchiveDataTypeBoxes();
        
        DrawArchiveFunctionBar();

        DrawArchiveEditDataTypeBox();
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

    private void DrawArchiveDataTypeBoxes()
    {
        var _archive = _state.archive;
        var _font = _state.common.font;

        foreach(ArchiveDataTypeBox dataTypeBox in _archive.DataTypeBoxes)
        {   
            Raylib.DrawRectangleV(
                dataTypeBox.Box.PixelPosition,
                dataTypeBox.Box.PixelLengths,
                dataTypeBox.BoxColor
            );

            Raylib.DrawTexturePro(
                dataTypeBox.Texture.Texture,
                dataTypeBox.Texture.TextureUsage,
                dataTypeBox.Texture.TextureSize,
                new Vector2(0,0),
                0f,
                dataTypeBox.TextureColor
            );

            //Console.WriteLine(dataTypeBox.ViewText);

            Raylib.DrawTextEx(
                _font.EditorPanelRowNumbers,
                dataTypeBox.ViewText,
                new Vector2(
                    dataTypeBox.Texture.TextureSize.Position.X + dataTypeBox.Texture.TextureSize.Width, 
                    dataTypeBox.Texture.TextureSize.Position.Y
                ),
                dataTypeBox.Text.TextSize,
                dataTypeBox.Text.TextSpacing,
                dataTypeBox.TextColor
            );
        }
    }

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

    private void DrawArchiveEditDataTypeBox()
    {
        DrawEditDataTypeBox();

        DrawArchiveNamingDataTypeTextBox();
    }

    private void DrawEditDataTypeBox()
    {
        var _archive = _state.archive;
        
        if(!_archive.ArchiveEditDataType.IsEditBoxActive) return;

        Custom.DrawCustomRectangle(
            _archive.ArchiveEditDataType.DeleteClickField.PixelPosition,
            _archive.ArchiveEditDataType.DeleteClickField.PixelLengths,
            _archive.ArchiveEditDataType.DeleteClickField.Radius,
            _archive.ArchiveEditDataType.DeleteClickField.CornerStates,
            _archive.ArchiveEditDataType.DeleteBoxColor
        );

        Custom.DrawCustomRectangle(
            _archive.ArchiveEditDataType.RenameClickField.PixelPosition,
            _archive.ArchiveEditDataType.RenameClickField.PixelLengths,
            _archive.ArchiveEditDataType.RenameClickField.Radius,
            _archive.ArchiveEditDataType.RenameClickField.CornerStates,
            _archive.ArchiveEditDataType.RenameBoxColor
        );

        Raylib.DrawTextEx(
            _archive.ArchiveEditDataType.TextFont,
            _archive.ArchiveEditDataType.DeleteText,
            _archive.ArchiveEditDataType.DrawTextPixelPosition,
            _archive.ArchiveEditDataType.CharHeight,
            _archive.ArchiveEditDataType.CharSpacing,
            _archive.ArchiveEditDataType.DeleteTextColor
        );

        Raylib.DrawTextEx(
            _archive.ArchiveEditDataType.TextFont,
            _archive.ArchiveEditDataType.RenameText,
            new Vector2(
                _archive.ArchiveEditDataType.DrawTextPixelPosition.X + 
                Raylib.MeasureTextEx(
                    _archive.ArchiveEditDataType.TextFont, 
                    _archive.ArchiveEditDataType.DeleteText, 
                    _archive.ArchiveEditDataType.CharHeight, 
                    _archive.ArchiveEditDataType.CharSpacing).X + 
                _archive.ArchiveEditDataType.WhiteSpacePixelLengthBetweenText, 
                _archive.ArchiveEditDataType.DrawTextPixelPosition.Y
            ),
            _archive.ArchiveEditDataType.CharHeight,
            _archive.ArchiveEditDataType.CharSpacing,
            _archive.ArchiveEditDataType.RenameTextColor
        );
    }
    
    private void DrawArchiveNamingDataTypeTextBox()
    {
        var _archive = _state.archive;
        var _commonBoxes = _state.common.boxes;

        if(_archive.ArchiveNamingDataTypeTextBox.IsBoxActive)
        {
            Custom.DrawCustomRectangle(
                _archive.ArchiveNamingDataTypeTextBox.Box.PixelPosition,
                _archive.ArchiveNamingDataTypeTextBox.Box.PixelLengths,
                _archive.ArchiveNamingDataTypeTextBox.Box.Radius,
                _archive.ArchiveNamingDataTypeTextBox.Box.CornerStates,
                _archive.ArchiveNamingDataTypeTextBox.BackgroundColor
            );

            if(_archive.ArchiveNamingDataTypeTextBox.IsCursorVisible)
            {
                Raylib.DrawRectangleV(
                    _archive.ArchiveNamingDataTypeTextBox.CursorBox.PixelPosition,
                    _archive.ArchiveNamingDataTypeTextBox.CursorBox.PixelLengths,
                    _archive.ArchiveNamingDataTypeTextBox.CursorColor
                );
            }

            Raylib.DrawTextEx(
                _state.common.font.EditorPanelText,
                _archive.ArchiveNamingDataTypeTextBox.ViewText,
                _archive.ArchiveNamingDataTypeTextBox.ViewTextPixelPosition,
                _archive.ArchiveNamingDataTypeTextBox.Text.TextSize,
                _archive.ArchiveNamingDataTypeTextBox.Text.TextSpacing,
                _archive.ArchiveNamingDataTypeTextBox.TextColor
            );
    
            Raylib.DrawTexturePro(
                _archive.ArchiveNamingDataTypeTextBox.Texture2D.Texture, 
                _archive.ArchiveNamingDataTypeTextBox.Texture2D.TextureUsage,
                _archive.ArchiveNamingDataTypeTextBox.Texture2D.TextureSize,
                new Vector2(0, 0),
                0f,
                _archive.ArchiveNamingDataTypeTextBox.TextureColor
            );
        }
    }
}