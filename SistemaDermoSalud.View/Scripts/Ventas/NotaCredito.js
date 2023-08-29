var cabeceras = ["idNota", "F. Emisión", "Número", "Motivo", "Doc.Ref.", "Cliente"];
var listaDatos;
var txtModal;
var txtModal2;
var listaComprobantes;
var listaTipoCompra;
var listaMotivo;
var listaArticulos;
var listaMoneda;
var listaTipoAfectacion;
var DireccionReciclemos = "AV. NICOLAS AYLLON NRO. 2433 - ATE";
var DireccionProvesur = "CAR.PANAMERICANA SUR NRO. 18 INT. B-14 MUTUAL AYACUCHO SAN JUAN DE MIRAFLORES";

var url = "CuentaCobrar/ObtenerDatos";
enviarServidor(url, mostrarLista);

$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
$("#txtFechaDocFisico").datetimepicker({
    format: 'DD-MM-YYYY',
});
configBM();

cfgKP(["txtNroSerie", "txtRazonSocial", "txtDireccion", "txtMonedaFisico", "txtArticulo"], cfgTMKP);
cfgKP(["txtFecha", "txtDescripcion", "txtSerieNumDocFisico", "txtFechaDocFisico", "txtTipoCompra", "txtCantidad", "txtPrecio", "txtTotal"], cfgTKP);

