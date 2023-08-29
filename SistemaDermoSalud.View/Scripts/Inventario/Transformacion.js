var cabeceras = ["idMovimiento", "Local", "Almacen", "Observacion", "Fecha", "Estado"];
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

console.log("configBM");
//variables modal
var matrizModal = [];
var listaDatosModal;
var cabecera_Modal = [];
var txtModal;//input para poner el valor
var txtValor;//input para obtener el valor

//Inicializando
var Tipo = "SALIDA";//Salida
var url = "Transformacion/ObtenerDatos?Tipo=" + 1;
enviarServidor(url, mostrarLista);
configBM();
configurarBotonesModal();
reziseTabla();

var txtID = document.getElementById("txtID");
var cboLocal = document.getElementById("cboLocal");
var cboAlmacen = document.getElementById("cboAlmacen");
var cboTipoMovimiento = document.getElementById("cboTipoMovimiento");
var txtObservacion = document.getElementById("txtObservacion");
var txtFecha = document.getElementById("txtFecha");
var cboEstado = document.getElementById("cboEstado");

var tbArticulo = document.getElementById("tbArticulo");
var txtArticulo = document.getElementById("txtArticulo");
var txtMarca = document.getElementById("txtMarca");
var txtUnidadMedida = document.getElementById("txtUnidadMedida");
var txtPrecio = document.getElementById("txtPrecio");
var txtCantidad = document.getElementById("txtCantidad");
var txtStock = document.getElementById("txtStock");
var txtStockTotal = document.getElementById("txtStockTotal");
var divArticulo = document.getElementById("contentArticulo");
//datos de la entrada q sera transformacion
//var tbArticuloT = document.getElementById("tbArticulo");
var txtArticuloT = document.getElementById("txtArticuloS");
var txtMarcaT = document.getElementById("txtMarcaS");
var txtUnidadMedidaT = document.getElementById("txtUnidadMedidaS");
var txtPrecioT = document.getElementById("txtPrecioS");
var txtCantidadT = document.getElementById("txtCantidadS");
var txtStockT = document.getElementById("txtStockS");
var txtStockTotalT = document.getElementById("txtStockTotalS");
var divArticuloT = document.getElementById("contentArticuloT");
//
console.log("script");

$('#datepicker-range').datepicker({ format: 'dd-mm-yyyy', autoclose: true });
jQuery("#txtFecha").datepicker({
    format: 'dd-mm-yyyy',
    autoclose: true
});

