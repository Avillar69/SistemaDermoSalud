var cabeceras = ["Nº Documento", "Razon Social", "Fecha", "Estado"];
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
var idTablaDetalle;
var nombreEmpresa = "HARI´S SPORT EMPRESA INDIVIDUAL DE RESPONSABILIDAD LIMITADA";
var rucEmpresa = "20612173452";
var direccionEmpresa = "JR. ANCASH NRO. 1265 (ESQUINA CON TARAPACA) JUNIN - HUANCAYO - HUANCAYO";
//idUsuario
var idUsuario;
//ListasGlobales
var listaDepartamento;
var listaProvincia;
var listaDistrito;
var idxDireccion = 0;
var idxTelefono = 0;
var idxContacto = 0;
var idxCuenta = 0;
var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var cboTipoPersona = document.getElementById("cboTipoPersona");
var txtRazonSocial = document.getElementById("txtRazonSocial");
var cboTipoDocumento = document.getElementById("cboTipoDocumento");
var txtNroDocumento = document.getElementById("txtNroDocumento");
var cboPais = document.getElementById("cboPais");
var cboDepartamento = document.getElementById("cboDepartamento");
var cboProvincia = document.getElementById("cboProvincia");
var cboDistrito = document.getElementById("cboDistrito");
var txtWeb = document.getElementById("txtWeb");
var txtMail = document.getElementById("txtMail");
var chkCliente = document.getElementById("chkCliente");
var chkProveedor = document.getElementById("chkProveedor");
var chkActivo = document.getElementById("chkActivo");
//Inicializando
$(function () {
    var url = "/SocioNegocio/ObtenerDatosProv";
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
    configBM();
});
//
//listar
$("#cboTipoDocumento").change(function () {
    switch ($("#cboTipoDocumento").val()) {
        case "2":
            $("#btnBSN").html("Reniec");
            $("#btnBSN").prop("disabled", false);

            break;
        case "4":
            $("#btnBSN").html("Sunat");
            $("#btnBSN").prop("disabled", false);

            break;
        default:
            $("#btnBSN").html("-");
            $("#btnBSN").prop("disabled", true);

            break;
    }
});
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        listaDatos = listas[2].split("▼");
        var listaPais = listas[3].split("▼");
        var listaTipoPersona = listas[4].split("▼");
        var listaTipoDocumento = listas[5].split("▼");
        var listaMoneda = listas[6].split("▼");
        idUsuario = listas[7];
        listaDepartamento = listas[8].split("▼");
        listaProvincia = listas[9].split("▼");
        listaDistrito = listas[10].split("▼");
        //if (listas[11] != "Error") {
        //    document.getElementById("imgCaptcha").style.display = "inherit";
        //    cargarImagen(listas[11], "imgCaptcha");
        //}
        //else {
        //    //document.getElementById("btnBuscar").disabled = true;
        //    document.getElementById("imgCaptcha").style.display = "none";
        //}
        //if (listas[12] != "Error") {
        //    document.getElementById("imgCaptchaReniec").style.display = "inherit";
        //    cargarImagen(listas[12], "imgCaptchaReniec");
        //}
        //else {
        //    // document.getElementById("btnBuscarReniec").disabled = true;
        //    document.getElementById("imgCaptchaReniec").style.display = "none";
        //}
        cargarDatosTipoPersona(listaTipoPersona);
        cargarDatosTipoDocumento(listaTipoDocumento);
        cargarDatosPais(listaPais);
        cargarDatosDepartamento(listaDepartamento);
        let urlBancos = "/Banco/ListarBancos";
        enviarServidor(urlBancos, cargarDatosBanco);
        cargarDatosMoneda(listaMoneda);
        listar(listaDatos);
    }
}
function listar(r) {
    let newDatos = [];
    if (r[0] !== '') {
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idSocioNegocio: valor[0],
                numDocumento: valor[1],
                razonSocial: valor[2],
                fecha: valor[3],
                estado: valor[4]
            })
        });
    }
    let cols = ["numDocumento", "razonSocial", "fecha", "estado"];
    loadDataTable(cols, newDatos, "idSocioNegocio", "tbDatos", cadButtonOptions(), false);
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
        pageLength: 25,
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
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            gbi("txtID").value = "0";
            lblTituloPanel.innerHTML = "Nuevo Proveedor";
            gbi("cboPais").value = "1";
            break;
        case 2:
            let idProv = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Modificar Proveedor";
            TraerDetalle(idProv);
            show_hidden_Formulario();
            break;
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validate("vFormulario")) {
            var url = "/SocioNegocio/Grabar";
            var frm = new FormData();
            frm.append("idSocioNegocio", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("idTipoPersona", cboTipoPersona.value);
            frm.append("RazonSocial", txtRazonSocial.value.trim());
            frm.append("idTipoDocumento", cboTipoDocumento.value);
            frm.append("Documento", txtNroDocumento.value.trim());
            frm.append("idPais", cboPais.value);
            frm.append("idDepartamento", cboDepartamento.value);
            frm.append("idProvincia", cboProvincia.value);
            frm.append("idDistrito", cboDistrito.value);
            frm.append("Web", txtWeb.value.length == 0 ? "www" : txtWeb.value);
            frm.append("Mail", txtMail.value.length == 0 ? "@" : txtMail.value);
            frm.append("Cliente", chkCliente.checked);
            frm.append("Proveedor", true);
            frm.append("Estado", chkActivo.checked);
            //detalles

            frm.append("Lista_Direccion", crearCadDetalleDireccion());
            frm.append("Lista_Telefono", crearCadDetalleTelefono());
            frm.append("Lista_Contacto", crearCadDetalleContacto());
            frm.append("Lista_CuentaBancaria", crearCadDetalleCuenta());
            enviarServidorPost(url, actualizarListar, frm);
        }
    };

    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () { show_hidden_Formulario(); }

    //TABS
    var btnAgregarDireccion = document.getElementById("btnAgregarDireccion");
    btnAgregarDireccion.onclick = function () {
        if (validate("vDireccion")) {
            addRowDireccion(0, []);
        }
    }
    var btnAgregarTelefono = document.getElementById("btnAgregarTelefono");
    btnAgregarTelefono.onclick = function () {
        if (validate("vTelefono")) {
            addRowTelefono(0, []);
        }
    }
    var btnAgregarContacto = document.getElementById("btnAgregarContacto");
    btnAgregarContacto.onclick = function () {
        if (validate("vContacto")) {
            addRowContacto(0, []);
        }
    }
    var btnAgregarCuenta = document.getElementById("btnAgregarCuenta");
    btnAgregarCuenta.onclick = function () {
        if (validate("vCuenta")) {
            addRowCuenta(0, []);
        }
    }
    //
    //var btnGrabar_Modal = document.getElementById('btnGrabar_Modal');
    //btnGrabar_Modal.onclick = function () {
    //    var grabar = btnGrabar_Modal.dataset.grabar;
    //    var frm = new FormData();
    //    var txtCodigo = document.getElementById("txtCodigo_Banco");
    //    var txtDescripcion = document.getElementById("txtDescripcion_Banco");
    //    var chkActivo = document.getElementById("chkActivo_Banco");
    //    //obtenemos el valor del input del formulario
    //    txtValor = txtDescripcion;
    //    //solo cambiamos la url y los id
    //    var url = '/Banco/Grabar';

    //    frm.append("idBanco", 0);
    //    frm.append("Descripcion", txtDescripcion.value);
    //    frm.append("Estado", chkActivo.checked);
    //    //validarFormularioModal est aen app.js
    //    if (validarFormularioModal(grabar)) { enviarServidorPost(url, rpta_GrabarModal, frm); }
    //}
    //var btnBusqueda = gbi("btnModalBusqueda");
    //btnBusqueda.onclick = function () {
    //    BuscarxRuc();
    //    gbi("cboPais").value = "1";
    //}
    //var cboBusqueda = gbi("cboTipoBusqueda");
    //cboBusqueda.onchange = function (e) {
    //    switch (cboBusqueda.value) {
    //        case "":
    //            displayXclass("snt", "none");
    //            displayXclass("rnc", "none");
    //            break;
    //        case "1":
    //            displayXclass("snt", "none");
    //            displayXclass("rnc", "inline-block");
    //            break;
    //        case "2":
    //            displayXclass("snt", "inline-block");
    //            displayXclass("rnc", "none");
    //            break;
    //        default:
    //            break;
    //    }
    //}
    var btnBSN = gbi("btnBSN");
    btnBSN.onclick = function () {
        BuscarxRuc(gbi("cboTipoBusqueda").value);
        //CerrarModal("modal-Busqueda");
    }
}
function limpiarTodo() {
    limpiarControl("txtID");
    limpiarControl("txtCodigo");
    limpiarControl("cboTipoPersona");
    limpiarControl("cboTipoDocumento");
    limpiarControl("txtNroDocumento");
    limpiarControl("txtRazonSocial");
    limpiarControl("txtWeb");
    limpiarControl("txtEstadoC");
    limpiarControl("txtCondicionC");
    limpiarControl("txtMail");
    limpiarControl("cboPais");
    limpiarControl("cboDepartamento");
    limpiarControl("cboProvincia");
    limpiarControl("cboDistrito");
    chkCliente.checked = false;
    chkProveedor.checked = false;
    chkActivo.checked = true;
    gbi("tbDirecciones").innerHTML = "";
    gbi("tbTelefonos").innerHTML = "";
    gbi("tbContactos").innerHTML = "";
    gbi("tbCuentas").innerHTML = "";
    cleanControl("direccion");
    cleanControl("telefono");
    cleanControl("contacto");
    cleanControl("cuenta");
}
function eliminar(id) {
    let idProv = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar este Socio de Negocio?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var u = "/SocioNEgocio/Eliminar?idSocioNegocio=" + idProv;
            enviarServidor(u, eliminarListar);
        } else {
            Swal.fire('Cancelado', 'No se elminó el Socio de Negocio', 'error');
        }
    });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó el Socio de Negocio";
            tipo = "success";
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
    } else {
        mensaje = "No hubo respuesta";
    }
    Swal.fire(res, mensaje, tipo);
    setTimeout(function () {
        listaDatos = data[2].split("▼");
        listar(listaDatos);
    }, 1000);
}

