var cabeceras = ["idComisionMedico", "idCita", "Servicio", "N", "Costo", "Gastos", "Diferencia", "%", "Comision"];
var listaDatos;
var matriz = [];
var url = "Comision_Medico/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
var fechaFiltro = new Date();
$('#txtFilFecIn').data("DateTimePicker").date(new Date(fechaFiltro.getFullYear(), fechaFiltro.getMonth(), 1));
$('#txtFilFecFn').data("DateTimePicker").date(fechaFiltro);
function mostrarLista(rpta) {
    crearTablaCargos(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaMedicos = listas[2].split("▼");
        }
        llenarCombo(listaMedicos, "cboMedico", "Seleccione");
    }
}
function mostrarListaxMedico(rpta) {
    crearTablaCargos(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            let porcentaje = listas[3];

            listar();
            calcularComision();
        }
    }
}
function crearTablaCargos(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row rowTableDet panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 10px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0: case 1: //case 5:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
            case 6:
                contenido += "              <div class='col-12 col-md-1' >";//style='display:none;'
                break;
            default:
                contenido += "              <div class='col-12 col-md-1' >";
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
        indiceActualPagina = 0;
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
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 10px;margin-bottom:2px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0: case 1:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 2:
                            contenido2 += "col-md-4' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-1' >";//style='display:none;'
                            break;
                        case 6:
                            contenido2 += "col-md-1' style='padding-top:5px;'>"//display:none;
                            break;
                        case 7:
                            contenido2 += "col-md-1'>";
                            break;
                        default:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                    }
                    if (j === 5) {
                        contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" +
                            `<input type='number' min='0' max='100' step='5' class='form-control  form-control-sm' onkeypress='return IngresoNumero(event)' onchange='calcularComision(this)' onkeyup='calcularComision(this)' value='${matriz[i][j]}'>`
                            + "</span>";
                    } else if (j === 7) {
                        contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" +
                            `<input type='number'  min='0' max='100' step='5' class='form-control  form-control-sm' onkeypress='return IngresoNumero(event)' onchange='calcularComision(this)' onkeyup='calcularComision(this)' value='${matriz[i][j]}'>`
                            + "</span>";
                    } else {
                        contenido2 += "<span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    }
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-1'>";
                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='EliminarFila(this)'> <i class='fa fa-trash-o fs-11'></i> </button>";
                contenido2 += "</div>";
                contenido2 += "<div class='col-12' style='display:none'>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='guardarComision(this)'> <i class='fa fa-save fs-11'></i> </button>";
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
function TraerDetalle() {
    let esValido = validarFormulario();
    var idMedico = document.getElementById("cboMedico").value;
    let fechaIni = $('#txtFilFecIn').data("DateTimePicker").date();
    let fechaFin = $('#txtFilFecFn').data("DateTimePicker").date();

    if (esValido && idMedico && fechaIni != null && fechaFin != null) {
        var url = `Comision_Medico/ObteneComisionMedico/?id=${idMedico}&fechaIni=${fechaIni.format("YYYY-MM-DD")}&fechaFin=${fechaFin.format("YYYY-MM-DD")}`;
        enviarServidor(url, mostrarListaxMedico);
    }



    //let x = document.getElementById("contentPrincipal");
    //let y = x.childNodes["length"];
    //var z = [];
    //for (i = 0; i < y; i++) {
    //    let u = document.getElementById("num" + i);
    //    for (t = 0; t < u.children["length"]; t++) {
    //        z += u.childNodes[t].outerText;
    //        if (u.childNodes[t].outerText != " ") {
    //            z += "®";  //169
    //        }
    //    }
    //    z += "~"//126
    //}
    //console.log(z.split("~"));



    //exportTableToExcel(contentPrincipal,"priueba");
}

function exportar() {
    let x = document.getElementById("contentPrincipal");
    let y = x.childNodes["length"];
    var z = [];
    for (i = 0; i < y; i++) {
        let u = document.getElementById("num" + i);
        for (t = 0; t < u.children["length"]; t++) {
            z += u.childNodes[t].outerText;
            if (u.childNodes[t].outerText != " ") {
                z += "®";  //169
            }
        }
        z += "~"//126
    }
    //luego validas que el count del selector de esta clase rowTableDet sea +0

    //if (esValido && idMedico && fechaIni != null && fechaFin != null) {
    var url = `Comision_Medico/armarTemp`;
    var frm = new FormData();
    frm.append("cad", z || 0);
    enviarServidorPost(url, redirecciona, frm);
    //}

}
function redirecciona(r) {
    let x = gbi("cboMedico");
    let f = x.selectedOptions[0].outerText;
    if (r == "OK") {
        window.location = "Comision_Medico/ExportarExcel?fechaIni=" + gvt("txtFilFecIn") + "&fechaFin=" + gvt("txtFilFecFn")+"&med="+f;
    }
    else {
        mostrarRespuesta("Error", r, "error");
    }
}
//function exportTableToExcel(tableID, filename = '') {
//    var downloadLink;
//    var dataType = 'application/vnd.ms-excel';
//    var tableSelect = document.getElementById(tableID);
//    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

//    // Specify file name
//    filename = filename ? filename + '.xls' : 'excel_data.xls';

//    // Create download link element
//    downloadLink = document.createElement("a");

//    document.body.appendChild(downloadLink);

//    if (navigator.msSaveOrOpenBlob) {
//        var blob = new Blob(['ufeff', tableHTML], {
//            type: dataType
//        });
//        navigator.msSaveOrOpenBlob(blob, filename);
//    } else {
//        // Create a link to the file
//        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

//        // Setting the file name
//        downloadLink.download = filename;

//        //triggering the function
//        downloadLink.click();
//    }
//}


















function actualizarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";

        if (res == "OK") {
            mensaje = "Se guardaron los datos corectamente.";
            tipo = "success";

            listaDatos = data[2].split("▼");
            listar();
            calcularComision();
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }

        //if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
    }
}
function configurarBotonesModal() {
    var btnImprimir_General = gbi("btnImprimir_General");
    btnImprimir_General.onclick = function () {
        let esValido = validarFormulario();
        var idMedico = document.getElementById("cboMedico").value;
        let fechaIni = $('#txtFilFecIn').data("DateTimePicker").date();
        let fechaFin = $('#txtFilFecFn').data("DateTimePicker").date();

        if (esValido && idMedico && fechaIni != null && fechaFin != null) {
            var url = `Comision_Medico/ReporteGeneral/?id=${idMedico}&fechaIni=${fechaIni.format("YYYY-MM-DD")}&fechaFin=${fechaFin.format("YYYY-MM-DD")}`;
            enviarServidor(url, ReporteGeneral);
        }
    }
    var btnImprimir_Detallado = document.getElementById("btnImprimir_Detallado");
    btnImprimir_Detallado.onclick = function () {
        let esValido = validarFormulario();
        var idMedico = document.getElementById("cboMedico").value;
        let fechaIni = $('#txtFilFecIn').data("DateTimePicker").date();
        let fechaFin = $('#txtFilFecFn').data("DateTimePicker").date();

        if (esValido && idMedico && fechaIni != null && fechaFin != null) {
            var url = `Comision_Medico/ReporteDetallado/?id=${idMedico}&fechaIni=${fechaIni.format("YYYY-MM-DD")}&fechaFin=${fechaFin.format("YYYY-MM-DD")}`;
            enviarServidor(url, ReporteDetallado);
        }
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
    var btnAgregar = gbi("btnAgregar");
    btnAgregar.onclick = function () {
        let count = 0;  // 30;
        count = count + 1;
        var contenido = "";
        contenido += '<div class="row panel salt" id="num' + count + '" tabindex="100" style="padding:3px 10px;margin-bottom:2px;cursor:pointer;">';
        contenido += '<div class="col-12 col-md-2" style="display:none;"><span id="tp-0">0</span></div>';
        contenido += '<div class="col-12 col-md-2" style="display:none;"><span id="tp-1">0</span></div>';
        contenido += '<div class="col-12 col-md-4" style="padding-top:5px;"><span id="tp-2">' + gbi("txtDescripcion").value + '</span></div>';
        contenido += '<div class="col-12 col-md-1" style="padding-top:5px;"><span id="tp-3">' + gbi("txtCantidad").value + '</span></div>';
        contenido += '<div class="col-12 col-md-1" style="padding-top:5px;"><span id="tp-4">' + gbi("txtMonto").value + '</span></div>';
        contenido += '<div class="col-12 col-md-1" ><span class="d-sm-none">Gastos : </span><span id="tp-5"><input type="number" min="0" max="100" step="5" class="form-control  form-control-sm" onkeypress="return IngresoNumero(event)" onchange="calcularComision(this)" onkeyup="calcularComision(this)" value="0.00"></span></div>';//style="display:none;"
        contenido += '<div class="col-12 col-md-1" style="padding-top:5px;"><span id="tp-6">' + gbi("txtMonto").value + '</span></div>';//display:none;
        contenido += '<div class="col-12 col-md-1"><span class="d-sm-none">% : </span><span id="tp-7"><input type="number" min="0" max="100" step="5" class="form-control  form-control-sm" onkeypress="return IngresoNumero(event)" onchange="calcularComision(this)" onkeyup="calcularComision(this)" value="0.00"></span></div>';
        contenido += '<div class="col-12 col-md-1" style="padding-top:5px;"><span id="tp0-8">0.000</span></div>';
        contenido += '<div class="col-12 col-md-1"><div class="row saltbtn">';
        contenido += '<div class="col-12"><button type="button" class="btn btn-sm waves-effect waves-light btn-info pull-right" style="padding:3px 10px;" onclick="EliminarFila(this)"> <i class="fa fa-trash-o fs-11"></i> </button></div>';
        contenido += '</div></div>';
        contenido += '</div>';
        gbi("contentPrincipal").innerHTML += contenido;
        gbi("txtDescripcion").value = "";
        gbi("txtMonto").value = "";
        CerrarModal("modal-Modal");
    }
    var btnGrabarComisionMedico = gbi("btnGrabarComisionMedico");
    btnGrabarComisionMedico.onclick = function () {
        var listaComisionxDia = gbi("contentPrincipal").children;
        var idComisionMedico; var idServicio; var FechaComision; var Tratamiento; var NroTratamiento; var Gasto; var PorcentajeMedico;
        if (listaComisionxDia.length >= 1) {
            for (var i = 0; i < listaComisionxDia.length; i++) {
                idComisionMedico = listaComisionxDia[i].children[0].innerText;
                idServicio = listaComisionxDia[i].children[1].innerText;
                //FechaComision = listaComisionxDia[i].children[2].innerText;
                Tratamiento = listaComisionxDia[i].children[2].innerText;
                NroTratamiento = listaComisionxDia[i].children[3].innerText;
                Gasto = listaComisionxDia[i].children[5].querySelector("input").value;
                PorcentajeMedico = listaComisionxDia[i].children[7].querySelector("input").value;
                Costo = listaComisionxDia[i].children[6].innerText;
                var url = "Comision_Medico/Grabar";
                var frm = new FormData();
                frm.append("idComisionMedico", idComisionMedico || 0);
                frm.append("idCita", 0);
                frm.append("idServicio", idServicio || 0);
                frm.append("FechaCreacion", $('#txtFilFecIn').data("DateTimePicker").date().format("YYYY-MM-DD") || 0);
                frm.append("FechaComision", $('#txtFilFecFn').data("DateTimePicker").date().format("YYYY-MM-DD") || 0);
                frm.append("Tratamiento", Tratamiento || 0);
                frm.append("NroTratamiento", NroTratamiento || 0);
                frm.append("Gasto", Gasto || 0);
                frm.append("Costo", Costo || 0);
                frm.append("PorcentajeMedico", PorcentajeMedico || 0);
                frm.append("idPersonal", gbi("cboMedico").value || 0);
                frm.append("FecIni", $('#txtFilFecIn').data("DateTimePicker").date().format("YYYY-MM-DD"));
                frm.append("FecFin", $('#txtFilFecFn').data("DateTimePicker").date().format("YYYY-MM-DD"));

                swal({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
                enviarServidorPost(url, actualizarListar, frm);
            }
        }
    }
}
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}

gbi("txtCantidad").onfocus = function () { this.select(); }
gbi("txtPrecio").onfocus = function () { this.select(); }

function validarFormulario() {
    var error = true;
    if (validarControl("txtFilFecIn")) error = false;
    if (validarControl("txtFilFecFn")) error = false;
    if (validarControl("cboMedico")) error = false;
    return error;
}
function calcularComision(input) {
    if (input) {
        let row = input.parentElement.parentElement.parentElement;
        let costo = parseFloat(row.children[4].innerText);
        costo = costo || 0;
        let gasto = parseFloat(row.children[5].querySelector("input").value);
        gasto = gasto || 0;
        let porc = parseFloat(row.children[7].querySelector("input").value);
        porc = (porc || 0) / 100;
        let diferencia = costo - gasto;

        row.children[6].innerText = diferencia.toFixed(2);
        row.children[8].innerText = (diferencia * porc).toFixed(2);
    }

    let comision = 0;
    let contenedor = gbi("contentPrincipal");
    let colm = document.getElementsByClassName("row panel salt");
    for (var i = 0; i < colm.length; i++) {
        let itemComosion = parseFloat(colm[i].children[8].innerText);
        comision += (itemComosion || 0);
    }

    //console.log(colm);
    //console.log(colm.length);
    //contenedor.querySelectorAll(".row.panel.salt").forEach(item => {
    //    let itemComosion = parseFloat(item.children[7].innerText);
    //    comision += (itemComosion || 0);
    //});
    gbi("txtMontoComision").value = comision.toFixed(2);
    CalcularTotal();
}
function CalcularTotal() {
    var comisionMonto = gbi("txtMontoComision").value;
    var PorcentajeComision = 0;
    var TotalMonto = 0;
    //PorcentajeComision = comisionMonto - (comisionMonto / 1.08);
    PorcentajeComision = comisionMonto * 0.08;
    TotalMonto = comisionMonto - PorcentajeComision;
    gbi("txtMontoTotal").value = TotalMonto.toFixed(2);
    gbi("txtDscto").value = PorcentajeComision.toFixed(2);
}
function guardarComision(element) {
    let row = element.parentElement.parentElement.parentElement.parentElement;

    var url = "Comision_Medico/Grabar";
    var frm = new FormData();
    frm.append("idComisionMedico", row.children[0].lastElementChild.innerText || 0);
    frm.append("idCita", 0);
    frm.append("idServicio", row.children[1].lastElementChild.innerText || 0);
    frm.append("FechaComision", row.children[2].lastElementChild.innerText || 0);
    frm.append("Tratamiento", row.children[3].lastElementChild.innerText || 0);
    frm.append("NroTratamiento", row.children[4].lastElementChild.innerText || 0);
    frm.append("Gasto", row.children[6].querySelector("input").value || 0);
    frm.append("PorcentajeMedico", row.children[8].querySelector("input").value || 0);
    frm.append("idPersonal", gbi("cboMedico").value || 0);
    frm.append("FecIni", $('#txtFilFecIn').data("DateTimePicker").date().format("YYYY-MM-DD"));
    frm.append("FecFin", $('#txtFilFecFn').data("DateTimePicker").date().format("YYYY-MM-DD"));

    swal({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
    enviarServidorPost(url, actualizarListar, frm);
}
function EliminarFila(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function mostrarDetalle(rpta) {
    AbrirModal('modal-Modal');
}
function MontoTotal() {
    var cantidad = gbi("txtCantidad").value;
    var Precio = gbi("txtPrecio").value;
    var monto = 0;
    monto = parseFloat(cantidad * Precio).toFixed(2);
    gbi("txtMonto").value = monto;
}
//Faltaria reporte general y reporte detallado 
function ReporteGeneral(rpta) {
    var listas = rpta.split('↔');
    var Resultado = listas[0];
    var datos = listas[3].split('▲');
    var det = listas[2].split("▼");
    var texto = "";
    var doc = new jsPDF('p', 'mm', 'A4');
    var imggg = logoDermosalud;
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    doc.setFont('arial')
    var xic = 35;
    var altc = 6;
    doc.addImage(imggg, "JPEG", 15, 15, 50, 10);

    doc.setFontSize(12);
    doc.setFontType("bold");
    doc.text("NOMBRE ", 15, xic);
    doc.text("DOCUMENTO ", 15, xic + altc * 1);
    doc.text("FECHA ", 15, xic + altc * 2);
    doc.setFontType("normal");
    doc.line(48, xic + altc * 0 + 1, 130, xic + altc * 0 + 1);
    doc.line(48, xic + altc * 1 + 1, 130, xic + altc * 1 + 1);
    doc.line(48, xic + altc * 2 + 1, 130, xic + altc * 2 + 1);
    doc.text(" " + datos[0] + " " + datos[1] + " " + datos[2], 48, xic);
    doc.text(" " + datos[3], 48, xic + altc * 1);
    doc.text(" " + gbi("txtFilFecIn").value + " al " + gbi("txtFilFecFn").value, 48, xic + altc * 2);
    //Crear Cabecera
    var xid = 60;
    var xad = 5;
    var n = 0;
    doc.setFontSize(11);
    doc.rect(15, xid - 4, width - 25, 6);
    doc.setFontType("bold");
    doc.text("CANTIDAD", 17, xid);
    doc.text("SERVICIO", 53, xid);
    doc.text("MONTO", 180, xid);
    //crea Detalle
    var x = det;
    var xi = 0;
    xid = 68;
    var xiDetalle = 0;
    var altDetalle = 50;
    var TablaDetalle = gbi("contentPrincipal");
    var iDetallealtura = 0;
    doc.setFontType("normal");
    for (var i = 0; i < x.length; i++) {
        for (var j = 0; j < TablaDetalle.children.length; j++) {
            if (xid + (n * xad) + xad > height - 20) {
                doc.addPage();
                n = 0;
                xid = 20;
                iDetallealtura = 0;
            }
            if (x[i].split("▲")[1] == TablaDetalle.children[j].children[1].innerText && x[i].split("▲")[2] == TablaDetalle.children[j].children[2].innerText) {
                doc.text(x[i].split("▲")[3], 28, xid + (iDetallealtura * xad), "right"); // cantidad
                doc.text(x[i].split("▲")[2], 53, xid + (iDetallealtura * xad)); // servicio
                doc.text(parseFloat(x[i].split("▲")[8]).toFixed(2), 195, xid + (iDetallealtura * xad), "right"); // total
                iDetallealtura++; n++;
            }
        }
        xidDetalle = xid + ((iDetallealtura * xad));
    }
    xid = 60;
    doc.line(15, xid + 2, 15, xidDetalle);       //|
    doc.line(40, xid - 4, 40, xidDetalle);       //.|
    doc.line(170, xid - 4, 170, xidDetalle);     //..|   
    doc.line(width - 10, xid + 2, width - 10, xidDetalle);  //...|
    doc.line(15, xidDetalle, width - 10, xidDetalle);//____

    doc.setFontType("bold");
    doc.text("MONTO COMISION ", 130, xidDetalle + 6)
    doc.rect(width - 40, xidDetalle, 30, 8);
    doc.text(gbi("txtMontoComision").value, width - 12, xidDetalle + 6, "right");

    doc.text("DSCTO 8% ", 130, xidDetalle + 14)
    doc.rect(width - 40, xidDetalle + 8, 30, 8);
    doc.text(gbi("txtDscto").value, width - 12, xidDetalle + 14, "right");

    doc.text("TOTAL ", 130, xidDetalle + 22)
    doc.rect(width - 40, xidDetalle + 16, 30, 8);
    doc.text(gbi("txtMontoTotal").value, width - 12, xidDetalle + 22, "right");
    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');

}
function ReporteDetallado(rpta) {
    var listas = rpta.split('↔');
    var Resultado = listas[0];
    var datos = listas[3].split('▲');
    var det = listas[2].split("▼");
    var detallado = listas[4].split("▼");
    var texto = "";
    var doc = new jsPDF('p', 'mm', 'A4');
    var imggg = logoDermosalud;
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    doc.setFont('arial')
    var xic = 35;
    var altc = 6;
    doc.addImage(imggg, "JPEG", 15, 15, 50, 10);

    doc.setFontSize(9);
    doc.setFontType("bold");
    doc.text("NOMBRE ", 15, xic);
    doc.text("DOCUMENTO ", 15, xic + altc * 1);
    doc.text("FECHA ", 15, xic + altc * 2);
    doc.setFontType("normal");
    doc.line(48, xic + altc * 0 + 1, 130, xic + altc * 0 + 1);
    doc.line(48, xic + altc * 1 + 1, 130, xic + altc * 1 + 1);
    doc.line(48, xic + altc * 2 + 1, 130, xic + altc * 2 + 1);
    doc.text(" " + datos[0] + " " + datos[1] + " " + datos[2], 48, xic);
    doc.text(" " + datos[3], 48, xic + altc * 1);
    doc.text(" " + gbi("txtFilFecIn").value + " al " + gbi("txtFilFecFn").value, 48, xic + altc * 2);

    //Crear Cabecera
    var xid = 60;
    var xad = 5;
    doc.setFontSize(8);
    doc.rect(15, xid - 4, width - 25, 6);
    doc.setFontType("bold");
    doc.text("SERVICIO", 17, xid);
    doc.text("MONTO", 180, xid);
    //crea Detalle
    var x = det;
    var xi = 0;
    xid = 68;
    var xiDetalle = 0;
    var altDetalle = 50;
    var TablaDetalle = gbi("contentPrincipal");
    var iDetallealtura = 0;
    var existe = 0;
    var n = 0;
    doc.setFontType("normal");
    for (var i = 0; i < x.length; i++) {
        for (var j = 0; j < TablaDetalle.children.length; j++) {

            if (xid + (n * xad) + xad > height - 30) {
                doc.addPage();
                n = 0;
                xid = 20;
                iDetallealtura = 0;
            }
            if (x[i].split("▲")[1] == TablaDetalle.children[j].children[1].innerText && x[i].split("▲")[2] == TablaDetalle.children[j].children[2].innerText) {
                doc.setFontType("bold");
                doc.text(x[i].split("▲")[2], 18, xid + (iDetallealtura * xad)); // servicio
                doc.text(parseFloat(x[i].split("▲")[8]).toFixed(2), 195, xid + (iDetallealtura * xad), "right"); // total
                iDetallealtura++;
                n += 1;
                for (var k = 0; k < detallado.length; k++) {
                    if (detallado[k].split("▲")[1] == x[j].split("▲")[1] && detallado[k].split("▲")[2] == x[j].split("▲")[2]) {
                        doc.setFontType("normal");

                        if (xid + (n * xad) + xad > height - 30) {
                            doc.addPage();
                            n = 0;
                            xid = 20;
                            iDetallealtura = 0;
                        }
                        doc.text(detallado[k].split("▲")[0], 24, xid + (iDetallealtura * xad)); // fecha
                        doc.text(detallado[k].split("▲")[3], 55, xid + (iDetallealtura * xad)); // nombre de cliente
                        iDetallealtura++;
                        n += 1;
                    }
                }
                existe = 1;
            }
        }
        if (existe > 0) {
            xidDetalle = xid + ((iDetallealtura * xad));
            //doc.line(15, xidDetalle, width - 10, xidDetalle);//____
            iDetallealtura++;
            n += 1;
        }
        existe = 0;
    }
    xid = 60;
    //doc.line(15, xid + 2, 15, xidDetalle);       //|
    //doc.line(170, xid - 4, 170, xidDetalle);     //..|   
    //doc.line(width - 10, xid + 2, width - 10, xidDetalle);  //...|
    //doc.line(15, xidDetalle, width - 10, xidDetalle);//____

    doc.setFontType("bold");
    doc.text("MONTO COMISION ", 135, xidDetalle + 6)
    doc.rect(width - 40, xidDetalle, 30, 8);
    doc.text(gbi("txtMontoComision").value, width - 12, xidDetalle + 6, "right");

    doc.text("DSCTO 8% ", 135, xidDetalle + 14)
    doc.rect(width - 40, xidDetalle + 8, 30, 8);
    doc.text(gbi("txtDscto").value, width - 12, xidDetalle + 14, "right");

    doc.text("TOTAL ", 135, xidDetalle + 22)
    doc.rect(width - 40, xidDetalle + 16, 30, 8);
    doc.text(gbi("txtMontoTotal").value, width - 12, xidDetalle + 22, "right");
    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');

}