var cabeceras = ["id", "Laboratio", "Producto", "Cantidad Vendida", "Precio Medicamento", "Precio Total"];
var cabecerasTwo = ["Fecha", "Factura", "Razon Social", "Producto", "Cantidad", "Precio", "Total"];
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
var url = "Reportes/ObtenerDatos_RegistroVenta";
enviarServidor(url, mostrarLista);
configBM();
$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
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
    reziseTabla();
}
function listar() {
    matriz = crearMatriz(listaDatos);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
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

var btnCancelar = document.getElementById("btnCancelar");
btnCancelar.onclick = function () {
    show_hidden_Formulario(true);
}

function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    //limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Detalle Venta Producto";
            var url = 'Reportes/ReporteVenta_Fecha_Medicamento?fechaInicio=' + gbi("txtFilFecIn").value + '&fechaFin=' + gbi("txtFilFecFn").value + '&idMedicamento='+id;
            enviarServidor(url, mostrarBusquedaTwo);
            break;
    }
}

function CargarTipoCambio(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var datos = listas[2].split('▲'); //lista OC

    }
}

function mostrarBusquedaTwo(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        matriz = crearMatriz(listaDatos);
        crearTablaComprasTwo(cabecerasTwo, "cabeTablaTwo");
        mostrarMatrizTwo(matriz, cabecerasTwo, "divTablaTwo", "contentPrincipalTwo");
        reziseTabla();
    }
}

function crearTablaComprasTwo(cabecerasTwo, div) {
    var contenido = "";
    nCampos = cabecerasTwo.length;
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
            case 2: case 3: 
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
        }
        contenido += "                  <label>" + cabecerasTwo[i] + "</label>";
        contenido += "              </div>";
    }
    contenido += "          </div>";

    var divTabla = gbi(div);
    divTabla.innerHTML = contenido;
}


function mostrarMatrizTwo(matriz, cabecerasTwo, tabId, contentID) {
    var nRegistros = matriz.length;
    var sumt = 0;
    var obcant = 0;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                //if (matriz[i][8] == "INACTIVO") {
                //    var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;color:red; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                //} else {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                //}   
                for (var j = 0; j < cabecerasTwo.length; j++) {
                    //var enlaceDoc = (matriz[i][9]).toLowerCase();
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-2'>";
                            break;
                        case 2: case 3: 
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabecerasTwo[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
        
                }
                contenido2 += "<div class='col-12 col-md-2'>";
                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                dat.push(contenido2);
                console.log(matriz);

                obcant += parseFloat(matriz[i][4]);

                sumt += parseFloat(matriz[i][6]);

                gbi("hproducto").innerHTML = ": "+matriz[0][3];
                
                gbi("hcantidad").innerHTML = ": "+obcant;

                gbi("htotal").innerHTML = ": "+sumt.toFixed(2);
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




function mostrarMatriz(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                //if (matriz[i][8] == "INACTIVO") {
                //    var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;color:red; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                //} else {
                    var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer; 'ondblclick=mostrarDetalle(2,\"" + matriz[i][0] + "\") >";
                //}   
                for (var j = 0; j < cabeceras.length; j++) {
                    //var enlaceDoc = (matriz[i][9]).toLowerCase();
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0: case 8:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-2'>";
                            break;
                        case 2: case 3: case 4:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-2'>";
                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;'  onclick='mostrarDetalle(1, \"" + matriz[i][0] + "\")'> <i class='fa fa-eye'></i></button>";
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
            case 0: case 8:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 2: case 3: case 4:
                contenido += "              <div class='col-12 col-md-2'>";
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
        listaDatos = listas[2].split('▼');

        matriz = crearMatriz(listaDatos);
        mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
        reziseTabla();
    }
}
function BuscarxFecha(f1, f2) {
    //var chkConTicket = document.getElementById("chkTicket").checked;
    ////var chkSinTicket = document.getElementById("chkSinTicket").checked;
    ////var chkTickets = document.getElementById("chkTickets").checked;
    //var Documentos;
    //if (chkConTicket == true) {
    //    Documentos = 1;
    //} else {
    //    Documentos = 2
    //}
    //var Documentos = chkFacturas + "-" + chkBoletas + "-" + chkTickets;

    var url = 'Reportes/ReporteVenta_Fecha?fechaInicio=' + f1 + '&fechaFin=' + f2 ;
    enviarServidor(url, mostrarBusqueda);
}