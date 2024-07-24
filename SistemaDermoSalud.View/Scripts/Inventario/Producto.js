var cabeceras = ["idProducto", "Descripcion", "Marca", "Estado"];
var posiciones = [0, 1, 2, 3];
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
var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtCodigoProducto = document.getElementById("txtCodigoProducto");
var txtDescripcion = document.getElementById("txtDescripcion");
var cboMarca = document.getElementById("cboMarca");
var chkActivo = document.getElementById("chkActivo");
var txtPrecio = document.getElementById("txtPrecio");
$(function () {
    var url = "Producto/ObtenerDatos";
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
});

//listar Producto
function mostrarLista(rpta) {
    if (rpta != "") {
        let listas = rpta.split("↔");
        let resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[3].split("▼");
            let listaMarca = listas[2].split("▼");
            cargarDatosMarca(listaMarca);
            listar(listaDatos);
        }
    }
}
function listar(r) {   
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idProducto: valor[0],
                producto: valor[1],
                marca: valor[2],
                estado: valor[3]
            })
        });
        let cols = ["producto", "marca", "estado"];
        loadDataTable(cols, newDatos, "idProducto", "tbDatos", cadButtonOptions(), false);
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
            chkActivo.checked = true;
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Producto";
            gbi("txtCodigoProducto").focus();
            break;
        case 2:
            let idProducto = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Producto";
            TraerDetalle(idProducto);
            show_hidden_Formulario();
            gbi("txtCodigoProducto").focus();
            break;
    }
}
function limpiarTodo() {
    gbi("txtID").value = "";
    gbi("txtCodigoProducto").value = "";
    gbi("txtPrecio").value = "";
    txtCodigo.value = "";
    txtDescripcion.value = "";
    cboMarca.value = "";
    chkActivo.checked = true;
}
function eliminar(id) {
    let idProducto = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar este producto?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var url = "Producto/Eliminar?idProductos=" + idProducto;
            enviarServidor(url, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se elminó el Producto', 'error');
        }
    });    
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el Producto";
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
//Crear Producto
function cargarDatosMarca(r) {
    let marcas = r
    $("#cboMarca").empty();
    $("#cboMarca").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        marcas.forEach(element => {
            $("#cboMarca").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Producto/Grabar";
            var frm = new FormData();
            frm.append("idProducto", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("Descripcion", txtDescripcion.value);
            frm.append("idMarca", cboMarca.value);
            frm.append("Estado", chkActivo.checked);
            frm.append("Precio",txtPrecio.value);
            frm.append("CodigoProducto", txtCodigoProducto.value);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
    //var btnPDF = gbi("btnImprimirPDF");
    //btnPDF.onclick = function () {
    //    ExportarPDFs("p", "Productos", cabeceras, matriz, "Productos", "a4", "e");
    //}
    //var btnImprimir = document.getElementById("btnImprimir");
    //btnImprimir.onclick = function () {
    //    ExportarPDFs("p", "Productos", cabeceras, matriz, "Productos", "a4", "i");
    //}
    //var btnExcel = gbi("btnImprimirExcel");
    //btnExcel.onclick = function () {
    //    fnExcelReport(cabeceras, matriz);
    //}
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtDescripcion")) error = false;
    if (validarControl("txtPrecio")) error = false;
    if (validarControl("cboMarca")) error = false;
    return error;
}
function actualizarListar(rpta) {
    if (rpta != "") { 
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//AGREGAR
            if (res == "OK") {
                mensaje = "Se adicionó el nuevo Producto";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Producto";
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
//Editar Producto
function TraerDetalle(id) {
    var url = "Producto/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var a = datos[0].split("▲");
        var listaDetalle = datos[2].split("▲");        
        txtID.value = listaDetalle[0];
        txtDescripcion.value = listaDetalle[1];
        cboMarca.value = listaDetalle[2];
        chkActivo.checked = listaDetalle[8] == "ACTIVO" ? true : false;
        txtCodigo.value = listaDetalle[11];
        txtPrecio.value = listaDetalle[9];
        txtCodigoProducto.value = listaDetalle[10];
    }
}
//

//function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
//    var texto = "";
//    var columns = [];
//    for (var i = 0; i < cabeceras.length; i++) {
//        if (i != 0) {
//            columns[i - 1] = cabeceras[i];
//        }
//    }
//    var data = [];
//    for (var i = 0; i < matriz.length; i++) {
//        data[i] = [];
//        for (var j = 0; j < matriz[i].length; j++) {
//            if (j != 0) {
//                data[i][j - 1] = matriz[i][j];
//            }
//        }
//    }
//    var doc = new jsPDF(orientation, 'pt', (tipo == undefined ? "a3" : "a4"));
//    var width = doc.internal.pageSize.width;
//    var height = doc.internal.pageSize.height;
//    var fec = new Date();
//    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
//    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
//    var y = fec.getFullYear();

//    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
//    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
//    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
//    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
//    doc.setFont('helvetica')
//    doc.setFontSize(14);
//    doc.text(titulo, width / 2 - 80, 95);
//    doc.line(30, 125, width - 30, 125);
//    doc.setFontSize(10);
//    doc.setFontType("bold");
//    doc.text("Dermosalud S.A.C", 10, 30);
//    doc.setFontSize(8);
//    doc.setFontType("normal");
//    doc.text("Ruc:", 10, 40);
//    doc.text("20565643143", 30, 40);
//    doc.text("Dirección:", 10, 50);
//    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 50, 50);
//    doc.setFontType("bold");
//    doc.text("Fecha Impresión", width - 90, 40)
//    doc.setFontType("normal");
//    doc.setFontSize(7);
//    doc.text(fechaImpresion, width - 90, 50)

//    doc.autoTable(columns, data, {
//        theme: 'plain',
//        startY: 110, showHeader: 'firstPage',
//        headerStyles: { styles: { overflow: 'linebreak', halign: 'center' }, fontSize: 7, },
//        bodyStyles: { fontSize: 6, valign: 'middle', cellPadding: 2, columnWidt: 'wrap' },
//        columnStyles: {},

//    });
//    if (v == "e") {
//        doc.save((nombre != undefined ? nombre : "table.pdf"));
//    }
//    else if (v == "i") {
//        doc.autoPrint();
//        var iframe = document.getElementById('iframePDF');
//        iframe.src = doc.output('dataurlstring');
//    }
//}
//function configurarFiltro(cabe) {
//    var texto = document.getElementById("txtFiltro");
//    texto.onkeyup = function () {
//        matriz = crearMatriz(listaDatos);
//        mostrarMatrizPersonal(matriz, cabe, "divTabla", "contentPrincipal");
//    };
//}
//function mostrarMatrizPersonal(matriz, cabeceras, tabId, contentID) {
//    var nRegistros = matriz.length;
//    if (nRegistros > 0) {
//        nRegistros = matriz.length;
//        var dat = [];
//        for (var i = 0; i < nRegistros; i++) {
//            if (i < nRegistros) {
//                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
//                for (var j = 0; j < cabeceras.length; j++) {
//                    contenido2 += "<div class='col-12 ";
//                    switch (j) {
//                        case 0:
//                            contenido2 += "col-md-2' style='display:none;'>";
//                            break;
//                        case 1:
//                            contenido2 += "col-md-4' style='padding-top:5px;'>";
//                            break;
//                        case 5:
//                            contenido2 += "col-md-2' style='padding-top:5px;'>";
//                            break;
//                        default:
//                            contenido2 += "col-md-2' style='padding-top:5px;'>";
//                            break;
//                    }
//                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
//                    contenido2 += "</div>";
//                }
//                contenido2 += "<div class='col-12 col-md-2'>";

//                contenido2 += "<div class='row saltbtn'>";
//                contenido2 += "<div class='col-12'>";
//                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o fs-11'></i> </button>";
//                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil fs-11'></i></button>";
//                contenido2 += "</div>";
//                contenido2 += "</div>";
//                contenido2 += "</div>";
//                contenido2 += "</div>";
//                dat.push(contenido2);
//            }
//            else break;
//        }
//        var clusterize = new Clusterize({
//            rows: dat,
//            scrollId: tabId,
//            contentId: contentID
//        });
//    }
//}
