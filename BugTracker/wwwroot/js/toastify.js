// Basic success and error toasts with customizable text


// error
function errorToast(message = "Something went wrong") {
    Toastify({
        text: message,
        duration: 1500,
        style: {
            background: "linear-gradient(to right, #B0099B, #C2246D)",
            cursor: "default",
        },
        position: "center",
        stopOnFocus: false,
        onClick: null,
    }).showToast();
}

// Success
function successToast(message = "Success") {
    Toastify({
        text: message,
        duration: 1500,
        style: {
            background: "linear-gradient(to right, #00B09B, #96C96D)",
            cursor: "default",
        },
        position: "center",
        stopOnFocus: false,
        onClick: null,
    }).showToast();
}
