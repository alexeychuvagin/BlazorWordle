using BlazorWordle.Clients;
using BlazorWordle.Core;
using BlazorWordle.Core.Enums;
using BlazorWordle.Core.ValueObjects;
using BlazorWordle.Interfaces;

namespace BlazorWordle.Models;

public sealed class GameState : IKeyboardState, IGameBoardState
{
    private readonly WordleGame _game;
    private CurrentWordState _currentWord;

    public GameState(string solution)
    {
        _game = new WordleGame(solution, 6);
        _currentWord = new CurrentWordState(solution.Length);
    }

    #region Events

    public event Action? OnStateChanged;

    #endregion

    #region State

    public int WordLength => _game.WordLength;
    public int AttemptsLeft => _game.AttemptsLeft;
    public ICurrentWordState CurrentWord => _currentWord;
    public IReadOnlyList<IReadOnlyList<Letter>> Board => _game.Board;
    public IReadOnlyDictionary<char, LetterState> LettersInUse => _game.LettersInUse;

    #endregion

    #region Actions

    public void Submit()
    {
        if (_game.Status != GameStatus.InProgress || !CurrentWord.IsCompleted)
        {
            return;
        }

        _game.Submit(CurrentWord.Value);
        _currentWord.Clear();

        OnStateChanged?.Invoke();
    }

    public void PressKey(char c)
    {
        if (_game.Status != GameStatus.InProgress)
        {
            return;
        }

        _currentWord.Add(c);
    }

    public void Backspace()
    {
        if (_game.Status != GameStatus.InProgress)
        {
            return;
        }

        _currentWord.RemoveLast();
    }

    #endregion
}