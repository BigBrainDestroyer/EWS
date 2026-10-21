using System.Numerics;
using System.Text;
using System.Diagnostics;
using Raylib_cs;

namespace Program;

public class UpdateEditorPanelText
{
    private readonly AppState _state;
    private readonly UpdateTextView _updateTextView;

    public UpdateEditorPanelText(AppState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));

        _updateTextView = new UpdateTextView(_state);
    }

    public void UpdatingPanelText()
    {
        var _input = _state.common.input;
        var _textPanel = _state.editor.layout.textPanel;
        var _commonBoxes = _state.common.boxes;
        var _time = _state.common.time;

        if (_commonBoxes.ActiveMainBox == MainBoxNames.Editor)
        {
            _updateTextView.IsMouseWheelActive();
   
            if (_input.KeyboardCharInput.Count > 0 || Raylib.IsKeyPressed(KeyboardKey.Tab) || Raylib.IsKeyPressed(KeyboardKey.Backspace) || Raylib.IsKeyPressed(KeyboardKey.Enter) || Raylib.IsKeyPressed(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.Left) || Raylib.IsKeyPressed(KeyboardKey.Right) || _time.KeyboardKeyTimeDown.Values.Any(time => time.StartDownTime >= _textPanel.KeyboardKeyDownTimeDuration))
            {
                (_textPanel.CursorTextPosition, _textPanel.PanelText, _input.KeyboardCharInput, _textPanel.KeyboardKeyDownSettings, _time.FileChangeTimer) =
                    UpdatingText(
                        _state.common.time.Stopwatch,
                        _time.FileChangeTimer,
                        _time.KeyboardKeyTimeDown,
                        _textPanel.KeyboardKeyDownSettings,
                        _textPanel.KeyboardKeyDownTimeDuration,
                        _textPanel.CursorTextPosition,
                        _textPanel.PanelText,
                        _input.KeyboardCharInput,
                        _textPanel.GridColumnPositions,
                        _textPanel.GridRowPositions,
                        _textPanel.TapTextSpaces,
                        _textPanel.TextGridBoxesOffSetX,
                        _textPanel.TextGridBoxesOffSetY,
                        _textPanel.GridBoxesSpaceOffRowNumbers
                    );

                _updateTextView.UpdateView();
            }
            
            if(Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.IsKeyPressed(KeyboardKey.Equal))
            {
                _textPanel.TextSize = UpdateTextCharHeight(
                    _textPanel.TextSize,
                    _textPanel.CharHeightIncrement,
                    _textPanel.MinimumCharHeight
                );
    
                _textPanel.CharHeightHasChanged = true;
            }
            else if(Raylib.IsKeyDown(KeyboardKey.LeftControl) && Raylib.IsKeyPressed(KeyboardKey.Minus))
            {
                _textPanel.TextSize = UpdateTextCharHeight(
                    _textPanel.TextSize,
                    -_textPanel.CharHeightIncrement,
                    _textPanel.MinimumCharHeight
                );
    
                _textPanel.CharHeightHasChanged = true;
            }
            else if(_textPanel.CharHeightHasChanged)
            {
                _textPanel.CharHeightHasChanged = false;
                
                _textPanel.ViewText = _updateTextView.UpdateViewText(
                    _textPanel.PanelText, 
                    _textPanel.GridColumnPositions,
                    _textPanel.GridRowPositions,
                    _textPanel.TextGridBoxesOffSetX,
                    _textPanel.TextGridBoxesOffSetY,
                    _textPanel.GridBoxesSpaceOffRowNumbers
                );
            }
        }

        if(_textPanel.TextSize != _textPanel.EditorPanelTextFont.BaseSize)
        {
            _textPanel.EditorPanelTextFont =
                Load.UpdateMonoSpaceFontEx(
                    _textPanel.EditorPanelTextFont,
                    _textPanel.PanelTextFontPath,
                    _textPanel.TextSize,
                    _textPanel.MaximumCharHeightOffSet
                );

            _textPanel.EditorPanelRowNumbersFont =
                Load.UpdateMonoSpaceFontEx(
                    _textPanel.EditorPanelRowNumbersFont,
                    _textPanel.RowNumbersFontPath,
                    _textPanel.TextSize,
                    _textPanel.MaximumCharHeightOffSet
                );
        }
        
        if(Raylib.IsWindowResized() || Raylib.IsWindowMaximized())
        {
            _textPanel.ViewText = _updateTextView.UpdateViewText(
                _textPanel.PanelText, 
                _textPanel.GridColumnPositions,
                _textPanel.GridRowPositions,
                _textPanel.TextGridBoxesOffSetX,
                _textPanel.TextGridBoxesOffSetY,
                _textPanel.GridBoxesSpaceOffRowNumbers
            );
        }
    }

    private (Vector2, List<string>, List<int>, Dictionary<KeyboardKey, KeyboardKeyDownSettings>, FileChangeTimer) UpdatingText(Stopwatch stopwatch, FileChangeTimer fileChangeTimer, Dictionary<KeyboardKey, KeyboardKeyTimeDown> keyboardKeyTimeDown, Dictionary<KeyboardKey, KeyboardKeyDownSettings> keyboardKeyDownSettings, double keyboardDownDuration, Vector2 cursorTextPosition, List<string> panelText, List<int> chars, float[] gridColumnPositions, float[] gridRowPositions, string tapTextSpaces, int textGridBoxesOffSetX, int textGridBoxesOffSetY, int gridBoxesSpaceOffRowNumbers)
    {
        StringBuilder newString = new StringBuilder(panelText[(int)cursorTextPosition.Y]);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter) || keyboardKeyTimeDown[KeyboardKey.Enter].TotalDownTime > keyboardDownDuration && keyboardKeyTimeDown[KeyboardKey.Enter].TotalDownTime - keyboardKeyDownSettings[KeyboardKey.Enter].LastReactionTime >= keyboardKeyDownSettings[KeyboardKey.Enter].ReactionInterval)
        {
            (cursorTextPosition, newString, panelText) =
                UpdateEnterToText(cursorTextPosition, newString, panelText);

            keyboardKeyDownSettings[KeyboardKey.Enter] = new KeyboardKeyDownSettings(keyboardKeyTimeDown[KeyboardKey.Enter].TotalDownTime, keyboardKeyDownSettings[KeyboardKey.Enter].ReactionInterval);

            if (fileChangeTimer.StartTime == 0)
            {
                fileChangeTimer = CalculateUsingTime.UpdateFileChangeTimer(fileChangeTimer, stopwatch,  true);
            }
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Backspace) || keyboardKeyTimeDown[KeyboardKey.Backspace].TotalDownTime > keyboardDownDuration && keyboardKeyTimeDown[KeyboardKey.Backspace].TotalDownTime - keyboardKeyDownSettings[KeyboardKey.Backspace].LastReactionTime >= keyboardKeyDownSettings[KeyboardKey.Backspace].ReactionInterval)
        {
            (cursorTextPosition, newString, panelText) =
                UpdateBackspaceToString(cursorTextPosition, newString, panelText);

            keyboardKeyDownSettings[KeyboardKey.Backspace] = new KeyboardKeyDownSettings(keyboardKeyTimeDown[KeyboardKey.Backspace].TotalDownTime, keyboardKeyDownSettings[KeyboardKey.Backspace].ReactionInterval);
        
            if (fileChangeTimer.StartTime == 0)
            {
                fileChangeTimer = CalculateUsingTime.UpdateFileChangeTimer(fileChangeTimer, stopwatch,  true);
            }
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Tab))
        {
            (cursorTextPosition, newString, panelText) =
                UpdateTabToString(cursorTextPosition, newString, panelText, tapTextSpaces);

            if (fileChangeTimer.StartTime == 0)
            {
                fileChangeTimer = CalculateUsingTime.UpdateFileChangeTimer(fileChangeTimer, stopwatch,  true);
            }
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Up) || keyboardKeyTimeDown[KeyboardKey.Up].TotalDownTime > keyboardDownDuration && keyboardKeyTimeDown[KeyboardKey.Up].TotalDownTime - keyboardKeyDownSettings[KeyboardKey.Up].LastReactionTime >= keyboardKeyDownSettings[KeyboardKey.Up].ReactionInterval)
        {
            cursorTextPosition =
                UpdateUpToText(cursorTextPosition, panelText);

            keyboardKeyDownSettings[KeyboardKey.Up] = new KeyboardKeyDownSettings(keyboardKeyTimeDown[KeyboardKey.Up].TotalDownTime, keyboardKeyDownSettings[KeyboardKey.Up].ReactionInterval);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Down) || keyboardKeyTimeDown[KeyboardKey.Down].TotalDownTime > keyboardDownDuration && keyboardKeyTimeDown[KeyboardKey.Down].TotalDownTime - keyboardKeyDownSettings[KeyboardKey.Down].LastReactionTime >= keyboardKeyDownSettings[KeyboardKey.Down].ReactionInterval)
        {
            cursorTextPosition =
                UpdateDownToText(cursorTextPosition, panelText);

            keyboardKeyDownSettings[KeyboardKey.Down] = new KeyboardKeyDownSettings(keyboardKeyTimeDown[KeyboardKey.Down].TotalDownTime, keyboardKeyDownSettings[KeyboardKey.Down].ReactionInterval);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Left) || keyboardKeyTimeDown[KeyboardKey.Left].TotalDownTime > keyboardDownDuration && keyboardKeyTimeDown[KeyboardKey.Left].TotalDownTime - keyboardKeyDownSettings[KeyboardKey.Left].LastReactionTime >= keyboardKeyDownSettings[KeyboardKey.Left].ReactionInterval)
        {
            cursorTextPosition =
                UpdateLeftToText(cursorTextPosition, panelText);

            keyboardKeyDownSettings[KeyboardKey.Left] = new KeyboardKeyDownSettings(keyboardKeyTimeDown[KeyboardKey.Left].TotalDownTime, keyboardKeyDownSettings[KeyboardKey.Left].ReactionInterval);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Right) || keyboardKeyTimeDown[KeyboardKey.Right].TotalDownTime > keyboardDownDuration && keyboardKeyTimeDown[KeyboardKey.Right].TotalDownTime - keyboardKeyDownSettings[KeyboardKey.Right].LastReactionTime >= keyboardKeyDownSettings[KeyboardKey.Right].ReactionInterval)
        {
            cursorTextPosition =
                UpdateRightToText(cursorTextPosition, panelText);

            keyboardKeyDownSettings[KeyboardKey.Right] = new KeyboardKeyDownSettings(keyboardKeyTimeDown[KeyboardKey.Right].TotalDownTime, keyboardKeyDownSettings[KeyboardKey.Right].ReactionInterval);
        }

        foreach (KeyboardKey key in keyboardKeyDownSettings.Keys)
        {
            if (!Raylib.IsKeyDown(key) && keyboardKeyDownSettings[key].LastReactionTime != 0)
            {
                keyboardKeyDownSettings[key] = new KeyboardKeyDownSettings(0, keyboardKeyDownSettings[key].ReactionInterval);
            }
        }

        if (chars.Count > 0)
        {
            (cursorTextPosition, panelText, chars) =
                UpdateCharInputToText(cursorTextPosition, newString, panelText, chars);

            if (fileChangeTimer.StartTime == 0)
            {
                fileChangeTimer = CalculateUsingTime.UpdateFileChangeTimer(fileChangeTimer, stopwatch,  true);
            }
        }

        return (cursorTextPosition, panelText, chars, keyboardKeyDownSettings, fileChangeTimer);
    }


    private (Vector2, StringBuilder, List<string>) UpdateEnterToText(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText)
    {
        string newRow = newString.ToString((int)cursorTextPosition.X, newString.Length - (int)cursorTextPosition.X);

        panelText[(int)cursorTextPosition.Y] = newString.ToString(0, (int)cursorTextPosition.X);

        cursorTextPosition.Y += 1;

        if(panelText.Count == cursorTextPosition.Y)
        {
            panelText.Add(newRow);
        }
        else
        {
            panelText.Insert((int)cursorTextPosition.Y, newRow);
        }

        cursorTextPosition.X = 0;

        newString = new StringBuilder(newRow);

        return (cursorTextPosition, newString, panelText);
    }

    
    private (Vector2, StringBuilder, List<string>) UpdateBackspaceToString(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText)
    {
        if(cursorTextPosition.X == 0 && cursorTextPosition.Y == 0)
        {
            return (cursorTextPosition, newString, panelText);
        }
        else if(cursorTextPosition.X == 0)
        {
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y - 1].Length;

            newString.Insert(0, panelText[(int)cursorTextPosition.Y - 1]);
            
            panelText.RemoveAt((int)cursorTextPosition.Y);

            cursorTextPosition.Y -= 1;

            panelText[(int)cursorTextPosition.Y] = newString.ToString();

            return (cursorTextPosition, newString, panelText);
        }

        cursorTextPosition.X -= 1;
        
        newString.Remove((int)cursorTextPosition.X, 1);
        
        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        return (cursorTextPosition, newString, panelText);
    }

    private (Vector2, StringBuilder, List<string>) UpdateTabToString(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText, string tapTextSpaces)
    {
        newString.Insert((int)cursorTextPosition.X, tapTextSpaces);

        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        cursorTextPosition.X += tapTextSpaces.Length;

        return (cursorTextPosition, newString, panelText);
    }

    private Vector2 UpdateUpToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if((int)cursorTextPosition.Y == 0)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X > panelText[(int)cursorTextPosition.Y - 1].Length)
        {
            cursorTextPosition.Y -= 1;
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y].Length;

            return cursorTextPosition;
        }

        cursorTextPosition.Y -= 1;

        return cursorTextPosition;
    }

    
    private Vector2 UpdateDownToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if((int)cursorTextPosition.Y == panelText.Count - 1)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X > panelText[(int)cursorTextPosition.Y + 1].Length)
        {
            cursorTextPosition.Y += 1;
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y].Length;

            return cursorTextPosition;
        }

        cursorTextPosition.Y += 1;

        return cursorTextPosition;
    }

    
    private Vector2 UpdateLeftToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if(cursorTextPosition.X == 0 && cursorTextPosition.Y == 0)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X == 0)
        {
            cursorTextPosition.Y -= 1;
            cursorTextPosition.X = panelText[(int)cursorTextPosition.Y].Length;

            return cursorTextPosition;
        }

        cursorTextPosition.X -= 1;
        
        return cursorTextPosition;
    }

    
    private Vector2 UpdateRightToText(Vector2 cursorTextPosition, List<string> panelText)
    {
        if(cursorTextPosition.X == panelText[(int)cursorTextPosition.Y].Length && cursorTextPosition.Y == panelText.Count - 1)
        {
            return cursorTextPosition;
        }
        else if(cursorTextPosition.X == panelText[(int)cursorTextPosition.Y].Length)
        {
            cursorTextPosition.Y += 1;
            cursorTextPosition.X = 0;

            return cursorTextPosition;
        }

        cursorTextPosition.X += 1;
        
        return cursorTextPosition;
    }

    
    private (Vector2, List<string>, List<int>) UpdateCharInputToText(Vector2 cursorTextPosition, StringBuilder newString, List<string> panelText, List<int> chars)
    {
        foreach(int character in chars)
        {
            newString.Insert((int)cursorTextPosition.X, (char)character);
            cursorTextPosition.X += 1;
        }

        chars = new List<int>();

        panelText[(int)cursorTextPosition.Y] = newString.ToString();

        return (cursorTextPosition, panelText, chars);
    }

    private int  UpdateTextCharHeight(int currentCharHeight, int charHeightIncrement, int minimumCharHeight)
    {
        if(charHeightIncrement > 0)
        {
            return currentCharHeight + charHeightIncrement;
        }
        else if(currentCharHeight + charHeightIncrement >= minimumCharHeight)
        {
            return currentCharHeight + charHeightIncrement;
        }
        else
        {
            return minimumCharHeight;
        }
    }
}