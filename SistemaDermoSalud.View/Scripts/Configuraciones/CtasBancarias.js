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
var url = "CuentaOrigen/ObtenerDatos";
enviarServidor(url, mostrarLista);
configBM();
reziseTabla();
cfgKP(["txtBanco"], cfgTMKP);
cfgKP(["txtNombreCuenta", "cboMoneda", "txtNroCuenta", "chkEstado"], cfgTKP);

function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function JsonParse(data) {

}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            gbi("chkEstado").checked = true;
            show_hidden_Formulario(false);
            lblTituloPanel.innerHTML = "Nuevo Cuenta Bancaria";//Titulo Insertar
            gbi("txtNombreCuenta").focus();
            break;
            gbi("btnCancelarDetalle").style.display = "";
        case 2:
            lblTituloPanel.innerHTML = "Editar Cuenta Bancaria";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(false);
            gbi("txtNombreCuenta").focus();
            break;
    }
}
$('#collapseOne-2').on('shown.bs.collapse', function () {
    reziseTabla();
})
$('#collapseOne-2').on('hidden.bs.collapse', function () {
    reziseTabla();
})
function TraerDetalle(id) {
    var url = 'CuentaOrigen/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function mostrarLista(rpta) {
    crearTablaCuenta(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        //var mensaje = listas[2];
        if (Resultado == "OK") {

            listaMoneda = listas[1].split("▼");
            listaDatos = listas[2].split("▼");
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        llenarCombo(listaMoneda, "cboMoneda", "Seleccione");
        listar();
    }
    reziseTabla();
}

function listar() {
    crearMatriz(listaDatos); configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    //configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}

function crearTablaCuenta(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 3: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-2'>";
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
function CTM() {

    var contenido = "";
    contenido += "<div class='panel panel-default' style='margin-bottom:10px;'>";
    contenido += "  <div id='collapse" + i + "' class='panel-collapse collapse in' role='tabpanel' aria-labelledby='heading" + i + "'>";
    contenido += "      <div class='panel-body'  style='padding: 10px 20px 20px 10px'>";
    contenido += "          <div class='row' style='padding-left:20px;padding-right:20px'>";
    contenido += "              <div class='col-xs-12 col-md-2'>";
    contenido += "                  <label style='padding-top:5px;'> Código : " + matriz[i][4] + "</label>";
    contenido += "              </div>";
    contenido += "              <div class='col-xs-12 col-md-4'>";
    contenido += "                  <label style='padding-top:5px;'> Categoría : " + matriz[i][5] + "</label>";
    contenido += "              </div>";
    contenido += "              <div class='col-xs-12 col-md-4'>";
    contenido += "                  <label style='padding-top:5px;'> Descripción : " + matriz[i][6] + "</label>";
    contenido += "              </div>";
    contenido += "              <div class='col-xs-12 col-md-2'>";
    contenido += "                  <label style='padding-top:5px;'> Stock Actual : " + matriz[i][7] + "</label>";
    contenido += "              </div>";
    contenido += "          </div>";
    contenido += "      </div>";
    contenido += "  </div>";
    contenido += "</div>";
}

function eliminar(id) {

    swal({
        title: 'Desea Eliminar esta Cuenta Bancaria? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var u = "CuentaOrigen/Eliminar?idCuentaOrigen=" + id;
                enviarServidor(u, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó la Cuenta Bancaria', 'error');
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
function cfgTMKP(evt) {
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        var n = o.replace("txt", "");
        gbi("btnModal" + n).click();
    }
}
function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}
function cfgTKP(evt) {
    //event.target || event.srcElement
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        switch (o) {
            case "txtNombreCuenta":
                gbi("txtBanco").focus();
                break;
            case "cboMoneda":
                gbi("txtNroCuenta").focus();
                break;
            case "txtNroCuenta":
                gbi("chkEstado").focus();
                break;
        }
        return true;
    }   
}
function cargarAlmacen(rpta) {
    if (rpta.split('↔')[0] == "OK") {
        var listas = rpta.split('↔');
        listaAlmacenes = listas[2].split("▼");
        llenarCombo(listaAlmacenes, "cboAlmacen", "Seleccione");
    }
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
function configBM() {
    var btnModalBanco = gbi("btnModalBanco");
    btnModalBanco.onclick = function () {
        cbmu("banco", "Banco", "txtBanco", null,
            ["id", "Codigo", "Descripcion"], "/Banco/ListarBancos", cargarLista);
    };

    function cargarBusqueda(rpta) {
        if (rpta != "") {

            var data = rpta.split('↔');
            var res = data[0];
            var mensaje = '';
            var tipo = '';
            var txtCodigo = document.getElementById('txtID');
            var codigo = txtCodigo.value;
            if (res == 'OK') {
                listaDatos = data[2].split('▼');

                listar();
            }
            else {
                mensaje = data[1];
                tipo = 'error';
                mostrarRespuesta(res, mensaje, tipo);
            }

        }
    }
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Orden Compra", cabeceras, matriz, "Orden Compra", "a4", "e");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    };

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

    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "CuentaOrigen/Grabar";
            var frm = new FormData();
            frm.append("idCuentaOrigen", gvt("txtID"));
            frm.append("NombreCuenta", gvt("txtNombreCuenta"));
            frm.append("Banco", gbi("txtBanco").dataset.id);
            frm.append("DescripcionBanco", gvt("txtBanco"));
            frm.append("idMoneda", gbi("cboMoneda").value);
            frm.append("DescMoneda", $("#cboMoneda option:selected").text());
            frm.append("NumeroCuenta", gvt("txtNroCuenta"));
            frm.append("Estado", gbi("chkEstado").checked);
            /*if (gbi("chkEstado").checked=1) {
                frm.append("Estado", true);
            } else {
                frm.append("Estado", false);
            }*/
            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ImprimirOC();
    };
}
function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[3].dataset.id;
    gbi("txtArticulo").value = row.children[3].innerHTML;
    gbi("txtCategoria").dataset.id = row.children[2].dataset.id;
    gbi("txtCategoria").value = row.children[2].innerHTML;
    gbi("txtCantidad").value = row.children[4].innerHTML;
    gbi("txtPrecio").value = row.children[6].innerHTML;
    gbi("txtTotal").value = row.children[7].innerHTML;
    gbi("btnGrabarDetalle").innerHTML = "Actualizar";
    idTablaDetalle = id;
    gbi("btnGrabarDetalle").style.display = "";
    gbi("btnCancelarDetalle").style.display = "";
}
function guardarItemDetalle() {
    var id = idTablaDetalle;
    var row = gbi(id);
    row.children[3].dataset.id = gbi("txtArticulo").dataset.id;
    row.children[3].innerHTML = gbi("txtArticulo").value;
    row.children[2].dataset.id = gbi("txtCategoria").dataset.id;
    row.children[2].innerHTML = gbi("txtCategoria").value;
    row.children[4].innerHTML = gbi("txtCantidad").value;
    row.children[6].innerHTML = gbi("txtPrecio").value;
    row.children[7].innerHTML = gbi("txtTotal").value;
    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
    calcularSumaDetalle();

    limpiarCamposDetalle();
}
function eliminarDetalle(id) {

    limpiarControl("txtCantidad");
    bDM("txtCategoria");
    bDM("txtArticulo");
    bDM("txtPrecio");
    bDM("txtTotal");

    gbi("btnGrabarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvc("txtCategoria")) + '">' + (tipo == 1 ? data[5] : gvt("txtCategoria")) + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[16] : 'Und.') + '</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[7]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[9]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-1">';
    contenido += '      <div class="row rowDetbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalle(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItem(\"gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleF").innerHTML += contenido;
}
//Crear Detalle como Cadena para env Serv.
function crearCadDetalleOrden() {
    //ingresar los datos correctos
    var cdet = "";
    $(".rowDet").each(function (obj) {

        cdet += $(".rowDet")[obj].children[0].innerHTML;//idOrdenCompraDetalle
        cdet += "|" + $(".rowDet")[obj].children[1].innerHTML;//idCompra
        cdet += "|" + $(".rowDet")[obj].children[3].dataset.id;//idCategoria
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;//descripcionCategoria
        cdet += "|" + $(".rowDet")[obj].children[2].dataset.id;//idArticulo
        cdet += "|" + $(".rowDet")[obj].children[2].innerHTML;//DescripcionArticulo
        cdet += "|" + $(".rowDet")[obj].children[4].innerHTML;//Cantidad
        //cdet += "|" + $(".rowDet")[obj].children[5].innerHTML;//Unidad
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//Precio
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//Precio
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//Total-nacional
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//Total-nacional
        cdet += "|0|01-01-2000|01-01-2000|1|1|true";
        cdet += "¯";
    });
    return cdet;
}
function calcularSumaDetalle() {
    var sum = 0;
    $(".rowDet").each(function (obj) {
        sum += parseFloat($(".rowDet")[obj].children[7].innerHTML);
    });
    if (gbi("chkIGV").checked) {
        gbi("txtSubTotalF").value = parseFloat(sum * 100 / 118).toFixed(3);
        gbi("txtDescuento").value = parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100).toFixed(3);
        gbi("txtIGVF").value = parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value)).toFixed(3);
    }
    else {
        gbi("txtSubTotalF").value = parseFloat(sum).toFixed(3);
        gbi("txtDescuento").value = parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100).toFixed(3);
        gbi("txtIGVF").value = parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value)).toFixed(3);
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
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split('▼');
        listar();
    }
}
//Borrar Data del Modal x ID
function bDM(v) {
    gbi(v).value = "";
    gbi(v).dataset.id = "";
}
function limpiarTodo() {
    var elems = document.getElementsByClassName("form-control");
    for (var i = elems.length - 1; i >= 0; i--) {
        bDM(elems[i].id);
    }
    
}
function limpiarCamposDetalle() {
    bDM("txtCategoria");
    bDM("txtArticulo");
    gbi("txtCantidad").value = "";
    gbi("txtPrecio").value = "";
    gbi("txtTotal").value = "";
}
function limpiarTablaDetalle() {
    $("#tb_DetalleF").html("");
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtBanco":
            return gbi("cboMoneda");
            break;
    }
}
function cargarAlmacenDet(rpta) {


}
function CerrarModal(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}

