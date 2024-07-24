var cabeceras = ["idMarca", "Marca", "FechaCreacion", "Estado"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var txtID = document.getElementById("txtID");
var txtCodigo = document.getElementById("txtCodigo");
var txtMarca = document.getElementById("txtMarca");
var chkActivo = document.getElementById("chkActivo");

$(function () {
    var url = "Marca/ObtenerDatos";
    enviarServidor(url, mostrarLista);
    configurarBotonesModal();
});
//index listar Marca
function mostrarLista(rpta) {
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            listar(listaDatos);
        }
    }
}
function listar(r) {
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idMarca: valor[0],
                marca: valor[1],
                fechaCreacion: valor[2],
                estado: valor[3]
            })
        });
        let cols = ["marca", "fechaCreacion", "estado"];
        loadDataTable(cols, newDatos, "idMarca", "tbDatos", cadButtonOptions(), false);
    }    
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
    let lblTituloPanel = document.getElementById('lblTituloPanel');   
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nueva Marca";
            gbi("txtMarca").focus();
            break;
        case 2:
            let idMarca = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Editar Marca";
            TraerDetalle(idMarca);
            show_hidden_Formulario();
            gbi("txtMarca").focus();
            break;
    }
}
function limpiarTodo() {
    gbi("txtID").value = "";
    txtCodigo.value = "";
    txtMarca.value = "";
    chkActivo.checked = true;
}
function eliminar(id) {
    let idMarca = id.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;

    Swal.fire({
        title: '¿Estás seguro de eliminar esta marca?',
        text: '¡No podrás revertir esto!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            var url = "Marca/Eliminar?idMarca=" + idMarca;
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
            mensaje = "Se eliminó la Marca";
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
//

//crear Marca
function configurarBotonesModal() {
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = "Marca/Grabar";
            var frm = new FormData();
            frm.append("idMarca", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("Marca", txtMarca.value);
            frm.append("Estado", chkActivo.checked);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    };
}
function validarFormulario() {
    var error = true;
    if (validarControl("txtMarca")) error = false;
    return error;
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
                mensaje = "Se adicionó la nuevo Marca";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó la Marca";
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
//editar Marca
function TraerDetalle(id) {
    var url = "Marca/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        txtID.value = listaDetalle[0];
        txtCodigo.value = listaDetalle[0];
        txtMarca.value = listaDetalle[1]
        chkActivo.checked = listaDetalle[6] == "ACTIVO" ? true : false;
    }
}
//
//Descarga Archivo
//function LabDescargarPDF(tipoImpresion) {
//    var texto = "";
//    var columns = cabeceras;
//    var data = [];
//    for (var i = 0; i < matriz.length; i++) {
//        data[i] = matriz[i];
//    }
//    var doc = new jsPDF('p', 'pt', "a4");
//    var width = doc.internal.pageSize.width;
//    var height = doc.internal.pageSize.height;
//    var fec = new Date();
//    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
//    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
//    var y = fec.getFullYear();
//    //Datos Cabecera de Página
//    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
//    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
//    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
//    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
//    doc.setFont('helvetica');
//    doc.setFontSize(10);
//    doc.setFontType("bold");
//    doc.text("Dermosalud S.A.C", 30, 30);
//    doc.setFontSize(8);
//    doc.setFontType("normal");
//    doc.text("Ruc:", 30, 40);
//    doc.text("20565643143", 50, 40);
//    doc.text("Dirección:", 30, 50);
//    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 70, 50);
//    doc.setFontType("bold");
//    doc.text("Fecha Impresión", width - 90, 40)
//    doc.setFontType("normal");
//    doc.setFontSize(7);
//    doc.text(fechaImpresion, width - 90, 50)
//    doc.setFontSize(14);
//    doc.setFontType("bold");
//    //Titulo de Documento y datos
//    doc.text("MarcaS", width / 2, 95, "center");// + gbi("txtRequerimiento").value
//    var xic = 140;
//    var altc = 12;
//    doc.setFontType("bold");
//    doc.setFontSize(7);

//    //Inicio de Detalle
//    //Crear Cabecera
//    var xid = 120;
//    var xad = 12;
//    agregarCabeceras();
//    function agregarCabeceras() {
//        doc.setFontType("bold");
//        doc.setFontSize(7);
//        doc.text("ITEM", 30, xid);
//        doc.text("Marca", 120, xid);
//        doc.text("FECHA CREACION", 260, xid);
//        doc.text("ESTADO", 450, xid);
//        //doc.text("SUB TOTAL", width - 208, xid, 'right');
//        //doc.text("IGV", width - 138, xid, 'right');
//        //doc.text("TOTAL", width - 68, xid, 'right');
//        doc.line(30, xid + 3, width - 30, xid + 1);
//        doc.setFontType("normal");
//        doc.setFontSize(6.5);
//    }

//    //Crear Detalle
//    var n = 0;

//    gbi("contentPrincipal").querySelectorAll(".row.panel.salt").forEach((item, index) => {
//        doc.text((index + 1).toString(), 30, xid + xad + (n * xad));//item
//        doc.text(item.children[1].lastChild.innerHTML, 120, xid + xad + (n * xad));//fecha
//        doc.text(item.children[2].lastChild.innerHTML, 260, xid + xad + (n * xad));//Numero doc
//        doc.text(item.children[3].lastChild.innerHTML, 450, xid + xad + (n * xad));//cliente
//        //doc.text(item.children[5].lastChild.innerHTML, width - 208, xid + xad + (n * xad), 'right');//sub total
//        //doc.text(item.children[6].lastChild.innerHTML, width - 138, xid + xad + (n * xad), 'right');//igv
//        //doc.text(item.children[7].lastChild.innerHTML, width - 68, xid + xad + (n * xad), 'right');//total

//        if (xid + xad + (n * xad) > height - 30) {
//            doc.addPage();
//            n = 0;
//            xid = 30;
//            agregarCabeceras();
//            xid = 35;
//        }
//        n += 1;
//    });

//    if (tipoImpresion == "e") {//exportar
//        doc.save("Marcas.pdf");
//    }
//    else if (tipoImpresion == "i") {//imprimir
//        doc.autoPrint();
//        var iframe = document.getElementById('iframePDF');
//        iframe.src = doc.output('dataurlstring');
//    }
//}