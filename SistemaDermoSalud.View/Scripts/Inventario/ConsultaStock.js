var cabeceras = ["idStock", "Código", "Marca", "Descripcion", "Stock"];
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
var cboLocal = document.getElementById("cboLocal");
var cboAlmacen = document.getElementById("cboAlmacen");

$(function () {
    var url = 'ConsultaStock/ObtenerDatos';
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
    configBM();
});
function mostrarLista(rpta) {
    if (rpta != '') {
        let listas = rpta.split('↔');
        let listaLocales = listas[0].split("▼");
        cargarDatosLocal(listaLocales);
        var urlA = '/ConsultaStock/ObtenerStockxAlmacen?pA=1';
        enviarServidor(urlA, mostrarProductosStock);
    }
}
function listar(r) {
    let newDatos = [];
    if (r[0] !== '') {        
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idStock: valor[0],
                codigo: valor[1],
                marca: valor[2],
                producto: valor[3],
                stock: valor[4]
            })
        });
    }
    let cols = ["codigo", "marca", "producto", "stock"];
    loadDataTable(cols, newDatos, "idStock", "tbDatos", "", false);
}
function loadDataTable(cols, datos, rid, tid, btns, arrOrder, showFirstField) {
    var columnas = [];
    for (var i = 0; i < cols.length; i++) {
        let item = {
            data: cols[i]
        };
        columnas.push(item);
    }
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
function cargarDatosLocal(r) {
    let locales = r
    $("#cboLocal").empty();
    $("#cboLocal").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        locales.forEach(element => {
            $("#cboLocal").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
    $("#cboLocal").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "") {
            var urlC = '/ConsultaStock/ObtenerAlmacen?pL=' + cboLocal.value;
            enviarServidor(urlC, cargarDatosAlmacen);
        } else {
            $("#cboLocal").empty();
            $("#cboLocal").append(`<option value="">Seleccione</option>`);
        }
    });
}
function cargarDatosAlmacen(rpta) {
    if (rpta.split('↔')[0] == "OK") {
        let listas = rpta.split('↔');
        let listaAlmacenes = listas[2].split("▼");

        $("#cboAlmacen").empty();
        $("#cboAlmacen").append(`<option value="">Seleccione</option>`);

        if (listaAlmacenes && listaAlmacenes.length > 0) {
            listaAlmacenes.forEach(element => {
                $("#cboAlmacen").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
            });
        }
    }
}
function mostrarProductosStock(rpta) {
    if (rpta != '') {
        var listasArt = rpta.split('↔');
        if (listasArt[0] == "OK") {
            listaDatos = listasArt[2].split("▼");
            listar(listaDatos);
        }
    }
}
function configurarBotonesModal() {
    var btnConsultar = gbi("btnConsultar");
    btnConsultar.onclick = function () {
        var urlA = '/ConsultaStock/ObtenerStockxAlmacen?pA=' + cboAlmacen.value;
        enviarServidor(urlA, mostrarProductosStock);
    }
}
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
        fnExcelReport(cabeceras, matriz);
    }
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



function configAlmacen() {
    console.log("INGRESO STOCK X ALMACEN");
    var urlA = '/ConsultaStock/ObtenerStockxAlmacen?pA=' + cboAlmacen.value;
    enviarServidor(urlA, mostrarArticulos);
    cboAlmacen.onchange = function () {
        var urlA = '/ConsultaStock/ObtenerStockxAlmacen?pA=' + cboAlmacen.value;
        enviarServidor(urlA, mostrarArticulos);
    }
}

function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info d-none d-md-flex' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-1' style='display:none;'>";
                break;
            case 3:
                contenido += "              <div class='col-12 col-md-6'>";
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
                            contenido2 += "col-md-1' style='display:none;'>";
                            break;
                        case 3:
                            contenido2 += "col-md-6'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-1'>";
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