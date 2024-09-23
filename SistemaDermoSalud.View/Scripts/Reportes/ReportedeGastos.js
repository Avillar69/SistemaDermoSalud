﻿var cabeceras = ["Fecha", "Concepto", "Observación", "Total", "Forma de Pago"];
var listaDatos;

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
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                fechaCreacion: valor[0],
                descripcionConcepto: valor[1],
                observaciones: valor[2],
                totalNacional: valor[3],
                tipoPago: valor[4],
            })
        });
        console.log(newDatos);
        let cols = ["fechaCreacion", "descripcionConcepto", "observaciones", "totalNacional", "tipoPago"];
        loadDataTable(cols, newDatos, "fechaCreacion", "tbDatos", cadButtonOptions(), false);
    }

}
function BuscarxFecha(f1, f2) {
    var url = '/Reportes/ReporteVenta_GastosxDia?fechaInicio=' + f1 + '&fechaFin=' + f2 + '&idTipoDctos=' + 2;
    enviarServidor(url, mostrarBusqueda);
}

function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        listar(listaDatos);
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