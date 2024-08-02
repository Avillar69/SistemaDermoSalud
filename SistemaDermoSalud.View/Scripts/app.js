var ventanaActual = 1;
var listaDatos;
function ExportarPDF(orientation, titulo, cabeceras, matriz, nombre, tipo) {
    var texto = "";
    var columns = cabeceras;
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = matriz[i];
    }
    var doc = new jsPDF(orientation, 'pt', (tipo != undefined ? "a3" : "a4"));
    doc.text(titulo, 40, 50);
    doc.autoTable(columns, data, { startY: 60, overflow: 'linebreak', headerStyles: { styles: { overflow: 'linebreak' }, rowHeight: 15, fontSize: 8 }, bodyStyles: { rowHeight: 12, fontSize: 8, valign: 'middle' } });
    doc.save((nombre != undefined ? nombre : "table.pdf"));
}
function reziseTabla() {

    var div = document.getElementById("divTabla");
    var altoTotal = window.innerHeight;
    var puntoInicio = getPos(div).y;
    var altoFooter = 21;
    div.style.height = "" + (altoTotal - altoFooter - puntoInicio - 100) + "px";
}

function loadDataTable(cols, datos, rid, tid, btns, arrOrder, showFirstField) {
    var columnas = [];
    for (var i = 0; i < cols.length; i++) {
        let item = {
            data: cols[i]
        };
        columnas.push(item);
    }
    let itemBtn = {
        "data": null,
        "defaultContent": "<center>" + btns + "</center>"
    };
    columnas.push(itemBtn);
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
function cadButtonOptions() {
    let cad = "";
    cad += '<ul class="list-inline" style="margin-bottom: 0px;">';
    cad += '<li class="list-inline-item">';
    cad += '<div class="dropdown">';
    cad += '<button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">';
    cad += '<i class="ri-more-fill align-middle"></i>';
    cad += '</button>';
    cad += '<ul class="dropdown-menu dropdown-menu-end" style="">';
    cad += '<li>';
    cad += '<a class="dropdown-item edit-item-btn" href="javascript:void(0)" onclick="mostrarDetalle(2, this)" ><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>Editar</a>';
    cad += '</li>';
    cad += '<li>';
    cad += '<a class="dropdown-item remove-item-btn" data-bs-toggle="modal" href="javascript:void(0)" onclick="eliminar(this)"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>';
    cad += '</li>';
    cad += '</ul>';
    cad += '</div>';
    cad += '</li>';
    cad += ' </ul>';
    return cad;
}
function crearMatrizReporte(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    //var textos = document.getElementById("txtFiltro").value.trim();
    matrizReporte = [];
    var exito;
    if (listaDatos != "") {

        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            nCampos = campos.length;
            exito = true;

            if (exito) {
                matrizReporte[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matrizReporte[c][j] = campos[j];
                }
                c++;
            }
        }
    } else {
        document.getElementById("divTabla").innerHTML = "";
    }
    return matrizReporte;
}
function mostrarMatrizReportes(matriz, cabeceras, tabId, contentID) {
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
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 2:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 4:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        case 3:
                            contenido2 += "col-md-1 style='padding-top:5px;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-3 style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
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
function IngresoNumero(e) { // recordar un keyup es cuando se presione igual que el onkey press
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true; //Tecla de retroceso (para poder borrar)  
    patron = /[0-9.]+(\.[0-9.][0-9.]?)?/;// Solo acepta números y el punto 
    te = String.fromCharCode(tecla);
    return patron.test(te);
}
function crearTablaReportes(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 2: case 3: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            case 0:
                contenido += "              <div class='col-12 col-md-1'>";
                break;

            default:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
        }
        contenido += "                  <label>" + cabeceras[i] + "</label>";
        contenido += "              </div>";
    }
    contenido += "          </div>";

    var divTabla = gbi(div);
    divTabla.innerHTML = contenido;
}
function configNav() {
    $("#modal-Modal").keyup(function (e) {
        var key = (e.keyCode ? e.keyCode : e.charCode);
        switch (key) {
            case 13:
                if (ventanaActual == 2) {
                    if (e.target.id.includes("num")) {
                        funcionModal(e.target);
                    }
                }
                break;
            case 27://escape
                //var rowTabla = document.getElementById("rowTabla");
                //if (rowTabla && rowTabla.style.display == "none") {
                //    var rowFrm = document.getElementById("rowFrm");
                //    if (rowFrm) {
                //        show_hidden_Formulario();
                //    }
                //}
                //closePopup();
                break;
            case 113://f2
                mostrarDetalle(1);
                break;
            case 114://f3
                //navigateUrl($('a[id$=lnk3]'));
                break;
            case 115://f4 grabar
                var url = window.location.href;
                if (url.includes("Movimientos/IngresoCompras")) {
                    if (validarFormulario() && validarDetalle()) {
                        var url = "grabarIngresoCompra";
                        enviarServidorPost(url, actualizarListar);
                    }
                } else if (url.includes("Movimientos/IngresoVentas")) {
                    if (validarFormulario() && validarDetalle()) {
                        var url = "grabarIngresoVenta";
                        enviarServidorPost(url, actualizarListar);
                    }
                } else if (url.includes("Movimientos/IngresoDiario")) {
                    if (validarFormulario() && validarDetalle()) {
                        var url = "grabarIngresoDiario";
                        enviarServidorPost(url, actualizarListar);
                    }
                } else if (validarFormulario()) {
                    var url = "grabar";
                    enviarServidorPost(url, actualizarListar);
                }
                break;
            case 38://arriba
                if (ventanaActual == 2) {
                    if (e.target.id == "numMod0") {
                        gbi("numMod0").className = gbi("numMod0").className.replace(" saltactive", "");
                        gbi("txtFiltroMod").focus();
                    }
                    else {
                        var ob = document.querySelector(".saltactive");
                        if (ob) {
                            ob.className = ob.className.replace(" saltactive", "");
                            gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) - 1)).className += " saltactive";
                            gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) - 1)).focus();
                        } else if (document.activeElement.id === "txtFiltroMod") {
                            document.getElementById("contentArea").lastChild.className += " saltactive";
                            document.getElementById("contentArea").lastChild.focus();
                        }
                    }
                }
                break;
            case 40://abajo
                if (ventanaActual == 2) {
                    if (e.target.id == "txtFiltroMod") {
                        gbi("numMod0").className += " saltactive";
                        gbi("numMod0").focus();
                    }
                    else {
                        var ob = document.querySelector(".saltactive");
                        if (ob) {
                            if (gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) + 1))) {
                                ob.className = ob.className.replace(" saltactive", "");
                                gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) + 1)).className += " saltactive";
                                gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) + 1)).focus();
                            } else {
                                ob.className = ob.className.replace(" saltactive", "");
                                gbi("txtFiltroMod").focus();
                            }
                        }
                    }
                }
                break;
            default: ;
        }
    });
}
function gvt(i) {
    return gbi(i).value.trim() == "" ? " " : gbi(i).value;
}
function gvc(i) {
    return gbi(i).dataset.id;
}
function adFV(f, c) {
    f.append(c);
}
function getPos(el) {
    for (var lx = 0, ly = 0;
        el != null;
        lx += el.offsetLeft, ly += el.offsetTop, el = el.offsetParent);
    return { x: lx, y: ly };
}
function armarTablaReporte() {
    var cadena = "";
    cadena += "<table id='tbDatosCompletos'>";
    cadena += "<thead style='width:500px;'>";
    nCampos = cabeceras.length;
    cadena += "<tr style='height:30px;'>";
    for (var i = 0; i < nCampos; i++) {
        cadena += "<th class='center'>";
        cadena += cabeceras[i];
        cadena += "</th>";
    }
    cadena += "</tr></thead>";
    cadena += "<tbody id='tbTabla'>";
    var nRegistros = matriz.length;
    for (var i = 0; i < nRegistros; i++) {
        if (i < nRegistros) {
            cadena += "<tr class=''>";
            for (var j = 0; j < nCampos; j++) {
                cadena += "<td>";
                cadena += matriz[i][j];
                cadena += "</td>";
            }
            cadena += "</tr>";
        }
        else break;
    }
    cadena += "</tbody></table>";
    return cadena;
}
function fnExcelReport(cabeceras, matriz) {
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
    var nRegistros = matriz.length;
    for (var i = 0; i < nRegistros; i++) {
        if (i < nRegistros) {
            tab_text += "<tr>";
            for (var j = 0; j < nCampos; j++) {
                tab_text += "<td>";
                tab_text += matriz[i][j];
                tab_text += "</td>";
            }
            tab_text += "</tr>";
        }
        else break;
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
function validarControl(id) {
    var textbox = document.getElementById(id);
    if (textbox.value === null || textbox.value.length === 0 || /^\s+$/.test(textbox.value)) {
        textbox.style.border = "1px solid red";
        var labels = document.getElementsByTagName("LABEL");
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].htmlFor == id) {
                labels[i].style.color = "red";
                break;
            }
        }
        //textbox.labels[0].style.color = "red";
        textbox.focus();
        return true;
    } else {
        textbox.style.border = "";
        var labels = document.getElementsByTagName("LABEL");
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].htmlFor == id) {
                labels[i].style.color = "";
                break;
            }
        }
        //textbox.labels[0].style.color = "";
        return false;
    }
}
function limpiarControl(id) {
    var elemento = document.getElementById(id);
    elemento.value = "";
    elemento.style.border = "";
    var labels = document.getElementsByTagName("LABEL");
    for (var i = 0; i < labels.length; i++) {
        if (labels[i].htmlFor == id) {
            labels[i].style.color = "";
            if (elemento.dataset.id != undefined) {
                elemento.dataset.id = 0;
            }
            if (elemento.tagName == "SELECT" && elemento.dataset.id != undefined) {
                elemento.dataset.id = "";
            }
            break;
        }
    }
}
function validarAutocompletar(id, mensaje) {
    var textbox = document.getElementById(id);
    if (textbox.value === null || textbox.value.length === 0 || /^\s+$/.test(textbox.value)) {
        textbox.parentNode.parentNode.parentNode.setAttribute("class", "form-group-sm has-error");
        textbox.focus();
        return true;
    } else {
        textbox.parentNode.parentNode.parentNode.setAttribute("class", "form-group-sm");
        return false;
    }
}
function quitarValidacion(id, mensaje) {
    var textbox = document.getElementById(id);
    if (textbox.value === null || textbox.value.length === 0 || /^\s+$/.test(textbox.value)) {
        textbox.style.border = "";
        textbox.parentNode.parentNode.children[0].style.color = "";
        return true;
    }
}
function quitarValidacionAutocompletar(id, mensaje) {
    var textbox = document.getElementById(id);
    if (textbox.value === null || textbox.value.length === 0 || /^\s+$/.test(textbox.value)) {
        textbox.parentNode.parentNode.parentNode.setAttribute("class", "form-group-sm");
        return true;
    }
}
function getSelectText(elem) {
    var elt = document.getElementById(elem);

    if (elt.selectedIndex == -1)
        return null;

    return elt.options[elt.selectedIndex].text;
}
function validarCombo(id, mensaje) {
    var combo = document.getElementById(id);
    if (combo.value === null || combo.value === 0 || combo.value === "") {
        combo.parentNode.parentNode.setAttribute("class", "form-group-sm has-error");
        return true;
    } else {
        combo.parentNode.parentNode.setAttribute("class", "form-group-sm");
        return false;
    }
}
function AbrirModal(idModal) {
    ventanaActual = 2
    $('#' + idModal).modal('show');
}
function CerrarModal(idModal) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
}
function enviarServidorPost(url, metodo, frm) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open("post", url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
    };
    xhr.send(frm);
}
function enviarServidor(url, metodo) {
    var xhr;
    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhr.open("get", url);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            metodo(xhr.responseText);
        }
        if (xhr.status == 404) {
        }
    };
    xhr.send();
}
function llenarCombo(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {
        campos = lista[i].split("▲");
        contenido += "<option value='" + campos[0] + "' " + (nRegistros == 1 ? "selected" : "") + ">" + campos[1] + "</option>";
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}
function configurarDobleClick() {

    $('#tbTabla').on('contextmenu', 'tr', function (e) {
        var value = $(this).find('td:nth-child(3)').html();
        $("#tbDatos tr").css("background-color", "#f5f5f5");
        $(this).css("background-color", "rgba(9,182,235,0.43)");
        var Menu = document.getElementById("divMenu");
        Menu.style.position = "fixed";
        Menu.style.top = "" + e.clientY + "px";
        Menu.style.left = "" + e.clientX + "px";
        Menu.style.display = "inherit";
        Menu.style.zIndex = "9999";
        document.getElementById("mnEdit").onclick = function () { mostrarDetalle(2, value.toString().trim()); document.getElementById("divMenu").style.display = "none"; };
        document.getElementById("mnDel").onclick = function () { eliminar(value.toString().trim()); document.getElementById("divMenu").style.display = "none"; };
        return false;
    });
    $("#tbDatos tr").dblclick(function () {
        $(this).addClass('selected').siblings().removeClass('selected');
        var value = $(this).find('td:nth-child(3)').html();
        mostrarDetalle(2, value.toString().trim());
    });
    $(document).mouseup(function (e) {
        var container = $("#divMenu");
        if (!container.is(e.target)
            && container.has(e.target).length === 0) {
            $("#tbDatos tr").hover(function () {
                $(this).css("background-color", "rgba(9,182,235,0.43)");
            }, function () {
                $(this).css("background-color", "#f5f5f5");
            });
            container.hide();
        }
    });
}
function crearMatrizModal(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    var textos = document.getElementById("txtFiltroMod").value.trim();
    matrizModal = [];
    var exito;
    if (listaDatos != "") {
        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            if (campos.length == 2) {
                nCampos = 2
            }
            else {
                switch (campos.length) {
                    case 3: nCampos = 3; break;
                    case 4: nCampos = 4; break;
                    case 5: nCampos = 5; break;
                    case 6: nCampos = 6; break;
                    default: nCampos = 3;
                }
                //nCampos = 3
            }
            exito = true;
            if (textos.trim() != "") {
                for (var l = 1; l < nCampos; l++) {
                    exito = true;
                    exito = exito && campos[l].toLowerCase().indexOf(textos.toLowerCase()) != -1;
                    if (exito) break;
                }
            }
            if (exito) {
                matrizModal[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matrizModal[c][j] = campos[j];
                }
                c++;
            }
        }
    }
    return matrizModal;
}
function crearMatrizModalDoc(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    var textos = document.getElementById("txtFiltroMod").value.trim();
    matrizModal = [];
    var exito;
    if (listaDatos != "") {
        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            if (campos.length == 2) {
                nCampos = 2
            }
            else {
                switch (campos.length) {
                    case 3: nCampos = 3; break;
                    case 4: nCampos = 4; break;
                    case 5: nCampos = 5; break;
                    case 6: nCampos = 6; break;
                    default: nCampos = 3;
                }
                //nCampos = 3
            }
            exito = true;
            if (textos.trim() != "") {
                for (var l = 1; l < nCampos; l++) {
                    exito = true;
                    exito = exito && campos[l].toLowerCase().indexOf(textos.toLowerCase()) != -1;
                    if (exito) break;
                }
            }
            if (exito) {
                matrizModal[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matrizModal[c][j] = campos[j];
                }
                c++;
            }
        }
    }
    return matrizModal;
}
function crearMatriz(listaDatos) {
    var nRegistros = listaDatos.length;
    var nCampos;
    var campos;
    var c = 0;
    var textos = document.getElementById("txtFiltro").value.trim();
    matriz = [];
    var exito;
    if (listaDatos != "") {

        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatos[i].split("▲");
            nCampos = campos.length;
            exito = true;
            if (textos.trim() != "") {
                for (var l = 1; l < nCampos; l++) {
                    exito = true;
                    exito = exito && campos[l].toLowerCase().indexOf(textos.toLowerCase()) != -1;
                    if (exito) break;
                }
            }
            if (exito) {
                matriz[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matriz[c][j] = campos[j];
                }
                c++;
            }
        }
    } else {
        if (document.getElementById("contentPrincipal"))
            document.getElementById("contentPrincipal").innerHTML = "";
    }
    return matriz;
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "grabar";
            enviarServidorPost(url, actualizarListar);
        }
    };
}
function mostrarTotal(matriz) {
    var spnMensaje = document.getElementById("spnTotalRegistros");
    spnMensaje.innerHTML = matriz.length;
}
function mostrarRespuesta(titulo, mensaje, tipo) {
    Swal.fire(titulo, mensaje, tipo);
}
function crearTabla(cabeceras) {
    var nCampos;
    var campos;
    var contenido = "<table style='width:100%;'class='table table-responsive table-bordered table-condensed table-hover'><thead id='tbCabecera'>";
    nCampos = cabeceras.length;
    contenido += "<tr class='bg-primary' style=';height:50px;padding-top:0px;'>";
    contenido += "<th class='center' style='width:42px'>";
    contenido += "</th>";
    contenido += "<th class='center' style='width:42px'>";
    contenido += "</th>";
    for (var i = 0; i < nCampos; i++) {
        if (i == 0) {
            contenido += "<th class='center' style='display:none'>";
        } else {
            contenido += "<th class='center' style='padding-bottom:3px'>";
        }
        contenido += "<span href='#' style='color:white;font-weight:lighter;font-size:12px;' id='a";
        contenido += i.toString();
        contenido += "'>";
        contenido += cabeceras[i];
        contenido += "</a><span class='span' id='spn";
        contenido += i.toString();
        //if (i == 1) {
        //contenido += "'></span><br/><input class='texto hide-on-med-and-down center' style='width:100%'/></th>";
        //} else {
        //    contenido += "'></span><br/><input class='texto hide-on-med-and-down center'/></th>";
        //}
        contenido += "'></span><br/><input class='texto hide-on-med-and-down center form-control input-sm' style='width:100%'/></th>";
    }
    contenido += "</tr>";
    var divTabla = document.getElementById("tbCabecera");
    divTabla.innerHTML = contenido;
    divTabla.style.overflowY = "scroll";
}
function mostrarMatriz(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 2:
                            contenido2 += "col-md-4' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
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
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o fs-11'></i> </button>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil fs-11'></i></button>";
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
function configurarPaginacion(matriz, registrosPagina, paginasBloque, indiceActualBloque, indiceActualPagina) {
    var contenido = "";
    var nRegistros = matriz.length;
    var indiceUltimaPagina = Math.floor(nRegistros / registrosPagina);
    if (nRegistros % registrosPagina == 0) indiceUltimaPagina--;
    var indiceUltimoBloque = Math.floor(nRegistros / (registrosPagina * paginasBloque));
    if (nRegistros % (registrosPagina * paginasBloque) == 0) indiceUltimoBloque--;
    var existeBloques = matriz.length > (paginasBloque * registrosPagina);
    if (existeBloques && indiceActualBloque > 0) {
        contenido += "<li><a href='#' onclick='mostrarMatriz(-1, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);'><span class='fa fa-angle-double-left'></span></a></li>";
        contenido += "<li><a href='#' onclick='mostrarMatriz(-2, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);'><span class='fa fa-angle-left'></span></a></li>";
    }
    var inicio = indiceActualBloque * paginasBloque;
    var fin = inicio + paginasBloque;
    for (var i = inicio; i < fin; i++) {
        if (i <= indiceUltimaPagina) {
            contenido += "<li><a href='#' onclick='mostrarMatriz(";
            contenido += i.toString();
            contenido += ",registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);' id='a";
            contenido += i.toString();
            contenido += "'>";
            contenido += (i + 1).toString();
            contenido += "</a></li>";
        } else break;
    }
    if (existeBloques && indiceActualBloque < indiceUltimoBloque) {
        contenido += "<li><a href='#' onclick='mostrarMatriz(-3, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);'><span class='fa fa-angle-right'></span></a></li>";
        contenido += "<li><a href='#' onclick='mostrarMatriz(-4, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);'><span class='fa fa-angle-double-right'></span></a></li>";
    }
    var ulPagina = document.getElementById("ulPagina");
    ulPagina.innerHTML = contenido;
}
function configurarFiltro(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatriz(matriz, cabe, "divTabla", "contentPrincipal");
    };
}
/*
$("#modal-Modal").keyup(function (e) {
    var key = (e.keyCode ? e.keyCode : e.charCode);
    switch (key) {
        case 13:
            if (ventanaActual == 2) {
                if (e.target.id.includes("num")) {
                    funcionModal(e.target);
                }
            }
            break;
        case 27://escape
            //var rowTabla = document.getElementById("rowTabla");
            //if (rowTabla && rowTabla.style.display == "none") {
            //    var rowFrm = document.getElementById("rowFrm");
            //    if (rowFrm) {
            //        show_hidden_Formulario();
            //    }
            //}
            //closePopup();
            break;
        case 113://f2
            mostrarDetalle(1);
            break;
        case 114://f3
            //navigateUrl($('a[id$=lnk3]'));
            break;
        case 115://f4 grabar
            var url = window.location.href;
            if (url.includes("Movimientos/IngresoCompras")) {
                if (validarFormulario() && validarDetalle()) {
                    var url = "grabarIngresoCompra";
                    enviarServidorPost(url, actualizarListar);
                }
            } else if (url.includes("Movimientos/IngresoVentas")) {
                if (validarFormulario() && validarDetalle()) {
                    var url = "grabarIngresoVenta";
                    enviarServidorPost(url, actualizarListar);
                }
            } else if (url.includes("Movimientos/IngresoDiario")) {
                if (validarFormulario() && validarDetalle()) {
                    var url = "grabarIngresoDiario";
                    enviarServidorPost(url, actualizarListar);
                }
            } else if (validarFormulario()) {
                var url = "grabar";
                enviarServidorPost(url, actualizarListar);
            }
            break;
        case 38://arriba
            if (ventanaActual == 2) {
                if (e.target.id == "numMod0") {
                    gbi("numMod0").className = gbi("numMod0").className.replace(" saltactive", "");
                    gbi("txtFiltroMod").focus();
                }
                else {
                    var ob = document.querySelector(".saltactive");
                    if (ob) {
                        ob.className = ob.className.replace(" saltactive", "");
                        gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) - 1)).className += " saltactive";
                        gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) - 1)).focus();
                    }
                }
            }
            break;
        case 40://abajo
            if (ventanaActual == 2) {
                if (e.target.id == "txtFiltroMod") {
                    gbi("numMod0").className += " saltactive";
                    gbi("numMod0").focus();
                }
                else {
                    var ob = document.querySelector(".saltactive");
                    if (ob) {
                        ob.className = ob.className.replace(" saltactive", "");
                        gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) + 1)).className += " saltactive";
                        gbi("numMod" + (parseInt(ob.id.replace("numMod", "")) + 1)).focus();
                    }
                }
            }
            break;
        default: ;
    }
});
*/
function isDecimal(evt, element) {
    if (evt.keyCode == 13) {
        var elem;
        elem = document.activeElement;
        var tidx = +(elem.getAttribute('tabindex')) + 1,
            elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == tidx) elems[i].focus();
        }
    }
    else {
        var charCode = evt.which || event.keyCode;
        if (charCode === 46 && $(element).val().length === 0) {
            return false;
        }
        else {
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode === 46 || charCode === 8)) {
                return false;
            }
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode === 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = parseInt(parseInt(len + 1) - index);
                    if (CharAfterdot > 4) {
                        return false;
                    }
                }

            }
            return true;
        }
    }
}
function darFormatoFecha(cadena) {
    var fechaSplit = (cadena.substring(0, 10)).split("-");
    var fecha = fechaSplit[2] + "-" + fechaSplit[1] + "-" + fechaSplit[0];
    return fecha;
}
function isLetterKey(evt, element) {
    if (evt.keyCode == 13) {
        var elem;
        elem = document.activeElement;
        var tidx = +(elem.getAttribute('tabindex')) + 1,
            elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == tidx) elems[i].focus();
        }
    }
    else {

        var charCode = evt.which || event.keyCode;
        if (charCode === 46 && $(element).val().length === 0) {
            return false;
        }
        else {
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return true;
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode === 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = parseInt(parseInt(len + 1) - index);
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return false;
        }
    }
}
//var divTabla = document.getElementById("divTabla");
//if (divTabla) { divTabla.title = "Click Derecho para ver Opciones"; }
function show_hidden_Formulario(filtro) {
    var frm = document.getElementById("rowFrm");
    var frmFilt = document.getElementById("rowFilter");
    if (frm.style.display == "none") {
        document.getElementById("rowTabla").style.display = "none";
        $("#rowFrm").fadeIn();
    } else {
        frm.style.display = "none";
        $("#rowTabla").fadeIn();
    }
    if ($('.content-header-right')) {
        $('.content-header-right').fadeIn();
    }
    //reziseTabla();
}
function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2: case 3:
                contenido += "              <div class='col-12 col-md-4'>";
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
function crearTablaModalDoc(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-5'>";
                break;
            case 3:
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
function gbi(t) {
    return document.getElementById(t);
}
function configurarFiltroModal(listaDatosModal, cabecera_Modal) {

    var texto = document.getElementById("txtFiltroMod");
    texto.onkeyup = function () {
        matriz = crearMatrizModal(listaDatosModal);
        indiceActualBloquem = 0;
        indiceActualPaginam = 0;
        mostrarMatrizModal(matriz, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal);
    };
}
function mostrarMatrizModal(matriz, cabeceras, tabId, contentID, confdbc) {
    if (matriz.length == 0) {

    } else {
        var esBloque = false;
        var contenido = "";
        var nRegistros = matriz.length;
        if (nRegistros > 0) {
            nRegistros = matriz.length;
            var nCampos = matriz[0].length;
            var tbTabla = document.getElementById(tabId);
            tbTabla.style.cursor = "pointer";
            var tipocol = "";
            var tipoColDes = "";
            var tipoColum = "";
            switch (cabeceras.length) {
                case 2:
                    tipoColDes = 12;
                    tipoColum = 12;
                    break;
                case 3:
                    tipocol = 4;
                    tipoColDes = 8;
                    tipoColum = 8
                    break;

                case 4:
                    tipocol = 3
                    tipoColDes = 3
                    tipoColum = 6
                    break
                case 5:
                    tipocol = 2
                    tipoColDes = 4;
                    tipoColum = 4;
                    break;
                case 6:
                    tipocol = 2
                    tipoColDes = 4;
                    tipoColum = 4;
                    break;
                default:

            }
            var dat = [];
            for (var i = 0; i < nRegistros; i++) {
                if (i < nRegistros) {
                    var contenido2 = "<div class='row panel salt' id='numMod" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;cursor:pointer;'>";
                    for (var j = 0; j < cabeceras.length; j++) {
                        switch (j) {
                            case 0: case 6:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += ("<div class='col-12 col-md-" + (cabeceras.length == 2 ? tipoColDes : tipocol) + "'>");
                                break;
                            case 2:
                                contenido2 += ("<div class='col-12 col-md-" + tipoColDes) + "'>";
                                break;
                            case 3:
                                contenido2 += ("<div class='col-12 col-md-" + tipoColum) + "'>";
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-" + tipocol + "'>");
                                break;
                        }
                        contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='md" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
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
        confdbc();
    }
}
function mostrarMatrizModalDoc(matriz, cabeceras, tabId, contentID, confdbc) {
    if (matriz.length == 0) {

    } else {
        var esBloque = false;
        var contenido = "";
        var nRegistros = matriz.length;
        if (nRegistros > 0) {
            nRegistros = matriz.length;
            var nCampos = matriz[0].length;
            var tbTabla = document.getElementById(tabId);
            tbTabla.style.cursor = "pointer";
            var dat = [];
            for (var i = 0; i < nRegistros; i++) {
                if (i < nRegistros) {
                    var contenido2 = "<div class='row panel salt' id='numMod" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;cursor:pointer;'>";
                    for (var j = 0; j < cabeceras.length; j++) {
                        switch (j) {
                            case 0:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                            case 2:
                                contenido2 += ("<div class='col-12 col-md-5'>");
                                break;
                            case 3:
                                contenido2 += ("<div class='col-12 col-md-3'>");
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                        }
                        contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='md" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
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
        confdbc();
    }
}
function configurarDobleClickModal() {
    $("#divTabla_Modal .salt").dblclick(function () {
        funcionModal(this);
    });
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
    function cbm(ds, t, tM, tM2, cab, dat, m) {
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
        m(dat);
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
//Carga con botones de Modal sin URL (Con datos dat[])
function cbm(ds, t, tM, tM2, cab, dat, m) {
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
    m(dat);
}
function accionModal(url, tr, id) {//esta funcion puede crecer como ******
    if (url.includes("OperacionesStock")) {
        //segun el id del txtModal hacer algo
        if (txtModal.id == "txtArticulo") {
            var marca = $(tr).find('td:nth-child(4)').html();
            var unidadMedida = $(tr).find('td:nth-child(6)').html();
            var txtMarca = document.getElementById("txtMarca");
            var txtUnidadMedida = document.getElementById("txtUnidadMedida");
            if (txtMarca && txtUnidadMedida) {
                txtMarca.value = marca;
                txtUnidadMedida.value = unidadMedida;
            }
            var cboAlmacenDestino = document.getElementById("cboAlmacenDestino");
            if (cboAlmacenDestino) {
                var url_2 = "OperacionesStock/cargarStock?idArticulo=" + id + "&idAlmacenO=" + cboAlmacen.value + "&idAlmacenD=" + cboAlmacenDestino.value;
            } else {
                var url_2 = "OperacionesStock/cargarStock?idArticulo=" + id + "&idAlmacenO=" + cboAlmacen.value;
            }
            enviarServidor(url_2, cargarStock);
        }
        if (txtModal.id == "txtAlmacen") {
            var txtArticulo = document.getElementById("txtArticulo");
            if (txtArticulo) {
                var url = "OperacionesStock/ObtenerStockActual?idArticulo=" + txtArticulo.dataset.id + "&idAlmacen=" + id;
                enviarServidor(url, ObtenerStockActual);
            }
        }
        if (txtModal.id == "txtAlmacenO") {
            var txtArticulo = document.getElementById("txtArticulo");
            if (txtArticulo) {
                var url = "OperacionesStock/ObtenerStockActual?idArticulo=" + txtArticulo.dataset.id + "&idAlmacen=" + id;
                enviarServidor(url, ObtenerStockActualO);
            }
        }
        if (txtModal.id == "txtAlmacenD") {
            var txtArticulo = document.getElementById("txtArticulo");
            if (txtArticulo) {
                var url = "OperacionesStock/ObtenerStockActual?idArticulo=" + txtArticulo.dataset.id + "&idAlmacen=" + id;
                enviarServidor(url, ObtenerStockActualD);
            }
        }
    }
}
//Modal cargar y grabar
function cargarLista(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
}
function cargarListaDoc(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        mostrarModalDoc(cabecera_Modal, listaDatosModal);
    }
}
function cargarSinXR(dat) {
    if (dat != "") {
        mostrarModal(cabecera_Modal, dat);
    }
}
function mostrarModal(cabecera_Modal, lista) {
    if (lista.length == 1 && lista[0].trim().length == 0) {
        mostrarRespuesta("Mensaje", "No se encontraron resultados para esta opción", "info")
    }
    else {
        crearTablaModal(cabecera_Modal, "divTablaCabecera_Modal");
        configurarFiltroModal(lista, cabecera_Modal);
        matrizModal = crearMatrizModal(lista);
        $.when(mostrarMatrizModal(matrizModal, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal)).then(
            function () {
                AbrirModal('modal-Modal');
            });
        setTimeout(function () { gbi("txtFiltroMod").focus() }, 200);
    }
}
function mostrarModalDoc(cabecera_Modal, lista) {
    if (lista.length == 1 && lista[0].trim().length == 0) {
        mostrarRespuesta("Mensaje", "No se encontraron resultados para esta opción", "info")
    }
    else {
        crearTablaModalDoc(cabecera_Modal, "divTablaCabecera_Modal");
        //configurarFiltroModal(lista, cabecera_Modal);
        matrizModal = crearMatrizModalDoc(lista);
        $.when(mostrarMatrizModalDoc(matrizModal, cabecera_Modal, "divTabla_Modal", "contentArea", configurarDobleClickModal)).then(
            function () {
                AbrirModal('modal-Modal');
            });
        setTimeout(function () { gbi("txtFiltroMod").focus() }, 200);
    }
}
function rpta_GrabarModal(rpta) {
    var Resultado = rpta.split("↔")[0];
    var id = rpta.split("↔")[3];
    if (Resultado == "OK") {
        mostrarRespuesta(Resultado, "", 'success');
        txtModal.value = txtValor.value;
        txtModal.dataset.id = id;
        CerrarModal("modal-Modal");
    } else {
        mostrarRespuesta(Resultado, "", 'error');
        CerrarModal("modal-Modal");
    }
}
function validarFormularioModal(modal) {
    var error = true;
    switch (modal) {
        case "unidadMedida":
            if (validarControl("txtCodigo_Marca")) error = false;
            if (validarControl("txtDescripcion_Marca")) error = false;
            break;
        case "banco":
            if (validarControl("txtDescripcion_Banco")) error = false;
            break;
        case "almacen":
            if (validarControl("txtDescripcion_Almacen")) error = false;
            break;
        default:
            //if (validarControl("txtDescripcion_Marca")) error = false;
            break;
    }
    return error;
}
function isNumberKey(evt, element) {
    if (evt.keyCode == 13) {
        var elem;
        elem = document.activeElement;
        var tidx = +(elem.getAttribute('tabindex')) + 1,
            elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == tidx) elems[i].focus();
        }
    }
    else {
        var charCode = evt.which || event.keyCode;
        if (charCode === 46 && $(element).val().length === 0) {
            return false;
        }
        else {
            if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode === 46 || charCode === 8))
                return false;
            else {
                var len = $(element).val().length;
                var index = $(element).val().indexOf('.');
                if (index > 0 && charCode === 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = parseInt(parseInt(len + 1) - index);
                    if (CharAfterdot > 4) {
                        return false;
                    }
                }

            }
            return true;
        }

    }

}
function bDM(v) {
    gbi(v).value = "";
    gbi(v).dataset.id = "";
}
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}
function adc(l, id, ctrl, c) {
    var ind;
    for (var i = 0; i < l.length; i++) {
        if (l[i].split('▲')[0] == id) {
            ind = i;
            break;
        }
    }
    gbi(ctrl).value = l[ind].split('▲')[c];
    gbi(ctrl).dataset.id = id;
}
var numeroALetras = (function () {

    // Código basado en https://gist.github.com/alfchee/e563340276f89b22042a
    function Unidades(num) {

        switch (num) {
            case 1: return 'UN';
            case 2: return 'DOS';
            case 3: return 'TRES';
            case 4: return 'CUATRO';
            case 5: return 'CINCO';
            case 6: return 'SEIS';
            case 7: return 'SIETE';
            case 8: return 'OCHO';
            case 9: return 'NUEVE';
        }

        return '';
    }//Unidades()

    function Decenas(num) {

        let decena = Math.floor(num / 10);
        let unidad = num - (decena * 10);

        switch (decena) {
            case 1:
                switch (unidad) {
                    case 0: return 'DIEZ';
                    case 1: return 'ONCE';
                    case 2: return 'DOCE';
                    case 3: return 'TRECE';
                    case 4: return 'CATORCE';
                    case 5: return 'QUINCE';
                    default: return 'DIECI' + Unidades(unidad);
                }
            case 2:
                switch (unidad) {
                    case 0: return 'VEINTE';
                    default: return 'VEINTI' + Unidades(unidad);
                }
            case 3: return DecenasY('TREINTA', unidad);
            case 4: return DecenasY('CUARENTA', unidad);
            case 5: return DecenasY('CINCUENTA', unidad);
            case 6: return DecenasY('SESENTA', unidad);
            case 7: return DecenasY('SETENTA', unidad);
            case 8: return DecenasY('OCHENTA', unidad);
            case 9: return DecenasY('NOVENTA', unidad);
            case 0: return Unidades(unidad);
        }
    }//Unidades()

    function DecenasY(strSin, numUnidades) {
        if (numUnidades > 0)
            return strSin + ' Y ' + Unidades(numUnidades)

        return strSin;
    }//DecenasY()

    function Centenas(num) {
        let centenas = Math.floor(num / 100);
        let decenas = num - (centenas * 100);

        switch (centenas) {
            case 1:
                if (decenas > 0)
                    return 'CIENTO ' + Decenas(decenas);
                return 'CIEN';
            case 2: return 'DOSCIENTOS ' + Decenas(decenas);
            case 3: return 'TRESCIENTOS ' + Decenas(decenas);
            case 4: return 'CUATROCIENTOS ' + Decenas(decenas);
            case 5: return 'QUINIENTOS ' + Decenas(decenas);
            case 6: return 'SEISCIENTOS ' + Decenas(decenas);
            case 7: return 'SETECIENTOS ' + Decenas(decenas);
            case 8: return 'OCHOCIENTOS ' + Decenas(decenas);
            case 9: return 'NOVECIENTOS ' + Decenas(decenas);
        }

        return Decenas(decenas);
    }//Centenas()

    function Seccion(num, divisor, strSingular, strPlural) {
        let cientos = Math.floor(num / divisor)
        let resto = num - (cientos * divisor)

        let letras = '';

        if (cientos > 0)
            if (cientos > 1)
                letras = Centenas(cientos) + ' ' + strPlural;
            else
                letras = strSingular;

        if (resto > 0)
            letras += '';

        return letras;
    }//Seccion()

    function Miles(num) {
        let divisor = 1000;
        let cientos = Math.floor(num / divisor)
        let resto = num - (cientos * divisor)

        let strMiles = Seccion(num, divisor, 'UN MIL', 'MIL');
        let strCentenas = Centenas(resto);

        if (strMiles == '')
            return strCentenas;

        return strMiles + ' ' + strCentenas;
    }//Miles()

    function Millones(num) {
        let divisor = 1000000;
        let cientos = Math.floor(num / divisor)
        let resto = num - (cientos * divisor)

        let strMillones = Seccion(num, divisor, 'UN MILLON DE', 'MILLONES DE');
        let strMiles = Miles(resto);

        if (strMillones == '')
            return strMiles;

        return strMillones + ' ' + strMiles;
    }//Millones()

    return function NumeroALetras(num, currency) {
        currency = currency || {};
        let data = {
            numero: num,
            enteros: Math.floor(num),
            centavos: (((Math.round(num * 100)) - (Math.floor(num) * 100))),
            letrasCentavos: '',
            letrasMonedaPlural: currency.plural || 'PESOS CHILENOS',//'PESOS', 'Dólares', 'Bolívares', 'etcs'
            letrasMonedaSingular: currency.singular || 'PESO CHILENO', //'PESO', 'Dólar', 'Bolivar', 'etc'
            letrasMonedaCentavoPlural: currency.centPlural || 'CHIQUI PESOS CHILENOS',
            letrasMonedaCentavoSingular: currency.centSingular || 'CHIQUI PESO CHILENO'
        };

        //if (data.centavos > 0) {
        var ceroLetra = (parseFloat(num) == 0 ? "CERO " : "");

        data.letrasCentavos = ceroLetra + 'CON ' + (function () {
            if (data.centavos == 1)
                return data.letrasMonedaCentavoSingular;
            else
                return data.letrasMonedaCentavoPlural;
        })();
        //};

        //if (data.enteros == 0)
        //    return 'CERO ' + data.letrasMonedaPlural + ' ' + data.letrasCentavos;
        //if (data.enteros == 1)
        //    return Millones(data.enteros) + ' ' + data.letrasMonedaSingular + ' ' + data.letrasCentavos;
        //else
        return Millones(data.enteros) + ' ' + data.letrasCentavos + ' ' + data.letrasMonedaPlural;
    };

})();
configNav();

$('input').attr('autocomplete', 'off');