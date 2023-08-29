var cabeceras = ["idLaboratorio", "Laboratorio", "FechaCreacion", "Estado"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var listaTipoServicio;
var matriz = [];
var url = "Laboratorio/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();
var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtLaboratorio = document.getElementById("txtLaboratorio");
var chkActivo = document.getElementById("chkActivo");

function mostrarLista(rpta) {
    crearTablaServicio(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listar();
        }
    }
}
function crearTablaServicio(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
            case 6:
                contenido += "              <div class='col-12 col-md-1'>";
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
function listar() {
    ;
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatrizServicio(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function mostrarMatrizServicio(matriz, cabeceras, tabId, contentID) {
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
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-4' style='padding-top:5px;'>";
                            break;
                        case 5:
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
function TraerDetalle(id) {
    var url = "Laboratorio/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function actualizarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;
        if (codigo.length == 0) {
            if (res == "OK") {
                mensaje = "Se adicionó al nuevo Laboratorio";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó el Laboratorio";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Laboratorio/Grabar";
            var frm = new FormData();
            frm.append("idLaboratorio", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("Laboratorio", txtLaboratorio.value);
            frm.append("Estado", chkActivo.checked);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    };
}
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        txtID.value = listaDetalle[0];
        txtCodigo.value = listaDetalle[0];
        txtLaboratorio.value = listaDetalle[1]
        chkActivo.checked = listaDetalle[6] == "ACTIVO" ? true : false;
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            //AbrirModal('modal-form');
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Laboratorio";//Titulo Insertar
            gbi("txtLaboratorio").focus();
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Laboratorio";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario();
            gbi("txtLaboratorio").focus();
            break;
    }
}

function limpiarTodo() {
    gbi("txtID").value = "";
    txtCodigo.value = "";
    txtLaboratorio.value = "";
    chkActivo.checked = true;
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtLaboratorio")) error = false;
    return error;
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar este Laboratorio?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "Laboratorio/Eliminar?idLaboratorio=" + id;
                enviarServidor(url, eliminarListar);
            } else {
                swal("Cancelado", "No se elminó el Laboratorio", "error");
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el Laboratorio";
            tipo = "success";
            //CerrarModal("modal-form");
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
    } else {
        mensaje = "No hubo respuesta";
    }
    mostrarRespuesta(res, mensaje, tipo);
    listaDatos = data[2].split("▼");
    listar();
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
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtPaciente":
            return gbi("txtPersonal");
            break;
        case "txtFormaPago":
            return gbi("txtCategoria");
            break;
        default:
            break;
    }
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

function LabDescargarPDF(tipoImpresion) {
    var texto = "";
    var columns = cabeceras;
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = matriz[i];
    }
    var doc = new jsPDF('p', 'pt', "a4");
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
    doc.setFont('helvetica');
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
    doc.text("Fecha Impresión", width - 90, 40)
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50)
    doc.setFontSize(14);
    doc.setFontType("bold");
    //Titulo de Documento y datos
    doc.text("LABORATORIOS", width / 2, 95, "center");// + gbi("txtRequerimiento").value
    var xic = 140;
    var altc = 12;
    doc.setFontType("bold");
    doc.setFontSize(7);

    //Inicio de Detalle
    //Crear Cabecera
    var xid = 120;
    var xad = 12;
    agregarCabeceras();
    function agregarCabeceras() {
        doc.setFontType("bold");
        doc.setFontSize(7);
        doc.text("ITEM", 30, xid);
        doc.text("LABORATORIO", 120, xid);
        doc.text("FECHA CREACION", 260, xid);
        doc.text("ESTADO", 450, xid);
        //doc.text("SUB TOTAL", width - 208, xid, 'right');
        //doc.text("IGV", width - 138, xid, 'right');
        //doc.text("TOTAL", width - 68, xid, 'right');
        doc.line(30, xid + 3, width - 30, xid + 1);
        doc.setFontType("normal");
        doc.setFontSize(6.5);
    }

    //Crear Detalle
    var n = 0;

    gbi("contentPrincipal").querySelectorAll(".row.panel.salt").forEach((item, index) => {
        doc.text((index + 1).toString(), 30, xid + xad + (n * xad));//item
        doc.text(item.children[1].lastChild.innerHTML, 120, xid + xad + (n * xad));//fecha
        doc.text(item.children[2].lastChild.innerHTML, 260, xid + xad + (n * xad));//Numero doc
        doc.text(item.children[3].lastChild.innerHTML, 450, xid + xad + (n * xad));//cliente
        //doc.text(item.children[5].lastChild.innerHTML, width - 208, xid + xad + (n * xad), 'right');//sub total
        //doc.text(item.children[6].lastChild.innerHTML, width - 138, xid + xad + (n * xad), 'right');//igv
        //doc.text(item.children[7].lastChild.innerHTML, width - 68, xid + xad + (n * xad), 'right');//total

        if (xid + xad + (n * xad) > height - 30) {
            doc.addPage();
            n = 0;
            xid = 30;
            agregarCabeceras();
            xid = 35;
        }
        n += 1;
    });

    if (tipoImpresion == "e") {//exportar
        doc.save("Laboratorios.pdf");
    }
    else if (tipoImpresion == "i") {//imprimir
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}