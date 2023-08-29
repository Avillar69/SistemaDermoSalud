var cabeceras = ["id", "Fecha", "Serie", "Numero", "Descripcion", "Pago", "Operacion", "Monto"];
var cabeceras2 = ["DOCUMENTO", "RAZON SOCIAL", "MONTO TOTAL", "MONTO APLICADO", "MONTO POR PAGAR"];
var listaDatos;
var matriz = [];
var txtModal;
var txtModal2;
var listaLocales;
var listaMoneda;
var listaFormaPago;
var listaComprobantes;
var listaTipoCompra;
var listaSocios;
var FechaActual;
//Inicializando
var idTablaDetalle;
var idDiv;
var ListaPago = [];
/*ingrese una variable global para capturar los datos del documento por el id */
//configNav();
var datosDelDocumento = [];
var url = "PagarSocio/ObtenerDatosPagoDetalle?id=1";
enviarServidor(url, mostrarLista);
$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFechaPago").datetimepicker({
    format: 'DD-MM-YYYY',

});
$("#txtFechaCobroCheque").datetimepicker({
    format: 'DD-MM-YYYY',

});
$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',

});
$("#date-range").datetimepicker({
    format: 'DD-MM-YYYY',
});
$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
configBM();
reziseTabla();
cfgKP(["txtDocumento", "txtRazonSocial", "txtCuentaDestino", "txtCuentaOrigen", "txtTipoOperacion"], cfgTMKP);
cfgKP(["txtTotal", "txtMontoAplicado", "txtNOperacion", "txtFechaPago", "txtFechaCobroCheque", "txtMontoCancelar", "txtTipoOperacion", "txtObservacion"], cfgTKP);
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}

