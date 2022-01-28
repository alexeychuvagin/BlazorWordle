using System;
using BlazorWordle.Interfaces;

namespace BlazorWordle.Models;

public class CurrentWordState : ICurrentWordState
{
    public CurrentWordState(int maxLength)
        => MaxLength = maxLength;

    public char this[int i]
        => i < Value.Length ? Value[i] : ' ';
    
    #region Events

    public event Action? OnStateChanged;

    #endregion

    #region State

    public int MaxLength { get; init; }
    public string Value { get; private set; } = String.Empty;
    public bool IsCompleted => Value.Length == MaxLength;

    #endregion

    #region Actions

    public void Add(char c)
    {
        if (IsCompleted)
        {
            return;
        }

        Value += c;
        OnStateChanged?.Invoke();
    }

    public void RemoveLast()
    {
        if (Value.Length == 0)
        {
            return;
        }

        Value = Value[0..^1];
        OnStateChanged?.Invoke();
    }

    public void Clear()
    {
        Value = String.Empty;
        OnStateChanged?.Invoke();
    }

    #endregion
}