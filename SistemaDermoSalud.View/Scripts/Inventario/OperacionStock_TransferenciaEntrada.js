var cabeceras = ["idMovimiento", "Local", "Almacen Origen", "Almacen Destino", "Observacion", "Fecha", "Fecha Destino", "Estado"];
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

//variables modal
var matrizModal = [];
var listaDatosModal;
var cabecera_Modal = [];
var txtModal;//input para poner el valor
var txtValor;//input para obtener el valor

console.log("ingresando a aaaar");
//Inicializando
var Tipo = "TRANSFERENCIA_ENTRADA";//Transferencia
var url = "/ObtenerDatos?Tipo=" + Tipo;
enviarServidor(url, mostrarLista);
configurarBotonesModal();
reziseTabla();

var txtID = document.getElementById("txtID");
var cboLocal = document.getElementById("cboLocal");
var cboAlmacen = document.getElementById("cboAlmacen");
var cboTipoMovimiento = document.getElementById("cboTipoMovimiento");
var txtObservacion = document.getElementById("txtObservacion");
var txtFecha = document.getElementById("txtFecha");
var txtFechaDestino = document.getElementById("txtFechaDestino");
var cboEstado = document.getElementById("cboEstado");

var tbArticulo = document.getElementById("tbArticulo");
var txtArticulo = document.getElementById("txtArticulo");
var txtMarca = document.getElementById("txtMarca");
var txtUnidadMedida = document.getElementById("txtUnidadMedida");

var tbTransferencia = document.getElementById("tbTransferencia");
var btnTransferenciaSalida = document.getElementById("btnTransferenciaSalida");
var listaLocales;
var listaAlmacen;
var listaTipoMovimiento;
var listaEstado;
jQuery("#txtFechaDestino").datepicker({ format: "dd/mm/yyyy" });
console.log("script");

