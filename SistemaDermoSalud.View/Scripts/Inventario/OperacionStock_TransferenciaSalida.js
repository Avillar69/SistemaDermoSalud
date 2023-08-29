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

//Inicializando
var Tipo = "TRANSFERENCIA_SALIDA";//Transferencia
var url = "OperacionesStock/ObtenerDatos?Tipo=" + Tipo;
enviarServidor(url, mostrarLista);
configurarBotonesModal();
reziseTabla();

var txtID = document.getElementById("txtID");
var cboLocal = document.getElementById("cboLocal");
var cboAlmacen = document.getElementById("cboAlmacen");
var cboAlmacenDestino = document.getElementById("cboAlmacenDestino");
var cboTipoMovimiento = document.getElementById("cboTipoMovimiento");
var txtObservacion = document.getElementById("txtObservacion");
var txtFecha = document.getElementById("txtFecha");
var cboEstado = document.getElementById("cboEstado");

var tbArticulo = document.getElementById("tbArticulo");
var txtArticulo = document.getElementById("txtArticulo");
var txtMarca = document.getElementById("txtMarca");
var txtUnidadMedida = document.getElementById("txtUnidadMedida");
var txtCantidad = document.getElementById("txtCantidad");
var txtStock = document.getElementById("txtStock");
var txtStockTotal = document.getElementById("txtStockTotal");

console.log("script");
jQuery("#txtFecha").datepicker({ format: "dd/mm/yyyy" });

