var cabeceras = ["idPersonal", "DNI", "Nombre", "Fecha Ingreso", "Estado"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var matriz = [];
var url = "Personal/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtFechaIngreso = document.getElementById("txtFechaIngreso");
var txtnombre = document.getElementById("txtnombre");
var txtapepat = document.getElementById("txtapepat");
var txtapemat = document.getElementById("txtapemat");
var txtEdad = document.getElementById("txtEdad");
var txtdoc = document.getElementById("txtdoc");
var cbosex = document.getElementById("cbosex");
var txtFechaNacimiento = document.getElementById("txtFechaNacimiento");
var cboEstadoCivil = document.getElementById("cboEstadoCivil");
var cboCargo = document.getElementById("cboCargo");
var txtColegiatura = document.getElementById("txtColegiatura");
var txtdireccion = document.getElementById("txtdireccion");
var txttelf = document.getElementById("txttelf");
var txtmovil = document.getElementById("txtmovil");
var txtlogin = document.getElementById("txtlogin");
var chkActivo = document.getElementById("chkActivo");
var dbimg64 = "";
var txtColor = document.getElementById("mycp")
function mostrarLista(rpta) {
    crearTablaModal(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            llenarCombo(listas[3].split("▼"), "cboCargo", "Seleccione");
            listar();
        }
    }
}
function imgClick() {
    var fI = gbi("inpImage");
    fI.click();
}

$(function () {
    $('#mycp').colorpicker();
});
function crearTablaModal(cabeceras, div) {
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
                contenido += "              <div class='col-12 col-md-3'>";
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
    configurarFiltro(cabeceras);
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
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
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
                contenido2 += "<div class='col-12 col-md-2'>";

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
    var url = "Personal/ObtenerDatosxID/?id=" + id;
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
                mensaje = "Se adicionó al nuevo Personal";
                tipo = "success";
                show_hidden_Formulario();
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó el Personal";
                tipo = "success";
                show_hidden_Formulario();
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
            var url = "Personal/Grabar";
            var frm = new FormData();
            frm.append("idPersonal", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("FechaIngreso", txtFechaIngreso.value);
            frm.append("Nombres", txtnombre.value);
            frm.append("ApellidoP", txtapepat.value);
            frm.append("ApellidoM", txtapemat.value);
            frm.append("Edad", txtEdad.value);
            frm.append("Documento", txtdoc.value);
            frm.append("Sexo", cbosex.value);
            frm.append("FechaNacimiento", txtFechaNacimiento.value);
            frm.append("EstadoCivil", cboEstadoCivil.value);
            frm.append("Direccion", txtdireccion.value);
            frm.append("Telefono", txttelf.value);
            frm.append("Movil", txtmovil.value);
            frm.append("Login", txtlogin.value);
            frm.append("Color", txtColor.value);
            frm.append("Estado", chkActivo.checked);
            frm.append("PorcentajeUtilidad", 0);
            frm.append("Img", dbimg64);
            frm.append("idCargo", cboCargo.value);
            frm.append("Planilla", gbi("chkPlanilla").checked);
            frm.append("Colegiatura", txtColegiatura.value);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
    var inputImage = document.getElementById("inpImage");
    inputImage.onchange = function (evt) {
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;
        if (FileReader && files && files.length) {
            var fr = new FileReader();
            fr.onload = function () {
                var img = gbi("persImg");
                dbimg64 = fr.result;
                img.src = dbimg64;
            }
            fr.readAsDataURL(files[0]);
        }
    }

    gbi("mycp").onchange = function () { this.style.backgroundColor = this.value; };
    var image = document.getElementById('persImg');
    image.onerror = function () {
        this.src = "../../app-assets/images/users/dftuser.png"; 
    };
}


function adt(v, ctrl) {
    gbi(ctrl).value = v;
}

$("#txtFechaIngreso").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFechaNacimiento").datetimepicker({
    format: 'DD-MM-YYYY',
});

function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        var imagen = datos[3];
        txtID.value = listaDetalle[0];
        txtCodigo.value = listaDetalle[0];
        adt(listaDetalle[1], "txtFechaIngreso");
        txtnombre.value = listaDetalle[2];
        txtapepat.value = listaDetalle[3];
        txtapemat.value = listaDetalle[4];
        txtEdad.value = listaDetalle[5];
        txtdoc.value = listaDetalle[6];
        cbosex.value = listaDetalle[7];
        adt(listaDetalle[8], "txtFechaNacimiento");
        cboEstadoCivil.value = listaDetalle[9];
        txtdireccion.value = listaDetalle[10];
        txttelf.value = listaDetalle[11];
        txtmovil.value = listaDetalle[12];
        txtlogin.value = listaDetalle[13];
        txtColegiatura.value = listaDetalle[24];
        cboCargo.value = listaDetalle[17];
        chkActivo.checked = listaDetalle[19] == "ACTIVO" ? true : false;
        txtColor.value = listaDetalle[20];
        gbi("persImg").src = imagen == "" ? "../../app-assets/images/users/dftuser.png" : imagen;
        gbi("chkPlanilla").checked = listaDetalle[26] == "TRUE" ? true : false;
        gbi("mycp").onchange();
        $('#mycp').colorpicker('setValue', gbi("mycp").value);
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Personal";
            $('#mycp').colorpicker('setValue', "RGB(30, 209, 0)");
            gbi("mycp").style.backgroundColor = "RGB(30, 209, 0)";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Personal";
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    var divs = document.querySelectorAll('.cf');
    [].forEach.call(divs, function (div) {
        div.value = "";
        quitarValidacion(div.id);
    });
    chkActivo.checked = true;
    gbi("persImg").src = "../../app-assets/images/users/dftuser.png";
}
function validarFormulario() {
    var error = true;
    var divs = document.querySelectorAll('.cf');
    [].forEach.call(divs, function (div) {
        if (validarControl(div.id)) error = false;
    });
    return error;
}
function eliminar(id) {
    swal({
        title: "Desea Eliminar este Personal?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "Personal/Eliminar?idPersonal=" + id;
                enviarServidor(url, eliminarListar);
            } else {
                swal("Cancelado", "No se elminó al Personal", "error");
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó al Personal";
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