function mostrarLista(rpta) {
    crearTablaModal(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listaLocales = listas[3].split("▼");
            listaTipoMovimiento = listas[5].split("▼");
            listaEstado = listas[6].split("▼");
            var Fecha = listas[7];
            txtFecha.value = Fecha;
            listar();
            if (listaLocales.length > 0 && listaLocales[0] != "")
                var btnModalLocalDet = document.getElementById("btnModalLocalDet");
            btnModalLocalDet.onclick = function () {
                cbm("local", "Locales", "txtLocalDet", null,
                    ["idLocal", "Descripcion"], listaLocales, cargarSinXR);
                CambioLocal();
            }
            var btnModalAlmacen = document.getElementById("btnModalAlmacen");
            btnModalAlmacen.onclick = function () {
                if (gvt("txtLocalDet") != "") {
                    cbmu("almacen", "Almacenes", "txtAlmacen", null,
                        ["idLocal", "Descripcion"], "OperacionesStock/cargarAlmacenes?idLocal=" + gbi("txtLocalDet").dataset.id, cargarLista);

                } else {
                    mostrarRespuesta("Error", "Debe Seleccionar Local", "error")
                }
            }
            console.log("hola mundo");
            if (listaTipoMovimiento.length > 0 && listaTipoMovimiento[0] != "")
                console.log(listaTipoMovimiento);
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
                    // E : enrtada
                    if (tipo[j] == "E") {
                        Estados.push(listaEstado[i]);
                    }
                }
            }
            console.log(Estados);
            if (Estados.length > 0 && Estados[0] != "") {
                var btnModalEstadoMovimiento = document.getElementById("btnModalEstadoMovimiento");
                btnModalEstadoMovimiento.onclick = function () {
                    cbm("estadoMovimiento", "Estado Movimiento", "txtEstadoMovimiento", null,
                        ["idEstado", "Descripcion"], Estados, cargarSinXR);
                }
            }

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
    var url = "Transformacion/ObtenerDatosxID/?id=" + id;
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
                mensaje = "Se adicionó la Salida de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó la Salida de Mercancia";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configBM() {

    var btnModalCategoriaS = gbi("btnModalCategoriaS");
    var Tipo = 1;
    btnModalCategoriaS.onclick = function () {
        cbmu("categoria", "Categoría", "txtCategoriaS", null,
            ["idCategoria", "Código", "Descripción"], "/Categoria/ObtenerDatosxTipo?Tipo=" + Tipo + "&Activo=A", cargarListaK);
    }
    var btnModalCategoria = gbi("btnModalCategoria");
    btnModalCategoria.onclick = function () {
        var Tipo = 1;
        cbmu("categoria", "Categoría", "txtCategoria", null,
            ["idCategoria", "Código", "Descripción"], "/Categoria/ObtenerDatosxTipo?Tipo=" + Tipo + "&Activo=A", cargarListaK);
    }

    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Transformacion/GrabarTr";
            var frm = new FormData();
            frm.append("idMovimiento", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("idLocal", gbi("txtLocalDet").dataset.id);
            frm.append("idAlmacenOrigen", gbi("txtAlmacen").dataset.id);
            frm.append("FechaMovimiento", txtFecha.value);
            frm.append("FechaMovimientoDestino", txtFecha.value);
            frm.append("Observaciones", txtObservacion.value);
            frm.append("Estado", true);
            frm.append("idTipoMovimiento", gbi("txtTipoMovimiento").dataset.id);
            frm.append("TipoMovimiento", Tipo);
            frm.append("idEstado", gbi("txtEstadoMovimiento").dataset.id);
            frm.append("idDocumento", gbi("txtVenta").value.length == 0 ? "0" : gbi("txtVenta").dataset.id);
            frm.append("Lista_Articulo", lista_Articulo());
            frm.append("Lista_Articulo2", lista_transformacion());
            swal({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);

            //ingresando a el detalle de stock  transformacion

        }
    };
}

function cargarListaK(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        console.log("cL");
        console.log(data);
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
}

function adt(v, ctrl) {
    gbi(ctrl).value = v;
}
function configurarBotonesModal() {

    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }

    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }
    //Detalle de Articulo

    var btnModalArticulo = document.getElementById("btnModalArticulo");
    btnModalArticulo.onclick = function () {


        if (gvt("txtAlmacen") != "" && gvt("txtCategoria") != "") {
            cbmu("articulo", "Articulo", "txtArticulo", null,
                ["idArticulo", "Código Barras", "Descripción", "Marca", "Categoria", "Unidad de Medida"], ' /Transformacion/cargarArticulos?idAlmacen=' + gbi("txtAlmacen").dataset.id + '&categoria=' + gbi("txtCategoria").dataset.id, cargarListaArticulo);

        } if (gvt("txtAlmacen") == "") {
            mostrarRespuesta("Alerta", "ingrese almacen", "error");
        } if (gvt("txtCategoria") == "") {
            mostrarRespuesta("Alerta", "ingrese categoria de la salida", "error");
        } if (gvt("txtAlmacen") == "" && gvt("txtCategoria") == "") {

            mostrarRespuesta("Alerta", "ingrese almacen y la categoria", "error");
        }
    }



    var btnModalArticuloS = document.getElementById("btnModalArticuloS");
    btnModalArticuloS.onclick = function () {
        if (gvt("txtAlmacen") != "" && gvt("txtCategoria") != "") {
            cbmu("articulos", "Articulos", "txtArticuloS", null,
                ["idArticulo", "Código Barras", "Descripción", "Marca", "Categoria", "Unidad de Medida"], ' /Transformacion/cargarArticulos?idAlmacen=' + gbi("txtAlmacen").dataset.id + '&categoria=' + gbi("txtCategoriaS").dataset.id, cargarListaArticuloT);

        } if (gvt("txtAlmacen") == "") {
            mostrarRespuesta("Alerta", "ingrese almacen", "error");
        } if (gvt("txtCategoriaS") == "") {
            mostrarRespuesta("Alerta", "ingrese categoria de la entrada", "error");
        } if (gvt("txtAlmacen") == "" && gvt("txtCategoria") == "") {

            mostrarRespuesta("Alerta", "ingrese almacen y la categoria", "error");
        }
    }



    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    btnAgregarArticulo.onclick = function () {
        var error = true;
        console.log("add1");
        error = validarAddArticulo();

        if (error) {
            add_ItemArticulo();
        }
    }
    var btnCrearArticulo = document.getElementById("btnCrearArticulo");
    btnCrearArticulo.onclick = function () {
        console.log("add2");
        var error = true;
        //error = validarAddArticulo();
        if (error) {
            add_ItemArticuloTransformacion();
        }
    }

    var btnCancelarArticulo = document.getElementById("btnCancelarArticulo");
    btnCancelarArticulo.onclick = function () { cancel_AddArticulo(); }

    var btnModalVenta = document.getElementById("btnModalVenta");
    btnModalVenta.onclick = function () {
        cbmu("venta", "Ventas", "txtVenta", null,
            ["idVenta", "Fecha", "Numero", "Razon Social", "Total"], ' /OperacionesStock/cargarVentas', cargarListaArticulo);
    }
}






