using Ardalis.GuardClauses;
using BlazorWordle.Core.Enums;
using BlazorWordle.Core.ValueObjects;

namespace BlazorWordle.Core;

public sealed class WordleGame
{
    private readonly string _solution;
    private readonly int _attemptsCount;
    private readonly List<IReadOnlyList<Letter>> _board;

    public int WordLength => _solution.Length;
    public int AttemptsLeft => _attemptsCount - _board.Count;
    public GameStatus Status { get; private set; }
    public IReadOnlyList<IReadOnlyList<Letter>> Board => _board;

    public WordleGame(string word, int attemptsCount)
    {
        _attemptsCount = attemptsCount;
        _solution = Guard.Against.NullOrWhiteSpace(word, nameof(word)).ToLower();
        _board = new List<IReadOnlyList<Letter>>(_attemptsCount);

        Status = GameStatus.InProgress;
    }
    
    public void Submit(string word)
    { 
        if (AttemptsLeft == 0)
        {
            throw new Exception("Game over");
        }

        if (word.Length != WordLength)
        {
            throw new ArgumentException($"Word length must be {WordLength}", nameof(word));
        }

        var (matchCount, row) = Match(word.ToLower());

        _board.Add(row);

        if (matchCount == WordLength)
        {
            Status = GameStatus.Win;
        }
        else if (AttemptsLeft == 0)
        {
            Status = GameStatus.Lose;
        }
    }

    #region Private methods

    private (int matchCount, IReadOnlyList<Letter> row) Match(string word)
    {
        var count = 0;
        var row = new List<Letter>(word.Length);

        for (var i = 0; i < _solution.Length; i++)
        {
            var curChar = word[i];

            if (curChar == _solution[i])
            {
                row.Add(new Letter(curChar, LetterState.Correct));
                count++;
            }
            else if (_solution.Contains(curChar))
            {
                row.Add(new Letter(curChar, LetterState.Present));
            }
            else
            {
                row.Add(new Letter(curChar, LetterState.Absent));
            }
        }

        return (count, row);
    }

    #endregion
}