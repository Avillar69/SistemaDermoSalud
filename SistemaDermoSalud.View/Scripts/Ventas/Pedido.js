var cabeceras = ["idPedido", "Fecha", "N°pedido", "ProvDoc", "ProvRazon", "Estado"];
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
var txtModal;
var txtModal2;
var listaLocales;
var listaMoneda;
var listaFormaPago;
var listaComprobantes;
var listaTipoCompra;
var listaSocios;
//Inicializando
//Recordar para poner invisible un div  display none
//$('#divSerie').hide();


//datos globales --- crearCadDetalle()
var idTablaDetalle;
var idDiv;


$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',

});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',

});
$("#txtFechaCobroCheque").datetimepicker({
    format: 'DD-MM-YYYY',

});
$("#txtFecha").datetimepicker({
    format: 'DD-MM-YYYY',

});

//////////////txtOrdenCompra
var url = "Pedido/ObtenerDatos";
enviarServidor(url, mostrarLista);

reziseTabla();
cfgKP(["txtCategoria", "txtArticulo", "txtMoneda", "txtDireccion", "txtRazonSocial", "txtFormaPago", "txtTipoCompra"], cfgTMKP);
cfgKP(["txtCantidad", "txtRequerimiento", "txtPrecio", "txtTotal", "txtNroDocumento", "txtFecha", "txtObservacion", "txtDescuento", "txtDescuentoPrincipal"], cfgTKP);
configBM();
function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function JsonParse(data) {

}



//function cfgOC(l, m) {
//    for (var i = 0; i < l.length; i++) {
//        gbi(l[i]).onchange = m;
//    }
//}

function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();

    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nuevo Pedido";//Titulo Insertar
            //       $("#txtLocalDet").innerHTML="funciono";
            var f = new Date();
            var fechaActual = "";
            fechaActual = (f.getDate() + "-" + (f.getMonth() + 1) + "-" + f.getFullYear());
            adt(fechaActual, "txtFecha");
            break;
            gbi("btnCancelarDetalle").style.display = "";
        case 2:
            lblTituloPanel.innerHTML = "Editar Pedido";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            //$("#txtLocalDet").focus();
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
    var url = 'Pedido/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        //var mensaje = listas[2];
        if (Resultado == "OK") {

            listaSocios = listas[1].split("▼");
            //listaLocales = listas[2].split("▼");
            listaMoneda = listas[2].split("▼");
            listaFormaPago = listas[3].split("▼");
            listaComprobantes = listas[4].split("▼");
            listaTipoCompra = listas[5].split("▼");
            listaDatos = listas[6].split("▼");

            var fechaInicio = listas[7];
            var fechaFin = listas[8];
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;

            var btnTipoCompra = document.getElementById("btnModalTipoCompra");
            btnTipoCompra.onclick = function () {
                cbm("tipocompra", "Tipo de Compra", "txtTipoCompra", null,
                    ["id", "Código", "Descripción"], listaTipoCompra, cargarSinXR);
            }
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        listar();
    }
    reziseTabla();
}
function configurarFiltroOC(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatrizOC(matriz, cabe, "divTabla", "contentPrincipal");
    };
}


function CargarTipoCambio(rpta) {
    if (rpta != '') {
        console.log(rpta);
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲'); //lista OC

        if (Resultado == 'OK') {
            console.log("fafsd");
            console.log(datos);
            adt(datos[4], "txtTipoCambio");
        }
    }
}


function listar() {
    matriz = crearMatriz(listaDatos);
    configurarFiltroOC(cabeceras);
    mostrarMatrizOC(matriz, cabeceras, "divTabla", "contentPrincipal");
    //configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
    gbi("chkIGV").onchange = function () {
        calcularSumaDetalle();
    }
}
function mostrarMatrizOC(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 4:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 2: case 3: case 1: case 5: case 6: case 7:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-1'>";

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
function crearTablaCompras(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2: case 3: case 1: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-3'>";
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
function isNumberKey(evt) {

}

function eliminar(id) {

    swal({
        title: 'Desea Eliminar el pedido? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var u = "Pedido/Eliminar?idPedido=" + id;
                enviarServidor(u, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó la Orden Compra', 'error');
            }
        });
}

function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se elimino la Orden de compra';
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
            case "txtCantidad":
                gbi("txtPrecio").focus();
                break;
            case "txtPrecio":
                gbi("txtTotal").focus();
                break;
            case "txtTotal":
                addItem(0, []);
                limpiarCamposDetalle();
                gbi("txtCategoria").focus();
                calcularSumaDetalle();
                break;
            case "txtFecha":
                var txtFecha = gbi("txtFecha").value;
                console.log("ingresando a la opcion para q salga la cantidad det tipo de cambio");
                console.log(txtFecha);
                var url = 'TipoCambio/ObtenerDatosTipoCambio?fecha=' + txtFecha;
                enviarServidor(url, CargarTipoCambio);
                gbi("txtObservacion").focus();
                break;
            case "txtObservacion":
                gbi("txtRazonSocial").focus();
                break;
            /*case "txtOrdenCompra":
                gbi("txtNroComprobante").focus();
                break;*/
            case "txtNroComprobante":
                gbi("txtFecha").focus();
                break;

            case "txtDescuentoPrincipal":
                gbi("txtCategoria").focus();
                break;

            default:

        }
        return true;
    }
    else {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        var valor;
        switch (o) {
            case "txtCantidad":
            case "txtPrecio":
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    valor = false;
                }
                else {
                    valor = true;
                }
                var c = gbi("txtCantidad").value == "" ? 0 : parseFloat(gbi("txtCantidad").value);
                var p = gbi("txtPrecio").value == "" ? 0 : parseFloat(gbi("txtPrecio").value);

                gbi("txtTotal").value = parseFloat(c * p).toFixed(2);
                return valor;
                break;
            default:
                break;


        }
    }
}