function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        listaSocios = listas[4].split("▼");
        listaMoneda = listas[5].split("▼");

        ListaPago = listas[6].split("▼");
        if (Resultado == "OK") {
            listaDatos = listas[1].split("▼");
            var fechaInicio = listas[2];
            var fechaFin = listas[3];
            FechaActual = listas[3];
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;
            gbi("txtFechaPago").value = FechaActual;
            gbi("txtFechaCobroCheque").value = FechaActual;
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }

        llenarCombo(listaMoneda, "cboMoneda", "Seleccione");
        listar();
    }
    reziseTabla();
}
function TraerDetalle(id) {
    var url = "PagarSocio/ObtenerDatosxIdDetalle?IdDetalle=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        var dir = listas[2].split("▼");
        var det = listas[3].split("▼");
        if (Resultado == 'OK') {
            var temporal = datos[33] + '-' + datos[34];
            adc(ListaPago, datos[1], "txtDocumento", 1);
            adt(datos[32], "txtFecha");
            adt(datos[27], "txtRazonSocial");
            adt(datos[28], "txtTotal");
            adt(datos[30], "txtMontoAplicado");
            adt(datos[31], "txtDiferencia");
            adt(datos[8], "cboMoneda");
            adt(datos[13], "txtMontoCancelar");
            adt(datos[23], "txtFechaPago", 1);
            adt(datos[3], "txtTipoOperacion");
            if (datos[3] == 1) {
                var lblTituloPanel = document.getElementById('lblOperacion');
                lblTituloPanel.innerHTML = "N°.Operacion";
                gbi("divCuentaOrigen").style.display = "";
                gbi("divCuentaDestino").style.display = "";
                gbi("divOperacion").style.display = "";
                gbi("divFCC").style.display = "none";

                limpiarControl("txtNOperacion");
                limpiarControl("txtCuentaDestino");
                limpiarControl("txtCuentaOrigen");
            } else if (datos[3] == 2) {
                var lblTituloPanel = document.getElementById('lblOperacion');
                lblTituloPanel.innerHTML = "N°.Cheque";//Titulo Insertar
                gbi("divCuentaOrigen").style.display = "none";
                gbi("divCuentaDestino").style.display = "none";
                gbi("divOperacion").style.display = "";
                gbi("divFCC").style.display = "";
                limpiarControl("txtCuentaDestino");
                limpiarControl("txtCuentaOrigen");
            }
            else {
                var lblTituloPanel = document.getElementById('lblOperacion');
                lblTituloPanel.innerHTML = "N°.Operacion";
                gbi("divCuentaDestino").style.display = "";
                gbi("divOperacion").style.display = "";
                gbi("divFCC").style.display = "none";
                gbi("divCuentaOrigen").style.display = "none";
                limpiarControl("txtFechaCobroCheque");

                limpiarControl("txtCuentaOrigen");
            }
            adt(datos[19], "txtObservacion", 1);
            gbi("txtCuentaOrigen").dataset.id = datos[11];
            adt(datos[12], "txtCuentaOrigen");
            gbi("txtCuentaDestino").dataset.id = datos[21];
            adt(datos[22], "txtCuentaDestino");


            var idPago = gbi("txtDocumento").dataset.id;
            var url = 'Pago/ObtenerDatosxIDPagoCompras?id=' + idPago;
            enviarServidor(url, CargarDetalleOC);

            adt(datos[10], "txtNOperacion");
            adt(datos[23], "txtFechaCobroCheque");
            adt(datos[0], "txtID");
            adt(datos[1], "txtID2");

        }
    }
}
function TimeStamp() {
    var t = new Date();
    var ss = t.substring()
}
var btnPDF = gbi("btnImprimirPDF");
btnPDF.onclick = function () {
    ExportarPDFs("p", "Pagar Socio", cabeceras, matriz, "Pago por Socios", "a4", "e");
}
var btnImprimir = document.getElementById("btnImprimir");
btnImprimir.onclick = function () {
    ExportarPDFs("p", "Pagar Socio", cabeceras, matriz, "Pago por Socios", "a4", "i");
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
function JsonParse(data) {

}
function cfgSOC(evt) {
    switch (evt.srcElement.id) {
        case "cboLocalDet":
            var urlC = 'ConsultaStock/ObtenerAlmacen?pL=' + cboLocalDet.value;
            enviarServidor(urlC, cargarAlmacenDet);
            gbi("cboAlmacenDet").focus();
            break;
        case "cboAlmacenDet":
            gbi("cboTipoCompra").focus();
            break;
        default:

    }

}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);//true es cuando tiene filtro
            lblTituloPanel.innerHTML = "Nuevo Pago";//Titulo Insertar
            document.getElementById("btnModalDocumento").disabled = false;
            gbi("txtFechaPago").value = FechaActual;
            gbi("txtFechaCobroCheque").value = FechaActual;
            gbi("txtTipoOperacion").value = "1";
            gbi("txtTipoOperacion").onchange();
            break;
            gbi("btnCancelarDetalle").style.display = "";
        case 2:
            lblTituloPanel.innerHTML = "Editar Pago";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            document.getElementById("btnModalDocumento").disabled = true;
            break;
    }
}
$('#collapseOne-2').on('shown.bs.collapse', function () {
    reziseTabla();
})
$('#collapseOne-2').on('hidden.bs.collapse', function () {
    reziseTabla();
})

