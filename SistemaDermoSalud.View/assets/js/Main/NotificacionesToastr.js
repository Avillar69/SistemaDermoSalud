let Toastr_Titulo_General = "¿Estás seguro?";
let Toastr_Text_General = "No podrás revertir este cambio";
let Toastr_Titulo_Generico = "¡Aviso del Sistema!"
let Toastr_Mensaje_Error_Generico = "Ocurrió un error"
let Toastr_Mensaje_Success_Generico = "Se registró correctamente"
let Toastr_Mensaje_update_Generico = "Se actualizó correctamente"

$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
})

function Toastr_NotificacionSuccess(Titulo, Mensaje, PrevenirDuplicados) {
    Command: toastr["success"](Mensaje, Titulo)
}

function Toastr_NotificacionInfo(Titulo, Mensaje, PrevenirDuplicados) {
    Command: toastr["info"](Mensaje, Titulo)
}

function Toastr_NotificacionWarning(Titulo, Mensaje, PrevenirDuplicados) {
    Command: toastr["warning"](Mensaje, Titulo)
}

function Toastr_NotificacionError(Titulo, Mensaje, PrevenirDuplicados) {
    Command: toastr["error"](Mensaje, Titulo)
}

function Toastr_Remove() {
    toastr.remove();
}
