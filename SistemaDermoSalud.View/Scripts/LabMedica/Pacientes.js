var cabeceras = ["idPaciente", "DNI", "Nombre Completo", "Última Consulta", "Estado"];
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
var url = "Pacientes/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

var txtID = document.getElementById("txtidPaciente");
var txtNombres = document.getElementById("txtNombres");
var txtApellidoP = document.getElementById("txtApellidoP");
var txtApellidoM = document.getElementById("txtApellidoM");
var txtFechaNacimiento = document.getElementById("txtFechaNacimiento");
var txtDNI = document.getElementById("txtDNI");
var cboSexo = document.getElementById("cboSexo");
var txtDireccion = document.getElementById("txtDireccion");
var txtTelefono = document.getElementById("txtTelefono");
var txtMovil = document.getElementById("txtMovil");
var txtEmail = document.getElementById("txtEmail");
var txtObservaciones = document.getElementById("txtObservaciones");
var chkActivo = document.getElementById("chkActivo");
var dbimg64 = " ";
var txtEdad = document.getElementById("txtEdad");

function mostrarLista(rpta) {
    crearTablaPaciente(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listar();
        }
    }
}

function imgClick() {
    var fI = gbi("inpImage");
    fI.click();
}

function crearTablaPaciente(cabeceras, div) {
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
function configurarFiltro(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        mostrarMatrizPaciente(matriz, cabe, "divTabla", "contentPrincipal");
    };
}
function listar() {
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatrizPaciente(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function mostrarMatrizPaciente(matriz, cabeceras, tabId, contentID) {
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
                        case 2:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
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
    var url = "Pacientes/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function actualizarListar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtidPaciente");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó al nuevo Paciente";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Paciente";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(false); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function configurarBotonesModal() {


    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Servicios", cabeceras, matriz, "Servicios", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "servicios", cabeceras, matriz, "Servicios", "a4", "i");
    }

    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
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

    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Pacientes/Grabar";
            var frm = new FormData();
            frm.append("idPaciente", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("Nombres", txtNombres.value);
            frm.append("ApellidoP", gvt("txtApellidoP"));
            frm.append("ApellidoM", gvt("txtApellidoM"));
            frm.append("DNI", gvt("txtDNI").length == 0 ? "0" : gvt("txtDNI"));
            frm.append("Edad", gvt("txtEdad"));
            frm.append("Sexo", cboSexo.value.length == 0 ? "0" : cboSexo.value);
            frm.append("Direccion", gvt("txtDireccion"));
            frm.append("Telefono", gvt("txtTelefono"));
            frm.append("Movil", gvt("txtMovil"));
            frm.append("Email", gvt("txtEmail"));
            frm.append("Observaciones", gvt("txtObservaciones"));
            frm.append("FechaNacimiento", gvt("txtFechaNacimiento"));
            frm.append("Img", dbimg64.length == 0 ? "0" : dbimg64);
            frm.append("Estado", true);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }

    var inputImage = document.getElementById("inpImage");
    inputImage.onchange = function (evt) {
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;
        if (FileReader && files && files.length) {
            var fr = new FileReader();
            fr.onload = function () {
                var img = gbi("persImg");
                dbimg64 = fr.result;
                img.src = dbimg64;
            }
            fr.readAsDataURL(files[0]);
        }
    }
}
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}

$("#txtFechaIngreso").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFechaNacimiento").datetimepicker({
    format: 'DD-MM-YYYY',
});

function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        txtID.value = listaDetalle[0];
        txtNombres.value = listaDetalle[1];
        txtApellidoP.value = listaDetalle[2];
        txtApellidoM.value = listaDetalle[3];
        txtFechaNacimiento.value = listaDetalle[12];
        txtDNI.value = listaDetalle[5];
        txtEdad.value = listaDetalle[4];
        cboSexo.value = listaDetalle[6];
        txtDireccion.value = listaDetalle[7];
        txtTelefono.value = listaDetalle[8];
        txtMovil.value = listaDetalle[9];
        txtEmail.value = listaDetalle[10];
        txtObservaciones.value = listaDetalle[11];
        chkActivo.checked = listaDetalle[19].toUpperCase() == "ACTIVO" ? true : false;
        gbi("persImg").src = listaDetalle[14] == "" ? "../../app-assets/images/users/dftuser.png" : listaDetalle[14];
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Usuario";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Usuario";
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function limpiarTodo() {
    var divs = document.querySelectorAll('.cf');
    [].forEach.call(divs, function (div) {
        div.value = "";
        quitarValidacion(div.id);
    });
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtNombres")) error = false;
    if (validarControl("txtApellidoP")) error = false;
    if (validarControl("txtApellidoM")) error = false;
    if (validarControl("txtFechaNacimiento")) error = false;    
    if (validarControl("txtEdad")) error = false;
    
    return error;
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar este Paciente?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
    function (isConfirm) {
        if (isConfirm) {
            var url = "Pacientes/Eliminar?idPaciente=" + id;
            enviarServidor(url, eliminarListar);
        } else {
            swal("Cancelado", "No se elminó al Paciente", "error");
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó al Paciente";
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
gbi("txtFechaNacimiento").onblur = function () {
    var edad = 0;
    hoy = new Date();
    diaActual = hoy.getDate();
    mesActual = hoy.getMonth() + 1;
    yearActual = hoy.getFullYear();
    if (diaActual < 10) { diaActual = '0' + diaActual; }
    if (mesActual < 10) { mesActual = '0' + mesActual; }
    var fecha = gbi("txtFechaNacimiento").value;
    var array_fecha = fecha.split("-")
    dia = array_fecha[0];
    mes = array_fecha[1];
    year = array_fecha[2];

    //Valido que la fecha de nacimiento no sea mayor a la fecha actual 
    //if (year >= yearActual) {
    //    document.getElementById('txt_fechaNacimiento').setCustomValidity('La fecha de Nacimiento no puede ser mayor o igual a la fecha Actual...');
    //    document.getElementById('submit').click();
    //    //return;
    //}
    //else 
    if ((mes >= mesActual) && (dia > diaActual)) {

        edad = (yearActual - 1) - year;
    }
    else {
        edad = yearActual - year;
    }

    gbi("txtEdad").value = edad;
}