function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    console.log("hola");
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nueva Nota de Credito";
            gbi("btnGrabar").style.display = "";
            adc(listaMotivo, 1, "txtMotivo", 1);
            //gbi("txtNroSerie").dataset.id = 2;
            //gbi("txtNroSerie").value = "F004";
            obtenerNumero();
            break;
        case 2:
            lblTituloPanel.innerHTML = "Ver Nota de Credito";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            gbi("btnGrabar").style.display = "none";
            break;
        case 3:
            TraerDetalleImpresion(id);
            break;
    }
}
function TraerDetalle(id) {
    var url = 'CuentaCobrar/ObtenerDatosxID?id=' + id;
    enviarServidor_NC(url, CargarDetalles);
}
configNav();
function mostrarLista(rpta) {
    crearTablNota(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listaMotivo = listas[3].split("▼");
            var numeroNotaCredito = listas[4].split("▼");
            listaTipoAfectacion = listas[7].split("▼");
            listaTipoCompra = listas[8].split("▼");
            listaComprobantes = listas[9].split("▼");
            listaArticulos = listas[10].split("▼");
            listaMoneda = listas[11].split("▼");
            gbi("txtFilFecIn").value = listas[5];
            gbi("txtFilFecFn").value = listas[6];
            gbi("txtFecha").value = listas[6];
            gbi("txtNumero").value = pad(numeroNotaCredito[0], 7);
            llenarCombo(listaTipoAfectacion, "cboTipoAfectacion");
            adc(listaMotivo, 1, "txtMotivo", 1);
            gbi("txtDescripcion").value = gbi("txtMotivo").value;
            listar();
            var btnModalMotivo = document.getElementById("btnModalMotivo");
            btnModalMotivo.onclick = function () {
                cbm("Motivo", "Motivo", "txtMotivo", null,
                    ["idMotivo", "Descripción"], listaMotivo, cargarSinXR_G);
            }
            var btnTipoCompra = document.getElementById("btnModalTipoCompra");
            btnTipoCompra.onclick = function () {
                cbm("tipoventa", "Tipo de Venta", "txtTipoCompra", null,
                    ["id", "Código", "Descripción"], listaTipoCompra, cargarSinXR);
            }

            var btnTipoDocumento = document.getElementById("btnModalTipoDocumento");
            btnTipoDocumento.onclick = function () {
                cbm("tipodocumento", "Tipo de Documento", "txtTipoDocumento", null,
                    ["idTipoComprobante", "Descripción"], listaComprobantes, cargarSinXR_G);
            }

            gbi("txtNroSerie").dataset.id = 2;
            gbi("txtNroSerie").value = "FFF1";
            obtenerNumero();
        }
        else {
            mostrarRespuesta(Resultado, listas[1], "error");
        }
    } else {
        swal('Error', 'No hay respuesta del servidor', 'error');
    }
}
function validarAgregarDetalle() {
    var error = true;
    if (validarControl("txtArticulo")) error = false;
    if (validarControl("txtCantidad")) error = false;
    if (validarControl("txtPrecio")) error = false;
    if (validarControl("txtTotal")) error = false;
    return error;
}
function CargarDetalles(rpta) {
    var listas = rpta.split("↔");
    if (listas[0] == "OK") {
        var listaNotaCredito = listas[1].split("▼");
        var listaNotaCreditoDetalle = listas[2].split("▼");
        var objNota = listaNotaCredito[0].split("▲");
        gbi("txtNroSerie").value = objNota[4];
        gbi("txtNumero").value = objNota[5];
        adc(listaMotivo, objNota[9], "txtMotivo", 1);
        gbi("txtFecha").value = objNota[7];
        gbi("txtSerieNumDocFisico").value = objNota[24];
        gbi("txtFechaDocFisico").value = objNota[64];
        gbi("txtRazonSocial").value = objNota[18];
        gbi("txtNroDocumento").value = objNota[29];
        gbi("txtDescripcion").value = objNota[6];
        gbi("txtDireccion").value = objNota[39];
        adc(listaComprobantes, objNota[26], "txtTipoDocumento", 1);
        adc(listaTipoCompra, objNota[30], "txtTipoCompra", 1);
        gbi("txtTotalF").value = objNota[10];
        gbi("txtIGVF").value = objNota[11];
        gbi("txtGravada").value = objNota[12];
        gbi("txtExportacion").value = objNota[33];

        var totalExportacion = parseFloat(objNota[33]);
        if (!isNaN(totalExportacion) && totalExportacion > 0) {
            gbi("txtMotivo").dataset.id = "5";
            gbi("txtMotivo").disabled = true;
        } else {
            gbi("txtMotivo").dataset.id = "1";
            gbi("txtMotivo").disabled = false;
        }

        if (listaNotaCreditoDetalle[0] != "") {
            if (listaNotaCreditoDetalle.length >= 1) {
                for (var i = 0; i < listaNotaCreditoDetalle.length; i++) {
                    addItem(1, listaNotaCreditoDetalle[i].split("▲"));
                }
            }
        }
    } else {
        swal('Error', 'Error al cargar los datos', 'error');
    }
}
function mostrarModalDocVenta() {
    if (gbi("txtRazonSocial").value == "") {
        swal("Error", "Necesita seleccionar el cliente.", "error");
    }
    else {
        enviarServidor("CuentaCobrar/ObtenerDocVenta?ic=" + gbi("txtRazonSocial").dataset.id, asignarListaDocVenta);
    }

    function asignarListaDocVenta(rpta) {
        var listas = rpta.split("↔");
        if (listas[0] == "OK") {
            var listaDocVenta = listas[2].split("▼");
            cbm("docventa", "Tipo de Documento", "btnDocVenta", null, ["idDocumentoVenta", "Fecha", "Cliente", "Serie", "Numero"], listaDocVenta, cargarSinXR);
        } else {
            swal('Error', 'Error al cargar los datos', 'error');
        }
    }
}
//funciones para el documento de venta
var listaVentaDetalle = [];
function cargarDatosDocVenta(rpta) {
    var listas = rpta.split("↔");
    if (listas[0] == "OK") {
        var listaVenta = listas[1].split("▼");
        listaVentaDetalle = listas[3].split("▼");
        var docVenta = listaVenta[0].split("▲");
        console.log(docVenta);
        console.log(listaVentaDetalle);
        gbi("txtSerieNumDocFisico").dataset.id = docVenta[0];
        gbi("txtSerieNumDocFisico").value = docVenta[13] + "-" + docVenta[14];
        gbi("txtRazonSocial").value = docVenta[5];
        gbi("txtRazonSocial").dataset.id = docVenta[4];
        gbi("txtDireccion").value = docVenta[6];
        gbi("txtNroDocumento").value = docVenta[7];
        gbi("txtMonedaFisico").dataset.id = docVenta[9];
        adc(listaMoneda, docVenta[9], "txtMonedaFisico", 1);
        gbi("txtFechaDocFisico").value = docVenta[12];
        var porcentajeDescuento = parseFloat(docVenta[32]);
        gbi("txtOperacion").value = docVenta[77];
        gbi("tb_DetalleF").innerHTML = "";

        //agregarItemNota(datos, index);
        if (listaVentaDetalle[0] != "") {
            if (listaVentaDetalle.length >= 1) {
                for (var i = 0; i < listaVentaDetalle.length; i++) {
                    console.log(listaVentaDetalle[i].split("▲"));
                    addItemDocVenta(1, listaVentaDetalle[i].split("▲"));
                }
            }
        }
        var totalGratuito = parseFloat(docVenta[33]);
        if (!isNaN(totalGratuito) && totalGratuito > 0) {
            gbi("txtGratuita").value = totalGratuito;
            gbi("txtGravada").value = "0.00";
            gbi("txtExportacion").value = "0.00";
            gbi("txtIGV").value = "0.00";
            gbi("txtGratuita").value = "0.00";
            gbi("txtTotalFinal").value = "0.00";
            gbi("txtDescuento").value = "0.00";
        }
        adc(listaComprobantes, docVenta[3], "txtTipoDocumento", 1);
        adc(listaTipoCompra, docVenta[2], "txtTipoCompra", 2);
        if (docVenta[77] == "2") {
            gbi("cboTipoAfectacion").value = "16";
        } else {
            gbi("cboTipoAfectacion").value = "1";
        }
        calcularSumaDetalle();

    } else {
        swal("Error", "Error al cargar los datos.", "error");
    }
}
function actualizarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        var tipo = '';
        var txtCodigo = document.getElementById('txtID');
        var codigo = txtCodigo.value;

        if (data.length == 1 && res.toLowerCase().includes("error")) {
            mostrarRespuesta("ERROR", res, 'error');
            return;
        }
        if (res == 'OK') {
            mensaje = 'Se registró la Nota de Credito';
            tipo = 'success';
            show_hidden_Formulario(true);
        }
        else {
            mensaje = data[1];
            tipo = 'error';
        }

        //mostrarRespuesta(res, mensaje, tipo);

        listaDatos = data[2].split('▼');
        swal({
            title: res,
            text: mensaje,
            type: tipo,
            buttons: false,
            confirmButtonText: 'Ok'
            //cancelButtonText: 'Descargar PDF'
        }//, function (isConfirm) {
            //if (!isConfirm) {
            //    document.location.href = "VEN_NotaCredito/DownloadPDF?idDocumento=" + parseInt(data[3]);
            //}
            //}
        );

        listar();
    }
}
function obtenerNumero() {
    enviarServidor("CuentaCobrar/ObtenerNumeroNotaCredito?serie=" + gbi("txtNroSerie").value, obtenerNumeroNotaCredito);
    function obtenerNumeroNotaCredito(rpta) {
        var listas = rpta.split('↔');
        if (listas[0] == "OK") {
            gbi("txtNumero").value = pad(listas[2], 7);
        }
    }
}
function cambiarMotivo() {
    gbi("tb_DetalleDocVenta").innerHTML = "";
    gbi("tb_DetalleNota").innerHTML = "";
    var listaMostrar = [3, 5, 7];
    if (listaMostrar.includes(parseInt(gbi("txtMotivo").dataset.id))) {
        $("#divDetalleDocVenta").show();
    } else {
        $("#divDetalleDocVenta").hide();
    }
    gbi("divFactura").querySelectorAll("input, button").forEach(item => {
        if (item.tagName == "INPUT") item.value = "";
        item.dataset.id = "";
    });
    calcularSumaDetalle();
    cambiarCabDetDocVenta();
}
function agregarItemDetDocVenta(data, index) {

    var contenido = "";
    contenido += '<div class="row rowDetDocVen" data-id="' + (data[0]) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';


    contenido += '<div class="col-sm-3 p-t-5" data-id="' + (data[2]) + '">' + (data[3]) + '</div>';//producto o servicio
    contenido += '<div class="col-sm-1 p-t-5">' + (data[5]) + '</div>';//unidadmedida
    contenido += '<div class="col-sm-1 p-t-5">' + (data[4]) + '</div>';//cantidad
    contenido += '<div class="col-sm-1 p-t-5">' + (data[8]) + '</div>';//precio
    contenido += '<div class="col-sm-1 p-t-5">' + (parseFloat(data[4]) * parseFloat(data[8])).toFixed(3) + '</div>';//subtotal
    contenido += '<div class="col-sm-1 p-t-5">' + (data[21]) + '</div>';//descuento
    contenido += '<div class="col-sm-1 p-t-5">' + (data[10]) + '</div>';//total

    switch (gbi("cboMotivo").value) {
        case "3"://error en la descripcion
            contenido += '<div class="col-sm-2"><input type="text" class="form-control form-control-sm" /></div>';
            break;
        case "5"://descuento x item
            contenido += '<div class="col-sm-1"><input type="text" class="form-control form-control-sm" /></div>';
            break;
        case "7"://devolucion x item
            contenido += '<div class="col-sm-1"><input type="text" class="form-control form-control-sm" /></div>';
            break;
        default:
    }

    contenido += '<div class="col-sm-1 p-t-5">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='agregarItemNota(null, " + (index) + ");' class='btn btn-sm waves-effect waves-light btn-success pull-right m-l-10' style='padding:2px 10px; margin: 0px;' /> <i class='fa fa-arrow-down'></i> </button>";
    contenido += '          </div>';
    contenido += '  </div>';

    contenido += "</div>";
    gbi("tb_DetalleDocVenta").innerHTML += contenido;

}
function cambiarCabDetDocVenta() {
    var contenido = "";
    $("#hdNuevaDescripcion").remove();
    $("#hdDescuentoAdicional").remove();
    $("#hdDevolucionXItem").remove();

    switch (gbi("cboMotivo").value) {
        case "3"://error en la descripcion
            contenido += '<div class="col-12 col-md-3" id="hdNuevaDescripcion"><label>Nueva Descripcion</label></div>';
            break;
        case "5"://descuento x item
            contenido += '<div class="col-12 col-md-1" id="hdDescuentoAdicional"><label>dto. Adicional</label></div>';
            break;
        case "7"://devolucion x item
            contenido += '<div class="col-12 col-md-1" id="hdDevolucionXItem"><label>Cant. a Devolver</label></div>';
            break;
        default:

    }
    gbi("cabDetalleDocVenta").innerHTML += contenido;
}
function agregarItemDocFisico() {
    let esValido = true;
    if (validarControl("txtArticulo")) { esValido = false; }
    if (validarControl("txtCantidad")) { esValido = false; }
    if (validarControl("txtPrecio")) { esValido = false; }
    if (validarControl("txtTotal")) { esValido = false; }
    if (!esValido) return;

    var contenido = "";
    contenido += '<div class="row rowNota" style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-right: 0px;">';
    contenido += '<div class="col-sm-1 p-t-5">' + (gvt("txtUnidad")) + '</div>';//unidad medida
    contenido += '<div class="col-sm-4 p-t-5" data-id="' + (gbi("txtArticulo").dataset.id) + '">' + (gvt("txtArticulo")) + '</div>';
    contenido += '<div class="col-sm-1 p-t-5">' + (gvt("txtCantidad")) + '</div>';
    contenido += '<div class="col-sm-2 p-t-5">' + (gvt("txtPrecio")) + '</div>';
    contenido += '<div class="col-sm-2 p-t-5">0.00</div>';
    contenido += '<div class="col-sm-2 p-t-5"><div class="col-xs-12"><button type="button" onclick="borrarDetalle(this);" class="btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10" style="padding:2px 10px; margin: 0px;" /><i class="fa fa-trash-o"></i></button></div></div>';

    gbi("tb_DetalleNota").innerHTML += contenido;
    calcularMontos();
    //limpiar campos
    for (let item of gbi("detalle1").querySelectorAll("input")) {
        item.value = "";
        item.dataset.id = "";
    }
    for (let item of gbi("detalle2").querySelectorAll("input")) {
        item.value = "";
        item.dataset.id = "";
    }
}
function crearJsonDocFisico() {
    obj = {
        SerieNumDocRef: gvt("txtSerieNumDocFisico"),
        FechaDocFisico: gvt("txtFechaDocFisico"),
        TipoCompra: gvt("txtTipoCompra"),
        TipoDocumento: gvt("txtTipoDocumento"),
        RazonSocial: gvt("txtRazonSocial"),
        Direccion: gvt("txtDireccion"),
        NroDocumento: gvt("txtNroDocumento"),
        TipoAfectacion: gbi("cboTipoAfectacion").value,
        MonedaFisico: gvt("txtMonedaFisico")
    };
    return JSON.stringify(obj);
}
function cargarDocFisico(obj) {
    //gbi("chbDocFisico").checked = true;
    //gbi("chbDocFisico").onchange();

    //var obj = JSON.parse(jsonDocFisico);
    gbi("txtSerieNumDocFisico").value = obj.SerieNumDocRef;
    gbi("txtFechaDocFisico").value = obj.FechaDocFisico;
    gbi("txtTipoCompra").value = obj.TipoCompra;
    gbi("txtTipoDocumento").value = obj.TipoDocumento;
    gbi("txtRazonSocial").value = obj.RazonSocial;
    gbi("txtDireccion").value = obj.Direccion;
    gbi("txtNroDocumento").value = obj.NroDocumento;
    gbi("cboTipoAfectacion").value = obj.TipoAfectacion;
    gbi("txtMonedaFisico").value = obj.MonedaFisico;
}
//funciones de configuracion
function listar() {
    configurarFiltro();
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
    gbi("chkIGV").onchange = function () {
        calcularSumaDetalle();
    }
}
function crearMatriz(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    var textos = document.getElementById("txtFiltro").value.trim();
    matriz = [];
    var exito;
    if (listaDatos != "") {

        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            nCampos = campos.length;
            exito = true;
            if (textos.trim() != "") {
                for (var l = 1; l < nCampos; l++) {
                    exito = true;
                    exito = exito && campos[l].toLowerCase().indexOf(textos.toLowerCase()) != -1;
                    if (exito) break;
                }
            }
            if (exito) {
                matriz[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matriz[c][j] = campos[j];
                }
                c++;
            }
        }
    } else {
        document.getElementById("contentPrincipal").innerHTML = "";
    }
    return matriz;
}
function mostrarMatriz(matriz, cabeceras, tabId, contentID) {

    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                switch (matriz[i][8]) {
                    //case "E": var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;background-color:#0c88e5;color:white; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >"; break;
                    //case "A": var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;background-color:#01c013;color:white; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >"; break;
                    case "N": var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;background-color:#fc2020;color:white; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >"; break;
                    default: var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >"; break;
                }
                for (var j = 0; j < cabeceras.length; j++) {
                    var enlaceDoc = (matriz[i][7]).toLowerCase();
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0: case 8:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 3:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
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
                contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' title='Ver'; style='3px 5px;margin-right:2px;color:white;'  onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-eye'></i></button>";
                contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:3px 5px;margin-right:2px;color:white;'  target='_blank' href='" + enlaceDoc + "'> <i class='fa fa-print'></i></a>";
                contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' title='CDR'; style='padding:3px 5px;margin-right:2px;color:white;'   onclick='descargarCDR(\"" + matriz[i][2].split('-')[0] + "\",\"" + matriz[i][2].split('-')[1] + "\")'> <i class='fa fa-file-code-o'></i> </button>";
                contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' title='XML'; style='padding:3px 5px;margin-right:2px;color:white;' onclick='descargarXML(\"" + matriz[i][2].split('-')[0] + "\",\"" + matriz[i][2].split('-')[1] + "\")'> <i class='fa fa-file-o'></i></button>";
                contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' title='Anular'; style='3px 5px;margin-right:2px;color:white;'  onclick='anular(\"" + matriz[i][0] + "\")'> <i class='fa fa-ban'></i> </button>";
                //contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:3px 10px;' title='PDF' onclick='mostrarDetalle(3, \"" + matriz[i][0] + "\")'> <i class='fa fa-file-pdf-o'></i></a>";

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
function crearTablNota(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0: case 8:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 3:
                contenido += "              <div class='col-12 col-md-3'>";
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
//Configurar botones
function configBM() {

    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormularioFisico()) {
            if (parseInt($("#txtTotalF").text()) == 0 || parseInt($("#txtTotalF").text()) == "NaN") {
                mostrarRespuesta("Error", "tiene que ingresar un detalle", "error");
            } else {
                if (gbi("txtDescuentoPrincipal").value >= 100) {
                    mostrarRespuesta("Alerta", "El porcentaje no puede ser mayor al 100%", "error");
                } else {
                    var url = "CuentaCobrar/Grabar";
                    var frm = new FormData();
                    frm.append("idDocVenta", gbi("btnDocVenta").dataset.id);
                    frm.append("Serie", gbi("txtNroSerie").value);
                    frm.append("Numero", gvt("txtNumero"));
                    frm.append("idMotivo", gbi("txtMotivo").dataset.id);
                    frm.append("Descripcion", gvt("txtDescripcion"));
                    frm.append("FechaEmision", gvt("txtFecha"));
                    frm.append("FechaRecepcion", gvt("txtFecha"));
                    frm.append("TotalGravada", gvt("txtGravada"));
                    frm.append("IGV", gvt("txtIGVF"));
                    frm.append("Total", gvt("txtTotalF"));
                    frm.append("TotalExportacion", gvt("txtExportacion"));
                    frm.append("TotalGratuito", gvt("txtGratuita"));
                    frm.append("NumDocRef", gvt("txtSerieNumDocFisico"));
                    frm.append("FechaDocRef", gvt("txtFechaDocFisico"));
                    frm.append("idTipoDocumentoRef", gbi("txtTipoDocumento").dataset.id);
                    frm.append("idTipoVenta", gbi("txtTipoCompra").dataset.id);
                    frm.append("idTipoAfectacion", gbi("cboTipoAfectacion").value);
                    frm.append("idMoneda", gbi("txtMonedaFisico").dataset.id);
                    frm.append("idCliente", gbi("txtRazonSocial").dataset.id);
                    frm.append("ClienteDocumento", gvt("txtNroDocumento"));
                    frm.append("ClienteRazon", gvt("txtRazonSocial"));
                    frm.append("ClienteDireccion", gvt("txtDireccion"));
                    frm.append("TotalDescuentoNacional", gvt("txtDescuento"));
                    var subtotal = 0;
                    var porId = document.getElementById("chkIGV").checked;
                    if (gbi("txtTipoCompra").dataset.id == "5") {
                        document.getElementById("chkIGV").checked = false
                        porId = document.getElementById("chkIGV").checked
                        subtotal = gvt("txtInafecto");
                    } else {
                        subtotal = gvt("txtGravada");
                        document.getElementById("chkIGV").checked = true
                        porId = document.getElementById("chkIGV").checked
                    }
                    frm.append("flgIGV", porId);
                    frm.append("SubTotalNacional", subtotal);
                    frm.append("TotalInafecto", gvt("txtInafecto"));
                    frm.append("TotalExonerado", gvt("txtExonerado"));
                    frm.append("IGVNacional", gvt("txtIGVF"));
                    frm.append("TotalGratuito", gvt("txtGratuita"));
                    frm.append("TotalOtrosCargos", gvt("txtOtrosCargosF"));
                    frm.append("Unidad_Medida", gbi("cboUnidadMedida").value.length == 0 ? "  " : gbi("cboUnidadMedida").value);
                    frm.append("cadDetalle", crearCadenaDetalle());
                    frm.append("idOperacion", gbi("txtOperacion").value.length == 0 ? "  " : gbi("txtOperacion").dataset.id);
                    swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
                    enviarServidorPost(url, actualizarListar, frm);
                }
            }
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

    var btnSocio = document.getElementById("btnModalRazonSocial");
    btnSocio.onclick = function () {
        bDM("txtDireccion");
        cbmu("socio", "Clientes", "txtRazonSocial", "txtNroDocumento",
            ["idSocioNegocio", "Documento", "Razón Social", ""], "/SocioNegocio/ObtenerSocioxTipo?tipo=C", cargarListaRazonSocial);
    }
    var btnModalMonedaFisico = document.getElementById("btnModalMonedaFisico");
    btnModalMonedaFisico.onclick = function () {
        cbmu("moneda", "Moneda", "txtMonedaFisico", null,
            ["idMoneda", "Código", "Descripción"], "/Moneda/ObtenerDatos?Activo=A", cargarLista);
    }

    var btnModalArticulo = document.getElementById("btnModalArticulo");
    btnModalArticulo.onclick = function () {
        cbm("material", "Material", "txtArticulo", null,
            ["CodigoGenerado", "Descripción"], listaArticulos, cargarSinXR_G);
    };

    var btnModalNroSerie = document.getElementById("btnModalNroSerie");
    btnModalNroSerie.onclick = function () {
        //let idNotaCredito = "";
        //for (let item of listaComprobantes) {
        //    let obj = item.split("▲");
        //    if (obj[2] === "07") {
        //        idNotaCredito = obj[0];
        //        break;
        //    }
        //}
        //var url = 'Factura/ObtenerSeriexTipoDocumento?id=' + gbi("txtTipoDocumento").dataset.id;
        //enviarServidor(url, CargarSeriesVenta);
        cbmu("Serie", "Serie", "txtNroSerie", null,
            ["idSerie", "Número"], "/CuentaCobrar/ObtenerSeriexTipoDocumento?id=4", cargarListaModificada);
    }

    var btnCancelarArticulo = document.getElementById("btnCancelarArticulo");
    btnCancelarArticulo.onclick = function () { cancel_AddArticulo(); $("#btnAgregarArticulo").show(); }
    for (let item of gbi("rowFrm").querySelectorAll("input")) {
        item.onfocus = function () { item.select(); };
    }

    let btnAgregarArticulo = gbi("btnAgregarArticulo");
    btnAgregarArticulo.onclick = function () {
        if (validarAgregarDetalle()) {
            addItem(0, []);
            limpiarCamposDetalle();
            calcularSumaDetalle();
        }
    }

    var cboTipoAfectacion = document.getElementById("cboTipoAfectacion");
    cboTipoAfectacion.onchange = function () {
        var tipoAfectacion = gbi("cboTipoAfectacion").value;
        var sumExonerado = 0;
        var sumInafecta = 0;
        var sumGravada = 0;
        var SumGratuita = 0;
        var SumOtrosCargos = 0;
        var DescuentoTotal = 0;
        var sum = 0;
        var sumGratuita = 0;
        var sumExportacion = 0;

        switch (tipoAfectacion) {
            case "16":
                $(".rowDet").each(function (obj) {
                    sumExportacion += parseFloat($(".rowDet")[obj].children[7].innerHTML);
                });

                if (sumExportacion > 0) {
                    gbi("chkIGV").checked = false;
                    var sumaTotal = parseFloat(0).toFixed(2);
                    var descuento = parseFloat(0).toFixed(2);
                    gbi("txtOtrosCargosF").value = (0).toFixed(2);
                    var otrosCargos = parseFloat(gbi("txtOtrosCargosF").value).toFixed(2);
                    descuento = parseFloat(sumExportacion * gbi("txtDescuentoPrincipal").value / 100);
                    sumaTotal = sumExportacion + otrosCargos - descuento;

                    gbi("txtDescuento").value = descuento.toFixed(2);
                    gbi("txtGravada").value = (0).toFixed(2);
                    gbi("txtIGVF").value = (0).toFixed(2);
                    gbi("txtExonerado").value = (0).toFixed(2);
                    gbi("txtAnticipo").value = (0).toFixed(2);
                    gbi("txtExportacion").value = parseFloat(sumExportacion - descuento).toFixed(2);
                    gbi("txtInafecto").value = parseFloat(sumExportacion - descuento).toFixed(2);
                    gbi("txtTotalF").value = parseFloat(sumExportacion - descuento).toFixed(2);
                }
                break;
            default:
                $(".rowDet").each(function (obj) {
                    sum += parseFloat($(".rowDet")[obj].children[7].innerHTML);
                });

                gbi("txtInafecto").value = (0).toFixed(2);
                gbi("txtExportacion").value = (0).toFixed(2);
                if (gbi("chkIGV").checked) {
                    gbi("txtGravada").value = (parseFloat((parseFloat(sum * 100 / 118)).toFixed(3))).toFixed(2);
                    gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(4))).toFixed(2);
                    gbi("txtIGVF").value = (parseFloat(((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(4))).toFixed(2);
                    gbi("txtTotalF").value = (parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value)).toFixed(4))).toFixed(2);
                }
                else {
                    gbi("txtGravada").value = (parseFloat((parseFloat(sum)).toFixed(3))).toFixed(2);
                    gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(4))).toFixed(2);
                    gbi("txtIGVF").value = (parseFloat((((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100)).toFixed(4))).toFixed(2);
                    gbi("txtTotalF").value = (parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - parseFloat(gbi("txtDescuento").value))).toFixed(4))).toFixed(2);
                }
                break;
        }
    }

    var btnVistaPrevia = gbi("btnVistaPrevia");
    btnVistaPrevia.onclick = function () {
        VistaPreviaNotaCredito();
    }
}
function BuscarxFecha(f1, f2) {
    var url = 'CuentaCobrar/ObtenerPorFecha?fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
}
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        if (listas[0] == "OK") {
            listaDatos = listas[2].split('▼');

            matriz = crearMatriz(listaDatos);
            configurarFiltro(cabeceras);
            mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
            reziseTabla();
        } else {
            swal('Error', listas[2], 'error');
        }
        //ConsultarRespuestaComprobante();
        //ConsultarEstadoComprobante();
    }
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtFactura")) error = false;
    if (validarControl("txtDescripcion")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (validarControl("txtNroSerie")) error = false;
    if (validarControl("txtNumero")) error = false;


    return error;
}
function validarFormularioFisico() {
    var error = true;
    if (validarControl("txtDescripcion")) error = false;
    if (validarControl("txtFecha")) error = false;

    if (validarControl("txtSerieNumDocFisico")) error = false;
    if (validarControl("txtFechaDocFisico")) error = false;
    if (validarControl("txtTipoCompra")) error = false;

    if (validarControl("txtTipoDocumento")) error = false;
    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtNroDocumento")) error = false;

    if (validarControl("txtNroSerie")) error = false;
    if (validarControl("txtNumero")) error = false;
    if (validarControl("txtMonedaFisico")) error = false;

    if (error && gbi("tb_DetalleF").children.length == 0) {
        error = false;
        swal("Alerta", "Debe ingresar al menos una fila al detalle", "warning");
    }
    return error;
}
//Carga con botones de Modal sin URL (Con datos dat[])
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}
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
        case "btnDocVenta":
            enviarServidor("DocumentoVenta/ObtenerDatosxID?id=" + id, cargarDatosDocVenta);
            break;
        case "txtTipoCompra":
            return gbi("txtRazonSocial");
            break;
        case "txtArticulo":
            var u = "ListaPrecio/ObtenerPrecioArtProv?iA=" + gvc("txtArticulo") + "&iP=" + gvc("txtRazonSocial") + "&iM=" + gvc("txtMonedaFisico");
            enviarServidor(u, cLP);
            return gbi("txtCantidad");
            break;
        case "txtNroSerie":
            //var u = "ListaPrecio/ObtenerPrecioArtProv?iA=" + gvc("txtArticulo") + "&iP=" + gvc("txtRazonSocial") + "&iM=" + gvc("txtMonedaFisico");
            //enviarServidor(u, cLP);
            obtenerNumero();
            return gbi("txtDescripcion");
            break;
        //case "txtCategoria":
        //    //var txtCategoria = document.getElementById("txtCategoria");
        //    //gbi("txtArticulo").value = "";
        //    //gbi("txtArticulo").dataset = "";
        //    //var btnModalArticulo = gbi("btnModalArticulo");
        //    //btnModalArticulo.onclick = function () {
        //    //    cbmu("articulo", "Articulo", "txtArticulo", null,
        //    //        ["idArticulo", "Código", "Descripción"], "/Articulo/ObtenerDatosxCategoria?Cat=" + gbi("txtCategoria").dataset.id + "&Activo=A", cargarLista);
        //    //}
        //    return gbi("txtGravada");
        //    break;
        default:
            break;
    }
}
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
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function cLP(r) {
    if (r.split('↔')[0] == "OK") {
        var listas = r.split('↔');
        var lp = listas[2].split("▲");
        var UnidadMedida = lp[0];
        var Precio = lp[1];
        gbi("txtUnidad").value = UnidadMedida == null ? "" : UnidadMedida;
        gbi("txtPrecio").value = Precio == null ? 0 : Precio;
    }
}
function cfgTKP(evt) {
    //event.target || event.srcElement
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {
            case "txtFecha":
                gbi("txtDescripcion").focus();
                break;
            case "txtDescripcion":
                //if (gbi("chbDocFisico").checked)
                gbi("txtSerieNumDocFisico").focus();
                //else
                //    gbi("btnDocVenta").focus();
                break;
            case "txtSerieNumDocFisico":
                gbi("txtFechaDocFisico").focus();
                break;
            case "txtFechaDocFisico":
                gbi("txtTipoCompra").focus();
                break;
            case "txtTipoCompra":
                gbi("btnModalTipoCompra").onclick();
                gbi("txtRazonSocial").focus();
                break;
            //case "txtRazonSocial":
            //    gbi("txtRazonSocial").focus();
            //    break;
            case "txtCantidad":
                gbi("txtPrecio").focus();
                break;
            case "txtPrecio":
                gbi("txtTotal").focus();
                break;
            case "txtTotal":
                if (validarAgregarDetalle()) {
                    addItem(0, []);
                    limpiarCamposDetalle();
                    calcularSumaDetalle();
                }
                break;
            case "txtFactura":
                editarMontoAnticipo();
                gbi("txtTotalAnticipo").focus();
                break;
            case "txtDescuentoPrincipal":
                gbi("txtCategoria").focus();
                break;

            case "txtNroSerie":
                var url = 'Serie/ObtenerNumeroVenta?idSerie=' + gvt("txtNroSerie");
                enviarServidor(url, ObtenerNumeroVenta);
                gbi("txtFecha").focus();
                break;
            case "txtNroComprobante":
                if (gbi("txtNroComprobante").value.trim().length == 0) {
                    mostrarRespuesta("Alerta", "Llene el Nro de Documento", "error");
                }
                else {
                    gbi("txtNroComprobante").value = pad(gbi("txtNroComprobante").value, 7);
                    gbi("txtFecha").focus();
                }
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

                gbi("txtTotal").value = parseFloat(c * p).toFixed(3);

                break;
            case "txtDescuentoPrincipal":
                if (document.getElementsByClassName("rowDet").length > 0) {
                    calcularSumaDetalle();
                }
                break;
            default:
                break;

        }
    }
}
function cfgTMKP(evt) {
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        var n = o.replace("txt", "");
        gbi("btnModal" + n).click();
    }
}

