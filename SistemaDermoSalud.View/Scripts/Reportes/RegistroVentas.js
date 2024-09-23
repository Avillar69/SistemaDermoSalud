var cabeceras = ["id", "Fecha", "Serie", "Número", "Razon Social", "SubTotal", "IGV", "Total", "Estado"];
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
var url = "/Reportes/ObtenerDatos_RegistroVenta";
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
    console.log(rpta);
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            var fechaInicio = listas[1];
            var fechaFin = listas[2];
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}
function listar(r) {
    console.log(r);
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                fechaDocumento: valor[1],
                serieDocumento: valor[2],
                numDocumento: valor[3],
                clienteRazon: valor[4],
                subTotalNacional: valor[5],
                iGVNacional: valor[6],
                totalNacional: valor[7],
                estado: valor[8],
                enlace: valor[9]
            })
        });
        console.log(newDatos);
        let cols = ["fechaDocumento", "serieDocumento", "numDocumento", "clienteRazon", "subTotalNacional", "iGVNacional", "totalNacional", "estado"];
        loadDataTable(cols, newDatos, "idDocumentoVenta", "tbDatos", cadButtonOptions(), false);
    }

}
function configBM() {
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Doc. Ventas", cabeceras, matriz, "Registro de Ventas", "a4", "e");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
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
    var url = '/Reportes/ObtenerPorFecha_RegistroVentas?fechaInicio=' + f1 + '&fechaFin=' + f2 + '&idTipoDctos=' + 2;
    enviarServidor(url, mostrarBusqueda);
}