var cabeceras = ["idPersonal", "DNI", "Nombre", "Fecha Ingreso", "Estado"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var matriz = [];
var url = "Personal/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtFechaIngreso = document.getElementById("txtFechaIngreso");
var txtnombre = document.getElementById("txtnombre");
var txtapepat = document.getElementById("txtapepat");
var txtapemat = document.getElementById("txtapemat");
var txtEdad = document.getElementById("txtEdad");
var txtdoc = document.getElementById("txtdoc");
var cbosex = document.getElementById("cbosex");
var txtFechaNacimiento = document.getElementById("txtFechaNacimiento");
var cboEstadoCivil = document.getElementById("cboEstadoCivil");
var cboCargo = document.getElementById("cboCargo");
var txtColegiatura = document.getElementById("txtColegiatura");
var txtdireccion = document.getElementById("txtdireccion");
var txttelf = document.getElementById("txttelf");
var txtmovil = document.getElementById("txtmovil");
var txtlogin = document.getElementById("txtlogin");
var chkActivo = document.getElementById("chkActivo");
var dbimg64 = "";
var txtColor = document.getElementById("mycp")
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            llenarCombo(listas[3].split("▼"), "cboCargo", "Seleccione");
            listar(listaDatos);
        }
    }
}
function imgClick() {
    var fI = gbi("inpImage");
    fI.click();
}

function listar(r) {
    let newDatos = [];
    if (r[0] !== '') {
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idPersonal: valor[0],
                Documento: valor[1],
                NombreCompleto: valor[2],
                FechaIngreso: valor[3],
                Estado: valor[4]
            })
        });
    }
    let cols = ["Documento", "NombreCompleto", "FechaIngreso", "Estado"];
    loadDataTable(cols, newDatos, "idPersonal", "tbDatos", cadButtonOptions(), false);
}
function TraerDetalle(id) {
    var url = "Personal/ObtenerDatosxID/?id=" + id;
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
                mensaje = "Se adicionó al nuevo Personal";
                tipo = "success";
                show_hidden_Formulario();
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó el Personal";
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
        listar(listaDatos);
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Personal/Grabar";
            var frm = new FormData();
            frm.append("idPersonal", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("FechaIngreso", txtFechaIngreso.value);
            frm.append("Nombres", txtnombre.value);
            frm.append("ApellidoP", txtapepat.value);
            frm.append("ApellidoM", txtapemat.value);
            frm.append("Edad", txtEdad.value);
            frm.append("Documento", txtdoc.value);
            frm.append("Sexo", cbosex.value);
            frm.append("FechaNacimiento", txtFechaNacimiento.value);
            frm.append("EstadoCivil", cboEstadoCivil.value);
            frm.append("Direccion", txtdireccion.value);
            frm.append("Telefono", txttelf.value);
            frm.append("Movil", txtmovil.value);
            frm.append("Login", txtlogin.value);
            frm.append("Color", txtColor.value);
            frm.append("Estado", chkActivo.checked);
            frm.append("PorcentajeUtilidad", 0);
            frm.append("Img", dbimg64);
            frm.append("idCargo", cboCargo.value);
            frm.append("Planilla", gbi("chkPlanilla").checked);
            frm.append("Colegiatura", txtColegiatura.value);
            frm.append("Sueldo", gbi("txtSueldo").value);
            frm.append("TipoJornada", gbi("cboTipoJornada").value);
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

    var image = document.getElementById('persImg');
    image.onerror = function () {
        this.src = "../../app-assets/images/users/dftuser.png";
    };
}


function adt(v, ctrl) {
    gbi(ctrl).value = v;
}

$("#txtFechaIngreso").flatpickr({
    enableTime: false,
    dateFormat: "d-m-Y"
});
$("#txtFechaNacimiento").flatpickr({
    enableTime: false,
    dateFormat: "d-m-Y"
});

function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        var imagen = datos[3];
        txtID.value = listaDetalle[0];
        txtCodigo.value = listaDetalle[0];
        adt(listaDetalle[1], "txtFechaIngreso");
        txtnombre.value = listaDetalle[2];
        txtapepat.value = listaDetalle[3];
        txtapemat.value = listaDetalle[4];
        txtEdad.value = listaDetalle[5];
        txtdoc.value = listaDetalle[6];
        cbosex.value = listaDetalle[7];
        adt(listaDetalle[8], "txtFechaNacimiento");
        cboEstadoCivil.value = listaDetalle[9];
        txtdireccion.value = listaDetalle[10];
        txttelf.value = listaDetalle[11];
        txtmovil.value = listaDetalle[12];
        txtlogin.value = listaDetalle[13];
        txtColegiatura.value = listaDetalle[24];
        cboCargo.value = listaDetalle[17];
        chkActivo.checked = listaDetalle[19] == "ACTIVO" ? true : false;
        txtColor.value = listaDetalle[20];
        gbi("txtSueldo").value = listaDetalle[27];
        gbi("cboTipoJornada").value = listaDetalle[28];
        gbi("persImg").src = imagen == "" ? "../../app-assets/images/users/dftuser.png" : imagen;
        gbi("chkPlanilla").checked = listaDetalle[26] == "TRUE" ? true : false;
        //$('#mycp').colorpicker('setValue', gbi("mycp").value);
    }
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            txtID.value = "";
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Personal";
            break;
        case 2:
            let idPersonal = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Personal";
            TraerDetalle(idPersonal);
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
    chkActivo.checked = true;
    gbi("persImg").src = "../../app-assets/images/users/dftuser.png";
}
function validarFormulario() {
    var error = true;
    var divs = document.querySelectorAll('.cf');
    [].forEach.call(divs, function (div) {
        if (validarControl(div.id)) error = false;
    });
    return error;
}
function eliminar(id) {
    let idPersonal = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar este Personal?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var url = "Personal/Eliminar?idPersonal=" + idPersonal;
            enviarServidor(url, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se eliminó al Personal', 'error');
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó al Personal";
            tipo = "success";
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