function cargarListaModificada(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        listaDatosModal = data[1].split("▼");
        mostrarModalModificada(cabecera_Modal, listaDatosModal);
    }
}
function crearTablaModalModificado(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:2px 10px 0px 10px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 3:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 1:
                contenido += //nCampos == 3 ? "              <div class='col-12 col-md-4'>" : "              <div class='col-12 col-md-2'>";
                    "              <div class='col-12 col-md-8'>";
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
function mostrarModalModificada(cabecera_Modal, lista) {
    if (lista.length == 1 && lista[0].trim().length == 0) {
        mostrarRespuesta("Mensaje", "No se encontraron resultados para esta opción", "info")
    }
    else {
        crearTablaModalModificado(cabecera_Modal, "divTablaCabecera_Modal");
        configurarFiltroModalModificada(lista, cabecera_Modal);
        matrizModal = crearMatrizModal(lista);
        $.when(mostrarMatrizModalModificada(matrizModal, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal)).then(
            function () {
                AbrirModal('modal-Modal');
            });
        setTimeout(function () { gbi("txtFiltroMod").focus() }, 500);
    }
}
function configurarFiltroModalModificada(listaDatosModal, cabecera_Modal) {

    var texto = document.getElementById("txtFiltroMod");
    texto.onkeyup = function () {
        matriz = crearMatrizModal(listaDatosModal);
        indiceActualBloquem = 0;
        indiceActualPaginam = 0;
        mostrarMatrizModalModificada(matriz, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal);
    };
}
function mostrarMatrizModalModificada(matriz, cabeceras, tabId, contentID, confdbc) {
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
            switch (cabeceras.length) {
                case 2:
                    tipoColDes = 12;
                    break;
                case 3:
                    tipocol = 4;
                    tipoColDes = 8;
                    break;

                case 4:
                    tipocol = 3
                    tipoColDes = 4
                    break
                case 5:
                    tipocol = 2
                    tipoColDes = 4;
                    break;
                case 6:
                    tipocol = 2
                    tipoColDes = 4;
                    break;
                default:

            }
            ///Si usas clusterize usar dat en lugar de contenido    
            var dat = [];
            //var contenido2 = "";
            for (var i = 0; i < nRegistros; i++) {
                if (i < nRegistros) {

                    var contenido2 = "<div class='row panel salt fs-10' id='numMod" + i + "' tabindex='" + (100 + i) + "' style='padding:0px 10px;cursor:pointer;'>";
                    for (var j = 0; j < cabeceras.length; j++) {
                        switch (j) {
                            case 0:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += ("<div class='col-12 col-md-8'>");
                                break;
                            case 2:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                            case 3:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-1" + tipocol + "'>");
                                break;
                        }
                        contenido2 += "<span class='hidden-sm-up'>" + cabeceras[j] + " : </span><span id='md" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                        contenido2 += "</div>";
                    }
                    contenido2 += "</div>";
                    dat.push(contenido2);
                }
                else break;
            }
            //$("#" + contentID).html(contenido2);
            var clusterize = new Clusterize({
                rows: dat,
                scrollId: tabId,
                contentId: contentID
            });
        }
        confdbc();
    }
}