function cLP(r) {
    if (r.split('↔')[0] == "OK") {
        var listas = r.split('↔');
        var lp = listas[2].split("▲");
        var UnidadMedida = lp[0];
        var Precio = lp[1];
        gbi("txtUnidad").value = UnidadMedida == null ? "" : UnidadMedida;
        gbi("txtPrecio").value = Precio == null ? 0 : Precio;
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
function mostrarMatriz(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 3: case 5: case 6: case 7:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    //contenido2 += "<span class='hidden-sm-up'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += matriz[i][j];
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-2'>";

                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;'  onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o'></i> </button>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;'  onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil'></i></button>";
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

function CerrarModalR(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtNombreCuenta")) error = false;
    if (validarControl("txtBanco")) error = false;
    if (validarControl("cboMoneda")) error = false;
    if (validarControl("txtNroCuenta")) error = false;
    return error;
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        // var alm = listas[3].split("▼");
        if (Resultado == 'OK') {
            adt(datos[1], "txtNombreCuenta");
            gbi("txtBanco").dataset.id = datos[2];
            adt(datos[3], "txtBanco");
            adt(datos[4], "cboMoneda");
            adt(datos[6], "txtNroCuenta");
            adt(datos[0], "txtID");
            gbi("chkEstado").checked = datos[7] != 'INACTIVO';
        }
    }
}

function CargarDetalleRequerimiento(rpta) {
    gbi("tb_DetalleF").innerHTML = "";
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲'); //lista Requerimientos
        var det = listas[2].split("▼");//lista Requerimiento Detalle
        if (Resultado == 'OK') {
            adc(listaTipoCompra, datos[4], "txtTipoCompra", 2);
            if (det.length >= 1) {
                if (det[0].trim() != "") {
                    for (var i = 0; i < det.length; i++) {
                        addItemRequerimiento(1, det[i].split("▲"));
                    }
                }
            }
        }
    }
}
function addItemRequerimiento(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvc("txtCategoria")) + '">' + (tipo == 1 ? data[5] : gvt("txtCategoria")) + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[15] : 'Und.') + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + 0 + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + 0 + '</div>';
    contenido += '  <div class="col-sm-2">';
    contenido += '      <div class="row rowDetbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalle(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += "              <button type='button' onclick='editItem(\"gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + "\");' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-pencil'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleF").innerHTML += contenido;
}


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