using BlazorWordle.Core.Enums;

namespace BlazorWordle.Interfaces;

public interface IKeyboardState
{
    #region Events
    event Action? OnStateChanged;
    #endregion

    #region State
    IReadOnlyDictionary<char, LetterState> LettersInUse { get; }
    #endregion

    #region Actions
    void Submit();
    void PressKey(char c);
    void Backspace();
    #endregion
}