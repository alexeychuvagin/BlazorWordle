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
    /// Called by JavaScript when a KeyDown event fires.
    /// </summary>
    [JSInvokable]
    public static void JsKeyDown(KeyboardEventArgs e)
    {
        if (Enum.TryParse<ConsoleKey>(e.Code, out var consoleKey))
        {
            KeyDownEvent?.Invoke(null, consoleKey);
        }
    }
}