//Global
let ColaboradorID = $("#txtIdColaborador").val();
let TablaHistorial = $("#TablaHistorial");
let TablaSanciones = $("#TablaSanciones");
let TablaAccidenteIncidente = $("#TablaAccidenteIncidente");
let G_FechaActual = $("#G_FechaActual").val();
let rutaAdjuntoSancion = "";
let descripcionAdjuntoSancion = "";
let rutaAdjuntoAccidente = "";
let descripcionAdjuntoAccidente = "";
//let PermisoPaginaEliminar = $("#PermisoPaginaEliminar").val();
//let PermisoPaginaModificar = $("#PermisoPaginaModificar").val();
//let PermisoPaginaDetalle = $("#PermisoPaginaDetalle").val();

$(document).ready(function () {
    //Listado
    setTimeout(() => {
        GetHistorialByColaborador(ColaboradorID);
        ListaSancionesByColaborador(ColaboradorID);
        ListaAccidenteIncidenteByColaborador(ColaboradorID);
    }, 1500)

    $("#txtFechaHallazgo").val(currentDate());
    $("#txtFecha").val(currentDate());
    $("#txtFechaReingreso").val(currentDate());

    //IniciarCKEditor();
    //IniciarCKEditor2();

    //COMBOS SSOMA
    ComboCodigoSancion("#cboCodigoSancion", "Seleccionar...");
    ComboTipoSancion("#cboTipoSancion", "Seleccionar...");
    ComboSsomaArea("#cboAreaSancion", "Seleccionar...");
    ComboSsomaArea("#cboAreaAccidente", "Seleccionar...");
    ComboSsomaSede("#cboSedeSancion", "Seleccionar...");
    ComboTipoEvento("#cboTipoEvento", "Seleccionar...");
    ComboNivelAccidente("#cboNivelAccidente", "Seleccionar...");
    ComboAccidentadoPor("#cboAccidentadoPor", "Seleccionar...");
    ComboAccidenteIncapacitado("#cboAccidenteIncapacitado", "Seleccionar...");
    ComboTurno("#cboTurno", "Seleccionar...");
    ComboPlanta("#cboPlanta", "Seleccionar...");
    ComboTipoContacto("#cboTipoContacto", "Seleccionar...");
    ComboSituacion("#cboSituacion", "Seleccionar...");
    //FIN COMBOS SSOMA
    
    //SANCIÓN
    $("#BtnAddSancion").click(function () {
        $("#titleModalSancion").text("Añadir sanción");
        $("#BtnGuardarSancion").fadeIn(0);
        $("#BtnActualizarSancion").fadeOut(0);
        $(".camposEdicion").fadeOut(0);
        GetColaborador(ColaboradorID);
        $("#ModalSancion").modal("show");
    })

    $("#TablaSanciones").on("click", ".BtnEditar", function () {
        let id = $(this).attr("data-idsancion");
        $("#titleModalSancion").text(`Editar sanción: ${id}`);
        $("#BtnGuardarSancion").fadeOut(0);
        $("#BtnActualizarSancion").fadeIn(0);
        $(".camposEdicion").fadeIn(0);
        GetSancion(id);
        $("#ModalSancion").modal("show");
    })

    $("#TablaSanciones").on("click", ".BtnEliminar", function () {
        let id = $(this).attr("data-idsancion");
        DeleteSancion(id);
    })

    $("#BtnGuardarSancion").click(function () {
        CreateSancion();
    })

    $("#BtnActualizarSancion").click(function () {
        UpdateSancion();
    })

    $(".cancelSancion").click(function () {
        LimpiarSancion();
    })
    //FIN SANCIÓN


    //ACCIDENTE INCIDENTE
    $("#BtnAddAccidenteIncidente").click(function () {
        $("#titleModalAccidenteIncidente").text("Añadir accidentes / incidentes");
        $("#BtnGuardarAccidente").fadeIn(0);
        $("#BtnActualizarAccidente").fadeOut(0);
        $(".camposEdicion").fadeOut(0);
        GetColaborador(ColaboradorID);
        $("#ModalAccidenteIncidente").modal("show");
    })

    $("#BtnGuardarAccidente").click(function () {
        CreateAccidenteIncidente();
    })

    $("#TablaAccidenteIncidente").on("click", ".BtnEditar", function () {
        let id = $(this).attr("data-id");
        $("#titleModalAccidenteIncidente").text(`Editar accidentes / incidentes: ${id}`);
        $("#BtnGuardarAccidente").fadeOut(0);
        $("#BtnActualizarAccidente").fadeIn(0);
        $(".camposEdicion").fadeIn(0);
        setTimeout(() => {
            GetAccidenteIncidente(id);
        }, 1500)
        GetAccidenteIncidente(id);
        $("#ModalAccidenteIncidente").modal("show");
    })

    $("#TablaAccidenteIncidente").on("click", ".BtnEliminar", function () {
        let id = $(this).attr("data-id");
        DeleteAccidenteIncidente(id);
    })

    $("#BtnActualizarAccidente").click(function () {
        UpdateAccidenteIncidente();
    })

    $(".closeAccidenteIncidente").click(function () {
        LimpiarAccidenteIncidente();
    })
    //FIN ACCIDENTE INCIDENTE
})

