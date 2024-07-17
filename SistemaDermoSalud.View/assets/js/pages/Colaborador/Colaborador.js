//Global
let TablaColaborador = $("#TablaColaborador");
let G_FechaActual = $("#G_FechaActual").val();
let ColaboradorID = 0;

$(document).ready(function () {
    //Listado
    GetAll()
    $("#tools-export > a.tool-action").on("click", function () {
        var e = $(this).attr("data-action");
        TablaColaborador.DataTable().button(e).trigger()
    })
    //Fin Listado

    //Filtro
    ComboDepartamento("#cboSedeFiltro", "Todas...");
    ComboProfesion("#cboProfesionFiltro", "Todos...");
    ComboTipoEstudio("#cboTipoEstudioFiltro", "Todos...");
    //Fin Filtro

    //CRUD
    ComboDepartamento("#cboDepartamento", "Seleccionar...");
    ComboProvincia("#cboProvincia", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");
    ComboProfesion("#cboProfesion", "Seleccionar...");
    ComboTipoEstudio("#cboNivelEducativo", "Seleccionar...");
    //Fin CRUD

    $("#cboDepartamento").change(function () {
        $("#cboProvincia").val(-1);
        $("#cboDistrito").val(-1);
        ComboProvincia("#cboProvincia", 'Seleccionar...', this.value);
        ComboDistrito("#cboDistrito", 'Seleccionar...');
    })

    $("#cboProvincia").change(function () {
        $("#cboDistrito").val(-1);
        ComboDistrito("#cboDistrito", 'Seleccionar...', this.value);
    })

    //Nuevo
    $("#BtnNuevo").click(function () {
        $("#TituloModal").text("Nuevo Colaborador");
        $("#txtFechaInicio").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
        $("#txtFechaFin").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
        $("#BtnGuardar").fadeIn(0);
        $("#BtnActualizar").fadeOut(0);
        $("#ModalMantenimiento").modal("show");
    })

    $("#BtnGuardar").click(function () {
        Create();
    });
    //Fin Nuevo

    //Actualizar
    $("#TablaColaborador").on("click", ".BtnEditar", function () {
        let id = $(this).attr("data-id");
        ColaboradorID = $(this).attr("data-id");
        $("#TituloModal").text(`Editar Colaborador : ${id}`);
        $("#BtnGuardar").fadeOut(0);
        $("#BtnActualizar").fadeIn(0);
        Get(id);
    })

    $("#BtnActualizar").click(function () {
        Update();
    });
    //Fin Actualizar

    //Filtrar
    $("#BtnFiltrar").click(function () {
        GetAll();
    });
    //Fin Filtrar
})

//Listado
function GetAll() {
    let SedeId = ValidarIntNull($("#cboSedeFiltro").val()) == false ? -1 : $("#cboSedeFiltro").val();
    let SituacionId = $("#cboEstadoFiltro").val();

    $.ajax({
        type: 'GET',
        url: `../Colaborador/GetAllFilter?Situacion=${SituacionId}`,
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
    TablaColaborador.DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[0, "desc"]],
        columns: [
            { data: 'colaboradorID' },
            { data: 'empresa' },
            { data: 'cliente' },
            { data: 'colaboradorView' },
            { data: 'email' },
            { data: 'telefono' },
            { data: 'direccion' },
            { data: 'tipoEstudio' },
            { data: 'profesion' },
            { data: 'anioExperiencia' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    /*return `<center><a class="BtnEditar" data-id="${data}" title="Editar">${data}</a></center>`*/
                    return `<center><b>${data}</b></center>`;
                },
            },
            {
                targets: 1,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                    /*return `<center><a href="javascript:void(0);" class="BtnEditar" data-id="${full.convocatoriaId}" title="Editar">${data}</a></center>`*/
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
                    return `<a>${data}</a>`;
                },
            },
            {
                targets: 5,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 6,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 7,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 8,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 9,
                render: function (data, type, full, meta) {
                    return `<center><a>${data} año(s)</a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    return `<center>
                                <ul class="list-inline" style="margin-bottom: 0px;">
                                    <li class="list-inline-item">
                                        <div class="dropdown">
                                            <button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                <i class="ri-more-fill align-middle"></i>
                                            </button>
                                            <ul class="dropdown-menu dropdown-menu-end" style="">                                                
                                                <li>
                                                    <a href="/Colaborador/EditarColaborador?ColaboradorID=${full.colaboradorID}" class="dropdown-item view-item-btn" data-id="${full.colaboradorID}">
                                                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i>
                                                        Editar
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="/Colaborador/DetalleColaborador?ColaboradorID=${full.colaboradorID}" class="dropdown-item view-item-btn" data-id="${full.colaboradorID}">
                                                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i>
                                                        Ver detalle
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                </ul>
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
function Create() {
    let validacion = true;

    let model = {
        //"FechaSolicitud": $("#txtFechaSolicitud").val(),
        "NumeroDocumentoIdentidad": $("#txtNumDocumento").val(),
        "Nombres": $("#txtNombres").val(),
        "ApellidoParterno": $("#txtApellidoPaterno").val(),
        "ApellidoMarterno": $("#txtApellidoMaterno").val(),
        "Direccion": $("#txtDirección").val(),
        "DistritoResidenciaID": $("#cboDistrito").val(),
        "Telelefono1": $("#txtTelefono").val(),
        "DireccionMail1": $("#txtCorreo").val(),
        "TipoEstudioID": $("#cboNivelEducativo").val(),
        "ProfesionID": $("#cboProfesion").val(),
        "Sexo": $("#cboSexo").val(),
        "AnioExperiencia": $("#txtAnioExperiencia").val(),
        "EmpresaActual": $("#txtEmpresaActual").val(),
        "FechaInicialLaboral": $("#txtFechaInicio").val(),
        "FechaFinalLaboral": $("#txtFechaFin").val()
    };

    //if (model.CargoID == -1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Cargo es obligatorio`, false);
    //}
    //if (model.MotivoConvocatoriaID == -1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Motivo es obligatorio`, false);
    //}
    //if (model.Vacante.length < 0) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Vacante es obligatorio`, false);
    //}
    //if (model.PrioridadConvocatoriaID == -1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Prioridad es obligatorio`, false);
    //}
    //if (model.EmpresaID == -1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Empresa es obligatorio`, false);
    //}
    //if (model.ClienteID == -1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Cliente es obligatorio`, false);
    //}
    //if (model.FechaSolicitud < 1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Solicitud es obligatorio`, false);
    //}
    //if (model.FechaLimite.length < 1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Límite es obligatorio`, false);
    //}

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
                    url: `../Colaborador/Create`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            //$("#ModalMantenimiento").modal("hide");
                            GetAll();
                            Swal.fire("¡Aviso del Sistema!", "Se registro correctamente.", "success")
                            Limpiar();
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
function Get(id) {
    $.ajax({
        type: 'GET',
        url: `../Colaborador/Get?colaboradorID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) {
                    $("#txtId").val(obj.colaboradorID);
                    //$("#txtFechaSolicitud").val(obj.fechaSolicitud);
                    $("#txtNumDocumento").val(obj.numeroDocumentoIdentidad);
                    $("#txtNombres").val(obj.nombres);
                    $("#txtApellidoPaterno").val(obj.apellidoParterno);
                    $("#txtApellidoMaterno").val(obj.apellidoMarterno);
                    $("#txtDirección").val(obj.direccion);
                    $("#cboDepartamento").val(obj.departamentoID);
                    ComboProvincia("#cboProvincia", "Seleccionar", obj.departamentoID, obj.provinciaID);
                    ComboDistrito("#cboDistrito", "Seleccionar", obj.provinciaID, obj.distritoResidenciaID);
                    $("#txtTelefono").val(obj.telelefono1);
                    $("#txtCorreo").val(obj.direccionMail1);
                    $("#cboNivelEducativo").val(obj.tipoEstudioID);
                    $("#cboProfesion").val(obj.profesionID);
                    $("#cboSexo").val(obj.sexo);
                    $("#txtAnioExperiencia").val(obj.anioExperiencia);
                    $("#txtEmpresaActual").val(obj.empresaActual);
                    $("#txtFechaInicio").datepicker('setDate', obj.fechaInicialLaboralView);
                    $("#txtFechaFin").datepicker('setDate', obj.fechaFinalLaboralView);

                    //$("#chkEstado").prop("checked", obj.estado);
                    $("#ModalMantenimiento").modal("show");
                } else {
                    swal("No se encontro la convocatoria", "", "warning");
                }
            } else {
                console.log(response);
            }
        },
        error: function () { },
        complete: function () { }
    });
}
function Update() {
    let validacion = true;

    let model = {
        "ColaboradorID": ColaboradorID,
        "NumeroDocumentoIdentidad": $("#txtNumDocumento").val(),
        "Nombres": $("#txtNombres").val(),
        "ApellidoParterno": $("#txtApellidoPaterno").val(),
        "ApellidoMarterno": $("#txtApellidoMaterno").val(),
        "Direccion": $("#txtDirección").val(),
        "DistritoResidenciaID": $("#cboDistrito").val(),
        "Telelefono1": $("#txtTelefono").val(),
        "DireccionMail1": $("#txtCorreo").val(),
        "TipoEstudioID": $("#cboNivelEducativo").val(),
        "ProfesionID": $("#cboProfesion").val(),
        "Sexo": $("#cboSexo").val(),
        "AnioExperiencia": $("#txtAnioExperiencia").val(),
        "EmpresaActual": $("#txtEmpresaActual").val(),
        "FechaInicialLaboral": $("#txtFechaInicio").val(),
        "FechaFinalLaboral": $("#txtFechaFin").val()
    };

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
                    url: `../Colaborador/Update`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            GetAll();
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó correctamente.", "success")
                            Limpiar();
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "Error API - Update", "error")
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
function Limpiar() {
    ColaboradorID = 0;
    //$("#txtFechaSolicitud").val(G_FechaActual);

    $("#txtNumDocumento").val("");
    $("#txtNombres").val("");
    $("#txtApellidoPaterno").val("");
    $("#txtApellidoMaterno").val("");
    $("#txtDirección").val("");
    $("#cboDistrito").val(-1);
    $("#txtTelefono").val("");
    $("#txtCorreo").val("");
    $("#cboNivelEducativo").val(-1);
    $("#cboProfesion").val(-1);
    $("#cboSexo").val(-1);
    $("#txtAnioExperiencia").val("");
    $("#txtEmpresaActual").val("");
    $("#txtFechaInicio").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
    $("#txtFechaFin").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());

    $("#ModalMantenimiento").modal("hide");
}
function ValidarIntNull(numero) {
    let resultado = true;

    numero = G_FormatoNumero_NotEval(numero);

    if (numero == null || numero == "" || numero == "-1" || numero == "0") {
        resultado = false;
    }

    return resultado;
}
function G_FormatoNumero_NotEval(numero) {
    if (numero == null) {
        numero = 0;
    }

    let Rpt = Number(numero.toString().replace(/,/g, ""));

    if (isNaN(Rpt)) {
        Rpt = 0;
    }

    if (!isFinite(Rpt)) {
        Rpt = 0;
    }

    return Number(Rpt);
}