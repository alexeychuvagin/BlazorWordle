var JsFunctions = window.JsFunctions || {};

JsFunctions = {
    setBodyClass: function (className) {
        window.document.body.classList.add(className)
    },
    removeBodyClass: function (className) {
        window.document.body.classList.remove(className)
    }
};