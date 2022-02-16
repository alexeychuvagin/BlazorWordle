namespace BlazorWordle.Interfaces;

public interface ICurrentWordState
{
    char this[int i] { get; }

    #region Events

    event Action? OnStateChanged;

    #endregion

    #region State

    int MaxLength { get; }
    string Value { get; }
    bool IsCompleted { get; }

    #endregion
}