function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == "OK") {
            console.log(listas);
            var listaCabecera = listas[2].split("▲");

            var alm = listas[3].split("▼");
            txtID.value = listaCabecera[0];
            adc(listaLocales, listaCabecera[2], "txtLocalDet", 1);
            // adc(listaTipoMovimiento, listaCabecera[8], "txtTipoMovimiento", 1);
            adc(listaEstado, listaCabecera[13], "txtEstadoMovimiento", 1);
            txtObservacion.value = listaCabecera[9];
            var fecha = listaCabecera[6].split("-");
            var fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];
            txtFecha.value = fechaStr;

            fecha = listaCabecera[7].split("-");
            fechaStr = fecha[0] + "/" + fecha[1] + "/" + fecha[2];

            var div = document.getElementById("contentArticulo");
            div.innerHTML = "";
            //Detalle de la salida
            var listaDetalle = listas[5].split("▼");
            //lista detalle del 2 movimiento entrada
            var listaDetalle2 = listas[4].split("▼");

            if (listaDetalle.length >= 1) {
                if (listaDetalle[0].trim() != "") {
                    for (var i = 0; i < listaDetalle.length; i++) {
                        addItem(1, listaDetalle[i].split("▲"));
                    }
                }
            }
            if (listaDetalle2.length >= 1) {
                if (listaDetalle2[0].trim() != "") {
                    for (var i = 0; i < listaDetalle2.length; i++) {
                        addItemTransformacion(1, listaDetalle2[i].split("▲"));
                    }
                }
            }
            adc(alm, listaCabecera[3], "txtAlmacen", 1);
            gbi("art").style.display = "none";
            //del 2do detalle
            gbi("artDetalle").style.display = "none";
            gbi("divSotckd").style.display = "none";
            gbi("divSotckTotald").style.display = "none";
            gbi("divSotck").style.display = "none";
            gbi("divSotckTotal").style.display = "none";
            gbi("art3").style.display = "none";
            gbi("art3d").style.display = "none";

            //
            gbi("btnGrabar").style.display = "none";
            gbi("btnGrabar").style.disabled = true;
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}


