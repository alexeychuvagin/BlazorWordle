using BlazorWordle.Clients;
using BlazorWordle.Core;
using BlazorWordle.Core.Enums;
using BlazorWordle.Core.ValueObjects;

namespace BlazorWordle.Models;

public sealed class GameState
{
    private readonly WordsClient _client;

    public GameState(WordsClient client)
        => _client = client;

    private WordleGame? _game = null;

    #region State
    public event Action? OnStateChanged;
    public string CurrentWord { get; private set; } = string.Empty;
    public int WordLength => _game?.WordLength ?? 0;
    public int AttemptsLeft => _game?.AttemptsLeft ?? 0;
    public IReadOnlyList<IReadOnlyList<Letter>> Board => _game?.Board ?? new List<IReadOnlyList<Letter>>();
    #endregion

    public async Task CreateNewGameAsync()
    {
        var word = await _client.GetRandomWordAsync();
        _game = new WordleGame("world", 6);
    }

    public void Submit()
    {
        if (IsGameInProgress && CurrentWord.Length == _game!.WordLength)
        {
            _game.Submit(CurrentWord);
            CurrentWord = String.Empty;
            NotifyStateChanged();
        }
    }

    public void PressKey(char c)
    {
        if (IsGameInProgress && CurrentWord.Length < _game!.WordLength)
        {
            CurrentWord += c;
            NotifyStateChanged();
        }
    }

    public void Backspace()
    {
        if (IsGameInProgress && CurrentWord.Length > 0)
        {
            CurrentWord = CurrentWord[0..^1];
            NotifyStateChanged();
        }
    }

    private bool IsGameInProgress => _game is not null && _game.Status == GameStatus.InProgress;
    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}