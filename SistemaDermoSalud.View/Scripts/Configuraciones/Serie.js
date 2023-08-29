var cabeceras = ["idSerie", "Tipo", "Serie", "Numero", "Fecha"];
var listaDatos;
var matriz = [];
var orden = 0;
var registrosPagina = 1000000000;
var indiceActualPagina = 0;
var paginasBloque = 3;
var indiceActualBloque = 0;
var indicePagina = 0;
var textoExportar;
var excelExportar;
var txtModal;
var txtModal2;
var FechaInicioTraslado;
var SerieGuia;
var NumeroGuia;
var RazonSocial;
var listaLocales;
var listaMoneda;
var listaFormaPago;
var listaTipoCompra;
var listaComprobante;
var listaSocios;
var listaDireccionT
var idTablaDetalle = 0;
var idDiv;

var url = "Serie/ObtenerDatos";
enviarServidor(url, mostrarLista);
//configNav();
configBM();
reziseTabla();
cfgKP(["txtTipoDocumento"], cfgTMKP); //configuration  K P
cfgKP(["txtSerie"], cfgTKP);
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function cfgTMKP(evt) {
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        var n = o.replace("txt", "");
        gbi("btnModal" + n).click();
    }
}
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}
function cfgTKP(evt) {

    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {
            case "txtTipoDocumento":
                gbi("txtNroSerie").focus();
                break;
            case "txtNroSerie":
                if (gbi("txtNroSerie").value.trim().length == 0) {
                    mostrarRespuesta("Alerta", "Llene el Nro de Serie", "error");
                }
                else {
                    gbi("txtNroSerie").value = pad(gbi("txtNroSerie").value, 4);
                    gbi("txtObservacion").focus();
                }
                break;
            case "txtObservacion":
                gbi("txtTipoDocumento").focus();
                break;
        }
    }

}
function mostrarLista(rpta) {
    crearTabla(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            listaDatos = listas[1].split("▼");
            listaComprobante = listas[2].split("▼");
            var btnTipoDocumento = document.getElementById("btnModalTipoDocumento");
            btnTipoDocumento.onclick = function () {
                cbm("tipodocumento", "Tipo de Documento", "txtTipoDocumento", null,
                    ["idTipoComprobante", "Descripción"], listaComprobante, cargarSinXR);
            }
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        listar();
    }
    reziseTabla();
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    //limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nueva Serie";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Serie";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }

    function TraerDetalle(id) {
        var url = 'Serie/ObtenerDatosxID?id=' + id;
        enviarServidor(url, CargarDetalles);
    }
}
function listar() {

    matriz = crearMatriz(listaDatos);
    configurarFiltroGR(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });

}
function mostrarMatriz(matriz, cabeceras, tabId, contentID) {
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
function funcionModal(tr) {
    var num = tr.id.replace("numMod", "");
    var id = gbi("md" + num + "-0").innerHTML;
    var value;
    if (tr.children.length === 2) {
        value = gbi("md" + num + "-1").innerHTML;
    }
    else {
        value = gbi("md" + num + "-2").innerHTML;
    }
    var value2 = gbi("md" + num + "-1").innerHTML;

    txtModal.value = value;
    txtModal.dataset.id = id;
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtTipoDocumento":
            return gbi("txtSerie");
            break;
        default:
            break;
    }
}
function cbm(ds, t, tM, tM2, cab, dat, m) {
    document.getElementById("div_Frm_Modals").innerHTML = document.getElementById("div_Frm_Detalles").innerHTML;
    document.getElementById("btnGrabar_Modal").dataset.grabar = ds;
    document.getElementById("lblTituloModal").innerHTML = t;
    var txtCodigo_FormaPago = document.getElementById("txtCodigo_Detalle");
    txtCodigo_FormaPago.disabled = true;
    txtCodigo_FormaPago.placeholder = "Autogenerado";
    var txtM1 = document.getElementById(tM);
    txtModal = txtM1;
    tM2 == null ? txtModal2 = tM2 : txtModal2 = document.getElementById(tM2);
    cabecera_Modal = cab;
    m(dat);
}
function configBM() {
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
        limpiarTodo();
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Serie/Grabar";
            var frm = new FormData();
            frm.append("idSerie", gvt("txtID"));
            frm.append("idTipoComprobante", gvc("txtTipoDocumento"));
            frm.append("Descripcion", gvt("txtTipoDocumento"));
            frm.append("NroSerie", gvt("txtSerie"));
            frm.append("Estado", true);
            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
}
function limpiarTodo() {
    bDM("txtTipoDocumento");
    limpiarControl("txtSerie");
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtTipoDocumento")) error = false;
    if (validarControl("txtSerie")) error = false;
    return error;
}
function configurarFiltroGR(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatriz(matriz, cabe, "divTabla", "contentPrincipal");
    };
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        if (Resultado == 'OK') {
            adc(listaComprobante, datos[1], "txtTipoDocumento", 1);
            adt(datos[3], "txtSerie");
            adt(datos[0], "txtID");
        }
    }
}
function crearTabla(cabeceras, div) {
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
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}
function adc(l, id, ctrl, c) {
    var ind;
    for (var i = 0; i < l.length; i++) {
        if (l[i].split('▲')[0] == id) {
            ind = i;
            break;
        }
    }
    gbi(ctrl).value = l[ind].split('▲')[c];
    gbi(ctrl).dataset.id = id;
}


function actualizarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        var tipo = '';
        var txtCodigo = document.getElementById('txtID');
        var codigo = txtCodigo.value;
        if (codigo.length == 0) {
            if (res == 'OK') {
                mensaje = 'Se adicionó el Serie';
                tipo = 'success';
            }
            else {
                mensaje = data[1];
                tipo = 'error';
            }
        }
        else {
            if (res == 'OK') {
                mensaje = 'Se actualizó el Serie';
                tipo = 'success';
            }
            else {
                mensaje = data[1];
                tipo = 'error';
            }
        }
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split('▼');
        listar();
    }
}

function eliminar(id) {
    swal({
        title: 'Desea Eliminar esta Serie? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                IdEliminar = "";
                IdEliminar = id;
                var u = "Serie/Eliminar?idSerie=" + id;
                enviarServidor(u, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó la Serie', 'error');
            }
        });
}


function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó la Serie';
            tipo = 'success';
            listaDatos = data[2].split('▼');
            listar();
        }
        else {
            mensaje = data[1]
            tipo = 'error';
        }
    } else {
        mensaje = 'No hubo respuesta';
    }
    mostrarRespuesta(res, mensaje, tipo);
}