//
function add_ItemArticuloTransformacion() {
    console.log("entro additemartic");
    var txtArticuloS = document.getElementById("txtArticuloS");
    var txtMarcaS = document.getElementById("txtMarcaS");
    var txtUnidadMedidaS = document.getElementById("txtUnidadMedidaS");
    var txtCantidadS = document.getElementById("txtCantidadS");
    var rows = gbi("contentArticuloT").querySelectorAll(".art");
    var btnCrearArticulo = document.getElementById("btnCrearArticulo");
    var arti = rows.length + 1;
    var div = document.getElementById("contentArticuloT");

    if (btnCrearArticulo.dataset.row != undefined && btnCrearArticulo.dataset.row != -1) {//editar
        rows[(btnCrearArticulo.dataset.row * 1)].children[2].innerHTML = txtArticuloS.value.toUpperCase().trim();
        rows[(btnCrearArticulo.dataset.row * 1)].children[2].dataset.id = txtArticuloS.dataset.id;
        rows[(btnCrearArticulo.dataset.row * 1)].children[3].innerHTML = txtMarcaS.value.toUpperCase().trim();
        rows[(btnCrearArticulo.dataset.row * 1)].children[4].innerHTML = txtUnidadMedidaS.value.toUpperCase().trim();
        console.log(parseFloat(txtCantidadS.value.trim()).toFixed(2));
        rows[(btnCrearArticulo.dataset.row * 1)].children[5].innerHTML = parseFloat(txtCantidadS.value.trim()).toFixed(2);
        rows[(btnCrearArticulo.dataset.row * 1)].children[6].innerHTML = parseFloat(txtPrecioS.value.trim()).toFixed(2);
    } else if (btnCrearArticulo.dataset.row != undefined && btnCrearArticulo.dataset.row != -1) {
    } else {//nuevo
        var estaAgregado = false;
        console.log();
        for (var i = 0; i < rows.length; i++) {
            if (txtArticuloS.dataset.id == rows[i].children[2].dataset.id) {
                console.log("fefe");
                console.log(rows);
                var cantidad = parseFloat(rows[i].children[5].innerHTML);
                rows[i].children[5].innerHTML = (cantidad + parseFloat(txtCantidadS.value));
                estaAgregado = true;
                break;
            }
        }
        if (!estaAgregado) {
            var cadena = "<div class='art row panel salt' data-id='0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
            cadena += "<div class='col-12 col-md-1' style='display:none' data-id='" + (rows.length + 1) + "'>" + (rows.length + 1) + "</div>";
            cadena += "<div class='col-12 col-md-1'>" + (rows.length + 1) + "</div>";
            cadena += "<div class='col-12 col-md-3' data-id='" + txtArticuloS.dataset.id + "'>" + txtArticuloS.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtMarcaS.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtUnidadMedidaS.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtCantidadS.value + "</div>";
            cadena += "<div class='col-12 col-md-1' style='display:none'>0</div>";
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
    cancel_AddArticuloTransformacion();
}
//






function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nueva Salida de Mercancia";//Titulo Insertar
            gbi("art").style.display = "";
            // gbi("art2").style.display = "";
            gbi("art3").style.display = "";
            gbi("btnGrabar").style.display = "";
            gbi("btnGrabar").style.disabled = true;
            gbi("artDetalle").style.display = "";
            gbi("divSotckd").style.display = "";
            gbi("divSotckTotald").style.display = "";
            gbi("divSotck").style.display = "";
            gbi("divSotckTotal").style.display = "";
            gbi("art3d").style.display = "";

            break;
        case 2:
            lblTituloPanel.innerHTML = "Visualizar transformacion";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            break;
    }
}
function limpiarTodo() {
    var contentArticulo = gbi("contentArticulo");
    var contentArticuloT = gbi("contentArticuloT");
    limpiarControl("txtID");
    limpiarControl("txtLocalDet");
    limpiarControl("txtAlmacen");
    limpiarControl("txtTipoMovimiento");
    limpiarControl("txtEstadoMovimiento");
    limpiarControl("txtObservacion");
    limpiarControl("txtFecha");
    limpiarControl("txtVenta");
    limpiarControl("txtCategoria");
    limpiarControl("txtCategoriaS");

    // limpiarControl("txtGuia");
    var fec = new Date().toLocaleDateString();
    var fecha;
    if (fec.includes("/")) { fecha = fec.split("/"); }
    if (fec.includes("-")) { fecha = fec.split("-"); }
    var fechaStr = (fecha[0] * 1 < 10 ? "0" + fecha[0] : fecha[0]) + "/" + (fecha[1] * 1 < 10 ? "0" + fecha[1] : fecha[1]) + "/" + fecha[2];
    txtFecha.value = fechaStr;
    cancel_AddArticulo();
    cancel_AddArticuloTransformacion();
    contentArticulo.innerHTML = "";
    contentArticuloT.innerHTML = "";
}
function BuscarxFecha(f1, f2) {
    var url = 'OperacionesStock/ObtenerPorFecha?Tipo=' + Tipo + '&fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
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
function validarFormulario() {
    var rows = gbi("contentArticulo").querySelectorAll(".art");
    var error = true;
    if (validarControl("txtObservacion")) error = false;
    if (validarControl("txtFecha")) error = false;
    if (error && rows.length == 0) {
        error = false;
        swal("Error", "Debe agregar articulos al detalle", "error")
    }
    return error;
}
function eliminar(id) {
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
                var url = "Transformacion/Eliminar?id=" + id;
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
            mensaje = "Se eliminó la Salida de Mercancia";
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
                if (gvt("txtNroDocumento") != "") {
                    cbmu("almacen", "Almacen", "txtAlmacen", null,
                        ["idAlmacen", "Descripción"], "/OperacionesStock/cargarAlmacenes?idlocal=" + gbi("txtLocalDet").dataset.id, cargarLista);

                } else {
                    mostrarRespuesta("Error", "Debe Seleccionar Local", "error");
                }
            }
        } else {
            txtAlmacen.value = "";
            //divArticulo.innerHTML = "";
        }
    }
    cancel_AddArticulo();
    cancel_AddArticuloTransformacion();
}


