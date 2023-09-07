var listaDatos;
var matriz = [];
var listaLocales;
var listaAlmacenes;

$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});

cfgKP(["txtCategoria", "txtArticulo"], cfgTMKP);
//configNav();
configBM();
reziseTabla();

function cfgKP(l, m) {
    for (var i = 0; i < l.length; i++) {
        gbi(l[i]).onkeyup = m;
    }
}
function cfgTMKP(evt) {
    var o = evt.srcElement.id;
    if (evt.keyCode === 13) {
        var n = o.replace("txt", "");
        gbi("btnModal" + n).click();
    }
}
function cbmu(ds, t, tM, tM2, cab, u, m) {
    document.getElementById("div_Frm_Modal").innerHTML = document.getElementById("div_Frm_Detalle").innerHTML;
    document.getElementById("btnGrabar_Modal").dataset.grabar = ds;
    document.getElementById("lblTituloModal").innerHTML = t;
    var txtCodigo_FormaPago = document.getElementById("txtCodigo_Detalle");
    txtCodigo_FormaPago.disabled = true;
    txtCodigo_FormaPago.placeholder = "Autogenerado";
    var txtM1 = document.getElementById(tM);
    txtModal = txtM1;
    tM2 == null ? txtModal2 = tM2 : txtModal2 = document.getElementById(tM2);
    cabecera_Modal = cab;
    enviarServidor(u, m);
}
function configBM() {
    var btnModalCategoria = gbi("btnModalCategoria");
    btnModalCategoria.onclick = function () {
        cbmu("categoria", "Categoría", "txtCategoria", null,
            ["idLaboratorio", "Descripción"], "/Laboratorio/ObtenerDatos", cargarLista);
    }
    var btnBuscar = gbi("btnBuscar");
    btnBuscar.onclick = function () {
        var error = true;
        if (validarControl("txtFilFecIn")) error = false;
        if (validarControl("txtFilFecFn")) error = false;
        //if (validarControl("txtCategoria")) error = false;
        if (!error) return;
        var url = "/Kardex/BuscarKardex?fI=" + gvt("txtFilFecIn") + "&fF=" + gvt("txtFilFecFn") + "&iC=0" + "&iA=" + (gbi("txtArticulo").dataset.id == undefined ? "0" : gbi("txtArticulo").dataset.id);
        enviarServidor(url, MostrarBusqueda)
    };
    //var btnModalArticulo = document.getElementById("btnModalArticulo");
    //btnModalArticulo.onclick = function () {
    //    cbmu("Medicamento", "Medicamento", "txtArticulo", null,
    //        ["idMedicamentos", "Codigo", "Descripcion", "Laboratorio", "Precio"], ' /OperacionesStock/cargarMedicamento', cargarListaArticulo);
    //};
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        window.addEventListener('focus', window_focus, false);
        function window_focus() {
            //ocultarLoader();
        }

        try {
            var cabecera = "Fecha,Doc,Articulo,U.M.,Stock Inicial,Precio,Cant. Entrada,Precio Entrada,Total Entrada";
            window.location = "Kardex/exportarExcel?local=1&fechaInicio=" + gvt("txtFilFecIn") + "&fechaFin=" + gvt("txtFilFecFn") + "&cabecera=" + cabecera;
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
function funcionModal(tr) {
    var num = tr.id.replace("numMod", "");
    var id = gbi("md" + num + "-0").innerHTML;
    var value;
    if (tr.children.length === 2) {
        value = gbi("md" + num + "-1").innerHTML;
    }
    else {
        value = gbi("md" + num + "-2").innerHTML;
    }
    var value2 = gbi("md" + num + "-1").innerHTML;
    txtModal.value = value;
    txtModal.dataset.id = id;
    var next = accionModal2(tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}

function accionModal2(tr, id) {
    switch (txtModal.id) {
        case "txtCategoria":
            gbi("txtArticulo").value = "";
            gbi("txtArticulo").dataset = "";

            var btnModalArticulo = document.getElementById("btnModalArticulo");
            btnModalArticulo.onclick = function () {
                cbmu("Medicamento", "Medicamento", "txtArticulo", null,
                    ["idMedicamentos", "Descripcion"], ' /OperacionesStock/cargarMedicamentoxLaboratorio?idLab=' + gbi("txtCategoria").dataset.id, cargarListaArticulo);
            };
            return gbi("txtArticulo");
            break;
        case "txtArticulo":
            return gbi("btnBuscar");
            break;
        default:
            break;
    }
}
function CerrarModal(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}
function cargarListaArticulo(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        console.log(listaDatosModal);
        mostrarModal(cabecera_Modal, listaDatosModal);
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
    doc.text("Dermosalud S.A.C", 30, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 30, 40);
    doc.text("20565643143", 50, 40);
    doc.text("Dirección:", 30, 50);
    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 70, 50);
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
