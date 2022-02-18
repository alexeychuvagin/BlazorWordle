using Ardalis.GuardClauses;
using BlazorWordle.Core.Enums;
using BlazorWordle.Core.ValueObjects;

namespace BlazorWordle.Core;

public sealed class WordleGame
{
    private readonly string _solution;
    private readonly int _attemptsCount;
    private readonly List<IReadOnlyList<Letter>> _board;
    private readonly Dictionary<char, LetterState> _lettersInUse;

    public int WordLength => _solution.Length;
    public int AttemptsLeft => _attemptsCount - _board.Count;
    public GameStatus Status { get; private set; }
    public IReadOnlyList<IReadOnlyList<Letter>> Board => _board;
    public IReadOnlyDictionary<char, LetterState> LettersInUse => _lettersInUse;

    public WordleGame(string solution, int attemptsCount)
    {
        _attemptsCount = attemptsCount;
        _solution = Guard.Against.NullOrWhiteSpace(solution, nameof(solution)).ToLower();
        _board = new List<IReadOnlyList<Letter>>(_attemptsCount);
        _lettersInUse = new Dictionary<char, LetterState>();

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
            var state = LetterState.Absent;

            if (curChar == _solution[i])
            {
                state = LetterState.Correct;
                count++;
            }
            else if (_solution.Contains(curChar))
            {
                state = LetterState.Present;
            }

            UpdateLetterInUse(curChar, state);
            row.Add(new Letter(curChar, state));
        }

        return (count, row);
    }

    private void UpdateLetterInUse(char letter, LetterState state)
    {
        if (!_lettersInUse.ContainsKey(letter))
        {
            _lettersInUse[letter] = state;
            return;
        }

        var prevState = _lettersInUse[letter];

        if (state > prevState)
        {
            _lettersInUse[letter] = state;
        }
    }

    #endregion
}