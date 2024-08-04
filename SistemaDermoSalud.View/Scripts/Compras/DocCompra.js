var cabeceras = ["Fecha", "Serie", "Numero", "Razon Social", "SubTotal", "IGV", "Total"];
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
var idxDetalle = 0;
var precioProducto = 0;
var nombreEmpresa = "HARI´S SPORT EMPRESA INDIVIDUAL DE RESPONSABILIDAD LIMITADA";
var rucEmpresa = "20612173452";
var direccionEmpresa = "JR. ANCASH NRO. 1265 (ESQUINA CON TARAPACA) JUNIN - HUANCAYO - HUANCAYO";
$(function () {
    var url = "Compras/ObtenerDatos";
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
    configBM();
});
//Listar DctoCompras
function mostrarLista(rpta) {
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
            $("#txtFilFecIn").val(fechaInicio.substring(0, 10));
            $("#txtFilFecFn").val(fechaFin.substring(0, 10));
            $("#txtFecha").val(FechaActual.substring(0, 10));
            $("#txtFechaVencimiento").val(FechaActual.substring(0, 10));
            cargarDatosTipoCompra(listaTipoCompra);
            cargarDatosTipoDocumento(listaComprobantes);
            cargarDatosProveedores(listaSocios);
            cargarDatosMonedas(listaMoneda);
            cargarDatosFormaPago(listaFormaPago);

            let urlProducts = "/OperacionesStock/cargarProductos";
            enviarServidor(urlProducts, cargarDatosProductos);
            var urlTipoCambio = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + FechaActual;
            enviarServidor(urlTipoCambio, CargarTipoCambio);
            listar(listaDatos);
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}
function listar(r) {
    let newDatos = [];
    if (r[0] !== '') {
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idDctoCompra: valor[0],
                fecha: valor[1],
                serie: valor[2],
                numero: valor[3],
                razonSocial: valor[4],
                subTotal: valor[5],
                igv: valor[6],
                total: valor[7]
            })
        });
    }
    let cols = ["fecha", "serie", "numero", "razonSocial", "subTotal", "igv", "total"];
    loadDataTable(cols, newDatos, "idDctoCompra", "tbDatos", cadButtonOptions(), false);
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

            if (gbi("txtDescuentoPrincipal").value >= 100) {
                mostrarRespuesta("Alerta", "El porcentaje no puede ser mayor al 100%", "error");
                return;
            }
            var url = "Compras/Grabar";
            var frm = new FormData();
            frm.append("idDocumentoCompra", gvt("txtID"));
            frm.append("idTipoCompra", gvt("cboTipoCompra"));
            frm.append("idTipoDocumento", gvt("cboTipoDocumento"));
            frm.append("idProveedor", gvt("cboRazonSocial"));
            frm.append("ProveedorRazon", $("#cboRazonSocial option:selected").text());
            frm.append("ProveedorDireccion", gvt("cboDireccion"));
            frm.append("ProveedorDocumento", gvt("txtNroDocumento"));
            frm.append("idMoneda", gvt("cboMoneda"));
            frm.append("idOrdenCompra", gvc("txtOC"));
            frm.append("idFormaPago", gvt("cboFormaPago"));
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
            enviarServidorPostCompra(url, actualizarListar, frm);
        }
    };
    var txtCantidad = document.getElementById("txtCantidad");
    txtCantidad.onfocus = function () { this.select(); }
    txtCantidad.onkeyup = function () {
        calcularTotal();
    }
    var txtPrecio = document.getElementById("txtPrecio");
    txtPrecio.onfocus = function () { this.select(); }
    txtPrecio.onkeyup = function () {
        calcularTotal();
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
            addRowDetalle(0, []);
            limpiarCamposDetalle();
            calcularSumaDetalle();
        }
    }
}
function BuscarxFecha(f1, f2) {
    var url = 'Compras/ObtenerPorFecha?fechaInicio=' + f1 + '&fechaFin=' + f2;
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
            lblTituloPanel.innerHTML = "Nuevo Documento de Compra";
            gbi("txtFecha").value = FechaActual;
            break;
        case 2:
            let idDctoCompra = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Documento de Compra";
            TraerDetalle(idDctoCompra);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    gbi("chkIGV").checked = false;
    gbi("cboTipoCompra").value = "";
    gbi("cboTipoDocumento").value = "";
    gbi("txtNroSerie").value = "";
    gbi("txtNroComprobante").value = "";
    gbi("txtObservacion").value = "";
    $("#cboRazonSocial").val(null).trigger('change');
    gbi("txtNroDocumento").value = "";
    gbi("cboDireccion").value = "";
    gbi("txtTipoCambio").value = "";
    gbi("cboFormaPago").value = ""
    gbi("txtDescuentoPrincipal").value = "";
    gbi("tbDetalle").innerHTML = "";
    gbi("txtSubTotalF").value = "";
    gbi("txtDescuento").value = "";
    gbi("txtIGVF").value = "";
    gbi("txtTotalF").value = "";
    gbi("txtID").value = "";
    limpiarCamposDetalle();

    //var lblNotas = document.getElementById('txtNotas');
    //lblNotas.innerHTML = "";

    //for (let item of gbi("rowFrm").querySelectorAll("input")) {
    //    if (item.id) { limpiarControl(item.id); }
    //}
}
function eliminar(id) {
    let idDctoCompra = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar este documento de compra?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var u = "Compras/Eliminar?idDocumentoCompra=" + idDctoCompra;
            enviarServidor(u, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se elminó el documento de compra', 'error');
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el dodcumento de compra";
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
//Crear DctoCompras
function cargarDatosTipoCompra(r) {
    let tipoCompras = r
    $("#cboTipoCompra").empty();
    $("#cboTipoCompra").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        tipoCompras.forEach(element => {
            $("#cboTipoCompra").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[2]}</option>`);
        });
    }
}
function cargarDatosTipoDocumento(r) {
    let tipoDocumentos = r
    $("#cboTipoDocumento").empty();
    $("#cboTipoDocumento").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        tipoDocumentos.forEach(element => {
            $("#cboTipoDocumento").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosProveedores(r) {
    let proveedores = r
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    for (var j = 0; j < proveedores.length; j++) {
        let objChild = {
            id: proveedores[j].split('▲')[0],
            text: proveedores[j].split('▲')[1]
        };
        arr.push(objChild);
    }
    $("#cboRazonSocial").select2({
        placeholder: "Seleccione",
        data: arr, allowClear: true,
        escapeMarkup: function (e) {
            return e;
        }
    });

    $("#cboRazonSocial").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            for (let i = 0; i < proveedores.length; i++) {
                if (selectedValue === proveedores[i].split("▲")[0]) {
                    $("#txtNroDocumento").val(proveedores[i].split("▲")[2])
                }
            }
            var url = "/SocioNegocio/ObtenerDireccionxID?id=" + selectedValue;
            enviarServidor(url, cargarDatosDireccion);

        }
    });
}
function cargarDatosDireccion(r) {
    let dtDireccion = r.split("↔");
    let direcciones = dtDireccion[2].split("▼");

    $("#cboDireccion").empty();
    $("#cboDireccion").append(`<option value="">Seleccione</option>`);
    if (direcciones && direcciones.length > 0) {
        direcciones.forEach(element => {
            $("#cboDireccion").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
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
function cargarDatosFormaPago(r) {
    let formaPagos = r
    $("#cboFormaPago").empty();
    $("#cboFormaPago").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        formaPagos.forEach(element => {
            $("#cboFormaPago").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosProductos(r) {
    let dataP = r.split("↔");
    let productos = dataP[2].split("▼");
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    for (var j = 0; j < productos.length; j++) {
        let objChild = {
            id: productos[j].split('▲')[0],
            text: productos[j].split('▲')[2]
        };
        arr.push(objChild);
    }
    $("#cboProducto").select2({
        placeholder: "Seleccione",
        data: arr, allowClear: true,
        escapeMarkup: function (e) {
            return e;
        }
    });

    $("#cboProducto").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            let urlProducto = '/Producto/ObtenerDatosxID?id=' + selectedValue;
            enviarServidor(urlProducto, cargarPrecio);
        }
    });
}
function CargarTipoCambio(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲');

        if (Resultado == 'OK') {
            $("#txtTipoCambio").val(datos[4]);
        }
    }
}
function cargarPrecio(r) {
    if (r.split('↔')[0] == "OK") {
        let listas = r.split('↔');
        let lp = listas[2].split("▲");
        console.log(lp);
        precioProducto = lp[9];
        let Precio = lp[9];
        gbi("txtPrecio").value = Precio == null ? 0 : Precio;
    }
}
function calcularTotal() {
    let txtPrecio = 0;
    let txtCantidad = 0;
    (gbi("txtPrecio").value == "") ? txtPrecio = 0 : txtPrecio = gbi("txtPrecio").value;
    (gbi("txtCantidad").value == "") ? txtCantidad = 0 : txtCantidad = gbi("txtCantidad").value;

    let tot = parseFloat(txtCantidad) * parseFloat(txtPrecio);
    gbi("txtTotal").value = tot.toFixed(2);
}
////  Agregar Item a Tabla Detalle
function validarAgregarDetalle() {
    var error = true;
    if (validarControl("cboProducto")) error = false;
    if (validarControl("txtCantidad")) error = false;
    if (validarControl("txtPrecio")) error = false;
    if (validarControl("txtTotal")) error = false;
    return error;
}
function limpiarCamposDetalle() {
    document.getElementById("lblDetalleId").innerHTML = "";
    document.getElementById("lblDocumentoId").innerHTML = "";
    $("#cboProducto").val(null).trigger('change');
    document.getElementById("txtCantidad").value = "";
    document.getElementById("txtPrecio").value = "";
    document.getElementById("txtTotal").value = "";
}
function calcularSumaDetalle() {
    var sum = 0;
    $(".rowDetalle").each(function (obj) {
        sum += parseFloat($(".rowDetalle")[obj].children[6].innerHTML);
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
function cadButtonDet(t) {
    let cad = "";
    cad += '<ul class="list-inline" style="margin-bottom: 0px;">';
    cad += '<li class="list-inline-item">';
    cad += '<div class="dropdown">';
    cad += '<button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">';
    cad += '<i class="ri-more-fill align-middle"></i>';
    cad += '</button>';
    cad += '<ul class="dropdown-menu dropdown-menu-end" style="">';
    cad += '<li>';
    cad += '<a class="dropdown-item edit-item-btn" href="javascript:void(0)" onclick="edit' + t + '(this)"><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>Editar</a>';
    cad += '</li>';
    cad += '<li>';
    cad += '<a class="dropdown-item remove-item-btn" href="javascript:void(0)" onclick="deleteRowDet(this);"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>';
    cad += '</li>';
    cad += '</ul>';
    cad += '</div>';
    cad += '</li>';
    cad += ' </ul>';
    return cad;
}
function addRowDetalle(tipo, data) {
    let cad = "";
    if (tipo == 0) {
        if (idxDetalle == 0) {
            cad += "<tr class='rowDetalle'>";
            cad += '<td class="d-none">0</td>';
            cad += '<td class="d-none">0</td>';
            cad += '<td class="" data-id="' + $("#cboProducto").val() + '">' + $("#cboProducto option:selected").text() + '</td>';
            cad += '<td class="">' + parseFloat($("#txtCantidad").val()).toFixed(2) + '</td>';
            cad += '<td class="">UNI</td>';
            cad += '<td class="">' + parseFloat($("#txtPrecio").val()).toFixed(3) + '</td>';
            cad += '<td class="">' + parseFloat($("#txtTotal").val()).toFixed(3) + '</td>';
            cad += '<td class="">' + cadButtonDet("Detalle") + '</td>';
            cad += "</tr>";
            document.getElementById("tbDetalle").innerHTML += cad;
        }
        else {
            cad += '<td class="d-none">0</td>';
            cad += '<td class="d-none">0</td>';
            cad += '<td class="" data-id="' + $("#cboProducto").val() + '">' + $("#cboProducto option:selected").text() + '</td>';
            cad += '<td class="">' + parseFloat($("#txtCantidad").val()).toFixed(2) + '</td>';
            cad += '<td class="">UNI</td>';
            cad += '<td class="">' + parseFloat($("#txtPrecio").val()).toFixed(3) + '</td>';
            cad += '<td class="">' + parseFloat($("#txtTotal").val()).toFixed(3) + '</td>';
            cad += '<td class="">' + cadButtonDet("Detalle") + '</td>';
            $("#tbDetalle")[0].rows[idxDetalle - 1].innerHTML = cad;
        }
    } else {
        cad += "<tr class='rowDetalle'>";
        cad += '<td class="d-none">' + data[0] + '</td>';
        cad += '<td class="d-none">' + data[1] + '</td>';
        cad += '<td class="" data-id="' + data[2] + '">' + data[3] + '</td>';
        cad += '<td class="">' + parseFloat(data[4]).toFixed(2) + '</td>';
        cad += '<td class="">' + data[5] + '</td>';
        cad += '<td class="">' + parseFloat(data[8]).toFixed(3) + '</td>';
        cad += '<td class="">' + parseFloat(data[10]).toFixed(3) + '</td>';
        cad += '<td class="">' + cadButtonDet("Detalle") + '</td>';
        cad += "</tr>";
        document.getElementById("tbDetalle").innerHTML += cad;
    }
    idxDetalle = 0;
}
function editDetalle(e) {
    idxDetalle = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#lblDetalleId").html($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[0].innerHTML);
    $("#lblDocumentoId").html($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[1].innerHTML);
    $("#cboProducto").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[2].dataset.id).trigger('change');
    $("#txtCantidad").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[3].innerHTML);
    $("#txtPrecio").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[5].innerHTML);
    $("#txtTotal").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[6].innerHTML);
}
function deleteRowDet(e) {
    e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.remove();
    calcularSumaDetalle();
}
function validarFormulario() {
    var error = true;

    if (validarControl("txtTipoCambio")) error = false;
    if (validarControl("cboDireccion")) error = false;
    if (validarControl("cboTipoDocumento")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (validarControl("cboTipoCompra")) error = false;
    if (validarControl("txtNroSerie")) error = false;
    if (validarControl("txtNroComprobante")) error = false;
    if (validarControl("cboRazonSocial")) error = false;
    if (validarControl("txtNroDocumento")) error = false;
    if (validarControl("cboMoneda")) error = false;
    if (validarControl("cboFormaPago")) error = false;
    if (error && parseInt($("#txtTotalF").val()) == 0 || parseInt($("#txtTotalF").val()) == "NaN") {
        mostrarRespuesta("Info", "Tiene que ingresar al menos un registro al detalle.", "warning");
        error = false;
    }

    return error;
}
function enviarServidorPostCompra(url, metodo, frm) {
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
function crearCadDetalle() {
    var cdet = "";
    $(".rowDetalle").each(function (obj) {
        cdet += $(".rowDetalle")[obj].children[0].innerHTML;//idDetalle
        cdet += "|" + $(".rowDetalle")[obj].children[1].innerHTML;
        cdet += "|" + ($(".rowDetalle")[obj].children[2].dataset.id || "0");//idArticulo
        cdet += "|" + $(".rowDetalle")[obj].children[2].innerHTML;//DescripcionArticulo
        cdet += "|" + $(".rowDetalle")[obj].children[3].innerHTML;//Cantidad
        cdet += "|0";//idCategoria
        cdet += "|0";//descripcionCategoria
        cdet += "|" + $(".rowDetalle")[obj].children[5].innerHTML;//PrecioNacional
        cdet += "|" + $(".rowDetalle")[obj].children[5].innerHTML;//PrecioExtranjero
        cdet += "|" + $(".rowDetalle")[obj].children[6].innerHTML;//SubTotalNacional
        cdet += "|" + $(".rowDetalle")[obj].children[6].innerHTML;//SubTotalExtranjero
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
//Editar Dcto Compra
function TraerDetalle(id) {
    var url = 'Compras/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
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

            gbi("txtID").value = datos[0];
            gbi("cboTipoCompra").value = datos[2];
            gbi("cboTipoDocumento").value = datos[3];
            gbi("txtNroSerie").value = datos[12];
            gbi("txtNroComprobante").value = datos[13];
            gbi("cboTipoDocumento").value = datos[3];
            gbi("txtFecha").value = datos[11];
            gbi("txtObservacion").value = datos[33];
            setTimeout(function () { $("#cboRazonSocial").val(datos[4]).trigger('change'); }, 200);
            gbi("txtFechaVencimiento").value = datos[35];
            gbi("txtNroDocumento").value = datos[7];
            setTimeout(function () { $("#cboDireccion").val(datos[6]).trigger('change'); }, 1500)
            gbi("txtTipoCambio").value = datos[16];
            gbi("cboMoneda").value = datos[8];
            gbi("cboFormaPago").value = datos[10];
            if (datos[23] == 'TRUE') {
                gbi("chkIGV").checked = true;
            } else {
                gbi("chkIGV").checked = false;
            }
            gbi("txtDescuentoPrincipal").value = datos[34];

            if (det[0] != "") {
                if (det.length >= 1) {
                    for (var i = 0; i < det.length; i++) {
                        addRowDetalle(1, det[i].split("▲"));
                    }
                }
            }

            gbi("txtSubTotalF").value = datos[14];
            gbi("txtDescuento").value = 0;
            gbi("txtIGVF").value = datos[17];
            gbi("txtTotalF").value = datos[19];
            calcularSumaDetalle();
        }
        else {
            mostrarRespuesta(Resultado, mensaje, 'error');
        }
    }
}
//
//exportar
function configBM() {
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
        fnExcelReport(cabeceras);
    }
}
function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    let cabPdf = ["Fecha", "Serie", "Número", "Razon Social", "SubTotal", "IGV", "Total"];
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
        doc.save((nombre != undefined ? nombre : "doc_compras.pdf"));
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
        sa = txtArea1.document.execCommand("SaveAs", true, "Descarga.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}
//




