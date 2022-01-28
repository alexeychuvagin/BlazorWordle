using BlazorWordle.Core.Enums;

namespace BlazorWordle.Core.ValueObjects;

public readonly record struct Letter(char Character, LetterState State);