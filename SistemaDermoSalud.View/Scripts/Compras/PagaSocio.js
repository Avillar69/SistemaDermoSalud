var cabeceras = ["Fecha", "Serie", "Numero", "Descripcion", "Pago", "Operacion", "Monto"];
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
var nombreEmpresa = "HARI´S SPORT EMPRESA INDIVIDUAL DE RESPONSABILIDAD LIMITADA";
var rucEmpresa = "20612173452";
var direccionEmpresa = "JR. ANCASH NRO. 1265 (ESQUINA CON TARAPACA) JUNIN - HUANCAYO - HUANCAYO";
/*ingrese una variable global para capturar los datos del documento por el id */
//configNav();
var datosDelDocumento = [];

$(function () {
    var url = "PagarSocio/ObtenerDatosPagoDetalle?id=1";
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
    configBM();
});
//listar
function mostrarLista(rpta) {
    //crearTablaCompras(cabeceras, "cabeTabla");
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
            cargarDatosMonedas(listaMoneda);
            let urlDocumento = "/PagarSocio/ListarPagosPendientes?id=1";
            enviarServidor(urlDocumento, cargarDatosPagosPendiente);

            let urlOrigen = "/PagarSocio/ObtenerCuentasOrigen";
            enviarServidor(urlOrigen, cargarDatosCuentaOrigen);
            listar(listaDatos);
        }
    }
}
function listar(r) {
    let newDatos = [];
    if (r[0] !== '') {
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                id: valor[0],
                fecha: valor[1],
                serie: valor[2],
                numero: valor[3],
                descripcion: valor[4],
                pago: valor[5],
                operacion: valor[6],
                monto: valor[7]
            })
        });
    }
    let cols = ["fecha", "serie", "numero", "descripcion", "pago", "operacion", "monto"];
    loadDataTable(cols, newDatos, "id", "tbDatos", cadButtonOptions(), false);

}
function cadButtonOptions() {
    let cad = "";
    cad += '<ul class="list-inline" style="margin-bottom: 0px;">';
    cad += '<li class="list-inline-item">';
    cad += '<div class="dropdown">';
    cad += '<button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">';
    cad += '<i class="ri-more-fill align-middle"></i>';
    cad += '</button>';
    cad += '<ul class="dropdown-menu dropdown-menu-end" style="">';
    cad += '<li>';
    cad += '<a class="dropdown-item edit-item-btn" href="javascript:void(0)" onclick="mostrarDetalle(2, this)" ><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>Editar</a>';
    cad += '</li>';
    cad += '<li>';
    cad += '<a class="dropdown-item remove-item-btn" data-bs-toggle="modal" href="javascript:void(0)" onclick="eliminar(this)"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>';
    cad += '</li>';
    cad += '</ul>';
    cad += '</div>';
    cad += '</li>';
    cad += ' </ul>';
    return cad;
}
function loadDataTable(cols, datos, rid, tid, btns, arrOrder, showFirstField) {
    var columnas = [];
    for (var i = 0; i < cols.length; i++) {
        let item = {
            data: cols[i]
        };
        columnas.push(item);
    }
    let itemBtn = {
        "data": null,
        "defaultContent": "<center>" + btns + "</center>"
    };
    columnas.push(itemBtn);
    tbDatos = $('#' + tid).DataTable({
        data: datos,
        columns: columnas,
        rowId: rid,
        order: arrOrder,
        columnDefs:
            [
                {
                    "targets": 0,
                    "visible": showFirstField,
                },
                {
                    "targets": columnas.length - 1,
                    "width": "10%"
                }],
        searching: !0,
        bLengthChange: !0,
        destroy: !0,
        pagingType: "full_numbers",
        info: !1,
        paging: !0,
        pageLength: 25,
        responsive: !0,
        footer: false,
        deferRender: !1,
        language: {
            "decimal": "",
            "emptyTable": "No existen registros a mostrar.",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Registros",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total registros)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Registros",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            search: "_INPUT_",
            searchPlaceholder: "Buscar ",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "<<",
                "last": ">>",
                "next": ">",
                "previous": "<"
            }
        }
    });
}
function configurarBotonesModal() {
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "PagarSocio/grabar";

            var MontoCancelar = parseFloat(gbi("txtMontoCancelar").value);
            var Diferencia = parseFloat(gbi("txtDiferencia").value);
            if (MontoCancelar > Diferencia || MontoCancelar <= 0) {
                mostrarRespuesta("Error", "El monto a pagar no puede ser mayor al Saldo actual", "error");
                return;
            }
            var frm = new FormData();
            if (datosDelDocumento.length <= 0) {
                frm.append("idPagoDetalle", gbi("txtID").value);
                frm.append("idPago", gbi("txtID2").value);
            }
            else {
                frm.append("idPagoDetalle", gbi("txtID").value);
                frm.append("idPago", datosDelDocumento[0]);
            }
            frm.append("idCajaDetalle", 1);//gvt texto 
            frm.append("idTipo", gbi("cboTipoOperacion").value);
            frm.append("idDocumento", gbi("cboDocumento").value);
            frm.append("idEmpresa", 1);
            frm.append("idConcepto", 0);
            frm.append("Concepto", "  ");
            frm.append("idFormaPago", gbi("cboMoneda").value);
            frm.append("DescripcionFormaPago", $("#cboMoneda option:selected").text());
            frm.append("NumeroOperacion", gvt("txtNOperacion"));
            frm.append("idCuentaBancario", gbi("cboCuentaOrigen").value);
            frm.append("NumeroCuenta", $("#cboCuentaOrigen option:selected").text());

            frm.append("idCuentaBancarioDestino", gbi("cboCuentaDestino").value);
            frm.append("NumeroCuentaDestino", $("#cboCuentaDestino option:selected").text());
            frm.append("Monto", gbi("txtMontoCancelar").value);

            frm.append("Estado", true);
            frm.append("Observacion", gbi("txtObservacion").value.length == 0 ? "-" : gbi("txtObservacion").value);
            frm.append("DescripcionOperacion", $("#cboTipoOperacion option:selected").text());

            if (gbi("cboTipoOperacion").value == "2") {
                frm.append("FechaDetalle", gbi("txtFechaCobroCheque").value);
            } else {
                frm.append("FechaDetalle", gbi("txtFechaPago").value);
            }
            enviarServidorPost(url, actualizarListar, frm);
            datosDelDocumento = [];
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
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
        listar(listaDatos);
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Pago";
            gbi("txtFechaPago").value = FechaActual;
            gbi("txtFechaCobroCheque").value = FechaActual;
            gbi("cboTipoOperacion").value = "";
            break;
        case 2:
            let idPago = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Pago";//Titulo Modificar
            TraerDetalle(idPago);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    limpiarControl("txtID");
    limpiarControl("txtID2");
    $("#cboDocumento").val(null).trigger('change');
    limpiarControl("txtDocumento");
    limpiarControl("txtFecha");
    limpiarControl("txtRazonSocial");
    limpiarControl("txtTotal");
    limpiarControl("txtMontoAplicado");
    limpiarControl("txtDiferencia");    
    limpiarControl("cboMoneda");   
    limpiarControl("txtMontoCancelar");
    limpiarControl("txtFechaPago");
    limpiarControl("cboTipoOperacion");
    limpiarControl("txtNOperacion");
    limpiarControl("cboCuentaDestino");
    limpiarControl("cboCuentaOrigen");
    limpiarControl("txtFechaCobroCheque")
    limpiarControl("txtObservacion");
}
function eliminar(id) {
    let idPago = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar este pago?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var u = "PagarSocio/Eliminar?idPagoDetalle=" + idPago;
            enviarServidor(u, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se elminó el pago', 'error');
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el pago";
            tipo = "success";
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
    } else {
        mensaje = "No hubo respuesta";
    }
    Swal.fire(res, mensaje, tipo);
    setTimeout(function () {
        listaDatos = data[2].split("▼");
        listar(listaDatos);
    }, 1000);
}

