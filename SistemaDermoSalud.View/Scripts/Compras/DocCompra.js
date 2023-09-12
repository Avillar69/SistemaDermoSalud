var cabeceras = ["id", "Fecha", "Serie", "Número", "Razon Social", "SubTotal", "IGV", "Total"];
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
/*objetos globales para el detalle pago si es al contado*/
var listaSocioPorSiaca;
var listarCuentasO;
var listarBanco;
var IdEliminar;
/*fin de objetos globales pago detalle*/
var lp2;

$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFecha").datetimepicker({
    format: 'DD-MM-YYYY',
});
$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
$('#collapseOne-2').on('shown.bs.collapse', function () {
    reziseTabla();
})
$('#collapseOne-2').on('hidden.bs.collapse', function () {
    reziseTabla();
})

//Inicializando
var url = "Compras/ObtenerDatos";
enviarServidor(url, mostrarLista);
configBM();
reziseTabla();
cfgKP(["txtMoneda", "txtDireccion", "txtRazonSocial", "txtFormaPago", "txtTipoDocumento", "txtTipoCompra","txtArticulo"], cfgTMKP);
cfgKP(["txtArticulo", "txtCantidad", "txtPrecio", "txtTotal", "txtNroSerie", "txtNroSerie", "txtNroDocumento", "txtObservacion", "txtDescuento", "txtDescuentoPrincipal", "txtNroComprobante", "txtNroComprobante", "txtFecha", "txtOC"], cfgTKP);
//configNav();
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        matriz = crearMatriz(listaDatos);
        configurarFiltro(cabeceras);
        mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
        reziseTabla();
    }
}
function BuscarxFecha(f1, f2) {
    var url = 'Compras/ObtenerPorFecha?fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
}
function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            listaSocios = listas[1].split("▼");
            listaMoneda = listas[2].split("▼");
            listaFormaPago = listas[3].split("▼");
            listaComprobantes = listas[4].split("▼");
            listaTipoCompra = listas[5].split("▼");
            listaDatos = listas[6].split("▼");
            var fechaInicio = listas[7];
            var fechaFin = listas[8];
            FechaActual = listas[8];
            listarBanco = listas[9].split("▼");
            listarCuentasO = listas[10].split("▼");
            //ingresar el banco
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;
            gbi("txtFecha").value = FechaActual;
            var btnTipoDocumento = document.getElementById("btnModalTipoDocumento");
            btnTipoDocumento.onclick = function () {
                cbm("tipodocumento", "Tipo de Documento", "txtTipoDocumento", null,
                    ["idTipoComprobante", "Descripción"], listaComprobantes, cargarSinXR);
            }
            var btnTipoCompra = document.getElementById("btnModalTipoCompra");
            btnTipoCompra.onclick = function () {
                cbm("tipocompra", "Tipo de Compra", "txtTipoCompra", null,
                    ["id", "Código", "Descripción"], listaTipoCompra, cargarSinXR);
            }
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        llenarCombo(listarBanco, "cboBanco", "Seleccione");

        if (listarCuentasO.length > 0 && listarCuentasO[0] !== "")
            llenarCombo(listarCuentasO, "cboOrigen", "Seleccione");

        listar();
    }
    reziseTabla();
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nuevo Documento de Compra";
            gbi("txtFecha").value = FechaActual;
            gbi("txtTipoCompra").focus();
            var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + gbi("txtFecha").value;
            enviarServidor(url, CargarTipoCambio);
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Documento de Compra";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            break;
    }
}
function TraerDetalle(id) {
    var url = 'Compras/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function listar() {
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
function mostrarMatriz(matriz, cabeceras, tabId, contentID) {
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
                        case 2: case 3: case 5: case 6: case 7:
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
            case 2: case 3: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            case 1:
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
            case "txtArticulo":
                gbi("txtCantidad").focus();
                break;
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
                    gbi("txtArticulo").focus();
                    calcularSumaDetalle();
                }
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
            case "txtDescuentoPrincipal":
                gbi("txtArticulo").focus();
                break;
            case "txtNroSerie":
                if (gbi("txtNroSerie").value.trim().length == 0) {
                    mostrarRespuesta("Alerta", "Llene el Nro de Serie", "error");
                }
                else {
                    gbi("txtNroSerie").value = pad(gbi("txtNroSerie").value, 4);
                    gbi("txtNroComprobante").focus();
                }
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

                gbi("txtTotal").value = parseFloat(c * p).toFixed(2);
                return valor;
                break;
            case "txtDescuentoPrincipal":
                calcularSumaDetalle();
                break;
            default:
                break;

        }
    }
}
function buscarXCombo() {
    var comb = document.getElementById("cbxTipoOperacion").value;

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
function eliminar(id) {
    swal({
        title: 'Desea Eliminar el Documento de  Compra? ',
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
                var u = "PagarSocio/BuscarPago?id=" + id + "&idEmpresa=" + 1;
                enviarServidor(u, BuscarPago);
            } else {
                swal('Cancelado', 'No se eliminó el Documento de Compra', 'error');
            }
        });
}
function BuscarPago(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var datos = data[1].split('▲');
        var mensaje = '';

        if (res == 'OK' && datos[0] != [""]) {

            swal({
                title: 'Este Documento de  Compra tiene Pagos Realizados',
                text: 'No podrá recuperar los datos eliminados.',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Si, Eliminalo!',
                closeOnConfirm: false,
                closeOnCancel: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        var u = "Compras/Eliminar?idDocumentoCompra=" + datos[25];
                        enviarServidor(u, eliminarListar);

                    } else {
                        mostrarRespuesta('Cancelado', 'Elimine los pagos realizados manualmenteeee', 'error');
                    }
                });
        } else {
            var u = "Compras/Eliminar?idDocumentoCompra=" + IdEliminar;
            enviarServidor(u, eliminarListar);
        }
    }
}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó el Documento de compra';
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
/*function cargarAlmacen(rpta) {
    if (rpta.split('↔')[0] == "OK") {
        var listas = rpta.split('↔');
        listaAlmacenes = listas[2].split("▼");
        llenarCombo(listaAlmacenes, "cboAlmacen", "Seleccione");
    }
}*/
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
    var btnModalArticulo = document.getElementById("btnModalArticulo");
    btnModalArticulo.onclick = function () {
        cbmu("Medicamento", "Medicamento", "txtArticulo", null,
            ["idMedicamentos", "Codigo", "Descripcion", "Laboratorio", "Precio"], ' /OperacionesStock/cargarMedicamento', cargarListaArticulo);
    }

    var btnFormaPago = document.getElementById("btnModalFormaPago");
    btnFormaPago.onclick = function () {
        cbmu("formaPago", "Forma de Pago", "txtFormaPago", null,
            ["idFormaPago", "Código", "Descripción"], "/FormaPago/ObtenerDatos?Activo=A", cargarLista);
    }

    var btnSocio = document.getElementById("btnModalRazonSocial");
    btnSocio.onclick = function () {
        cbmu("socio", "Proveedores", "txtRazonSocial", "txtNroDocumento",
            ["idSocioNegocio", "Documento", "Razón Social"], "/SocioNegocio/ObtenerSocioxTipo?tipo=P", cargarLista);



    }
    var btnMoneda = document.getElementById("btnModalMoneda");
    btnMoneda.onclick = function () {
        cbmu("moneda", "Moneda", "txtMoneda", null, ["idMoneda", "Código", "Descripción"], "/Moneda/ObtenerDatos?Activo=A", cargarLista);
    }

    // --------------------ingresando la lista Orden de compra

    var btnModOrdenCom = gbi("btnModalOrdenCompra");
    btnModOrdenCom.onclick = function () {
        cbmu("ordCompra", "OrdCompra", "txtOC", null,
            ["idOC", "fecha", "N°Compra", "Obs"], "/OrdenCompra/ListarOrdenCompra?ordenCompra=3", cargarLista);

    }

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
    }
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Doc. Compras", cabeceras, matriz, "Documento Compras", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "Doc. Compras", cabeceras, matriz, "Documento Compras", "a4", "i");
    }

    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            //if (gvc("txtFormaPago") == "2") {
            //    if (lp2 != "") {
            //        AbrirModal("modalPago");
            //    } else {
            //        mostrarRespuesta("Error", "Debe Registrar una cuenta bancaria del Proveedor", "error");
            //    }
            //}
            //else {

                if (gbi("txtDescuentoPrincipal").value >= 100) {
                    mostrarRespuesta("Alerta", "El porcentaje no puede ser mayor al 100%", "error");
                    return;
                }
                var url = "Compras/Grabar";
                var frm = new FormData();
                frm.append("idDocumentoCompra", gvt("txtID"));
                frm.append("idTipoCompra", gvc("txtTipoCompra"));
                frm.append("idTipoDocumento", gvc("txtTipoDocumento"));
                frm.append("idProveedor", gvc("txtRazonSocial"));
                frm.append("ProveedorRazon", gvt("txtRazonSocial"));
                frm.append("ProveedorDireccion", gvt("txtDireccion"));
                frm.append("ProveedorDocumento", gvt("txtNroDocumento"));
                frm.append("idMoneda", gvc("txtMoneda"));
                frm.append("idOrdenCompra", gvc("txtOC"));
                frm.append("idFormaPago", gvc("txtFormaPago"));
                frm.append("EstadoDoc", "P");
                frm.append("FechaDocumento", gvt("txtFecha"));
                frm.append("SerieDocumento", gvt("txtNroSerie"));
                frm.append("NumDocumento", gvt("txtNroComprobante"));
                var porId = document.getElementById("chkIGV").checked;
                frm.append("flgIGV", porId);
                frm.append("SubTotalNacional", gvt("txtSubTotalF"));
                frm.append("IGVNacional", gvt("txtIGVF"));
                frm.append("TotalNacional", gvt("txtTotalF"));
                frm.append("cadDetalle", crearCadDetalle());
                frm.append("Estado", true);
                frm.append("ObservacionCompra", gvt("txtObservacion"));

                frm.append("TipoCambio", gvt("txtTipoCambio"));
                frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
                frm.append("FechaVencimiento", gvt("txtFechaVencimiento"));

                swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
                enviarServidorPostKevin(url, actualizarListar, frm);


            //}
        }
    };

    var btnGrabarPago = document.getElementById("btnGrabarPago");
    btnGrabarPago.onclick = function () {
        if (validarFormulario()) {

            var url = "Compras/Grabar";
            var frm = new FormData();
            frm.append("idDocumentoCompra", gvt("txtID"));
            frm.append("idTipoCompra", gvc("txtTipoCompra"));
            frm.append("idTipoDocumento", gvc("txtTipoDocumento"));
            frm.append("idProveedor", gvc("txtRazonSocial"));
            frm.append("ProveedorRazon", gvt("txtRazonSocial"));
            frm.append("ProveedorDireccion", gvt("txtDireccion"));
            frm.append("ProveedorDocumento", gvt("txtNroDocumento"));
            frm.append("idMoneda", gvc("txtMoneda"));
            frm.append("idOrdenCompra", gvc("txtOC"));
            frm.append("idFormaPago", gvc("txtFormaPago"));
            frm.append("EstadoDoc", "P");
            frm.append("FechaDocumento", gvt("txtFecha"));
            frm.append("SerieDocumento", gvt("txtNroSerie"));
            frm.append("NumDocumento", gvt("txtNroComprobante"));
            frm.append("SubTotalNacional", gvt("txtSubTotalF"));
            frm.append("IGVNacional", gvt("txtIGVF"));
            frm.append("TotalNacional", gvt("txtTotalF"));
            frm.append("cadDetalle", crearCadDetalle());
            frm.append("Estado", true);
            var porId = document.getElementById("chkIGV").checked;
            frm.append("flgIGV", porId);
            if (gvt("txtObservacion") == "") {
                frm.append("ObservacionCompra", " ");
            }
            else {
                frm.append("ObservacionCompra", gvt("txtObservacion"));
            }
            frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
            frm.append("FechaVencimiento", gvt("txtFecha"));
            frm.append("idPagoDetalle", 0);
            frm.append("idEmpresa", 1);
            frm.append("idCajaDetalle", 0);
            frm.append("Observacion", "0");
            frm.append("idTipoOperacion", gvt("txtTipoOperacion"));
            frm.append("DescripcionOperacion", $("#cbxTipoOperacion option:selected").text());
            frm.append("NumeroOperacion", gvt("txtNOperacion"));
            frm.append("idConcepto", "0");
            frm.append("Concepto", "0");
            frm.append("DescripcionFormaPago", gvt("txtFormaPago"));
            frm.append("Monto", gvt("txtTotalF"));
            frm.append("TipoCambio", gvt("txtTipoCambio"));
            frm.append("idCuentaBancario", gvt("cboOrigen"));
            frm.append("NumeroCuenta", $("#cboOrigen option:selected").text());
            frm.append("idCuentaBancarioDestino", gvt("cboDestino"));/////////
            frm.append("NumeroCuentaDestino", $("#cboDestino option:selected").text());
            frm.append("FechaDetalle", gvt("txtFecha"));

            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);

            CerrarModal("modalPago");

        }
    };
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }

    var txtFecha = gbi("txtFecha");
    txtFecha.onblur = function () {
        var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + gbi("txtFecha").value;
        enviarServidor(url, CargarTipoCambio);
        gbi("txtObservacion").focus();
    }

    var btnAgregarDetalle = gbi("btnAgregarDetalle");
    btnAgregarDetalle.onclick = function () {
        if (validarAgregarDetalle()) {
            addItem(0, []);
            limpiarCamposDetalle();
            gbi("txtArticulo").focus();
            calcularSumaDetalle();
        }
    }
}
function enviarServidorPostKevin(url, metodo, frm) {
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
function cargarListaArticulo(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
}
function buscarXCombo() {
    var e = gvt("txtTipoOperacion");
    var a = $("#txtTipoOperacion option:selected").value;
    var comb = document.getElementById("txtTipoOperacion").value;
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
    if (comb == 4) {//cuando es deposito
        $("#divBanco").hide();
        $("#divCuentaDestino").hide();
        $("#divCuentaOrigen").hide();
        $("#divOperacion").hide();
        $("#divFCC").hide();
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
function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[2].dataset.id;
    gbi("txtArticulo").value = row.children[2].innerHTML;
    gbi("txtCantidad").value = row.children[3].innerHTML;
    gbi("txtPrecio").value = row.children[5].innerHTML;
    gbi("txtTotal").value = row.children[6].innerHTML;
    gbi("btnGrabarDetalle").innerHTML = "Actualizar";
    idTablaDetalle = id;
    //para que aparescan , ya que en el html  esta en style="display:none;"
    gbi("btnGrabarDetalle").style.display = "";
    gbi("btnCancelarDetalle").style.display = "";
    $("#btnAgregarDetalle").hide();
}
function guardarItemDetalle() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[2].dataset.id = gbi("txtArticulo").dataset.id;
    row.children[2].innerHTML = gbi("txtArticulo").value;
    row.children[3].innerHTML = gbi("txtCantidad").value;
    row.children[5].innerHTML = gbi("txtPrecio").value;
    row.children[6].innerHTML = gbi("txtTotal").value;
    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
    calcularSumaDetalle();
    limpiarCamposDetalle();
    $("#btnAgregarDetalle").show();
}
function eliminarDetalle(id) {

    limpiarControl("txtCantidad");
    bDM("txtArticulo");
    bDM("txtPrecio");
    bDM("txtTotal");

    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
    $("#btnAgregarDetalle").show();
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
    calcularSumaDetalle();
}
//Agregar Item a Tabla Detalle
function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-left:0px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[5] : "UNI") + '</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[8]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[10]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
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
function validarAgregarDetalle() {
    var error = true;
    if (validarControl("txtArticulo")) error = false;
    if (validarControl("txtCantidad")) error = false;
    if (validarControl("txtPrecio")) error = false;
    if (validarControl("txtTotal")) error = false;
    return error;
}
//Crear Detalle como Cadena para env Serv.
function crearCadDetalle() {
    var cdet = "";
    $(".rowDet").each(function (obj) {
        cdet += $(".rowDet")[obj].children[0].innerHTML;//idDetalle
        cdet += "|" + $(".rowDet")[obj].children[1].innerHTML;
        cdet += "|" + ($(".rowDet")[obj].children[2].dataset.id || "0");//idArticulo
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
        gbi("txtSubTotalF").value = parseFloat(sum * 100 / 118).toFixed(3);
        gbi("txtDescuento").value = parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100).toFixed(3);
        gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value).toFixed(3);
    }
    else {
        gbi("txtSubTotalF").value = parseFloat(sum).toFixed(3);
        gbi("txtDescuento").value = parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100).toFixed(3);
        gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value).toFixed(3);
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
                mensaje = 'Se adicionó el Documento de Compra';
                tipo = 'success';
            }
            else if (res == "REPITE") {
                mensaje = 'Revise el proveedor, serie y numero no se pueden repetir';
                tipo = 'error';
            }
            else {
                mensaje = data[1];//aca entra el error
                tipo = 'error';
            }
        }
        else {
            if (res == 'OK') {
                mensaje = 'Se actualizó el Documento de Compra';
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
function limpiarTodo() {
    gbi("chkIGV").checked = false;
    bDM("txtTipoCompra");
    bDM("txtTipoDocumento");
    bDM("txtNroSerie");
    bDM("txtObservacion");
    bDM('txtDescuentoPrincipal');
    bDM("txtDescuento");
    bDM("txtNroComprobante");
    limpiarControl("txtFecha");
    bDM("txtRazonSocial");
    limpiarControl("txtNroDocumento");
    bDM("txtDireccion");
    bDM("txtMoneda");
    bDM("txtFormaPago");
    gbi("tb_DetalleF").innerHTML = "";
    limpiarControl("txtSubTotalF");
    limpiarControl("txtIGVF");
    limpiarControl("txtTotalF");
    limpiarCamposDetalle();
    limpiarControl("txtID");
    limpiarControl("txtFechaVencimiento");
    limpiarControl("txtNOperacion");
    limpiarControl("txtTipoCambio");

    var lblNotas = document.getElementById('txtNotas');
    lblNotas.innerHTML = "";

    for (let item of gbi("rowFrm").querySelectorAll("input")) {
        if (item.id) { limpiarControl(item.id); }
    }

    //limpiarControl("txtBanco");
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
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtArticulo":
            var txtArt = gbi("txtArticulo").dataset.id;
            var url = 'Medicamento/ObtenerDatosxID?id=' + txtArt;
            enviarServidor(url, cLP);
            break;
        case "txtRazonSocial":
            var Tipo = document.getElementById("txtRazonSocial").getAttribute('data-id');
            if (Tipo != 0 || Tipo != null) {
                var url = 'PagarSocio/ObtenerCuentasSocioCombo?id=' + Tipo;
                enviarServidor(url, cdC);
            } else {
                mostrarRespuesta("Error", "elija un proveedor", "error");
            }
            return gbi("txtDireccion");
            break;
        case "txtFormaPago":
            var fechaDocumento = gbi("txtFecha").value;
            var forma = gbi("txtFormaPago").dataset.id;
            var url = 'Compras/fechaVencimiento?fecha=' + fechaDocumento + '&forma=' + forma;
            enviarServidor(url, FechaVencimiento);
            return gbi("txtDescuentoPrincipal");
            break;

        case "txtMoneda":
            return gbi("txtFormaPago");
            break;
        case "txtFecha":
            return gbi("txtObservacion");
            break;
        case "txtNroComprobante":
            return gbi("txtObservacion");
            break;
        case "txtObservacion":
            return gbi("txtRazonSocial");
            break;
        case "txtTipoCompra":
            return gbi("txtTipoDocumento");
            break;
        case "txtDescuentoPrincipal":
            return gbi("txtArticulo");
            break;
        case "txtTipoDocumento":
            return gbi("txtNroSerie");
            break;
        case "txtDireccion":
            return gbi("txtMoneda");
            break;
        default:
            break;
        case "txtOC":
            var idOC = gbi("txtOC").dataset.id;
            var url = 'OrdenCompra/ObtenerDatosxID?id=' + idOC;
            enviarServidor(url, CargarDetalleOC);
            return gbi("");
            break;
    }

    if (txtModal.id == "txtRazonSocial") {
        return gbi("txtDireccion");
    }
}
function cdC(r) {
    lp2 = "";
    if (r.split('↔')[0] == "OK") {
        var listas2 = r.split('↔');
        lp2 = listas2[2].split("▼");
        if (lp2 != "") {
            llenarCombo(lp2, "cboDestino", "Seleccione");
        } else {
            gbi("cboDestino").innerHTML = "";
            mostrarRespuesta("Alerta", "El proveedor no tiene una cuenta bancaria registrada.", "warning");

        }
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
    txtModal.value = value;
    txtModal.dataset.id = id;
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}
function validarFormulario() {
    var error = true;

    if (validarControl("txtTipoCambio")) error = false;
    //if (validarControl("txtObservacion")) error = false;
    if (validarControl("txtDireccion")) error = false;
    if (validarControl("txtTipoDocumento")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (validarControl("txtTipoCompra")) error = false;
    if (validarControl("txtNroSerie")) error = false;
    if (validarControl("txtNroComprobante")) error = false;
    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtNroDocumento")) error = false;
    if (validarControl("txtMoneda")) error = false;
    if (validarControl("txtFormaPago")) error = false;

    if (error && parseInt($("#txtTotalF").val()) == 0 || parseInt($("#txtTotalF").val()) == "NaN") {
        mostrarRespuesta("Info", "Tiene que ingresar al menos un registro al detalle.", "warning");
        error = false;
    }

    return error;
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        //var alm = listas[3].split("▼");
        var dir = listas[2].split("▼");
        var det = listas[3].split("▼");
        var not = listas[4].split("▼");
        var temporal = listas[4].split("▲");
        if (Resultado == 'OK') {
            if (temporal[0] > 0) {
                if (not.length > 1) {
                    var lblNotas = document.getElementById('txtNotas');
                    lblNotas.innerHTML = "Este documento tiene varias Notas de Credito!!!";
                    gbi("txtNotas").style.color = "#FF0000";
                } else {
                    var lblNotas = document.getElementById('txtNotas');
                    lblNotas.innerHTML = "Tiene una Notas de Credito = " + temporal[13];
                    gbi("txtNotas").style.color = "#FF0000";
                }


            } else {
                var lblNotas = document.getElementById('txtNotas');
                lblNotas.innerHTML = "Este documento no tiene Notas de Credito ";
                gbi("txtNotas").style.color = "blue";
            }
            adc(listaTipoCompra, datos[2], "txtTipoCompra", 2);
            adc(listaComprobantes, datos[3], "txtTipoDocumento", 1);
            adt(datos[6], "txtDireccion");
            adc(listaMoneda, datos[8], "txtMoneda", 1);
            adc(listaFormaPago, datos[10], "txtFormaPago", 1);
            adc(listaSocios, datos[4], "txtRazonSocial", 1);

            adt(datos[7], "txtNroDocumento", 1);
            adt(datos[12], "txtNroSerie");
            adt(datos[13], "txtNroComprobante");
            adt(datos[11], "txtFecha");
            adt(datos[0], "txtID");
            adt(datos[34], "txtDescuentoPrincipal");
            adt(datos[16], "txtTipoCambio");
            adt(datos[33], "txtObservacion");
            adt(datos[35], "txtFechaVencimiento");
            if (datos[23] == 'TRUE') {
                gbi("chkIGV").checked = true;
            } else {
                gbi("chkIGV").checked = false;
            }
            if (det[0] != "") {
                if (det.length >= 1) {
                    for (var i = 0; i < det.length; i++) {
                        addItem(1, det[i].split("▲"));
                    }
                }
            }
            //datos[2] -idTipoCompra 
            //listaTipoCompra

            //datos[3] -idTipoDocumento
            //listaTipoCompra

            adt(parseFloat(datos[14]).toFixed(3), "txtSubTotalF");
            adt(parseFloat(datos[17]).toFixed(3), "txtIGVF");
            adt(parseFloat(datos[19]).toFixed(3), "txtTotalF");
            calcularSumaDetalle();
        }
        else {
            mostrarRespuesta(Resultado, mensaje, 'error');
        }
    }
}
function CargarDetalleOC(rpta) {
    gbi("tb_DetalleF").innerHTML = "";
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲'); //lista OC
        var det = listas[3].split("▼");//lista OC Detalle
        if (Resultado == 'OK') {
            adc(listaTipoCompra, datos[2], "txtTipoCompra", 2);
            adc(listaMoneda, datos[14], "txtMoneda", 1);
            adc(listaFormaPago, datos[5], "txtFormaPago", 1);
            adc(listaSocios, datos[10], "txtRazonSocial", 1);
            adt(datos[12], "txtNroDocumento", 1);
            adt(datos[13], "txtDireccion");
            adt(datos[17], "txtTipoCambio");
            adt(datos[15], "txtSubTotalF");
            adt(datos[18], "txtIGVF");
            adt(datos[19], "txtTotalF");
            if (det.length >= 1) {
                if (det[0].trim() != "") {
                    for (var i = 0; i < det.length; i++) {
                        addItemOC(1, det[i].split("▲"));
                    }
                }
            }
        }
    }
}
function FechaVencimiento(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲'); //lista OC
        if (Resultado == 'OK') {
            adt(datos[35], "txtFechaVencimiento");
        }
    }
}
function addItemOC(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[16] : 'Und.') + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[8]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[10]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
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
    calcularSumaDetalle();
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
    if (ind != undefined)
        gbi(ctrl).value = l[ind].split('▲')[c];
    gbi(ctrl).dataset.id = id;
}

//TIPO CAMBIO
function TipoCambio() {
    var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + gbi("txtFecha").value;
    enviarServidor(url, CargarTipoCambio);
}
function CargarTipoCambio(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲');
        if (Resultado == 'OK') {
            adt(datos[4], "txtTipoCambio");
        }
    }
}
