var cabeceras = ["idSocioNegocio", "Nº Documento", "Razon Social", "Fecha Modificacion", "Estado"];
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
var idTablaDetalle;
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
var url = "/SocioNegocio/ObtenerDatosProv";
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
var tbContacto = document.getElementById("tbContacto");
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
function cargarImagen(data64, imgCapcha) {
    if (data64 == "") {
        var contenido = '<label class="label label-alert label-sm">Sin respuesta</label>';
        document.getElementById(imgCapcha).innerHTML = contenido;
    } else {
        var contenido = '<img style="height:30px;" src="data:image/jpeg;base64,' + data64 + '"/>';
        document.getElementById(imgCapcha).innerHTML = contenido;
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
            case 1: case 3: case 4: case 6: case 7:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
            case 5:
                contenido += "              <div class='col-12 col-md-1'>";
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
function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
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
        if (listas[11] != "Error") {
            document.getElementById("imgCaptcha").style.display = "inherit";
            cargarImagen(listas[11], "imgCaptcha");
        }
        else {
            //document.getElementById("btnBuscar").disabled = true;
            document.getElementById("imgCaptcha").style.display = "none";
        }
        if (listas[12] != "Error") {
            document.getElementById("imgCaptchaReniec").style.display = "inherit";
            cargarImagen(listas[12], "imgCaptchaReniec");
        }
        else {
            // document.getElementById("btnBuscarReniec").disabled = true;
            document.getElementById("imgCaptchaReniec").style.display = "none";
        }
        llenarCombo(listaPais, "cboPais", "Seleccione");
        llenarCombo(listaTipoPersona, "cboTipoPersona", "Seleccione");
        llenarCombo(listaTipoDocumento, "cboTipoDocumento", "Seleccione");
        llenarCombo(listaMoneda, "cboMoneda");
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
    ExportarPDFs("p", "Proveedores", cabeceras, matriz, "Reporte de Proveedores", "a4", "e");
}
var btnImprimir = document.getElementById("btnImprimir");
btnImprimir.onclick = function () {
    ExportarPDFs("p", "Proveedores", cabeceras, matriz, "Reporte de Proveedores", "a4", "i");
}
var btnExcel = gbi("btnImprimirExcel");
btnExcel.onclick = function () {
    fnExcelReport(cabeceras, matriz);
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
function cbmu(ds, t, tM, tM2, cab, u, m) {
    // document.getElementById("div_Frm_Modal").innerHTML = document.getElementById("div_Frm_Detalle").innerHTML;
    document.getElementById("btnGrabar_Modal").dataset.grabar = ds;
    document.getElementById("lblTituloModal").innerHTML = t;
    // var txtCodigo_FormaPago = document.getElementById("txtCodigo_Detalle");
    // txtCodigo_FormaPago.disabled = true;
    // txtCodigo_FormaPago.placeholder = "Autogenerado";
    var txtM1 = document.getElementById(tM);
    txtModal = txtM1;
    tM2 == null ? txtModal2 = tM2 : txtModal2 = document.getElementById(tM2);
    cabecera_Modal = cab;
    enviarServidor(u, m);
}
function configBM() {
    var btnModalBanco = gbi("btnModalBanco");
    btnModalBanco.onclick = function () {
        cbmu("banco", "Banco", "txtBanco", null,
            ["id", "Codigo", "Descripcion"], "/Banco/ListarBancos", cargarLista);
    };
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "/SocioNegocio/Grabar";
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
            frm.append("Web", txtWeb.value.length == 0 ? "www" : txtWeb.value);
            frm.append("Mail", txtMail.value.length == 0 ? "@" : txtMail.value);
            frm.append("Cliente", chkCliente.checked);
            frm.append("Proveedor", true);
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

    var tbContacto = document.getElementById("tbContacto");
    tbContacto.onclick = function (e) {
        if (e.target.tagName == "SPAN") {
            var tr = e.target.parentElement.parentElement;
            if (e.target.className.includes("pencil")) {
                txtNombre.value = tr.querySelectorAll(".snctc")[3].innerHTML;
                txtCargo.value = tr.querySelectorAll(".snctc")[4].innerHTML;
                txtTelefonoContacto.value = tr.querySelectorAll(".snctc")[5].innerHTML;
                txtMailContacto.value = tr.querySelectorAll(".snctc")[6].innerHTML;

                btnAgregarContacto.dataset.row = tr.rowIndex - 1;
                btnAgregarContacto.innerHTML = "Editar";
                btnCancelarContacto.style.visibility = "visible";
            } else {
                cancel_AddContacto();
                tbContacto.removeChild(tr);
                for (var i = 0; i < tbContacto.querySelectorAll(".snct").length; i++) {
                    tbContacto.querySelectorAll(".snct")[i].querySelectorAll(".snct")[2].innerHTML = (i + 1);
                }
            }
        }
    }

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
        //obtenemos el valor del input del formulario
        txtValor = txtDescripcion;
        //solo cambiamos la url y los id
        var url = '/Banco/Grabar';

        frm.append("idBanco", 0);
        frm.append("Descripcion", txtDescripcion.value);
        frm.append("Estado", chkActivo.checked);
        //validarFormularioModal est aen app.js
        if (validarFormularioModal(grabar)) { enviarServidorPost(url, rpta_GrabarModal, frm); }
    }
    var btnBusqueda = gbi("btnModalBusqueda");
    btnBusqueda.onclick = function () {
        BuscarxRuc();
        gbi("cboPais").value = "1";
    }
    var cboBusqueda = gbi("cboTipoBusqueda");
    cboBusqueda.onchange = function (e) {
        switch (cboBusqueda.value) {
            case "":
                displayXclass("snt", "none");
                displayXclass("rnc", "none");
                break;
            case "1":
                displayXclass("snt", "none");
                displayXclass("rnc", "inline-block");
                break;
            case "2":
                displayXclass("snt", "inline-block");
                displayXclass("rnc", "none");
                break;
            default:
                break;
        }
    }

    var btnBSN = gbi("btnBSN");
    btnBSN.onclick = function () {
        Buscar(gbi("cboTipoBusqueda").value);
        CerrarModal("modal-Busqueda");
    }
}

function BuscarxRuc() {
    if (txtNroDocumento.value.trim().length == 11) {
        var d = txtNroDocumento.value;
        var url = "/SocioNegocio/bsn?r=" + d;
        enviarServidor(url, cargarBusqueda);
        gbi("txtNroDocumento").value = d;
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
// ESTADO Y condicion de contribuyenye

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
            lblTituloPanel.innerHTML = "Nuevo Proveedor";
            gbi("cboPais").value = "1";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Modificar Proveedor";
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

    chkCliente.checked = false;
    chkProveedor.checked = false;
    chkActivo.checked = true;

    tbContacto.innerHTML = "";
    tbDireccion.innerHTML = "";
    tbTelefono.innerHTML = "";
    tbCuenta.innerHTML = "";
    cboMoneda.value = "1";
    limpiarCamposDetalleCt();
}
function validarFormulario() {
    var error = true;
    // Campos
    if (validarControl("txtRazonSocial")) { error = false; }
    if (validarControl("cboPais")) { error = false; }
    if (validarControl("cboTipoPersona")) { error = false; }
    if (validarControl("cboTipoDocumento")) { error = false; }
    if (validarControl("txtNroDocumento")) { error = false; }

    return error;
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
                var url = "/SocioNEgocio/Eliminar?idSocioNegocio=" + id;
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

/*metodos actualizados con div  23/04/2018 kevin*/
cfgKP(["txtBanco"], cfgTMKP);
cfgKP(["txtMailContacto", "txtDireccion", "txtTelefono", "txtCuentaDescripcion"], cfgTKP);
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
function CerrarModalR(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
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
                if (validarCombo("cboMoneda")) error = false;
                if (validarControl("txtCuenta")) error = false;
                if (error == false) return;

                var opcion = $("#cboMoneda option:selected").text();
                addItemCuent(0, []);
                limpiarCamposDetalleCuent();
                gbi("txtBanco").focus();
                break;
            case "txtBanco":
                addItemCuent(0, []);
                limpiarCamposDetalleCuent();
                gbi("txtCuenta").focus();
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
    contenido += '<div class="row rowCt" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowCt").length + 1) + '"style="margin: 0px 0px 2px 0px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtNombre")) + '">' + (tipo == 1 ? data[1] : gvt("txtNombre")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtCargo")) + '">' + (tipo == 1 ? data[2] : gvt("txtCargo")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[3] : gvc("txtTelefonoContacto")) + '">' + (tipo == 1 ? data[3] : gvt("txtTelefonoContacto")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvc("txtMailContacto")) + '">' + (tipo == 1 ? data[4] : gvt("txtMailContacto")) + '</div>';
    contenido += '  <div class="col-sm-1">';
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
    contenido += '<div class="row rowDir" id="gdD' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDir").length + 1) + '"style="margin: 0px 0px 2px 0px;padding:0px 20px;padding-top:4px;">';
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
    contenido += '<div class="row rowTel" id="gdT' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowTel").length + 1) + '"style="margin: 0px 0px 2px 0px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtTelefono")) + '">' + (tipo == 1 ? data[1] : gvt("txtTelefono")) + '</div>';
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
    var row = gbi(id);
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
var opcion = $("#cboMoneda option:selected").text();
function addItemCuent(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowCuent" id="gdCu' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowCuent").length + 1) + '"style="margin: 0px 0px 2px 0px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-3 p-t-5" data-id="' + (tipo == 1 ? data[1] : gvc("txtBanco")) + '">' + (tipo == 1 ? data[2] : gvt("txtBanco")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[3] : gvt("txtCuenta")) + '">' + (tipo == 1 ? data[3] : gvt("txtCuenta")) + '</div>';
    //contenido += '  <div class="col-sm-1 p-t-5" data-id="' + (tipo == 1 ? data[6] : gvc("cboMoneda ")) + '">' + (tipo == 1 ? data[6] : gvt("cboMoneda")) + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[5] : gvt("cboMoneda")) + '">' + (tipo == 1 ? data[6] : $("#cboMoneda option:selected").text()) + '</div>';
    contenido += '  <div class="col-sm-3 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvt("txtCuentaDescripcion")) + '">' + (tipo == 1 ? data[4] : gvt("txtCuentaDescripcion")) + '</div>';
    contenido += '  <div class="col-sm-1">';
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
    var prueba = row.children[4].dataset.id;
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
    var error = true;
    if (validarControl("txtBanco")) error = false;
    if (validarControl("txtCuenta")) error = false;
    if (validarControl("cboMoneda")) error = false;
    if (error == false) return;

    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[2].innerHTML = gbi("txtBanco").value;
    row.children[3].innerHTML = gbi("txtCuenta").value;
    row.children[4].dataset.id = gbi("cboMoneda").value;
    row.children[4].innerHTML = $("#cboMoneda option:selected").text();
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