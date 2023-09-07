function VerifySessionState(result) {
    if (result) {
    }
    else {
        mostrarRespuesta("Aviso", "La sesión ha caducado", "info");
    }
}
$(document).ready(function () {
    $(document).everyTime(5000, function () {
        enviarServidor("/Home/KeepActiveSession", VerifySessionState);
    });

});