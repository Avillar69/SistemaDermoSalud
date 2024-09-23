var listaDatos;
var matriz = [];
var listaLocales;
var listaAlmacenes;
var nombreEmpresa = "HARI´S SPORT EMPRESA INDIVIDUAL DE RESPONSABILIDAD LIMITADA";
var rucEmpresa = "20612173452";
var direccionEmpresa = "JR. ANCASH NRO. 1265 (ESQUINA CON TARAPACA) JUNIN - HUANCAYO - HUANCAYO";

var url = "/Reportes/ObtenerDatos_RegistroVenta";
enviarServidor(url, mostrarLista);

function mostrarLista(rpta) {
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
$("#txtFilFecIn").flatpickr({
    enableTime: false,
    dateFormat: "d-m-Y"
});
$("#txtFilFecFn").flatpickr({
    enableTime: false,
    dateFormat: "d-m-Y"
});
$(function () {
    let urlMarcas = "/Marca/ObtenerDatos";
    enviarServidor(urlMarcas, cargarDatosMarca);
    configBM();
});
//function cargarDatosMarca(r) {
//    $('#cboMarca').select2('destroy');
//    $('#cboMarca').html('');
//    let rd = r.split("↔");
//    //let marcas = rd[2].split("▼");
//    //$("#cboMarca").empty();
//    //$("#cboMarca").append(`<option value="0">Seleccione</option>`);
//    //$("#cboProducto").empty();
//    //$("#cboProducto").append(`<option value="0">Seleccione</option>`);

//    //if (r && r.length > 0) {
//    //    marcas.forEach(element => {
//    //        $("#cboMarca").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
//    //    });
//    //}


//    let proveedores = rd[2].split("▼");
//    let arr = [{
//        id: 0,
//        text: "Seleccione",
//    }];
//    for (var j = 0; j < proveedores.length; j++) {
//        let objChild = {
//            id: proveedores[j].split('▲')[0],
//            text: proveedores[j].split('▲')[1]
//        };
//        arr.push(objChild);
//    }
//    $("#cboMarca").select2({
//        placeholder: "Seleccione",
//        data: arr, allowClear: true,
//        escapeMarkup: function (e) {
//            return e;
//        }
//    });


//    $("#cboMarca").change(function () {
//        var selectedValue = $(this).val();
//        if (selectedValue !== "" && selectedValue !== null) {
//            var url = "/OperacionesStock/cargarProductoxMarca?idMarca=" + selectedValue;
//            enviarServidor(url, cargarDatosProductos);

//        }
//    });
//}
function cargarDatosMarca(r) {
    console.log(r);
    //$('#cboMarca').select2('destroy');
    $('#cboMarca').html('');
    let rd = r.split("↔");
    let marcas = rd[2].split("▼");
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    for (var j = 0; j < marcas.length; j++) {
        let objChild = {
            id: marcas[j].split('▲')[0],
            text: marcas[j].split('▲')[1]
        };
        arr.push(objChild);
    }
    $("#cboMarca").select2({
        placeholder: "Seleccione",
        data: arr, allowClear: true,
        escapeMarkup: function (e) {
            return e;
        }
    });
    $("#cboMarca").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            var url = "/OperacionesStock/cargarProductoxMarca?idMarca=" + selectedValue;
            enviarServidor(url, cargarDatosProductos);

        }
    });
}
function cargarDatosProductos(r) {
    let rd = r.split("↔");
    let productos = rd[2].split("▼");

    $('#cboProducto').html('');
    let arr = [{
        id: 0,
        text: "Seleccione",
    }];
    for (var j = 0; j < productos.length; j++) {
        let objChild = {
            id: productos[j].split('▲')[0],
            text: productos[j].split('▲')[1]
        };
        arr.push(objChild);
    }
    $("#cboProducto").select2({
        placeholder: "Seleccione",
        data: arr, allowClear: true,
        escapeMarkup: function (e) {
            return e;
        }
    });
    //$("#cboProducto").empty();
    //$("#cboProducto").append(`<option value="0">Seleccione</option>`);
    //if (r && r.length > 0) {
    //    productos.forEach(element => {
    //        $("#cboProducto").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
    //    });
    //}
}
function configBM() {

    var btnBuscar = gbi("btnBuscar");
    btnBuscar.onclick = function () {
        var error = true;
        if (validarControl("txtFilFecIn")) error = false;
        if (validarControl("txtFilFecFn")) error = false;
        if (!error) return;
        var url = "/Kardex/BuscarKardex?fI=" + gbi("txtFilFecIn").value + "&fF=" + gbi("txtFilFecFn").value + "&iC= " + gbi("cboMarca").value + "&iA=" + (gbi("cboProducto").value == "" ? "0" : gbi("cboProducto").value);
        enviarServidor(url, MostrarBusqueda)
    };
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        window.addEventListener('focus', window_focus, false);

        try {
            var cabecera = "Fecha,Doc,Articulo,U.M.,Stock Inicial,Precio,Cant. Entrada,Precio Entrada,Total Entrada";
            window.location = "Kardex/exportarExcel?local=1&fechaInicio=" + gbi("txtFilFecIn").value + "&fechaFin=" + gbi("txtFilFecFn").value + "&cabecera=" + cabecera;
        }
        catch (ex) {
            mostrarMensaje('Error', ex, 'error');
        }

        //var url = "Kardex/exportarExcel?fechaInicio=" + gvt("txtFilFecIn") + "&fechaFin=" + gvt("txtFilFecFn") + "&iC=0" + "&iA=" + (gbi("txtArticulo").dataset.id == undefined ? "0" : gbi("txtArticulo").dataset.id);
        //enviarServidor(url, mostrarExcel);
    }
}
function mostrarExcel(r) {
    console.log(r);
}
function crearMatrizKardex(listaDatos) {
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
    }
    return matriz;
}
function MostrarBusqueda(r) {
    if (r != "") {
        var datos = r.split("↔");
        var resultado = datos[0];
        var mensaje = datos[1];
        var lista = datos[2].split("▼");;
        if (resultado == "OK") {
            ImprimirKardex(crearMatrizKardex(lista));
        }
        else {
            mostrarRespuesta("Error", mensaje, "error");
        }
    }
}
function ImprimirKardex(matriz) {
    var texto = "";
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = matriz[i];
    }
    var doc = new jsPDF('l', 'pt', "a4");
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    var fec = new Date();
    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
    var y = fec.getFullYear();
    //Datos Cabecera de Página
    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
    doc.setFont('helvetica')
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text(nombreEmpresa, 30, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 30, 40);
    doc.text(rucEmpresa, 50, 40);
    doc.text("Dirección:", 30, 50);
    doc.text(direccionEmpresa, 70, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 90, 40);
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50);
    doc.setFontSize(7);

    var xid = 140;
    var xad = 14;
    var xan = 12;
    var artActual = "";
    var n = 0;
    var Stock = 0;
    var CantidadAcumulada = 0;
    var CantidadCompraAcum = 0;
    var TotalCompraAcum = 0;
    var PrecioPromedio = 0;
    var TotalAcumulado = 0;
    doc.setFontSize(6);
    doc.setFontType("bold");

    for (var i = 0; i < data.length; i++) {
        console.log(data);
        if (artActual != data[i][2]) {
            if (xid + (n * xad) + xad > height - 30) {
                doc.addPage();
                n = 0;
                xid = 50;
            }
            CantidadCompraAcum = parseFloat(data[i][5]);
            TotalCompraAcum = parseFloat(parseFloat(data[i][5]) * parseFloat(data[i][6]));
            doc.setFontSize(7);
            doc.setFontType("bold");
            doc.text("Producto : " + data[i][2] + " - " + data[i][3], 30, xid + (n * xad) + xad);
            doc.text("Medida : " + data[i][4], width / 2, xid + (n * xad) + xad);
            doc.text("Stock Inicial : " + parseFloat(data[i][5]).toFixed(3), width / 2 + 80, xid + (n * xad) + xad);
            doc.text("Costo Promedio : " + parseFloat(data[i][6]).toFixed(3), width / 2 + 180, xid + (n * xad) + xad);
            doc.text("Valor Inicial : " + parseFloat(parseFloat(data[i][5]).toFixed(3) * parseFloat(data[i][6]).toFixed(3)).toFixed(3), width / 2 + 280, xid + (n * xad) + xad);
            CantidadAcumulada = parseFloat(data[i][5]);
            PrecioPromedio = parseFloat(data[i][6]);
            TotalAcumulado = parseFloat(parseFloat(data[i][5]) * parseFloat(data[i][6]));
            artActual = data[i][2];
            n += 1;
            if (xid + (n * xad) + xad > height - 30) {
                doc.addPage();
                n = 0;
                xid = 50;
            }
            doc.text("ENTRADAS", 490, xid + (n * xad) + xad, "right");
            doc.text("SALIDAS", 610, xid + (n * xad) + xad, "right");
            doc.text("EXISTENCIAS", 730, xid + (n * xad) + xad, "right");
            n += 1;
            if (xid + (n * xad) + xad > height - 30) {
                doc.addPage();
                n = 0;
                xid = 50;
            }
            doc.text("FECHA", 50, xid + (n * xad) + xad);
            doc.text("DOC. REF.", 120, xid + (n * xad) + xad);
            doc.text("OPERACIÓN", 180, xid + (n * xad) + xad);
            doc.text("OBSERVACIONES", 320, xid + (n * xad) + xad);
            doc.text("CANTIDAD", 450, xid + (n * xad) + xad, "right");
            doc.text("COSTO U.", 490, xid + (n * xad) + xad, "right");
            doc.text("TOTAL.", 530, xid + (n * xad) + xad, "right");
            doc.text("CANTIDAD", 570, xid + (n * xad) + xad, "right");
            doc.text("COSTO U.", 610, xid + (n * xad) + xad, "right");
            doc.text("TOTAL", 650, xid + (n * xad) + xad, "right");
            doc.text("CANTIDAD", 690, xid + (n * xad) + xad, "right");
            doc.text("COSTO U.", 730, xid + (n * xad) + xad, "right");
            doc.text("TOTAL", 770, xid + (n * xad) + xad, "right");
            doc.line(30, xid + (n * xad) + xad + 1, width - 30, xid + (n * xad) + xad + 1);
            n += 2;
        }

        if (xid + (n * xad) + xad > height - 30) {
            doc.addPage();
            n = 0;
            xid = 50;
        }
        doc.setFontSize(6);
        doc.setFontType("normal");
        //Fecha
        doc.text(data[i][0], 50, xid + (n * xad));
        //Doc. Referencia
        doc.text(data[i][1], 120, xid + (n * xad));
        //Tipo Movimiento
        doc.text(data[i][14], 180, xid + (n * xad));
        //Observaciones
        doc.text(data[i][13], 320, xid + (n * xad));
        if (parseFloat(data[i][7]) == parseFloat(0)) {
            CantidadAcumulada -= parseFloat(data[i][10]);
        }
        else {
            CantidadAcumulada += parseFloat(data[i][7]);
        }
        if (data[i][14] == 'ENTRADA DE INVENTARIO INICIAL' || data[i][14] == 'ENTRADA POR COMPRA') {
            CantidadCompraAcum += parseFloat(data[i][7]);
            TotalCompraAcum += parseFloat(data[i][9]);
            PrecioPromedio = (TotalCompraAcum / CantidadCompraAcum);
        }

        //Entrada Cantidad
        doc.text(parseFloat(data[i][7]).toFixed(3), 450, xid + (n * xad), "right");
        //Entrada Precio
        if (parseFloat(data[i][8]).toFixed(3) == "0.000" && parseFloat(data[i][7]).toFixed(3) != "0.000") {
            doc.text(PrecioPromedio.toFixed(3), 490, xid + (n * xad), "right");
        }
        else {
            doc.text(parseFloat(data[i][8]).toFixed(3), 490, xid + (n * xad), "right");
        }
        //Entrada Total
        if (parseFloat(data[i][9]).toFixed(3) == "0.000") {
            doc.text(parseFloat(data[i][8]).toFixed(3), 530, xid + (n * xad), "right");
        }
        else {
            doc.text((PrecioPromedio * parseFloat(data[i][7])).toFixed(3), 530, xid + (n * xad), "right");
        }
        //Salida Cantidad
        doc.text(parseFloat(data[i][10]).toFixed(3), 570, xid + (n * xad), "right");
        //Salida Precio
        doc.text(parseFloat(data[i][11]).toFixed(3), 610, xid + (n * xad), "right");
        //Salida Total
        doc.text(parseFloat(data[i][12]).toFixed(3), 650, xid + (n * xad), "right");
        //Acum Total
        doc.text(CantidadAcumulada.toFixed(3), 690, xid + (n * xad), "right");
        //Acum Precio
        doc.text(PrecioPromedio.toFixed(3), 730, xid + (n * xad), "right");
        //Acum Total
        doc.text((CantidadAcumulada * PrecioPromedio).toFixed(3), 770, xid + (n * xad), "right");
        n += 1;
    }
    //doc.autoPrint();
    var iframe = document.getElementById('ifrmReport');
    iframe.src = doc.output('dataurlstring');
}


