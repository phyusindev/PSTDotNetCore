function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function successMessage(message) {
    // Swal.fire({
    //     title: "Sucess!",
    //     text: message,
    //     icon: "success"
    // });

    Notiflix.Report.success(
        'Success!',
        message,
        'ok',
    );
}

function errorMessage(message) {
    // Swal.fire({
    //     title: "Error!",
    //     text: message,
    //     icon: "error"
    // });

    Notiflix.Report.failure(
        'Error!',
        message,
        'ok',
    );
}

function warningMessage(message) {
    Notiflix.Report.warning(
        'Warning!',
        message,
        'ok',
    );
}