function ConsultarRespuestaComprobante() {
    var url = "Factura/ConsultarRespuestaComprobante";
    enviarServidor(url, ConsultarRptaComprobante);

    function ConsultarRptaComprobante(rpta) {

        if (rpta != "") {
            var listas = rpta.split('↔');
            var res = listas[0];
            var mensaje = listas[1];
            if (res != "OK") {
                //swal(res, mensaje, res == "Error" ? 'error' : 'warning');
                return;
            }

            var litaDatos = listas[2].split("▼");
            var listaObj = [];

            litaDatos.forEach(item => {
                var objStr = item.split("▲");
                var obj = {
                    TipoComprobante: objStr[0],
                    Serie: objStr[1],
                    Numero: objStr[2],
                    CodigoRespuesta: objStr[4],
                    DescripcionRespuesta: objStr[5]
                };
                listaObj.push(obj);
            });

            gbi("contentPrincipal").querySelectorAll(".row.panel.salt").forEach((item, index) => {

                for (var i = 0; i < listaObj.length; i++) {
                    var obj = listaObj[0];
                    if (item.innerText.includes(obj.Serie + "\n" + pad(obj.Numero, 7, "0"))) {

                        var cad = '<div class="text-center" title="' + obj.DescripcionRespuesta + '"><a class="fs-12">';
                        var className = "";
                        switch (obj.CodigoRespuesta) {
                            case "0"://CDR-Aceptado
                                //cad += '<i class="fa fa-check"></i>';
                                className = "fa fa-check";
                                break;
                            case "1"://CDR-Rechazado 
                                //cad += '<i class="fa fa-close"></i>';
                                className = "fa fa-close";
                                break;
                            case "2"://Excepción
                                //cad += '<i class="fa fa-ban"></i>';
                                className = "fa fa-ban";
                                break;
                            case "3"://CDR-Aceptado con observaciones
                                //cad += '<i class="fa fa-warning"></i>';
                                className = "fa fa-warning";
                                break;
                        }
                        //cad += "</a></div>";
                        item.children[7].querySelector("i").className = className;
                        item.children[7].querySelector("i").parentElement.title = obj.DescripcionRespuesta;
                        break;
                    }
                }
            });
            //ConfirmarRespuestaComprobante(listaObj);
        } else {
            swal('Error', 'Sin respuesta del servidor.', 'error');
        }
    }
}
function ConsultarEstadoComprobante() {
    var url = "DocumentoVenta/ConsultarEstadoComprobante";
    enviarServidor(url, ConsultarRptaEstadoComprobante);

    function ConsultarRptaEstadoComprobante(rpta) {

        if (rpta != "") {
            var listas = rpta.split('↔');
            var res = listas[0];
            var mensaje = listas[1];
            if (res != "OK") {
                //swal(res, mensaje, res == "Error" ? 'error' : 'warning');
                return;
            }

            var litaDatos = listas[2].split("▼");
            var listaObj = [];

            litaDatos.forEach(item => {
                var objStr = item.split("▲");
                var obj = {
                    TipoComprobante: objStr[0],
                    Serie: objStr[1],
                    Numero: objStr[2],
                    Otorgado: objStr[3],
                    Leido: objStr[4],
                    Rechazado: objStr[5]
                };
                listaObj.push(obj);
            });

            gbi("contentPrincipal").querySelectorAll(".row.panel.salt").forEach((item, index) => {

                for (var i = 0; i < listaObj.length; i++) {
                    var obj = listaObj[0];
                    if (item.innerText.includes(obj.Serie + "\n" + pad(obj.Numero, 7, "0"))) {

                        var cad = '<div class="text-center" title="' + obj.DescripcionRespuesta + '"><a class="fs-12">';
                        var className = "";
                        var title = "";
                        if (obj.Otorgado.toLowerCase() == "true") {
                            className = "fa fa-envelope";
                            title = "OTORGADO";
                        }
                        if (obj.Leido.toLowerCase() == "true") {
                            className = "fa fa-envelope-open";
                            title = "LEÍDO";
                        }
                        if (obj.Rechazado.toLowerCase() == "true") {
                            className = "fa fa-window-close";
                            title = "RECHAZADO";
                        }

                        item.children[8].querySelector("i").className = className;
                        item.children[8].querySelector("i").parentElement.title = title;
                        break;
                    }
                }
            });
        } else {
            swal('Error', 'Sin respuesta del servidor.', 'error');
        }
    }
}
function enviarServidorPostWS(url, metodo, frm) {
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
//Modal General
function cargarSinXR_G(dat) {
    if (dat != "") {
        mostrarModal_G(cabecera_Modal, dat);
    }
}
function mostrarModal_G(cabecera_Modal, lista) {
    if (lista.length == 1 && lista[0].trim().length == 0) {
        mostrarRespuesta("Mensaje", "No se encontraron resultados para esta opción", "info")
    }
    else {
        crearTablaModal(cabecera_Modal, "divTablaCabecera_Modal");
        configurarFiltroModal(lista, cabecera_Modal);
        matrizModal = crearMatrizModal(lista);
        $.when(mostrarMatrizModal_G(matrizModal, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal)).then(
            function () {
                AbrirModal('modal-Modal');
            });
        setTimeout(function () { gbi("txtFiltroMod").focus() }, 200);
    }
}
function mostrarMatrizModal_G(matriz, cabeceras, tabId, contentID, confdbc) {
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
            switch (cabeceras.length) {
                case 2:
                    tipoColDes = 12;
                    tipoColum = 12;
                    break;
                case 3:
                    tipocol = 4;
                    tipoColDes = 8;
                    tipoColum = 8
                    break;

                case 4:
                    tipocol = 3
                    tipoColDes = 3
                    tipoColum = 6
                    break
                case 5:
                    tipocol = 2
                    tipoColDes = 4;
                    tipoColum = 4;
                    break;
                case 6:
                    tipocol = 2
                    tipoColDes = 4;
                    tipoColum = 4;
                    break;
                default:

            }
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
                                contenido2 += ("<div class='col-12 col-md-6'>");
                                break;
                            case 3:
                                contenido2 += ("<div class='col-12 col-md-2'>");
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
//ModalDocVenta
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
                contenido += "              <div class='col-12 col-md-6'>";
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
            switch (cabeceras.length) {
                case 2:
                    tipoColDes = 12;
                    tipoColum = 12;
                    break;
                case 3:
                    tipocol = 4;
                    tipoColDes = 8;
                    tipoColum = 8
                    break;

                case 4:
                    tipocol = 3
                    tipoColDes = 3
                    tipoColum = 6
                    break
                case 5:
                    tipocol = 2
                    tipoColDes = 4;
                    tipoColum = 4;
                    break;
                case 6:
                    tipocol = 2
                    tipoColDes = 4;
                    tipoColum = 4;
                    break;
                default:

            }
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
                                contenido2 += ("<div class='col-12 col-md-6'>");
                                break;
                            case 3:
                                contenido2 += ("<div class='col-12 col-md-2'>");
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
function configurarFiltroModal(listaDatosModal, cabecera_Modal) {
    var texto = document.getElementById("txtFiltroMod");
    texto.onkeyup = function () {
        matriz = crearMatrizModal(listaDatosModal);
        indiceActualBloquem = 0;
        indiceActualPaginam = 0;
        mostrarMatrizModal(matriz, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal);
    };
}
//Para Razon Social
function cargarListaRazonSocial(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        mostrarModalRazonSocial(cabecera_Modal, listaDatosModal);
    }
}
function mostrarModalRazonSocial(cabecera_Modal, lista) {
    if (lista.length == 1 && lista[0].trim().length == 0) {
        mostrarRespuesta("Mensaje", "No se encontraron resultados para esta opción", "info")
    }
    else {
        crearTablaModal(cabecera_Modal, "divTablaCabecera_Modal");
        configurarFiltroModal(lista, cabecera_Modal);
        matrizModal = crearMatrizModalDoc(lista);
        $.when(mostrarMatrizModalRazonSocial(matrizModal, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal)).then(
            function () {
                AbrirModal('modal-Modal');
            });
        setTimeout(function () { gbi("txtFiltroMod").focus() }, 200);
    }
}
function mostrarMatrizModalRazonSocial(matriz, cabeceras, tabId, contentID, confdbc) {
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
                            case 0: case 6: case 3:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += "<div class='col-12 col-md-2'>";
                                break;
                            case 2:
                                contenido2 += "<div class='col-12 col-md-7'>";
                                break;
                            default:
                                contenido2 += "<div class='col-12 col-md-3'>";
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
function configurarFiltroModal(listaDatosModal, cabecera_Modal) {

    var texto = document.getElementById("txtFiltroMod");
    texto.onkeyup = function () {
        matriz = crearMatrizModal(listaDatosModal);
        indiceActualBloquem = 0;
        indiceActualPaginam = 0;
        mostrarMatrizModalRazonSocial(matriz, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal);
    };
}
//Agregar detalle de doc parseFloat(data[10]).toFixed(3
function addItemDocVenta(tipo, data,val) {
    var contenido = "";
    var precio = val ? parseFloat(data[8]) / 1.18 : parseFloat(data[8]);
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" style="display:none;" data-id="' + (tipo == 1 ? data[0] : "0") + '">' + (tipo == 1 ? data[0] : "0") + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5" style="display:none;">' + (tipo == 1 ? data[5] : '-') + '</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? precio : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[4]) * precio : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2">';
    contenido += '      <div class="row rowDetbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalle(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItem(\"gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1 p-t-5" style="display:none;">' + (tipo == 1 ? data[23] : gvt("cboTipoAfectacion")) + '</div>';//T-D
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleF").innerHTML += contenido;
}
function addItem(tipo, data) {

    var idArt = (gvc("txtArticulo").length == 0 ? 1 : (tipo == 1 ? data[2] : parseInt(gvc("txtArticulo"))));
    var totdetallef = parseFloat(data[3]).toFixed(2) * parseFloat(data[6]).toFixed(2)
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" style="display:none;" data-id="' + (tipo == 1 ? data[0] : "0") + '">' + (tipo == 1 ? data[0] : "0") + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + idArt + '">' + (tipo == 1 ? data[5] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[3]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5" style="display:none;">-</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? totdetallef : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2">';
    contenido += '      <div class="row rowDetbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalle(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItem(\"gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1 p-t-5" style="display:none;">' + (tipo == 1 ? data[23] : gvt("cboTipoAfectacion")) + '</div>';//T-D
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleF").innerHTML += contenido;
}
function calcularSumaDetalle() {
    var sumExonerado = 0;
    var sumInafecta = 0;
    var sumGravada = 0;
    var SumGratuita = 0;
    var SumOtrosCargos = 0;
    var DescuentoTotal = 0;

    var sum = 0;
    var sumGratuita = 0;
    var sumExportacion = 0;

    $(".rowDet").each(function (obj) {
        var idTipoAfect = gbi("txtTipoCompra").dataset.id;
        var Tipo = gbi("txtTipoCompra").value;
        //var idTipoAfect = $(".rowDet")[obj].children[9].innerHTML;
        //var Tipo = "";
        //for (var i = 0; i < listaTipoAfectacion.length; i++) {
        //    if (listaTipoAfectacion[i].split("▲")[0] == idTipoAfect) {
        //        Tipo = (listaTipoAfectacion[i].split("▲")[1]).split("-")[0];
        //        break;
        //    }
        //}
        if (Tipo.toUpperCase().indexOf("GRATUITA") != -1) {
            sumGratuita += parseFloat($(".rowDet")[obj].children[7].innerHTML);
        }
        else if (Tipo.toUpperCase() == "TIPO EXPORTACION") {
            sumExportacion += parseFloat($(".rowDet")[obj].children[7].innerHTML);
        } else {
            sum += parseFloat($(".rowDet")[obj].children[7].innerHTML);
        }
    });


    //gratuita
    gbi("txtGratuita").value = (parseFloat(parseFloat(sumGratuita).toFixed(3))).toFixed(2);
    if (sumExportacion > 0) {
        gbi("chkIGV").checked = false;
        var sumaTotal = parseFloat(0).toFixed(2);
        var descuento = parseFloat(0).toFixed(2);
        gbi("txtOtrosCargosF").value = (0).toFixed(2);
        var otrosCargos = parseFloat(gbi("txtOtrosCargosF").value).toFixed(2);
        descuento = parseFloat(sumExportacion * gbi("txtDescuentoPrincipal").value / 100);
        sumaTotal = sumExportacion + otrosCargos - descuento;

        gbi("txtDescuento").value = descuento.toFixed(2);
        gbi("txtGravada").value = (0).toFixed(2);
        gbi("txtIGVF").value = (0).toFixed(2);
        gbi("txtExonerado").value = (0).toFixed(2);
        gbi("txtAnticipo").value = (0).toFixed(2);
        gbi("txtExportacion").value = parseFloat(sumExportacion - descuento).toFixed(2);
        gbi("txtInafecto").value = parseFloat(sumExportacion - descuento).toFixed(2);
        gbi("txtTotalF").value = parseFloat(sumExportacion - descuento).toFixed(2);
    } else {
        gbi("txtInafecto").value = (0).toFixed(2);
        gbi("txtExportacion").value = (0).toFixed(2);
        if (gbi("chkIGV").checked) {
            gbi("txtGravada").value = (parseFloat((parseFloat(sum * 100 / 118)).toFixed(3))).toFixed(2);
            gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(4))).toFixed(2);
            gbi("txtIGVF").value = (parseFloat(((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(4))).toFixed(2);
            gbi("txtTotalF").value = (parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value)).toFixed(4))).toFixed(2);
        }
        else {
            gbi("txtGravada").value = (parseFloat((parseFloat(sum)).toFixed(3))).toFixed(2);
            gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(4))).toFixed(2);
            gbi("txtIGVF").value = (parseFloat((((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100)).toFixed(4))).toFixed(2);
            gbi("txtTotalF").value = (parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - parseFloat(gbi("txtDescuento").value))).toFixed(4))).toFixed(2);
        }
    }
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
    calcularSumaDetalle();
}
function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[3].dataset.id;
    gbi("txtArticulo").value = row.children[3].innerHTML;
    gbi("txtCantidad").value = row.children[4].innerHTML;
    gbi("txtPrecio").value = row.children[6].innerHTML;
    gbi("txtTotal").value = row.children[7].innerHTML;
    idTablaDetalle = id;
    //para que aparescan , ya que en el html  esta en style="display:none;"
    gbi("btnActualizarArticulo").style.display = "";
    gbi("btnCancelarArticulo").style.display = "";
    $("#btnAgregarArticulo").hide();
}
function guardarItemDetalle() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[3].dataset.id = gbi("txtArticulo").dataset.id;
    row.children[3].innerHTML = gbi("txtArticulo").value;
    row.children[4].innerHTML = gbi("txtCantidad").value;
    row.children[6].innerHTML = gbi("txtPrecio").value;
    row.children[7].innerHTML = gbi("txtTotal").value;
    gbi("btnActualizarArticulo").style.display = "none";
    gbi("btnCancelarArticulo").style.display = "none";
    gbi("btnAgregarArticulo").style.display = "";
    calcularSumaDetalle();
    cancel_AddArticulo();

}
function cancel_AddArticulo() {
    limpiarControl("txtArticulo");
    gbi("txtTotal").value = "0.00";
    txtPrecio.value = "0.00";
    gbi("txtCantidad").value = "0.00";
    gbi("btnActualizarArticulo").style.display = "none";
    gbi("btnCancelarArticulo").style.display = "none";
}
function crearCadenaDetalle() {
    var cdet = "";
    $(".rowDet").each(function (obj) {
        //cdet += $(".rowDet")[obj].children[0].innerHTML;//idDetalle
        cdet += "0|0"; //idNota
        cdet += "|" + $(".rowDet")[obj].children[3].dataset.id;//idArticulo
        cdet += "|" + $(".rowDet")[obj].children[4].innerHTML;//Cantidad
        cdet += "|" + (gbi("cboUnidadMedida").value.length == 0 ? "  " : gbi("cboUnidadMedida").value);//$(".rowDet")[obj].children[5].innerHTML;; //unidad medida
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;//descripcion articulo
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//PrecioNacional        
        cdet += "|0";//Descuento
        cdet += "|01-01-2000|01-01-2000|1|1";
        cdet += "¯";
    });
    return cdet;
}
function crearDetalleNota(lista) {
    var contenido = "";
    lista.forEach(item => {
        var data = item.split("▲");
        contenido += '<div class="row rowNota" data-id-det-ven="' + data[0] + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
        contenido += '<div class="col-sm-1 p-t-5">' + (data[4]) + '</div>';//unidad medida
        contenido += '<div class="col-sm-4 p-t-5">' + (data[5]) + '</div>';
        contenido += '<div class="col-sm-1 p-t-5">' + (data[3]) + '</div>';
        contenido += '<div class="col-sm-2 p-t-5">' + (data[6]) + '</div>';
        contenido += '<div class="col-sm-2 p-t-5">' + (data[7]) + '</div>';
        contenido += "</div>";
    });
    gbi("tb_DetalleNota").innerHTML += contenido;
    calcularMontos();
}
//Anular
function anular(id) {
    swal({
        title: 'Desea Anular la Nota de Credito? ',
        text: 'No podrá recuperar los datos anulados.',
        icon: 'warning',
        buttons: true,
        confirmButtonText: 'Si, Anular!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var u = "CuentaCobrar/Anular?idNotaCredito=" + id;
                enviarServidor(u, anularListar);
            } else {
                swal('Cancelado', 'No se Anuló la Nota de Credito', 'error');
            }
        });
}
function anularListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se Anuló la Nota de Credito';
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
//limpiar
function limpiarTodo() {
    bDM("txtID");
    bDM("txtNroSerie");
    bDM("txtNumero");
    bDM("txtDescripcion");
    bDM("txtRazonSocial");
    limpiarControl("txtSerieNumDocFisico");
    limpiarControl("txtFechaDocFisico");
    bDM("txtDireccion");
    bDM("txtMonedaFisico");
    bDM("txtTipoCompra");
    bDM("txtTipoDocumento");
    bDM("txtTipoCompra");
    bDM("txtNroDocumento");
    gbi("tb_DetalleServicio").innerHTML = "";
    gbi("tb_DetalleF").innerHTML = "";
    //gbi("txtSubTotalF").value = parseFloat(0).toFixed(2);
    gbi("txtTotalF").value = parseFloat(0).toFixed(2);
    gbi("txtIGVF").value = parseFloat(0).toFixed(2);
    gbi("txtGravada").value = parseFloat(0).toFixed(2);
    gbi("txtDescuento").value = parseFloat(0).toFixed(2);
    gbi("txtIGVF").value = parseFloat(0).toFixed(2);
    gbi("txtTotalF").value = parseFloat(0).toFixed(2);
    limpiarCamposDetalle();
}
function limpiarCamposDetalle() {
    //bDM("txtCategoria");
    bDM("txtArticulo");
    gbi("txtCantidad").value = "";
    gbi("txtPrecio").value = "";
    gbi("txtTotal").value = "";
}
//impresion
function TraerDetalleImpresion(id) {
    var url = 'CuentaCobrar/ObtenerDatosImpresion?id=' + id;
    enviarServidor(url, ImprimirNC);
}
function ImprimirNC(rpta) { //fc

    var listas = rpta.split('↔');
    var Resultado = listas[0];
    var datos = listas[1].split('▲');
    console.log(datos);
    var det = listas[2].split("▼");
    var texto = "";
    var doc = new jsPDF('p', 'mm', 'A4');
    var imggg = logoProvesur;
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    doc.setFont('arial')  //'helvetica'
    var xic = 70;  //59
    var altc = 4;
    doc.addImage(imggg, "JPEG", 15, 15, 50, 10);
    //doc.text("Experiencia en Reciclaje",30,15)
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Planta Centro", 15, 30);
    doc.text(":   Av. Nicolas Ayllón N° 2433 - Ate Lima", 45, 30);
    doc.text("    Telf: 711-0631", 45, 33);

    doc.text("Planta Sur", 15, 38);
    doc.text(":   Panamericana Sur Km. 18, Int. B-14 Mutual Ayacucho", 45, 38);
    doc.text("    San Juan de Miraflores - Lima - Lima", 45, 41);
    doc.text("    Telf.. 258-4673 Fax: 258-4289", 45, 44);

    doc.text("Planta Norte", 15, 49);
    doc.text(":   Av. Los Platinos Mz. A Lte. 24 Zona Industrial Infantas - ", 45, 49);
    doc.text("    Los Olivos - Lima - Lima ", 45, 52);
    doc.text("    Telf.. 528-3019 Telefax: 528-5877", 45, 55);
    doc.text("Web : www.provesur.com.pe / E-mail: gerencia@provesur.com.pe", 15, 60);

    doc.setFontSize(12);
    doc.rect((width / 2) + 22, 15, (width / 2) - 32, 65 - 35);
    doc.setFontType("bold");
    doc.text("RUC. 20503067391", width * 0.75 + 6, 25, "center");
    doc.text("NOTA DE CRÉDITO ELECTRÓNICA", width * 0.75 + 6, 32, "center");
    doc.text(datos[0] + "-" + datos[1], width * 0.75 + 6, 39, "center");

    //doc.addImage(imggg, "JPEG", 15, 15, 70, 30);
    //doc.setFontSize(10);
    //doc.setFontType("bold");
    //doc.text("PROVESUR SAC", 50, 55, "center");
    //doc.setFontSize(9);
    //doc.setFontType("normal");
    ////doc.text("AV. SAN JUAN NRO. 1209 (FRENTE AL CASINO TROPICANA)", 50, 56, "center");

    //doc.setFontSize(8);
    //doc.text(DireccionReciclemos, 15, 61);

    //Cuadro de NOTA CREDITO
    //doc.rect((width / 2) + 10, 15, (width / 2) - 20, 65 - 25);
    //doc.setFontSize(14);
    //doc.setFontType("bold");
    //doc.text("RUC. 20503067391", width * 0.75 + 2.5, 25, "center");
    //doc.text("NOTA DE CRÉDITO ", width * 0.75 + 2.5, 35, "center");
    //doc.text("ELECTRÓNICA", width * 0.75 + 2.5, 42, "center");
    //doc.text(datos[0] + "-" + datos[1], width * 0.75 + 5, 50, "center");


    //Cuadro de Cliente
    //doc.rect(15, xic - 5, width - 25, altc * 2 + 13);
    //doc.setFontSize(8);
    //doc.setFontType("bold");
    //doc.text("CLIENTE", 18, xic);
    //doc.text("RUC", 18, xic + altc * 1);
    //doc.text("DIRECCIÓN", 18, xic + altc * 2);
    //doc.text("FECHA EMISIÓN", 18, xic + altc * 3);
    //doc.text(":", 42, xic);
    //doc.text(":", 42, xic + altc * 1);
    //doc.text(":", 42, xic + altc * 2);
    //doc.text(":", 42, xic + altc * 3);
    //doc.setFontSize(8);
    //doc.setFontType("normal");
    //doc.text(datos[2], 44, xic); //CLIENTE NOMBRE
    //doc.text(datos[3], 44, xic + altc * 1); // cab ruc
    //doc.text(datos[4], 44, xic + altc * 2);
    //doc.text(datos[5], 44, xic + altc * 3);
    //Cuadro de Cliente
    var xic = 70;
    var altc = 4;
    doc.rect(15, xic - 5, width - 25, altc * 3 + 13);
    doc.setFontSize(8);
    doc.setFontType("bold");
    doc.text("CLIENTE", 18, xic);
    doc.text("RUC", 18, xic + altc * 1);
    doc.text("DIRECCIÓN", 18, xic + altc * 2);
    //doc.text("FECHA EMISIÓN", 18, xic + altc * 3);
    doc.text(":", 42, xic);
    doc.text(":", 42, xic + altc * 1);
    doc.text(":", 42, xic + altc * 2);
    //doc.text(":", 42, xic + altc * 3);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text(datos[2], 44, xic); //CLIENTE NOMBRE
    doc.text(datos[3], 44, xic + altc * 1); // cab ruc
    //doc.text(datos[4], 44, xic + altc * 2);
    //doc.text(datos[5], 44, xic + altc * 2);


    //SALTO DE LINEA DE DESCRIPCION
    let lineaAdicionalDir = 0;
    var y = 152;
    var yAlto = 7;
    var altoTemp = y;
    let lineaAdicional = 0;
    var xadCab = 3;
    if (datos[4].length > 45) {
        let desc = datos[4];
        let textoSplit = desc.split(" ");
        let cadena = "";
        let lastTxt = "";
        let prevTxt = "";
        for (let txt of textoSplit) {
            let cadTmp = (cadena + `${txt} `);
            if (cadTmp.trim().length <= 45)
                cadena += `${txt} `;
            else {
                prevTxt = cadena;
                doc.text(cadena.trim().toUpperCase(), 44, xic + (altc + lineaAdicional) * 2);
                lineaAdicional++;
                cadena = `${txt} `;
                lastTxt = `${txt} `;
            }
        }
        if (cadena !== prevTxt) {
            doc.text(cadena.trim().toUpperCase(), 44, xic + (altc + lineaAdicional + 1) * 2);
        }
        doc.setFontType("bold");
        doc.text("FECHA EMISIÓN", 18, xic + (altc + lineaAdicional) * 3);
        doc.setFontType("normal");
        doc.text(":", 42, xic + (altc + lineaAdicional) * 3);
        doc.text(datos[5], 44, xic + (altc + lineaAdicional) * 3);
    } else {

        doc.text(datos[4], 44, xic + altc * 2);
        doc.setFontType("bold");
        doc.text("FECHA EMISIÓN", 18, xic + altc * 3);
        doc.setFontType("normal");
        doc.text(":", 42, xic + altc * 3);
        doc.text(datos[5], 44, xic + altc * 3);
    }
    // FIN SALTO LINEA

    doc.setFontSize(8);
    doc.setFontType("bold");
    doc.text("REFERENCIA", (width / 2) + 20, xic);
    doc.text("FACTURA/BOLETA NRO", (width / 2) + 20, xic + altc * 1);
    doc.text("IMPORTE", (width / 2) + 20, xic + altc * 2);
    doc.text("FECHA", (width / 2) + 20, xic + altc * 3);
    doc.text(":", (width / 2) + 55, xic);
    doc.text(":", (width / 2) + 55, xic + altc * 1);
    doc.text(":", (width / 2) + 55, xic + altc * 2);
    doc.text(":", (width / 2) + 55, xic + altc * 3);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text(datos[6], (width / 2) + 58, xic);
    doc.text(datos[7], (width / 2) + 58, xic + altc * 1);
    doc.text(datos[8], (width / 2) + 58, xic + altc * 2);
    doc.text(datos[9], (width / 2) + 58, xic + altc * 3);


    //Crear Cabecera
    var xid = 95;
    var xad = 3;
    doc.setFontSize(7);
    //crea Detalle
    var x = det;
    xid = 102;
    for (var i = 0; i < x.length; i++) {
        var lenDesc = x[i].split("▲")[2].length;
        var UnidadMedida;
        switch (x[i].split("▲")[1]) {
            case "NIU": UnidadMedida = "U"; break;
            case "TNE": UnidadMedida = "TM"; break;
            case "TN": UnidadMedida = "TM"; break;
            case "KGM": UnidadMedida = "KG"; break;
            case "KG": UnidadMedida = "KG"; break;
        }
        doc.text(x[i].split("▲")[0], 17, xid + (i * xad)); // cantidad
        doc.text(UnidadMedida, 43, xid + (i * xad)); // UnidadMedida

        if (lenDesc > 59) {
            doc.text(x[i].split("▲")[2].substring(0, 59), 53, xid + (i * xad)); // DESCRIPCION
            doc.text(x[i].split("▲")[2].substring(59, lenDesc - 1), 53, xid + ((i + 1) * xad)); // DESCRIPCION
        }
        else {
            doc.text(x[i].children[3].innerHTML, 53, xid + (i * xad)); // DESCRIPCION
        }
        doc.text(x[i].split("▲")[3], 170, xid + (i * xad), "right"); //pUNIT
        doc.text(parseFloat(x[i].split("▲")[4]).toFixed(2), 190, xid + (i * xad), "right");   // MONTO TOTAL

    }  // VENDEDOR


    doc.setFontSize(8);

    doc.setFontType("bold");
    doc.text("SUBTOTAL", 155, height - 45);  // MONTO AFECTO
    doc.text("IGV     ", 155, height - 40);  // MONTO IGV
    doc.text("TOTAL   ", 155, height - 35);  // MONTO TOTAL
    doc.text(":", 173, height - 45);
    doc.text(":", 173, height - 40);
    doc.text(":", 173, height - 35);
    doc.setFontType("normal");
    doc.text(datos[14], width - 15, height - 45, "right");  // MONTO AFECTO
    doc.text(datos[13], width - 15, height - 40, "right");  // MONTO IGV
    doc.text(parseFloat(datos[12]).toFixed(2), width - 15, height - 35, "right");  // MONTO TOTAL

    doc.rect(15, height - 60, width - 25, 7);
    doc.rect(15, height - 50, width - 73, 18);


    doc.text("SON : " + numeroALetras(parseFloat(datos[12]).toFixed(2), {
        plural: datos[16] == "1" ? " SOLES" : " DOLARES AMERICANOS",
        singular: datos[16] == "1" ? " SOL" : " DOLAR AMERICANO",
        centPlural: datos[12].split('.')[1] + '/100',
        centSingular: datos[12].split('.')[1] + '/100'
    }), 17, height - 55); // MONTO EN LETRAS

    doc.setFontType("bold");
    doc.text("MOTIVO DE EMISION", 48, height - 45, "right");
    doc.text("OBSERVACION", 48, height - 41, "right");
    doc.text(":", 53, height - 45);
    doc.text(":", 53, height - 41);
    doc.setFontType("normal");
    doc.text(datos[11], 55, height - 45);
    doc.text(datos[10], 55, height - 41);


    doc.setFontType("normal");
    doc.setDrawColor(0);
    doc.setFillColor(53, 166, 236);
    doc.rect(15, 90, width - 25, 7, "FD");
    doc.setFontSize(8);
    xid = 95;
    doc.text("Cant.", 17, xid); //COD ALTERNO
    doc.text("U/M", 43, xid); // CANTIDAD
    doc.text("Descripción", 53, xid); // DESCRIPCION
    doc.text("Precio", 165, xid); //pUNIT
    doc.text("Total", 185, xid);   // MONTO TOTAL
    doc.line(15, 90, 15, height - 65);
    doc.line(width - 10, 90, width - 10, height - 65);
    doc.line(15, height - 65, width - 10, height - 65);
    doc.setFontSize(7);
    var qr = new QRious({
        value: datos[15]
    });

    doc.addImage(qr.toDataURL('image/jpeg'), "JPEG", 15, height - 30, 25, 25);

    doc.setFontType("normal");
    doc.text("Representación impresa de la  NOTA DE CRÉDITO ELECTRÓNICA Podrá ser consultada en: ", (width / 2) - 60, height - 17);
    doc.text(datos[15], (width / 2) - 60, height - 14);
    doc.text("Autorizado mediante resolución: N° 034-005-0005315", (width / 2) - 60, height - 10);

    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');

}
var numeroALetras = (function () {

    function Unidades(num) {

        switch (num) {
            case 1: return 'UN';
            case 2: return 'DOS';
            case 3: return 'TRES';
            case 4: return 'CUATRO';
            case 5: return 'CINCO';
            case 6: return 'SEIS';
            case 7: return 'SIETE';
            case 8: return 'OCHO';
            case 9: return 'NUEVE';
        }

        return '';
    }//Unidades()

    function Decenas(num) {

        let decena = Math.floor(num / 10);
        let unidad = num - (decena * 10);

        switch (decena) {
            case 1:
                switch (unidad) {
                    case 0: return 'DIEZ';
                    case 1: return 'ONCE';
                    case 2: return 'DOCE';
                    case 3: return 'TRECE';
                    case 4: return 'CATORCE';
                    case 5: return 'QUINCE';
                    default: return 'DIECI' + Unidades(unidad);
                }
            case 2:
                switch (unidad) {
                    case 0: return 'VEINTE';
                    default: return 'VEINTI' + Unidades(unidad);
                }
            case 3: return DecenasY('TREINTA', unidad);
            case 4: return DecenasY('CUARENTA', unidad);
            case 5: return DecenasY('CINCUENTA', unidad);
            case 6: return DecenasY('SESENTA', unidad);
            case 7: return DecenasY('SETENTA', unidad);
            case 8: return DecenasY('OCHENTA', unidad);
            case 9: return DecenasY('NOVENTA', unidad);
            case 0: return Unidades(unidad);
        }
    }//Unidades()

    function DecenasY(strSin, numUnidades) {
        if (numUnidades > 0)
            return strSin + ' Y ' + Unidades(numUnidades)

        return strSin;
    }//DecenasY()

    function Centenas(num) {
        let centenas = Math.floor(num / 100);
        let decenas = num - (centenas * 100);

        switch (centenas) {
            case 1:
                if (decenas > 0)
                    return 'CIENTO ' + Decenas(decenas);
                return 'CIEN';
            case 2: return 'DOSCIENTOS ' + Decenas(decenas);
            case 3: return 'TRESCIENTOS ' + Decenas(decenas);
            case 4: return 'CUATROCIENTOS ' + Decenas(decenas);
            case 5: return 'QUINIENTOS ' + Decenas(decenas);
            case 6: return 'SEISCIENTOS ' + Decenas(decenas);
            case 7: return 'SETECIENTOS ' + Decenas(decenas);
            case 8: return 'OCHOCIENTOS ' + Decenas(decenas);
            case 9: return 'NOVECIENTOS ' + Decenas(decenas);
        }

        return Decenas(decenas);
    }//Centenas()

    function Seccion(num, divisor, strSingular, strPlural) {
        let cientos = Math.floor(num / divisor)
        let resto = num - (cientos * divisor)

        let letras = '';

        if (cientos > 0)
            if (cientos > 1)
                letras = Centenas(cientos) + ' ' + strPlural;
            else
                letras = strSingular;

        if (resto > 0)
            letras += '';

        return letras;
    }//Seccion()

    function Miles(num) {
        let divisor = 1000;
        let cientos = Math.floor(num / divisor)
        let resto = num - (cientos * divisor)

        let strMiles = Seccion(num, divisor, 'UN MIL', 'MIL');
        let strCentenas = Centenas(resto);

        if (strMiles == '')
            return strCentenas;

        return strMiles + ' ' + strCentenas;
    }//Miles()

    function Millones(num) {
        let divisor = 1000000;
        let cientos = Math.floor(num / divisor)
        let resto = num - (cientos * divisor)

        let strMillones = Seccion(num, divisor, 'UN MILLON DE', 'MILLONES DE');
        let strMiles = Miles(resto);

        if (strMillones == '')
            return strMiles;

        return strMillones + ' ' + strMiles;
    }//Millones()

    return function NumeroALetras(num, currency) {
        currency = currency || {};
        let data = {
            numero: num,
            enteros: Math.floor(num),
            centavos: (((Math.round(num * 100)) - (Math.floor(num) * 100))),
            letrasCentavos: '',
            letrasMonedaPlural: currency.plural || 'PESOS CHILENOS',//'PESOS', 'Dólares', 'Bolívares', 'etcs'
            letrasMonedaSingular: currency.singular || 'PESO CHILENO', //'PESO', 'Dólar', 'Bolivar', 'etc'
            letrasMonedaCentavoPlural: currency.centPlural || 'CHIQUI PESOS CHILENOS',
            letrasMonedaCentavoSingular: currency.centSingular || 'CHIQUI PESO CHILENO'
        };

        //if (data.centavos > 0) {
        data.letrasCentavos = 'Y ' + (function () {
            if (data.centavos == 1)
                return Millones(data.centavos) + ' ' + data.letrasMonedaCentavoSingular;
            else
                return Millones(data.centavos) + ' ' + data.letrasMonedaCentavoPlural;
        })();
        //};

        //if (data.enteros == 0)
        //    return 'CERO ' + data.letrasMonedaPlural + ' ' + data.letrasCentavos;
        //if (data.enteros == 1)
        //    return Millones(data.enteros) + ' ' + data.letrasMonedaSingular + ' ' + data.letrasCentavos;
        //else
        return Millones(data.enteros) + ' ' + data.letrasCentavos + ' ' + data.letrasMonedaPlural;
    };

})();
//Descarga
function descargarXML(serie, numero) {
    var url = 'CuentaCobrar/ConsultarDocumento?serie=' + serie + '&numero=' + numero + '&idVenta=1';
    enviarServidor(url, descargaXML);
}
function descargaXML(rpta) {
    var listaArchivos = rpta.split("↔");
    var archivoXML = listaArchivos[1];
    window.location.href = archivoXML;

}
function descargarCDR(serie, numero) {
    var url = 'CuentaCobrar/ConsultarDocumento?serie=' + serie + '&numero=' + numero + '&idVenta=1';
    enviarServidor(url, descargaCDR);
}
function descargaCDR(rpta) {
    var listaArchivos = rpta.split("↔");
    var archivoCDR = listaArchivos[0];
    window.location.href = archivoCDR;
}
function formatNumber(num) {
    if (!num || num == 'NaN') return '-';
    if (num == 'Infinity') return '&#x221e;';
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}
function VistaPreviaNotaCredito() {
    var texto = "";
    var doc = new jsPDF('p', 'mm', 'A4');
    var imggg = logoProvesur;
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    doc.setFont('arial')
    var xic = 70;
    var altc = 4;
    doc.addImage(imggg, "JPEG", 15, 15, 50, 10);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Planta Centro", 15, 30);
    doc.text(":   Av. Nicolas Ayllón N° 2433 - Ate Lima", 45, 30);
    doc.text("    Telf: 711-0631", 45, 33);

    doc.text("Planta Sur", 15, 38);
    doc.text(":   Panamericana Sur Km. 18, Int. B-14 Mutual Ayacucho", 45, 38);
    doc.text("    San Juan de Miraflores - Lima - Lima", 45, 41);
    doc.text("    Telf.. 258-4673 Fax: 258-4289", 45, 44);

    doc.text("Planta Norte", 15, 49);
    doc.text(":   Av. Los Platinos Mz. A Lte. 24 Zona Industrial Infantas - ", 45, 49);
    doc.text("    Los Olivos - Lima - Lima ", 45, 52);
    doc.text("    Telf.. 528-3019 Telefax: 528-5877", 45, 55);
    doc.text("Web : www.provesur.com.pe / E-mail: gerencia@provesur.com.pe", 15, 60);

    doc.setFontSize(12);
    doc.rect((width / 2) + 22, 15, (width / 2) - 32, 65 - 35);
    doc.setFontType("bold");
    doc.text("RUC. 20503067391", width * 0.75 + 6, 25, "center");
    doc.text("NOTA DE CRÉDITO ELECTRÓNICA", width * 0.75 + 6, 32, "center");
    doc.text(gbi("txtNroSerie").value + "-" + gbi("txtNumero").value, width * 0.75 + 6, 39, "center");

    var xic = 70;
    var altc = 4;
    doc.rect(15, xic - 5, width - 25, altc * 3 + 13);
    doc.setFontSize(8);
    doc.setFontType("bold");
    doc.text("CLIENTE", 18, xic);
    doc.text("RUC", 18, xic + altc * 1);
    doc.text("DIRECCIÓN", 18, xic + altc * 2);
    doc.text(":", 42, xic);
    doc.text(":", 42, xic + altc * 1);
    doc.text(":", 42, xic + altc * 2);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text(gbi("txtRazonSocial").value, 44, xic);
    doc.text(gbi("txtNroDocumento").value, 44, xic + altc * 1);

    //SALTO DE LINEA DE DESCRIPCION
    let lineaAdicionalDir = 0;
    var y = 152;
    var yAlto = 7;
    var altoTemp = y;
    let lineaAdicional = 0;
    var xadCab = 3;
    if (gbi("txtDireccion").value.length > 45) {
        let desc = gbi("txtDireccion").value;
        let textoSplit = desc.split(" ");
        let cadena = "";
        let lastTxt = "";
        let prevTxt = "";
        for (let txt of textoSplit) {
            let cadTmp = (cadena + `${txt} `);
            if (cadTmp.trim().length <= 45)
                cadena += `${txt} `;
            else {
                prevTxt = cadena;
                doc.text(cadena.trim().toUpperCase(), 44, xic + (altc + lineaAdicional) * 2);
                lineaAdicional++;
                cadena = `${txt} `;
                lastTxt = `${txt} `;
            }
        }
        if (cadena !== prevTxt) {
            doc.text(cadena.trim().toUpperCase(), 44, xic + (altc + lineaAdicional + 1) * 2);
        }
        doc.setFontType("bold");
        doc.text("FECHA EMISIÓN", 18, xic + (altc + lineaAdicional) * 3);
        doc.setFontType("normal");
        doc.text(":", 42, xic + (altc + lineaAdicional) * 3);
        doc.text(gbi("txtFecha").value, 44, xic + (altc + lineaAdicional) * 3);
    } else {

        doc.text(gbi("txtDireccion").value, 44, xic + altc * 2);
        doc.setFontType("bold");
        doc.text("FECHA EMISIÓN", 18, xic + altc * 3);
        doc.setFontType("normal");
        doc.text(":", 42, xic + altc * 3);
        doc.text(gbi("txtFecha").value, 44, xic + altc * 3);
    }
    // FIN SALTO LINEA

    doc.setFontSize(8);
    doc.setFontType("bold");
    doc.text("REFERENCIA", (width / 2) + 20, xic);
    doc.text("FACTURA/BOLETA NRO", (width / 2) + 20, xic + altc * 1);
    doc.text("IMPORTE", (width / 2) + 20, xic + altc * 2);
    doc.text("FECHA", (width / 2) + 20, xic + altc * 3);
    doc.text(":", (width / 2) + 55, xic);
    doc.text(":", (width / 2) + 55, xic + altc * 1);
    doc.text(":", (width / 2) + 55, xic + altc * 2);
    doc.text(":", (width / 2) + 55, xic + altc * 3);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text(gbi("txtTipoDocumento").value, (width / 2) + 58, xic);
    doc.text(gbi("txtSerieNumDocFisico").value, (width / 2) + 58, xic + altc * 1);
    doc.text("0", (width / 2) + 58, xic + altc * 2);
    doc.text(gbi("txtFechaDocFisico").value, (width / 2) + 58, xic + altc * 3);


    //Crear Cabecera
    var xid = 95;
    var xad = 3;
    doc.setFontSize(7);
    //crea Detalle    
    var x = gbi("tb_DetalleF").querySelectorAll(".rowDet");;
    xid = 102;

    for (var i = 0; i < x.length; i++) {
        var lenDesc = x[i].children[3].innerHTML.length;
        var UnidadMedida;
        switch (gbi("cboUnidadMedida").value) {
            case "NIU": UnidadMedida = "U"; break;
            case "TNE": UnidadMedida = "TM"; break;
            case "TN": UnidadMedida = "TM"; break;
            case "KGM": UnidadMedida = "KG"; break;
            case "KG": UnidadMedida = "KG"; break;
        }
        doc.text(x[i].children[4].innerHTML, 17, xid + (i * xad)); // cantidad
        doc.text(UnidadMedida, 43, xid + (i * xad)); // UnidadMedida
        if (lenDesc > 59) {
            doc.text(x[i].children[3].innerHTML.substring(0, 59), 53, xid + (i * xad)); // DESCRIPCION
            doc.text(x[i].children[3].innerHTML.substring(59, lenDesc - 1), 53, xid + ((i + 1) * xad)); // DESCRIPCION
        }
        else {
            doc.text(x[i].children[3].innerHTML, 53, xid + (i * xad)); // DESCRIPCION
        }
        doc.text(x[i].children[6].innerHTML, 170, xid + (i * xad), "right"); //pUNIT
        doc.text(parseFloat(x[i].children[7].innerHTML).toFixed(2), 190, xid + (i * xad), "right");   // MONTO TOTAL

    }  // VENDEDOR


    doc.setFontSize(8);

    doc.setFontType("bold");
    doc.text("SUBTOTAL", 155, height - 45);  // MONTO AFECTO
    doc.text("IGV     ", 155, height - 40);  // MONTO IGV
    doc.text("TOTAL   ", 155, height - 35);  // MONTO TOTAL
    doc.text(":", 173, height - 45);
    doc.text(":", 173, height - 40);
    doc.text(":", 173, height - 35);
    doc.setFontType("normal");
    var siMon = gbi("txtMoneda").dataset.id == "1" ? "S/." : "$";

    //doc.text(datos[14], width - 15, height - 45, "right");  // MONTO AFECTO
    if (gbi("txtGravada").dataset.id == '5') {
        doc.text(formatNumber(gbi("txtInafecto").value), width - 15, height - 45, "right");  // MONTO AFECTO                
    } else {
        doc.text(formatNumber(gbi("txtGravada").value), width - 15, height - 45, "right");  // MONTO AFECTO                
    }

    doc.text(formatNumber(gbi("txtIGVF").value), width - 15, height - 40, "right");  // MONTO IGV
    doc.text(formatNumber(parseFloat(gbi("txtTotalF").value).toFixed(2)), width - 15, height - 35, "right");  // MONTO TOTAL
    doc.rect(15, height - 60, width - 25, 7);
    doc.rect(15, height - 50, width - 73, 18);


    doc.text("SON : " + numeroALetras(parseFloat(gbi("txtTotalF").value).toFixed(2), {
        plural: gbi("txtMoneda").dataset.id == "1" ? " SOLES" : " DOLARES AMERICANOS",
        singular: gbi("txtMoneda").dataset.id == "1" ? " SOL" : " DOLAR AMERICANO",
        centPlural: gbi("txtTotalF").value.split('.')[1] + '/100',
        centSingular: gbi("txtTotalF").value.split('.')[1] + '/100'
    }), 17, height - 55); // MONTO EN LETRAS

    doc.setFontType("bold");
    doc.text("MOTIVO DE EMISION", 48, height - 45, "right");
    doc.text("OBSERVACION", 48, height - 41, "right");
    doc.text(":", 53, height - 45);
    doc.text(":", 53, height - 41);
    doc.setFontType("normal");
    doc.text(gbi("txtMotivo").value, 55, height - 45);
    doc.text(gbi("txtDescripcion").value, 55, height - 41);


    doc.setFontType("normal");
    doc.setDrawColor(0);
    doc.setFillColor(53, 166, 236);
    doc.rect(15, 90, width - 25, 7, "FD");
    doc.setFontSize(8);
    xid = 95;
    doc.text("Cant.", 17, xid); //COD ALTERNO
    doc.text("U/M", 43, xid); // CANTIDAD
    doc.text("Descripción", 53, xid); // DESCRIPCION
    doc.text("Precio", 165, xid); //pUNIT
    doc.text("Total", 185, xid);   // MONTO TOTAL
    doc.line(15, 90, 15, height - 65);
    doc.line(width - 10, 90, width - 10, height - 65);
    doc.line(15, height - 65, width - 10, height - 65);
    doc.setFontSize(7);

    var iframe = document.getElementById('frmReport');
    iframe.src = doc.output('dataurlstring');
    AbrirModal("modalVistaPrevia");

}


function enviarServidor_NC(url, metodo) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open("get", url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
        if (xhr.status == 404) {
        }
    };
    xhr.send();
}