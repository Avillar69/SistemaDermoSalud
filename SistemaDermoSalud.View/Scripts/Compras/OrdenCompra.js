var cabeceras = ["idOrdenCompra", "Fecha", "N°OrdenC", "ProvDoc", "ProvRazon", "Total", "Estado"];
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
var listaLocales;
var listaMoneda;
var listaFormaPago;
var listaComprobantes;
var listaTipoCompra;
var listaSocios;
var idTablaDetalle;
var idDiv;
$('#datepicker-range').datetimepicker({ format: 'dd-mm-yyyy' });


$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFecha").datetimepicker({
    format: 'DD-MM-YYYY',
});
configBM();

var url = "OrdenCompra/ObtenerDatos";
enviarServidor(url, mostrarLista);



reziseTabla();
cfgKP(["txtArticulo", "txtMoneda", "txtDireccion", "txtRazonSocial", "txtFormaPago", "txtTipoCompra"], cfgTMKP);
cfgKP(["txtCantidad", "txtRequerimiento", "txtPrecio", "txtTotal", "txtNroDocumento", "txtNroComprobante", "txtFecha", "txtObservacion", "txtDescuento", "txtDescuentoPrincipal"], cfgTKP);

function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function JsonParse(data) {

}

function CargarTipoCambio(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲'); //lista OC

        if (Resultado == 'OK') {
            adt(datos[4], "txtTipoCambio");
        }
    }
}



function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            limpiarTodo();
            lblTituloPanel.innerHTML = "Nuevo Orden de Compra";//Titulo Insertar
            //       $("#txtLocalDet").innerHTML="funciono";
            var f = new Date();
            var fechaActual = "";
            fechaActual = (f.getDate() + "-" + (f.getMonth() + 1) + "-" + f.getFullYear());
            adt(fechaActual, "txtFecha");
            break;
            gbi("btnCancelarDetalle").style.display = "";
        case 2:
            lblTituloPanel.innerHTML = "Editar Orden de Compra";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            //$("#txtLocalDet").focus();
            break;
    }
}
$('#collapseOne-2').on('shown.bs.collapse', function () {
    reziseTabla();
})
$('#collapseOne-2').on('hidden.bs.collapse', function () {
    reziseTabla();
})
function TraerDetalle(id) {
    var url = 'OrdenCompra/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        //var mensaje = listas[2];
        if (Resultado == "OK") {

            listaSocios = listas[1].split("▼");
            //listaLocales = listas[2].split("▼");
            listaMoneda = listas[2].split("▼");
            listaFormaPago = listas[3].split("▼");
            listaComprobantes = listas[4].split("▼");
            listaTipoCompra = listas[5].split("▼");
            listaDatos = listas[6].split("▼");

            var fechaInicio = listas[7];
            var fechaFin = listas[8];
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;

            var btnTipoCompra = document.getElementById("btnModalTipoCompra");
            btnTipoCompra.onclick = function () {
                cbm("tipocompra", "Tipo de Compra", "txtTipoCompra", null,
                    ["id", "Código", "Descripción"], listaTipoCompra, cargarSinXR);
            }
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        listar();
    }
    reziseTabla();
}





function listar() {
    matriz = crearMatriz(listaDatos);
    configurarFiltroOC(cabeceras);
    mostrarMatrizOC(matriz, cabeceras, "divTabla", "contentPrincipal");
    //configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
    gbi("chkIGV").onchange = function () {
        calcularSumaDetalle();
    }
}


