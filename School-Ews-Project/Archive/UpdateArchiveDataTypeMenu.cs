using System.Numerics;

namespace Program;

public class UpdateArchiveDataTypeMenu
{
    private readonly AppState _state;

    public UpdateArchiveDataTypeMenu(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public void UpdateDataTypeMenu()
    {
        var _layout = _state.archive.layout;
        var _core = _state.archive.core;

        _layout.AddDataTypeMenu =
            CalculateAddDataTypeMenuBox(
                _core.AddLegalDataTypeToStorage,
                _layout.SearchBarPixelPosition,
                _layout.SearchBarPixelLengths
            );
    }

    public ArchiveAddDataTypeMenu UpdateArchiveTypeMenuActiveState(ArchiveAddDataTypeMenu currentAddDataTypeMenu)
    {
        ArchiveAddDataTypeMenu newAddDataTypeMenu =
            new ArchiveAddDataTypeMenu(
                currentAddDataTypeMenu.AddLegalDataTypeToStorage,
                currentAddDataTypeMenu.BoxPosition,
                currentAddDataTypeMenu.BoxLengths,
                currentAddDataTypeMenu.SingleBoxLengths,
                currentAddDataTypeMenu.Texture2DPosition,
                currentAddDataTypeMenu.Texture2DSize,
                !currentAddDataTypeMenu.IsMenuActive
            );

        return newAddDataTypeMenu;
    }
    
    private ArchiveAddDataTypeMenu CalculateAddDataTypeMenuBox(ArchiveStorageNames?[] addLegalDataTypeStorage, Vector2 searchBoxPosition, Vector2 searchBoxLengths)
    {
        ArchiveAddDataTypeMenu newAddDataTypeMenu = 
            new ArchiveAddDataTypeMenu(
                addLegalDataTypeStorage,
                searchBoxPosition,
                CalculateAddDataTypeMenuBoxLengths(searchBoxLengths, addLegalDataTypeStorage),
                searchBoxLengths,
                searchBoxPosition,
                CalculateTexture2DSize(searchBoxLengths),
                true
            );

        return newAddDataTypeMenu;
    }

    private Vector2 CalculateAddDataTypeMenuBoxLengths(Vector2 searchBoxLengths, ArchiveStorageNames?[] addLegalDataTypeStorage)
    {
        int legalDataTypeCount = 0;
        foreach(ArchiveStorageNames? storage in addLegalDataTypeStorage)
        {
            if(storage != null)
            {
                legalDataTypeCount += 1;
            }
        }

        return new Vector2(searchBoxLengths.X, searchBoxLengths.Y * legalDataTypeCount);
    }

    private Vector2 CalculateTexture2DSize(Vector2 searchBoxLengths)
    {
        if(searchBoxLengths.X > searchBoxLengths.Y)
        {
            return new Vector2(searchBoxLengths.Y, searchBoxLengths.Y);
        }

        return new Vector2(0,0);
    }
    
}