//
//crear
function cargarDatosTipoPersona(r) {
    let TipoPersonas = r;
    $("#cboTipoPersona").empty();
    $("#cboTipoPersona").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        TipoPersonas.forEach(element => {
            $("#cboTipoPersona").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosTipoDocumento(r) {
    let TipoDocumentos = r;
    $("#cboTipoDocumento").empty();
    $("#cboTipoDocumento").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        TipoDocumentos.forEach(element => {
            $("#cboTipoDocumento").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosPais(r) {
    let paises = r;
    $("#cboPais").empty();
    $("#cboPais").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        paises.forEach(element => {
            $("#cboPais").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosDepartamento(r) {
    let departamentos = r;
    $("#cboDepartamento").empty();
    $("#cboDepartamento").append(`<option value="">Seleccione</option>`);

    $("#cboProvincia").empty();
    $("#cboProvincia").append(`<option value="">Seleccione</option>`);

    $("#cboDistrito").empty();
    $("#cboDistrito").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        departamentos.forEach(element => {
            $("#cboDepartamento").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
    $("#cboDepartamento").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            $("#cboProvincia").prop("disabled", false);
            let listaProvFilt = [];
            for (var i = 0; i < listaProvincia.length; i++) {
                if (listaProvincia[i].split('▲')[0].indexOf(selectedValue) != -1) {
                    listaProvFilt.push(listaProvincia[i]);
                }
            }
            cargarDatosProvincia(listaProvFilt);
        }
    });
}
function cargarDatosProvincia(r) {
    let provincias = r;
    $("#cboProvincia").empty();
    $("#cboProvincia").append(`<option value="">Seleccione</option>`);

    $("#cboDistrito").empty();
    $("#cboDistrito").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        provincias.forEach(element => {
            $("#cboProvincia").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
    $("#cboProvincia").change(function () {
        var selectedValue = $(this).val();
        if (selectedValue !== "" && selectedValue !== null) {
            $("#cboDistrito").prop("disabled", false);
            let listaDistFilt = [];
            for (var i = 0; i < listaDistrito.length; i++) {
                if (listaDistrito[i].split('▲')[0].indexOf(selectedValue) != -1) {
                    listaDistFilt.push(listaDistrito[i]);
                }
            }
            cargarDatosDistrito(listaDistFilt);
        }
    });
}
function cargarDatosDistrito(r) {
    let distritos = r;
    $("#cboDistrito").empty();
    $("#cboDistrito").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        distritos.forEach(element => {
            $("#cboDistrito").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cargarDatosBanco(r) {
    let dataP = r.split("↔");
    let bancos = dataP[2].split("▼");
    $("#cboBanco").empty();
    $("#cboBanco").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        bancos.forEach(element => {
            $("#cboBanco").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[2]}</option>`);
        });
    }
}
function cargarDatosMoneda(r) {
    let monedas = r;
    $("#cboMoneda").empty();
    $("#cboMoneda").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        monedas.forEach(element => {
            $("#cboMoneda").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function cadButton(t) {
    let cad = "";
    cad += '<ul class="list-inline" style="margin-bottom: 0px;">';
    cad += '<li class="list-inline-item">';
    cad += '<div class="dropdown">';
    cad += '<button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">';
    cad += '<i class="ri-more-fill align-middle"></i>';
    cad += '</button>';
    cad += '<ul class="dropdown-menu dropdown-menu-end" style="">';
    cad += '<li>';
    cad += '<a class="dropdown-item edit-item-btn" href="javascript:void(0)" onclick="edit' + t + '(this)"><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>Editar</a>';
    cad += '</li>';
    cad += '<li>';
    cad += '<a class="dropdown-item remove-item-btn" href="javascript:void(0)" onclick="deleteRowDet(this);"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>';
    cad += '</li>';
    cad += '</ul>';
    cad += '</div>';
    cad += '</li>';
    cad += ' </ul>';
    return cad;
}
function addRowDireccion(tipo, data) {
    let cad = "";
    if (tipo == 0) {
        if (idxDireccion == 0) {
            cad += "<tr class='rowDetDireccion'>";
            cad += '<td class="" data-id="0">' + (document.getElementsByClassName("rowDetDireccion").length + 1) + '</td>';
            cad += '<td class="">' + $("#txtDireccion").val() + '</td>';
            cad += '<td class="">' + cadButton("Direccion") + '</td>';
            cad += "</tr>";
            document.getElementById("tbDirecciones").innerHTML += cad;
        }
        else {
            cad += '<td class="" data-id="' + $("#lblIdDireccion").val() + '">' + idxDireccion + '</td>';
            cad += '<td class="">' + $("#txtDireccion").val() + '</td>';
            cad += '<td class="">' + cadButton("Direccion") + '</td>';
            $("#tbDirecciones")[0].rows[idxDireccion - 1].innerHTML = cad;
        }
    } else {
        cad += "<tr class='rowDetDireccion'>";
        cad += '<td class="" data-id="' + data[0] + '">' + (document.getElementsByClassName("rowDetDireccion").length + 1) + '</td>';
        cad += '<td class="">' + data[1] + '</td>';
        cad += '<td class="">' + cadButton("Direccion") + '</td>';
        cad += "</tr>";
        document.getElementById("tbDirecciones").innerHTML += cad;
    }

    cleanControl("direccion");
    idxDireccion = 0;
}
function editDireccion(e) {
    idxDireccion = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#lblIdDireccion").val($("#tbDirecciones")[0].rows[idxTelefono - 1].childNodes[0].dataset.id);
    $("#txtDireccion").val($("#tbDirecciones")[0].rows[idxDireccion - 1].childNodes[1].innerHTML);

}
function crearCadDetalleDireccion() {
    let listaDirecciones = "";
    $(".rowDetDireccion").each(function (obj) {
        listaDirecciones += $(".rowDetDireccion")[obj].children[0].dataset.id;
        listaDirecciones += "|" + $("#txtID").val();
        listaDirecciones += "|1";
        listaDirecciones += "|" + $(".rowDetDireccion")[obj].children[1].innerHTML;
        listaDirecciones += "|0|01-01-2000|01-01-2000|1|1|true";
        listaDirecciones += "¯";
    });
    console.log(listaDirecciones);
    return listaDirecciones;
}
function addRowTelefono(tipo, data) {
    let cad = "";
    if (tipo == 0) {
        if (idxTelefono == 0) {
            cad += "<tr class='rowDetTelefono'>";
            cad += '<td class="" data-id="0">' + (document.getElementsByClassName("rowDetTelefono").length + 1) + '</td>';
            cad += '<td class="">' + $("#txtTelefono").val() + '</td>';
            cad += '<td class="">' + cadButton("Telefono") + '</td>';
            cad += "</tr>";
            document.getElementById("tbTelefonos").innerHTML += cad;
        }
        else {
            cad += '<td class="" data-id="' + $("#lblIdTelefono").val() + '">' + idxTelefono + '</td>';
            cad += '<td class="">' + $("#txtTelefono").val() + '</td>';
            cad += '<td class="">' + cadButton("Telefono") + '</td>';
            $("#tbTelefonos")[0].rows[idxTelefono - 1].innerHTML = cad;
        }
    } else {
        cad += "<tr class='rowDetTelefono'>";
        cad += '<td class="" data-id="' + data[0] + '">' + (document.getElementsByClassName("rowDetTelefono").length + 1) + '</td>';
        cad += '<td class="">' + data[1] + '</td>';
        cad += '<td class="">' + cadButton("Telefono") + '</td>';
        cad += "</tr>";
        document.getElementById("tbTelefonos").innerHTML += cad;
    }
    cleanControl("telefono");
    idxTelefono = 0;
}
function editTelefono(e) {
    idxTelefono = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#lblIdTelefono").val($("#tbTelefonos")[0].rows[idxTelefono - 1].childNodes[0].dataset.id);
    $("#txtTelefono").val($("#tbTelefonos")[0].rows[idxTelefono - 1].childNodes[1].innerHTML);

}
function crearCadDetalleTelefono() {
    let listatelefonos = "";
    $(".rowDetTelefono").each(function (obj) {
        listatelefonos += $(".rowDetTelefono")[obj].children[0].dataset.id;
        listatelefonos += "|" + $("#txtID").val();
        listatelefonos += "|1";
        listatelefonos += "|" + $(".rowDetTelefono")[obj].children[1].innerHTML;
        listatelefonos += "|01-01-2000|01-01-2000|1|1|true";
        listatelefonos += "¯";
    });
    console.log(listatelefonos);
    return listatelefonos;
}
function addRowContacto(tipo, data) {
    let cad = "";
    if (tipo == 0) {
        if (idxContacto == 0) {
            cad += "<tr class='rowDetContacto'>";
            cad += '<td class=""  data-id="0">' + (document.getElementsByClassName("rowDetContacto").length + 1) + '</td>';
            cad += '<td class="">' + $("#txtNombre").val() + '</td>';
            cad += '<td class="">' + $("#txtCargo").val() + '</td>';
            cad += '<td class="">' + $("#txtTelefonoContacto").val() + '</td>';
            cad += '<td class="">' + $("#txtMailContacto").val() + '</td>';
            cad += '<td class="">' + cadButton("Contacto") + '</td>';
            cad += "</tr>";
            document.getElementById("tbContactos").innerHTML += cad;
        }
        else {
            cad += '<td class="" data-id="' + $("#lblIdContacto").val() + '">' + idxContacto + '</td>';
            cad += '<td class="">' + $("#txtNombre").val() + '</td>';
            cad += '<td class="">' + $("#txtCargo").val() + '</td>';
            cad += '<td class="">' + $("#txtTelefonoContacto").val() + '</td>';
            cad += '<td class="">' + $("#txtMailContacto").val() + '</td>';
            cad += '<td class="">' + cadButton("Contacto") + '</td>';
            $("#tbContactos")[0].rows[idxContacto - 1].innerHTML = cad;
        }
    } else {
        cad += "<tr class='rowDetContacto'>";
        cad += '<td class=""  data-id="' + data[0] + '">' + (document.getElementsByClassName("rowDetContacto").length + 1) + '</td>';
        cad += '<td class="">' + data[1] + '</td>';
        cad += '<td class="">' + data[2] + '</td>';
        cad += '<td class="">' + data[3] + '</td>';
        cad += '<td class="">' + data[4] + '</td>';
        cad += '<td class="">' + cadButton("Contacto") + '</td>';
        cad += "</tr>";
        document.getElementById("tbContactos").innerHTML += cad;
    }

    cleanControl("contacto");
    idxContacto = 0;
}
function editContacto(e) {
    idxContacto = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#lblIdContacto").val($("#tbContactos")[0].rows[idxTelefono - 1].childNodes[0].dataset.id);
    $("#txtNombre").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[1].innerHTML);
    $("#txtCargo").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[2].innerHTML);
    $("#txtTelefonoContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[3].innerHTML);
    $("#txtMailContacto").val($("#tbContactos")[0].rows[idxContacto - 1].childNodes[4].innerHTML);

}
function crearCadDetalleContacto() {
    let listaContactos = "";
    $(".rowDetContacto").each(function (obj) {
        listaContactos += $(".rowDetContacto")[obj].children[0].dataset.id;
        listaContactos += "|" + $("#txtID").val();
        listaContactos += "|1";
        listaContactos += "|" + $(".rowDetContacto")[obj].children[1].innerHTML;
        listaContactos += "|" + $(".rowDetContacto")[obj].children[2].innerHTML;
        listaContactos += "|" + $(".rowDetContacto")[obj].children[3].innerHTML;
        listaContactos += "|" + $(".rowDetContacto")[obj].children[4].innerHTML;
        listaContactos += "|01-01-2000|01-01-2000|1|1|true";
        listaContactos += "¯";
    });
    console.log(listaContactos);
    return listaContactos;
}
function addRowCuenta(tipo, data) {
    let cad = "";
    if (tipo == 0) {
        if (idxCuenta == 0) {
            cad += "<tr class='rowDetCuenta'>";
            cad += '<td class=""  data-id="0">' + (document.getElementsByClassName("rowDetCuenta").length + 1) + '</td>';
            cad += '<td class="" data-id="' + $("#cboBanco").val() + '">' + $("#cboBanco option:selected").text() + '</td>';
            cad += '<td class="">' + $("#txtCuenta").val() + '</td>';
            cad += '<td class="" data-id="' + $("#cboMoneda").val() + '">' + $("#cboMoneda option:selected").text() + '</td>';
            cad += '<td class="">' + $("#txtCuentaDescripcion").val() + '</td>';
            cad += '<td class="">' + cadButton("Cuenta") + '</td>';
            cad += "</tr>";
            document.getElementById("tbCuentas").innerHTML += cad;
        }
        else {
            cad += '<td class="" data-id="' + $("#lblIdCuenta").val() + '">' + idxCuenta + '</td>';
            cad += '<td class="" data-id="' + $("#cboBanco").val() + '">' + $("#cboBanco option:selected").text() + '</td>';
            cad += '<td class="">' + $("#txtCuenta").val() + '</td>';
            cad += '<td class="" data-id="' + $("#cboMoneda").val() + '">' + $("#cboMoneda option:selected").text() + '</td>';
            cad += '<td class="">' + $("#txtCuentaDescripcion").val() + '</td>';
            cad += '<td class="">' + cadButton("Cuenta") + '</td>';
            $("#tbCuentas")[0].rows[idxCuenta - 1].innerHTML = cad;
        }
    } else {
        cad += "<tr class='rowDetCuenta'>";
        cad += '<td class="" data-id="' + data[0] + '">' + (document.getElementsByClassName("rowDetCuenta").length + 1) + '</td>';
        cad += '<td class="" data-id="' + data[1] + '">' + data[2] + '</td>';
        cad += '<td class="">' + data[3] + '</td>';
        cad += '<td class="" data-id="' + data[5] + '">' + data[6] + '</td>';
        cad += '<td class="">' + data[4] + '</td>';
        cad += '<td class="">' + cadButton("Cuenta") + '</td>';
        cad += "</tr>";
        document.getElementById("tbCuentas").innerHTML += cad;
    }

    cleanControl("cuenta");
    idxCuenta = 0;
}
function editCuenta(e) {
    idxCuenta = (e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode).rowIndex;
    $("#lblIdCuenta").val($("#tbCuentas")[0].rows[idxTelefono - 1].childNodes[0].dataset.id);
    $("#cboBanco").val($("#tbCuentas")[0].rows[idxCuenta - 1].childNodes[1].dataset.id);
    $("#txtCuenta").val($("#tbCuentas")[0].rows[idxCuenta - 1].childNodes[2].innerHTML);
    $("#cboMoneda").val($("#tbCuentas")[0].rows[idxCuenta - 1].childNodes[3].dataset.id);
    $("#txtCuentaDescripcion").val($("#tbCuentas")[0].rows[idxCuenta - 1].childNodes[4].innerHTML);

}
function crearCadDetalleCuenta() {
    let listaCuentas = "";
    $(".rowDetCuenta").each(function (obj) {
        listaCuentas += $(".rowDetCuenta")[obj].children[0].dataset.id;
        listaCuentas += "|" + $("#txtID").val();
        listaCuentas += "|1";
        listaCuentas += "|" + $(".rowDetCuenta")[obj].children[1].dataset.id;
        listaCuentas += "|" + $(".rowDetCuenta")[obj].children[4].innerHTML;
        listaCuentas += "|" + $(".rowDetCuenta")[obj].children[2].innerHTML;
        listaCuentas += "|" + $(".rowDetCuenta")[obj].children[3].dataset.id;
        listaCuentas += "|01-01-2000|01-01-2000|1|1|true";
        listaCuentas += "¯";
    });
    return listaCuentas;
}
function deleteRowDet(e) {
    e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.remove();
}
function actualizarListar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo == "0") {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó el Socio de Negocio";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Socio de Negocio";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") {
            Swal.fire(res, mensaje, tipo);
            setTimeout(function () {
                show_hidden_Formulario(true);
                listaDatos = data[2].split("▼");
                listar(listaDatos);
            }, 1000);
        }
    }
}
//
//editar
function TraerDetalle(id) {
    var url = "/SocioNegocio/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == 'OK') {
            let lista = listas[2].split('▲');
            let lstDireccion = listas[4].split("▼");
            let lstTelefono = listas[5].split("▼");
            let lstContacto = listas[3].split("▼");
            let lstCuenta = listas[6].split("▼");
            //Asignar valores a controles
            gbi("txtID").value = lista[0];//idArticulo
            gbi("txtCodigo").value = lista[1];//codigoGenerado
            gbi("cboTipoPersona").value = lista[3];//idTipoPersona
            gbi("cboTipoDocumento").value = lista[5];//idTipoDocumento
            gbi("txtNroDocumento").value = lista[6];//Documento
            gbi("txtRazonSocial").value = lista[4];//RazonSocial
            gbi("txtWeb").value = lista[11];//Web
            gbi("txtMail").value = lista[12];//Mail
            gbi("cboPais").value = lista[7];//idPais
            setTimeout(function () { $("#cboDepartamento").val(lista[8]).trigger('change'); }, 200);
            setTimeout(function () { $("#cboProvincia").val(lista[9]).trigger('change'); }, 500);
            setTimeout(function () { $("#cboDistrito").val(lista[10]).trigger('change'); }, 800);
            gbi("chkCliente").checked = lista[13] == "TRUE" ? true : false;//Cliente
            gbi("chkProveedor").checked = lista[14] == "TRUE" ? true : false;//Proveedor
            gbi("chkActivo").checked = lista[19] == "ACTIVO" ? true : false;//Estado

            if (lstDireccion.length >= 1) {
                if (lstDireccion[0].trim() != "") {
                    for (let i = 0; i < lstDireccion.length; i++) {
                        addRowDireccion(1, lstDireccion[i].split("▲"));
                    }
                }
            }
            if (lstTelefono.length >= 1) {
                if (lstTelefono[0].trim() != "") {
                    for (var i = 0; i < lstTelefono.length; i++) {
                        addRowTelefono(1, lstTelefono[i].split("▲"));
                    }
                }
            }
            if (lstContacto.length >= 1) {
                if (lstContacto[0].trim() != "") {
                    for (var i = 0; i < lstContacto.length; i++) {
                        addRowContacto(1, lstContacto[i].split("▲"));
                    }
                }
            }
            if (lstCuenta.length >= 1) {
                if (lstCuenta[0].trim() != "") {
                    for (var i = 0; i < lstCuenta.length; i++) {
                        addRowCuenta(1, lstCuenta[i].split("▲"));
                    }
                }
            }
        }
    }
}
//
//exportar
function configBM() {
    var btnPDF = gbi("btnImprimirPDF");
    btnPDF.onclick = function () {
        ExportarPDFs("p", "Proveedores", cabeceras, matriz, "Reporte de Proveedores", "a4", "e");
    }
    var btnImprimir = document.getElementById("btnImprimir");
    btnImprimir.onclick = function () {
        ExportarPDFs("p", "Proveedores", cabeceras, matriz, "Reporte de Proveedores", "a4", "i");
    }
    var btnExcel = gbi("btnImprimirExcel");
    btnExcel.onclick = function () {
        fnExcelReport(cabeceras, matriz);
    }
}
function ExportarPDFs(orientation, titulo, cabeceras, matriz, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    let cabPdf = ["Nº Documento", "Razon Social", "Fecha Modificacion", "Estado"];
    for (var i = 0; i < cabPdf.length; i++) {
        columns[i] = cabPdf[i];
    }
    var data = [];
    let lstDatos = gbi("tbDatos").children[1].children;
    for (var i = 0; i < lstDatos.length; i++) {
        let lstcolDatos = lstDatos[i].children;
        data[i] = [];
        for (var j = 0; j < lstcolDatos.length; j++) {
            data[i][j] = lstcolDatos[j];
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
    doc.text(nombreEmpresa, 10, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 10, 40);
    doc.text(rucEmpresa, 30, 40);
    doc.text("Dirección:", 10, 50);
    doc.text(direccionEmpresa, 50, 50);
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
        doc.save((nombre != undefined ? nombre : "nota_de_ingreso.pdf"));
    }
    else if (v == "i") {
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}
function fnExcelReport(cabeceras) {
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

    let lstDatos = gbi("tbDatos").children[1].children;
    let nRegitros = lstDatos.length;
    for (var i = 0; i < nRegitros; i++) {
        let nCampos = lstDatos[i].children;
        tab_text += "<tr>";
        for (var j = 0; j < nCampos.length - 1; j++) {
            tab_text += "<td>";
            tab_text += nCampos[j].innerHTML;
            tab_text += "</td>";
        }
        tab_text += "</tr>";
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
        sa = txtArea1.document.execCommand("SaveAs", true, "Proveedores.xls");
    }
    else                 //other browser not tested on IE 11
        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    return (sa);
}
//
function cleanControl(css) {
    $("." + css).each(function (index) {
        switch ($(this)[0].localName) {
            case "select":
                $(this).val("");
                break;
            case "input": case "textarea":
                $(this).val("");
                break;
            case "img":
                $(this).attr("src", "/assets/images/image-placeholder.jpg");
                break;
            default:
                break;
        }
    });
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
        textbox.focus();
        return false;
    } else {
        textbox.style.border = "";
        var labels = document.getElementsByTagName("LABEL");
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].htmlFor == id) {
                labels[i].style.color = "";
                break;
            }
        }
        return true;
    }
}
function validate(css) {
    var value = true;
    $("." + css).each(function (index) {
        value = value & validarControl($(this)[0].id);
    });
    return value;
}
//


function BuscarxRuc() {
    if (txtNroDocumento.value.trim().length == 11) {
        var d = txtNroDocumento.value;
        //var url = "https://localhost:7100/api/Sunat/" + gbi("txtNroDocumento").value;
        var url = "http://niveldigital.pe:8035/api/Sunat/" + gbi("txtNroDocumento").value;
        enviarServidor(url, cargarBusquedaSunat);
        gbi("txtNroDocumento").value = d;
    }
    else {

        var d = txtNroDocumento.value;
        //var url = "https://localhost:7100/api/Reniec/" + gbi("txtNroDocumento").value;
        var url = "http://niveldigital.pe:8035/api/Reniec" + gbi("txtNroDocumento").value;
        enviarServidor(url, cargarBusquedaReniec);
        gbi("txtNroDocumento").value = d;
    }
}
function cargarBusquedaSunat(rpta) {
    if (rpta) {
        let objPersona = JSON.parse(rpta);
        $("#txtRazonSocial").val(objPersona.razonSocial);
        $("#txtEstadoC").val(objPersona.estado);
        $("#txtCondicionC").val(objPersona.condicion);
        let cad = "";
        cad += "<tr class='rowDetDireccion'>";
        cad += '<td class="" data-id="0">' + (document.getElementsByClassName("rowDetDireccion").length + 1) + '</td>';
        cad += '<td class="">' + objPersona.direccion + '</td>';
        cad += '<td class="">' + cadButton("Direccion") + '</td>';
        cad += "</tr>";
        document.getElementById("tbDirecciones").innerHTML += cad;
    }
    else {
        MensajeRapido("No hay respuesta del servidor remoto.", "Error", "error");
    }
}
function cargarBusquedaReniec(rpta) {
    if (rpta) {
        let objPersona = JSON.parse(rpta);
        console.log(objPersona);
        $("#txtRazonSocial").val(objPersona.nombres + ' ' + objPersona.apellidoPaterno + ' ' + objPersona.apellidoMaterno);
        $("#txtEstadoC").val("ACTIVO");
        $("#txtCondicionC").val("HABIDO");
        let cad = "";
        cad += "<tr class='rowDetDireccion'>";
        cad += '<td class="" data-id="0">' + (document.getElementsByClassName("rowDetDireccion").length + 1) + '</td>';
        cad += '<td class="">' + objPersona.direccion + '</td>';
        cad += '<td class="">' + cadButton("Direccion") + '</td>';
        cad += "</tr>";
        document.getElementById("tbDirecciones").innerHTML += cad;
    }
    else {
        MensajeRapido("No hay respuesta del servidor remoto.", "Error", "error");
    }
}
// ESTADO Y condicion de contribuyenye







