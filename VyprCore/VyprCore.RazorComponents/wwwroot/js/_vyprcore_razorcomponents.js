// <copyright file="_vyprcore_razorcomponents.js" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

let connectionChangedHandler;
let outsideClickHandler;

window.VyprRazorComponents = {
    /// <summary>
    /// Set the search text which appears on drop downs.
    /// </summary>
    SetDropDownFilterPlaceholderText: function () { SetDropDownFilterPlaceholderText(); },

    /// <summary>
    /// Clear input for an element.
    /// </summary>
    ClearInput: function (inputId) { ClearInput(inputId); },

    /// <summary>
    /// Get screensize.
    /// </summary>
    GetScreenSize: function () { return GetScreenSize(); },

    /// <summary>
    /// Outside element click handler.
    /// </summary>
    OutsideClickHandler: function (elementId, dotnetHelper) { OutsideClickHandlerEvent(elementId, dotnetHelper); },

    /// <summary>
    /// Connection.
    /// </summary>
    ConnectionInitialise: function (interop) { ConnectionInitialiseImpl(interop); },

    /// <summary>
    /// Connection.
    /// </summary>
    ConnectionDispose: ConnectionDisposeImpl()
};

function ConnectionInitialiseImpl (interop) {
    connectionChangedHandler = function () {
        interop.invokeMethodAsync("Connection.StatusChanged", navigator.onLine);
    }
    window.addEventListener("online", connectionChangedHandler);
    window.addEventListener("offline", connectionChangedHandler);
    connectionChangedHandler(navigator.onLine);
};

function ConnectionDisposeImpl () {
    if (connectionChangedHandler != null) {
        window.removeEventListener("online", connectionChangedHandler);
        window.removeEventListener("offline", connectionChangedHandler);
    }
};

function OutsideClickHandlerEvent(elementId, dotnetHelper) {
    window.addEventListener("click", (e) => {
        if (!document.getElementById(elementId).contains(e.target)) {
            dotnetHelper.invokeMethodAsync("InvokeClickOutside");
        }
    });
};

function GetScreenSize () {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

function ClearInput(inputId) {
    setTimeout(function () {
        var input = document.querySelector("#" + inputId);
        if (input) {
            input.value = "";
        }
    }, 30);
}

function SetDropDownFilterPlaceholderText() {
    var el = document.getElementsByClassName('rz-multiselect-filter-container');
    if (typeof (el) != 'undefined' && el != null) {
        for (var i = 0; i < el.length; i++) {
            var input = el[i].getElementsByTagName('INPUT')[0];
            if (input != null && input.className == 'rz-inputtext') {
                input.setAttribute('placeholder', 'Enter search term');
            }
        };
    }
}