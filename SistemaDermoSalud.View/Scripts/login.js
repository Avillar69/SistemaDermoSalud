var txtUsuario = document.getElementById("txtUsuario");
var txtPassword = document.getElementById("txtPassword");
var txtLoginTipoCambio;
TipoCambioLoginxFecha();
configurarBoton();
function TipoCambioLoginxFecha() {
    var f = new Date();
    var fechaActualLogin = (f.getDate() + "-" + (f.getMonth() + 1) + "-" + f.getFullYear());
    var url = "../TipoCambio/ListarTipoXFechaActual";
    var frm = new FormData();
    frm.append("fecha", fechaActualLogin);
    enviarServidorPost2(url, CargarTipoCambioLogin, frm);
}
function CargarTipoCambioLogin(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲'); //lista OC
        if (Resultado == 'OK') {
            txtLoginTipoCambio = datos;
        }
    }
}
function enviarServidorPost(url, metodo, tipo) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open(tipo, url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
    };
    var frm = new FormData();

    frm.append("Usuario", txtUsuario.value.toUpperCase());
    frm.append("Password", txtPassword.value.toUpperCase());
    xhr.send(frm);
}
function enviarServidorPost2(url, metodo, frm) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open("post", url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
    };

    xhr.send(frm);
}
function configurarBoton() {
    var btnGrabar = document.getElementById("btnLogin");
    btnGrabar.onclick = function () {
        if (validarLogin()) {
            var url = "Log";
            enviarServidorPost(url, redireccion, "post");
        }
    };
    //var btnLoginOmitir = document.getElementById("btnLoginOmitir");
    //btnLoginOmitir.onclick = function () {
    //    actualizarListarLogin();
    //}
    var btnLoginGrabarMonedaCambio = document.getElementById("btnLoginGrabarMonedaCambio");
    btnLoginGrabarMonedaCambio.onclick = function () {
        var ValorCompra = parseFloat(gbi("txtLoginValorCompra").value);
        var ValorVenta = parseFloat(gbi("txtLoginValorVenta").value);
        if (validarLogin()) {
            if (ValorCompra > 0 && ValorVenta > 0) {
                var url = "../TipoCambio/Grabar";
                var frm = new FormData();
                var f = new Date();
                var fechaActualLogin = (f.getDate() + "-" + (f.getMonth() + 1) + "-" + f.getFullYear());
                frm.append("idTipoCambio", 0);
                frm.append("idMoneda", txtLoginTipoMoneda.value);
                frm.append("ValorCompra", gbi("txtLoginValorCompra").value);
                frm.append("ValorVenta", txtLoginValorVenta.value);
                frm.append("Fecha", fechaActualLogin);
                enviarServidorPost2(url, actualizarListarLogin, frm);
            } else {
                gbi("txtRptaTipoCambio").innerHTML = "Ingresar correctamente los valores de Tipo de Cambio";
                console.log(gbi("txtRptaTipoCambio"));
            }
        }
    };

    txtUsuario.onkeypress = function () { txtUsuario.style.border = ""; }
    txtPassword.onkeypress = function () { txtPassword.style.border = ""; }

    txtPassword.onkeyup = function (e) {
        if (e.keyCode == 13) {
            btnGrabar.onclick();
        }
    }

    let txtLoginValorCompra = document.getElementById("txtLoginValorCompra");
    txtLoginValorCompra.onkeypress = function (e) {
        var reg = /^[0-9.]+$/;
        if (!reg.test(e.key)) return false;
    }
    let txtLoginValorVenta = document.getElementById("txtLoginValorVenta");
    txtLoginValorVenta.onkeypress = function (e) {
        var reg = /^[0-9.]+$/;
        if (!reg.test(e.key)) return false;
    }
}
function redireccion(rpta) {
    if (rpta == "OK") {
        var logoutUrl = '../../';
        window.location.href = logoutUrl;
    } else {
        swal('Información', rpta, 'info');
    }
}
function AbrirModalLogin(idModal) {
    ventanaActual = 2
    $('#' + idModal).modal('show');
}
function validarLogin() {
    error = true;
    if (txtUsuario.value == "") { txtUsuario.style.border = "1px solid red"; error = false; };
    if (txtPassword.value == "") { txtPassword.style.border = "1px solid red"; error = false; };
    return error;
}
//evitar que el boton back del browser funcione ... no se si funciona bien .. 
//solo funciona cuando se ve la consola del browser X_Xs
window.onbeforeunload = function (e) {
    if (e.currentTarget == window) {
        location.reload();
    }
}
function actualizarListarLogin(rpta) {
    var logoutUrl = '../../';
    window.location.href = logoutUrl;
}
