using System;
using BlazorWordle.Core.Enums;
using BlazorWordle.Core.ValueObjects;

namespace BlazorWordle.Interfaces;

public interface IGameBoardState
{
    #region Events

    event Action? OnStateChanged;

    #endregion

    #region State

    ICurrentWordState CurrentWord { get; }
    int WordLength { get; }
    int AttemptsLeft { get; }
    IReadOnlyList<IReadOnlyList<Letter>> Board { get; }
    IReadOnlyDictionary<char, LetterState> LettersInUse { get; }

    #endregion
}