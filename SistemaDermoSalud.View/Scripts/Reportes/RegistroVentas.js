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
    //var chkConTicket = document.getElementById("chkTicket").checked;
    //var chkSinTicket = document.getElementById("chkSinTicket").checked;
    //var chkTickets = document.getElementById("chkTickets").checked;
    //var Documentos;
    //if (chkConTicket == true) {
    //    Documentos = 1;
    //} else {
    //    Documentos = 2
    //}
    //var Documentos = chkFacturas + "-" + chkBoletas + "-" + chkTickets;

    var url = '/Reportes/ObtenerPorFecha_RegistroVentas?fechaInicio=' + f1 + '&fechaFin=' + f2 + '&idTipoDctos=' + 2;
    enviarServidor(url, mostrarBusqueda);
}