function mostrarLista(rpta) {
    crearTabla(cabeceras);
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            listaDatos = listas[2].split("▼");
            var listaLocales = listas[3].split("▼");
            var listaAlmacen = listas[4].split("▼");
            var listaTipoMovimiento = listas[5].split("▼");
            var listaEstado = listas[6].split("▼");
            if (listaLocales.length > 0 && listaLocales[0] != "")
                llenarCombo(listaLocales, "cboLocal", "Seleccione");
            if (listaAlmacen.length > 0 && listaAlmacen[0] != "")
                llenarCombo(listaAlmacen, "cboAlmacenDestino", "Seleccione");
            if (listaTipoMovimiento.length > 0 && listaTipoMovimiento[0] != "")
                llenarCombo(listaTipoMovimiento, "cboTipoMovimiento");

            var Estados = [];
            for (var i = 0; i < listaEstado.length; i++) {
                var item = listaEstado[i].split("▲");
                var tipo = item[2].split("-");
                for (var j = 0; j < tipo.length; j++) {
                    // TS : Transferencia Salida
                    if (tipo[j] == "TS") {
                        Estados.push(listaEstado[i]);
                    }
                }
            }
            if (Estados.length > 0 && Estados[0] != "")
                llenarCombo(Estados, "cboEstado");
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
                mensaje = "Se adicionó la Transferencia de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó la Transferencia de Mercancia";
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
            var url = "OperacionesStock/Grabar";
            var frm = new FormData();
            frm.append("idMovimiento", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("idLocal", cboLocal.value);
            frm.append("idAlmacenOrigen", cboAlmacen.value);
            frm.append("idAlmacenDestino", cboAlmacenDestino.value);
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
    var btnModalArticulo = document.getElementById("btnModalArticulo");
    btnModalArticulo.onclick = function () {
        var error = true;
        if (validarControl("cboAlmacen")) { error = false; }
        if (validarControl("cboAlmacenDestino")) { error = false; }
        if (error) {
            document.getElementById("div_Frm_Modal").innerHTML = "";
            document.getElementById("btnGrabar_Modal").dataset.grabar = "articulo";
            document.getElementById("lblTituloModal").innerHTML = "Artículos";
            //ocultar boton grabar
            document.getElementById("btnGrabar_Modal").style.display = "none";

            txtModal = txtArticulo;
            cabecera_Modal = ["idArticulo", "Código Barras", "Descripción", "Marca", "Categoria", "Unidad de Medida"];
            var url = '/OperacionesStock/cargarArticulos?idAlmacen=' + cboAlmacen.value;

            enviarServidor(url, cargarLista);
        }
    }
    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    btnAgregarArticulo.onclick = function () {
        var error = true;
        //error = validarAddArticulo();
        //if (error) {
        add_ItemArticulo();
        //}
    }

    var btnCancelarArticulo = document.getElementById("btnCancelarArticulo");
    btnCancelarArticulo.onclick = function () { cancel_AddArticulo(); }

    var tbArticulo = document.getElementById("tbArticulo");
    tbArticulo.onclick = function (e) {
        if (e.target.tagName == "SPAN") {
            var tr = e.target.parentElement.parentElement;
            if (e.target.className.includes("pencil")) {
                txtArticulo.value = tr.cells[3].innerHTML;
                txtArticulo.dataset.id = tr.cells[3].dataset.id;
                txtMarca.value = tr.cells[4].innerHTML;
                txtUnidadMedida.value = tr.cells[5].innerHTML;
                txtCantidad.value = tr.cells[6].innerHTML;

                btnAgregarArticulo.dataset.row = tr.rowIndex - 1;
                btnAgregarArticulo.innerHTML = "Editar";
                btnCancelarArticulo.style.visibility = "visible";

                var url_2 = "OperacionesStock/cargarStock?idArticulo=" + tr.cells[3].dataset.id + "&idAlmacenO=" + cboAlmacen.value;
                enviarServidor(url_2, cargarStock);
            } else {
                cancel_AddArticulo();
                tbArticulo.removeChild(tr);
                for (var i = 0; i < tbArticulo.rows.length; i++) {
                    tbArticulo.rows[i].cells[2].innerHTML = (i + 1);
                }
            }
        }
    }
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            var listaCabecera = listas[2].split("▲");
            var listaAlmacen = listas[3].split("▼");
            txtID.value = listaCabecera[0];
            cboLocal.value = listaCabecera[2] == "0" ? "" : listaCabecera[2];
            cboLocal.dataset.id = listaCabecera[2] == "0" ? "" : listaCabecera[2];
            llenarCombo(listaAlmacen, "cboAlmacen", "Seleccione");
            cboAlmacen.value = listaCabecera[3] == "0" ? "" : listaCabecera[3];
            cboAlmacen.dataset.id = listaCabecera[3] == "0" ? "" : listaCabecera[3];
            cboAlmacenDestino.value = listaCabecera[4] == "0" ? "" : listaCabecera[4];
            txtObservacion.value = listaCabecera[9];
            cboEstado.value = listaCabecera[13];
            var fecha = listaCabecera[6].split("-");
            var fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];
            txtFecha.value = fechaStr;

            fecha = listaCabecera[7].split("-");
            fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];

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
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nueva Transferencia de Salida Mercancia";//Titulo Insertar
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Transferencia de Salida Mercancia";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            break;
    }
}
function limpiarTodo() {
    //limpiarControl("txtID");
    //limpiarControl("cboLocal");
    //limpiarControl("cboAlmacen");
    //limpiarControl("cboAlmacenDestino");
    //limpiarControl("txtObservacion");
    //limpiarControl("txtFecha");
    //var fec = new Date().toLocaleDateString();
    //var fecha;
    //if (fec.includes("/")) { fecha = fec.split("/"); }
    //if (fec.includes("-")) { fecha = fec.split("-"); }
    //var fechaStr = (fecha[0] * 1 < 10 ? "0" + fecha[0] : fecha[0]) + "/" + (fecha[1] * 1 < 10 ? "0" + fecha[1] : fecha[1]) + "/" + fecha[2];
    //txtFecha.value = fechaStr;
    //cancel_AddArticulo();
    //tbArticulo.innerHTML = "";
    //cboAlmacen.innerHTML = "";

    //for (var i = 0; i < cboEstado.options.length; i++) {
    //    if (cboEstado.options[i].text.toUpperCase() == "PENDIENTE") {
    //        cboEstado.value = cboEstado.options[i].value;
    //        break;
    //    }
    //}
}
function validarFormulario() {
    var error = true;
    if (validarControl("cboLocal")) error = false;
    if (validarControl("cboAlmacen")) error = false;
    if (validarControl("cboAlmacenDestino")) error = false;
    if (validarControl("txtObservacion")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (cboAlmacenDestino.value == cboAlmacen.value) {
        cboAlmacenDestino.value = "";
        validarControl("cboAlmacenDestino");
        error = false;
        swal("Error", "El almacen Destino debe ser diferente al de Origen", "error");
    }
    if (error && tbArticulo.rows.length == 0) {
        error = false;
        swal("Error", "Debe agregar articulos al detalle", "error");
    }
    if (cboEstado.selectedOptions[0].text != "PENDIENTE") {
        error = false;
        swal("Info", "La transferencia esta En Proceso, no puede modificar los datos", "info");
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

cboLocal.onchange = function () {
    //cargar almacenenes
    if (tbArticulo.rows.length > 0) {
        swal({
            title: "Desea cambiar el Local?", text: "Si cambia de Local se perderan los Artículos agregados!", type: "warning",
            showCancelButton: true,
            confirmButtonText: "Si, cambiar de Local!",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                cboLocal.dataset.id = cboLocal.value;
                postLocales();
                swal.close();
            } else {
                cboLocal.value = cboLocal.dataset.id;
            }
        });
    } else {
        this.dataset.id = this.value;
        postLocales();
    }
    function postLocales() {
        if (cboLocal.value != "") {
            tbArticulo.innerHTML = "";
            var url = "OperacionesStock/cargarAlmacenes?idlocal=" + cboLocal.value;
            enviarServidor(url, cargarAlmacenes);
        } else {
            cboAlmacen.innerHTML = "";
            tbArticulo.innerHTML = "";
        }
    }
    cancel_AddArticulo();
}
function cargarAlmacenes(rpta) {
    if (rpta != "") {
        var lista = rpta.split("▼");
        llenarCombo(lista, "cboAlmacen", "Seleccione")
    }
}
cboAlmacen.onchange = function () {
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
    cancel_AddArticulo();
}
txtCantidad.onkeyup = function () {
    if (this.value == "") { this.value = "0.00"; }
    var stock = parseFloat(txtStock.value);
    var total = stock - parseFloat(this.value);
    if (total < 0) {
        txtCantidad.value = stock;
        txtStockTotal.value = stock.toFixed(2);
    } else {
        txtStockTotal.value = total.toFixed(2);
    }
}
txtCantidad.onfocus = function () { this.select(); }

//Detalle Articulo
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
    //estas ingresando incorrectamente los datos el doc referencia esta hasta el final y en el JS lo pones al inicio -intermedio
    var rows = gbi("contentArticulo").querySelectorAll(".art");
    var lista = "";
    console.log("entrando a rows  lista Articulo");
    console.log(rows);
    for (var i = 0; i < rows.length; i++) {
        lista += rows[i].dataset.id + "|";//idMovimientoDetalle
        lista += "0|";//idMovimiento
        lista += rows[i].children[1].dataset.id + "|";//idArticulo
        lista += (i + 1) + "|";//Item
        lista += rows[i].children[5].innerHTML + "|0|";//Cantidad | Cantidad Recibida
        lista += "||0|1|0";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
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

function cargarStock(rpta) {
    if (rpta != "") {
        var stock = rpta.split("-");
        var stock = parseFloat(stock[0]);
        txtStock.value = stock.toFixed(2);
        if (txtCantidad.value == "") { txtCantidad.value = "0.00"; }
        var cantidad = parseFloat(txtCantidad.value);
        var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
        if (btnAgregarArticulo.dataset.row == -1) {
            var total = (cantidad + parseFloat(txtStock.value));
            txtStockTotal.value = total == 0 ? "0.00" : total.toFixed(2);
        } else {
            var row = parseInt(btnAgregarArticulo.dataset.row);
            var idDetalle = parseInt(tbArticulo.rows[row].dataset.id);
            if (idDetalle == 0) {
                txtStockTotal.value = (parseFloat(txtStock.value) - cantidad).toFixed(2);
            } else {
                //stock += cantidad;
                if (stock <= 0) {
                    txtStock.value = "0.00";
                    txtStockTotal.value = txtCantidad.value;
                } else {
                    txtStock.value = stock;
                    txtStockTotal.value = (stock - cantidad).toFixed(2);
                }
            }
        }
    }
}