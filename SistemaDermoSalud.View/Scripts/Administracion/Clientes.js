var cabeceras = ["idSocioNegocio", "Nº Documento", "Razon Social", "Fecha Modificacion", "Estado"];
var listaDatos;
var matriz = [];
//variables modal
var matrizModal = [];
var listaDatosModal;
var cabecera_Modal = [];
var txtModal;//input para poner el valor
var txtValor;//input para obtener el valor
//idUsuario
var idUsuario;
configBM();
//Inicializando
var url = "/SocioNegocio/ObtenerDatosCliente";
enviarServidor(url, mostrarLista);
configurarBotonesModal();
reziseTabla();
var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var cboTipoPersona = document.getElementById("cboTipoPersona");
var txtRazonSocial = document.getElementById("txtRazonSocial");
var cboTipoDocumento = document.getElementById("cboTipoDocumento");
var txtNroDocumento = document.getElementById("txtNroDocumento");
var cboPais = document.getElementById("cboPais");
var cboDepartamento = document.getElementById("cboDepartamento");
var cboProvincia = document.getElementById("cboProvincia");
var cboDistrito = document.getElementById("cboDistrito");
var txtWeb = document.getElementById("txtWeb");
var txtMail = document.getElementById("txtMail");
var chkCliente = document.getElementById("chkCliente");
var chkProveedor = document.getElementById("chkProveedor");
var chkActivo = document.getElementById("chkActivo");

//inputs TAB Contacto

var txtNombre = document.getElementById("txtNombre");
var txtCargo = document.getElementById("txtCargo");
var txtTelefonoContacto = document.getElementById("txtTelefonoContacto");
var txtMailContacto = document.getElementById("txtMailContacto");
//inputs TAB Direccion
var tbDireccion = document.getElementById("tbDireccion");
var txtDireccion = document.getElementById("txtDireccion");
var chkDirPrincipal = document.getElementById("chkDirPrincipal");
//inputs TAB Telefono
var tbTelefono = document.getElementById("tbTelefono");
var txtTelefono = document.getElementById("txtTelefono");
//inputs TAB Cuenta Banco
var tbCuenta = document.getElementById("tbCuenta");
var txtBanco = document.getElementById("txtBanco");
var txtCuenta = document.getElementById("txtCuenta");
var txtCuentaDescripcion = document.getElementById("txtCuentaDescripcion");
var cboMoneda = document.getElementById("cboMoneda");

