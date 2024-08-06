var cabeceras = ["Fecha", "N°", "Razon Social", "Moneda", "Total"];
var matriz = [];
var url = "Home/DashboardGeneral";
enviarServidor(url, mostrarDashboard);

function crearMatriz(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    matriz = [];
    var exito;
    if (listaDatos != "") {
        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            nCampos = campos.length;
            exito = true;
            if (exito) {
                matriz[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matriz[c][j] = campos[j];
                }
                c++;
            }
        }
    } else {
        //document.getElementById("contentPrincipal").innerHTML = "";
    }
    return matriz;
}

function mostrarDashboard(r) {
    var listas = r.split('↔');

    var datos = listas[0].split('▲');
    var compras = listas[1].split('▼');
    var ventas = listas[2].split('▼');

    var ventasDoc = listas[4].split('▼');
    var comprasDoc = listas[5].split('▼');
    console.log(listas);
    crearCabeceraDoc(cabeceras, "cabeCompras");
    crearCabeceraDoc(cabeceras, "cabeVentasRecientes");

    rT("divTablaCompras");
    rT("divTablaVentasRecientes");
    gbi("cmpS").innerHTML = datos[0];
    gbi("cmpD").innerHTML = datos[1];
    gbi("vntS").innerHTML = datos[2];
    gbi("vntD").innerHTML = datos[3];
    gbi("ppC").innerHTML = datos[4];
    gbi("pcV").innerHTML = datos[5];
    var ctx = document.getElementById("graphCV").getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre"],
            datasets: [{
                //    label: 'Ventas',
                //    data: [parseFloat(datos[18]), parseFloat(datos[19]), parseFloat(datos[20]), parseFloat(datos[21]), parseFloat(datos[22]), parseFloat(datos[23]),
                //    parseFloat(datos[24]), parseFloat(datos[25]), parseFloat(datos[26]), parseFloat(datos[27]), parseFloat(datos[28]), parseFloat(datos[29])],
                //    backgroundColor: [
                //        'rgba(255, 99, 132, 0.2)',
                //        'rgba(54, 162, 235, 1)',
                //    ],
                //    borderColor: [
                //        'rgba(255,99,132,1)',
                //        'rgba(54, 162, 235, 1)',
                //    ],
                //    borderWidth: 1
                //},
                //{
                label: 'Ventas',
                data: [parseFloat(datos[6]), parseFloat(datos[7]), parseFloat(datos[8]), parseFloat(datos[9]), parseFloat(datos[10]), parseFloat(datos[11]),
                parseFloat(datos[12]), parseFloat(datos[13]), parseFloat(datos[14]), parseFloat(datos[15]), parseFloat(datos[16]), parseFloat(datos[17])],
                backgroundColor: [
                    'rgba(54, 162, 235,0.2)',
                ],
                borderColor: [
                    'rgba(54, 162, 235, 1)',
                ],
                borderWidth: 1
            },]
        },
        options: {
            responsive: true, maintainAspectRatio: false,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });

    var matrizVentasDoc = crearMatriz(ventasDoc);
    var matrizComprasDoc = crearMatriz(comprasDoc);

    mostrarMatrizDocumentos(matrizComprasDoc, cabeceras, "divTablaCompras", "contentPrincipalCompras");
    mostrarMatrizDocumentos(matrizVentasDoc, cabeceras, "divTablaVentasRecientes", "contentPrincipalVentasRecientes");

    //
    var url = "Home/DashboardCircular";
    enviarServidor(url, GraficoEst);
    //
}

function listar() {
    matriz = crearMatriz(compras);
    configurarFiltro(cabeceras);
    mostrarMatrizDash(matriz, cabeceras, "divTabla", "contentPrincipalCompras");
}