function cargarAlmacen(rpta) {
    if (rpta.split('↔')[0] == "OK") {
        var listas = rpta.split('↔');
        listaAlmacenes = listas[2].split("▼");
        llenarCombo(listaAlmacenes, "cboAlmacen", "Seleccione");
    }
}
//Carga con botones de Modal desde URL
function cbmu(ds, t, tM, tM2, cab, u, m) {
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
    enviarServidor(u, m);
}
//Carga con botones de Modal sin URL (Con datos dat[])
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
//Configurar botones de Modal
function configBM() {
    /*var btnFormaPago = document.getElementById("btnModalFormaPago");
    btnFormaPago.onclick = function () {
        cbmu("formaPago", "Forma de Pago", "txtFormaPago", null,
            ["idFormaPago", "Código", "Descripción"], "/FormaPago/ObtenerDatos?Activo=A", cargarLista);
    }*/



    var btnSocio = document.getElementById("btnModalRazonSocial");
    btnSocio.onclick = function () {
        bDM("txtDireccion");
        cbmu("socio", "Proveedores", "txtRazonSocial", "txtNroDocumento",
            ["idSocioNegocio", "Documento", "Razón Social"], "/SocioNegocio/ObtenerSocioxTipo?tipo=C", cargarLista);
    }

    //------
    var btnMoneda = document.getElementById("btnModalMoneda");
    btnMoneda.onclick = function () {
        cbmu("moneda", "Moneda", "txtMoneda", null,
            ["idMoneda", "Código", "Descripción"], "/Moneda/ObtenerDatos?Activo=A", cargarLista);
    }


    /*    ------------------------------Listar los Requerimientos------------------------------------     */

    /*var btnModRequerimiento = gbi("btnModalRequerimiento");
    btnModRequerimiento.onclick = function () {
        cbmu("requerimiento", "Requerimiento", "txtRequerimiento", null,
            ["idReq", "fecha", "NroReque", "Obs"], "/Requerimientos/ListarRequerimiento?estRequerimiento=3", cargarLista);
    }*/
    //cbm una lista precargadad
    // cbmu es desde un url

    var btnModalCategoria = gbi("btnModalCategoria");
    btnModalCategoria.onclick = function () {
        var Tipo = document.getElementById("txtTipoCompra").getAttribute('data-id');
        if (Tipo) {
            cbmu("categoria", "Categoría", "txtCategoria", null,
                ["idCategoria", "Código", "Descripción"], "/Categoria/ObtenerDatosxTipo?Tipo=" + Tipo + "&Activo=A", cargarLista);
        }
        else {
            mostrarRespuesta("Error", "Debe seleccionar un Tipo de Compra", "error");
        }
    }
    var btnModalDireccion = gbi("btnModalDireccion");
    btnModalDireccion.onclick = function () {
        var Prov = document.getElementById("txtRazonSocial").getAttribute('data-id');
        if (Prov) {
            cbmu("direccion", "Dirección", "txtDireccion", null,
                ["idDireccion", "Descripción"], "/SocioNegocio/ObtenerDireccionxID?id=" + Prov, cargarLista);
        }
        else {
            mostrarRespuesta("Error", "Debe Seleccionar un Proveedor", "error");
        }
    }

    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        var u = "Pedido/ObtenerPorFecha?fechaInicio=" + gbi("txtFilFecIn").value + "&fechaFin=" + gbi("txtFilFecFn").value;
        enviarServidor(u, cargarBusqueda);
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
        ExportarPDFs("p", "Pedido", cabeceras, matriz, "Pedido", "a4", "e");
    };
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "Pedido", cabeceras, matriz, "Pedido", "a4", "i");
    };

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




    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Pedido/Grabar";
            var frm = new FormData();
            //ejemplo
            //frm.append("idGuiaRemision", gvt("txtID"));//id gvc
            frm.append("idPedido", gvt("txtID"));
            frm.append("idEmpresa", 1);//gvt texto 
            frm.append("idTipoCompra", gvc("txtTipoCompra"));
            frm.append("EstadoPedido", gvt("txtOrdenCompra"));

            frm.append("NumPedido", 0);

            frm.append("FechaOrdenCompra", gvt("txtFecha"));
            frm.append("FechaEntrega", gvt("txtFecha"));
            // frm.append("FechaFin", gvt("txtFecha"));
            frm.append("idProveedor", gvc("txtRazonSocial"));
            frm.append("ProveedorRazon", gvt("txtRazonSocial"));
            frm.append("ProveedorDocumento", gvt("txtNroDocumento"));
            frm.append("ProveedorDireccion", gvt("txtDireccion"));
            frm.append("idMoneda", gvc("txtMoneda"));
            frm.append("EstadoOC", 0);

            frm.append("SubTotalNacional", gvt("txtSubTotalF"));
            frm.append("SubTotalExtranjero", gvt("txtSubTotalF"));
            frm.append("TipoCambio", gvt("txtTipoCambio"));
            frm.append("IGVNacional", gvt("txtIGVF"));
            frm.append("IGVExtranjero", gvt("txtIGVF"));
            frm.append("TotalNacional", gvt("txtTotalF"));
            frm.append("TotalExtranjero", gvt("txtTotalF"));
            //
            frm.append("Observacion", gvt("txtObservacion"));
            frm.append("PorcDescuento", gvt("txtDescuentoPrincipal"));
            var porId = document.getElementById("chkIGV").checked;

            frm.append("IGVcheck", porId);
            //
            frm.append("Impreso", 0);
            frm.append("Estado", true);
            frm.append("cadDetalle", crearCadDetalleOrden());
            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
            gbi("btnGrabarDetalle").style.display = "none";
            gbi("btnCancelarDetalle").style.display = "none";

        }
    };


    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }

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
    //para que aparescan , ya que en el html  esta en style="display:none;"
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
    calcularSumaDetalle();
}