//
//crear
function cargarDatosMonedas(r) {
    let monedas = r
    $("#cboMoneda").empty();
    $("#cboMoneda").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        monedas.forEach(element => {
            $("#cboMoneda").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosPagosPendiente(r) {
    let dataP = r.split("↔");
    let pagos = dataP[2].split("▼");
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    for (var j = 0; j < pagos.length; j++) {
        let objChild = {
            id: pagos[j].split('▲')[0],
            text: pagos[j].split('▲')[1] + " | " + pagos[j].split('▲')[4] + "-" + pagos[j].split('▲')[3] + " | " + pagos[j].split('▲')[2] + " | " + pagos[j].split('▲')[5]
        };
        arr.push(objChild);
    }
    $("#cboDocumento").select2({
        placeholder: "Seleccione",
        data: arr, allowClear: true,
        escapeMarkup: function (e) {
            return e;
        }
    });

    $("#cboDocumento").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            var url = '/Pago/ObtenerDatosxIDPagoCompras?id=' + selectedValue;
            enviarServidor(url, CargarDetalleOC);
        }
    });
}
function cargarDatosCuentaOrigen(r) {
    let dataP = r.split("↔");
    let ctasOrigen = dataP[2].split("▼");
    $("#cboCuentaOrigen").empty();
    $("#cboCuentaOrigen").append(`<option value="">Seleccione</option>`);
    if (r && r.length > 0) {
        ctasOrigen.forEach(element => {
            $("#cboCuentaOrigen").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[2]}</option>`);
        });
    }
}
function cargarDatosCuentaDestino(r) {
    let dataP = r.split("↔");
    let ctasDestino = dataP[2].split("▼");
    $("#cboCuentaDestino").empty();
    $("#cboCuentaDestino").append(`<option value="">Seleccione</option>`);
    if (r && r.length > 0) {
        ctasDestino.forEach(element => {
            $("#cboCuentaDestino").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[2]}</option>`);
        });
    }
}
function CargarDetalleOC(rpta) {
    datosDelDocumento = "";
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲'); 
        datosDelDocumento = listas[1].split('▲');
        console.log(datosDelDocumento);
        if (Resultado == 'OK') {
            var temp = datos[6] + '-' + datos[7];
            adt(temp, "txtDocumento");
            adt(datos[13], "txtFecha", 1);
            adt(datos[9], "txtTotal", 1);
            adt(datos[11], "txtMontoAplicado", 1);
            adt(datos[12], "txtDiferencia", 1);
            adc(listaSocios, datos[4], "txtRazonSocial", 1);
            adt(datos[20], "cboMoneda", 1);
            adt(0, "txtID", 1);
            adt(datos[0], "txtID2", 1);

            let urlDestino = "/PagarSocio/ObtenerCuentasSocio?id=" + datosDelDocumento[4];
            enviarServidor(urlDestino, cargarDatosCuentaDestino);
        }
    }
}
function buscarXCombo() {
    var comb = document.getElementById("cboTipoOperacion").value;

    gbi("cboCuentaDestino").value = "";
    gbi("txtNOperacion").value = "";
    gbi("cboCuentaOrigen").value = "";

    if (comb == "1") {// cuando es transferencia 

        var lblTituloPanel = document.getElementById('lblOperacion');
        lblTituloPanel.innerHTML = "N°.Operacion";
        gbi("divCuentaOrigen").style.display = "";
        gbi("divCuentaDestino").style.display = "";
        gbi("divOperacion").style.display = "";
        gbi("divFCC").style.display = "none";
    }
    if (comb == "2") {//cuando es cheque
        gbi("divCuentaOrigen").style.display = "none";
        gbi("divCuentaDestino").style.display = "none";
        gbi("divOperacion").style.display = "";
        gbi("divFCC").style.display = "";
        var lblTituloPanel = document.getElementById('lblOperacion');
        lblTituloPanel.innerHTML = "N°.Cheque";//Titulo Insertar
    }
    if (comb == "3") {//cuando es deposito
        var lblTituloPanel = document.getElementById('lblOperacion');
        lblTituloPanel.innerHTML = "N°.Operacion";
        gbi("divCuentaDestino").style.display = "";
        gbi("divOperacion").style.display = "";
        gbi("divFCC").style.display = "none";
        gbi("divCuentaOrigen").style.display = "none";
    }
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtDocumento")) error = false;
    if (validarControl("txtMontoCancelar")) error = false;
    if (validarControl("txtFechaPago")) error = false;
    if (validarControl("txtNOperacion")) error = false;
    if (gbi("cboTipoOperacion").value == "1") {
        if (validarControl("cboCuentaOrigen")) error = false;
        if (validarControl("cboCuentaDestino")) error = false;
    } else if (gbi("cboTipoOperacion").value == "3") {
        if (validarControl("cboCuentaDestino")) error = false;
    } else {
        if (validarControl("txtFechaCobroCheque")) error = false;        
    }
    return error;
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
        if (res == "OK") {
            Swal.fire(res, mensaje, tipo);
            setTimeout(function () {
                show_hidden_Formulario(true);
                listaDatos = data[2].split("▼");
                listar(listaDatos);
            }, 1000);
        }
    }
}
//
//editar
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
            adt(datos[3], "cboTipoOperacion");
            if (datos[3] == 1) {
                var lblTituloPanel = document.getElementById('lblOperacion');
                lblTituloPanel.innerHTML = "N°.Operacion";
                gbi("divCuentaOrigen").style.display = "";
                gbi("divCuentaDestino").style.display = "";
                gbi("divOperacion").style.display = "";
                gbi("divFCC").style.display = "none";

                limpiarControl("txtNOperacion");
                limpiarControl("cboCuentaDestino");
                limpiarControl("cboCuentaOrigen");
            } else if (datos[3] == 2) {
                var lblTituloPanel = document.getElementById('lblOperacion');
                lblTituloPanel.innerHTML = "N°.Cheque";//Titulo Insertar
                gbi("divCuentaOrigen").style.display = "none";
                gbi("divCuentaDestino").style.display = "none";
                gbi("divOperacion").style.display = "";
                gbi("divFCC").style.display = "";
                limpiarControl("cboCuentaDestino");
                limpiarControl("cboCuentaOrigen");
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
            gbi("cboCuentaOrigen").value = datos[11];
            gbi("cboCuentaDestino").value = datos[21];

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
//
//exportar
function configBM() {
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
}
function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    let cabPdf = ["Fecha", "Serie", "Numero", "Descripcion", "Pago", "Operacion", "Monto"];
    for (var i = 0; i < cabPdf.length; i++) {
        columns[i] = cabPdf[i];
    }
    var data = [];
    let lstDatos = gbi("tbDatos").children[1].children;
    for (var i = 0; i < lstDatos.length; i++) {
        let lstcolDatos = lstDatos[i].children;
        data[i] = [];
        for (var j = 0; j < lstcolDatos.length; j++) {
            data[i][j] = lstcolDatos[j];
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
    doc.text(nombreEmpresa, 10, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 10, 40);
    doc.text(rucEmpresa, 30, 40);
    doc.text("Dirección:", 10, 50);
    doc.text(direccionEmpresa, 50, 50);
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
        doc.save((nombre != undefined ? nombre : "Pagos.pdf"));
    }
    else if (v == "i") {
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}
function fnExcelReport(cabeceras) {
    var tab_text = "<table border='2px'>";
    var j = 0;

    var nCampos = cabeceras.length;
    tab_text += "<tr >";
    for (var i = 0; i < nCampos; i++) {
        tab_text += "<td style='height:30px;background-color:#29b6f6'>";
        tab_text += cabeceras[i];
        tab_text += "</td>";
    }
    tab_text += "</tr>";

    let lstDatos = gbi("tbDatos").children[1].children;
    let nRegitros = lstDatos.length;
    for (var i = 0; i < nRegitros; i++) {
        let nCampos = lstDatos[i].children;
        tab_text += "<tr>";
        for (var j = 0; j < nCampos.length - 1; j++) {
            tab_text += "<td>";
            tab_text += nCampos[j].innerHTML;
            tab_text += "</td>";
        }
        tab_text += "</tr>";
    }
    tab_text = tab_text + "</table>";
    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    {
        txtArea1.document.open("txt/html", "replace");
        txtArea1.document.write(tab_text);
        txtArea1.document.close();
        txtArea1.focus();
        sa = txtArea1.document.execCommand("SaveAs", true, "Pagos.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}
//
