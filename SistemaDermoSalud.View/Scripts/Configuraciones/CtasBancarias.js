var cabeceras = ["id", "Cuenta", "Banco", "Moneda", "Numero Cuenta", "Estado"];
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
var idTablaDetalle;
var idDiv;
var nombreEmpresa = "HARI´S SPORT EMPRESA INDIVIDUAL DE RESPONSABILIDAD LIMITADA";
var rucEmpresa = "20612173452";
var direccionEmpresa = "JR. ANCASH NRO. 1265 (ESQUINA CON TARAPACA) JUNIN - HUANCAYO - HUANCAYO";

$(function () {
    var url = "CuentaOrigen/ObtenerDatos";
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
    configBM();
});
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        //var mensaje = listas[2];
        if (Resultado == "OK") {
            listaMoneda = listas[1].split("▼");
            listaDatos = listas[2].split("▼");
            cargarDatosMoneda(listaMoneda);
            let urlBancos = "/Banco/ListarBancos";
            enviarServidor(urlBancos, cargarDatosBancos);
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
                cuenta: valor[1],
                banco: valor[2],
                moneda: valor[3],
                numCuenta: valor[4],
                estado: valor[5]
            })
        });
    }
    let cols = ["cuenta", "banco", "moneda", "numCuenta", "estado"];
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
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nueva Cuenta Bancaria";
            gbi("txtNombreCuenta").focus();
            break;
        case 2:
            let idCta = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Cuenta Bancaria";
            TraerDetalle(idCta)
            show_hidden_Formulario();
            gbi("txtNombreCuenta").focus();
            break;
    }
}
function limpiarTodo() {
    gbi("txtID").value = "";
    gbi("txtNombreCuenta").value = "";
    gbi("cboBanco").value = "0";
    gbi("cboMoneda").value = "0";
    gbi("txtNroCuenta").value = "";
    gbi("chkEstado").checked = true;
}
function eliminar(id) {
    let idCta = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar esta cuenta bancaria?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var url = "CuentaOrigen/Eliminar?idCuentaOrigen=" + idCta;
            enviarServidor(url, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se elminó la cuenta bancaria', 'error');
        }
    });        
}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó la Cuenta Bancaria';
            tipo = 'success';
        }
        else {
            mensaje = data[1]
            tipo = 'error';
        }
    } else {
        mensaje = 'No hubo respuesta';
    }
    Swal.fire(res, mensaje, tipo);
    setTimeout(function () {
        listaDatos = data[2].split("▼");
        listar(listaDatos);
    }, 1000);
}
//
//nuevo
function cargarDatosMoneda(r) {
    let marcas = r
    $("#cboMoneda").empty();
    $("#cboMoneda").append(`<option value="0">Seleccione</option>`);

    if (r && r.length > 0) {
        marcas.forEach(element => {
            $("#cboMoneda").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosBancos(r) {
    let rd = r.split("↔")
    let bancos = rd[2].split("▼");
    $("#cboBanco").empty();
    $("#cboBanco").append(`<option value="0">Seleccione</option>`);

    if (r && r.length > 0) {
        bancos.forEach(element => {
            $("#cboBanco").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[2]}</option>`);
        });
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "CuentaOrigen/Grabar";
            var frm = new FormData();
            frm.append("idCuentaOrigen", gbi("txtID").value);
            frm.append("NombreCuenta", gbi("txtNombreCuenta").value);
            frm.append("Banco", gbi("cboBanco").value);
            frm.append("DescripcionBanco", $("#cboBanco option:selected").text());
            frm.append("idMoneda", gbi("cboMoneda").value);
            frm.append("DescMoneda", $("#cboMoneda option:selected").text());
            frm.append("NumeroCuenta", gvt("txtNroCuenta"));
            frm.append("Estado", gbi("chkEstado").checked);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtNombreCuenta")) error = false;
    if (validarControl("cboBanco")) error = false;
    if (validarControl("cboMoneda")) error = false;
    if (validarControl("txtNroCuenta")) error = false;
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
//editar
function TraerDetalle(id) {
    var url = 'CuentaOrigen/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        if (Resultado == 'OK') {
            gbi("txtID").value = datos[0];
            gbi("txtNombreCuenta").value = datos[1];
            gbi("cboBanco").value = datos[2];
            gbi("cboMoneda").value = datos[4];
            gbi("txtNroCuenta").value = datos[6];            
            gbi("chkEstado").checked = datos[7] == "ACTIVO" ? true : false;
        }
    }
}
//
//exportar
function configBM() {
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Orden Compra", cabeceras, matriz, "Orden Compra", "a4", "e");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    };
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ImprimirOC();
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
    doc.text("CLICK.PE", 10, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 10, 40);
    doc.text("10459451043", 30, 40);
    doc.text("Dirección:", 10, 50);
    doc.text("Av. Santa Lucia Nro. 237 Z.I. la Aurora (a 2 Cuadras Clinica San Juan de Dios)", 50, 50);
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