function GetHistorialByColaborador(ColaboradorID) {
    $.ajax({
        type: 'GET',
        url: `../Proceso/ListaProcesoByColaborador?ColaboradorID=${ColaboradorID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            if (response.resultado) {
                PintaTabla(response.data);
            } else {
                console.log(response);
            }
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });

}
function PintaTabla(data) {
    TablaHistorial.DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[2, "desc"]],
        //scrollX: true,
        "pageLength": 50,
        columns: [
            { data: 'codigo' },
            { data: 'tipoProceso' },
            { data: 'fechaView' },
            { data: 'tema' },
            { data: 'evaluador' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 1,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 2,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 3,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 4,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    return `<center>
                                   <a class="dropdown-item view-item-btn" href="/Proceso/RegistroCapacitacion?procesoID=${full.procesoID}" target="_blank" style="color:red;">
                                        <i class="bx bxs-file-pdf fs-20"></i>
                                   </a>
                              </center>`;
                },
            }
        ],
        "ordering": true,
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
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "<<",
                "last": ">>",
                "next": ">",
                "previous": "<"
            }
        },
        buttons: [{
            extend: "print",
            className: "btn dark btn-outline"
        }, {
            extend: "copy",
            className: "btn red btn-outline"
        }, {
            extend: "pdf",
            className: "btn green btn-outline"
        }, {
            extend: "excel",
            className: "btn yellow btn-outline "
        }, {
            extend: "csv",
            className: "btn purple btn-outline "
        }]
    });

}
function ListaSancionesByColaborador(ColaboradorID) {
    $.ajax({
        type: 'GET',
        url: `../Sanciones/ListaSancionesByColaborador?ColaboradorID=${ColaboradorID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $("#TablaSanciones tbody").empty();
        },
        success: function (response) {
            if (response.resultado) {
                PintaTablaSanciones(response.data);
            } else {
                console.log(response);
            }
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });

}
function PintaTablaSanciones(data) {
    TablaSanciones.DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[0, "desc"]],
        //scrollX: true,
        "pageLength": 50,
        columns: [
            { data: 'fecha' },
            { data: 'sede' },
            { data: 'area' },
            { data: 'codigoSancionID' },
            { data: 'tipoSancion' },
            { data: 'observacion' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 1,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 2,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 3,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 4,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 5,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    return `<center>
                                <a type="button" class="btn btn-primary waves-effect waves-light BtnEditar" data-idsancion="${full.sancionID}"><i class="ri-pencil-fill"></i></a>
                                <a type="button" class="btn btn-danger waves-effect waves-light BtnEliminar" data-idsancion="${full.sancionID}"><i class="ri-delete-bin-line"></i></a>
                            </center>`;
                },
            }
        ],
        "ordering": true,
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
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "<<",
                "last": ">>",
                "next": ">",
                "previous": "<"
            }
        },
        buttons: [{
            extend: "print",
            className: "btn dark btn-outline"
        }, {
            extend: "copy",
            className: "btn red btn-outline"
        }, {
            extend: "pdf",
            className: "btn green btn-outline"
        }, {
            extend: "excel",
            className: "btn yellow btn-outline "
        }, {
            extend: "csv",
            className: "btn purple btn-outline "
        }]
    });

}
function ListaAccidenteIncidenteByColaborador(ColaboradorID) {
    $.ajax({
        type: 'GET',
        url: `../AccidenteIncidente/ListaAccidenteIncidenteByColaborador?ColaboradorID=${ColaboradorID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $("#TablaAccidenteIncidente tbody").empty();
        },
        success: function (response) {
            if (response.resultado) {
                PintaTablaAccidenteIncidente(response.data);
            } else {
                console.log(response);
            }
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });

}
function PintaTablaAccidenteIncidente(data) {
    TablaAccidenteIncidente.DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[0, "desc"]],
        //scrollX: true,
        "pageLength": 50,
        columns: [
            { data: 'fecha' },
            { data: 'planta' },
            { data: 'area' },
            { data: 'hora' },
            { data: 'turno' },
            { data: 'descripcion' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 1,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 2,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 3,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 4,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 5,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    return `<center>
                                <a type="button" class="btn btn-primary waves-effect waves-light BtnEditar" data-id="${full.accidenteIncidenteID}"><i class="ri-pencil-fill"></i></a>
                                <a type="button" class="btn btn-danger waves-effect waves-light BtnEliminar" data-id="${full.accidenteIncidenteID}"><i class="ri-delete-bin-line"></i></a>
                            </center>`;
                },
            }
        ],
        "ordering": true,
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
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "<<",
                "last": ">>",
                "next": ">",
                "previous": "<"
            }
        },
        buttons: [{
            extend: "print",
            className: "btn dark btn-outline"
        }, {
            extend: "copy",
            className: "btn red btn-outline"
        }, {
            extend: "pdf",
            className: "btn green btn-outline"
        }, {
            extend: "excel",
            className: "btn yellow btn-outline "
        }, {
            extend: "csv",
            className: "btn purple btn-outline "
        }]
    });
}
function PintaTablaAccidenteIncidente_(data) {
    $("#TablaAccidenteIncidente").DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[0, "desc"]],
        "pageLength": 50,
        columns: [
            { data: 'fecha' },
            { data: 'planta' },
            { data: 'area' },
            { data: 'hora' },
            { data: 'turno' },
            { data: 'descripcion' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 1,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 2,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 3,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 4,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 5,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    return `<center>
                                <a type="button" class="btn btn-primary waves-effect waves-light BtnEditar" data-id="${full.accidneteIncidenteID}"><i class="ri-pencil-fill"></i></a>
                                <a type="button" class="btn btn-danger waves-effect waves-light BtnEliminar" data-id="${full.accidneteIncidenteID}"><i class="ri-delete-bin-line"></i></a>
                            </center>`;
                },
            }
        ],
        "ordering": true,
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
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "<<",
                "last": ">>",
                "next": ">",
                "previous": "<"
            }
        },
        buttons: [{
            extend: "print",
            className: "btn dark btn-outline"
        }, {
            extend: "copy",
            className: "btn red btn-outline"
        }, {
            extend: "pdf",
            className: "btn green btn-outline"
        }, {
            extend: "excel",
            className: "btn yellow btn-outline "
        }, {
            extend: "csv",
            className: "btn purple btn-outline "
        }]
    });

}
function CreateSancion() {
    let validacion = true;

    var model = new FormData();

    if ($("#fileAdjunto").val() != "") {
        let File = $("#fileAdjunto").get(0).files;
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("ColaboradorID", ColaboradorID);
    model.append("CodigoSancionID", $("#cboCodigoSancion").val());
    model.append("TipoSancionID", $("#cboTipoSancion").val());
    model.append("Fecha", $("#txtFechaHallazgo").val());
    model.append("SedeID", $("#cboSedeSancion").val());
    model.append("AreaID", $("#cboAreaSancion").val());
    model.append("CreadoPor", G_IdUsuario_Logeado);
    model.append("Observacion", $("#txtObservacion").val());
    //model.append("Observacion", CKEDITOR.instances['editor1'].getData());

    if (ValidarIntNull(model.get('CodigoSancionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Código Sanción es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TipoSancionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Sanción es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('SedeID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Sede es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AreaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Área es obligatorio`, false);
    }

    if (validacion) {
        Swal.fire({
            title: "¿Estás seguro?",
            text: "No podrás revertir este cambio",
            icon: "warning",
            showCancelButton: true,
            cancelButtonText: "NO",
            confirmButtonText: "SI",
            showLoaderOnConfirm: true,
        }).then((result) => {
            if (result.value) {
                Swal.fire({
                    html: 'Espere por favor...',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    showConfirmButton: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    }
                });

                $.ajax({
                    type: 'POST',
                    url: `../Sanciones/Create`,
                    contentType: "application/json",
                    data: model,
                    enctype: 'multipart/form-data',
                    contentType: false,
                    processData: false,
                    async: true,
                    cache: false,
                    timeout: 600000,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Swal.fire("¡Aviso del Sistema!", "Se registro la sanción correctamente.", "success");                           
                            ListaSancionesByColaborador(ColaboradorID);
                            LimpiarSancion();
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se registro correctamente.", "error")
                            }
                            else if (response.codigoError == "ERROR_CONECT_API") {
                                Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                            }
                            else {
                                console.log(response);
                                Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
}
function GetSancion(id) {
    $.ajax({
        type: 'GET',
        url: `../Sanciones/Get?SancionID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { GetColaborador(ColaboradorID); },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) {
                    $("#txtIdSancion").val(obj.sancionID);
                    //$("#txtColaboradorSancion").val(obj.colaborador);
                    //$("#txtClienteSancion").val(obj.cliente);
                    //$("#txtCargoSancion").val(obj.cargo);
                    //$("#txtAntiguedadSancion").val(obj.fechaInicialLaboral);
                    //$("#txtEdadSancion").val(`${obj.edad} ${obj.edad > 1 ? "Años" : "Año"}`);
                    $("#cboCodigoSancion").val(obj.codigoSancionID);
                    $("#cboTipoSancion").val(obj.tipoSancionID);
                    $("#txtFechaHallazgo").val(obj.fecha.substring(0, 10));
                    $("#cboSedeSancion").val(obj.sedeID);
                    $("#cboAreaSancion").val(obj.areaID);
                    $(".custom-file-label").text(obj.descripcionAdjunto);

                    //ADJUNTO
                    $("#BtnEnlaceAdjunto_url_img").attr("data-url-adjunto-img", obj.urlAdjunto);
                    rutaAdjunto = obj.urlAdjunto;
                    descripcionAdjuntoSancion = obj.descripcionAdjunto;

                    if (obj.urlAdjunto != null) {
                        $("#BtnEnlaceAdjunto_url_img").attr("data-url-adjunto-img", obj.urlAdjunto);
                        if (obj.urlAdjunto.trim().length != 0) {
                            $(".divBtnAdjuntoView_url_img").fadeIn(0);

                            let extensionAdjunto = ObtenerExtension(obj.urlAdjunto);
                            $("#IconoDocumentoImagen").removeClass();
                            if (extensionAdjunto == "jpg" || extensionAdjunto == "png" || extensionAdjunto == "gif" || extensionAdjunto == "jpeg") {
                                $("#IconoDocumentoImagen").addClass("las la-file-image");
                                document.getElementById('IconoDocumentoImagen').style.color = 'lightgreen';
                            } else if (extensionAdjunto == "pdf") {
                                $("#IconoDocumentoImagen").addClass("las la-file-pdf");
                                document.getElementById('IconoDocumentoImagen').style.color = 'red';
                            } else if (extensionAdjunto == "doc" || extensionAdjunto == "docx" || extensionAdjunto == "docm" || extensionAdjunto == "dotx") {
                                $("#IconoDocumentoImagen").addClass("las la-file-word");
                                document.getElementById('IconoDocumentoImagen').style.color = 'blue';
                            } else if (extensionAdjunto == "xlsx" || extensionAdjunto == "xls" || extensionAdjunto == "xlsm" || extensionAdjunto == "xlsb") {
                                $("#IconoDocumentoImagen").addClass("las la-file-excel");
                                document.getElementById('IconoDocumentoImagen').style.color = 'green';
                            } else {
                                $("#IconoDocumentoImagen").addClass("las la-download");
                                document.getElementById('IconoDocumentoImagen').style.color = 'blue';
                            }

                            $("#BtnEliminarAdjunto").fadeIn(0);
                        } else {
                            $(".divBtnAdjuntoView_url_img").fadeOut(0);
                            $("#BtnEliminarAdjunto").fadeOut(0);
                        }
                    }

                    //CKEDITOR.instances.editor1.setData(obj.observacion);
                    $("#txtObservacion").val(obj.observacion);
                } else {
                    swal("No se encontro la sanción", "", "warning");
                }
            } else {
                console.log(response);
            }
        },
        error: function () { },
        complete: function () { }
    });
}
function UpdateSancion() {
    let validacion = true;

    var model = new FormData();

    if ($("#fileAdjunto").val() != "") {
        let File = $("#fileAdjunto").get(0).files;
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("SancionID", $("#txtIdSancion").val());
    model.append("ColaboradorID", ColaboradorID);
    model.append("CodigoSancionID", $("#cboCodigoSancion").val());
    model.append("TipoSancionID", $("#cboTipoSancion").val());
    model.append("Fecha", $("#txtFechaHallazgo").val());
    model.append("SedeID", $("#cboSedeSancion").val());
    model.append("AreaID", $("#cboAreaSancion").val());
    model.append("ModificadoPor", G_IdUsuario_Logeado);
    //model.append("Observacion", CKEDITOR.instances['editor1'].getData());
    model.append("Observacion", $("#txtObservacion").val());
    model.append("UrlAdjunto", rutaAdjunto);
    model.append("DescripcionAdjunto", descripcionAdjuntoSancion);

    if (ValidarIntNull(model.get('CodigoSancionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Código Sanción es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TipoSancionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Sanción es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('SedeID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Sede es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AreaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Área es obligatorio`, false);
    }

    if (validacion) {
        Swal.fire({
            title: "¿Estás seguro?",
            text: "No podrás revertir este cambio",
            icon: "warning",
            showCancelButton: true,
            cancelButtonText: "NO",
            confirmButtonText: "SI",
            showLoaderOnConfirm: true,
        }).then((result) => {
            if (result.value) {
                Swal.fire({
                    html: 'Espere por favor...',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    showConfirmButton: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    }
                });

                $.ajax({
                    type: 'POST',
                    url: `../Sanciones/Update`,
                    contentType: "application/json",
                    data: model,
                    enctype: 'multipart/form-data',
                    contentType: false,
                    processData: false,
                    async: true,
                    cache: false,
                    timeout: 600000,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó la sanción correctamente.", "success");
                            $("#ModalSancion").modal("hide");
                            ListaSancionesByColaborador(ColaboradorID);
                            LimpiarSancion();
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se actualizó correctamente.", "error")
                            }
                            else if (response.codigoError == "ERROR_CONECT_API") {
                                Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                            }
                            else {
                                console.log(response);
                                Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
}
function DeleteSancion(id) {
    var model = new FormData();
    model.append("SancionID", id);

    Swal.fire({
        title: "¿Estás seguro de eliminar la sanción?",
        text: "No podrás revertir este cambio",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "NO",
        confirmButtonText: "SI",
        showLoaderOnConfirm: true,
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                html: 'Espere por favor...',
                allowOutsideClick: false,
                allowEscapeKey: false,
                allowEnterKey: false,
                showConfirmButton: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                }
            });

            $.ajax({
                //type: 'DELETE',
                //url: `../Sanciones/Delete?SancionID=${id}`,
                //contentType: "application/json; charset=utf-8",
                //async: true,
                //cache: false,
                type: 'POST',
                url: `../Sanciones/Delete`,
                contentType: "application/json",
                data: model,
                enctype: 'multipart/form-data',
                contentType: false,
                processData: false,
                async: true,
                cache: false,
                timeout: 600000,
                beforeSend: function () {
                },
                success: function (response) {
                    if (response.resultado) {
                        Swal.fire("¡Aviso del Sistema!", "Se elimino la sanción correctamente.", "success");
                        ListaSancionesByColaborador(ColaboradorID);
                    } else {
                        if (response.codigoError == "ERROR_API") {
                            Swal.fire("¡Aviso del Sistema!", "No se elimino correctamente.", "error")
                        }
                        else if (response.codigoError == "ERROR_CONECT_API") {
                            Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                        }
                        else {
                            console.log(response);
                            Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                        }
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                },
                complete: function () {
                }
            });
        }
    });
}
function LimpiarSancion() {
    //$("#txtFechaHallazgo").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
    $("#txtFechaHallazgo").val(currentDate());
    $("#cboCodigoSancion").val(-1);
    $("#cboAreaSancion").val(-1);
    $("#cboTipoSancion").val(-1);
    $("#cboSedeSancion").val(-1);
    document.getElementById("fileAdjunto").value = "";
    $("#txtObservacion").val("");
   //CKEDITOR.instances.editor1.setData('');
    $("#ModalSancion").modal("hide");
}
function CreateAccidenteIncidente() {
    let validacion = true;

    var model = new FormData();

    if ($("#fileAdjuntoAI").val() != "") {
        let File = $("#fileAdjuntoAI").get(0).files;
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("ColaboradorID", ColaboradorID);
    model.append("TipoEventoID", $("#cboTipoEvento").val());
    model.append("NivelAccidenteID", $("#cboNivelAccidente").val());
    model.append("AccidentadoPorID", $("#cboAccidentadoPor").val());
    model.append("AccidenteIncapacitadoID", $("#cboAccidenteIncapacitado").val());
    model.append("Fecha", $("#txtFecha").val());
    model.append("TurnoID", $("#cboTurno").val());
    model.append("Hora", $("#txtHora").val());
    model.append("PlantaID", $("#cboPlanta").val());
    model.append("AreaID", $("#cboAreaAccidente").val());
    model.append("TipoContactoID", $("#cboTipoContacto").val());
    model.append("ParteCuerpoAfectada", $("#txtParteCuerpoAfectada").val());
    model.append("ActividadRealizada", $("#txtActividadRealizada").val());
    model.append("BajoSupervision", $("#txtBajoSupervision").val());
    model.append("Diagnostico", $("#txtDiagnostico").val());
    model.append("DiasDM", $("#txtDiasDM").val());
    model.append("DMCargoANSI", $("#txtDMCargoANSI").val());
    model.append("SituacionID", $("#cboSituacion").val());
    model.append("FechaReingreso", $("#txtFechaReingreso").val());
    model.append("Accidentador", $("#txtAccidentador").val());
    model.append("AntiguedadEmpleo", $("#txtAntiguedadEmpleo").val());
    model.append("AntiguedadPuesto", $("#txtAntiguedadPuesto").val());
    model.append("Descripcion", $("#txtObservacionAI").val());
    //model.append("Descripcion", CKEDITOR.instances['editor2'].getData());
    model.append("CreadoPor", G_IdUsuario_Logeado);    

    if (ValidarIntNull(model.get('TipoEventoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Evento es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('NivelAccidenteID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Nivel Accidente es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AccidentadoPorID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Accidentado Por es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AccidenteIncapacitadoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Accidente Incapacitado es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TurnoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Turno es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('PlantaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Planta es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AreaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Área es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TipoContactoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Contacto es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('SituacionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Situación es obligatorio`, false);
    }

    if (validacion) {
        Swal.fire({
            title: "¿Estás seguro?",
            text: "No podrás revertir este cambio",
            icon: "warning",
            showCancelButton: true,
            cancelButtonText: "NO",
            confirmButtonText: "SI",
            showLoaderOnConfirm: true,
        }).then((result) => {
            if (result.value) {
                Swal.fire({
                    html: 'Espere por favor...',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    showConfirmButton: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    }
                });

                $.ajax({
                    type: 'POST',
                    url: `../AccidenteIncidente/Create`,
                    contentType: "application/json",
                    data: model,
                    enctype: 'multipart/form-data',
                    contentType: false,
                    processData: false,
                    async: true,
                    cache: false,
                    timeout: 600000,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Swal.fire("¡Aviso del Sistema!", "Se registro el accidente / incidente correctamente.", "success");
                            ListaAccidenteIncidenteByColaborador(ColaboradorID);
                            LimpiarAccidenteIncidente();
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se registro correctamente.", "error")
                            }
                            else if (response.codigoError == "ERROR_CONECT_API") {
                                Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                            }
                            else {
                                console.log(response);
                                Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
}
function GetAccidenteIncidente(id) {
    $.ajax({
        type: 'GET',
        url: `../AccidenteIncidente/Get?AccidenteIncidenteID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { GetColaborador(ColaboradorID); },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) {
                    $("#txtIdAccidenteIncidente").val(obj.accidenteIncidenteID);
                    //$("#txtColaboradorAI").val(obj.colaborador);
                    //$("#txtClienteAI").val(obj.cliente);
                    //$("#txtCargoAI").val(obj.cargo);
                    //$("#txtAntiguedadAI").val(obj.fechaInicialLaboral);
                    //$("#txtEdadAI").val(`${obj.edad} ${obj.edad > 1 ? "Años" : "Año"}`);
                    $("#cboTipoEvento").val(obj.tipoEventoID);
                    $("#cboNivelAccidente").val(obj.nivelAccidenteID);
                    $("#cboAccidentadoPor").val(obj.accidentadoPorID);
                    $("#cboAccidenteIncapacitado").val(obj.accidenteIncapacitadoID);
                    $("#txtFecha").val(obj.fecha.substring(0, 10));
                    $("#cboTurno").val(obj.turnoID);
                    $("#txtHora").val(obj.hora);
                    $("#cboPlanta").val(obj.plantaID);
                    $("#cboAreaAccidente").val(obj.areaID);
                    $("#cboTipoContacto").val(obj.tipoContactoID);
                    $("#txtParteCuerpoAfectada").val(obj.parteCuerpoAfectada);
                    $("#txtActividadRealizada").val(obj.actividadRealizada);
                    $("#txtBajoSupervision").val(obj.bajoSupervision);
                    $("#txtDiagnostico").val(obj.diagnostico);
                    $("#txtDiasDM").val(obj.diasDM);
                    $("#txtDMCargoANSI").val(obj.dmCargoANSI);
                    $("#cboSituacion").val(obj.situacionID);                    
                    $("#txtFechaReingreso").val(obj.fechaReingreso.substring(0, 10));
                    $("#txtAccidentador").val(obj.accidentador);
                    $("#txtAntiguedadEmpleo").val(obj.antiguedadEmpleo);
                    $("#txtAntiguedadPuesto").val(obj.antiguedadPuesto);
                    $(".custom-file-label").text(obj.descripcionAdjunto);

                    //ADJUNTO
                    $("#BtnEnlaceAdjunto_url_img").attr("data-url-adjunto-img", obj.urlAdjunto);
                    rutaAdjuntoAccidente = obj.urlAdjunto;
                    descripcionAdjuntoAccidente = obj.descripcionAdjunto;

                    if (obj.urlAdjunto != null) {
                        $("#BtnEnlaceAdjunto_url_img").attr("data-url-adjunto-img", obj.urlAdjunto);
                        if (obj.urlAdjunto.trim().length != 0) {
                            $(".divBtnAdjuntoView_url_img").fadeIn(0);

                            let extensionAdjunto = ObtenerExtension(obj.urlAdjunto);
                            $("#IconoDocumentoImagen").removeClass();
                            if (extensionAdjunto == "jpg" || extensionAdjunto == "png" || extensionAdjunto == "gif" || extensionAdjunto == "jpeg") {
                                $("#IconoDocumentoImagen").addClass("las la-file-image");
                                document.getElementById('IconoDocumentoImagen').style.color = 'lightgreen';
                            } else if (extensionAdjunto == "pdf") {
                                $("#IconoDocumentoImagen").addClass("las la-file-pdf");
                                document.getElementById('IconoDocumentoImagen').style.color = 'red';
                            } else if (extensionAdjunto == "doc" || extensionAdjunto == "docx" || extensionAdjunto == "docm" || extensionAdjunto == "dotx") {
                                $("#IconoDocumentoImagen").addClass("las la-file-word");
                                document.getElementById('IconoDocumentoImagen').style.color = 'blue';
                            } else if (extensionAdjunto == "xlsx" || extensionAdjunto == "xls" || extensionAdjunto == "xlsm" || extensionAdjunto == "xlsb") {
                                $("#IconoDocumentoImagen").addClass("las la-file-excel");
                                document.getElementById('IconoDocumentoImagen').style.color = 'green';
                            } else {
                                $("#IconoDocumentoImagen").addClass("las la-download");
                                document.getElementById('IconoDocumentoImagen').style.color = 'blue';
                            }

                            $("#BtnEliminarAdjunto").fadeIn(0);
                        } else {
                            $(".divBtnAdjuntoView_url_img").fadeOut(0);
                            $("#BtnEliminarAdjunto").fadeOut(0);
                        }
                    }

                    $("#txtObservacionAI").val(obj.descripcion);
                    //CKEDITOR.instances.editor2.setData(obj.descripcion);
                } else {
                    swal("No se encontro el accidente / incidente", "", "warning");
                }
            } else {
                console.log(response);
            }
        },
        error: function () { },
        complete: function () { }
    });
}
function UpdateAccidenteIncidente() {
    let validacion = true;

    var model = new FormData();

    if ($("#fileAdjuntoAI").val() != "") {
        let File = $("#fileAdjuntoAI").get(0).files;
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("AccidenteIncidenteID", $("#txtIdAccidenteIncidente").val());
    model.append("ColaboradorID", ColaboradorID);
    model.append("TipoEventoID", $("#cboTipoEvento").val());
    model.append("NivelAccidenteID", $("#cboNivelAccidente").val());
    model.append("AccidentadoPorID", $("#cboAccidentadoPor").val());
    model.append("AccidenteIncapacitadoID", $("#cboAccidenteIncapacitado").val());
    model.append("Fecha", $("#txtFecha").val());
    model.append("TurnoID", $("#cboTurno").val());
    model.append("Hora", $("#txtHora").val());
    model.append("PlantaID", $("#cboPlanta").val());
    model.append("AreaID", $("#cboAreaAccidente").val());
    model.append("TipoContactoID", $("#cboTipoContacto").val());
    model.append("ParteCuerpoAfectada", $("#txtParteCuerpoAfectada").val());
    model.append("ActividadRealizada", $("#txtActividadRealizada").val());
    model.append("BajoSupervision", $("#txtBajoSupervision").val());
    model.append("Diagnostico", $("#txtDiagnostico").val());
    model.append("DiasDM", $("#txtDiasDM").val());
    model.append("DMCargoANSI", $("#txtDMCargoANSI").val());
    model.append("SituacionID", $("#cboSituacion").val());
    model.append("FechaReingreso", $("#txtFechaReingreso").val());
    model.append("Accidentador", $("#txtAccidentador").val());
    model.append("AntiguedadEmpleo", $("#txtAntiguedadEmpleo").val());
    model.append("AntiguedadPuesto", $("#txtAntiguedadPuesto").val());
    model.append("Descripcion", $("#txtObservacionAI").val());
    //model.append("Descripcion", CKEDITOR.instances['editor2'].getData());
    model.append("ModificadoPor", G_IdUsuario_Logeado);
    model.append("UrlAdjunto", rutaAdjuntoAccidente);
    model.append("DescripcionAdjunto", descripcionAdjuntoAccidente);

    if (ValidarIntNull(model.get('TipoEventoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Evento es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('NivelAccidenteID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Nivel Accidente es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AccidentadoPorID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Accidentado Por es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AccidenteIncapacitadoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Accidente Incapacitado es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TurnoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Turno es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('PlantaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Planta es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('AreaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Área es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TipoContactoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Contacto es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('SituacionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Situación es obligatorio`, false);
    }

    if (validacion) {
        Swal.fire({
            title: "¿Estás seguro?",
            text: "No podrás revertir este cambio",
            icon: "warning",
            showCancelButton: true,
            cancelButtonText: "NO",
            confirmButtonText: "SI",
            showLoaderOnConfirm: true,
        }).then((result) => {
            if (result.value) {
                Swal.fire({
                    html: 'Espere por favor...',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    showConfirmButton: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    }
                });

                $.ajax({
                    type: 'POST',
                    url: `../AccidenteIncidente/Update`,
                    contentType: "application/json",
                    data: model,
                    enctype: 'multipart/form-data',
                    contentType: false,
                    processData: false,
                    async: true,
                    cache: false,
                    timeout: 600000,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó el accidente / incidente correctamente.", "success");
                            $("#ModalAccidenteIncidente").modal("hide");
                            ListaAccidenteIncidenteByColaborador(ColaboradorID);
                            LimpiarAccidenteIncidente();
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se actualizó correctamente.", "error")
                            }
                            else if (response.codigoError == "ERROR_CONECT_API") {
                                Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                            }
                            else {
                                console.log(response);
                                Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
}
function DeleteAccidenteIncidente(id) {
    var model = new FormData();
    model.append("AccidenteIncidenteID", id);

    Swal.fire({
        title: "¿Estás seguro de eliminar la sanción?",
        text: "No podrás revertir este cambio",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "NO",
        confirmButtonText: "SI",
        showLoaderOnConfirm: true,
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                html: 'Espere por favor...',
                allowOutsideClick: false,
                allowEscapeKey: false,
                allowEnterKey: false,
                showConfirmButton: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                }
            });

            $.ajax({
                //type: 'DELETE',
                //url: `../AccidenteIncidente/Delete?AccidenteIncidenteID=${id}`,
                //contentType: "application/json; charset=utf-8",
                //async: true,
                //cache: false,
                type: 'POST',
                url: `../AccidenteIncidente/Delete`,
                contentType: "application/json",
                data: model,
                enctype: 'multipart/form-data',
                contentType: false,
                processData: false,
                async: true,
                cache: false,
                timeout: 600000,
                beforeSend: function () {
                },
                success: function (response) {
                    if (response.resultado) {
                        Swal.fire("¡Aviso del Sistema!", "Se elimino el accidente e incidente correctamente.", "success");
                        ListaAccidenteIncidenteByColaborador(ColaboradorID);
                    } else {
                        if (response.codigoError == "ERROR_API") {
                            Swal.fire("¡Aviso del Sistema!", "No se elimino correctamente.", "error")
                        }
                        else if (response.codigoError == "ERROR_CONECT_API") {
                            Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                        }
                        else {
                            console.log(response);
                            Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                        }
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                },
                complete: function () {
                }
            });
        }
    });
}
function LimpiarAccidenteIncidente() {
    $("#txtFecha").val(currentDate());
    $("#cboTipoEvento").val(-1);
    $("#cboNivelAccidente").val(-1);
    $("#cboAccidentadoPor").val(-1);
    $("#cboAccidenteIncapacitado").val(-1);
    $("#cboTurno").val(-1);
    $("#txtHora").val("");
    $("#cboPlanta").val(-1);
    $("#cboAreaAccidente").val(-1);
    $("#cboTipoContacto").val(-1);
    $("#txtParteCuerpoAfectada").val("");
    $("#txtActividadRealizada").val("");
    $("#txtBajoSupervision").val("");
    $("#txtDiagnostico").val("");
    $("#txtDiasDM").val("");
    $("#txtDMCargoANSI").val("");
    $("#cboSituacion").val(-1);
    $("#txtFechaReingreso").val(currentDate());
    $("#txtAccidentador").val("");
    $("#txtAntiguedadEmpleo").val("");
    $("#txtAntiguedadPuesto").val("");
    $("#txtObservacionAI").val("");
    document.getElementById("fileAdjuntoAI").value = "";
    //CKEDITOR.instances.editor2.setData('');
    $("#ModalAccidenteIncidente").modal("hide");
}
function IniciarCKEditor(idtp = null, url = null, alt_dinamica = 0) {

    if (idtp == null || idtp == -1) {
        if (CKEDITOR.instances.editor1) {
            CKEDITOR.instances.editor1.destroy();
        }
        CKEDITOR.replace('editor1', {
            height: '500px',
            width: '100%',
            contentsCss: ['/assets/libs/ckeditor4/mystyles.css'],
            customConfig: ['/config.js'],
            bodyClass: "document-editor"
        });
    } else {
        if (CKEDITOR.instances.editor1) {
            CKEDITOR.instances.editor1.destroy();
        }
        CKEDITOR.replace('editor1', {
            height: '500px',
            width: '100%',
            contentsCss: ['/assets/libs/ckeditor4/mystyles.css', String(url)],
            customConfig: ['/config.js'],
            bodyClass: "document-editor",
            stylesSet: [
                {
                    name: 'Green Title',
                    element: 'p',
                    styles:
                    {
                        'color': 'Green !important'
                    }
                },
                {
                    name: 'Green Title',
                    element: 'p',
                    styles:
                    {
                        'color': 'Green !important'
                    }
                }
            ]
        });
    }

}
function IniciarCKEditor2(idtp = null, url = null, alt_dinamica = 0) {

    if (idtp == null || idtp == -1) {
        if (CKEDITOR.instances.editor2) {
            CKEDITOR.instances.editor2.destroy();
        }
        CKEDITOR.replace('editor2', {
            height: '500px',
            width: '100%',
            contentsCss: ['/assets/libs/ckeditor4/mystyles.css'],
            customConfig: ['/config.js'],
            bodyClass: "document-editor"
        });
    } else {
        if (CKEDITOR.instances.editor2) {
            CKEDITOR.instances.editor2.destroy();
        }
        CKEDITOR.replace('editor2', {
            height: '500px',
            width: '100%',
            contentsCss: ['/assets/libs/ckeditor4/mystyles.css', String(url)],
            customConfig: ['/config.js'],
            bodyClass: "document-editor",
            stylesSet: [
                {
                    name: 'Green Title',
                    element: 'p',
                    styles:
                    {
                        'color': 'Green !important'
                    }
                },
                {
                    name: 'Green Title',
                    element: 'p',
                    styles:
                    {
                        'color': 'Green !important'
                    }
                }
            ]
        });
    }

}
function addCssCK(ancho, items) {
    CKEDITOR.addCss('body{background:blue;}');
}
function ObtenerExtension(filename) {
    return filename.split('.').pop();
}
function GetColaborador(id) {
    $.ajax({
        type: 'GET',
        url: `../Colaborador/Get?colaboradorID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(".colaborador").val("");
            $(".cliente").val("");
            $(".cargo").val("");
            $(".fantiguedad").val("");
            $(".edad").val("");
        },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) {
                    $(".colaborador").val(obj.nombreColaborador);
                    $(".cliente").val(obj.cliente);
                    $(".cargo").val(obj.cargo);
                    $(".fantiguedad").val(obj.fechaInicialLaboral);
                    $(".edad").val(`${obj.edad == null ? "" : obj.edad > 1 ? obj.edad + " Años" : obj.edad + "Año" }`);                    
                } else {
                    swal("No se encontro el colaborador", "", "warning");
                }
            } else {
                console.log(response);
            }
        },
        error: function () { },
        complete: function () { }
    });
}