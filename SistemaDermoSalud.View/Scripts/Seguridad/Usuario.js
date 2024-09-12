var cabeceras = ["idUsuario", "Rol", "Usuario", "Última Modificación", "-"];
//var cabecerasLocal = ["idLocal", "Código", "Descripción", "Dirección"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var matriz = [];
var orden = 0;
var registrosPagina = 1000000000;
var indiceActualPagina = 0;
var paginasBloque = 3;
var indiceActualBloque = 0;
var indicePagina = 0;
var textoExportar;
var excelExportar;
//Inicializando
var url = "Seg_Usuario/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var cboRol = document.getElementById("cboRol");
var txtUsuario = document.getElementById("txtUsuario");
var txtPassword = document.getElementById("txtPassword");
var chkActivo = document.getElementById("chkActivo");

function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        listaDatos = listas[0].split("▼");
        var listaRol = listas[1].split("▼");
        llenarCombo(listaRol, "cboRol", "Seleccione");
        listar(listaDatos);
    }
}
function listar(r) {
    let newDatos = [];
    if (r[0] !== '') {
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idUsuario: valor[0],
                CodigoGenerado: valor[1],
                RolDescripcion: valor[2],
                Usuario: valor[3],
                FechaModificacion: valor[4],
                UsuarioModificacionDescripcion: valor[5],
                Estado: valor[6]
            })
        });
    }
    let cols = ["RolDescripcion", "Usuario", "FechaModificacion"];
    loadDataTable(cols, newDatos, "idUsuario", "tbDatos", cadButtonOptions(), false);
}
function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    for (var i = 0; i < cabeceras.length; i++) {
        if (i != 0) {
            columns[i - 1] = cabeceras[i];
        }
    }
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = [];
        for (var j = 0; j < matriz[i].length; j++) {
            if (j != 0) {
                data[i][j - 1] = matriz[i][j];
            }
        }
    }
    var doc = new jsPDF(orientation, 'pt', (tipo == undefined ? "a3" : "a4"));
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    var fec = new Date();
    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
    var y = fec.getFullYear();

    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
    doc.setFont('helvetica')
    doc.setFontSize(14);
    doc.text(titulo, width / 2 - 80, 95);
    doc.line(30, 125, width - 30, 125);
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("Dermosalud S.A.C", 10, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 10, 40);
    doc.text("20565643143", 30, 40);
    doc.text("Dirección:", 10, 50);
    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 50, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 90, 40)
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50)

    doc.autoTable(columns, data, {
        theme: 'plain',
        startY: 110, showHeader: 'firstPage',
        headerStyles: { styles: { overflow: 'linebreak', halign: 'center' }, fontSize: 7, },
        bodyStyles: { fontSize: 6, valign: 'middle', cellPadding: 2, columnWidt: 'wrap' },
        columnStyles: {},

    });
    if (v == "e") {
        doc.save((nombre != undefined ? nombre : "table.pdf"));
    }
    else if (v == "i") {
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}

function editItem(id) {
    var row = gbi(id);
    gbi("txtArticulo").dataset.id = row.children[3].dataset.id;
    gbi("txtArticulo").value = row.children[3].innerHTML;
    gbi("txtCategoria").dataset.id = row.children[2].dataset.id;
    gbi("txtCategoria").value = row.children[2].innerHTML;
    gbi("txtCantidad").value = row.children[4].innerHTML;
    gbi("txtPrecio").value = row.children[6].innerHTML;
    gbi("txtTotal").value = row.children[7].innerHTML;

    gbi("btnGrabarDetalle").innerHTML = "Actualizar";
    idTablaDetalle = id;
    //para que aparescan , ya que en el html  esta en style="display:none;"
    gbi("btnGrabarDetalle").style.display = "";
    gbi("btnCancelarDetalle").style.display = "";

}
function mostrarMatrizUsuario(matriz, cabeceras, tabId, contentID) {
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
                        case 0: case 1:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 3:
                            contenido2 += "col-md-4' style='padding-top:5px;'>";
                            break;
                        case 2:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
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
    var url = "Seg_Usuario/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function actualizarListar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó al nuevo Usuario";
                tipo = "success";
                show_hidden_Formulario();
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Usuario";
                tipo = "success";
                show_hidden_Formulario();
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Seg_Usuario/Grabar";
            var frm = new FormData();
            frm.append("idUsuario", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("CodigoGenerado", txtCodigo.value);
            frm.append("Usuario", gvt("txtUsuario"));
            frm.append("Password", txtPassword.value);
            frm.append("idRol", cboRol.value);
            frm.append("Estado", chkActivo.checked);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[0].split("▲");
        txtID.value = listaDetalle[0];
        cboRol.value = listaDetalle[3];
        txtUsuario.value = listaDetalle[4];
        txtPassword.value = listaDetalle[5];
        chkActivo.checked = listaDetalle[10].toUpperCase() == "ACTIVO" ? true : false;
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Usuario";//Titulo Insertar
            break;
        case 2:
            let idMarca = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Usuario";//Titulo Modificar
            TraerDetalle(idMarca);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    txtCodigo.value = "";
    txtUsuario.value = "";
    txtPassword.value = "";
    cboRol.value = "";
    txtID.value = "";
    chkActivo.checked = true;
    quitarValidacion("txtUsuario");
    quitarValidacion("txtPassword");
    quitarValidacion("cboRol");
}
function validarFormulario() {
    var error = true;
    if (validarControl("cboRol")) error = false;
    if (validarControl("txtUsuario")) error = false;
    if (validarControl("txtPassword")) error = false;
    return error;
}
function eliminar(id) {
    let idUsuario = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar este Usuario?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var url = "Seg_Usuario/Eliminar?idUsuario=" + idUsuario;
            enviarServidor(url, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se eliminó la marca', 'error');
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó al Usuario";
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
    listar(listaDatos);
}