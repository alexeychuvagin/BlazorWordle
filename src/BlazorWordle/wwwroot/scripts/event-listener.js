window.EventListenerJsFunctions = {
    addKeyboardListenerEvent: function () {
        window.document.addEventListener('keydown', eventListenerCallback);
    },
    removeKeyboardListenerEvent: function () {
        window.document.removeEventListener('keydown', eventListenerCallback);
    }
};

function serializeEvent(e) {
    if (e) {
        return {
            key: e.key,
            code: e.keyCode.toString(),
            location: e.location,
            repeat: e.repeat,
            ctrlKey: e.ctrlKey,
            shiftKey: e.shiftKey,
            altKey: e.altKey,
            metaKey: e.metaKey,
            type: e.type
        };
    }
};

function eventListenerCallback(e) {
    DotNet.invokeMethodAsync('BlazorWordle', 'JsKeyDown', serializeEvent(e))
}
