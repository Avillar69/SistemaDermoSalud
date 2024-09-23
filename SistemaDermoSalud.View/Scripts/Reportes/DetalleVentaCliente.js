var cabeceras = ["Fecha", "Tipo Comprobante", "N° Comprobante", "Artículo", "Cantidad", "Precio", "Total"];
var listaDatos;
var listaImpresion;
var listaReportePorCliente;
var matriz = [];
var txtModal;
var txtModal2;
var listaLocales;
var listaMoneda;
var listaFormaPago;
var listaComprobantes;
var listaTipoVenta;
var listaSocios;
var listaTipoAfectacion;
/*objetos globales para el detalle pago si es al contado*/
var listaSocioPorSiaca;
var listarCuentasO;
var listarBanco;
/*fin de objetos globales pago detalle*/
var lp2;
var url = "/Reportes/ObtenerDatos_DetalleVentaCliente";
enviarServidor(url, mostrarLista);
configBM();

$("#txtFilFecIn").flatpickr({
    enableTime: false,
    dateFormat: "d-m-Y"
});
$("#txtFilFecFn").flatpickr({
    enableTime: false,
    dateFormat: "d-m-Y"
});
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            var fechaInicio = listas[1];
            var fechaFin = listas[2];
            var listaCliente = listas[3].split("▼");;
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;
            cargarDatosProveedores(listaCliente);
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}

function cargarDatosProveedores(r) {
    let proveedores = r;
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    for (var j = 0; j < proveedores.length; j++) {
        let objChild = {
            id: proveedores[j].split('▲')[0],
            text: proveedores[j].split('▲')[1]
        };
        arr.push(objChild);
    }
    console.log(arr);
    $("#cboRazonSocial").select2({
        placeholder: "Seleccione",
        data: arr,
    });
}
function listar(r) {
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                fechaDocumento: valor[0],
                tipoComprobante: valor[1],
                nroComprobante: valor[2],
                descripcionArticulo: valor[3],
                cantidad: valor[4],
                precioNacional: valor[5],
                totalNacional: valor[6]
            })
        });
        let cols = ["fechaDocumento", "tipoComprobante", "nroComprobante", "descripcionArticulo", "cantidad",
            "precioNacional", "totalNacional"];
        loadDataTableV(cols, newDatos, "fechaDocumento", "tbDatos", cadButtonOptions(), false);
    }
    else {
        var table = new DataTable('#tbDatos');

        var rows = table
            .rows()
            .remove()
            .draw();
    }

}

function loadDataTableV(cols, datos, rid, tid, btns, arrOrder, showFirstField) {
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
                    "width": "10%",
                }],
        searching: !0,
        bLengthChange: !0,
        destroy: !0,
        pagingType: "full_numbers",
        info: !1,
        paging: !0,
        pageLength: 10,
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
//Tabla
function crearMatriz(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    //var textos = document.getElementById("txtFiltro").value.trim();
    matriz = [];
    var exito;
    if (listaDatos != "") {

        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            nCampos = campos.length;
            exito = true;
            //if (textos.trim() != "") {
            //    for (var l = 1; l < nCampos; l++) {
            //        exito = true;
            //        exito = exito && campos[l].toLowerCase().indexOf(textos.toLowerCase()) != -1;
            //        if (exito) break;
            //    }
            //}
            if (exito) {
                matriz[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matriz[c][j] = campos[j];
                }
                c++;
            }
        }
    } else {
        document.getElementById("contentPrincipal").innerHTML = "";
    }
    return matriz;
}
function crearTablaCompras(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0: case 8:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 3: case 2: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-3'>";
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
//
function configBM() {
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Doc. Ventas", cabeceras, matriz, "Registro de Ventas", "a4", "e");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras);
    }
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }
}
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼'); listar(listaDatos);
    }
}
function BuscarxFecha(f1, f2) {
    var url = '/Reportes/ObtenerPorFecha_DetalleVentaCliente?fechaInicio=' + f1 + '&fechaFin=' + f2 + '&idCliente=' + $("#cboRazonSocial").val();
    enviarServidor(url, mostrarBusqueda);
}

function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    let cabPdf = ["Fecha", "Serie", "Número", "Razon Social", "SubTotal", "IGV", "Total"];
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
        doc.save((nombre != undefined ? nombre : "doc_compras.pdf"));
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
        sa = txtArea1.document.execCommand("SaveAs", true, "Descarga.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}
//