function GraficoEst(rpta) {

    var Res = rpta.split('↔');
    var lista = Res[0].split('▼');
    //los tipos de servicio
    //var T_Derma =    lista[0].split('▲'); //2
    //var T_Estetico = lista[0].split('▲'); //5
    //var C_Menor =   lista[0].split('▲'); //6
    //var Nutricion = lista[1].split('▲');//7
    //var T_Piel =   lista[2].split('▲');//8
    var T_Derma = 0;
    var T_Estetic = 0;
    var C_Menor = 0;
    var Nutricion = 0;
    var T_Piel = 0;
    //var compras = datos[1].split('▼'); ▲   el 0 y 9
    //001▲1▲texto
    let tipoServicios = [];
    let dataServicios = [];
    if (lista != "") {
        for (var i = 0; i < lista.length; i++) {
            var temp = lista[i].split('▲');
            tipoServicios.push(temp[2]);
            dataServicios.push(temp[1]);
            /*
            if (temp[0] == 2) {
                T_Derma = temp[1];
            }
            else if (temp[0] == 5) {
                T_Estetic = temp[1];
            }
            else if (temp[0] == 6) {
                C_Menor = temp[1];
            }
            else if (temp[0] == 7) {
                Nutricion = temp[1];
            }
            else if (temp[0] == 8) {
                T_Piel = temp[1];
            }
            */
        }
    } else {
        T_Derma = 1;
        T_Estetic = 1;
        C_Menor = 1;
        Nutricion = 1;
        T_Piel = 1;
    }

    var oilCanvas = document.getElementById("oilChart");//oilChart

    Chart.defaults.global.defaultFontFamily = "Lato";
    Chart.defaults.global.defaultFontSize = 15;

    var oilData = {
        labels: tipoServicios,//["T.Dermalogico", "T.Estetico", "C.Menor", "Nutricion", "T.Piel"],
        datasets: [
            {
                data: dataServicios,//[T_Derma, T_Estetic, C_Menor, Nutricion, T_Piel],
                backgroundColor: ['#00A5A8', '#626E82', '#FF7D4D', '#FF4558', '#16D39A']
            }]


    };
    var pieChart = new Chart(oilCanvas, {
        type: 'pie',
        data: oilData
    });

}

function mostrarMatrizDash(matriz, cabeceras, tabId, contentID) {
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
                        case 2: case 3:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:8px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-1'>";

                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                dat.push(contenido2);
            }
            else break;
        }
        //var clusterize = new Clusterize({
        //    rows: dat,
        //    scrollId: tabId,
        //    contentId: contentID
        //});
    }
}

function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:2px;padding:10px 10px 10px 10px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 2:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
        }
        contenido += "                  " + cabeceras[i] + "";
        contenido += "              </div>";
    }
    contenido += "          </div>";

    var divTabla = gbi(div);
    divTabla.innerHTML = contenido;
}

function mMD(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 10px;margin-bottom:2px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 2:
                            contenido2 += "col-md-4' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='hidden-sm-up'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "</div>";
                dat.push(contenido2);
            }
            else break;
        }
        /*  var clusterize = new Clusterize({
              rows: dat,
              scrollId: tabId,
              contentId: contentID
          });*/
    }
}
function rT(id) {

    var div = gbi(id);
    var altoTotal = window.innerHeight;
    var puntoInicio = getPos(div).y;
    var altoFooter = 69;
    div.style.height = "" + (altoTotal - altoFooter - puntoInicio - 55) + "px";
}


function crearCabeceraDoc(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:2px;padding:10px 25px 10px 10px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 2:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
        }
        contenido += "                  " + cabeceras[i] + "";
        contenido += "              </div>";
    }
    contenido += "          </div>";

    var divTabla = gbi(div);
    divTabla.innerHTML = contenido;
}
function mostrarMatrizDocumentos(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' style='padding:3px 10px; margin-bottom:2px; cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 2:
                            contenido2 += "col-md-4' style='padding-top:5px; padding-left: 3px; padding-right: 5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px; padding-left: 3px; padding-right: 5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
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
