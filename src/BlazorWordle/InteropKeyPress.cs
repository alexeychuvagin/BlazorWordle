using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorWordle;

/// <summary>
/// Proxy for receiving key press events from JavaScript.
/// </summary>
public static class InteropKeyPress
{
    /// <summary>
    /// Fires when a KeyDown message is received from JavaScript.
    /// </summary>
    public static event EventHandler<ConsoleKey>? KeyDownEvent;

    /// <summary>
    /// Called by JavaScript when a Key Down event fires.
    /// </summary>
    [JSInvokable]
    public static Task JsKeyDown(KeyboardEventArgs e)
    {
        Console.WriteLine("***********************************************");
        Console.WriteLine(e.Key);
        Console.WriteLine("***********************************************");

        if (Enum.TryParse<ConsoleKey>(e.Key, out var consoleKey))
        {
            KeyDownEvent?.Invoke(null, consoleKey);
        }

        return Task.CompletedTask;
    }
}