function mostrarLista(rpta) {
    crearTablaModal(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listaLocales = listas[3].split("▼");
            listaAlmacen = listas[4].split("▼");
            listaTipoMovimiento = listas[5].split("▼");
            listaEstado = listas[6].split("▼");
            var Fecha = listas[7];
            txtFecha.value = Fecha;
            var btnModalLocalDet = document.getElementById("btnModalLocalDet");
            btnModalLocalDet.onclick = function () {
                cbm("local", "Locales", "txtLocalDet", null,
                    ["idLocal", "Descripcion"], listaLocales, cargarSinXR);
                CambioLocal();
            }
            var btnModalAlmacen = document.getElementById("btnModalAlmacen");
            btnModalAlmacen.onclick = function () {
                cbmu("almacenDestino", "Almacenes", "txtAlmacen", null,
                    ["idLocal", "Descripcion"], "OperacionesStock/cargarAlmacenes?idLocal=" + gbi("txtLocalDet").dataset.id, cargarLista);
            }
            var btnModalAlmacenDestino = document.getElementById("btnModalAlmacenDestino");
            btnModalAlmacenDestino.onclick = function () {
                cbmu("almacen", "Almacenes", "txtAlmacenDestino", null,
                    ["idLocal", "Descripcion"], "OperacionesStock/cargarAlmacenes?idLocal=" + gbi("txtLocalDet").dataset.id, cargarLista);
            }
            var btnModalTipoMovimiento = document.getElementById("btnModalTipoMovimiento");
            btnModalTipoMovimiento.onclick = function () {
                cbm("tipoMovimiento", "Tipo Movimiento", "txtTipoMovimiento", null,
                    ["idTipo", "Descripcion"], listaTipoMovimiento, cargarSinXR);
            }

            var Estados = [];
            for (var i = 0; i < listaEstado.length; i++) {
                var item = listaEstado[i].split("▲");
                var tipo = item[2].split("-");
                for (var j = 0; j < tipo.length; j++) {
                    // TE : Transferencia Entrada
                    if (tipo[j] == "TE") {
                        Estados.push(listaEstado[i]);
                    }
                }
            }
            var btnModalEstado = document.getElementById("btnModalEstado");
            btnModalEstado.onclick = function () {
                cbm("estadoMovimiento", "Estado Movimiento", "txtEstadoMovimiento", null,
                    ["idEstado", "Descripcion"], Estados, cargarSinXR);
            }
            listar();
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}
function listar() {
    configurarFiltro();
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function TraerDetalle(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    var url = "OperacionesStock/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
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
                mensaje = "Se adicionó la Transferencia Entrada de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó la Transferencia Entrada de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            //cambiar estado a en proceso o recibido
            var sumCantEnviada = 0, sumCantRecibida = 0, recibido = "0", enProceso = "0";
            for (var i = 0; i < tbArticulo.rows.length; i++) {
                sumCantEnviada += parseFloat(tbArticulo.rows[i].cells[4].innerHTML);
                sumCantRecibida += parseFloat(tbArticulo.rows[i].cells[5].children[0].value);
            }
            for (var i = 0; i < cboEstado.options.length; i++) {
                if (cboEstado.options[i].text.toUpperCase() == "RECIBIDO") {
                    recibido = cboEstado.options[i].value;
                }
                if (cboEstado.options[i].text.toUpperCase() == "EN PROCESO") {
                    enProceso = cboEstado.options[i].value;
                }
            }
            cboEstado.value = sumCantEnviada == sumCantRecibida ? recibido : enProceso;
            //end
            var url = "OperacionesStock/Grabar";
            var frm = new FormData();
            frm.append("idMovimiento", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("idMovimientoref", tbTransferencia.dataset.id);
            frm.append("idLocal", cboLocal.value);
            frm.append("idAlmacenOrigen", tbTransferencia.dataset.almacen);
            frm.append("idAlmacenDestino", cboAlmacen.value);
            frm.append("FechaMovimiento", txtFecha.value);
            frm.append("FechaMovimientoDestino", txtFecha.value);
            frm.append("Observaciones", txtObservacion.value);
            frm.append("Estado", true);
            frm.append("idTipoMovimiento", cboTipoMovimiento.value);
            frm.append("TipoMovimiento", Tipo);
            frm.append("idEstado", cboEstado.value);

            frm.append("Lista_Articulo", lista_Articulo());
            swal({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
    //Detalle de Articulo
    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    btnAgregarArticulo.onclick = function () {
        var error = true;
        //error = validarAddArticulo();
        //if (error) {
        add_ItemArticulo();
        //}
    }
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            var cabeceras = listas[2].split("▼");
            var listaCabecera = cabeceras[0].split("▲");
            var transferencia = cabeceras[1].split("▲");
            var listaAlmacen = listas[3].split("▼");
            txtID.value = listaCabecera[0];
            cboLocal.value = listaCabecera[2] == "0" ? "" : listaCabecera[2];
            cboLocal.dataset.id = listaCabecera[2] == "0" ? "" : listaCabecera[2];
            llenarCombo(listaAlmacen, "cboAlmacen", "Seleccione");
            cboAlmacen.value = listaCabecera[4] == "0" ? "" : listaCabecera[4];
            cboAlmacen.dataset.id = listaCabecera[4] == "0" ? "" : listaCabecera[4];
            txtObservacion.value = listaCabecera[9];
            cboEstado.value = listaCabecera[13];
            var fecha = listaCabecera[6].split("-");
            var fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];
            txtFecha.value = fechaStr;

            fecha = listaCabecera[7].split("-");
            fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];
            txtFechaDestino.value = fechaStr;

            //cargar Transferencia
            if (transferencia.length > 0 && transferencia[0] != "") {
                var contenido = "";
                contenido += "<tr data-id='" + transferencia[0] + "' style='background: rgba(9, 182, 235, 0.498039);'>";
                contenido += "<td>1</td>";
                contenido += "<td data-id='" + transferencia[3] + "'>" + transferencia[20] + "</td>";
                contenido += "<td>" + transferencia[9] + "</td>";
                fecha = transferencia[6].split("-");
                fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];
                contenido += "<td>" + fechaStr + "</td>";
                contenido += "<td style='display:none;'>" + transferencia[13] + "</td>";
                contenido += "<td>" + transferencia[25] + "</td>";
                contenido += "</tr>";
                tbTransferencia.innerHTML = contenido;
            }
            tbTransferencia.dataset.id = listaCabecera[5] == "0" ? "" : listaCabecera[5];
            tbTransferencia.dataset.almacen = listaCabecera[3] == "0" ? "" : listaCabecera[3];
            //Detalle
            var listaDetalle = listas[4].split("▼");
            cargarDetalleArticulo(listaDetalle);
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    //limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nueva Transferencia de Entrada de Mercancia";//Titulo Insertar
            //btnTransferenciaSalida.textContent = "Cargar Transferencias de Entrada";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Transferencia de Entrada de Mercancia";//Titulo Modificar
            //btnTransferenciaSalida.textContent = "Ver Transferencia de Entrada";
            TraerDetalle(id);
            show_hidden_Formulario(true);
            break;
    }
}
function limpiarTodo() {
    //limpiarControl("txtID");
    //limpiarControl("cboLocal");
    //limpiarControl("cboAlmacen");
    //limpiarControl("txtObservacion");
    //limpiarControl("txtFecha");
    //limpiarControl("txtFechaDestino");
    //var fec = new Date().toLocaleDateString();
    //var fecha;
    //if (fec.includes("/")) { fecha = fec.split("/"); }
    //if (fec.includes("-")) { fecha = fec.split("-"); }
    //var fechaStr = (fecha[0] * 1 < 10 ? "0" + fecha[0] : fecha[0]) + "/" + (fecha[1] * 1 < 10 ? "0" + fecha[1] : fecha[1]) + "/" + fecha[2];
    //txtFechaDestino.value = fechaStr;
    //tbTransferencia.innerHTML = "";
    //tbTransferencia.dataset.id = "0";
    //tbTransferencia.dataset.almacen = "0";
    //tbArticulo.innerHTML = "";
    //cboAlmacen.innerHTML = "";
    //cboEstado.selectedIndex = 0;
}
function validarFormulario() {
    var error = true;
    //if (validarControl("cboLocal")) error = false;
    if (validarControl("cboAlmacen")) error = false;
    if (validarControl("txtObservacion")) error = false;
    //if (validarControl("txtFecha")) error = false;
    if (validarControl("txtFechaDestino")) error = false;
    if (error && txtFecha.value == "") {
        error = false;
        swal("Error", "Debe seleccionar una transferencia", "error")
    }
    if (error) {
        error = validarArticulos();
    }
    return error;
}
function validarArticulos() {
    var error = true;
    for (var i = 0; i < tbArticulo.rows.length; i++) {
        var input = tbArticulo.rows[i].cells[5].children[0];
        if (input.value == "") {
            input.style.border = "1px solid red";
            error = false;
        } else {
            input.style.border = "";
        }
    }
    return error;
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar este Registro?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "OperacionesStock/Eliminar?idMovimiento=" + id + "&TipoMovimiento=" + Tipo;
                enviarServidor(url, eliminarListar);
            } else {
                swal("Cancelado", "No se eliminó el Registro.", "error");
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó la Transferencia de Mercancia";
            tipo = "success";
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
    } else {
        mensaje = "No hubo respuesta";
    }
    mostrarRespuesta(res, mensaje, tipo);
    listaDatos = data[2].split("▼");
    listar();
}
function CambioLocal() {
    if (contentArticulo.length > 0) {
        swal({
            title: "Desea cambiar el Local?", text: "Si cambia de Local se perderan los Artículos agregados!", type: "warning",
            showCancelButton: true,
            confirmButtonText: "Si, cambiar de Local!",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                postLocales();
                swal.close();
            }
        });
    } else {
        //this.dataset.id = this.value;
        postLocales();
    }
    function postLocales() {
        if (txtLocalDet.value != "") {
            divArticulo.innerHTML = "";
            var btnModalAlmacen = document.getElementById("btnModalAlmacen");
            btnModalAlmacen.onclick = function () {
                cbmu("almacen", "Almacen", "txtAlmacen", null,
                    ["idAlmacen", "Descripción"], "/OperacionesStock/cargarAlmacenes?idlocal=" + gbi("txtLocalDet").dataset.id, cargarLista);
            }
        } else {
            txtAlmacen.value = "";
            divArticulo.innerHTML = "";
        }
    }
    cancel_AddArticulo();
}
//Detalle Articulo
function add_ItemArticulo() {
    var txtArticulo = document.getElementById("txtArticulo");
    var txtMarca = document.getElementById("txtMarca");
    var txtUnidadMedida = document.getElementById("txtUnidadMedida");
    var txtCantidad = document.getElementById("txtCantidad");
    var rows = gbi("contentArticulo").querySelectorAll(".art");
    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    var arti = rows.length + 1;
    var div = document.getElementById("contentArticulo");

    if (btnAgregarArticulo.dataset.row != undefined && btnAgregarArticulo.dataset.row != -1) {//editar
        rows[(btnAgregarArticulo.dataset.row * 1)].children[3].innerHTML = txtArticulo.value.toUpperCase().trim();
        rows[(btnAgregarArticulo.dataset.row * 1)].children[1].dataset.id = txtArticulo.dataset.id;
        rows[(btnAgregarArticulo.dataset.row * 1)].children[4].innerHTML = txtMarca.value.toUpperCase().trim();
        rows[(btnAgregarArticulo.dataset.row * 1)].children[5].innerHTML = txtUnidadMedida.value.toUpperCase().trim();
        rows[(btnAgregarArticulo.dataset.row * 1)].children[6].innerHTML = parseFloat(txtCantidad.value.trim()).toFixed(2);
    } else {//nuevo
        var estaAgregado = false;
        for (var i = 0; i < rows.length; i++) {
            if (txtArticulo.dataset.id == rows[i].children[1].dataset.id) {
                var cantidad = parseFloat(rows[i].children[5].innerHTML);
                rows[i].children[5].innerHTML = (cantidad + parseFloat(txtCantidad.value));
                estaAgregado = true;
                break;
            }
        }
        if (!estaAgregado) {
            var cadena = "<div class='art row panel salt' id='num0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
            cadena += "<div class='col-12 col-md-1' style='display:none' data-id='" + (rows.length + 1) + "'>" + (rows.length + 1) + "</div>";
            cadena += "<div class='col-12 col-md-1'>" + (rows.length + 1) + "</div>";
            cadena += "<div class='col-12 col-md-3'>" + txtArticulo.value + "</div>";
            cadena += "<div class='col-12 col-md-3'>" + txtMarca.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtUnidadMedida.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtCantidad.value + "</div>";
            cadena += "<div class='col-12 col-md-1'>";
            cadena += "<div class='row saltbtn'>";
            cadena += "<div class='col-12'>";
            cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' onclick='eliminar(\"" + (rows.length + 1) + "\")'> <i class='fa fa-trash-o fs-11'></i> </button>";
            cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalle(2, \"" + (rows.length + 1) + "\")'> <i class='fa fa-pencil fs-11'></i></button>";
            cadena += "</div>";
            cadena += "</div>";
            cadena += "</div>";
            cadena += "</div>";
            div.innerHTML += cadena;
        }
    }
    cancel_AddArticulo();
}
function cancel_AddArticulo() {
    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    var btnCancelarArticulo = document.getElementById("btnCancelarArticulo");
    btnCancelarArticulo.style.visibility = "hidden";
    btnAgregarArticulo.dataset.row = -1;
    limpiarControl("txtArticulo");
    limpiarControl("txtMarca");
    limpiarControl("txtUnidadMedida");
    txtCantidad.value = "0.00";
    txtStock.value = "0.00";
    txtStockTotal.value = "0.00";
    btnAgregarArticulo.innerHTML = "Agregar";
}
function validarAddArticulo() {
    var error = true;
    if (validarControl("txtArticulo")) { error = false; }
    if (validarControl("txtCantidad")) { error = false; }
    if (parseFloat(txtCantidad.value) == 0) {
        txtCantidad.value = "";
        validarControl("txtCantidad");
        txtCantidad.value = "0.00";
        error = false;
    }
    return error;
}
function lista_Articulo() {
    var rows = gbi("contentArticulo").querySelectorAll(".art");
    var lista = "";
    console.log(rows);
    for (var i = 0; i < rows.length; i++) {
        lista += rows[i].dataset.id + "|";//idMovimientoDetalle
        lista += "0|";//idMovimiento
        lista += rows[i].children[1].dataset.id + "|";//idArticulo
        lista += (i + 1) + "|";//Item
        lista += rows[i].children[5].innerHTML + "|0|";//Cantidad | Cantidad Recibida
        lista += "||0|0|1";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
        lista += "¯";
    }
    lista = lista.substring(0, lista.length - 1);
    return lista;
}
function cargarDetalleArticulo(lista) {
    if (lista.length > 0 && lista[0] != "") {
        for (var i = 0; i < lista.length; i++) {
            var item = lista[i].split("▲");
            add_ItemArticulo();
            rows[i].dataset.id = item[0];
            rows[i].children[1].dataset.id = item[1];
            rows[i].children[3].innerHTML = item[2];
            rows[i].children[4].innerHTML = item[3];
            rows[i].children[5].innerHTML = item[4];
            rows[i].children[6].innerHTML = item[5];
        }
    }
}
/*

cboLocal.onchange = function () {
    //cargar almacenenes
    if (cboLocal.value != "") {
        tbArticulo.innerHTML = "";
        var url = "OperacionesStock/cargarAlmacenes?idlocal=" + cboLocal.value;
        enviarServidor(url, cargarAlmacenes);
    } else {
        cboAlmacen.innerHTML = "";
        tbTransferencia.innerHTML = "";
        tbArticulo.innerHTML = "";
        txtFecha.value = "";
    }

    //if (tbTransferencia.rows.length > 0) {
    //    swal({
    //        title: "Desea cambiar el Local?", text: "Si cambia de Local se perderan los Artículos agregados!", type: "warning",
    //        showCancelButton: true,
    //        confirmButtonText: "Si, cambiar de Local!",
    //        closeOnConfirm: false
    //    }, function (isConfirm) {
    //        if (isConfirm) {
    //            cboLocal.dataset.id = cboLocal.value;
    //            postLocales();
    //            swal.close();
    //        } else {
    //            cboLocal.value = cboLocal.dataset.id;
    //        }
    //    });
    //} else {
    //    this.dataset.id = this.value;
    //    postLocales();
    //}
    //function postLocales() {

    //}
}



*/
function cargarAlmacenes(rpta) {
    if (rpta != "") {
        var lista = rpta.split("▼");
        llenarCombo(lista, "cboAlmacen", "Seleccione");
    }
}
/*
cboAlmacen.onchange = function () {
    //if (this.value != "") {
    //    var url = "OperacionesStock/cargarTransferenciaSalida?idLocal=" + cboLocal.value + "&idAlmacen=" + cboAlmacen.value;
    //    enviarServidor(url, cargarTransfrenciaSalida);
    //}
    //else {
    //    tbArticulo.innerHTML = "";
    //    tbTransferencia.innerHTML = "";
    //    txtFecha.value = "";
    //}
   
    if (tbArticulo.rows.length > 0) {
        swal({
            title: "Desea cambiar el Almacen?", text: "Si cambia de Almacen se perderan los Artículos agregados!", type: "warning",
            showCancelButton: true,
            confirmButtonText: "Si, cambiar de Almacen!",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                cboAlmacen.dataset.id = cboAlmacen.value;
                tbArticulo.innerHTML = "";
                swal.close();
            } else {
                cboAlmacen.value = cboAlmacen.dataset.id;
            }
        });
    } else {
        this.dataset.id = this.value;
    }
   

    cboAlmacen.onchange = function () {
        var urlA = 'ConsultaStock/ObtenerStockxAlmacen?pA=' + cboAlmacen.value;
        enviarServidor(urlA, mostrarArticulos);
    }
}
*/




function traerTransferenciaSalida() {
    if (txtID.value != "") {
        AbrirModal("modal-Modal");
    } else {
        if (cboAlmacen.value != "") {
            var url = "OperacionesStock/cargarTransferenciaSalida?idLocal=" + cboLocal.value + "&idAlmacen=" + cboAlmacen.value;
            enviarServidor(url, cargarTransfrenciaSalida);
        }
        else {
            tbArticulo.innerHTML = "";
            tbTransferencia.innerHTML = "";
            txtFecha.value = "";
        }
    }
}
function cargarTransfrenciaSalida(rpta) {
    tbTransferencia.innerHTML = "";
    tbArticulo.innerHTML = "";
    txtFecha.value = "";
    if (rpta != "") {
        var listas = rpta.split("↔");
        if (listas[0] != "") {
            var listaTransferencia = listas[0].split("▼");
            var listaDetalle = listas[1].split("▼");
            for (var i = 0; i < listaTransferencia.length; i++) {
                var item = listaTransferencia[i].split("▲");
                var contenido = "";
                contenido += "<tr data-id='" + item[0] + "'>";
                contenido += "<td>" + (i + 1) + "</td>";
                contenido += "<td data-id='" + item[1] + "'>" + item[2] + "</td>";
                contenido += "<td>" + item[3] + "</td>";
                contenido += "<td>" + item[4] + "</td>";
                contenido += "<td style='display:none;'>" + item[5] + "</td>";
                contenido += "<td>" + item[6] + "</td>";
                contenido += "</tr>";
                tbTransferencia.innerHTML += contenido;
            }
            tbTransferencia.ondblclick = function (e) {//onclick
                for (var i = 0; i < this.rows.length; i++) {
                    if (this.rows[i].style.background != "") {
                        this.rows[i].style.background = "";
                    }
                }
                if (e.target.parentElement.tagName == "TR") {
                    var tr = e.target.parentElement;
                    tr.style.background = "rgba(9, 182, 235, 0.5)";
                    txtFecha.value = tr.cells[3].innerHTML;
                    cboEstado.value = tr.cells[4].innerHTML;
                    tbTransferencia.dataset.id = tr.dataset.id;
                    tbTransferencia.dataset.almacen = tr.cells[1].dataset.id;
                    tbArticulo.innerHTML = "";
                    var cont = 1;
                    for (var i = 0; i < listaDetalle.length; i++) {
                        var item = listaDetalle[i].split("▲");
                        if (tr.dataset.id == item[0]) {
                            var contenido = "";
                            contenido += "<tr data-id='0'>";
                            contenido += "<td>" + cont + "</td>";
                            contenido += "<td data-id='" + item[2] + "'>" + item[3] + "</td>";
                            contenido += "<td>" + item[4] + "</td>";
                            contenido += "<td>" + item[5] + "</td>";
                            contenido += "<td>" + item[6] + "</td>";
                            contenido += "<td style='padding:0px'><input class='form-control input-sm' /></td>";
                            contenido += "</tr>";
                            cont++;
                            tbArticulo.innerHTML += contenido;
                        }
                    }
                }
                CerrarModal("modal-Modal");
            }
        }
    }
    AbrirModal("modal-Modal");
}

function cargarStock(rpta) {
    if (rpta != "") {
        var stock = rpta.split("-");
        var stock = parseFloat(stock[0]);
        txtStock.value = stock.toFixed(2);
        if (txtCantidad.value == "") { txtCantidad.value = "0.00"; }
        var cantidad = parseFloat(txtCantidad.value);
        var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
        if (btnAgregarArticulo.dataset.row == -1) {
            var total = (cantidad + parseFloat(txtStock.value))
            txtStockTotal.value = total == 0 ? "0.00" : total.toFixed(2);
        } else {
            var row = parseInt(btnAgregarArticulo.dataset.row);
            var idDetalle = parseInt(tbArticulo.rows[row].dataset.id);
            if (idDetalle == 0) {
                txtStockTotal.value = parseFloat(txtStock.value) + cantidad;
            } else {
                stock -= cantidad;
                if (stock <= 0) {
                    txtStock.value = "0.00";
                    txtStockTotal.value = txtCantidad.value;
                } else {
                    txtStock.value = stock;
                    txtStockTotal.value = (stock + cantidad).toFixed(2);
                }
            }
        }
        txtCantidad.readOnly = false;
    }
}
//Carga con botones de Modal sin URL (Con datos dat[])
function cbm(ds, t, tM, tM2, cab, dat, m) {
    comboConcepto = "";
    document.getElementById("div_Frm_Modals").innerHTML = document.getElementById("div_Frm_Detalles").innerHTML;
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
    combox = ds;
}
//Carga con botones de Modal desde URL
function cbmu(ds, t, tM, tM2, cab, u, m) {
    document.getElementById("div_Frm_Modals").innerHTML = document.getElementById("div_Frm_Detalles").innerHTML;
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
    combox = ds;
}
//
function funcionModal(tr) {
    var num = tr.id.replace("numMod", "");
    var id = gbi("md" + num + "-0").innerHTML;
    if (tr.children.length == 2) {
        var value = gbi("md" + num + "-1").innerHTML;
    } else {

        var value = gbi("md" + num + "-2").innerHTML;
    }

    var value2 = gbi("md" + num + "-1").innerHTML;
    //var azx = gbi("md" + num + "-3").innerHTML;
    txtModal.value = value;
    txtModal.dataset.id = id;

    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
    switch (combox) {
        case "almacen": almacenId = id; break;
        case "tipoMovimiento": tipoMovimientoId = id; break;
        case "estadoMovimiento": estadoMovimientoId = id; break;
        case "articulo": articuloId = id; document.getElementById("txtMarca").value = gbi("md" + num + "-3").innerHTML; document.getElementById("txtUnidadMedida").value = gbi("md" + num + "-5").innerHTML;
            var url_2 = "OperacionesStock/cargarStock?idArticulo=" + articuloId + "&idAlmacenO=" + almacenId;
            enviarServidor(url_2, cargarStock); break;
    }

}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtLocalDet":
            return gbi("txtAlmacen");
            break;

    }
}
function CerrarModal(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}
//Modal cargar y grabar
function cargarLista(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        listaDatosModal = data[0].split("▼");
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
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