btnModalAlmacen.onclick = function () {
    if (contentArticulo.length > 0) {
        swal({
            title: "Desea cambiar el Almacen?", text: "Si cambia de Almacen se perderan los Artículos agregados!", type: "warning",
            showCancelButton: true,
            confirmButtonText: "Si, cambiar de Almacen!",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                //cboAlmacen.dataset.id = cboAlmacen.value;
                tbArticulo.innerHTML = "";
                swal.close();
            } else {
                //cboAlmacen.value = cboAlmacen.dataset.id;
            }
        });
    } else {
        this.dataset.id = this.value;
    }
    cancel_AddArticulo();
}

txtCantidadS.onkeyup = function () {
    if (this.value == "") { this.value = "0.00"; }
    var stock = parseFloat(txtStockS.value);
    txtStockTotalS.value = stock + parseFloat(this.value);
}


txtCantidad.onkeyup = function () {
    if (this.value == "") { this.value = "0.00"; }
    var stock = parseFloat(txtStock.value);
    txtStockTotal.value = stock - parseFloat(this.value);
}

txtCantidad.onfocus = function () { this.select(); }
txtPrecio.onfocus = function () { this.select(); }
function MostrarxTipoMovimiento() {
    var nroVenta = document.getElementById("TVenta");
    var tipoMovimiento = document.getElementById("txtTipoMovimiento").dataset.id;
    switch (tipoMovimiento) {
        case "10": nroVenta.style.display = ""; break;
        default: nroVenta.style.display = "none";
    }
}

