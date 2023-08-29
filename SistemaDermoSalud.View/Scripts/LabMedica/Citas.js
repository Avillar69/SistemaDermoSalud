var cabeceras = ["idCita", "Codigo", "Tratamiento", "Paciente", "Personal", "Fecha y Hora"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var matriz = [];
var url = "Citas/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtFechaCita = gbi("txtFechaCita");
var txtHora = gbi("txtHoraCita");
var txtHoraF = gbi("txtHoraCitaF");
var txtPaciente = gbi("txtPaciente");
var txtPersonal = gbi("txtPersonal");
var txtServicio = gbi("txtServicio");
var txtObservacion = gbi("txtObservaciones");
var txtFechaInicio = gbi("txtFechaInicio");
var txtFechaFin = gbi("txtFechaFin");
var chkActivo = document.getElementById("chkActivo");


$("#txtHoraCita").datetimepicker({
    format: 'HH:mm',
});
$("#txtHoraCitaF").datetimepicker({
    format: 'HH:mm',
});
$("#txtFechaCita").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFechaInicio").datetimepicker({
    format: 'DD-MM-YYYY'
});
$("#txtFechaFin").datetimepicker({
    format: 'DD-MM-YYYY'
});
function mostrarLista(rpta) {
    crearTablaModal(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            txtFechaInicio.value = listas[3];
            txtFechaFin.value = listas[4];
            listar();
        }
        else {
            mostrarRespuesta("Error", listas[1], "error");
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

function listar() {
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatrizPersonal(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
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
    var url = "Citas/ObtenerDatosxID/?id=" + id;
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
                mensaje = "Se adicionó al nuevo Cita";
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
                mensaje = "Se actualizó la Cita";
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

    var btnModalPaciente = document.getElementById("btnModalPaciente");
    btnModalPaciente.onclick = function () {
        cbmu("personal", "Personal", "txtPaciente", null,
            ["idPersonal", "DNI", "Personal"], "/Pacientes/ObtenerDatos", cargarLista);
    }


    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Servicios", cabeceras, matriz, "Servicios", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "servicios", cabeceras, matriz, "Servicios", "a4", "i");
    }

    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
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

    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
         
            var url = "Citas/Grabar";
            var frm = new FormData();
            frm.append("idCita", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("FechaCita", txtFechaCita.value);
            frm.append("FechaFin", gvt("txtFechaFin"));
            frm.append("FechaInicio", gvt("txtFechaInicio"));
            frm.append("Hora", txtHora.value);
            frm.append("HoraF", txtHoraF.value);
            frm.append("idPaciente", gvc("txtPaciente"));
            frm.append("NombreCompleto", gvt("txtPaciente"));
            frm.append("idPersonal", gvc(txtPersonal.id));
            frm.append("NombrePersonal", gvt(txtPersonal.id));
            frm.append("idServicio", gvc(txtServicio.id));
            frm.append("DescripcionServicio", gvt(txtServicio.id));
            frm.append("Observaciones", gbi("txtObservaciones").value.length == 0 ? " " : gbi("txtObservaciones").value)  
            frm.append("Estado", chkActivo.checked);
            frm.append("Pago", gvt("txtPago"));
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        var u = "Citas/BuscarxFecha?fI=" + txtFechaInicio.value + "&fF=" + txtFechaFin.value;
        enviarServidor(u, mostrarBusqueda);
    }
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }
  
    var btnPersonal = document.getElementById("btnModalPersonal");
    btnPersonal.onclick = function () {
        cbmu("personal", "Personal", "txtPersonal", null, ["idPersonal", "DNI", "Personal"], "/Personal/ObtenerDatos", cargarLista);
    }
    var btnServicio = document.getElementById("btnModalServicio");
    btnServicio.onclick = function () {
        cbmu("servicio", "Tratamiento", "txtServicio", null, ["idServicio", "Código", "Tratamiento"], "/Servicios/ObtenerDatos", cargarLista);
    }
}
function mostrarBusqueda(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listar();
        }
        else {
            mostrarRespuesta("Error", listas[1], "error");
        }
    }
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtPaciente":
            return gbi("txtPersonal");
            break;
        case "txtPersonal":
            return gbi("txtServicio");
            break;
        default:
            break;
    }
}
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        console.log("datos ");
        console.log(listaDetalle);
        adt(listaDetalle[0], txtID.id);
        adt(listaDetalle[20], txtCodigo.id);
        adt(listaDetalle[1], txtFechaCita.id);
        adt(listaDetalle[9], txtHora.id);
        txtPaciente.dataset.id = listaDetalle[2];
        adt(listaDetalle[3], txtPaciente.id);
        txtPersonal.dataset.id = listaDetalle[4];
        adt(listaDetalle[5], txtPersonal.id);
        txtServicio.dataset.id = listaDetalle[6];
        adt(listaDetalle[7], txtServicio.id);
        txtHora.value = listaDetalle[9];
        txtObservacion.value = listaDetalle[11];
        chkActivo.checked = listaDetalle[16].toUpperCase() == "ACTIVO" ? true : false;
        adt(listaDetalle[21],"txtPago"); 
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nueva Cita";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Cita";
            TraerDetalle(id);
            show_hidden_Formulario(true);
            break;
    }
}
function limpiarTodo() {
    var divs = document.querySelectorAll('.cf');
    [].forEach.call(divs, function (div) {
        div.value = "";
        quitarValidacion(div.id);
    });
    var divsM = document.querySelectorAll('.cfm');
    [].forEach.call(divs, function (divM) {
        bDM(divM.id);
    });
    ("txtPago").value = "";
    txtID.value = "";
    chkActivo.checked = true;
    txtCodigo.value = "";
    txtServicio.value = "";
    txtPersonal.value = "";
    txtPaciente.value = "";
    txtObservacion.value = "";
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtFechaCita")) error = false;
    if (validarControl("txtHoraCita")) error = false;
    if (validarControl("txtPaciente")) error = false;
    if (validarControl("txtServicio")) error = false;
    if (validarControl("txtPersonal")) error = false;
    return error;
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar esta Cita?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
    function (isConfirm) {
        if (isConfirm) {
            var url = "Citas/Eliminar?idCita=" + id;
            enviarServidor(url, eliminarListar);
        } else {
            swal("Cancelado", "No se elminó la Cita", "error");
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó la Cita";
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