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
    var productos = listas[1].split('▼');
    var clientes = listas[2].split('▼');

    console.log(datos);
    $("#cmpS").html(parseFloat(datos[0]).toFixed(2));
    $("#vntS").html(parseFloat(datos[2]).toFixed(2));
    $("#pgS").html(parseFloat(datos[4]).toFixed(2));
    $("#cbS").html(parseFloat(datos[5]).toFixed(2));
    mostrarTablaProductos(productos);
    mostrarTablaClientes(clientes);
    //mostrarMatrizDocumentos(matrizComprasDoc, cabeceras, "divTablaCompras", "contentPrincipalCompras");
    //mostrarMatrizDocumentos(matrizVentasDoc, cabeceras, "divTablaVentasRecientes", "contentPrincipalVentasRecientes");

    //
    //
}

function listar() {
    matriz = crearMatriz(compras);
    configurarFiltro(cabeceras);
    mostrarMatrizDash(matriz, cabeceras, "divTabla", "contentPrincipalCompras");
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

function mostrarTablaProductos(listaProductos) {
    var contenido = "";
    nCampos = listaProductos.length;
    for (var i = 0; i < nCampos; i++) {
        let obj = listaProductos[i].split("▲");
        contenido += "<tr>";
        contenido += "<td>";
        contenido += '<div class="d-flex align-items-center"><div>';
        contenido += '<h5 class="fs-14 my-1"><a class="text-reset">' + obj[1] + '</a></h5>';
        contenido += '<span class="text-muted">' + obj[2] + '</span></div></div>'
        contenido += "</td>";
        contenido += "<td>";
        contenido += '<h5 class="fs-14 my-1 fw-normal">S/.' + parseFloat(obj[5]).toFixed(2) + '</h5>';
        contenido += '<span class="text-muted">Precio</span>';
        contenido += "</td>";
        contenido += "<td>";
        contenido += '<h5 class="fs-14 my-1 fw-normal">' + parseInt(obj[3]) + '</h5>';
        contenido += '<span class="text-muted">Ventas</span>';
        contenido += "</td>";
        contenido += "<td>";
        contenido += '<h5 class="fs-14 my-1 fw-normal">' + obj[4] + '</h5>';
        contenido += '<span class="text-muted">Stock</span>';
        contenido += "</td>";
        contenido += "</tr>";
    }

    var divTabla = gbi("tbArticulosVendidos");
    divTabla.innerHTML = contenido;
}
function mostrarTablaClientes(listaProductos) {
    var contenido = "";
    nCampos = listaProductos.length;
    for (var i = 0; i < nCampos; i++) {
        let obj = listaProductos[i].split("▲");
        contenido += "<tr>";
        contenido += "<td>";
        contenido += '<div class="d-flex align-items-center"><div>';
        contenido += '<h5 class="fs-14 my-1"><a class="text-reset">' + obj[1] + '</a></h5></div></div>';
        contenido += "</td>";
        contenido += "<td>";
        contenido += '<h5 class="fs-14 my-1 fw-normal">' + obj[2] + '</h5>';
        contenido += '<span class="text-muted">Última Compra</span>';
        contenido += "</td>";
        contenido += "<td>";
        contenido += '<h5 class="fs-14 my-1 fw-normal">S/.' + parseFloat(obj[3]).toFixed(2) + '</h5>';
        contenido += '<span class="text-muted">Valor</span>';
        contenido += "</td>";
        contenido += "</tr>";
    }

    var divTabla = gbi("tbClientesFrecuentes");
    divTabla.innerHTML = contenido;
}
