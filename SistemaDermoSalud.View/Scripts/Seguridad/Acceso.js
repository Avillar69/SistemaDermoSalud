var cabeceras = ["idUsuario", "Codigo", "Rol", "Usuario", "Última Modificación", "Usuario Modificación", "Estado"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var listaAccesos;
var matriz = [];
var url = "Seg_Acceso/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();
var $table = $('table.demo1');
var cboRol = document.getElementById("cboRol");
reziseTabla();
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        listaDatos = listas[0].split("▼");
        var listaRol = listas[1].split("▼");
        llenarCombo(listaRol, "cboRol", "Seleccione");
        configurarCombo();
    }
    crearTablaMenu();
}
function configurarCombo() {
    cboRol.onchange = function () {
        bAc();
    }
}
function bAc() {
    var uA = "Seg_Acceso/bAc?bAc=" + cboRol.value;
    enviarServidor(uA, cA);
}
function cA(r) {
    if (r != "") {
        var listas = r.split("↔");
        if (listas[0] == "OK") {
            cch();
            listaAccesos = listas[2].split("▼");
            var tabla = document.querySelectorAll(".rdt");
            for (var i = 0; i < listaAccesos.length; i++) {
                for (var j = 0; j < tabla.length; j++) {
                    if (listaAccesos[i] == tabla[j].childNodes[0].innerHTML) {
                        var checkbox = tabla[j].childNodes[5].childNodes[0].childNodes[0];
                        checkbox.checked = true;
                        break;
                    }
                }
            }
        }
    }
}
function cch() {
    var tabla = document.querySelectorAll(".rdt");
    for (var i = 0; i < tabla.length; i++) {
        var checkbox = tabla[i].childNodes[5].childNodes[0].childNodes[0];
        checkbox.checked = false;
    }
}
function crearTablaMenu() {
    var contenido = "";
    for (var i = 0; i < listaDatos.length; i++) {
        if (listaDatos[i].split('▲')[3] ==0) {
            contenido += "<div class='rdt row p-t-5 p-b-5 bg-white' style='margin-bottom:2px;'>"
            for (var j = 0; j < listaDatos[i].split('▲').length; j++) {
                if (j <= 1 || j >= 3) {
                    contenido += "<div style='display:none'>"
                }
                else {
                    contenido += "<div class='col-md-3 col-12' style='text-align:center;'>"
                }
                contenido += listaDatos[i].split('▲')[j];
                contenido += "</div>";
                if (j == 2) {
                    contenido += "<div class='col-md-3 col-12'></div><div class='col-md-3 col-12'></div><div class='col-md-3 col-12' style='text-align:center;'>";
                    contenido += "<div class='checkbox check-info'><input type='checkbox' id='p" + i + "'><label for='p" + i + "'></label></div></div>";
                }
            }
            for (var m = 0; m < listaDatos.length; m++) {
                if (listaDatos[m].split('▲')[3] == 1 && listaDatos[i].split('▲')[0] == listaDatos[m].split('▲')[1]) {
                    contenido += "</div><div class='rdt row p-t-5 p-b-5 bg-white' style='margin-bottom:2px;'>";
                    for (var n = 0; n < listaDatos[m].split('▲').length; n++) {
                        if (n == 2) {
                            contenido += "<div class='col-md-3 col-12' style='text-align:center;'>";
                            contenido += listaDatos[i].split('▲')[2];
                            contenido += "</div>";
                        }
                        if (n <= 1 || n >= 3) {
                            contenido += "<div class='col-md-3  col-12' style='display:none'>"
                        }
                        else {
                            contenido += "<div class='col-md-3 col-12' style='text-align:center;'>"
                        }
                        contenido += listaDatos[m].split('▲')[n];
                        contenido += "</div>";
                        if (n == 2) {
                            contenido += "<div class='col-md-3  col-12'></div><div class='col-md-3  col-12' style='text-align:center;'><div class='checkbox check-info'><input type='checkbox' id='m" + m + "'><label for='m" + m + "'></label></div></div>";
                        }
                    }
                    for (var d = 0; d < listaDatos.length; d++) {
                        if (listaDatos[d].split('▲')[3] == 2 && listaDatos[m].split('▲')[0] == listaDatos[d].split('▲')[1]) {
                            contenido += "</div><div class='rdt row p-t-5 p-b-5 bg-white m-b-1' style='margin-bottom:2px;'>";
                            for (var n = 0; n < listaDatos[d].split('▲').length; n++) {
                                if (n == 2) {
                                    contenido += "<div class='col-md-3  col-12' style='text-align:center;'>";
                                    contenido += listaDatos[i].split('▲')[2];
                                    contenido += "</div>";
                                    contenido += "<div class='col-md-3 col-12' style='text-align:center;'>";
                                    contenido += listaDatos[m].split('▲')[2];
                                    contenido += "</div>";
                                }
                                if (n <= 1 || n >= 3) {
                                    contenido += "<div class='col-md-3 col-12' style='display:none'>"
                                }
                                else {
                                    contenido += "<div class='col-md-3 col-12' style='text-align:center;'>"
                                }
                                contenido += listaDatos[d].split('▲')[n];
                                contenido += "</div>";
                                if (n == 2) {
                                    contenido += "<div class='col-md-3 col-12' style='text-align:center;'><div class='checkbox check-info'><input id='d" + d + "' type='checkbox'><label for='d" + d + "'></label></div></div>";
                                }
                            }
                        }
                    }
                }
            }
            contenido += "</div>"
        }
    }
    var tbDatos = document.getElementById("tbTabla");
    tbDatos.innerHTML = contenido;

    $(window).resize(function () {
        reziseTabla();
    });
}
function TraerDetalle(id) {
    var url = "Usuario/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function mostrarMensaje(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = data[1];
        if (res == "OK") {
            mostrarRespuesta("OK", "Se actualizaron los accesos para el Rol", "success");
        }
        else {
            mostrarRespuesta("Error", mensaje, "error");
        }
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGuardar");
    btnGrabar.onclick = function () {
        var u = "Seg_Acceso/gAc?iR=" + gbi("cboRol").value + "&cad=" + crearCadDetalle();
        enviarServidor(u, mostrarMensaje);
    };
}
function crearCadDetalle() {
    var cdet = "";
    $(".rdt").each(function (obj) {
        if ($(".rdt")[obj].children[5].children[0].children[0].checked) {
            cdet += "0";
            cdet += "|" + $(".rdt")[obj].children[0].innerHTML;
            cdet += "|" + gbi("cboRol").value;
            cdet += "|0";
            cdet += "¯";
        }
    });
    return cdet;
}