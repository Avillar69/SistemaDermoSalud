var cabeceras = ["Local", "Observacion", "Fecha", "Estado"];
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
var listaLocales;
var listaTipoMovimiento;
var listaEstado;
var listaGuias;
var nombreEmpresa = "HARI´S SPORT EMPRESA INDIVIDUAL DE RESPONSABILIDAD LIMITADA";
var rucEmpresa = "20612173452";
var direccionEmpresa = "JR. ANCASH NRO. 1265 (ESQUINA CON TARAPACA) JUNIN - HUANCAYO - HUANCAYO";
//
var idxDetalle = 0;
var precioProducto = 0;
var Tipo = "ENTRADA";
var txtCantidad = document.getElementById("txtCantidad");
var txtPrecio = document.getElementById("txtPrecio");
var txtStock = document.getElementById("txtStock");
var txtStockTotal = document.getElementById("txtStockTotal");
$(function () {
    var url = "/OperacionesStock/ObtenerDatos?Tipo=" + Tipo;
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
    configBM();
});

//Listar Stock Entrada
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listaLocales = listas[3].split("▼");
            listaTipoMovimiento = listas[5].split("▼");
            listaEstado = listas[6].split("▼");
            var fechaInicio = listas[9];
            var fecha = listas[7];
            listaGuias = listas[8].split("▼");
            $("#txtFecha").val(fecha);
            $("#txtFilFecIn").val(fechaInicio.substring(0, 10));
            $("#txtFilFecFn").val(fecha.substring(0, 10));
            if (listaLocales.length > 0 && listaLocales[0] != "") {
                cargarDatosLocal(listaLocales);
                //CambioLocal();
            }
            if (listaTipoMovimiento.length > 0 && listaTipoMovimiento[0] != "") {
                cargarDatosTipoMovimiento(listaTipoMovimiento);
            }
            var Estados = [];
            for (var i = 0; i < listaEstado.length; i++) {
                var item = listaEstado[i].split("▲");
                var tipo = item[2].split("-");
                for (var j = 0; j < tipo.length; j++) {
                    // E : enrtada
                    if (tipo[j] == "E") {
                        Estados.push(listaEstado[i]);
                    }
                }
            }
            if (Estados.length > 0 && Estados[0] != "") {
                cargarDatosEstadoMovimiento(Estados);
            }

            let urlProducts = "/OperacionesStock/cargarProductos";
            enviarServidor(urlProducts, cargarDatosProductos);

            let urlCompras = "/OperacionesStock/cargarCompras";
            enviarServidor(urlCompras, cargarDatosCompras);

            listar(listaDatos);
        }
        else {
            Swal.fire(Resultado, mensaje, "error");
        }
    }
}
function listar(r) {
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idMovimiento: valor[0],
                local: valor[1],
                observacion: valor[2],
                fecha: valor[3],
                estado: valor[4]
            })
        });
        let cols = ["local", "observacion", "fecha", "estado"];
        loadDataTable(cols, newDatos, "idMovimiento", "tbDatos", cadButtonOptions(), false);
    }
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
            lblTituloPanel.innerHTML = "Nueva Entrada de Mercancia";//Titulo Insertar
            let urlCompras = "/OperacionesStock/cargarCompras";
            enviarServidor(urlCompras, cargarDatosCompras);
            gbi("art").style.display = "";
            gbi("art2").style.display = "";
            gbi("art3").style.display = "";
            gbi("cboTipoMovimiento").focus();
            break;
        case 2:
            let idMovimiento = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Entrada de Mercancia";//Titulo Modificar
            TraerDetalle(idMovimiento);
            show_hidden_Formulario();
            break;
    }
}
function BuscarxFecha(f1, f2) {
    var url = '/OperacionesStock/ObtenerPorFecha?Tipo=' + Tipo + '&fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
}
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        listar(listaDatos);
    }
}
function configurarBotonesModal() {
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }

    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "/OperacionesStock/Grabar";
            var frm = new FormData();
            frm.append("idMovimiento", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("idLocal", gbi("cboLocalDet").value);
            frm.append("Observaciones", txtObservacion.value.length == 0 ? "-" : txtObservacion.value);
            frm.append("Estado", true);
            frm.append("idTipoMovimiento", gbi("cboTipoMovimiento").value);
            frm.append("TipoMovimiento", 'ENTRADA');
            frm.append("idEstado", gbi("cboEstadoMovimiento").value);
            frm.append("idDocumento", gbi("cboCompra").value);
            frm.append("idGuiaRemision", "0");
            frm.append("Lista_Articulo", lista_Articulo());
            Swal.fire({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }

    //Detalle de Articulo
    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    btnAgregarArticulo.onclick = function () {
        var error = true;
        error = validarAddProducto();
        if (error) {
            if (parseFloat(gbi("txtPrecio").value) < parseFloat(precioProducto)) {
                mostrarRespuesta("Error", "El precio a ingresar no puede ser menor al precio base del Producto", "error");
            } else {
                addRowDetalle(0, []);
                limpiarCampoDetalle();
            }
        }
    }
    txtCantidad.onkeyup = function () {
        if (this.value == "") { this.value = "0.00"; }
        var stock = parseFloat(txtStock.value);
        txtStockTotal.value = (stock + parseFloat(this.value)).toFixed(2);
    }
    txtCantidad.onfocus = function () { this.select(); }
    txtPrecio.onfocus = function () { this.select(); }
}
function limpiarTodo() {
    limpiarControl("txtID");
    limpiarControl("cboLocalDet");
    limpiarControl("txtFecha");
    limpiarControl("cboTipoMovimiento");
    $("#cboCompra").val(null).trigger('change');
    limpiarControl("cboEstadoMovimiento");
    limpiarControl("txtObservacion");
    gbi("tbDetalle").innerHTML = "";
    limpiarCampoDetalle();

    var fec = new Date().toLocaleDateString();
    var fecha;
    if (fec.includes("/")) { fecha = fec.split("/"); }
    if (fec.includes("-")) { fecha = fec.split("-"); }
    var fechaStr = (fecha[0] * 1 < 10 ? "0" + fecha[0] : fecha[0]) + "/" + (fecha[1] * 1 < 10 ? "0" + fecha[1] : fecha[1]) + "/" + fecha[2];
    txtFecha.value = fechaStr;
}
function eliminar(id) {
    let idMov = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar esta Entrada de Mercancia?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var u = "/OperacionesStock/Eliminar?idMovimiento=" + idMov + "&TipoMovimiento=" + Tipo;
            enviarServidor(u, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se elminó la Entrada de Mercancia', 'error');
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó la Entrada de Mercancia";
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
//crear movimiento entrada
function cargarDatosLocal(r) {
    let locales = r
    $("#cboLocalDet").empty();
    $("#cboLocalDet").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        locales.forEach(element => {
            $("#cboLocalDet").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosTipoMovimiento(r) {
    let tipoMovimientos = r
    $("#cboTipoMovimiento").empty();
    $("#cboTipoMovimiento").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        tipoMovimientos.forEach(element => {
            $("#cboTipoMovimiento").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }

    $("#cboTipoMovimiento").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            switch (selectedValue) {
                case "9": gbi("txtPrecio").value = 0; gbi("txtPrecio").disabled = true;
                    gbi("art").style.display = "none"; gbi("art2").style.display = "none"; gbi("art3").style.display = "none";
                    gbi("cboCompra").value = "";
                    gbi("TCompra").style.display = "";
                    break;
                case "2": gbi("txtPrecio").value = ""; gbi("txtPrecio").disabled = false;
                    gbi("art").style.display = ""; gbi("art2").style.display = ""; gbi("art3").style.display = "";
                    gbi("cboCompra").value = "";
                    gbi("TCompra").style.display = "none";
                    break;
                default: gbi("txtPrecio").disabled = true;
                    gbi("art").style.display = ""; gbi("art2").style.display = ""; gbi("art3").style.display = "";
                    gbi("cboCompra").value = "";
                    gbi("TCompra").style.display = "none";
                    break;
            }
        }
    });
}
function cargarDatosEstadoMovimiento(r) {
    let eestadoMovimientos = r
    $("#cboEstadoMovimiento").empty();
    $("#cboEstadoMovimiento").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        eestadoMovimientos.forEach(element => {
            $("#cboEstadoMovimiento").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
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
            enviarServidor(urlProducto, cLP);
            var url_2 = "/OperacionesStock/cargarStock?idProducto=" + selectedValue + "&idAlmacenO=0";// + almacenId;
            enviarServidor(url_2, cargarStock);
        }
    });

}
function cargarDatosCompras(r) {
    let dataP = r.split("↔");
    let productos = dataP[2].split("▼");
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    //["idCompra", "Fecha", "Numero", "Razon Social", "Total"
    for (var j = 0; j < productos.length; j++) {
        let objChild = {
            id: productos[j].split('▲')[0],
            text: productos[j].split('▲')[2] + ' | ' + productos[j].split('▲')[3]
        };
        arr.push(objChild);
    }
    $("#cboCompra").select2({
        placeholder: "Seleccione",
        data: arr, allowClear: true,
        escapeMarkup: function (e) {
            return e;
        }
    });
    $("#cboCompra").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            document.getElementById("tbDetalle").innerHTML = "";
            let urlCompra = '/OperacionesStock/cargarDetalleCompras?idCompras=' + selectedValue;
            enviarServidor(urlCompra, CargarDetalleCompra);
        }
    });
}
function cLP(r) {
    precioProducto = 0
    if (r.split('↔')[0] == "OK") {
        var listas = r.split('↔');
        var lp = listas[2].split("▲");
        var UnidadMedida = lp[0];
        var Precio = lp[9];
        gbi("txtMarca").dataset.id = lp[2];
        gbi("txtMarca").value = lp[3];
        gbi("txtPrecio").value = Precio == null ? 0 : Precio;
        gbi("txtCantidad").focus();
        precioProducto = gbi("txtPrecio").value;
    }
}
function cargarStock(rpta) {
    if (rpta != "") {
        let st = rpta.split("-");
        console.log(st);
        var stock = parseFloat(st[0]);
        txtStock.value = stock.toFixed(2);

        if (txtCantidad.value == "") { txtCantidad.value = "0.00"; }
        var cantidad = parseFloat(txtCantidad.value);
        var total = (cantidad + parseFloat(txtStock.value))
        txtStockTotal.value = total == 0 ? "0.00" : total.toFixed(2);
        txtCantidad.readOnly = false;
    }
}
function validarAddProducto() {
    var error = true;
    if (validarControl("cboProducto")) { error = false; }
    if (validarControl("txtCantidad")) { error = false; }
    if (parseFloat(txtCantidad.value) == 0) {
        txtCantidad.value = "";
        validarControl("txtCantidad");
        txtCantidad.value = "0.00";
        error = false;
    }
    return error;
}
function limpiarCampoDetalle() {
    $("#cboProducto").val(null).trigger('change');
    document.getElementById("txtCantidad").value = "0";
    document.getElementById("txtPrecio").value = "0.00";
    document.getElementById("txtMarca").dataset.id = "0";
    document.getElementById("txtMarca").value = "";
    document.getElementById("txtStock").value = "0.00";
    document.getElementById("txtStockTotal").value = "0.00";
    document.getElementById("txtUnidadMedida").value = "";
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
function addRowDetalle(tipo, data, idTipoMov) {
    let cad = "";
    let rows = gbi("tbDetalle").querySelectorAll(".rowDetalle");
    let cboProducto = gbi("cboProducto").value;
    if (tipo == 0) {
        var estaAgregado = false;
        for (let i = 0; i < rows.length; i++) {
            if (cboProducto == rows[i].children[2].dataset.id) {
                let cantidad = parseFloat(rows[i].children[5].innerHTML);
                rows[i].children[5].innerHTML = (cantidad + parseFloat(txtCantidad.value));
                estaAgregado = true;
                break;
            }
        }
        if (idxDetalle == 0 && !estaAgregado) {
            cad += "<tr class='rowDetalle'>";
            cad += '<td class="d-none">0</td>';
            cad += '<td class="" data-id="0">' + (rows.length + 1) + '</td>';
            cad += '<td class="" data-id="' + $("#cboProducto").val() + '">' + $("#cboProducto option:selected").text() + '</td>';
            cad += '<td class="" data-id="' + $("#txtMarca").data("id") + '">' + $("#txtMarca").val() + '</td>';
            cad += '<td class="d-none">' + $("#txtUnidadMedida").val() + '</td>';
            cad += '<td class="">' + parseFloat($("#txtCantidad").val()).toFixed(2) + '</td>';
            cad += '<td class="">' + parseFloat($("#txtPrecio").val()).toFixed(2) + '</td>';
            cad += '<td class="d-none">0</td>';
            cad += '<td class="">' + cadButtonDet("Detalle") + '</td>';
            cad += "</tr>";
            document.getElementById("tbDetalle").innerHTML += cad;
        }
        else {
            cad += '<td class="d-none">' + $("#lblDetalleId").html() + '</td>';
            cad += '<td class="" data-id="' + $("#lblMovimientoId").html() + '">' + $("#lblRowItem").html() + '</td>';
            cad += '<td class="" data-id="' + $("#cboProducto").val() + '">' + $("#cboProducto option:selected").text() + '</td>';
            cad += '<td class="" data-id="' + $("#txtMarca").data("id") + '">' + $("#txtMarca").val() + '</td>';
            cad += '<td class="d-none">' + $("#txtUnidadMedida").val() + '</td>';
            cad += '<td class="">' + parseFloat($("#txtCantidad").val()).toFixed(2) + '</td>';
            cad += '<td class="">' + parseFloat($("#txtPrecio").val()).toFixed(2) + '</td>';
            cad += '<td class="d-none">0</td>';
            cad += '<td class="">' + cadButtonDet("Detalle") + '</td>';
            $("#tbDetalle")[0].rows[idxDetalle - 1].innerHTML = cad;
        }
    } else {
        cad += "<tr class='rowDetalle'>";
        cad += '<td class="d-none">' + data[0] + '</td>';
        cad += '<td class="" data-id="' + data[1] + '">' + data[2] + '</td>';
        cad += '<td class="" data-id="' + data[3] + '">' + data[4] + '</td>';
        cad += '<td class="" data-id="' + data[5] + '">' + data[6] + '</td>';
        cad += '<td class="d-none">UNI</td>';
        cad += '<td class="">' + parseFloat(data[7]).toFixed(2) + '</td>';
        cad += '<td class="">' + parseFloat(data[8]).toFixed(2) + '</td>';
        cad += '<td class="d-none">0</td>';
        if (idTipoMov !== "9") {
            cad += '<td class="">' + cadButtonDet("Detalle") + '</td>';
        }
        cad += "</tr>";
        document.getElementById("tbDetalle").innerHTML += cad;
    }
    idxDetalle = 0;
}
function CargarDetalleCompra(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var det = listas[2].split("▼");
        let cadDet = "";
        if (Resultado == 'OK') {
            for (let i = 0; i < det.length; i++) {
                let itemDet = det[i].split("▲");
                cadDet += "<tr class='rowDetalle'>";
                cadDet += '<td class="d-none">0</td>';
                cadDet += '<td class="" data-id="0">' + (i + 1) + '</td>';
                cadDet += '<td class="" data-id="' + itemDet[0] + '">' + itemDet[1] + '</td>';
                cadDet += '<td class="" data-id="' + itemDet[2] + '">' + itemDet[2] + '</td>';
                cadDet += '<td class="d-none">UNI</td>';
                cadDet += '<td class="">' + parseFloat(itemDet[4]).toFixed(2) + '</td>';
                cadDet += '<td class="">' + parseFloat(itemDet[5]).toFixed(2) + '</td>';
                cadDet += '<td class="d-none">' + itemDet[6] + '</td>';
                cadDet += "</tr>";
            }
            document.getElementById("tbDetalle").innerHTML = cadDet;
        }
    }
}
function editDetalle(e) {
    gbi("art").style.display = "";
    gbi("art2").style.display = "";
    gbi("art3").style.display = "";
    idxDetalle = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#lblDetalleId").html($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[0].innerHTML);
    $("#lblMovimientoId").html($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[1].dataset.id);
    $("#lblRowItem").html($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[1].innerHTML);
    $("#cboProducto").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[2].dataset.id).trigger('change');
    gbi("txtMarca").dataset.id = $("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[3].dataset.id;
    $("#txtMarca").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[3].innerHTML);
    $("#txtUnidadMedida").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[4].innerHTML);
    $("#txtCantidad").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[5].innerHTML);
    $("#txtPrecio").val($("#tbDetalle")[0].rows[idxDetalle - 1].childNodes[6].innerHTML);
}
function deleteRowDet(e) {
    e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.remove();
    calcularSumaDetalle();
}
function validarFormulario() {
    var rows = gbi("tbDetalle").querySelectorAll(".rowDetalle");
    var error = true;
    if (validarControl("cboTipoMovimiento")) error = false;
    if (validarControl("cboLocalDet")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (error && rows.length == 0) {
        error = false;
        Swal.fire("Error", "Debe agregar productos al detalle", "error")
    }
    return error;
}
function lista_Articulo() {
    var cdet = "";
    $(".rowDetalle").each(function (obj) {
        cdet += $(".rowDetalle")[obj].children[0].innerHTML;//idmovDetalle
        cdet += "|" + $(".rowDetalle")[obj].children[1].dataset.id;//idMov
        cdet += "|" + $(".rowDetalle")[obj].children[2].dataset.id;//idProducto
        cdet += "|" + $(".rowDetalle")[obj].children[1].innerHTML;//item
        cdet += "|" + $(".rowDetalle")[obj].children[5].innerHTML;//Cantidad
        cdet += "|0";//Cantidad Recibida
        cdet += "|" + $(".rowDetalle")[obj].children[6].innerHTML;//precio
        cdet += "|true";//Cantidad Recibida
        cdet += "¯";
    });
    return cdet;

}
function actualizarListar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;
        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó la Entrada de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó la Entrada de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
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
// editar movimiento ingreso
function TraerDetalle(id) {
    var url = "/OperacionesStock/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            var listaCabecera = listas[2].split("▲");
            console.log(listaCabecera);
            $("#txtID").val(listaCabecera[0]);
            $("#cboLocalDet").val(listaCabecera[1]).trigger('change');
            $("#cboTipoMovimiento").val(listaCabecera[2]).trigger('change');
            $("#cboEstadoMovimiento").val(listaCabecera[6]).trigger('change');
            $("#txtObservacion").val(listaCabecera[3]);
            var fecha = listaCabecera[7].split("-");
            var fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];
            $("#txtFecha").val(fechaStr);

            var listaDetalle = listas[4].split("▼");
            if (listaDetalle.length >= 1) {
                if (listaDetalle[0].trim() != "") {
                    for (var i = 0; i < listaDetalle.length; i++) {
                        addRowDetalle(1, listaDetalle[i].split("▲"), listaCabecera[2]);
                    }
                }
            }

            let cboTipoMov = $("#cboTipoMovimiento").val();
            if (cboTipoMov == "9") {
                $("#cboCompra").next(".select2-container").hide();
                gbi("txtCompra").style.display = "";
                gbi("txtCompra").value = listaCabecera[22];
                gbi("txtCompra").dataset.id = listaCabecera[5];
            } else {
                $("#cboCompra").next(".select2-container").show();
                gbi("txtCompra").style.display = "none";
                gbi("txtCompra").value = "";
                gbi("txtCompra").dataset.id = 0;
            }
            gbi("art").style.display = "none";
            gbi("art2").style.display = "none";
            gbi("art3").style.display = "none";
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}
//
//exportar
function configBM() {
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Entrada Mercancia", cabeceras, matriz, "Entrada   Mercancia ", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "Entrada Mercancia", cabeceras, matriz, "Entrada   Mercancia ", "a4", "i");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    }
}
function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    let cabPdf = ["Local", "Observacion", "Fecha", "Estado"];
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
        doc.save((nombre != undefined ? nombre : "nota_de_ingreso.pdf"));
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
        sa = txtArea1.document.execCommand("SaveAs", true, "Nota_ingresos.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}
//