function mostrarMatrizOC(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 2: case 3: case 1: case 5: case 6: case 7:
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
            case 2: case 3: case 1: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-3'>";
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
function ImprimirOC() {
    var texto = "";
    var columns = cabeceras;
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = matriz[i];
    }
    var doc = new jsPDF('p', 'pt', "a4");
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    var fec = new Date();
    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
    var y = fec.getFullYear();
    //Datos Cabecera de Página
    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
    doc.setFont('helvetica')
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("Dermosalud S.A.C", 30, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 30, 40);
    doc.text("20565643143", 50, 40);
    doc.text("Dirección:", 30, 50);
    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 70, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 90, 40)
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50)
    doc.setFontSize(14);
    doc.setFontType("bold");
    //Titulo de Documento y datos
    doc.text("ORDEN DE COMPRA N° " + gbi("txtNroComprobante").value, width / 2, 95, "center");
    var xic = 140;
    var altc = 12;
    doc.setFontType("bold");
    doc.setFontSize(7);
    doc.text("SEÑOR(ES): ", 30, xic);
    doc.text("DIRECCIÓN: ", 30, xic + altc * 1);
    doc.text("RUC: ", 30, xic + altc * 2);
    doc.text("CONTACTO: ", 30, xic + altc * 3);
    doc.text("FECHA: ", width - 170, xic);
    doc.text("MONEDA: ", width - 170, xic + altc);
    doc.text("TELEFONO: ", width - 170, xic + altc * 2);
    doc.text("EMAIL: ", width - 170, xic + altc * 3);
    doc.roundedRect(26, xic - altc, width - 52, altc * 4 + 8, 3, 3)
    doc.text("Sirvase atender lo siguiente :", 30, xic + altc * 4 + 12);
    doc.setFontType("normal");

    doc.text(gbi("txtRazonSocial").value, 85, xic);
    doc.text(gbi("txtDireccion").value, 85, xic + altc);
    doc.text(gbi("txtNroDocumento").value, 85, xic + altc * 2);
    doc.text(gbi("txtFecha").value, width - 120, xic);
    doc.text(gbi("txtMoneda").value, width - 120, xic + altc);
    //Inicio de Detalle
    //Crear Cabecera
    var xid = 220;
    var xad = 12;
    doc.setFontType("bold");
    doc.setFontSize(7);
    doc.text("CÓDIGO", 30, xid);
    doc.text("DESCRIPCIÓN", 90, xid);
    doc.text("U.M.", width - 235, xid);
    doc.text("CANTIDAD", width - 180, xid, 'right');
    doc.text("PRECIO", width - 135, xid, 'right');
    doc.text("DESCUENTO", width - 85, xid, 'right');
    doc.text("TOTAL", width - 40, xid, 'right');
    doc.line(30, xid + 3, width - 30, xid + 1);
    doc.setFontType("normal");
    doc.setFontSize(6.5);
    //Crear Detalle
    var x = document.querySelectorAll(".rowDet");
    for (var i = 0; i < x.length; i++) {
        doc.text("SM01300064", 30, xid + xad + (i * xad));
        doc.text(x[i].children[3].innerHTML, 90, xid + xad + (i * xad));
        doc.text(x[i].children[5].innerHTML, width - 235, xid + xad + (i * xad));
        doc.text(x[i].children[4].innerHTML, width - 180, xid + xad + (i * xad), 'right');
        doc.text(x[i].children[6].innerHTML, width - 135, xid + xad + (i * xad), 'right');
        doc.text("0.00", width - 85, xid + xad + (i * xad), 'right');
        doc.text(x[i].children[7].innerHTML, width - 40, xid + xad + (i * xad), 'right');
    }
    //RESUMEN 
    doc.setFontType("bold");
    doc.text("TOTAL PRODUCTOS: ", 30, height - 80 - altc * 8);
    doc.text("FECHA DE ENTREGA: ", 30, height - 80 - altc * 7);
    doc.text("LUGAR DE ENTREGA: ", 30, height - 80 - altc * 6);
    doc.text("CONDICION DE PAGO: ", 30, height - 80 - altc * 5);
    doc.setFontType("normal");
    doc.text("" + x.length, 110, height - 80 - altc * 8);
    doc.text(gbi("txtFecha").value, 110, height - 80 - altc * 7);
    doc.text("Av. Santa Lucia Nro. 237 Z.I. la Aurora Ate", 110, height - 80 - altc * 6);
    doc.text(gbi("txtFormaPago").value, 110, height - 80 - altc * 5);
    doc.text("NOS RESERVAMOS EL DERECHO DE DEVOLVER LA MERCADERÍA QUE NO ESTE DE ACUERDO A NUESTRAS ESPECIFICACIONES", 30, height - 70 - altc * 4);

    //RESUMEN DE TOTALES

    doc.setFontType("bold");
    doc.text("SUBTOTAL: ", width - 150, height - 80 - altc * 8, "right");
    doc.text("% DCTO: ", width - 150, height - 80 - altc * 7, "right");
    doc.text("% IGV: ", width - 150, height - 80 - altc * 6, "right");
    doc.text("TOTAL: ", width - 150, height - 80 - altc * 5, "right");

    doc.setFontType("normal");
    doc.text(gbi("txtSubTotalF").value, width - 50, height - 80 - altc * 8, "right");
    doc.text("0.00", width - 50, height - 80 - altc * 7, "right");
    doc.text(gbi("txtIGVF").value, width - 50, height - 80 - altc * 6, "right");
    doc.text(gbi("txtTotalF").value, width - 50, height - 80 - altc * 5, "right");
    //FOOTER
    doc.text("NOTA: SIRVASE ADJUNTAR LA PRESENTE O/C A", 106, height - 70 - altc * 2, "center");
    doc.text("LA FACTURA CORRESPONDIENTE AL MOMENTO", 106, height - 70 - altc, "center");
    doc.text("DE PRESENTARLO A NUESTRA EMPRESA", 106, height - 70, "center");

    doc.roundedRect(width - 210, height - 90 - altc * 8, 180, 50, 3, 3)

    doc.roundedRect(26, height - 110, 160, 60, 3, 3)
    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');
}
function eliminar(id) {

    swal({
        title: 'Desea Eliminar la Orden Compra? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var u = "OrdenCompra/Eliminar?idOrdenCompra=" + id;
                enviarServidor(u, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó la Orden Compra', 'error');
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó la Orden de compra';
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
            case "txtCantidad":
                gbi("txtPrecio").focus();
                break;
            case "txtPrecio":
                gbi("txtTotal").focus();
                break;
            case "txtTotal":
                addItem(0, []);
                limpiarCamposDetalle();
                gbi("txtArticulo").focus();
                calcularSumaDetalle();
                break;
            case "txtFecha":
                var txtFecha = gbi("txtFecha").value;
                var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + txtFecha;
                enviarServidor(url, CargarTipoCambio);
                gbi("txtObservacion").focus();
                break;
            case "txtObservacion":
                gbi("txtRazonSocial").focus();
                break;
            /*case "txtOrdenCompra":
                gbi("txtNroComprobante").focus();
                break;*/
            case "txtNroComprobante":
                gbi("txtFecha").focus();
                break;

            case "txtDescuentoPrincipal":
                gbi("txtArticulo").focus();
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
function cargarListaArticulo(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
}

function configBM() {

    var btnModalArticulo = document.getElementById("btnModalArticulo");
    btnModalArticulo.onclick = function () {
        cbmu("Medicamento", "Medicamento", "txtArticulo", null,
            ["idMedicamentos", "Codigo", "Descripcion", "Laboratorio", "PagoMedicamento"], ' /OperacionesStock/cargarMedicamento', cargarListaArticulo);
    }
    var btnFormaPago = document.getElementById("btnModalFormaPago");
    btnFormaPago.onclick = function () {
        cbmu("formaPago", "Forma de Pago", "txtFormaPago", null,
            ["idFormaPago", "Código", "Descripción"], "/FormaPago/ObtenerDatos?Activo=A", cargarLista);
    };
    var btnSocio = document.getElementById("btnModalRazonSocial");
    btnSocio.onclick = function () {
        bDM("txtDireccion");
        cbmu("socio", "Proveedores", "txtRazonSocial", "txtNroDocumento",
            ["idSocioNegocio", "Documento", "Razón Social"], "/SocioNegocio/ObtenerSocioxTipo?tipo=P", cargarLista);
    };
    var btnMoneda = document.getElementById("btnModalMoneda");
    btnMoneda.onclick = function () {
        cbmu("moneda", "Moneda", "txtMoneda", null,
            ["idMoneda", "Código", "Descripción"], "/Moneda/ObtenerDatos?Activo=A", cargarLista);
    };
    var btnModRequerimiento = gbi("btnModalRequerimiento");
    btnModRequerimiento.onclick = function () {
        cbmu("requerimiento", "Requerimiento", "txtRequerimiento", null,
            ["idReq", "fecha", "NroReque", "Obs"], "/Requerimientos/ListarRequerimiento?estRequerimiento=3", cargarLista);
    };

    var btnModalDireccion = gbi("btnModalDireccion");
    btnModalDireccion.onclick = function () {
        var Prov = document.getElementById("txtRazonSocial").getAttribute('data-id');
        if (Prov) {
            cbmu("direccion", "Dirección", "txtDireccion", null,
                ["idDireccion", "Descripción"], "/SocioNegocio/ObtenerDireccionxID?id=" + Prov, cargarLista);
        }
        else {
            mostrarRespuesta("Error", "Debe Seleccionar un Proveedor", "error");
        }
    };
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        var u = "OrdenCompra/ObtenerPorFecha?fechaInicio=" + gbi("txtFilFecIn").value + "&fechaFin=" + gbi("txtFilFecFn").value;
        enviarServidor(u, cargarBusqueda);
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
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Orden Compra", cabeceras, matriz, "Orden Compra", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "Orden Compra", cabeceras, matriz, "Orden Compra", "a4", "i");
    }


    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    };

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
            var url = "OrdenCompra/Grabar";
            var frm = new FormData();
            //ejemplo
            //frm.append("idGuiaRemision", gvt("txtID"));//id gvc
            frm.append("idEmpresa", 1);//gvt texto 
            frm.append("idOrdenCompra", gvt("txtID"));
            frm.append("idTipoCompra", gvc("txtTipoCompra"));
            frm.append("NumOrdenCompra", 0);
            frm.append("EstadoOrdenCompra", 0);
            frm.append("idFormaPago", gvc("txtFormaPago"));
            if (gvt("txtObservacion") == "") {
                frm.append("Observacion", " ");
            }
            else {
                frm.append("Observacion", gvt("txtObservacion"));
            }
            frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
            frm.append("EstadoAprobacion", 0);
            var porId = document.getElementById("chkIGV").checked;
            frm.append("IGVcheck", porId);
            frm.append("idPedido", gvt("txtRequerimiento"));
            if (gvt("txtRequerimiento") == "") {
                frm.append("NumPedido", " ");
            }
            else {
                frm.append("NumPedido", gvt("txtRequerimiento"));
            }

            frm.append("FechaOrdenCompra", gvt("txtFecha"));
            frm.append("FechaEntrega", gvt("txtFecha"));
            frm.append("FechaFin", gvt("txtFecha"));
            frm.append("idProveedor", gvc("txtRazonSocial"));
            frm.append("ProveedorRazon", gvt("txtRazonSocial"));
            frm.append("ProveedorDocumento", gvt("txtNroDocumento"));
            frm.append("ProveedorDireccion", gvt("txtDireccion"));
            frm.append("idMoneda", gvc("txtMoneda"));
            frm.append("EstadoOC", 0);
            frm.append("SubTotalNacional", gvt("txtSubTotalF"));
            frm.append("SubTotalExtranjero", gvt("txtSubTotalF"));
            frm.append("TipoCambio", gvt("txtTipoCambio"));
            frm.append("IGVNacional", gvt("txtIGVF"));
            frm.append("IGVExtranjero", gvt("txtIGVF"));
            frm.append("TotalNacional", gvt("txtTotalF"));
            frm.append("TotalExtranjero", gvt("txtTotalF"));
            frm.append("Impreso", 0);
            frm.append("Estado", true);
            frm.append("flgIGV", gvt("txtNroComprobante"));
            frm.append("SubTotalNacional", gvt("txtSubTotalF"));
            frm.append("IGVNacional", gvt("txtIGVF"));
            frm.append("TotalNacional", gvt("txtTotalF"));

            frm.append("cadDetalle", crearCadDetalleOrden());
            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
            gbi("btnGrabarDetalle").style.display = "none";
            gbi("btnCancelarDetalle").style.display = "none";

        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ImprimirOC();
    };
}
function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[2].dataset.id;
    gbi("txtArticulo").value = row.children[2].innerHTML;
    gbi("txtCantidad").value = row.children[3].innerHTML;
    gbi("txtPrecio").value = row.children[5].innerHTML;
    gbi("txtTotal").value = row.children[6].innerHTML;
    gbi("btnGrabarDetalle").innerHTML = "Actualizar";
    idTablaDetalle = id;
    gbi("btnGrabarDetalle").style.display = "";
    gbi("btnCancelarDetalle").style.display = "";
}
function guardarItemDetalle() {
    var id = idTablaDetalle;
    var row = gbi(id);
    if (validarControl("txtCantidad")) error = false;
    row.children[2].dataset.id = gbi("txtArticulo").dataset.id;
    row.children[2].innerHTML = gbi("txtArticulo").value;
    row.children[3].innerHTML = gbi("txtCantidad").value;
    row.children[5].innerHTML = gbi("txtPrecio").value;
    row.children[6].innerHTML = gbi("txtTotal").value;
    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
    calcularSumaDetalle();

    limpiarCamposDetalle();
}
function eliminarDetalle(id) {

    limpiarControl("txtCantidad");
    bDM("txtArticulo");
    bDM("txtPrecio");
    bDM("txtTotal");

    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
    calcularSumaDetalle();
}
function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-left:0px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[5] : "UNI") + '</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[8]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[10]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
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

        cdet += $(".rowDet")[obj].children[0].innerHTML;//idDetalle
        cdet += "|" + $(".rowDet")[obj].children[1].innerHTML;
        cdet += "|" + $(".rowDet")[obj].children[2].dataset.id;//idArticulo
        cdet += "|" + $(".rowDet")[obj].children[2].innerHTML;//DescripcionArticulo
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;//Cantidad
        cdet += "|0";//idCategoria
        cdet += "|0";//descripcionCategoria
        cdet += "|" + $(".rowDet")[obj].children[5].innerHTML;//PrecioNacional
        cdet += "|" + $(".rowDet")[obj].children[5].innerHTML;//PrecioExtranjero
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//SubTotalNacional
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//SubTotalExtranjero
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function calcularSumaDetalle() {
    var sum = 0;
    $(".rowDet").each(function (obj) {
        sum += parseFloat($(".rowDet")[obj].children[6].innerHTML);
    });
    if (gbi("chkIGV").checked) {
        gbi("txtSubTotalF").value = (parseFloat(sum * 100 / 118)).toFixed(3);
        gbi("txtDescuento").value = (parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(3);
        gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value)).toFixed(3);
    }
    else {
        gbi("txtSubTotalF").value = (parseFloat(sum)).toFixed(3);
        gbi("txtDescuento").value = (parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(3);
        gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value)).toFixed(3);
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
//Borrar Data del Modal x ID
function bDM(v) {
    gbi(v).value = "";
    gbi(v).dataset.id = "";
}
function limpiarTodo() {
    bDM("txtTipoCompra");
    gbi("chkIGV").checked = false;
    bDM("txtNroComprobante");
    limpiarControl("txtFecha");
    limpiarControl("txtObservacion");
    limpiarControl("txtDescuento");
    limpiarControl("txtID");
    bDM("txtRazonSocial");
    limpiarControl("txtNroDocumento");
    limpiarControl("txtSubTotalF");
    limpiarControl("txtIGVF");
    limpiarControl("txtTotalF");

    bDM('txtDescuentoPrincipal');
    bDM("txtDireccion");
    bDM("txtMoneda");
    bDM("txtFormaPago");
    bDM("txtRequerimiento");
    gbi("tb_DetalleF").innerHTML = "";

    limpiarCamposDetalle();

    for (let item of gbi("rowFrm").querySelectorAll("input")) {
        if (item.id) { limpiarControl(item.id); }
    }
}
function limpiarCamposDetalle() {
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

        case "txtArticulo":
            var txtArt = gbi("txtArticulo").dataset.id;
            var url = 'Medicamento/ObtenerDatosxID?id=' + txtArt;
            enviarServidor(url, cLP);
            break;
        case "txtRazonSocial":
            return gbi("txtDireccion");
            break;
        case "txtFormaPago":
            return gbi("txtDescuentoPrincipal");
            break;
        case "txtDescuentoPrincipal":
            return gbi("txtArticulo");
            break;
        case "txtMoneda":
            return gbi("txtFormaPago");
            break;
        case "txtTipoCompra":
            return gbi("txtRequerimiento");
            break;
        case "txtNroComprobante":
            return gbi("txtFecha");
            break;
        case "txtFecha":
            return gbi("txtObservacion");
            break;
        case "txtObservacion":
            return gbi("txtRazonSocial");
            break;
        case "txtDireccion":
            return gbi("txtMoneda");
            break;
        case "txtRequerimiento":
            var idRequerimiento = gbi("txtRequerimiento").dataset.id;
            var url = 'Requerimientos/ObtenerDatosxID?id=' + idRequerimiento;
            enviarServidor(url, CargarDetalleRequerimiento);
            return gbi("txtNroComprobante");
            break;
        default:
            break;
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

function cLP(r) {
    if (r.split('↔')[0] == "OK") {
        var listas = r.split('↔');
        var lp = listas[2].split("▲");
        var UnidadMedida = lp[0];
        var Precio = lp[9];
        gbi("txtPrecio").value = Precio == null ? 0 : Precio;
        gbi("txtCantidad").focus();
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
    if (txtModal.id == "txtArticulo") {
        txtModal.dataset.codart = gbi("md" + num + "-1").innerHTML;
    }
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    gbi("txtFiltroMod").value = "";
    CerrarModalR("modal-Modal", next);

}
function mostrarMatrizR(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 2: case 3: case 1: case 5: case 6: case 7:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='hidden-sm-up'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-1'>";

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

function configurarFiltroOC(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatrizR(matriz, cabe, "divTabla", "contentPrincipal");
    };
}
function CerrarModalR(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}
function validarFormulario() {
    var error = true;
    //Aqui Ingresar todos los campos a validar || validarCombo || validarTexto

    if (validarControl("txtTipoCambio")) error = false;
    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtDireccion")) error = false;
    if (validarControl("txtNroDocumento")) error = false;
    if (validarControl("txtMoneda")) error = false;
    if (validarControl("txtFormaPago")) error = false;
    if (validarControl("txtTipoCompra")) error = false;
    if (validarControl("txtFecha")) error = false;

    return error;
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        // var alm = listas[3].split("▼");
        var dir = listas[2].split("▼");
        var det = listas[3].split("▼");
        if (Resultado == 'OK') {
            adc(listaTipoCompra, datos[2], "txtTipoCompra", 2);
            // adt(datos[4], "txtOrdenCompra");
            adt(datos[6], "txtRequerimiento");
            adt(datos[3], "txtNroComprobante", 1);
            adt(datos[23], "txtFecha");
            adc(listaSocios, datos[10], "txtRazonSocial", 1);
            adc(listaSocios, datos[10], "txtNroDocumento", 2);
            adt(datos[13], "txtDireccion");
            adc(listaMoneda, datos[14], "txtMoneda", 1);
            adc(listaFormaPago, datos[5], "txtFormaPago", 1);
            adt(datos[17], "txtTipoCambio");
            if (datos[31] == 'TRUE') {
                gbi("chkIGV").checked = true;
            } else {
                gbi("chkIGV").checked = false;
            }
            adt(datos[32], "txtObservacion");
            adt(datos[33], "txtDescuentoPrincipal");
            adt(datos[0], "txtID");

            if (det.length >= 1) {
                if (det[0].trim() != "") {
                    for (var i = 0; i < det.length; i++) {
                        addItem(1, det[i].split("▲"));
                    }
                }
            }
            calcularSumaDetalle();
        }
    }
}

function CargarDetalleRequerimiento(rpta) {
    gbi("tb_DetalleF").innerHTML = "";
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲'); //lista Requerimientos
        var det = listas[2].split("▼");//lista Requerimiento Detalle
        if (Resultado == 'OK') {
            adc(listaTipoCompra, datos[4], "txtTipoCompra", 2);
            if (det.length >= 1) {
                if (det[0].trim() != "") {
                    for (var i = 0; i < det.length; i++) {
                        addItemRequerimiento(1, det[i].split("▲"));
                    }
                }
            }
        }
    }
}
function addItemRequerimiento(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : "") + '">' + (tipo == 1 ? data[5] : "") + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[15] : 'Und.') + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + 0 + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + 0 + '</div>';
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