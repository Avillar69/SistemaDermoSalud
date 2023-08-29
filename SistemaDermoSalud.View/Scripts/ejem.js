var cabeceras = [];
var listaDatos;
var matriz = [];
var url = "Cargos/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

function mostrarLista(rpta) {
    crearTablaCargos(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listar();
        }
    }
}
function crearTablaCargos(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 6:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
        }
        contenido += "                  <label>" + cabeceras[i] + "</label>";
        contenido += "              </div>";
    }
    contenido += "          </div>";
    var divTabla = gbi(div);
    divTabla.innerHTML = contenido;
}
function configurarFiltro() {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        configurarPaginacion(matriz, registrosPagina, paginasBloque, indiceActualBloque);
        indiceActualBloque = 0;
        indiceActualPagina = 0;;
        mostrarMatrizPersonal(0, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);
    };
}
function listar() {
    configurarFiltro();
    matriz = crearMatriz(listaDatos);
    mostrarMatrizPersonal(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function mostrarMatrizPersonal(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 2:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                        case 6:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-1'>";

                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o fs-11'></i> </button>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil fs-11'></i></button>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                dat.push(contenido2);
            }
            else break;
        }
        var clusterize = new Clusterize({
            rows: dat,
            scrollId: tabId,
            contentId: contentID
        });
    }
}
function TraerDetalle(id) {
    var url = "Cargos/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function actualizarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;
        if (codigo.length == 0) {
            if (res == "OK") {
                mensaje = "Se adicionó al nuevo Servicio";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó el Servicio";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Cargos/Grabar";
            var frm = new FormData();

            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Servicio";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Servicio";
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    chkActivo.checked = true;
}
function validarFormulario() {
    var error = true;
    return error;
}
function eliminar(id) {
    swal({
        title: "Desea Eliminar este Cargo?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
    function (isConfirm) {
        if (isConfirm) {
            var url = "Cargos/Eliminar?idCargo=" + id;
            enviarServidor(url, eliminarListar);
        } else {
            swal("Cancelado", "No se elminó al Servicio", "error");
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó al Servicio";
            tipo = "success";
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
    } else {
        mensaje = "No hubo respuesta";
    }
    mostrarRespuesta(res, mensaje, tipo);
    listaDatos = data[2].split("▼");
    listar();
}