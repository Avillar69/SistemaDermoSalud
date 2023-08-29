var cabeceras = ["id", "Fecha", "Serie", "Número", "Razon Social", "SubTotal", "IGV", "Total", "Estado"];
var listaDatos;
var listaImpresion;
var listaReportePorCliente;
var matriz = [];
var txtModal;
var txtModal2;
var listaLocales;
var listaMoneda;
var listaFormaPago;
var listaComprobantes;
var listaTipoVenta;
var listaSocios;
var listaTipoAfectacion;
/*objetos globales para el detalle pago si es al contado*/
var listaSocioPorSiaca;
var listarCuentasO;
var listarBanco;
/*fin de objetos globales pago detalle*/
var lp2;
var url = "DocumentoVenta/ObtenerDatos";
enviarServidor(url, mostrarLista);
configBM();
reziseTabla();
cfgKP(["txtTipoDocumento", "txtTipoVenta", "txtRazonSocial", "txtDireccion", "txtMoneda", "txtFormaPago", "txtArticulo", "txtCita"], cfgTMKP);//"txtCategoria", solo para los q tienen boton
cfgKP(["txtCantidad", "txtPrecio", "txtTotal", "txtNroDocumento", "txtObservacion", "txtDescuento", "txtNroComprobante", "txtFecha", "txtTipoCambio", "txtDescuentoPrincipal"], cfgTKP);
//configNav();
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFechaCobroCheque").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFecha").datetimepicker({
    format: 'DD-MM-YYYY',
});
$('#collapseOne-2').on('shown.bs.collapse', function () {
    reziseTabla();
})
$(document).ready(function () {
    gbi("txtFecha").onchange = function () {
        var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + gbi("txtFecha").value;
        enviarServidor(url, CargarTipoCambio);
    };
});

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
            listaTipoVenta = listas[5].split("▼");
            listaDatos = listas[6].split("▼");
            var fechaInicio = listas[7];
            var fechaFin = listas[8];
            listarBanco = listas[9].split("▼");
            listarCuentasO = listas[10].split("▼");
            listaTipoAfectacion = listas[11].split("▼");
            listaImpresion = listas[12].split("▼");
            listaReportePorCliente = listas[13].split("▼");

            if (listaTipoAfectacion != "") llenarComboIGV(listaTipoAfectacion, "cboTipoAfectacion");
            //ingresar el banco
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;
            gbi("txtFecha").value = fechaFin;
            var btnTipoDocumento = document.getElementById("btnModalTipoDocumento");
            btnTipoDocumento.onclick = function () {
                cbm("tipodocumento", "Tipo de Documento", "txtTipoDocumento", null,
                    ["idTipoComprobante", "Descripción"], listaComprobantes, cargarSinXR);
            }
            var btnTipoVenta = document.getElementById("btnModalTipoVenta");
            btnTipoVenta.onclick = function () {
                cbm("tipoventa", "Tipo de Venta", "txtTipoVenta", null,
                    ["id", "Código", "Descripción"], listaTipoVenta, cargarSinXR);
            }

        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        llenarCombo(listarBanco, "cboBanco", "Seleccione");
        llenarCombo(listarCuentasO, "cboOrigen", "Seleccione");

        listar();
    }
    reziseTabla();
}
function listar() {
    configurarFiltro();
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    //configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
    gbi("chkIGV").onchange = function () {
        switch (gbi("txtTipoVenta").dataset.id) {
            case "1": calcularSumaDetalle(); break;
            case "2": calcularSumaDetalleServicio(); break;
            case "3": calcularSumaDetalleOtroServicio(); break;
        }
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nuevo Documento de Venta";
            gbi("txtTipoVenta").focus();
            gbi("cboNroSerie").style.display = "";
            gbi("txtNroSerie").style.display = "none";
            gbi("divArticulos").style.display = "";
            gbi("btnGrabar").style.display = "";
            gbi("tb_DetalleF").innerHTML = "";
            gbi("tb_DetalleServicio").innerHTML = "";
            var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + gbi("txtFecha").value;
            enviarServidor(url, CargarTipoCambio);
            gbi("txtTipoCambio").readOnly = false;
            gbi("txtDescuentoPrincipal").readOnly = false;
            gbi("txtFecha").readOnly = false;
            gbi("cboTipoAfectacion").disabled = false;
            gbi("txtObservacion").readOnly = false;
            adc(listaMoneda, 1, "txtMoneda", 1);
            adc(listaFormaPago, 2, "txtFormaPago", 1);
            gbi("btnEnviarSunat").style.display = "none";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Documento de Venta";//Titulo Modificar
            TraerDetalle(id);
            BloquearDetalle();
            show_hidden_Formulario(true);
            gbi("txtTipoCambio").readOnly = true;
            gbi("txtDescuentoPrincipal").readOnly = true;
            gbi("cboTipoAfectacion").disabled = true;
            gbi("btnEnviarSunat").style.display = "";
            break;
    }
}
function TraerDetalle(id) {
    var url = 'DocumentoVenta/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function BloquearDetalle() {
    gbi("txtFecha").readOnly = true;
    gbi("txtObservacion").readOnly = true;
    gbi("divOtroServicio").style.display = "none";
    gbi("divArticulos").style.display = "none";
    gbi("btnGrabar").style.display = "none";
    
}
function eliminar(id) {
    swal({
        title: 'Desea Eliminar el Documento de  Venta? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                IdEliminar = "";
                IdEliminar = id;
                var u = "PagarSocio/BuscarPago?id=" + id + "&idEmpresa=" + 2;
                enviarServidor(u, BuscarPago);
            } else {
                swal('Cancelado', 'No se eliminó el Documento Venta', 'error');
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
                title: 'Este Documento de  Venta tiene Cobros Realizados',
                text: 'No podrá recuperar los datos eliminados.',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Si, Eliminalo!',
                closeOnConfirm: false,
                closeOnCancel: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        var u = "DocumentoVenta/Eliminar?idDocumentoVenta=" + datos[25];
                        enviarServidor(u, eliminarListar);

                    } else {
                        mostrarRespuesta('Cancelado', 'Elimine los pagos realizados manualmenteeee', 'error'); z
                    }
                });
        } else {
            var u = "DocumentoVenta/Eliminar?idDocumentoVenta=" + IdEliminar;
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
            mensaje = 'Se eliminó el Documento de venta';
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
function anular(id) {
    swal({
        title: 'Desea Anular el Documento de  Venta? ',
        text: 'No podrá recuperar los datos anulados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Anular!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var u = "DocumentoVenta/Anular?idDocumentoVenta=" + id;
                enviarServidor(u, anularListar);
            } else {
                swal('Cancelado', 'No se Anuló el Documento Venta', 'error');
            }
        });
}
function anularListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se Anuló el Documento de venta';
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
//Configurar botones de Modal
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
    }
    var btnSocio = document.getElementById("btnModalRazonSocial");
    btnSocio.onclick = function () {
        if (validarControl("txtTipoDocumento"))
            return;
        bDM("txtDireccion");
        cbmu("socio", "Clientes", "txtRazonSocial", "txtNroDocumento",
            ["idSocioNegocio", "Documento", "Razón Social"], `/SocioNegocio/ObtenerSocioxTipo?tipo=C&idTipoComprobante=${gbi("txtTipoDocumento").dataset.id}`, cargarLista);
    }
    var btnMoneda = document.getElementById("btnModalMoneda");
    btnMoneda.onclick = function () {
        cbmu("moneda", "Moneda", "txtMoneda", null,
            ["idMoneda", "Código", "Descripción"], "/Moneda/ObtenerDatos?Activo=A", cargarLista);
    }
    var btnModalCita = gbi("btnModalCita");
    btnModalCita.onclick = function () {
        cbmu("cita", "Cita", "txtCita", null,
            ["idCita", "Fecha", "NroCita", "Paciente", "Pago", "idPaciente"], "/Citas/ListarCitasEnVenta", cargarLista);
    }
    var btnModalDireccion = gbi("btnModalDireccion");
    btnModalDireccion.onclick = function () {
        var Prov = document.getElementById("txtRazonSocial").getAttribute('data-id');
        if (Prov) {
            cbmu("direccion", "Dirección", "txtDireccion", null,
                ["idDireccion", "Descripción"], "/SocioNegocio/ObtenerDireccionxID?id=" + Prov, cargarLista);
        }
        else {
            mostrarRespuesta("Error", "Debe Seleccionar un Cliente", "error");
        }
    }
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Doc. Ventas", cabeceras, matriz, "Documento Ventas", "a4", "e");
    }
    //var btnImprimir = document.getElementById("btnImprimir");
    //btnImprimir.onclick = function () {
    //    ExportarPDFs("p", "Doc. Ventas", cabeceras, matriz, "Documento Ventas", "a4", "i");
    //}
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            if (gvc("txtFormaPago") == "2") {
                limpiarModalPago();
                AbrirModal("modalPago");
            }
            else {
                if (parseInt($("#txtTotalF").text()) == 0 || parseInt($("#txtTotalF").text()) == "NaN") {
                    mostrarRespuesta("Error", "tiene que ingresar un detalle", "error");
                } else {
                    if (gbi("txtDescuentoPrincipal").value >= 100) {
                        mostrarRespuesta("Alerta", "El porcentaje no puede ser mayor al 100%", "error");
                    } else {
                        var url = "DocumentoVenta/Grabar";
                        var frm = new FormData();
                        frm.append("idDocumentoVenta", gvt("txtID"));
                        frm.append("idTipoVenta", gvc("txtTipoVenta"));
                        frm.append("idTipoDocumento", gvc("txtTipoDocumento"));
                        frm.append("idCliente", gvc("txtRazonSocial"));
                        frm.append("ClienteRazon", gvt("txtRazonSocial"));
                        frm.append("ClienteDireccion", gvt("txtDireccion"));
                        frm.append("ClienteDocumento", gvt("txtNroDocumento"));
                        frm.append("idMoneda", gvc("txtMoneda"));
                        frm.append("idPedido", gvc("txtCita"));
                        frm.append("idFormaPago", gvc("txtFormaPago"));
                        frm.append("EstadoDoc", "P");
                        frm.append("FechaDocumento", gvt("txtFecha"));
                        frm.append("SerieDocumento", gbi("cboNroSerie").value);
                        frm.append("NumDocumento", gvt("txtNroComprobante"));
                        switch (gbi("txtTipoVenta").dataset.id) {
                            case "1": frm.append("cadDetalle", crearCadDetalle()); break;
                            case "2": frm.append("cadDetalle", crearCadDetalleServicio()); break;
                            case "3": frm.append("cadDetalle", crearCadDetalleOtroServicio()); break;
                        }
                        frm.append("TipoCambio", gvt("txtTipoCambio"));
                        frm.append("Estado", true);
                        frm.append("ObservacionVenta", gvt("txtObservacion"));
                        frm.append("NroCita", gbi("txtCita").value.length == 0 ? "0" : gbi("txtCita").value);
                        var porId = document.getElementById("chkIGV").checked;
                        frm.append("flgIGV", porId);
                        frm.append("idTipoAfectacion", gbi("cboTipoAfectacion").value);
                        frm.append("SubTotalNacional", gvt("txtGravada")); //gvt("txtSubTotalF"));
                        frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
                        frm.append("TotalExonerado", gvt("txtExonerado"));
                        frm.append("TotalInafecto", gvt("txtInafecto"));
                        frm.append("TotalGravado", gvt("txtGravada"));
                        frm.append("IGVNacional", gvt("txtIGVF"));
                        frm.append("TotalGratuito", gvt("txtGratuita"));
                        frm.append("TotalOtrosCargos", gvt("txtOtrosCargosF"));
                        frm.append("TotalDescuentoNacional", gvt("txtDescuento"));
                        frm.append("TotalDescuentoExtranjero", gvt("txtDescuento"));
                        frm.append("TotalNacional", gvt("txtTotalF"));
                        frm.append("TipoPago", gbi("cboTipoPago").selectedOptions[0].text.length == 0 ? " " : gbi("cboTipoPago").selectedOptions[0].text);
                        frm.append("Tarjeta", gbi("cboTarjeta").selectedOptions[0].text.length == 0 ? " " : gbi("cboTarjeta").selectedOptions[0].text);
                        frm.append("NroOperacion", gbi("txtNOperacion").value.length == 0 ? " " : gbi("txtNOperacion").value);
                        frm.append("FormaPago", gbi("txtFormaPago").value);
                        swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
                        enviarServidorPost(url, actualizarListar, frm);
                    }
                }
            }
        }
    };
    var btnGrabarPago = document.getElementById("btnGrabarPago");
    btnGrabarPago.onclick = function () {
        var nroDocumentoCliente = gvt("txtNroDocumento").replace(/\s+/, "");
        if (validarFormulario()) {
            if (parseInt($("#txtTotalF").text()) == 0 || parseInt($("#txtTotalF").text()) == "NaN") {
                mostrarRespuesta("Error", "tiene que ingresar un detalle", "error");
            } else {
                if (gbi("txtDescuentoPrincipal").value >= 100) {
                    mostrarRespuesta("Alerta", "El porcentaje no puede ser mayor al 100%", "error");
                } else if (parseInt(gbi("txtTotalF").value) < 0) {
                    mostrarRespuesta("Info", "El total no puede ser negativo", "info");
                } else {
                    CerrarModal("modalPago");
                    let esValido = true;
                    switch (gbi("txtTipoDocumento").dataset.id) {
                        case "1":
                            if (nroDocumentoCliente.length < 11 || nroDocumentoCliente.length > 11) {
                                esValido = false;
                            }
                            break;
                        case "2":
                            if (nroDocumentoCliente.length < 8 || nroDocumentoCliente.length > 8) {
                                esValido = false;
                            }
                            break;
                    }
                    if (esValido) {
                        swal({
                            title: "Confirmar",
                            text: "Está seguro de emitir el comprobante?\nNo podrá cambiar los datos registrados.",
                            type: "info",
                            showCancelButton: true,
                            confirmButtonText: 'Si Emitir',
                            closeOnConfirm: false,
                            closeOnCancel: true,
                            cancelButtonText: 'Cancelar'
                        }, function (isConfirm) {
                            if (isConfirm) {
                                var url = "DocumentoVenta/Grabar";
                                var frm = new FormData();
                                frm.append("idDocumentoVenta", gvt("txtID"));
                                frm.append("idTipoVenta", gvc("txtTipoVenta"));
                                frm.append("idTipoDocumento", gvc("txtTipoDocumento"));
                                frm.append("idCliente", gvc("txtRazonSocial"));
                                frm.append("ClienteRazon", gvt("txtRazonSocial"));
                                frm.append("ClienteDireccion", gvt("txtDireccion"));
                                frm.append("ClienteDocumento", gvt("txtNroDocumento").replace(/\s+/, ""));
                                frm.append("idMoneda", gvc("txtMoneda"));
                                frm.append("idPedido", gvc("txtCita"));
                                frm.append("idFormaPago", gvc("txtFormaPago"));
                                frm.append("EstadoDoc", "P");
                                frm.append("FechaDocumento", gvt("txtFecha"));
                                frm.append("SerieDocumento", gbi("cboNroSerie").value);
                                frm.append("NumDocumento", gvt("txtNroComprobante"));
                                switch (gbi("txtTipoVenta").dataset.id) {
                                    case "1": frm.append("cadDetalle", crearCadDetalle()); break;
                                    case "2": frm.append("cadDetalle", crearCadDetalleServicio()); break;
                                    case "3": frm.append("cadDetalle", crearCadDetalleOtroServicio()); break;
                                }
                                frm.append("TipoCambio", gvt("txtTipoCambio"));
                                frm.append("Estado", true);
                                frm.append("ObservacionVenta", gvt("txtObservacion"));
                                frm.append("NroCita", gbi("txtCita").value.length == 0 ? "0" : gbi("txtCita").value);
                                var porId = document.getElementById("chkIGV").checked;
                                frm.append("flgIGV", porId);
                                frm.append("idTipoAfectacion", gbi("cboTipoAfectacion").value);
                                frm.append("SubTotalNacional", gvt("txtGravada")); //gvt("txtSubTotalF"));
                                frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
                                frm.append("TotalExonerado", gvt("txtExonerado"));
                                frm.append("TotalInafecto", gvt("txtInafecto"));
                                frm.append("TotalGravado", gvt("txtGravada"));
                                frm.append("IGVNacional", gvt("txtIGVF"));
                                frm.append("TotalGratuito", gvt("txtGratuita"));
                                frm.append("TotalOtrosCargos", gvt("txtOtrosCargosF"));
                                frm.append("TotalDescuentoNacional", gvt("txtDescuento"));
                                frm.append("TotalDescuentoExtranjero", gvt("txtDescuento"));
                                frm.append("TotalNacional", gvt("txtTotalF"));
                                frm.append("TipoPago", gbi("cboTipoPago").selectedOptions[0].text);
                                frm.append("FormaPago", gbi("txtFormaPago").value);
                                switch (gbi("cboTipoPago").selectedOptions[0].text) {
                                    case "EFECTIVO": frm.append("Tarjeta", " ");
                                        frm.append("NroOperacion", " ");
                                        frm.append("ObservacionCaja", " ");
                                        break;
                                    case "TARJETA":
                                        frm.append("Tarjeta", gbi("cboTarjeta").selectedOptions[0].text.length == 0 ? " " : gbi("cboTarjeta").selectedOptions[0].text);
                                        frm.append("NroOperacion", gbi("txtNOperacion").value.length == 0 ? " " : gbi("txtNOperacion").value);
                                        frm.append("ObservacionCaja", " ");
                                        break;
                                    case "EFECTIVO Y TARJETA":
                                        frm.append("Tarjeta", gbi("cboTarjeta").selectedOptions[0].text.length == 0 ? " " : gbi("cboTarjeta").selectedOptions[0].text);
                                        frm.append("NroOperacion", gbi("txtNOperacion").value.length == 0 ? " " : gbi("txtNOperacion").value);
                                        frm.append("ObservacionCaja", " ");

                                }
                                swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
                                enviarServidorPost(url, actualizarListar, frm);
                            }
                        });
                    } else {
                        mostrarRespuesta("Error", "NO SE PUEDE REALIZAR UNA " + gbi("txtTipoDocumento").value + " DEBIDO AL N° DOCUMENTO DEL CLIENTE", "error");
                    }
                }
            }
        }
    };

    /*************************************************************************************/
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }
    var btnCancelarArticulo = document.getElementById("btnCancelarArticulo");
    btnCancelarArticulo.onclick = function () { cancel_AddArticulo(); $("#btnAgregarArticulo").show(); }

    let btnAgregarArticulo = gbi("btnAgregarArticulo");
    btnAgregarArticulo.onclick = function () {
        if (validarAgregarDetalle()) {
            addItem(0, []);
            limpiarCamposDetalle();
            calcularSumaDetalle();
        }
    }
    var btnAgregarOtroServicio = gbi("btnAgregarOtroServicio");
    btnAgregarOtroServicio.onclick = function () {
        if (validarAgregarDetalleOtroServicio()) {
            addItemOtroServicio(0, []);
            limpiarCamposDetalleOtroServicio();
            calcularSumaDetalleOtroServicio();
        }
    }

    var btnEnviarSunat = document.getElementById("btnEnviarSunat");
    btnEnviarSunat.onclick = function () {
        var nroDocumentoCliente = gvt("txtNroDocumento").replace(/\s+/, "");
        if (validarFormulario_Sunat()) {
            if (parseInt($("#txtTotalF").text()) == 0 || parseInt($("#txtTotalF").text()) == "NaN") {
                mostrarRespuesta("Error", "tiene que ingresar un detalle", "error");
            } else {
                if (gbi("txtDescuentoPrincipal").value >= 100) {
                    mostrarRespuesta("Alerta", "El porcentaje no puede ser mayor al 100%", "error");
                } else if (parseInt(gbi("txtTotalF").value) < 0) {
                    mostrarRespuesta("Info", "El total no puede ser negativo", "info");
                } else {
                    let esValido = true;
                    switch (gbi("txtTipoDocumento").dataset.id) {
                        case "1":
                            if (nroDocumentoCliente.length < 11 || nroDocumentoCliente.length > 11) {
                                esValido = false;
                            }
                            break;
                        case "2":
                            if (nroDocumentoCliente.length < 8 || nroDocumentoCliente.length > 8) {
                                esValido = false;
                            }
                            break;
                    }
                    if (esValido) {
                        swal({
                            title: "Confirmar",
                            text: "Está seguro de emitir el comprobante?\nNo podrá cambiar los datos registrados.",
                            type: "info",
                            showCancelButton: true,
                            confirmButtonText: 'Si Emitir',
                            closeOnConfirm: false,
                            closeOnCancel: true,
                            cancelButtonText: 'Cancelar'
                        }, function (isConfirm) {
                            if (isConfirm) {
                                var url = "DocumentoVenta/EnviarSunat";
                                var frm = new FormData();
                                frm.append("idDocumentoVenta", gvt("txtID"));
                                frm.append("idTipoVenta", gvc("txtTipoVenta"));
                                frm.append("idTipoDocumento", gvc("txtTipoDocumento"));
                                frm.append("idCliente", gvc("txtRazonSocial"));
                                frm.append("ClienteRazon", gvt("txtRazonSocial"));
                                frm.append("ClienteDireccion", gvt("txtDireccion"));
                                frm.append("ClienteDocumento", gvt("txtNroDocumento").replace(/\s+/, ""));
                                frm.append("idMoneda", gvc("txtMoneda"));
                                frm.append("idPedido", gvc("txtCita"));
                                frm.append("idFormaPago", gvc("txtFormaPago"));
                                frm.append("EstadoDoc", "P");
                                frm.append("FechaDocumento", gvt("txtFecha"));
                                frm.append("SerieDocumento", gbi("txtNroSerie").value);
                                frm.append("NumDocumento", gvt("txtNroComprobante"));
                                switch (gbi("txtTipoVenta").dataset.id) {
                                    case "1": frm.append("cadDetalle", crearCadDetalle()); break;
                                    case "2": frm.append("cadDetalle", crearCadDetalleServicio()); break;
                                    case "3": frm.append("cadDetalle", crearCadDetalleOtroServicio()); break;
                                }
                                frm.append("TipoCambio", gvt("txtTipoCambio"));
                                frm.append("Estado", true);
                                frm.append("ObservacionVenta", gvt("txtObservacion"));
                                frm.append("NroCita", gbi("txtCita").value.length == 0 ? "0" : gbi("txtCita").value);
                                var porId = document.getElementById("chkIGV").checked;
                                frm.append("flgIGV", porId);
                                frm.append("idTipoAfectacion", gbi("cboTipoAfectacion").value);
                                frm.append("SubTotalNacional", gvt("txtGravada")); //gvt("txtSubTotalF"));
                                frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
                                frm.append("TotalExonerado", gvt("txtExonerado"));
                                frm.append("TotalInafecto", gvt("txtInafecto"));
                                frm.append("TotalGravado", gvt("txtGravada"));
                                frm.append("IGVNacional", gvt("txtIGVF"));
                                frm.append("TotalGratuito", gvt("txtGratuita"));
                                frm.append("TotalOtrosCargos", gvt("txtOtrosCargosF"));
                                frm.append("TotalDescuentoNacional", gvt("txtDescuento"));
                                frm.append("TotalDescuentoExtranjero", gvt("txtDescuento"));
                                frm.append("TotalNacional", gvt("txtTotalF"));
                                frm.append("TipoPago", gbi("cboTipoPago").selectedOptions[0].text);
                                frm.append("FormaPago", gbi("txtFormaPago").value);
                                switch (gbi("cboTipoPago").selectedOptions[0].text) {
                                    case "EFECTIVO": frm.append("Tarjeta", " ");
                                        frm.append("NroOperacion", " ");
                                        frm.append("ObservacionCaja", " ");
                                        break;
                                    case "TARJETA":
                                        frm.append("Tarjeta", gbi("cboTarjeta").selectedOptions[0].text.length == 0 ? " " : gbi("cboTarjeta").selectedOptions[0].text);
                                        frm.append("NroOperacion", gbi("txtNOperacion").value.length == 0 ? " " : gbi("txtNOperacion").value);
                                        frm.append("ObservacionCaja", " ");
                                        break;
                                    case "EFECTIVO Y TARJETA":
                                        frm.append("Tarjeta", gbi("cboTarjeta").selectedOptions[0].text.length == 0 ? " " : gbi("cboTarjeta").selectedOptions[0].text);
                                        frm.append("NroOperacion", gbi("txtNOperacion").value.length == 0 ? " " : gbi("txtNOperacion").value);
                                        frm.append("ObservacionCaja", " ");

                                }
                                swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
                                enviarServidorPost(url, actualizarListar, frm);
                            }
                        });
                    } else {
                        mostrarRespuesta("Error", "NO SE PUEDE REALIZAR UNA " + gbi("txtTipoDocumento").value + " DEBIDO AL N° DOCUMENTO DEL CLIENTE", "error");
                    }
                }
            }
        }
    };
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
function cdC(r) {
    lp2 = "";
    if (r.split('↔')[0] == "OK") {
        var listas2 = r.split('↔');
        lp2 = listas2[2].split("▼");
        if (lp2 != "") {
            llenarCombo(lp2, "cboDestino", "Seleccione");
        } else {
            //gbi("cboDestino").innerHTML = "";
            //mostrarRespuesta("Error", "Debe Registrar una cuenta bancaria", "error");
            gbi("txtRazonSocial").onfocus();
        }
    }
}
function cancel_AddArticulo() {
    limpiarControl("txtArticulo");
    gbi("txtTotal").value = "0.00";
    txtPrecio.value = "0.00";
    gbi("txtCantidad").value = "0.00";
    gbi("btnActualizarArticulo").style.display = "none";
    gbi("btnCancelarArticulo").style.display = "none";
}
function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[3].dataset.id;
    gbi("txtArticulo").value = row.children[3].innerHTML;
    //gbi("txtCategoria").dataset.id = row.children[2].dataset.id;
    //gbi("txtCategoria").value = row.children[2].innerHTML;
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
    //row.children[2].dataset.id = gbi("txtCategoria").dataset.id;
    //row.children[2].innerHTML = gbi("txtCategoria").value;
    row.children[4].innerHTML = gbi("txtCantidad").value;
    row.children[6].innerHTML = gbi("txtPrecio").value;
    row.children[7].innerHTML = gbi("txtTotal").value;
    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
    calcularSumaDetalle();
    cancel_AddArticulo();

}
function eliminarDetalle(id) {

    limpiarControl("txtCantidad");
    //bDM("txtCategoria");
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
//Agregar Item a Tabla Detalle
function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" style="display:none;" data-id="' + (tipo == 1 ? data[6] : "0") + '">' + (tipo == 1 ? data[7] : "0") + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5" style="display:none;">-</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[8]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[10]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
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
function validarAgregarDetalle() {
    var error = true;
    var TotalDetalleAdd = parseFloat(gbi("txtTotal").value);
    if (TotalDetalleAdd <= 0) {
        error = false;
        mostrarRespuesta('Error', 'Detalle no puede tener monto 0', 'error');
    }
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
        cdet += "|" + $(".rowDet")[obj].children[2].innerHTML;
        cdet += "|" + $(".rowDet")[obj].children[3].dataset.id;//idArticulo
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;//DescripcionArticulo
        cdet += "|" + $(".rowDet")[obj].children[4].innerHTML;//Cantidad
        cdet += "|0"; //+ $(".rowDet")[obj].children[2].dataset.id;//idCategoria
        cdet += "|0"; //+ $(".rowDet")[obj].children[2].innerHTML;//descripcionCategoria
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//PrecioNacional
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//PrecioExtranjero
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//SubTotalNacional
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//SubTotalExtranjero
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
    //0|0|0|0|ASPIRINA|0|0|-|-|3.000|3.000|0|01-01-2000|01-01-2000|1|1|true
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
        var idTipoAfect = $(".rowDet")[obj].children[9].innerHTML;
        var Tipo = "";
        for (var i = 0; i < listaTipoAfectacion.length; i++) {
            if (listaTipoAfectacion[i].split("▲")[0] == idTipoAfect) {
                Tipo = (listaTipoAfectacion[i].split("▲")[1]).split("-")[0];
                break;
            }
        }
        if (Tipo.toUpperCase().indexOf("GRATUITA") != -1) {
            sumGratuita += parseFloat($(".rowDet")[obj].children[7].innerHTML);
        }
        else if (Tipo.toUpperCase() == "EXPORTACIÓN") {
            sumExportacion += parseFloat($(".rowDet")[obj].children[7].innerHTML);
        } else {
            sum += parseFloat($(".rowDet")[obj].children[7].innerHTML);
        }
    });
    if (gbi("chkIGV").checked) {
        gbi("txtGravada").value = (parseFloat((parseFloat(sum * 100 / 118)).toFixed(3))).toFixed(2);
        gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(3))).toFixed(2);
        gbi("txtIGVF").value = (parseFloat(((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3))).toFixed(2);
        gbi("txtTotalF").value = (parseFloat((parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value)).toFixed(3))).toFixed(1))).toFixed(2);
    }
    else {
        gbi("txtGravada").value = (parseFloat((parseFloat(sum)).toFixed(3))).toFixed(2);
        gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(1))).toFixed(2);
        gbi("txtIGVF").value = (parseFloat((((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100)).toFixed(1))).toFixed(2);
        gbi("txtTotalF").value = (parseFloat((parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - parseFloat(gbi("txtDescuento").value))).toFixed(3))).toFixed(1))).toFixed(2);
    }
    //gratuita
    gbi("txtGratuita").value = (parseFloat((parseFloat(sumGratuita)).toFixed(3))).toFixed(2);
    if (sumExportacion > 0) {
        var sumaTotal = 0;
        var descuento = 0;
        var otrosCargos = parseFloat(gbi("txtOtrosCargosF").value);
        descuento = (parseFloat((parseFloat(sumExportacion * gbi("txtDescuentoPrincipal").value / 100), 1).toFixed(3))).toFixed(2);
        sumaTotal = sumExportacion + otrosCargos - descuento;

        gbi("txtDescuento").value = (parseFloat((descuento, 1).toFixed(3))).toFixed(2);
        gbi("txtExportacion").value = (parseFloat((parseFloat(sumExportacion - descuento), 1).toFixed(3))).toFixed(2);
        gbi("txtTotalF").value = (parseFloat((parseFloat(sumaTotal), 1).toFixed(3))).toFixed(2);
    } else {
        gbi("txtExportacion").value = (0).toFixed(2);
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
                mensaje = 'Se adicionó el Documento de Venta';
                listaImpresion = data[3].split("▼");
                listaReportePorCliente = data[4].split("▼");
                tipo = 'success';
            }
            else {
                mensaje = data[1];
                tipo = 'error';
            }
        }
        else {
            if (res == 'OK') {
                mensaje = 'Se actualizó el Documento de Venta';
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
function validarFormulario() {
    var error = true;
    //Aqui Ingresar todos los campos a validar || validarCombo || validarTexto
    if (validarControl("txtTipoCambio")) error = false;
    if (validarControl("txtTipoDocumento")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (validarControl("txtTipoVenta")) error = false;
    if (validarControl("cboNroSerie")) error = false;
    if (validarControl("txtNroComprobante")) error = false;
    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtNroDocumento")) error = false;
    if (validarControl("txtMoneda")) error = false;
    if (validarControl("txtFormaPago")) error = false;

    if (error && gbi("tb_DetalleF").children.length === 0 && error && gbi("tb_DetalleServicio").children.length === 0) {
        mostrarRespuesta("Info", "Tiene que ingresar al menos un registro al detalle.", "warning");
        error = false;
    }

    return error;
}
function validarFormulario_Sunat() {
    var error = true;
    //Aqui Ingresar todos los campos a validar || validarCombo || validarTexto
    if (validarControl("txtTipoCambio")) error = false;
    if (validarControl("txtTipoDocumento")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (validarControl("txtTipoVenta")) error = false;
    if (validarControl("txtNroSerie")) error = false;
    if (validarControl("txtNroComprobante")) error = false;
    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtNroDocumento")) error = false;
    if (validarControl("txtMoneda")) error = false;
    if (validarControl("txtFormaPago")) error = false;

    if (error && gbi("tb_DetalleF").children.length === 0 && error && gbi("tb_DetalleServicio").children.length === 0) {
        mostrarRespuesta("Info", "Tiene que ingresar al menos un registro al detalle.", "warning");
        error = false;
    }

    return error;
}
function CargarDetalles(rpta) {
    gbi("tb_DetalleF").innerHTML = "";
    gbi("tb_DetalleServicio").innerHTML = "";
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        var det = listas[3].split("▼");
        if (Resultado == 'OK') {
            adc(listaTipoVenta, datos[2], "txtTipoVenta", 2);
            adc(listaComprobantes, datos[3], "txtTipoDocumento", 1);
            adt(datos[6], "txtDireccion");
            adc(listaMoneda, datos[9], "txtMoneda", 1);
            adc(listaFormaPago, datos[11], "txtFormaPago", 1);
            gbi("chkIGV").checked = datos[23] == "TRUE" ? true : false;            
            adt(datos[5], "txtRazonSocial");
            adt(datos[7], "txtNroDocumento");
            gbi("txtNroSerie").value = datos[13];
            gbi("cboNroSerie").style.display = "none";
            gbi("txtNroSerie").style.display = "";
            adt(datos[14], "txtNroComprobante");
            adt(datos[12], "txtFecha");
            adt(datos[0], "txtID");
            adt(datos[32], "txtObservacion");
            adt(datos[17], "txtTipoCambio");
            adt(datos[53], "txtCita");
            gbi("txtCita").dataset.id = datos[10];
            HabilitarDiv();
            switch (datos[2]) {
                case "1": if (det[0] != "") {
                    if (det.length >= 1) {
                        for (var i = 0; i < det.length; i++) {
                            addItem(1, det[i].split("▲"));
                        }
                    }
                } break;
                case "2": if (det[0] != "") {
                    if (det.length >= 1) {
                        gbi("tb_DetalleServicio").innerHTML = "";
                        for (var i = 0; i < det.length; i++) {
                            addItemServicio(2, det[i].split("▲"));
                        }
                        calcularSumaDetalleServicio();
                    }
                } break;
                case "3": if (det[0] != "") {
                    if (det.length >= 1) {
                        gbi("tb_DetalleServicio").innerHTML = "";
                        for (var i = 0; i < det.length; i++) {
                            addItemOtroServicio(1, det[i].split("▲"));
                        }
                        calcularSumaDetalleOtroServicio();
                    }
                } break;
            }
            if (datos[2] == "1") { calcularSumaDetalle(); } else { gbi("txtIGVF").value = parseFloat(0).toFixed(3); }
            gbi("divArticulos").style.display = "none";
            gbi("divOtroServicio").style.display = "none";
            adt(datos[18], "txtIGVF");
            adt(datos[20], "txtTotalF");
            adt(datos[15], "txtGravada");
            adt(datos[35], "txtDescuentoPrincipal");
            adt(datos[42], "txtDescuento");
            adt(datos[63], "txtCita");

            gbi("tb_DetalleF").querySelectorAll("button").forEach(item => { item.disabled = true; });
            gbi("tb_DetalleServicio").querySelectorAll("button").forEach(item => { item.disabled = true; });
        }
        else {
            mostrarRespuesta(Resultado, mensaje, 'error');
        }
    }
}
//MODAL
function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0: case 5:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            case 3:
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
            switch (cabeceras.length) {
                case 2:
                    tipoColDes = 12;
                    tipoColum = 12;
                    break;
                case 3:
                    tipocol = 2;
                    tipoColDes = 8;
                    tipoColum = 8
                    break;

                case 4:
                    tipocol = 3
                    tipoColDes = 3
                    tipoColum = 3
                    break
                case 5:
                    tipocol = 2
                    tipoColDes = 3;
                    tipoColum = 4;
                    break;
                case 6:
                    tipocol = 2
                    tipoColDes = 3;
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
                            case 0: case 5:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += ("<div class='col-12 col-md-" + (cabeceras.length == 2 ? tipoColDes : tipocol) + "'>");
                                break;
                            case 2:
                                contenido2 += ("<div class='col-12 col-md-" + tipoColDes) + "'>";
                                break;
                            case 3:
                                contenido2 += ("<div class='col-12 col-md-" + tipoColum) + "'>";
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
//detalle cita venta
function CargarDetalleCitaVenta(rpta) {
    var listaDatas = rpta.split("↔");
    var listaServicios = listaDatas[3].split("▼");
    var dataServicio = listaServicios[0].split("▲");
    var listaPaciente = listaDatas[4].split("▼");
    var detalleCita = listaDatas[5].split("▼");
    gbi("txtRazonSocial").value = listaPaciente[0].split("▲")[1] + ' ' + listaPaciente[0].split("▲")[2] + ' ' + listaPaciente[0].split("▲")[3];
    gbi("txtNroDocumento").value = listaPaciente[0].split("▲")[4];
    gbi("txtDireccion").value = listaPaciente[0].split("▲")[5];

    if (detalleCita[0] != "") {
        if (detalleCita.length >= 1) {
            for (var i = 0; i < detalleCita.length; i++) {
                addItemServicio(1, detalleCita[i].split("▲"));
            }
        }
    }

    //addItemServicio(1, dataServicio);
    calcularSumaDetalleServicio();
}
function addItemServicio(tipo, data) {

    var contenido = "";
    contenido += '<div class="row rowDetServicio" id="gd' + (tipo == 1 ? data[0] : data[0]) + '"style="margin: auto; margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[1] : data[1]) + '</div>';
    contenido += '  <div class="col-md-1">' + (tipo == 1 ? document.getElementsByClassName("rowDetServicio").length + 1 : document.getElementsByClassName("rowDetServicio").length + 1) + '</div>';
    contenido += '  <div class="col-md-9 p-t-5" data-id="' + (tipo == 1 ? data[3] : data[3]) + '">' + (tipo == 1 ? data[3] : data[3]) + '</div>';
    contenido += '  <div class="col-md-2 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(3) : parseFloat(data[10]).toFixed(3)) + '</div>';
    contenido += '  <div class="col-md-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleServicio").innerHTML += contenido;


    //gbi("chkIGV").checked = true;
    //if (tipo == 1) {
    //    gbi("txtRazonSocial").value = data[0];
    //    gbi("txtRazonSocial").dataset.id = data[5];
    //    gbi("txtNroDocumento").value = data[1];
    //    gbi("txtDireccion").value = data[2];
    //    gbi("txtSubTotalF").value = parseFloat(data[4] * 100 / 118).toFixed(3);
    //    gbi("txtDescuento").value = 0;
    //    gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
    //    gbi("txtTotalF").value = parseFloat(data[4]).toFixed(3);
    //} else {
    //    gbi("txtSubTotalF").value = parseFloat(data[10]).toFixed(3);
    //    gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
    //    gbi("txtTotalF").value = parseFloat(data[10]).toFixed(3);
    //}

}
function calcularSumaDetalleServicio() {
    var sumExonerado = 0;
    var sumInafecta = 0;
    var sumGravada = 0;
    var SumGratuita = 0;
    var SumOtrosCargos = 0;
    var DescuentoTotal = 0;

    var sum = 0;
    var sumGratuita = 0;
    var sumExportacion = 0;

    $(".rowDetServicio").each(function (obj) {
        var idTipoAfect = $(".rowDetServicio")[obj].children[3].innerHTML;
        var Tipo = "";
        for (var i = 0; i < listaTipoAfectacion.length; i++) {
            if (listaTipoAfectacion[i].split("▲")[0] == idTipoAfect) {
                Tipo = (listaTipoAfectacion[i].split("▲")[1]).split("-")[0];
                break;
            }
        }
        if (Tipo.toUpperCase().indexOf("GRATUITA") != -1) {
            sumGratuita += parseFloat($(".rowDetServicio")[obj].children[3].innerHTML);
        }
        else if (Tipo.toUpperCase() == "EXPORTACIÓN") {
            sumExportacion += parseFloat($(".rowDetServicio")[obj].children[3].innerHTML);
        } else {
            sum += parseFloat($(".rowDetServicio")[obj].children[3].innerHTML);
        }
    });
    if (gbi("chkIGV").checked) {
        gbi("txtGravada").value = (parseFloat((parseFloat(sum * 100 / 118)).toFixed(3))).toFixed(2);
        gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(3))).toFixed(2);
        gbi("txtIGVF").value = (parseFloat(((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3))).toFixed(2);
        gbi("txtTotalF").value = (parseFloat((parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value)).toFixed(3))).toFixed(1))).toFixed(2);
    }
    else {
        gbi("txtGravada").value = (parseFloat((parseFloat(sum)).toFixed(3))).toFixed(2);
        gbi("txtDescuento").value = (parseFloat((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(1))).toFixed(2);
        gbi("txtIGVF").value = (parseFloat((((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100)).toFixed(1))).toFixed(2);
        gbi("txtTotalF").value = (parseFloat((parseFloat(((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - parseFloat(gbi("txtDescuento").value))).toFixed(3))).toFixed(1))).toFixed(2);
    }
    //gratuita
    gbi("txtGratuita").value = (parseFloat(parseFloat(sumGratuita).toFixed(3))).toFixed(2);
    if (sumExportacion > 0) {
        var sumaTotal = 0;
        var descuento = 0;
        var otrosCargos = parseFloat(gbi("txtOtrosCargosF").value);
        descuento = (parseFloat((parseFloat(sumExportacion * gbi("txtDescuentoPrincipal").value / 100), 1).toFixed(3))).toFixed(2);
        sumaTotal = sumExportacion + otrosCargos - descuento;

        gbi("txtDescuento").value = (parseFloat((descuento, 1).toFixed(3))).toFixed(2);
        gbi("txtExportacion").value = (parseFloat((parseFloat(sumExportacion - descuento), 1).toFixed(3))).toFixed(2);
        gbi("txtTotalF").value = (parseFloat((parseFloat(sumaTotal), 1).toFixed(3))).toFixed(2);
    } else {
        gbi("txtExportacion").value = (0).toFixed(2);
    }

}
function crearCadDetalleServicio() {
    var cdet = "";
    $(".rowDetServicio").each(function (obj) {
        //cdet += $(".rowDetServicio")[obj].children[0].innerHTML;//idDetalle
        cdet += "0|0"// + $(".rowDetServicio")[obj].children[1].innerHTML;
        cdet += "|0";//idArticulo
        cdet += "|" + $(".rowDetServicio")[obj].children[2].innerHTML;//DescripcionArticulo
        cdet += "|1";//Cantidad
        cdet += "|0";//idCategoria
        cdet += "|0";//descripcionCategoria
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//PrecioNacional
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//PrecioExtranjero
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//SubTotalNacional
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//SubTotalExtranjero
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function HabilitarDiv() {
    switch (gbi("txtTipoVenta").dataset.id) {
        case "1":
            gbi("tablaDeDetalles").style.display = "";
            gbi("divArticulos").style.display = "";
            gbi("tablaDeServicios").style.display = "none";
            gbi("divCita").style.display = "none";
            gbi("divOtroServicio").style.display = "none";
            break;
        case "2": gbi("tablaDeDetalles").style.display = "none";
            gbi("divArticulos").style.display = "none";
            gbi("tablaDeServicios").style.display = "";
            gbi("divCita").style.display = "";
            gbi("divOtroServicio").style.display = "none";
            break;
        case "3": gbi("tablaDeDetalles").style.display = "none";
            gbi("divArticulos").style.display = "none";
            gbi("tablaDeServicios").style.display = "";
            gbi("divCita").style.display = "none";
            gbi("divOtroServicio").style.display = "";
            break;
    }
}
//cargar medicamentos
function cargarListaArticulo(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
}
//Busqueda
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        listaImpresion = listas[3].split("▼");
        listaReportePorCliente = listas[4].split("▼");

        matriz = crearMatriz(listaDatos);
        configurarFiltro(cabeceras);
        mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
        reziseTabla();
    }
}
function BuscarxFecha(f1, f2) {
    var url = 'DocumentoVenta/ObtenerPorFecha?fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
}
//Limpiar
function limpiarTodo() {
    bDM("txtID");
    bDM("txtTipoCambio");
    bDM("txtCita");
    bDM("txtTipoVenta");
    bDM("txtTipoDocumento");
    gbi("cboNroSerie").value = "";
    bDM("txtNroComprobante");
    bDM("txtRazonSocial");
    limpiarControl("txtNroDocumento");
    limpiarControl("txtNroDocumento");
    bDM("txtDireccion");
    bDM("txtMoneda");
    bDM("txtObservacion");
    bDM("txtFormaPago");
    gbi("tb_DetalleServicio").innerHTML = "";
    gbi("tb_DetalleF").innerHTML = "";
    //gbi("txtSubTotalF").value = parseFloat(0).toFixed(2);
    gbi("txtTotalF").value = parseFloat(0).toFixed(2);
    gbi("txtIGVF").value = parseFloat(0).toFixed(2);
    gbi("txtGravada").value = parseFloat(0).toFixed(2);
    gbi("txtDescuento").value = parseFloat(0).toFixed(2);
    gbi("txtIGVF").value = parseFloat(0).toFixed(2);
    gbi("txtTotalF").value = parseFloat(0).toFixed(2);
    gbi("txtCita");
    gbi("rowFrm").querySelectorAll("input, select").forEach(item => { if (item.id) limpiarControl(item.id); });
    //
    gbi("cboTipoAfectacion").value = gbi("cboTipoAfectacion").options[0].value;
    let fecha = new Date();
    let fecPart = {
        day: (fecha.getDate() < 10 ? `0${fecha.getDate()}` : fecha.getDate()),
        month: (fecha.getMonth() + 1),
        year: fecha.getFullYear()
    };
    gbi("txtFecha").value = `${fecPart.day}-${fecPart.month}-${fecPart.year}`;
    gbi("txtFecha").onchange();
    limpiarCamposDetalle();
}
function limpiarCamposDetalle() {
    //bDM("txtCategoria");
    bDM("txtArticulo");
    gbi("txtCantidad").value = "";
    gbi("txtPrecio").value = "";
    gbi("txtTotal").value = "";
}
function limpiarTablaDetalle() {
    $("#tb_DetalleF").html("");
}
//Tabla
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
                if (matriz[i][8] == "INACTIVO") {
                    var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;color:red; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                } else {
                    var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                }
                for (var j = 0; j < cabeceras.length; j++) {
                    var enlaceDoc = (matriz[i][9]).toLowerCase();
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0: case 8:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-2'>";
                            break;
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 3: case 2: case 5: case 6: case 7:
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
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' title='Anular'; style='padding:3px 10px;'  onclick='anular(\"" + matriz[i][0] + "\")'> <i class='fa fa-ban'></i> </button>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' title='Ver'; style='padding:3px 10px;'  onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-eye'></i></button>";

                if ((matriz[i][2]).includes("TK")) {
                    contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:3px 10px;' onclick='obtenerDatosTicket(" + matriz[i][0] + ")'> <i class='fa fa-file-pdf-o'></i></a>";
                } else {
                    if (enlaceDoc.length>10) {
                        contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:3px 10px;'  target='_blank' href='" + enlaceDoc + "'> <i class='fa fa-file-pdf-o'></i></a>";
                    } else {
                        contenido2 += "<a class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:3px 10px;' id='btnConsultaDcto' onclick='ConsultarDocumento(\"" + matriz[i][2] + "-" + matriz[i][3] + "\")'> <i class='fa fa-file-pdf-o'></i></a>";
                    }
                    
                }


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
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0: case 8:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 3: case 2: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
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
//Extras
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
function cLP(r) {
    if (r.split('↔')[0] == "OK") {
        var listas = r.split('↔');
        var lp = listas[2].split("▲");
        var Precio = lp[9];
        gbi("txtPrecio").value = Precio == null ? 0 : Precio;
    }
}
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}
////Carga con botones de Modal
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
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtTipoVenta":
            HabilitarDiv();
            return gbi("txtTipoDocumento");
            break;
        case "txtTipoDocumento":
            var url = 'DocumentoVenta/ObtenerSeriexTipoDocumento?id=' + gbi("txtTipoDocumento").dataset.id;
            enviarServidor(url, CargarSeriesVenta);
            return gbi("cboNroSerie");
            break;
        case "txtCita":
            gbi("tb_DetalleServicio").innerHTML = "";
            var url = 'DocumentoVenta/ObtenerDatosCita?idCita=' + gbi("txtCita").dataset.id;
            enviarServidor(url, CargarDetalleCitaVenta);
            gbi("txtPrecio").focus();
            break;
        case "txtRazonSocial":
            var Tipo = document.getElementById("txtRazonSocial").getAttribute('data-id');
            if (Tipo != 0 || Tipo != null) {
                //var url = 'PagarSocio/ObtenerCuentasSocioCombo?id=' + Tipo;
                //enviarServidor(url, cdC);
            } else {
                mostrarRespuesta("Error", "elija un Cliente", "error");
            }
            return gbi("txtDireccion");
            break;
        case "txtDireccion":
            return gbi("txtTipoCambio");
            break;
        case "txtMoneda":
            return gbi("txtFormaPago");
            break;
        case "txtFormaPago":
            return gbi("txtArticulo");
            break;
        case "txtArticulo":
            //var u = "ListaPrecio/ObtenerPrecioArtProv?iA=" + gvc("txtArticulo") + "&iP=" + gvc("txtRazonSocial") + "&iM=" + gvc("txtMoneda");
            //enviarServidor(u, cLP);
            var txtArt = gbi("txtArticulo").dataset.id;
            var url = 'Medicamento/ObtenerDatosxID?id=' + txtArt;
            enviarServidor(url, cLP);
            break;
            return gbi("txtCantidad");
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
function cfgTMKP(evt) {
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        var n = o.replace("txt", "");
        gbi("btnModal" + n).click();
    }
}
function cfgTKP(evt) {
    //event.target || event.srcElement
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {
            case "cboNroSerie":
                gbi("txtFecha").focus();
                break;
            case "txtFecha":
                var txtFecha = gbi("txtFecha").value;
                var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + txtFecha;
                enviarServidor(url, CargarTipoCambio);
                gbi("txtObservacion").focus();
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
                    calcularSumaDetalle();
                }
                break;
            case "txtObservacion":
                gbi("txtRazonSocial").focus();
                break;
            case "txtTipoCambio":
                gbi("txtMoneda").focus();
                break;
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

                break;
            case "txtDescuentoPrincipal":
                if (gbi("txtTipoVenta").dataset.id == "1") {
                    if (document.getElementsByClassName("rowDet").length > 0) {
                        calcularSumaDetalle();
                    }
                } else {
                    if (document.getElementsByClassName("rowDetServicio").length > 0) {
                        calcularSumaDetalleServicio();
                    }
                }
                break;
            default:
                break;

        }
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
//SERIE
function CargarSeriesVenta(rpta) {
    var listaSeries = rpta.split("▼");
    llenarCombo(listaSeries, "cboNroSerie", "Seleccione");
    ObtenerNumeroDocumento();
}
function llenarCombo(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {
        campos = lista[i].split("▲");
        contenido += "<option value='" + campos[1] + "' " + (nRegistros == 1 ? "selected" : "") + ">" + campos[1] + "</option>";
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function llenarComboIGV(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {
        campos = lista[i].split("▲");
        contenido += "<option value='" + campos[0] + "' " + (nRegistros == 1 ? "selected" : "") + ">" + campos[1] + "</option>";
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
gbi("cboNroSerie").onchange = function () {
    var cboSerie = gbi("cboNroSerie").value;
    var url = 'DocumentoVenta/ObtenerNumeroDoc?id=' + gbi("txtTipoDocumento").dataset.id + "&Serie=" + cboSerie;
    enviarServidor(url, CargarNumeroDocumento);
}
function CargarNumeroDocumento(rpta) {
    gbi("txtNroComprobante").value = rpta;
    //gbi("txtFecha").focus();
    gbi("txtObservacion").focus();
}
function ObtenerNumeroDocumento() {
    var cboSerie = gbi("cboNroSerie").value;
    var url = 'DocumentoVenta/ObtenerNumeroDoc?id=' + gbi("txtTipoDocumento").dataset.id + "&Serie=" + cboSerie;
    enviarServidor(url, CargarNumeroDocumento);
}
//
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
    $("#btnAgregarArticulo").show();
    cancel_AddArticulo();
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
        if (Resultado == 'OK' && datos[5]) {
            adt(datos[5], "txtTipoCambio");
        }
    }
}
//ImpresionDocumento
function ImpresionDocumento(id) {
    var url = 'DocumentoVenta/DownloadPDF?idDocumento=' + id;
    enviarServidor(url, CargarImpresion);
}
function CargarImpresion(rpta) {
    console.log(rpta);
}
function VentasDescargarPDF(tipoImpresion) {
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
    doc.setFont('helvetica');
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
    doc.text("VENTAS", width / 2, 95, "center");// + gbi("txtRequerimiento").value
    var xic = 140;
    var altc = 12;
    doc.setFontType("bold");
    doc.setFontSize(7);

    //Inicio de Detalle
    //Crear Cabecera
    var xid = 120;
    var xad = 12;
    agregarCabeceras();
    function agregarCabeceras() {
        doc.setFontType("bold");
        doc.setFontSize(7);
        doc.text("ITEM", 30, xid);
        doc.text("FECHA", 70, xid);
        doc.text("DOCUMENTO", 130, xid);
        doc.text("RAZON SOCIAL", 200, xid);
        doc.text("SUB TOTAL", width - 208, xid, 'right');
        doc.text("IGV", width - 138, xid, 'right');
        doc.text("TOTAL", width - 68, xid, 'right');
        doc.line(30, xid + 3, width - 30, xid + 1);
        doc.setFontType("normal");
        doc.setFontSize(6.5);
    }

    //Crear Detalle
    var n = 0;

    gbi("contentPrincipal").querySelectorAll(".row.panel.salt").forEach((item, index) => {
        doc.text((index + 1).toString(), 30, xid + xad + (n * xad));//item
        doc.text(item.children[1].lastChild.innerHTML, 70, xid + xad + (n * xad));//fecha
        doc.text(item.children[2].lastChild.innerHTML + "-" + item.children[3].lastChild.innerHTML, 130, xid + xad + (n * xad));//Numero doc
        doc.text(item.children[4].lastChild.innerHTML, 200, xid + xad + (n * xad));//cliente
        doc.text(item.children[5].lastChild.innerHTML, width - 208, xid + xad + (n * xad), 'right');//sub total
        doc.text(item.children[6].lastChild.innerHTML, width - 138, xid + xad + (n * xad), 'right');//igv
        doc.text(item.children[7].lastChild.innerHTML, width - 68, xid + xad + (n * xad), 'right');//total

        if (xid + xad + (n * xad) > height - 30) {
            doc.addPage();
            n = 0;
            xid = 30;
            agregarCabeceras();
            xid = 35;
        }
        n += 1;
    });

    if (tipoImpresion == "e") {//exportar
        doc.save("Ventas.pdf");
    }
    else if (tipoImpresion == "i") {//imprimir
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}
function obtenerDatosTicket(id) {
    var url = 'DocumentoVenta/ObtenerDatosxID?id=' + id;
    enviarServidor(url, imprimirTicket);
}
function imprimirTicket(rpta) {
    var listas = rpta.split("↔");
    var dataDocVenta = listas[1].split("▲");
    var dataDetalleVenta = listas[3].split("▼");
    var fecEmi = listas[4];
    var fecVen = listas[5];

    var doc = new jsPDF("p", "pt", [200, 400]),
        source = $("#iframePDF")[0],
        margins = {
            top: 18,
            bottom: 60,
            left: 80,
            width: 160
        };

    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;

    doc.setFont('arial');
    doc.setFontSize(12);
    doc.setFontType("bold");
    doc.text("Clínica Dermosalud", width / 2, 18, "center");
    //doc.setFontSize(5.5);
    doc.setFontSize(7);
    doc.text("DERMOSALUD S.A.C", width / 2, 28, "center");
    doc.setFontType("normal");
    doc.text("AV.MANUEL CIPRIANO DULANTO NRO. 1009 URB.", width / 2, 35, "center");
    doc.text("PUEBLO LIBRE", width / 2, 42, "center");
    doc.text("PUEBLO LIBRE - LIMA - LIMA", width / 2, 49, "center");

    doc.setFontType("bold");
    doc.text("RUC 20565643143", width / 2, 56, "center");

    doc.setFontType("normal");
    doc.text("www.clinicadermosalud.com", width / 2, 63, "center");

    //jalar datos
    doc.setFontType("bold");
    doc.text("TICKET", width / 2, 70, "center");
    doc.text(dataDocVenta[13] + "-" + dataDocVenta[14], width / 2, 77, "center");
    doc.text("CLIENTE", 0, 84);

    doc.setFontType("normal");
    doc.text("DNI: " + dataDocVenta[7], 0, 91);
    doc.text(dataDocVenta[5], 0, 98);


    let lineaAdicionalDir = 0;
    var y = 152;
    var yAlto = 7;
    var altoTemp = y;
    let lineaAdicional = 0;

    //direccion
    if (dataDocVenta[6].length > 52) {
        let primeraParte = dataDocVenta[6].substring(0, 52).trim();
        let primeraParteSplit = primeraParte.split(" ");
        primeraParteSplit.pop();
        let textoSplit = dataDocVenta[6].split(" ");

        let montoLetraParte1 = "";
        for (let item of primeraParteSplit) {
            montoLetraParte1 += item + " ";
        }
        montoLetraParte1 = montoLetraParte1.trim();
        doc.text(montoLetraParte1, 0, 105 + lineaAdicionalDir);

        let montoLetraParte2 = "";
        for (let i = primeraParteSplit.length; i < textoSplit.length; i++) {
            montoLetraParte2 += textoSplit[i] + " ";
        }
        montoLetraParte2 = montoLetraParte2.trim();

        lineaAdicionalDir = 7;
        y = y + lineaAdicionalDir;
        altoTemp = y;

        doc.text(montoLetraParte2, 0, 105 + lineaAdicionalDir);

    } else {
        doc.text(dataDocVenta[6], 0, 105 + lineaAdicionalDir);
    }

    doc.setFontType("bold");
    doc.text("FECHA EMISIÓN: ", 0, 112 + lineaAdicionalDir);
    doc.text("FECHA DE VENC: ", 0, 119 + lineaAdicionalDir);
    doc.text("MONEDA: ", 0, 126 + lineaAdicionalDir);
    doc.text("IGV: ", 0, 133 + lineaAdicionalDir);

    doc.setFontType("normal");
    doc.text(fecEmi, 61, 112 + lineaAdicionalDir);
    doc.text(fecVen, 62, 119 + lineaAdicionalDir);
    doc.text(dataDocVenta[67], 36, 126 + lineaAdicionalDir);
    doc.text("18.00 %", 18, 133 + lineaAdicionalDir);

    doc.line(0, 136 + lineaAdicionalDir, width, 136 + lineaAdicionalDir);

    //crear detalle


    doc.setFontType("bold");
    doc.text("[ CANT. ] DESCRIPCIÓN", 4, 144 + lineaAdicionalDir);
    doc.text("P/U", width - 40, 144 + lineaAdicionalDir, "right");
    doc.text("TOTAL", width - 4, 144 + lineaAdicionalDir, "right");

    dataDetalleVenta.forEach((item, index) => {
        var dataDet = item.split("▲");

        doc.setFontType("bold");
        doc.text("[ " + parseFloat(dataDet[4]).toFixed(2) + " ]", 4, y + ((index + lineaAdicional) * yAlto));
        doc.setFontType("normal");

        doc.text(dataDet[8], width - 40, y + ((index + lineaAdicional) * yAlto), "right");
        doc.text(parseFloat(dataDet[10]).toFixed(2), width - 4, y + ((index + lineaAdicional) * yAlto), "right");

        if (dataDet[3].length > 22) {
            let desc = dataDet[3];
            let textoSplit = desc.split(" ");
            let cadena = "";
            let lastTxt = "";
            let prevTxt = "";
            for (let txt of textoSplit) {
                let cadTmp = (cadena + `${txt} `);
                if (cadTmp.trim().length <= 20)
                    cadena += `${txt} `;
                else {
                    prevTxt = cadena;
                    doc.text(cadena.trim().toUpperCase(), 30, y + ((index + lineaAdicional) * yAlto));
                    lineaAdicional++;
                    cadena = `${txt} `;
                    lastTxt = `${txt} `;
                    /*
                    if (cadena.length <= 14) {
                        doc.setFontType("normal");
                        doc.text(cadena.trim().toUpperCase(), 30, y + ((index + lineaAdicional) * yAlto));
                    }
                    */
                }
            }
            if (cadena !== prevTxt) {
                doc.text(cadena.trim().toUpperCase(), 30, y + ((index + lineaAdicional) * yAlto));
                //lineaAdicional++;
            }


        } else {
            doc.text(dataDet[3], 28, y + ((index + lineaAdicional) * yAlto));
        }

        altoTemp = y + ((index + lineaAdicional) * yAlto);
    });

    //fin detalle
    doc.line(0, altoTemp + 4, 160, altoTemp + 4);
    //cambiar la posicion Y
    //totales
    doc.setFontType("bold");
    doc.text("GRAVADA", (width / 2), altoTemp + (yAlto * 2), "right");
    doc.text("IGV", (width / 2), altoTemp + (yAlto * 3), "right");
    doc.text("TOTAL", (width / 2), altoTemp + (yAlto * 4), "right");

    doc.text("S/", (width / 2) + 20, altoTemp + (yAlto * 2));
    doc.text("S/", (width / 2) + 20, altoTemp + (yAlto * 3));
    doc.text("S/", (width / 2) + 20, altoTemp + (yAlto * 4));

    doc.text(dataDocVenta[15], width - 4, altoTemp + (yAlto * 2), "right");
    doc.text(dataDocVenta[18], width - 4, altoTemp + (yAlto * 3), "right");
    doc.text(dataDocVenta[20], width - 4, altoTemp + (yAlto * 4), "right");

    doc.line(0, altoTemp + (yAlto * 4) + 4, 160, altoTemp + (yAlto * 4) + 4);

    doc.text("IMPORTE EN LETRAS:", 0, altoTemp + (yAlto * 6));
    doc.text("OBSERVACIONES: " + dataDocVenta[32], 0, altoTemp + (yAlto * 8) + 3);

    var montoLetras = numeroALetras(dataDocVenta[20], {
        plural: dataDocVenta[67],
        singular: dataDocVenta[67],
        centPlural: (dataDocVenta[20].split('.')[1]) + '/100',
        centSingular: (dataDocVenta[20].split('.')[1]) + '/100'
    });
    doc.setFontType("normal");

    if (montoLetras.length > 30) {
        let primeraParte = montoLetras.substring(0, 30).trim();
        let primeraParteSplit = primeraParte.split(" ");
        primeraParteSplit.pop();
        let textoSplit = montoLetras.split(" ");

        let montoLetraParte1 = "";
        for (let item of primeraParteSplit) {
            montoLetraParte1 += item + " ";
        }
        montoLetraParte1 = montoLetraParte1.trim();
        doc.text(montoLetraParte1, 78, altoTemp + (yAlto * 6));

        let montoLetraParte2 = "";
        for (let i = primeraParteSplit.length; i < textoSplit.length; i++) {
            montoLetraParte2 += textoSplit[i] + " ";
        }
        montoLetraParte2 = montoLetraParte2.trim();

        doc.text(montoLetraParte2, 0, altoTemp + (yAlto * 7));
    } else
        doc.text(montoLetras, 78, altoTemp + (yAlto * 6));

    doc.line(0, altoTemp + (yAlto * 9), 160, altoTemp + (yAlto * 9));


    doc.fromHTML(
        source, // HTML string or DOM elem ref.
        margins.left, // x coord
        margins.top, {
            // y coord
            width: margins.width // max width of content on PDF
        },
        function (dispose) {
            // dispose: object with X, Y of the last line add to the PDF
            //          this allow the insertion of new lines after html
            //doc.save("Test.pdf");
            doc.autoPrint();
            var iframe = document.getElementById('iframePDF');
            iframe.src = doc.output('dataurlstring');
        },
        margins
    );
}

function imprimirRegistroVentas() {
    if (listaImpresion.length == 0 || listaImpresion[0] == "") return;
    //BI base imponible
    let listaDocumentos = [];

    for (let item of listaImpresion) {
        let datos = item.split("▲");
        let obj = {
            fecha: datos[0],
            tipoDoc: datos[1],
            numDoc: datos[2],
            ruc: datos[3],
            cliente: datos[4],
            moneda: datos[5],
            tipoCambio: datos[6],
            baseImpDol: datos[7],
            baseImpSol: datos[8],
            inafecto: datos[9],
            igv: datos[10],
            total: datos[11],
            serie: datos[2].split("-")[0]
        };
    //documentos
        listaDocumentos.push(obj);
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
    doc.setFont('helvetica');
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("DERMOSALUD S.A.C", 15, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc: 20565643143", 15, 40);
    doc.text("AV.MANUEL CIPRIANO DULANTO NRO. 1009 URB. PUEBLO LIBRE - LIMA - LIMA", 15, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 15, 40, "right");
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 15, 50, "right");
    doc.setFontSize(14);
    doc.setFontType("bold");
    //Titulo de Documento y datos
    doc.text("REGISTRO DE VENTAS", width / 2, 80, "center");

    doc.setFontSize(8);
    doc.line(15, 105, width, 105);
    doc.line(15, 118, width, 118);

    //crear detalle
    var y = 130;
    var yAlto = 8;
    var altoTemp = 152;

    var xid = 115;
    var xad = 12;

    agregarCabeceras();
    function agregarCabeceras() {
        doc.setFontType("bold");
        doc.text("FECHA", 15, xid);
        doc.text("TD", 60, xid);
        doc.text("N° DOC", 85, xid);
        doc.text("RUC", 145, xid);
        doc.text("CLIENTE", 190, xid);
        doc.text("MON.", 290, xid);

        doc.text("T.C.", width - 260, xid, "right");
        doc.text("B.I.$", width - 215, xid, "right");
        doc.text("B.I.", width - 170, xid, "right");
        doc.text("INAFECTO", width - 125, xid, "right");
        doc.text("I.G.V.", width - 80, xid, "right");
        doc.text("TOTAL", width - 30, xid, "right");

        doc.setFontType("normal");
    }

    //Crear Detalle
    var n = 0;
    let sumBISol = 0, sumInafecto = 0, sumIGV = 0, sumTotal = 0;
    let sumSerie_BISol = 0, sumSerie_Inafecto = 0, sumSerie_IGV = 0, sumSerie_Total = 0;
    let sumDoc_BISol = 0, sumDoc_Inafecto = 0, sumDoc_IGV = 0, sumDoc_Total = 0;

    let tipoDocTemp = "";
    let serieTemp = "";

    let contSumPosY = 0;

    let contDocSerie = 0;
    let contTipoDoc = 0;
    let totalPosY = 0;

    for (let item of listaDocumentos) {
        let posY = xid + xad + (n * xad);// + (contSumPosY * xad);
        if (tipoDocTemp === "") { tipoDocTemp = item.tipoDoc }
        if (serieTemp === "") { serieTemp = item.serie }

        if (serieTemp != item.serie) {
            agregarTotalSerie();
        }

        if (tipoDocTemp != item.tipoDoc) {
            agregarTotalTipoDoc();
        }

        doc.text(item.fecha, 15, posY);
        doc.text(item.tipoDoc, 60, posY);
        doc.text(item.numDoc, 75, posY);

        doc.text(item.ruc, 135, posY);
        doc.text(item.cliente.substring(0, 20), 190, posY);
        doc.text(item.moneda, 290, posY);

        doc.text(item.tipoCambio, width - 260, posY, "right");
        doc.text(item.baseImpDol, width - 215, posY, "right");
        doc.text(item.baseImpSol, width - 170, posY, "right");
        doc.text(item.inafecto, width - 125, posY, "right");

        doc.text(item.igv, width - 80, posY, "right");
        doc.text(item.total, width - 30, posY, "right");

        sumSerie_BISol += parseFloat(item.baseImpSol);
        sumSerie_Inafecto += parseFloat(item.inafecto);
        sumSerie_IGV += parseFloat(item.igv);
        sumSerie_Total += parseFloat(item.total);


        sumDoc_BISol += parseFloat(item.baseImpSol);
        sumDoc_Inafecto += parseFloat(item.inafecto);
        sumDoc_IGV += parseFloat(item.igv);
        sumDoc_Total += parseFloat(item.total);

        contDocSerie++;
        contTipoDoc++;

        sumBISol += parseFloat(item.baseImpSol);
        sumInafecto += parseFloat(item.inafecto);
        sumIGV += parseFloat(item.igv);
        sumTotal += parseFloat(item.total);

        if (item === listaDocumentos[listaDocumentos.length - 1]) {
            posY = posY + xad;
            //posY = xid + xad + (n * xad) + (1 * xad) + 2;
            agregarTotalSerie();
            //posY -= (2 * xad);
            agregarTotalTipoDoc();
            totalPosY = posY;
        }

        agregarHoja();
        n += 1;

        function agregarHoja() {
            if (posY > height - 80) {
                doc.addPage();
                n = 0;
                xid = 30;
                agregarCabeceras();
                xid = 35;
            }
        }
        function agregarTotalSerie() {
            doc.setFontType("bold");
            doc.text(`TOTAL SERIE - ${serieTemp} (${contDocSerie} Docs.)`, 190, posY);

            serieTemp = item.serie;

            doc.text(sumSerie_BISol.toFixed(2), width - 170, posY, "right");
            doc.text(sumSerie_Inafecto.toFixed(2), width - 125, posY, "right");

            doc.text(sumSerie_IGV.toFixed(2), width - 80, posY, "right");
            doc.text(sumSerie_Total.toFixed(2), width - 30, posY, "right");

            //reiniciar variables de serie
            contDocSerie = 0;
            sumSerie_BISol = 0, sumSerie_Inafecto = 0, sumSerie_IGV = 0, sumSerie_Total = 0;
            //agregar salto de linea
            contSumPosY += 1;
            posY = posY + xad;
            //posY = posY + (contSumPosY * xad) + 2;
            //posY = posY + xad + 2;
            doc.setFontType("normal");

            agregarHoja();
            n += 1;
        }
        function agregarTotalTipoDoc() {
            doc.setFontType("bold");

            doc.line(100, posY - 10, 800, posY - 10);
            doc.text(`TOTAL DOC - ${tipoDocTemp} (${contTipoDoc} Docs.)`, 180, posY);

            serieTemp = item.serie;

            doc.text(sumDoc_BISol.toFixed(2), width - 170, posY, "right");
            doc.text(sumDoc_Inafecto.toFixed(2), width - 125, posY, "right");

            doc.text(sumDoc_IGV.toFixed(2), width - 80, posY, "right");
            doc.text(sumDoc_Total.toFixed(2), width - 30, posY, "right");

            //reiniciar variables de serie
            contTipoDoc = 0;
            sumDoc_BISol = 0, sumDoc_Inafecto = 0, sumDoc_IGV = 0, sumDoc_Total = 0;
            //agregar salto de linea
            contSumPosY += 1;
            posY = posY + (xad * 2);
            //posY = posY + (contSumPosY * xad);
            //posY = posY + xad + 5;
            doc.setFontType("normal");

            agregarHoja();
            n += 2;
        }
    }
    doc.setFontType("bold");
    totalPosY += xad;

    doc.line(100, totalPosY - 10, 800, totalPosY - 10);
    doc.text(`TOTAL FINAL`, 180, totalPosY);

    doc.text(sumBISol.toFixed(2), width - 170, totalPosY, "right");
    doc.text(sumInafecto.toFixed(2), width - 125, totalPosY, "right");

    doc.text(sumIGV.toFixed(2), width - 80, totalPosY, "right");
    doc.text(sumTotal.toFixed(2), width - 30, totalPosY, "right");


    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');
}
function imprimirVentaPorCliente() {
    if (listaReportePorCliente.length == 0 || listaReportePorCliente[0] == "") return;

    let listaDocumentos = [];

    for (let item of listaReportePorCliente) {
        let datos = item.split("▲");
        let obj = {
            tipoDoc: datos[0],
            numDoc: datos[1],
            fecha: datos[2],
            tipoVenta: datos[3],
            auxiliar: datos[4],
            total: datos[5],
            cantidad: datos[6],
            descripcion: datos[7],
            pUnit: datos[8],
            bImp: datos[9],
            igv: datos[10],
            totalDetalle: datos[11],
            moneda: datos[12]
        };
    //documentos
        listaDocumentos.push(obj);
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
    doc.setFont('helvetica');
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("DERMOSALUD S.A.C", 15, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc: 20565643143", 15, 40);
    doc.text("AV.MANUEL CIPRIANO DULANTO NRO. 1009 URB. PUEBLO LIBRE - LIMA - LIMA", 15, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 15, 40, "right");
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 15, 50, "right");
    doc.setFontSize(14);
    doc.setFontType("bold");
    //Titulo de Documento y datos
    doc.text("DETALLE DE VENTAS POR CLIENTE", width / 2, 80, "center");

    doc.setFontSize(8);

    //crear detalle
    var xid = 115;
    var xad = 12;

    agregarCabeceras();
    function agregarCabeceras() {
        doc.setFontType("bold");
        doc.line(15, xid - xad, width, xid - xad);
        doc.line(15, xid + xad + 2, width, xid + xad + 2);

        doc.text("Documento", 15, xid);
        doc.text("Fecha Doc.", 90, xid);
        doc.text("Tipo venta", 140, xid);
        doc.text("Auxiliar", 250, xid);
        doc.text("Inclu / No Inclu", width - 140, xid);
        doc.text("Monto Total", width - 50, xid);

        doc.setFontType("normal");
        doc.text("Cantidad", 30, xid + xad);
        doc.text("Descripción", 140, xid + xad);
        doc.text("P. Unit", width - 170, xid + xad);
        doc.text("B. Imp.", width - 115, xid + xad);
        doc.text("Imptos", width - 70, xid + xad);
        doc.text("Total", width - 30, xid + xad);
        xid = xid + xad;
        doc.setFontType("normal");
    }

    //Crear Detalle
    var n = 0;
    let docGroup = "";
    let nuevoGrupoCont = 0;
    for (let item of listaDocumentos) {
        let posY = xid + xad + (n * xad);
        let itemGroup = `${item.tipoDoc} ${item.numDoc}`;

        if (docGroup === "") {
            docGroup = itemGroup;
            agregarGrupoDoc();
        } else {
            if (docGroup != itemGroup) {
                agregarGrupoDoc();
            } else {
                agregarDetalleGrupoDoc();
            }
        }

        agregarHoja();
        n++;

        function agregarHoja() {
            if (posY > height - 80) {
                doc.addPage();
                n = 0;
                xid = 40;
                agregarCabeceras();
                xid = 45;
            }
        }
        function agregarGrupoDoc() {
            docGroup = itemGroup;

            n++;
            posY = xid + xad + (n * xad);

            doc.setFontType("bold");
            doc.text(`${item.tipoDoc} ${item.numDoc}`, 20, posY);
            doc.text(item.fecha, 90, posY);
            doc.text(item.tipoVenta, 140, posY);
            doc.text(item.auxiliar.substring(0, 40), 250, posY);
            doc.text("IGV Inclu.", width - 120, posY);
            doc.text(item.moneda, width - 60, posY);
            doc.text(item.total, width - 30, posY);

            posY = posY + xad;

            doc.setFontType("normal");
            doc.text(item.cantidad.toString(), 40, posY);
            doc.text(item.descripcion, 140, posY);

            doc.text(item.pUnit, width - 140, posY, "right");
            doc.text(item.bImp, width - 90, posY, "right");
            doc.text(item.igv, width - 50, posY, "right");
            doc.text(item.totalDetalle, width - 10, posY, "right");
            nuevoGrupoCont = 1;
            n++;
        }
        function agregarDetalleGrupoDoc() {
            doc.setFontType("normal");

            doc.text(item.cantidad.toString(), 40, posY);
            doc.text(item.descripcion, 140, posY);

            doc.text(item.pUnit, width - 140, posY, "right");
            doc.text(item.bImp, width - 90, posY, "right");
            doc.text(item.igv, width - 50, posY, "right");
            doc.text(item.totalDetalle, width - 10, posY, "right");

        }
    }


    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');
}
//Pagos
function buscarXCombo() {
    var comb = document.getElementById("cboTipoPago").value;
    if (gbi("cboTarjeta").options.length > 0) gbi("cboTarjeta").value = gbi("cboTarjeta").options[0].value;
    gbi("txtNOperacion").value = "";
    if (comb == 1) {// cuando es efectivo
        gbi("divOperacion").style.display = "none";
        gbi("divTarjeta").style.display = "none";
        gbi("divObservacionCaja").style.display = "none";
    }
    if (comb == 2) {// cuando es tarjeta
        gbi("divOperacion").style.display = "";
        gbi("divTarjeta").style.display = "";
        gbi("divObservacionCaja").style.display = "none";
    }
    if (comb == 3) {// cuando es tarjeta Y EFECTIVO
        gbi("divOperacion").style.display = "";
        gbi("divTarjeta").style.display = "";
        gbi("divObservacionCaja").style.display = "none";
    }
}
function limpiarModalPago() {
    gbi("txtNOperacion").value = "";
    gbi("cboTarjeta").selectedOptions[0].text = "";
}
function Truncate(value, decimales) {
    var aux_value = Math.pow(10, decimales);
    var result = (Math.Truncate(value * aux_value) / aux_value);
    console.log(result);
    return (Math.Truncate(value * aux_value) / aux_value);
}


function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" style="display:none;" data-id="' + (tipo == 1 ? data[6] : "0") + '">' + (tipo == 1 ? data[7] : "0") + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5" style="display:none;">-</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[8]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[10]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
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
//Otros Servicios
function addItemOtroServicio(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDetServicio" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDetServicio").length + 1) + '"style="margin: auto; margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : document.getElementsByClassName("rowDetServicio").length + 1) + '</div>';
    contenido += '  <div class="col-sm-1">' + (tipo == 1 ? document.getElementsByClassName("rowDetServicio").length + 1 : document.getElementsByClassName("rowDetServicio").length + 1) + '</div>';
    contenido += '  <div class="col-sm-8 p-t-5" data-id="' + (tipo == 1 ? data[3] : gvt("txtOtroServicio").toUpperCase()) + '">' + (tipo == 1 ? data[3] : gvt("txtOtroServicio").toUpperCase()) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5">' + (tipo == 1 ? parseFloat(data[4]).toFixed(3) : gvt("txtTotalOtroServicio")) + '</div>';
    contenido += '  <div class="col-sm-1">';
    contenido += '      <div class="row rowDetServiciobtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalleOtroServicio(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '</div>';
    gbi("tb_DetalleServicio").innerHTML += contenido;
}
function crearCadDetalleOtroServicio() {
    var cdet = "";
    $(".rowDetServicio").each(function (obj) {
        //cdet += $(".rowDetServicio")[obj].children[0].innerHTML;//idDetalle
        cdet += "0|0"// + $(".rowDetServicio")[obj].children[1].innerHTML;
        cdet += "|0";//idArticulo
        cdet += "|" + $(".rowDetServicio")[obj].children[2].innerHTML;//DescripcionArticulo
        cdet += "|1";//Cantidad
        cdet += "|0";//idCategoria
        cdet += "|0";//descripcionCategoria
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//PrecioNacional
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//PrecioExtranjero
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//SubTotalNacional
        cdet += "|" + $(".rowDetServicio")[obj].children[3].innerHTML;//SubTotalExtranjero
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function calcularSumaDetalleOtroServicio() {
    var sumExonerado = 0;
    var sumInafecta = 0;
    var sumGravada = 0;
    var SumGratuita = 0;
    var SumOtrosCargos = 0;
    var DescuentoTotal = 0;

    var sum = 0;
    var sumGratuita = 0;
    var sumExportacion = 0;

    $(".rowDetServicio").each(function (obj) {
        var idTipoAfect = $(".rowDetServicio")[obj].children[3].innerHTML;
        var Tipo = "";
        for (var i = 0; i < listaTipoAfectacion.length; i++) {
            if (listaTipoAfectacion[i].split("▲")[0] == idTipoAfect) {
                Tipo = (listaTipoAfectacion[i].split("▲")[1]).split("-")[0];
                break;
            }
        }
        if (Tipo.toUpperCase().indexOf("GRATUITA") != -1) {
            sumGratuita += parseFloat($(".rowDetServicio")[obj].children[3].innerHTML);
        }
        else if (Tipo.toUpperCase() == "EXPORTACIÓN") {
            sumExportacion += parseFloat($(".rowDetServicio")[obj].children[3].innerHTML);
        } else {
            sum += parseFloat($(".rowDetServicio")[obj].children[3].innerHTML);
        }
    });
    if (gbi("chkIGV").checked) {
        gbi("txtGravada").value = ((parseFloat(sum * 100 / 118)).toFixed(3));
        gbi("txtDescuento").value = ((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100)).toFixed(3));
        gbi("txtIGVF").value = (((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3));
        gbi("txtTotalF").value = ((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - gbi("txtDescuento").value)).toFixed(3);
    }
    else {
        gbi("txtGravada").value = ((parseFloat(sum)).toFixed(3));
        gbi("txtDescuento").value = ((parseFloat(gbi("txtGravada").value * gbi("txtDescuentoPrincipal").value / 100))).toFixed(3);
        gbi("txtIGVF").value = ((((parseFloat(gbi("txtGravada").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100))).toFixed(3);
        gbi("txtTotalF").value = (((parseFloat(gbi("txtGravada").value) + parseFloat(gbi("txtIGVF").value) - parseFloat(gbi("txtDescuento").value))).toFixed(3));
    }
    //gratuita
    gbi("txtGratuita").value = (parseFloat(sumGratuita).toFixed(3));
    if (sumExportacion > 0) {
        var sumaTotal = 0;
        var descuento = 0;
        var otrosCargos = parseFloat(gbi("txtOtrosCargosF").value);
        descuento = ((parseFloat(sumExportacion * gbi("txtDescuentoPrincipal").value / 100), 1).toFixed(3));
        sumaTotal = sumExportacion + otrosCargos - descuento;

        gbi("txtDescuento").value = ((descuento, 1).toFixed(3));
        gbi("txtExportacion").value = ((parseFloat(sumExportacion - descuento), 1).toFixed(3));
        gbi("txtTotalF").value = ((parseFloat(sumaTotal), 1).toFixed(3));
    } else {
        gbi("txtExportacion").value = (0).toFixed(3);
    }

}
function limpiarCamposDetalleOtroServicio() {
    gbi("txtOtroServicio").value = "";
    gbi("txtTotalOtroServicio").value = "";
}
function borrarDetalleOtroServicio(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
    calcularSumaDetalleOtroServicio();
}
function validarAgregarDetalleOtroServicio() {
    var error = true;
    if (validarControl("txtOtroServicio")) error = false;
    if (validarControl("txtTotalOtroServicio")) error = false;
    return error;
}

//Consulta
function ConsultarDocumento(Documento) {
    var datosDocumento = Documento.split('-');
    var serieDcto = datosDocumento[0];
    var NroDcto = datosDocumento[1];
    var idDcto = 0;
    switch (serieDcto.substr(0, 1)) {
        case "B": idDcto = 2; break;
        case "F": idDcto = 1; break;
    }
    var url = 'DocumentoVenta/ConsultarDocumento?idDocumento=' + idDcto + '&serieDcto=' + serieDcto + '&nroDcto=' + NroDcto;
    enviarServidor(url, CargarImpresion);
}
function CargarImpresion(rpta) {
    window.open(rpta, '_blank');
}