//Agregar Item a Tabla Detalle
function addItem(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '"style="margin-bottom:2px;padding:0px 20px;padding-top:4px;">';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-sm-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-sm-2 p-t-5" data-id="' + (tipo == 1 ? data[4] : gvc("txtCategoria")) + '">' + (tipo == 1 ? data[5] : gvt("txtCategoria")) + '</div>';
    contenido += '  <div class="col-sm-4 p-t-5" data-id="' + (tipo == 1 ? data[2] : gvc("txtArticulo")) + '">' + (tipo == 1 ? data[3] : gvt("txtArticulo")) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[6]).toFixed(2) : parseFloat(gvt("txtCantidad")).toFixed(2)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? data[15] : 'Und.') + '</div>';// +gvt("txtUnidad") + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[7]).toFixed(3) : parseFloat(gvt("txtPrecio")).toFixed(3)) + '</div>';
    contenido += '  <div class="col-sm-1 p-t-5">' + (tipo == 1 ? parseFloat(data[8]).toFixed(3) : parseFloat(gvt("txtTotal")).toFixed(3)) + '</div>';
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

        cdet += $(".rowDet")[obj].children[0].innerHTML;//idPedidoDetalle 
        cdet += "|" + $(".rowDet")[obj].children[1].innerHTML;//idPedido 
        cdet += "|" + $(".rowDet")[obj].children[3].dataset.id;// idCategoria
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;// DescripcionCategoria
        cdet += "|" + $(".rowDet")[obj].children[2].dataset.id;// idArticulo
        cdet += "|" + $(".rowDet")[obj].children[2].innerHTML;// DescripcionArticulo
        cdet += "|" + $(".rowDet")[obj].children[4].innerHTML;//Cantidad
        cdet += "|" + $(".rowDet")[obj].children[6].innerHTML;//CantidadAprobada 
        cdet += "|" + $(".rowDet")[obj].children[7].innerHTML;//CantidadRechazada 
        cdet += "|01-01-2000|01-01-2000|1|1|true|false";
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
        gbi("txtIGVF").value = ((parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100).toFixed(3);
        gbi("txtTotalF").value = (parseFloat(gbi("txtSubTotalF").value) + parseFloat(gbi("txtIGVF").value)).toFixed(3);
    }
    else {
        gbi("txtSubTotalF").value = parseFloat(sum).toFixed(3);
        gbi("txtDescuento").value = parseFloat(gbi("txtSubTotalF").value * gbi("txtDescuentoPrincipal").value / 100).toFixed(3);
        gbi("txtIGVF").value = (parseFloat(gbi("txtSubTotalF").value - gbi("txtDescuento").value).toFixed(3)) * 18 / 100;
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
    bDM("txtTipoCompra");
    bDM("txtOrdenCompra");
    limpiarControl("txtFecha");
    bDM("txtRazonSocial");
    limpiarControl("txtNroDocumento");
    limpiarControl("txtObservacion");
    bDM("txtDireccion");
    bDM("txtMoneda");
    bDM("txtRequerimiento");

    limpiarControl("txtSubTotalF");
    limpiarControl("txtDescuento");
    limpiarControl("txtIGVF");
    limpiarControl("txtTotalF");

    gbi("tb_DetalleF").innerHTML = "";
    limpiarCamposDetalle();
}
function limpiarCamposDetalle() {
    bDM("txtCategoria");
    bDM("txtArticulo");
    bDM("txtCantidad");
    bDM("txtPrecio");
    bDM("txtTotal");



}
function limpiarTablaDetalle() {
    $("#tb_DetalleF").html("");
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtCategoria":
            //var txtCategoria = document.getElementById("txtCategoria");
            gbi("txtArticulo").value = "";
            gbi("txtArticulo").dataset = "";
            var btnModalArticulo = gbi("btnModalArticulo");
            btnModalArticulo.onclick = function () {
                cbmu("articulo", "Articulo", "txtArticulo", null,
                    ["idArticulo", "Código", "Descripción"], "/Articulo/ObtenerDatosxCategoria?Cat=" + gbi("txtCategoria").dataset.id + "&Activo=A", cargarLista);
            }
            return gbi("txtArticulo");
            break;
        case "txtArticulo":
            return gbi("txtCantidad");
            break;
        case "txtRazonSocial":
            return gbi("txtDireccion");
            break;
        /*case "txtFormaPago":
            return gbi("txtCategoria");
            break;
        case "txtMoneda":
            return gbi("txtFormaPago");
            break;*/
        case "txtTipoCompra":
            return gbi("txtFecha");
            break;
        case "txtFecha":
            return gbi("txtRazonSocial");
            break;

        case "txtTipoDocumento":
            return gbi("txtOrdenCompra");
            break;
        case "txtDireccion":
            return gbi("txtMoneda");
            break;

    }
    if (txtModal.id == "txtCategoria") {
        ;
    }
    if (txtModal.id == "txtArticulo") {
        return gbi("txtCantidad");
    }
    if (txtModal.id == "txtRazonSocial") {
        return gbi("txtDireccion");
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
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}
function validarFormulario() {
    var error = true;
    //Aqui Ingresar todos los campos a validar || validarCombo || validarTexto

    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtNroDocumento")) error = false;
    if (validarControl("txtMoneda")) error = false;
    if (validarControl("txtObservacion")) error = false;
    // if (validarControl("txtFormaPago")) error = false;
    if (validarControl("txtRazonSocial")) error = false;
    if (validarControl("txtOrdenCompra")) error = false;
    if (validarControl("txtTipoCompra")) error = false;
    if (validarControl("txtFecha")) error = false;
    //if (validarControl("txtRequerimiento")) error = false;





    return error;
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[1].split('▲');
        // var alm = listas[3].split("▼");
        var dir = listas[2].split("▼");
        var det = listas[3].split("▼");
        if (Resultado == 'OK') {
            //adt es para id y adc tablas
            //txtNroSerie

            adc(listaTipoCompra, datos[2], "txtTipoCompra", 2);
            adt(datos[3], "txtOrdenCompra");
            adt(datos[4], "txtRequerimiento");
            adt(datos[21], "txtFecha");     //fecha  creacion

            adc(listaSocios, datos[7], "txtRazonSocial", 1);
            adc(listaSocios, datos[7], "txtNroDocumento", 2);
            adt(datos[10], "txtDireccion");
            adc(listaMoneda, datos[19], "txtMoneda", 1);
            adt(datos[13], "txtTipoCambio");


            adt(datos[30], "txtObservacion");
            adt(datos[14], "txtDescuento");
            adt(datos[31], "txtDescuentoPrincipal");
            if (datos[29] == 'TRUE') {
                gbi("chkIGV").checked = true;
            } else {
                gbi("chkIGV").checked = false;
            }

            adt(datos[0], "txtID");
            if (det.length >= 1) {
                if (det[0].trim() != "") {
                    for (var i = 0; i < det.length; i++) {
                        addItem(1, det[i].split("▲"));
                    }
                }
            }
            calcularSumaDetalle();
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