var cabeceras = ["idMedicamento", "Descripcion", "Laboratorio", "Estado"];
var posiciones = [0, 1, 2, 3];
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

cfgKP(["txtCodigoProducto", "txtDescripcion", "cboLaboratorio", "chkActivo", "txtPago"], cfgTKP);

function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
//Inicializando
//Punto 1
var url = "/Medicamento/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtDescripcion = document.getElementById("txtDescripcion");
var cbolaboratorio = document.getElementById("cboLaboratorio");
var chkActivo = document.getElementById("chkActivo");

function mostrarLista(rpta) {
    //Creas la tabla segun las cabeceras de arriba
    crearTablaModal(cabeceras, "cabeTabla");
    //aqui recibes la respuesta del punto 1
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[3].split("▼");
            var listalab = listas[2].split("▼");
            llenarCombo(listalab, "cboLaboratorio", "Seleccione");
            listar();
        }
    }
}
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
            case 1:
                contenido += "              <div class='col-12 col-md-4'>";
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

function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    for (var i = 0; i < cabeceras.length; i++) {
        if (i != 0) {
            columns[i - 1] = cabeceras[i];
        }
    }
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = [];
        for (var j = 0; j < matriz[i].length; j++) {
            if (j != 0) {
                data[i][j - 1] = matriz[i][j];
            }
        }
    }
    var doc = new jsPDF(orientation, 'pt', (tipo == undefined ? "a3" : "a4"));
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    var fec = new Date();
    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
    var y = fec.getFullYear();

    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
    doc.setFont('helvetica')
    doc.setFontSize(14);
    doc.text(titulo, width / 2 - 80, 95);
    doc.line(30, 125, width - 30, 125);
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("Dermosalud S.A.C", 10, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 10, 40);
    doc.text("20565643143", 30, 40);
    doc.text("Dirección:", 10, 50);
    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 50, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 90, 40)
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50)

    doc.autoTable(columns, data, {
        theme: 'plain',
        startY: 110, showHeader: 'firstPage',
        headerStyles: { styles: { overflow: 'linebreak', halign: 'center' }, fontSize: 7, },
        bodyStyles: { fontSize: 6, valign: 'middle', cellPadding: 2, columnWidt: 'wrap' },
        columnStyles: {},

    });
    if (v == "e") {
        doc.save((nombre != undefined ? nombre : "table.pdf"));
    }
    else if (v == "i") {
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}
function listar() {
    //Crea matriz de datos apartir de la lista (listaDatos)
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatrizPersonal(matriz, cabeceras, "divTabla", "contentPrincipal");
    //configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function configurarFiltro(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatrizPersonal(matriz, cabe, "divTabla", "contentPrincipal");
    };
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
                        case 1:
                            contenido2 += "col-md-4' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
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
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    var url = "Medicamento/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function actualizarListar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó el nuevo Medicamento";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Medicamento";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Medicamento/Grabar";
            var frm = new FormData();
            frm.append("idMedicamentos", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("Descripcion", txtDescripcion.value);
            frm.append("idLaboratorio", cbolaboratorio.value);
            frm.append("Estado", chkActivo.checked);
            frm.append("PagoMedicamento", gvt("txtPago"));
            frm.append("CodigoMedicamento", gvt("txtCodigoProducto"));
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Medicamentos", cabeceras, matriz, "Medicamentos", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "Medicamentos", cabeceras, matriz, "Medicamentos", "a4", "i");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    }
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
        var a = datos[0].split("▲");
        var listaDetalle = datos[2].split("▲");

        adt(listaDetalle[10], "txtCodigoProducto");
        txtID.value = listaDetalle[0];
        txtDescripcion.value = listaDetalle[1];
        cbolaboratorio.value = listaDetalle[2];
        chkActivo.checked = listaDetalle[8] == "ACTIVO" ? true : false;
        adt(listaDetalle[9], "txtPago");
        adt(listaDetalle[11], "txtCodigo");
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            //AbrirModal('modal-form');

            chkActivo.checked = true;
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Medicamento";//Titulo Insertar
            gbi("txtCodigoProducto").focus();
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Medicamento";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario();
            gbi("txtCodigoProducto").focus();
            break;
    }
}
function limpiarTodo() {
    gbi("txtID").value = "";
    gbi("txtCodigoProducto").value = "";
    gbi("txtPago").value = "";
    txtCodigo.value = "";
    txtDescripcion.value = "";
    cbolaboratorio.value = "";
    chkActivo.checked = true;
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtDescripcion")) error = false;
    if (validarControl("txtPago")) error = false;
    if (validarControl("cboLaboratorio")) error = false;
    return error;
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar este Medicamento?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "Medicamento/Eliminar?idMedicamentos=" + id;
                enviarServidor(url, eliminarListar);
            } else {
                swal("Cancelado", "No se elminó el Medicamento", "error");
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el Medicamento";
            tipo = "success";
            //CerrarModal("modal-form");
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
function cfgTKP(evt) {
    //event.target || event.srcElement
    cfgKP(["txtCodigoProducto", "txtDescripcion", "cboLaboratorio", "chkActivo", "txtPago"], cfgTKP);
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {
            case "txtCodigoProducto":
                gbi("txtDescripcion").focus();
                break;
            case "txtDescripcion":
                gbi("cboLaboratorio").focus();
                break;
            case "cboLaboratorio":
                gbi("chkActivo").focus();
                break;
            case "chkActivo":
                gbi("txtPago").focus();
                break;
        }
        return true;
    }
}