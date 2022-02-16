using BlazorWordle.Core.Enums;
using FluentAssertions;
using Xunit;

namespace BlazorWordle.Core.UnitTests;

public class WordleGameTests
{
    [Fact]
    public void Creating_a_new_game()
    {
        // Arrange
        const string solution = "Hello";

        // Act
        WordleGame sut = new(solution, 6);

        // Assert
        sut.Status.Should().Be(GameStatus.InProgress);
        sut.WordLength.Should().Be(solution.Length);
        sut.AttemptsLeft.Should().Be(6);
    }

    [Fact]
    public void Changing_attempt_number()
    {
        // Arrange
        const string solution = "Hello";
        WordleGame sut = new(solution, 6);

        // Act
        sut.Submit("World");

        // Assert
        sut.AttemptsLeft.Should().Be(5);
    }

    [Fact]
    public void Recognizing_absent_letters()
    {
        // Arrange
        const string solution = "Hello";
        WordleGame sut = new(solution, 6);

        // Act
        sut.Submit("World");

        // Assert
        sut.Board[0][0].State.Should().Be(LetterState.Absent);
        sut.Board[0][2].State.Should().Be(LetterState.Absent);
        sut.Board[0][4].State.Should().Be(LetterState.Absent);
    }

    [Fact]
    public void Recognizing_present_letters()
    {
        // Arrange
        const string solution = "Hello";
        WordleGame sut = new(solution, 6);

        // Act
        sut.Submit("World");

        // Assert
        sut.Board[0][1].State.Should().Be(LetterState.Present);
    }

    [Fact]
    public void Recognizing_correct_letters()
    {
        // Arrange
        const string solution = "Hello";
        WordleGame sut = new(solution, 6);

        // Act
        sut.Submit("World");

        // Assert
        sut.Board[0][3].State.Should().Be(LetterState.Correct);
    }

    [Fact]
    public void Win_game()
    {
        // Arrange
        const string solution = "Hello";
        WordleGame sut = new(solution, 6);

        // Act
        sut.Submit("Hello");

        // Assert
        sut.Status.Should().Be(GameStatus.Win);
    }

    [Fact]
    public void Lose_game()
    {
        // Arrange
        const string solution = "Hello";
        WordleGame sut = new(solution, 6);

        // Act
        sut.Submit("wordA");
        sut.Submit("wordB");
        sut.Submit("wordC");
        sut.Submit("wordD");
        sut.Submit("wordE");
        sut.Submit("wordF");

        // Assert
        sut.Status.Should().Be(GameStatus.Lose);
    }
}