//ListasGlobales
var listaDepartamento;
var listaProvincia;
var listaDistrito;
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function cfgTMKP(evt) {
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        var n = o.replace("txt", "");
        gbi("btnModal" + n).click();
    }
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {

        case "txtBanco":
            return gbi("txtCuenta");
            break;

        default:
            break;
    }
}
function CerrarModalR(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}
function cargarImagen(data64, imgCapcha) {
    if (data64 == "") {
        var contenido = '<label class="label label-alert label-sm">Sin respuesta</label>';
        document.getElementById(imgCapcha).innerHTML = contenido;
    } else {
        var contenido = '<img style="height:30px;" src="data:image/jpeg;base64,' + data64 + '"/>';
        document.getElementById(imgCapcha).innerHTML = contenido;
    }
}
function mostrarLista(rpta) {
    crearTablaModal(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        listaDatos = listas[2].split("▼");
        var listaPais = listas[3].split("▼");
        var listaTipoPersona = listas[4].split("▼");
        var listaTipoDocumento = listas[5].split("▼");
        var listaMoneda = listas[6].split("▼");
        idUsuario = listas[7];
        listaDepartamento = listas[8].split("▼");
        listaProvincia = listas[9].split("▼");
        listaDistrito = listas[10].split("▼");
        llenarCombo(listaPais, "cboPais", "Seleccione");
        llenarCombo(listaTipoPersona, "cboTipoPersona", "Seleccione");
        llenarCombo(listaTipoDocumento, "cboTipoDocumento", "Seleccione");
        llenarCombo(listaMoneda, "cboMoneda", "Seleccione");
        llenarCombo(listaDepartamento, "cboDepartamento", "Seleccione");
        listar();
    }
}
function listar() {
    crearMatriz(listaDatos); configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    configurarBotonesModal();
    configurarUbigeo();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function CargarProvincias() {
    var listaProvFilt = [];
    for (var i = 0; i < listaProvincia.length; i++) {
        if (listaProvincia[i].split('▲')[0].indexOf(cboDepartamento.value) != -1) {
            listaProvFilt.push(listaProvincia[i]);
        }
    }
    llenarCombo(listaProvFilt, "cboProvincia", "Seleccione");
}
function CargarDistritos() {
    var listaDistFilt = [];
    for (var i = 0; i < listaDistrito.length; i++) {
        if (listaDistrito[i].split('▲')[0].indexOf(cboProvincia.value) != -1) {
            listaDistFilt.push(listaDistrito[i]);
        }
    }
    llenarCombo(listaDistFilt, "cboDistrito", "Seleccione");
}
function configurarUbigeo() {

    cboDepartamento.onchange = function () {
        if (cboDepartamento.value == "") {
            cboProvincia.innerHTML = "<option>Seleccione</option>";
            cboDistrito.innerHTML = "<option>Seleccione</option>";
        }
        else {
            CargarProvincias();
        }
    }

    cboProvincia.onchange = function () {
        if (cboProvincia.value == "") {
            cboDistrito.innerHTML = "<option>Seleccione</option>";
        }
        else {
            CargarDistritos();
        }
    }
}
function TraerDetalle(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    var url = "/SocioNegocio/ObtenerDatosxID/?id=" + id;
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
                mensaje = "Se adicionó el Socio de Negocio";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Socio de Negocio";
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

var btnPDF = gbi("btnImprimirPDF");
btnPDF.onclick = function () {
    ExportarPDF("p", "Clientes", cabeceras, matriz, "Clientes", "a4");
}
var btnExcel = gbi("btnImprimirExcel");
btnExcel.onclick = function () {
    fnExcelReport(cabeceras, matriz);
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "/SocioNegocio/GrabarCliente";
            var frm = new FormData();
            frm.append("idSocioNegocio", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("idTipoPersona", cboTipoPersona.value);
            frm.append("RazonSocial", txtRazonSocial.value.trim());
            frm.append("idTipoDocumento", cboTipoDocumento.value);
            frm.append("Documento", txtNroDocumento.value.trim());
            frm.append("idPais", cboPais.value);
            frm.append("idDepartamento", cboDepartamento.value);
            frm.append("idProvincia", cboProvincia.value);
            frm.append("idDistrito", cboDistrito.value);
            frm.append("Web", gvt("txtWeb"));
            frm.append("Mail", gvt("txtMail"));
            frm.append("Cliente", true);
            frm.append("Proveedor", chkProveedor.checked);
            frm.append("Estado", chkActivo.checked);
            //detalles
            frm.append("Lista_Contacto", crearCadDetalleContacto());
            frm.append("Lista_Direccion", crearCadDetalleDireccion());
            frm.append("Lista_Telefono", crearCadDetalleTelefono());
            frm.append("Lista_CuentaBancaria", crearCadDetalleCuenta());
            swal({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
        }
    };

    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () { show_hidden_Formulario(); }

    //TAB CONTACTO
    var btnAgregarContacto = document.getElementById("btnAgregarContacto");
    btnAgregarContacto.onclick = function () {
        var error = true; error = validarAddContacto();
        if (error) { add_ItemContacto(); }
    }

    var btnCancelarContacto = document.getElementById("btnCancelarContacto");
    btnCancelarContacto.onclick = function () { cancel_AddContacto(); }


    //TAB DIRECCION
    var btnAgregarDir = document.getElementById("btnAgregarDir");
    btnAgregarDir.onclick = function () {
        if (!validarControl("txtDireccion")) {
            add_ItemDireccion();
        }
    }

    var btnCancelarDir = document.getElementById("btnCancelarDir");
    btnCancelarDir.onclick = function () { cancel_AddDireccion(); }

    var tbDireccion = document.getElementById("tbDireccion");
    tbDireccion.onclick = function (e) {
        if (e.target.tagName == "SPAN") {
            var tr = e.target.parentElement.parentElement;
            if (e.target.className.includes("pencil")) {
                txtDireccion.value = tr.cells[3].innerHTML;
                chkDirPrincipal.checked = tr.cells[5].innerHTML == "Principal" ? true : false;
                btnAgregarDir.dataset.row = tr.rowIndex - 1;
                btnAgregarDir.innerHTML = "Editar";
                btnCancelarDir.style.visibility = "visible";
            } else {
                cancel_AddDireccion();
                tbDireccion.removeChild(tr);
                for (var i = 0; i < tbDireccion.rows.length; i++) {
                    tbDireccion.rows[i].cells[2].innerHTML = (i + 1);
                }
            }
        }
    }

    //TAB TELEFONO
    var btnAgregarTel = document.getElementById("btnAgregarTel");
    btnAgregarTel.onclick = function () {
        if (!validarControl("txtTelefono")) {
            add_ItemTelefono();
        }
    }

    var btnCancelarTel = document.getElementById("btnCancelarTel");
    btnCancelarTel.onclick = function () { cancel_AddTelefono(); }

    var tbTelefono = document.getElementById("tbTelefono");
    tbTelefono.onclick = function (e) {
        if (e.target.tagName == "SPAN") {
            var tr = e.target.parentElement.parentElement;
            if (e.target.className.includes("pencil")) {
                txtTelefono.value = tr.cells[3].innerHTML;
                //chkTelPrincipal.checked = tr.cells[5].innerHTML == "Principal" ? true : false;
                btnAgregarTel.dataset.row = tr.rowIndex - 1;
                btnAgregarTel.innerHTML = "Editar";
                btnCancelarTel.style.visibility = "visible";
            } else {
                cancel_AddTelefono();
                tbTelefono.removeChild(tr);
                for (var i = 0; i < tbTelefono.rows.length; i++) {
                    tbTelefono.rows[i].cells[2].innerHTML = (i + 1);
                }
            }
        }
    }

    //TAB BANCO
    var btnAgregarCuenta = document.getElementById("btnAgregarCuenta");
    btnAgregarCuenta.onclick = function () {
        var error = true;
        error = validarAddCuenta();
        if (error) {
            add_ItemCuenta();
        }
    }

    var btnCancelarCuenta = document.getElementById("btnCancelarCuenta");
    btnCancelarCuenta.onclick = function () { cancel_AddCuenta(); }

    var tbCuenta = document.getElementById("tbCuenta");
    tbCuenta.onclick = function (e) {
        if (e.target.tagName == "SPAN") {
            var tr = e.target.parentElement.parentElement;
            if (e.target.className.includes("pencil")) {
                txtBanco.value = tr.cells[3].innerHTML;
                txtBanco.dataset.id = tr.cells[3].dataset.id;
                txtCuenta.value = tr.cells[4].innerHTML;
                txtCuentaDescripcion.value = tr.cells[5].innerHTML;
                cboMoneda.value = tr.cells[6].dataset.id;

                btnAgregarCuenta.dataset.row = tr.rowIndex - 1;
                btnAgregarCuenta.innerHTML = "Editar";
                btnCancelarCuenta.style.visibility = "visible";
            } else {
                cancel_AddCuenta();
                tbCuenta.removeChild(tr);
                for (var i = 0; i < tbCuenta.rows.length; i++) {
                    tbCuenta.rows[i].cells[2].innerHTML = (i + 1);
                }
            }
        }
    }
    var btnGrabar_Modal = document.getElementById('btnGrabar_Modal');
    btnGrabar_Modal.onclick = function () {
        var grabar = btnGrabar_Modal.dataset.grabar;
        var frm = new FormData();
        var txtCodigo = document.getElementById("txtCodigo_Banco");
        var txtDescripcion = document.getElementById("txtDescripcion_Banco");
        var chkActivo = document.getElementById("chkActivo_Banco");
        txtValor = txtDescripcion;
        var url = '/Banco/Grabar';
        frm.append("idBanco", 0);
        frm.append("Descripcion", txtDescripcion.value);
        frm.append("Estado", chkActivo.checked);
        if (validarFormularioModal(grabar)) { enviarServidorPost(url, rpta_GrabarModal, frm); }
    }
    var btnBusqueda = gbi("btnModalBusqueda");
    btnBusqueda.onclick = function () {
        Buscar();
    }
}
function Buscar() {
    if (txtNroDocumento.value.trim().length == 11) {
        var d = txtNroDocumento.value;
        //var url = "SocioNegocio/bsn?r=" + d;
        var url = "/SocioNegocio/ConsultaRUC?r=" + d;
        $("#divLoader").fadeIn(0);
        enviarServidorPost(url, cargarBusqueda);
        gbi("txtNroDocumento").value = d;
        gbi("cboPais").selectedIndex = 1;
    }
}
function validarFormulario() {
    var error = true;
    // Campos
    if (validarControl("txtRazonSocial")) { error = false; }
    if (validarControl("cboPais")) { error = false; }
    if (validarControl("cboTipoPersona")) { error = false; }
    if (validarControl("cboTipoDocumento")) { error = false; }
    if (validarControl("txtNroDocumento")) { error = false; }
    //if (validarControl("txtWeb")) { error = false; }
    //if (validarControl("txtMail")) { error = false; }

    return error;
}
function cbmu(ds, t, tM, tM2, cab, u, m) {
    document.getElementById("btnGrabar_Modal").dataset.grabar = ds;
    document.getElementById("lblTituloModal").innerHTML = t;
    var txtM1 = document.getElementById(tM);
    txtModal = txtM1;
    tM2 == null ? txtModal2 = tM2 : txtModal2 = document.getElementById(tM2);
    cabecera_Modal = cab;
    enviarServidor(u, m);
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
function configBM() {

    var btnModalBanco = gbi("btnModalBanco");
    btnModalBanco.onclick = function () {
        cbmu("banco", "Banco", "txtBanco", null,
            ["id", "Codigo", "Descripcion"], "/Banco/ListarBancos", cargarLista);
    };

    let txtTelefonoContacto = gbi("txtTelefonoContacto");
    txtTelefonoContacto.onkeypress = function (e) {
        var reg = /^[0-9]+$/;
        if (!reg.test(e.key)) return false;
    }
    let txtCuenta = gbi("txtCuenta");
    txtCuenta.onkeypress = function (e) {
        var reg = /^[0-9-]+$/;
        if (!reg.test(e.key)) return false;
    }
    let txtTelefono = gbi("txtTelefono");
    txtTelefono.onkeypress = function (e) {
        var reg = /^[0-9]+$/;
        if (!reg.test(e.key)) return false;
    }
}
function cargarBusqueda(rpta) {
    if (rpta != "") {
        if (rpta == "Error") {
            MensajeRapido("Este N° de RUC no existe", "Mensaje", "info");
        }
        else {
            var p = JSON.parse(rpta);
            txtRazonSocial.value = p.nombre_o_razon_social;
            txtNroDocumento.value = p.ruc;
            cboTipoPersona.value = 2;
            cboTipoDocumento.value = 4;
            gbi("txtCondicionC").value = p.estado_del_contribuyente;
            gbi("txtEstadoC").value = p.condicion_de_domicilio;
            gbi("tb_DetalleFDir").innerHTML = "";
            if (p.departamento.trim() != "")
                for (var i = 0; i < cboDepartamento.options.length; i++) {
                    if (cboDepartamento.options[i].text == p.departamento) {
                        cboDepartamento.selectedIndex = i;
                        CargarProvincias();
                        for (var j = 0; j < cboProvincia.options.length; j++) {
                            if (cboProvincia.options[j].text == p.provincia) {
                                cboProvincia.selectedIndex = j;
                                CargarDistritos();
                                for (var k = 0; k < cboDistrito.options.length; k++) {
                                    if (cboDistrito.options[k].text == p.distrito) {
                                        cboDistrito.selectedIndex = k;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            if (p.direccion.trim() != "") {
                txtDireccion.value = p.direccion_completa;
                addItemDir(0, []);
                limpiarCamposDetalleDir();
            }
            else {
                MensajeRapido("No se encontró ninguna dirección para el Cliente.", "Mensaje", "info");
            }
        }
    }
    else {
        MensajeRapido("No hay respuesta del servidor remoto.", "Error", "error");
    }
}

function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == 'OK') {
            //AbrirModal('modal-form');
            var lista = listas[2].split('▲');
            var Cont = listas[3].split("▼");
            var Dir = listas[4].split("▼");
            var Tel = listas[5].split("▼");
            var Cuent = listas[6].split("▼");
            //Asignar valores a controles
            txtID.value = lista[0];//idArticulo
            txtCodigo.value = lista[1];//codigoGenerado
            //input = lista[2];//idEmpresa
            cboTipoPersona.value = lista[3];//idTipoPersona
            txtRazonSocial.value = lista[4];//RazonSocial
            cboTipoDocumento.value = lista[5];//idTipoDocumento
            txtNroDocumento.value = lista[6];//Documento
            cboPais.value = lista[7];//idPais
            cboDepartamento.value = lista[8];//idDepartamento
            CargarProvincias();
            cboProvincia.value = lista[9];//idProvincia
            CargarDistritos();
            cboDistrito.value = lista[10];//idDistrito
            txtWeb.value = lista[11];//Web
            txtMail.value = lista[12];//Mail
            chkCliente.checked = lista[13] == "TRUE" ? true : false;//Cliente
            chkProveedor.checked = lista[14] == "TRUE" ? true : false;//Proveedor
            chkActivo.checked = lista[19] == "ACTIVO" ? true : false;//Estado
            //descripciones de id
            //gbi("cboTipoCuenta").value = lista[33];
            //Detalles
            //var detalleContacto = listas[3].split('▼');
            //cargarDetalleContacto(detalleContacto);
            var detalleDireccion = listas[4].split('▼');
            cargarDetalleDireccion(detalleDireccion);
            var detalleTelefono = listas[5].split('▼');
            cargarDetalleTelefono(detalleTelefono);
            var detalleCuenta = listas[6].split('▼');
            cargarDetalleCuenta(detalleCuenta);
            if (Cont.length >= 1) {
                if (Cont[0].trim() != "") {
                    for (var i = 0; i < Cont.length; i++) {
                        addItemCT(1, Cont[i].split("▲"));
                    }
                }
            }
            if (Dir.length >= 1) {
                if (Dir[0].trim() != "") {
                    for (var i = 0; i < Dir.length; i++) {
                        addItemDir(1, Dir[i].split("▲"));
                    }
                }
            }
            if (Tel.length >= 1) {
                if (Tel[0].trim() != "") {
                    for (var i = 0; i < Tel.length; i++) {
                        addItemTel(1, Tel[i].split("▲"));
                    }
                }
            }
            if (Cuent.length >= 1) {
                if (Cuent[0].trim() != "") {
                    for (var i = 0; i < Cuent.length; i++) {
                        addItemCuent(1, Cuent[i].split("▲"));
                    }
                }
            }

        }
        else {
            document.getElementById("error").innerHTML = mensaje;
            //mostrarRespuesta(Resultado, mensaje, 'error');
        }
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Cliente";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Modificar Cliente";
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    limpiarControl("txtID");
    limpiarControl("txtCodigo");
    limpiarControl("cboTipoPersona");
    limpiarControl("txtRazonSocial");
    limpiarControl("cboTipoDocumento");
    limpiarControl("txtNroDocumento");
    limpiarControl("cboPais");
    limpiarControl("cboDepartamento");
    limpiarControl("txtNroDocumento");
    limpiarControl("cboProvincia");
    limpiarControl("cboDistrito");
    limpiarControl("txtWeb");
    limpiarControl("txtMail");
    limpiarControl("cboDistrito");
    limpiarControl("txtBanco");
    limpiarControl("txtCuenta");
    limpiarControl("txtCuentaDescripcion");
    limpiarControl("cboMoneda");
    limpiarControl("txtNombre");
    limpiarControl("txtCargo");
    limpiarControl("txtTelefonoContacto");
    limpiarControl("txtMailContacto");
    limpiarControl("txtDireccion");

    gbi("tb_DetalleFCont").innerHTML = "";
    gbi("tb_DetalleFDir").innerHTML = "";
    gbi("tb_DetalleFTel").innerHTML = "";
    gbi("tb_DetalleFCuent").innerHTML = "";
    gbi("chkCliente").checked = false;
    gbi("chkProveedor").checked = false;
    gbi("chkActivo").checked = true;

    gbi("tbDireccion").innerHTML = "";
    gbi("tbTelefono").innerHTML = "";
    gbi("tbCuenta").innerHTML = "";
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar este Socio de Negocio?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                btnCancelar
                var url = "/SocioNEgocio/EliminarCliente?idSocioNegocio=" + id;
                enviarServidor(url, eliminarListar);
            } else {
                swal("Cancelado", "No se eliminó el Socio de Negocio.", "error");
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el Socio de Negocio";
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
//funciones de los TAB
function cancel_AddContacto() {
    var btnAgregarContacto = document.getElementById("btnAgregarContacto");
    var btnCancelarContacto = document.getElementById("btnCancelarContacto");
    btnCancelarContacto.style.visibility = "hidden";
    btnAgregarContacto.dataset.row = -1;
    limpiarControl("txtNombre");
    limpiarControl("txtCargo");
    limpiarControl("txtTelefonoContacto");
    limpiarControl("txtMailContacto");
    btnAgregarContacto.innerHTML = "Agregar";
}
function validarAddContacto() {
    var error = true;
    if (validarControl("txtNombre")) { error = false; }
    if (validarControl("txtCargo")) { error = false; }
    if (validarControl("txtTelefonoContacto")) { error = false; }
    if (validarControl("txtMailContacto")) { error = false; }
    return error;
}

//TAB Direccion
function add_ItemDireccion() {
    var btnAgregarDir = document.getElementById("btnAgregarDir");

    if (btnAgregarDir.dataset.row != undefined && btnAgregarDir.dataset.row != -1) {//editar
        tbDireccion.rows[(btnAgregarDir.dataset.row * 1)].cells[3].innerHTML = txtDireccion.value.toUpperCase().trim();
        tbDireccion.rows[(btnAgregarDir.dataset.row * 1)].cells[5].innerHTML = (chkDirPrincipal.checked == true ? "Principal" : "");
    } else {//nuevo
        var cadena = "<tr data-id='0'>";
        cadena += "<td style='text-align: center;'><span class='fa fa-pencil' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEditar
        cadena += "<td style='text-align: center;'><span class='fa fa-trash-o' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEliminar
        cadena += "<td>" + (tbDireccion.rows.length + 1) + "</td>";//item
        cadena += "<td>" + txtDireccion.value.trim().toUpperCase() + "</td>";//Direccion
        cadena += "<td></td>";//fecha
        cadena += "<td>" + (chkDirPrincipal.checked == true ? "Principal" : "") + "</td>";//Direccion Principal
        cadena += "</tr>";
        tbDireccion.innerHTML += cadena;
    }
    cancel_AddDireccion();
}
function cancel_AddDireccion() {
    var btnAgregarDir = document.getElementById("btnAgregarDir");
    var btnCancelarDir = document.getElementById("btnCancelarDir");
    btnCancelarDir.style.visibility = "hidden";
    btnAgregarDir.dataset.row = -1;
    txtDireccion.value = "";
    chkDirPrincipal.checked = false;
    btnAgregarDir.innerHTML = "Agregar";
}
function lista_Direccion() {
    var lista = "";
    for (var i = 0; i < tbDireccion.rows.length; i++) {
        lista += tbDireccion.rows[i].dataset.id + "|";//idDireccion
        lista += "0|";//idSocio
        lista += "0|";//idEmpresa
        lista += tbDireccion.rows[i].cells[3].innerHTML + "|";//direccion
        lista += (tbDireccion.rows[i].cells[5].innerHTML == "Principal" ? true : false) + "|";//principal
        lista += "||" + idUsuario + "|" + idUsuario + "|true";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
        lista += "¯";
        //var item = {
        //    idDireccion: tbDireccion.rows[i].dataset.id,
        //    Direccion: tbDireccion.rows[i].cells[3].innerHTML,
        //    Principal: 
        //};
        //lista.push(item);
    }
    lista = lista.substring(0, lista.length - 1);
    return lista;
}
function cargarDetalleDireccion(lista) {
    if (lista[0] != "") {
        for (var i = 0; i < lista.length; i++) {
            var item = lista[i].split("▲");
            add_ItemDireccion();
            tbDireccion.rows[i].dataset.id = item[0];
            tbDireccion.rows[i].cells[3].innerHTML = item[1];
            tbDireccion.rows[i].cells[4].innerHTML = item[2];
            tbDireccion.rows[i].cells[5].innerHTML = item[3] == "True" ? "Principal" : "";
        }
    }
}
//TAB Telefono
function add_ItemTelefono() {
    var btnAgregarTel = document.getElementById("btnAgregarTel");

    if (btnAgregarTel.dataset.row != undefined && btnAgregarTel.dataset.row != -1) {//editar
        tbTelefono.rows[(btnAgregarTel.dataset.row * 1)].cells[3].innerHTML = txtTelefono.value.toUpperCase().trim();
        //tbTelefono.rows[(btnAgregarTel.dataset.row * 1)].cells[5].innerHTML = (chkTelPrincipal.checked == true ? "Principal" : "");
    } else {//nuevo
        var cadena = "<tr data-id='0'>";
        cadena += "<td style='text-align: center;'><span class='fa fa-pencil' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEditar
        cadena += "<td style='text-align: center;'><span class='fa fa-trash-o' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEliminar
        cadena += "<td>" + (tbTelefono.rows.length + 1) + "</td>";//item
        cadena += "<td>" + txtTelefono.value.trim().toUpperCase() + "</td>";//Telefono
        cadena += "<td></td>";//fecha
        //cadena += "<td>" + (chkTelPrincipal.checked == true ? "Principal" : "") + "</td>";//Telefono Principal
        cadena += "</tr>";
        tbTelefono.innerHTML += cadena;
    }
    cancel_AddTelefono();
}
function cancel_AddTelefono() {
    var btnAgregarTel = document.getElementById("btnAgregarTel");
    var btnCancelarTel = document.getElementById("btnCancelarTel");
    btnCancelarTel.style.visibility = "hidden";
    btnAgregarTel.dataset.row = -1;
    txtTelefono.value = "";
    //chkTelPrincipal.checked = false;
    btnAgregarTel.innerHTML = "Agregar";
}
function lista_Telefono() {
    var lista = "";
    for (var i = 0; i < tbTelefono.rows.length; i++) {
        lista += tbTelefono.rows[i].dataset.id + "|";//idTelefono
        lista += "0|";//idSocio
        lista += "0|";//idEmpresa
        lista += tbTelefono.rows[i].cells[3].innerHTML + "|";//telefono
        //lista += (tbTelefono.rows[i].cells[5].innerHTML == "Principal" ? true : false) + "|";//principal
        lista += "||" + idUsuario + "|" + idUsuario + "|true";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
        lista += "¯";
    }
    lista = lista.substring(0, lista.length - 1);
    return lista;
}
function cargarDetalleTelefono(lista) {
    if (lista[0] != "") {
        for (var i = 0; i < lista.length; i++) {
            var item = lista[i].split("▲");
            add_ItemTelefono();
            tbTelefono.rows[i].dataset.id = item[0];
            tbTelefono.rows[i].cells[3].innerHTML = item[1];
            tbTelefono.rows[i].cells[4].innerHTML = item[2];
            //tbTelefono.rows[i].cells[5].innerHTML = item[3] == "True" ? "Principal" : "";
        }
    }
}
//TAB Cuenta Banco
function add_ItemCuenta() {
    var btnAgregarCuenta = document.getElementById("btnAgregarCuenta");
    if (btnAgregarCuenta.dataset.row != undefined && btnAgregarCuenta.dataset.row != -1) {//editar
        tbCuenta.rows[(btnAgregarCuenta.dataset.row * 1)].cells[3].innerHTML = txtBanco.value.toUpperCase().trim();
        tbCuenta.rows[(btnAgregarCuenta.dataset.row * 1)].cells[3].dataset.id = txtBanco.dataset.id;
        tbCuenta.rows[(btnAgregarCuenta.dataset.row * 1)].cells[4].innerHTML = txtCuenta.value.trim();
        tbCuenta.rows[(btnAgregarCuenta.dataset.row * 1)].cells[5].innerHTML = txtCuentaDescripcion.value.toUpperCase().trim();
        tbCuenta.rows[(btnAgregarCuenta.dataset.row * 1)].cells[6].innerHTML = cboMoneda.selectedOptions[0].text.toUpperCase().trim();
        tbCuenta.rows[(btnAgregarCuenta.dataset.row * 1)].cells[6].dataset.id = cboMoneda.value.trim();
    } else {//nuevo        
        var cadena = "<tr data-id='0'>";
        cadena += "<td style='text-align: center;'><span class='fa fa-pencil' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEditar
        cadena += "<td style='text-align: center;'><span class='fa fa-trash-o' style='cursor: pointer;color: #03a9f4; font-size:13px;'></span></td>";//btnEliminar
        cadena += "<td>" + (tbCuenta.rows.length + 1) + "</td>";//item
        cadena += "<td data-id='" + txtBanco.dataset.id + "'>" + txtBanco.value.trim().toUpperCase() + "</td>";//Banco
        cadena += "<td>" + txtCuenta.value.trim().toUpperCase() + "</td>";//Cuenta
        cadena += "<td>" + txtCuentaDescripcion.value.trim().toUpperCase() + "</td>";//Descripcion
        cadena += "<td data-id='" + cboMoneda.value + "'>" + cboMoneda.selectedOptions[0].text + "</td>";//moneda
        cadena += "</tr>";
        tbCuenta.innerHTML += cadena;
    }
    cancel_AddCuenta();
}
function cancel_AddCuenta() {
    var btnAgregarCuenta = document.getElementById("btnAgregarCuenta");
    var btnCancelarCuenta = document.getElementById("btnCancelarCuenta");
    btnCancelarCuenta.style.visibility = "hidden";
    btnAgregarCuenta.dataset.row = -1;
    limpiarControl("txtBanco");
    limpiarControl("txtCuenta");
    limpiarControl("txtCuentaDescripcion");
    limpiarControl("cboMoneda");
    btnAgregarCuenta.innerHTML = "Agregar";
}
function validarAddCuenta() {
    var error = true;
    if (validarControl("txtBanco")) { error = false; }
    if (validarControl("txtCuenta")) { error = false; }
    if (validarControl("cboMoneda")) { error = false; }
    return error;
}
function lista_Cuenta() {
    var lista = "";
    for (var i = 0; i < tbCuenta.rows.length; i++) {
        lista += tbCuenta.rows[i].dataset.id + "|";//idCuenta
        lista += "0|";//idSocio
        lista += "0|";//idEmpresa
        lista += tbCuenta.rows[i].cells[3].dataset.id + "|";//idBanco
        lista += tbCuenta.rows[i].cells[5].innerHTML + "|";//Descripcion
        lista += tbCuenta.rows[i].cells[4].innerHTML + "|";//Cuenta
        lista += tbCuenta.rows[i].cells[6].dataset.id + "|";//idMoneda
        lista += "||" + idUsuario + "|" + idUsuario + "|true";//fechacreacion,fehchaModificacion,usuarioCreacion,usuarioModificacion,estado
        lista += "¯";
    }
    lista = lista.substring(0, lista.length - 1);
    return lista;
}
function cargarDetalleCuenta(lista) {
    if (lista[0] != "") {
        for (var i = 0; i < lista.length; i++) {
            var item = lista[i].split("▲");
            add_ItemCuenta();
            tbCuenta.rows[i].dataset.id = item[0];
            tbCuenta.rows[i].cells[3].dataset.id = item[1];
            tbCuenta.rows[i].cells[3].innerHTML = item[2];
            tbCuenta.rows[i].cells[4].innerHTML = item[3];
            tbCuenta.rows[i].cells[5].innerHTML = item[4];
            tbCuenta.rows[i].cells[6].dataset.id = item[5];
            tbCuenta.rows[i].cells[6].innerHTML = item[6];
        }
    }
}


/*metodos actualizados con div  23/04/2018 kevin*/

cfgKP(["txtBanco"], cfgTMKP);
cfgKP(["txtMailContacto", "txtDireccion", "txtTelefono", "txtCuentaDescripcion"], cfgTKP);
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function cfgTKP(evt) {
    //event.target || event.srcElement
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {

            case "txtMailContacto":
                if (validarControl("txtNombre")) return;
                addItemCT(0, []);
                limpiarCamposDetalleCt();
                gbi("txtNombre").focus();
                break;
            case "txtDireccion":
                if (validarControl("txtDireccion")) return;
                addItemDir(0, []);
                limpiarCamposDetalleDir();
                gbi("txtDireccion").focus();
                break;
            case "txtTelefono":
                if (validarControl("txtTelefono")) return;
                addItemTel(0, []);
                limpiarCamposDetalleTel();
                gbi("txtTelefono").focus();
                break;
            case "txtCuentaDescripcion":
                var error = true;
                if (validarControl("txtBanco")) error = false;
                if (validarControl("txtCuenta")) error = false;
                if (validarControl("cboMoneda")) error = false;
                if (error == false) return;

                addItemCuent(0, []);
                limpiarCamposDetalleCuent();
                gbi("txtBanco").focus();
                break;
            default:

        }
        return true;
    }
    else {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        var valor;
        switch (o) {
            default:
                break;

        }
    }
}
/*ingresar contacto*/
function crearCadDetalleContacto() {
    //ingresar los datos correctos
    var cdet = "";
    $(".rowCt").each(function (obj) {
        cdet += $(".rowCt")[obj].children[0].innerHTML;//idContacto
        cdet += "|" + $(".rowCt")[obj].children[1].innerHTML;//idSocio
        cdet += "|0";//idEmpresa
        cdet += "|" + $(".rowCt")[obj].children[2].innerHTML;//Nombre
        cdet += "|" + $(".rowCt")[obj].children[3].innerHTML;//Cargo
        cdet += "|" + $(".rowCt")[obj].children[4].innerHTML;//Telefono
        cdet += "|" + $(".rowCt")[obj].children[5].innerHTML;//Mail
        cdet += "|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function addItemCT(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowCt" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowCt").length + 1) + '"style="margin: auto; margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-4 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtNombre")) + '">' + (tipo == 1 ? data[1] : gvt("txtNombre")) + '</div>';
    contenido += '  <div class="col-md-2 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtCargo")) + '">' + (tipo == 1 ? data[2] : gvt("txtCargo")) + '</div>';
    contenido += '  <div class="col-md-2 p-t-5" data-id="' + (tipo == 1 ? data[3] : gvc("txtTelefonoContacto")) + '">' + (tipo == 1 ? data[3] : gvt("txtTelefonoContacto")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvc("txtMailContacto")) + '">' + (tipo == 1 ? data[4] : gvt("txtMailContacto")) + '</div>';
    contenido += '  <div class="col-md-2">';
    contenido += '      <div class="row rowDetCtbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalleCt(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItemCt(\"gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowCt").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleFCont").innerHTML += contenido;
}
function editItemCt(id) {
    var row = gbi(id);
    gbi("txtNombre").value = row.children[2].innerHTML;
    gbi("txtCargo").value = row.children[3].innerHTML;
    gbi("txtTelefonoContacto").value = row.children[4].innerHTML;
    gbi("txtMailContacto").value = row.children[5].innerHTML;
    gbi("btnGrabarDetalleCt").innerHTML = "Actualizar";
    idTablaDetalle = id;
    gbi("btnGrabarDetalleCt").style.display = "";
    gbi("btnCancelarDetalleCt").style.display = "";
    $("#btnAgregarCt").hide();
}
function guardarItemDetalleCt() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[2].innerHTML = gbi("txtNombre").value;
    row.children[3].innerHTML = gbi("txtCargo").value;
    row.children[4].innerHTML = gbi("txtTelefonoContacto").value;
    row.children[5].innerHTML = gbi("txtMailContacto").value;
    gbi("btnGrabarDetalleCt").style.display = "none";
    gbi("btnCancelarDetalleCt").style.display = "none";
    limpiarCamposDetalleCt();
    $("#btnAgregarCt").show();
}
function eliminarDetalleCt(id) {
    bDM("txtNombre");
    bDM("txtCargo");
    bDM("txtTelefonoContacto");
    bDM("txtMailContacto");

    gbi("btnGrabarDetalleCt").style.display = "none";
    gbi("btnCancelarDetalleCt").style.display = "none";
    $("#btnAgregarCt").show();
}
function borrarDetalleCt(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function limpiarCamposDetalleCt() {

    gbi("txtNombre").value = "";
    gbi("txtCargo").value = "";
    gbi("txtTelefonoContacto").value = "";
    gbi("txtMailContacto").value = "";
}
/*ingresar direccion*/
function crearCadDetalleDireccion() {
    //ingresar los datos correctos
    var cdet = "";
    $(".rowDir").each(function (obj) {
        cdet += $(".rowDir")[obj].children[0].innerHTML;//idContacto
        cdet += "|" + $(".rowDir")[obj].children[1].innerHTML;//idSocio
        cdet += "|0";//idEmpresa
        cdet += "|" + $(".rowDir")[obj].children[2].innerHTML;//Nombre
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function addItemDir(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDir" id="gdD' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDir").length + 1) + '"style="margin: auto; margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-10 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtDireccion")) + '">' + (tipo == 1 ? data[1] : gvt("txtDireccion")) + '</div>';
    contenido += '  <div class="col-sm-2">';
    contenido += '      <div class="row rowDetDirbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalleDir(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItemDir(\"gdD" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDir").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleFDir").innerHTML += contenido;
}
function editItemDir(id) {
    var row = gbi(id);
    gbi("txtDireccion").value = row.children[2].innerHTML;
    gbi("btnGrabarDetalleDir").innerHTML = "Actualizar";
    idTablaDetalle = id;
    gbi("btnGrabarDetalleDir").style.display = "";
    gbi("btnCancelarDetalleDir").style.display = "";
    $("#btnAgregarItemDir").hide();
}
function guardarItemDetalleDir() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[2].innerHTML = gbi("txtDireccion").value;
    gbi("btnGrabarDetalleDir").style.display = "none";
    gbi("btnCancelarDetalleDir").style.display = "none";
    limpiarCamposDetalleDir();
    $("#btnAgregarItemDir").show();
}
function eliminarDetalleDir(id) {
    bDM("txtDireccion");

    gbi("btnGrabarDetalleDir").style.display = "none";
    gbi("btnCancelarDetalleDir").style.display = "none";
    $("#btnAgregarItemDir").show();
}
function borrarDetalleDir(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function limpiarCamposDetalleDir() {

    gbi("txtDireccion").value = "";
}
/*ingresar telefono*/
function crearCadDetalleTelefono() {
    //ingresar los datos correctos
    var cdet = "";
    $(".rowTel").each(function (obj) {
        cdet += $(".rowTel")[obj].children[0].innerHTML;//idContacto
        cdet += "|" + $(".rowTel")[obj].children[1].innerHTML;//idSocio
        cdet += "|0";//idEmpresa
        cdet += "|" + $(".rowTel")[obj].children[2].innerHTML;//telefono
        cdet += "|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function addItemTel(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowTel" id="gdT' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowTel").length + 1) + '"style="margin: auto; margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtTelefono")) + '">' + (tipo == 1 ? data[1] : gvt("txtTelefono")) + '</div>';
    contenido += '  <div class="col-sm-2">';
    contenido += '      <div class="row rowDetTelbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalleTel(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItemTel(\"gdT" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowTel").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleFTel").innerHTML += contenido;
}
function editItemTel(id) {
    gbi("txtTelefono").value = row.children[2].innerHTML;
    gbi("btnGrabarDetalleTel").innerHTML = "Actualizar";
    idTablaDetalle = id;
    gbi("btnGrabarDetalleTel").style.display = "";
    gbi("btnCancelarDetalleTel").style.display = "";
    $("#btnAgregarItemTel").hide();
}
function guardarItemDetalleTel() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[2].innerHTML = gbi("txtTelefono").value;
    gbi("btnGrabarDetalleTel").style.display = "none";
    gbi("btnCancelarDetalleTel").style.display = "none";
    limpiarCamposDetalleTel();
    $("#btnAgregarItemTel").show();
}
function eliminarDetalleTel(id) {
    bDM("txtTelefono");

    gbi("btnGrabarDetalleTel").style.display = "none";
    gbi("btnCancelarDetalleTel").style.display = "none";
    $("#btnAgregarItemTel").show();
}
function borrarDetalleTel(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function limpiarCamposDetalleTel() {

    gbi("txtTelefono").value = "";
}
/*ingresar cuentaBancaria*/
function crearCadDetalleCuenta() {
    //ingresar los datos correctos
    var cdet = "";
    $(".rowCuent").each(function (obj) {
        cdet += $(".rowCuent")[obj].children[0].innerHTML;//idContacto
        cdet += "|" + $(".rowCuent")[obj].children[1].innerHTML;//idSocio
        cdet += "|0";//idEmpresa
        cdet += "|" + $(".rowCuent")[obj].children[2].dataset.id;//banco1
        cdet += "|" + $(".rowCuent")[obj].children[5].innerHTML;//Cuenta3
        cdet += "|" + $(".rowCuent")[obj].children[3].innerHTML;//Descripcion2
        cdet += "|" + $(".rowCuent")[obj].children[4].dataset.id;//Moneda4
        cdet += "|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function addItemCuent(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowCuent" id="gdCu' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowCuent").length + 1) + '"style="margin: auto; margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-3 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtBanco")) + '">' + (tipo == 1 ? data[2] : gvt("txtBanco")) + '</div>';
    contenido += '  <div class="col-md-2 p-t-5" data-id="' + (tipo == 1 ? data[3] : gvt("txtCuenta")) + '">' + (tipo == 1 ? data[3] : gvt("txtCuenta")) + '</div>';;
    contenido += '  <div class="col-md-2 p-t-5" data-id="' + (tipo == 1 ? data[5] : gvt("cboMoneda")) + '">' + (tipo == 1 ? data[6] : $("#cboMoneda option:selected").text()) + '</div>';
    contenido += '  <div class="col-md-3 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvt("txtCuentaDescripcion")) + '">' + (tipo == 1 ? data[4] : gvt("txtCuentaDescripcion")) + '</div>';
    contenido += '  <div class="col-md-2">';
    contenido += '      <div class="row rowDetCtbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalleCuent(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItemCuent(\"gdCu" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowCuent").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleFCuent").innerHTML += contenido;
}
function editItemCuent(id) {
    var row = gbi(id);
    gbi("txtBanco").value = row.children[2].innerHTML;
    gbi("txtCuenta").value = row.children[3].innerHTML;
    gbi("cboMoneda").value = row.children[4].dataset.id;
    gbi("txtCuentaDescripcion").value = row.children[5].innerHTML;
    gbi("btnGrabarDetalleCuent").innerHTML = "Actualizar";
    idTablaDetalle = id;
    gbi("btnGrabarDetalleCuent").style.display = "";
    gbi("btnCancelarDetalleCuent").style.display = "";
    $("#btnAgregarItemCuent").hide();
}
function guardarItemDetalleCuent() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[2].innerHTML = gbi("txtBanco").value;
    row.children[3].innerHTML = gbi("txtCuenta").value;
    row.children[4].innerHTML = gbi("cboMoneda").selectedOptions[0].text;
    row.children[4].dataset.id = gbi("cboMoneda").value;

    row.children[5].innerHTML = gbi("txtCuentaDescripcion").value;
    gbi("btnGrabarDetalleCuent").style.display = "none";
    gbi("btnCancelarDetalleCuent").style.display = "none";
    limpiarCamposDetalleCuent();
    $("#btnAgregarItemCuent").show();
}
function eliminarDetalleCuent(id) {
    bDM("txtBanco");
    bDM("txtCuenta");
    bDM("cboMoneda");
    bDM("txtCuentaDescripcion");

    gbi("btnGrabarDetalleCuent").style.display = "none";
    gbi("btnCancelarDetalleCuent").style.display = "none";
    $("#btnAgregarItemCuent").show();
}
function borrarDetalleCuent(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function limpiarCamposDetalleCuent() {

    gbi("txtBanco").value = "";
    gbi("txtCuenta").value = "";
    gbi("cboMoneda").value = "";
    gbi("txtCuentaDescripcion").value = "";
}



function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
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

function agergarItemCt() {
    if (validarControl("txtNombre")) return;
    addItemCT(0, []);
    limpiarCamposDetalleCt();
    gbi("txtNombre").focus();
}
function agregarItemDir() {
    if (validarControl("txtDireccion")) return;
    addItemDir(0, []);
    limpiarCamposDetalleDir();
    gbi("txtDireccion").focus();
}
function agregarItemTel() {
    if (validarControl("txtTelefono")) return;
    addItemTel(0, []);
    limpiarCamposDetalleTel();
    gbi("txtTelefono").focus();
}
function agregarItemCuent() {
    var error = true;
    if (validarControl("txtBanco")) error = false;
    if (validarControl("txtCuenta")) error = false;
    if (validarControl("cboMoneda")) error = false;
    if (error == false) return;

    var opcion = $("#cboMoneda option:selected").text();
    addItemCuent(0, []);
    limpiarCamposDetalleCuent();
    gbi("txtBanco").focus();
}