function CargarDetalleVenta(rpta) {
    var div = document.getElementById("contentArticulo");
    div.innerHTML = "";
    if (rpta != '') {
        console.log(rpta);
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲'); //lista Requerimientos
        var det = listas[2].split("▼");//lista Requerimiento Detalle        
        if (Resultado == 'OK') {
            for (var i = 0; i < det.length; i++) {
                var itemDet = det[i].split("▲");
                var cadena = "<div class='art row panel salt' data-id='0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
                cadena += "<div class='col-12 col-md-1' style='display:none' data-id='" + (i + 1) + "'>" + (i + 1) + "</div>";
                cadena += "<div class='col-12 col-md-1'>" + (i + 1) + "</div>";
                cadena += "<div class='col-12 col-md-3' data-id='" + itemDet[0] + "'>" + itemDet[1] + "</div>";
                cadena += "<div class='col-12 col-md-2'>" + itemDet[2] + "</div>";
                cadena += "<div class='col-12 col-md-2'>" + itemDet[3] + "</div>";
                cadena += "<div class='col-12 col-md-2'>" + itemDet[4] + "</div>";
                cadena += "<div class='col-12 col-md-2'>" + itemDet[5] + "</div>";
                cadena += "<div class='col-12 col-md-2' style='display:none'>" + itemDet[6] + "</div>";
                cadena += "<div class='col-12 col-md-1'>";
                cadena += "<div class='row saltbtn'>";
                cadena += "<div class='col-12'>";
                cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' onclick='borrarDetalle(this)'> <i class='fa fa-trash-o fs-11'></i> </button>";
                cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalle(2, \"" + (i + 1) + "\")'> <i class='fa fa-pencil fs-11'></i></button>";
                cadena += "</div>";
                cadena += "</div>";
                cadena += "</div>";
                cadena += "</div>";
                div.innerHTML += cadena;
            }
        }
    }
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
//Detalle Articulo
function add_ItemArticulo() {
    console.log("entro add art 1");
    var txtArticulo = document.getElementById("txtArticulo");
    var txtMarca = document.getElementById("txtMarca");
    var txtUnidadMedida = document.getElementById("txtUnidadMedida");
    var txtCantidad = document.getElementById("txtCantidad");
    var txtPrecio = document.getElementById("txtPrecio");
    var rows = gbi("contentArticulo").querySelectorAll(".art");
    var btnAgregarArticulo = document.getElementById("btnAgregarArticulo");
    var arti = rows.length + 1;
    var div = document.getElementById("contentArticulo");

    if (btnAgregarArticulo.dataset.row != undefined && btnAgregarArticulo.dataset.row != -1) {//editar
        rows[(btnAgregarArticulo.dataset.row * 1)].children[2].innerHTML = txtArticulo.value.toUpperCase().trim();
        rows[(btnAgregarArticulo.dataset.row * 1)].children[2].dataset.id = txtArticulo.dataset.id;
        rows[(btnAgregarArticulo.dataset.row * 1)].children[3].innerHTML = txtMarca.value.toUpperCase().trim();
        rows[(btnAgregarArticulo.dataset.row * 1)].children[4].innerHTML = txtUnidadMedida.value.toUpperCase().trim();
        rows[(btnAgregarArticulo.dataset.row * 1)].children[5].innerHTML = parseFloat(txtCantidad.value.trim()).toFixed(2);
        rows[(btnAgregarArticulo.dataset.row * 1)].children[6].innerHTML = parseFloat(txtPrecio.value.trim()).toFixed(2);
    } else {//nuevo
        var estaAgregado = false;
        for (var i = 0; i < rows.length; i++) {
            if (txtArticulo.dataset.id == rows[i].children[2].dataset.id) {
                var cantidad = parseFloat(rows[i].children[5].innerHTML);
                rows[i].children[5].innerHTML = (cantidad + parseFloat(txtCantidad.value));
                estaAgregado = true;
                break;
            }
        }
        if (!estaAgregado) {
            var cadena = "<div class='art row panel salt' data-id='0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
            cadena += "<div class='col-12 col-md-1' style='display:none' data-id='" + (rows.length + 1) + "'>" + (rows.length + 1) + "</div>";
            cadena += "<div class='col-12 col-md-1'>" + (rows.length + 1) + "</div>";
            cadena += "<div class='col-12 col-md-3' data-id='" + txtArticulo.dataset.id + "'>" + txtArticulo.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtMarca.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtUnidadMedida.value + "</div>";
            cadena += "<div class='col-12 col-md-2'>" + txtCantidad.value + "</div>";
            cadena += "<div class='col-12 col-md-1' style='display:none'>0</div>";
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
            //var cadena = "<tr data-id='0'>";
            //cadena += "<td style='text-align: center;'><span class='fa fa-pencil' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEditar
            //cadena += "<td style='text-align: center;'><span class='fa fa-trash-o' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEliminar
            //cadena += "<td>" + (tbArticulo.rows.length + 1) + "</td>";//item
            //cadena += "<td data-id='" + txtArticulo.dataset.id + "'>" + txtArticulo.value.trim().toUpperCase() + "</td>";//Articulo
            //cadena += "<td>" + txtMarca.value.trim().toUpperCase() + "</td>";//Marca
            //cadena += "<td>" + txtUnidadMedida.value.trim().toUpperCase() + "</td>";//Unidad de Medida
            //cadena += "<td>" + parseFloat(txtCantidad.value).toFixed(2) + "</td>";//Cantidad
            //cadena += "</tr>";
            //tbArticulo.innerHTML += cadena;
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
    txtPrecio.value = "0.00";
    btnAgregarArticulo.innerHTML = "Agregar";
    //
    limpiarControl("txtArticuloS");
    limpiarControl("txtMarcaS");
    limpiarControl("txtUnidadMedidaS");
    txtCantidadS.value = "0.00";
    txtStockS.value = "0.00";
    txtStockTotalS.value = "0.00";
    txtPrecioS.value = "0.00";
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
    console.log("probando lista articulo");
    console.log(rows);

    for (var i = 0; i < rows.length; i++) {
        console.log(rows[i]);
        lista += rows[i].dataset.id + "|";//idMovimientoDetalle
        lista += "0|";//idMovimiento
        lista += rows[i].children[2].dataset.id + "|";//idArticulo
        lista += (i + 1) + "|";//Item
        lista += rows[i].children[5].innerHTML + "|0|0|";//Cantidad | Cantidad Recibida
        lista += "||0|0|1|0";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
        //lista += rows[i].children[7].innerHTML;
        console.log(rows[i].children[7]);
        lista += "¯";
    } console.log("ingresando ala Lista Articulos");
    console.log(lista);
    lista = lista.substring(0, lista.length - 1);
    return lista;
}


function lista_transformacion() {
    var rows = gbi("contentArticuloT").querySelectorAll(".art");
    var lista = "";
    console.log("probando lista articulo");
    console.log(rows);
    for (var i = 0; i < rows.length; i++) {
        console.log(rows);
        console.log(rows[i]);
        lista += rows[i].dataset.id + "|";//idMovimientoDetalle
        lista += "0|";//idMovimiento
        lista += rows[i].children[2].dataset.id + "|";//idArticulo
        lista += (i + 1) + "|";//Item
        lista += rows[i].children[5].innerHTML + "|0|0|";//Cantidad | Cantidad Recibida
        lista += "||0|0|1|0";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
        // lista += rows[i].children[7].innerHTML;
        lista += "¯";

    } console.log("ingresando ala Lista Articulos");
    console.log(lista);
    lista = lista.substring(0, lista.length - 1);
    return lista;
}

function cancel_AddArticuloTransformacion() {
    var btnCrearArticulo = document.getElementById("btnCrearArticulo");

    btnCrearArticulo.dataset.row = -1;

}



function cargarDetalleArticulo(lista) {
    var rows = gbi("contentArticulo").querySelectorAll(".art");
    if (lista.length > 0 && lista[0] != "") {
        for (var i = 0; i < lista.length; i++) {
            var item = lista[i].split("▲");
            console.log(item);
            add_ItemArticulo();
            rows[i].dataset.id = item[0];
            rows[i].children[2].dataset.id = item[1];
            rows[i].children[2].innerHTML = item[2];
            rows[i].children[3].innerHTML = item[3];
            rows[i].children[4].innerHTML = item[4];
            rows[i].children[5].innerHTML = item[5];
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
        txtPrecio.readOnly = false;
    }
}
function cargarStockT(rpta) {
    if (rpta != "") {
        console.log("ingreso al cargarStoc2");
        var stockS = rpta.split("-");
        var stockS = parseFloat(stockS[0]);
        txtStockS.value = stockS.toFixed(2);
        if (txtCantidadS.value == "") { txtCantidadS.value = "0.00"; }
        var cantidadS = parseFloat(txtCantidadS.value);
        var btnCrearArticulo = document.getElementById("btnCrearArticulo");
        if (btnCrearArticulo.dataset.row == -1) {
            var total = (cantidadS + parseFloat(txtStockS.value))
            txtStockTotalS.value = total == 0 ? "0.00" : total.toFixed(2);
        } else {
            var row = parseInt(btnCrearArticulo.dataset.row);
            var idDetalle = parseInt(tbArticulo.rows[row].dataset.id);
            if (idDetalle == 0) {
                txtStockTotalS.value = parseFloat(txtStockS.value) + cantidadS;
            } else {
                stockS -= cantidadS;
                if (stockS <= 0) {
                    txtStockS.value = "0.00";
                    txtStockTotalS.value = txtCantidadS.value;
                } else {
                    txtStock.value = stockS;
                    txtStockTotal.value = (stockS + cantidadS).toFixed(2);
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
        case "tipoMovimiento": tipoMovimientoId = id; MostrarxTipoMovimiento(); break;
        case "estadoMovimiento": estadoMovimientoId = id; break;
        case "articulo": articuloId = id; document.getElementById("txtMarca").value = gbi("md" + num + "-3").innerHTML; document.getElementById("txtUnidadMedida").value = gbi("md" + num + "-5").innerHTML;
            var url_2 = "OperacionesStock/cargarStock?idArticulo=" + articuloId + "&idAlmacenO=" + almacenId;
            enviarServidor(url_2, cargarStock); break;
        case "articulos": articuloId = id; document.getElementById("txtMarca").value = gbi("md" + num + "-3").innerHTML; document.getElementById("txtUnidadMedidaS").value = gbi("md" + num + "-5").innerHTML;
            var url_3 = "OperacionesStock/cargarStock?idArticulo=" + articuloId + "&idAlmacenO=" + almacenId;
            enviarServidor(url_3, cargarStockT); break;
    }

}


function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtLocalDet":
            return gbi("txtAlmacen");
            break;
        case "txtVenta":
            var idVenta = gbi("txtVenta").dataset.id;
            var url = 'OperacionesStock/cargarDetalleVentas?idVentas=' + idVenta;
            enviarServidor(url, CargarDetalleVenta);
            break;
        case "txtArticuloS": var url_T = "OperacionesStock/cargarStock?idArticulo=" + gbi("txtArticuloS").dataset.id; + "&idAlmacenO=" + gbi("txtAlmacen").dataset.id;;
            enviarServidor(url_T, cargarStockT); break;
        case "txtTipoMovimiento":
            var tipoMov = gbi("txtTipoMovimiento").dataset.id;
            var p;
            switch (tipoMov) {
                case "10": p = "txtVenta"; gbi("txtPrecio").value = 0; gbi("txtPrecio").disabled = true; break;
                default: p = "txtEstadoMovimiento"; gbi("txtPrecio").disabled = true; break;
            }
            return gbi(p);
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
function cargarListaArticuloT(rpta) {
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

//Al editar
function addItem(tipo, data) {
    var contenido = "";
    console.log(data);
    var div = document.getElementById("contentArticulo");
    var cadena = "<div class='art row panel salt' data-id='0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
    cadena += "<div class='col-12 col-md-1' style='display:none' data-id='" + (document.getElementsByClassName("art").length + 1) + "'>" + (document.getElementsByClassName("art").length + 1) + "</div>";
    cadena += "<div class='col-12 col-md-1'>" + (document.getElementsByClassName("art").length + 1) + "</div>";
    cadena += "<div class='col-12 col-md-3' data-id=''>" + (tipo == 1 ? data[2] : gvt("txtArticulo")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[3] : gvt("txtMarca")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[4] : gvt("txtUnidadMedida")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[5] : gvt("txtCantidad")) + "</div>";
    cadena += "<div class='col-12 col-md-1' style='display:none'>0</div>";
    cadena += "<div class='row saltbtn' style='display:none'>";
    cadena += "<div class='col-12'>";
    cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' > <i class='fa fa-trash-o fs-11'></i> </button>";
    cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' > <i class='fa fa-pencil fs-11'></i></button>";
    cadena += "</div>";
    cadena += "</div>";
    cadena += "</div>";
    cadena += "</div>";
    div.innerHTML += cadena;
}
//
function addItemTransformacion(tipo, data) {
    var contenido = "";
    console.log(data);
    var div = document.getElementById("contentArticuloT");
    var cadena = "<div class='art row panel salt' data-id='0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
    cadena += "<div class='col-12 col-md-1' style='display:none' data-id='" + (document.getElementsByClassName("art").length + 1) + "'>" + (document.getElementsByClassName("art").length + 1) + "</div>";
    cadena += "<div class='col-12 col-md-1'>" + (document.getElementsByClassName("art").length + 1) + "</div>";
    cadena += "<div class='col-12 col-md-3' data-id=''>" + (tipo == 1 ? data[2] : gvt("txtArticulo")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[3] : gvt("txtMarca")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[4] : gvt("txtUnidadMedida")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[5] : gvt("txtCantidad")) + "</div>";
    cadena += "<div class='col-12 col-md-1' style='display:none'>0</div>";
    cadena += "<div class='row saltbtn' style='display:none'>";
    cadena += "<div class='col-12'>";
    cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' > <i class='fa fa-trash-o fs-11'></i> </button>";
    cadena += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' > <i class='fa fa-pencil fs-11'></i></button>";
    cadena += "</div>";
    cadena += "</div>";
    cadena += "</div>";
    cadena += "</div>";
    console.log(cadena);
    div.innerHTML += cadena;
}