function crearTablaCompras(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info d-none d-md-flex' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 2: case 3: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            case 5:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 6:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-4'>";
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
    configurarFiltrok();
    matriz = crearMatrizReporte(listaDatos);
    configurarFiltrok(cabeceras);
    mostrarMatrizOC(matriz, cabeceras, "divTabla", "contentPrincipal");
    //configurarBotonesModal();
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
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 2: case 3: case 1: case 5: case 7:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 6:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
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
function mostrarMatrizOC(matriz, cabeceras, tabId, contentID) {
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
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 2: case 3: case 7:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 6:
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
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;'  onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o'></i> </button>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;'  onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil'></i></button>";

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
function CTM() {

    var contenido = "";
    contenido += "<div class='panel panel-default' style='margin-bottom:10px;'>";
    contenido += "  <div id='collapse" + i + "' class='panel-collapse collapse in' role='tabpanel' aria-labelledby='heading" + i + "'>";
    contenido += "      <div class='panel-body'  style='padding: 10px 20px 20px 10px'>";
    contenido += "          <div class='row' style='padding-left:20px;padding-right:20px'>";
    contenido += "              <div class='col-xs-12 col-md-2'>";
    contenido += "                  <label style='padding-top:5px;'> Código : " + matriz[i][4] + "</label>";
    contenido += "              </div>";
    contenido += "              <div class='col-xs-12 col-md-4'>";
    contenido += "                  <label style='padding-top:5px;'> Categoría : " + matriz[i][5] + "</label>";
    contenido += "              </div>";
    contenido += "              <div class='col-xs-12 col-md-4'>";
    contenido += "                  <label style='padding-top:5px;'> Descripción : " + matriz[i][6] + "</label>";
    contenido += "              </div>";
    contenido += "              <div class='col-xs-12 col-md-2'>";
    contenido += "                  <label style='padding-top:5px;'> Stock Actual : " + matriz[i][7] + "</label>";
    contenido += "              </div>";
    contenido += "          </div>";
    contenido += "      </div>";
    contenido += "  </div>";
    contenido += "</div>";
}
function isNumberKey(evt) {

}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó el Pago';
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
    //event.target || event.srcElement
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {


            case "txtMontoCancelar":
                gbi("txtFechaPago").focus();
                break;
            case "txtFechaPago":
                gbi("txtTipoOperacion").focus();
                break;

            case "txtTipoOperacion":
                if (gvc("txtTipoOperacion") == 1) {
                    gbi("txtCuentaOrigen").focus();

                }
                else if (gvc("txtTipoOperacion") == 2) {
                    gbi("txtNOperacion").focus();
                }
                else if (gvc("txtTipoOperacion") == 3) {
                    gbi("txtCuentaDestino").focus();
                }

                break;
            case "txtCuentaOrigen":
                gbi("txtCuentaDestino").focus();
                break;
            case "txtCuentaDestino":
                gbi("txtNOperacion").focus();
                break;
            case "txtNOperacion":
                gbi("txtFechaCobroCheque").focus();
                break;
            case "txtFechaCobroCheque":
                gbi("txtObservacion").focus();
                break;


            //case "txt"
            default:

        }
        return true;
    }
    else {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        var valor;
        switch (o) {
            case "txtCantidad":
            case "txtPrecio":
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    valor = false;
                }
                else {
                    valor = true;
                }
                var c = gbi("txtCantidad").value == "" ? 0 : parseFloat(gbi("txtCantidad").value);
                var p = gbi("txtPrecio").value == "" ? 0 : parseFloat(gbi("txtPrecio").value);

                gbi("txtTotal").value = parseFloat(c * p).toFixed(2);
                return valor;
                break;
            default:
                break;

        }
    }
}
function cargarAlmacen(rpta) {
    if (rpta.split('↔')[0] == "OK") {
        var listas = rpta.split('↔');
        listaAlmacenes = listas[2].split("▼");
        llenarCombo(listaAlmacenes, "cboAlmacen", "Seleccione");
    }
}
//Carga con botones de Modal desde URL
function cbmu(ds, t, tM, tM2, cab, u, m) {
    document.getElementById("div_Frm_Modal").innerHTML = document.getElementById("div_Frm_Detalle").innerHTML;
    document.getElementById("btnGrabar_Modal").dataset.grabar = ds;
    document.getElementById("lblTituloModal").innerHTML = t;
    var txtCodigo_FormaPago = document.getElementById("txtCodigo_Detalle");
    txtCodigo_FormaPago.disabled = true;
    txtCodigo_FormaPago.placeholder = "Autogenerado";
    var txtM1 = document.getElementById(tM);
    txtModal = txtM1;
    tM2 == null ? txtModal2 = tM2 : txtModal2 = document.getElementById(tM2);
    cabecera_Modal = cab;
    enviarServidor(u, m);
}
//Carga con botones de Modal sin URL (Con datos dat[])
function cbm(ds, t, tM, tM2, cab, dat, m) {
    document.getElementById("div_Frm_Modal").innerHTML = document.getElementById("div_Frm_Detalle").innerHTML;
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
//Configurar botones de Modal
function configBM() {
    var btnModDocumento = gbi("btnModalDocumento");
    btnModDocumento.onclick = function () {
        cbmu("l.pagos", "Lista de Pagos", "txtDocumento", null,
            ["id", "Fecha", "Razon", "Serie", "Numero Dcto", "Debe"], "/PagarSocio/ListarPagosPendientes?id=" + 1, cargarLista);
    };
    var btnModCuentaOrigen = gbi("btnModalCuentaOrigen");
    btnModCuentaOrigen.onclick = function () {
        cbmu("cuentasOrigen", "Cuentas Origen", "txtCuentaOrigen", null,
            ["id", "Banco", "Numero", "Cuenta"], "/PagarSocio/ObtenerCuentasOrigen", cargarLista);
    };
    var btnModCuentaDestino = gbi("btnModalCuentaDestino");
    btnModCuentaDestino.onclick = function () {
        if (datosDelDocumento.length > 0) {
            cbmu("cuentasDestino", "Cuentas Destino", "txtCuentaDestino", null,
                ["id", "Descripcion", "Cuenta"], "/PagarSocio/ObtenerCuentasSocio?id=" + datosDelDocumento[4], cargarLista);
        } else {
            mostrarRespuesta("Info", "Debe elegir un documento.", "warning");
        }
    };
    function cargarBusqueda(rpta) {
        if (rpta != "") {

            var data = rpta.split('↔');
            var res = data[0];
            var mensaje = '';
            var tipo = '';
            var txtCodigo = document.getElementById('txtID');
            var codigo = txtCodigo.value;
            if (res == 'OK') {
                listaDatos = data[2].split('▼');

                listar();
            }
            else {
                mensaje = data[1];
                tipo = 'error';
                mostrarRespuesta(res, mensaje, tipo);
            }

        }
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "PagarSocio/grabar";

            var MontoCancelar = parseFloat(gvt("txtMontoCancelar"));
            var Diferencia = parseFloat(gvt("txtDiferencia"));
            if (MontoCancelar > Diferencia || MontoCancelar <= 0) {
                mostrarRespuesta("Error", "El monto a pagar no puede ser mayor al Saldo actual", "error");
                return;
            }
            var frm = new FormData();
            if (datosDelDocumento.length <= 0) {
                frm.append("idPagoDetalle", gvt("txtID"));
                frm.append("idPago", gvt("txtID2"));
            }
            else {

                frm.append("idPagoDetalle", gvt("txtID"));
                frm.append("idPago", datosDelDocumento[0]);
            }
            frm.append("idCajaDetalle", 1);//gvt texto 
            frm.append("idTipo", gvt("txtTipoOperacion"));
            frm.append("idDocumento", datosDelDocumento[3]);
            frm.append("idEmpresa", 1);
            frm.append("idConcepto", 0);
            frm.append("Concepto", "  ");
            frm.append("idFormaPago", gvt("cboMoneda"));
            frm.append("DescripcionFormaPago", $("#cboMoneda option:selected").text());
            frm.append("NumeroOperacion", gvt("txtNOperacion"));
            frm.append("idCuentaBancario", gvc("txtCuentaOrigen"));
            frm.append("NumeroCuenta", gvt("txtCuentaOrigen"));

            frm.append("idCuentaBancarioDestino", gvc("txtCuentaDestino"));/////////
            frm.append("NumeroCuentaDestino", gvt("txtCuentaDestino"));
            frm.append("Monto", gvt("txtMontoCancelar"));

            frm.append("Estado", true);
            frm.append("Observacion", gvt("txtObservacion").length == 0 ? "-" : gvt("txtObservacion"));
            frm.append("DescripcionOperacion", $("#txtTipoOperacion option:selected").text());

            if (gvc("txtTipoOperacion") == 2) {
                frm.append("FechaDetalle", gvt("txtFechaCobroCheque"));
            } else {
                frm.append("FechaDetalle", gvt("txtFechaPago"));
            }

            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
            datosDelDocumento = [];
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }
}
function BuscarxFecha(f1, f2) {
    var url = 'PagarSocio/ObtenerPorFecha?fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
}
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        matriz = crearMatriz(listaDatos);
        //configurarFiltro(cabeceras);
        mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
        reziseTabla();
    }
}
function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[3].dataset.id;
    gbi("txtArticulo").value = row.children[3].innerHTML;
    gbi("txtCategoria").dataset.id = row.children[2].dataset.id;
    gbi("txtCategoria").value = row.children[2].innerHTML;
    gbi("txtCantidad").value = row.children[4].innerHTML;
    gbi("txtPrecio").value = row.children[6].innerHTML;
    gbi("txtTotal").value = row.children[7].innerHTML;

    gbi("btnGrabarDetalle").innerHTML = "Actualizar";
    idTablaDetalle = id;
    //para que aparescan , ya que en el html  esta en style="display:none;"
    gbi("btnGrabarDetalle").style.display = "";
    gbi("btnCancelarDetalle").style.display = "";

}
function validarFormulario() {
    var error = true;
    //Aqui Ingresar todos los campos a validar || validarCombo || validarTexto

    if (validarControl("txtDocumento")) error = false;
    if (validarControl("txtNOperacion")) error = false;
    if (validarControl("txtMontoCancelar")) error = false;
    if (validarControl("txtMontoCancelar")) error = false;
    if (validarControl("txtFechaPago")) error = false;

    if (gbi("txtTipoOperacion").value == "1") {
        if (validarControl("txtCuentaOrigen")) error = false;
        if (validarControl("txtCuentaDestino")) error = false;
    } else if (gbi("txtTipoOperacion").value == "3") {
        if (validarControl("txtCuentaDestino")) error = false;
    }

    return error;
}
function guardarItemDetalle() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[3].dataset.id = gbi("txtArticulo").dataset.id;
    row.children[3].innerHTML = gbi("txtArticulo").value;
    row.children[2].dataset.id = gbi("txtCategoria").dataset.id;
    row.children[2].innerHTML = gbi("txtCategoria").value;
    row.children[4].innerHTML = gbi("txtCantidad").value;
    row.children[6].innerHTML = gbi("txtPrecio").value;
    row.children[7].innerHTML = gbi("txtTotal").value;
    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
}
function eliminarDetalle(id) {

    limpiarControl("txtCantidad");
    bDM("txtCategoria");
    bDM("txtArticulo");
    bDM("txtPrecio");
    bDM("txtTotal");

    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
//Agregar Item a Tabla Detalle
function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvc("txtCategoria")) + '">' + (tipo == 1 ? data[5] : gvt("txtCategoria")) + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[16] : 'Und.') + '</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[7]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[9]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-1">';
    contenido += '      <div class="row rowDetbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalle(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItem(\"gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleF").innerHTML += contenido;
}
//Crear Detalle como Cadena para env Serv.
function crearCadDetalleOrden() {
    //ingresar los datos correctos
    var cdet = "";
    $(".rowDet").each(function (obj) {

        cdet += $(".rowDet")[obj].children[0].innerHTML;//idOrdenCompraDetalle
        cdet += "|" + $(".rowDet")[obj].children[1].innerHTML;//idCompra
        cdet += "|" + $(".rowDet")[obj].children[3].dataset.id;//idCategoria
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;//descripcionCategoria
        cdet += "|" + $(".rowDet")[obj].children[2].dataset.id;//idArticulo
        cdet += "|" + $(".rowDet")[obj].children[2].innerHTML;//DescripcionArticulo
        cdet += "|" + $(".rowDet")[obj].children[4].innerHTML;//Cantidad
        //cdet += "|" + $(".rowDet")[obj].children[5].innerHTML;//Unidad
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//Precio
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//Precio
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//Total-nacional
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//Total-nacional
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
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
                mensaje = 'Se adicionó el Registro';
                tipo = 'success';
            }
            else {
                mensaje = data[1];
                tipo = 'error';
            }
        }
        else {
            if (res == 'OK') {
                mensaje = 'Se actualizó el Registro';
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
function ValorMayorNegativo(id) {

    swal({
        title: 'Error',
        text: 'La cantiadad excede o es negativa.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'ingrese un valor menor al que debe pagar!',
        closeOnConfirm: false,
        closeOnCancel: false
    });
}
function eliminar(id) {

    swal({
        title: 'Desea Eliminar el Pago? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var u = "PagarSocio/Eliminar?idPagoDetalle=" + id;
                enviarServidor(u, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó el Pago', 'error');
            }
        });
}
//Borrar Data del Modal x ID
function bDM(v) {
    gbi(v).value = "";
    gbi(v).dataset.id = "";
}
function limpiarTodo() {

    limpiarControl("txtID");
    limpiarControl("txtID2");
    limpiarControl("txtDocumento");
    limpiarControl("txtTotal");
    limpiarControl("cboMoneda");
    limpiarControl("txtFecha");
    limpiarControl("txtMontoAplicado");
    limpiarControl("txtRazonSocial");
    limpiarControl("txtDiferencia");
    limpiarControl("txtMontoCancelar");
    limpiarControl("txtFechaPago");
    limpiarControl("txtTipoOperacion");
    limpiarControl("txtObservacion");
    limpiarControl("txtFechaCobroCheque");
    limpiarControl("txtNOperacion");
    limpiarControl("txtCuentaDestino");
    limpiarControl("txtCuentaOrigen");
    limpiarControl("txtFechaPago");
    limpiarControl("txtFechaCobroCheque");
}
function limpiarCamposDetalle() {
    bDM("txtCategoria");
    bDM("txtArticulo");
    gbi("txtCantidad").value = "";
    gbi("txtPrecio").value = "";
    gbi("txtTotal").value = "";
}
function limpiarTablaDetalle() {
    $("#tb_DetalleF").html("");
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtCategoria":
            //var txtCategoria = document.getElementById("txtCategoria");
            gbi("txtArticulo").value = "";
            gbi("txtArticulo").dataset = "";
            var btnModalArticulo = gbi("btnModalArticulo");
            btnModalArticulo.onclick = function () {
                cbmu("articulo", "Articulo", "txtArticulo", null,
                    ["idArticulo", "Código", "Descripción"], "/Articulo/ObtenerDatosxCategoria?Cat=" + gbi("txtCategoria").dataset.id + "&Activo=A", cargarLista);
            }
            return gbi("txtArticulo");
            break;
        case "txtArticulo":
            return gbi("txtCantidad");
            break;
        case "txtRazonSocial":
            return gbi("txtDireccion");
            break;
        case "txtFormaPago":
            return gbi("txtCategoria");
            break;
        case "txtMoneda":
            return gbi("txtFormaPago");
            break;
        case "txtDocumento":
            var idPago = gbi("txtDocumento").dataset.id;
            var url = 'Pago/ObtenerDatosxIDPagoCompras?id=' + idPago;
            enviarServidor(url, CargarDetalleOC);
            return gbi("txtMontoCancelar");
            break;

        case "txtTipoOperacion":
            return gbi("txtCuentaOrigen");
            break;

        case "txtCuentaOrigen":
            return gbi("txtCuentaDestino");
            break;
        case "txtCuentaDestino":
            return gbi("txtNOperacion");
            break;
        case "txtNOperacion":
            return gbi("txtFechaCobroCheque");
            break;
        default:
            break;
    }



    if (txtModal.id == "txtCategoria") {
    }
    if (txtModal.id == "txtArticulo") {
        return gbi("txtCantidad");
    }
    if (txtModal.id == "txtRazonSocial") {
        return gbi("txtDireccion");
    }
}
function cargarAlmacenDet(rpta) {


}
function CerrarModal(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
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
    //) {

    // }
    //var azx = gbi("md" + num + "-3").innerHTML;
    //funcion al dar doble click segun la URL

    txtModal.value = value;
    txtModal.dataset.id = id;
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}
function CargarDetalleOC(rpta) {
    datosDelDocumento = "";
    gbi("tb_DetalleF").innerHTML = "";
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲'); //lista OC
        datosDelDocumento = listas[1].split('▲');
        if (Resultado == 'OK') {
            var temp = datos[6] + '-' + datos[7];
            adt(temp, "txtDocumento");
            adt(datos[13], "txtFecha", 1);
            adt(datos[9], "txtTotal", 1);
            adt(datos[11], "txtMontoAplicado", 1);
            adt(datos[12], "txtDiferencia", 1);
            adc(listaSocios, datos[4], "txtRazonSocial", 1);
            adt(datos[20], "cboMoneda", 1);
        }
    }
}
function buscarXCombo() {
    var comb = document.getElementById("txtTipoOperacion").value;

    gbi("txtCuentaDestino").value = "";
    gbi("txtCuentaDestino").dataset.id = "";
    gbi("txtNOperacion").value = "";
    gbi("txtCuentaOrigen").value = "";
    gbi("txtCuentaOrigen").dataset.id = "";

    limpiarControl("txtCuentaDestino");
    limpiarControl("txtNOperacion");
    limpiarControl("txtCuentaOrigen");

    if (comb == 1) {// cuando es transferencia 

        var lblTituloPanel = document.getElementById('lblOperacion');
        lblTituloPanel.innerHTML = "N°.Operacion";
        gbi("divCuentaOrigen").style.display = "";
        gbi("divCuentaDestino").style.display = "";
        gbi("divOperacion").style.display = "";
        gbi("divFCC").style.display = "none";
    }
    if (comb == 2) {//cuando es cheque
        gbi("divCuentaOrigen").style.display = "none";
        gbi("divCuentaDestino").style.display = "none";
        gbi("divOperacion").style.display = "";
        gbi("divFCC").style.display = "";
        var lblTituloPanel = document.getElementById('lblOperacion');
        lblTituloPanel.innerHTML = "N°.Cheque";//Titulo Insertar



    }
    if (comb == 3) {//cuando es deposito
        var lblTituloPanel = document.getElementById('lblOperacion');
        lblTituloPanel.innerHTML = "N°.Operacion";
        gbi("divCuentaDestino").style.display = "";
        gbi("divOperacion").style.display = "";
        gbi("divFCC").style.display = "none";
        gbi("divCuentaOrigen").style.display = "none";
    }
}
function configurarFiltrok(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatrizOC(matriz, cabe, "divTabla", "contentPrincipal");
    };
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
                contenido += "              <div class='col-12 col-md-4'>";
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
function mostrarMatrizModal(matriz, cabeceras, tabId, contentID, confdbc) {
    if (matriz.length == 0) {

    } else {
        var esBloque = false;
        var contenido = "";
        var nRegistros = matriz.length;
        if (nRegistros > 0) {
            nRegistros = matriz.length;
            var nCampos = matriz[0].length;
            var tbTabla = document.getElementById(tabId);
            tbTabla.style.cursor = "pointer";
            var tipocol = "";
            var tipoColDes = "";
            var tipoColum = "";
            var dat = [];
            for (var i = 0; i < nRegistros; i++) {
                if (i < nRegistros) {
                    var contenido2 = "<div class='row panel salt' id='numMod" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;cursor:pointer;'>";
                    for (var j = 0; j < cabeceras.length; j++) {
                        switch (j) {
                            case 0: case 6:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                            case 2:
                                contenido2 += ("<div class='col-12 col-md-4'>");
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                        }
                        contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='md" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                        contenido2 += "</div>";
                    }
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
        confdbc();
    }
}