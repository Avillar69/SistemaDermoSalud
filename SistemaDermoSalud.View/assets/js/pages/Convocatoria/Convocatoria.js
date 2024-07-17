//Global
let orderTable = $("#orderTable");
let convocatoriaId = 0;
let cargoId = 0;
let distritoId = 0;
//let G_FechaActual = $("#G_FechaActual").val();
let PermisoPaginaEliminar = $("#PermisoPaginaEliminar").val();
let PermisoPaginaModificar = $("#PermisoPaginaModificar").val();
let PermisoPaginaDetalle = $("#PermisoPaginaDetalle").val();
$(document).ready(function () {
    //Listado
    GetAll()

    $("#tools-export > a.tool-action").on("click", function () {
        var e = $(this).attr("data-action");
        orderTable.DataTable().button(e).trigger()
    })
    //Fin Listado

    //ComboEmpresa("#cboEmpresaFiltro", "Todas...");
    //ComboCargo("#cboCargoFiltro", "Todos...");
    ComboEstadoConvocatoria("#cboStatusFiltro", "Status")
    ComboDepartamento("#cboSedeFiltro", "Sede");
    ComboCandidato("#buscar_cboCandidato", "Buscar...", true);
    loadSelect2("/Colaborador/ListaColaboradorPostulante?Situacion=ACT", "candidatoID", "colaboradorView", "cboColaborador","AddCandidato");
    ComboEmpresaSelect2("/Empresa/GetAll", "empresaID", "razonSocial", "cboEmpresaFiltro");
    ComboCargoSelect2("/Cargo/CargosInConvocatoria", "cargoID", "descripcion", "cboCargoFiltro");
    loadSelect2("/Colaborador/ListaResponsablesInConvocatoria", "responsableID", "responsable", "cboResponsableFiltro");
    ComboEtapaConvocatoria("#cboEtapaFiltro", "Etapa")

    $("#BtnGuardar").click(function () {
        InsertPostulante();
    });
    //Fin Nuevo

    //Actualizar
    $("#orderTable").on("click", ".BtnEditar", function () {
        let id = $(this).attr("data-id");
        let cargo = $(this).attr("data-cargo");
        convocatoriaId = $(this).attr("data-id");
        cargoId = $(this).attr("data-cargoid");
        distritoId = $(this).attr("data-distritoid");
        $("#TituloModal").text(`Añadir candidato a la convocatoria de ${cargo} - ${id}`);
        $("#AddCandidato").modal("show");
    })

    $("#orderTable").on("click", ".BtnTerna", function () {
        let CargoTerna = $(this).attr("data-cargo");
        let Cliente = $(this).attr("data-cliente");
        localStorage.setItem("CargoTerna", CargoTerna);
        localStorage.setItem("Cliente", Cliente);
    })

    //Filtrar
    $("#BtnFiltrar").click(function () {
        GetAll();
    });
    $(".closecandidato").click(function () {
        Limpiar();
    });    
    $("#BtnConvocatoriaDetalle").click(function () {
        location.href = `/Convocatoria/Detalle?convocatoriaId=${convocatoriaId}`;
    });

    $("#BtnLimpiarFiltro").click(function () {debugger
        $('#cboEmpresaFiltro').val(null).trigger('change');
        $('#cboCargoFiltro').val(null).trigger('change');
        $('#cboSedeFiltro').val(-1);
        $('#cboStatusFiltro').val(-1);
        $('#cboResponsableFiltro').val(null).trigger('change');
        $('#cboEtapaFiltro').val(-1);
    });
    
})

//Listado
function GetAll() {
    let EmpresaId = $("#cboEmpresaFiltro").val();
    let CargoId = ValidarIntNull($("#cboCargoFiltro").val()) == false ? -1 : $("#cboCargoFiltro").val();
    let StatusId = ValidarIntNull($("#cboStatusFiltro").val()) == false ? -1 : $("#cboStatusFiltro").val();
    let SedeId = ValidarIntNull($("#cboSedeFiltro").val()) == false ? -1 : $("#cboSedeFiltro").val();
    let ResponsableID = ValidarIntNull($("#cboResponsableFiltro").val()) == false ? -1 : $("#cboResponsableFiltro").val();
    let EtapaID = ValidarIntNull($("#cboEtapaFiltro").val()) == false ? -1 : $("#cboEtapaFiltro").val();

    $.ajax({
        type: 'GET',
        url: `../Convocatoria/GetAllFilter?EmpresaId=${EmpresaId}&CargoId=${CargoId}&StatusId=${StatusId}&SedeId=${SedeId}
        &ResponsableID=${ResponsableID}&EtapaID=${EtapaID}`,
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
    orderTable.DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[0, "desc"]],
        //scrollX: true,
        "pageLength": 50,
        columns: [
            { data: 'convocatoriaId' },
            { data: 'fechaSolicitudView' },
            { data: 'empresa' },
            { data: 'cliente' },
            { data: 'cargo' },
            { data: 'sede' },
            { data: 'responsable' },
            { data: 'vacante' },
            { data: 'presentados' },
            { data: 'vacanteCubierta' },
            { data: '' },
            { data: 'tipoProceso' },
            { data: 'diasDuracionProceso' },
            { data: 'estadoProceso' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    /*return `<center><a href="javascript:void(0);" class="BtnEditar" data-id="${data}" title="Editar">${data}</a></center>`*/
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
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 10,
                render: function (data, type, full, meta) {
                    let calculo = G_FormatoDecimales_Por_Numero_Decimales((full.presentados / full.vacante) * 100, 2);
                    return `<div class="progress progress-xs">
                                <div class="progress-bar" role="progressbar" aria-valuenow="${calculo}" aria-valuemin="0" aria-valuemax="100" style="width: ${calculo}%;"></div>                                                
                            </div>
                            <center><font color="blue">${calculo}%</font></center>`;
                },
            },
            {
                targets: 11,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 12,
                render: function (data, type, full, meta) {
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: 13,
                render: function (data, type, full, meta) {
                    if (full.estadoProcesoCodigo == "PENDI") {
                        return `<center><a><span class="badge rounded-pill bg-primary">${data}</span></a></center>`;
                    }
                    else if (full.estadoProcesoCodigo == "ENPRO") {
                        return `<center><a><span class="badge rounded-pill bg-warning">${data}</span></a></center>`;
                    }
                    else if (full.estadoProcesoCodigo == "CANCE") {
                        return `<center><a><span class="badge rounded-pill bg-danger">${data}</span></a></center>`;
                    }
                    else if (full.estadoProcesoCodigo == "CERRA") {
                        return `<center><a><span class="badge rounded-pill bg-success">${data}</span></a></center>`;
                    }
                    else {
                        return `<center></center>`;
                    }
                    //return `<center><a><span class="badge rounded-pill bg-danger">${data}</span></a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    let tr = `<center>
                                    <div class="dropdown">
                                        <a href="javascript:void(0);" class="btn btn-light btn-icon" id="dropdownMenuLink3" data-bs-toggle="dropdown" aria-expanded="false">
                                            <i class="ri-equalizer-fill"></i>
                                        </a>
                                        <ul class="dropdown-menu dropdown-menu-end" style="">`;

                    if (full.estadoProcesoCodigo == "PENDI") {
                        if (PermisoPaginaDetalle == 1) {
                            tr += `<li>
                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Detalle?convocatoriaId=${full.convocatoriaId}">
                                        <i class="ri-information-fill me-2 align-middle text-muted"></i>
                                            Ver detalle
                                    </a>
                                </li>`;
                        }
                        if (PermisoPaginaModificar == 1) {
                            tr += ` <li>
                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Editar?convocatoriaId=${full.convocatoriaId}">
                                        <i class="ri-eye-fill align-bottom me-2 align-middle text-muted"></i>
                                            Editar
                                    </a>
                                </li>`;
                        }
                        tr += `</ul></div></center>`;
                    }
                    else if (full.estadoProcesoCodigo == "ENPRO") {
                        if (PermisoPaginaDetalle == 1) {
                            tr += `<li>
                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Detalle?convocatoriaId=${full.convocatoriaId}">
                                        <i class="ri-information-fill me-2 align-middle text-muted"></i>
                                            Ver detalle
                                    </a>
                                </li>`;
                        }
                        if (PermisoPaginaModificar == 1) {
                            tr += ` <li>
                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Editar?convocatoriaId=${full.convocatoriaId}">
                                        <i class="ri-eye-fill align-bottom me-2 align-middle text-muted"></i>
                                            Editar
                                    </a>
                                </li>`;
                        }
                        tr += `<li>
                                    <a href="javascript:void(0);" class="dropdown-item remove-item-btn BtnEditar" data-id="${full.convocatoriaId}" data-cargo="${full.cargo}" data-cargoid="${full.cargoID}" data-distritoid="${full.distritoID}">
                                        <i class="ri-user-2-fill me-2 align-middle text-muted"></i>
                                            Añadir Candidatos
                                    </a>
                                </li>`;
                        if (full.numTerna > 0) {
                            tr += `<li>
                                    <a class="dropdown-item view-item-btn BtnTerna" href="/Convocatoria/Ternas?convocatoriaId=${full.convocatoriaId}" data-cargo="${full.cargo}" data-cliente="${full.cliente}">
                                        <i class="ri-eye-2-fill me-2 align-middle text-muted"></i>
                                            Ver Ternas
                                    </a>
                                </li>`
                        }
                                
                         tr += `</ul></div></center>`;
                    }
                    else if (full.estadoProcesoCodigo == "CANCE") {
                        if (PermisoPaginaDetalle == 1) {
                            tr += `<li>
                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Detalle?convocatoriaId=${full.convocatoriaId}">
                                        <i class="ri-information-fill me-2 align-middle text-muted"></i>
                                            Ver detalle
                                    </a>
                                </li>`;
                        }
                        tr += `</ul></div></center>`;
                    }
                    else if (full.estadoProcesoCodigo == "CERRA") {
                        if (PermisoPaginaDetalle == 1) {
                            tr += `<li>
                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Detalle?convocatoriaId=${full.convocatoriaId}">
                                        <i class="ri-information-fill me-2 align-middle text-muted"></i>
                                            Ver detalle
                                    </a>
                                </li>`;
                        }
                        if (full.numTerna > 0) {
                            tr += `<li>
                                    <a class="dropdown-item view-item-btn BtnTerna" href="/Convocatoria/Ternas?convocatoriaId=${full.convocatoriaId}" data-cargo="${full.cargo}" data-cliente="${full.cliente}">
                                        <i class="ri-eye-2-fill me-2 align-middle text-muted"></i>
                                            Ver Ternas
                                    </a>
                                </li>`
                        }

                        tr += `</ul></div></center>`;
                    }

                    return tr;
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
function InsertPostulante() {
    let validacion = true;

    let model = {
        "ReclutadorId": G_IdUsuario_Logeado,
        "ColaboradorID": $("#cboColaborador").val(),
        "CargoID": cargoId,
        "DistritoID": distritoId,
        "Comentarios": "Candidato registrado desde la web administrativa.",
        "ConvocatoriaPersonalID": convocatoriaId,
        "CreadoPor": G_IdUsuario_Logeado
    };

    if (model.ColaboradorID == -1 || model.ColaboradorID == "" || model.ColaboradorID == null) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Candidato es obligatorio`, false);
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
                $.ajax({
                    type: 'GET',
                    url: `../Postulacion/TotalPostulacionPorPostulante?colaboradorID=${model.ColaboradorID}`,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            let obj = response.data;
                            if (response.data.length > 0) {
                                //Swal.fire("¡Aviso del Sistema!", "El postulante ya tiene una postulación en proceso.", "info")
                                $("#ExistePostulacion").modal("show");
                            }
                            else {
                                $.ajax({
                                    type: 'POST',
                                    url: `../Postulacion/InsertPostulante`,
                                    data: JSON.stringify(model),
                                    contentType: "application/json; charset=utf-8",
                                    async: true,
                                    cache: false,
                                    beforeSend: function () {
                                    },
                                    success: function (response) {
                                        if (response.resultado) {
                                            $("#AddCandidato").modal("hide");
                                            $("#InsertPostulante").modal("show");
                                            GetAll();
                                            //Swal.fire("¡Aviso del Sistema!", "Se registro correctamente.", "success")
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
                        } else {
                            console.log(response);
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
        url: `../Convocatoria/Get?convocatoriaID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) {
                    $("#txtId").val(obj.convocatoriaId);
                    $("#txtFechaSolicitud").val(obj.fechaSolicitud);
                    //$("#txtFechaSolicitud").flatpickr({
                    //    //mode: 'range',
                    //    dateFormat: "d-m-Y"
                    //});

                    $("#cboCargo").val(obj.cargoID);
                    $("#cboMotivo").val(obj.motivoConvocatoriaID);
                    $("#txtVacante").val(obj.vacante);
                    $("#cboPrioridad").val(obj.prioridadConvocatoriaID);
                    $("#txtFechaLimite").val(obj.fechaLimite);
                    $("#cboEmpresa").val(obj.empresaID);
                    $("#txtTitulo").val(obj.titulo);
                    $("#txtDetalleConvocatoria").val(obj.detalleConvocatoria);
                    $("#txtDireccion").val(obj.direccion);
                    $("#txtComentarios").val(obj.comentarios);
                    $("#SolicitanteID").val(obj.solicitanteID);
                    $("#CoordinadorID").val(obj.coordinadorID);
                    $("#ClienteID").val(obj.clienteID);
                    $("#cboTipoModalidad").val(obj.tipoModalidadID);
                    $("#cboDepartamento").val(obj.departamentoID);
                    $("#cboProvincia").val(obj.provinciaID);
                    $("#txtAnioExperiencia").val(obj.anioExperiencia);
                    $("#txtSueldoMinimo").val(obj.sueldoMinimo);
                    $("#txtSueldoMaximo").val(obj.sueldoMaximo);
                    
                    $("#AddCandidato").modal("show");
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
        "ConvocatoriaID": ConvocatoriaID,
        "FechaSolicitud": $("#txtFechaSolicitud").val(),
        "CargoID": $("#cboCargo").val(),
        "MotivoConvocatoriaID": $("#cboMotivo").val(),
        "Vacante": $("#txtVacante").val(),
        "PrioridadConvocatoriaID": $("#cboPrioridad").val(),
        "FechaLimite": $("#txtFechaLimite").val(),
        "EmpresaID": $("#cboEmpresa").val(),
        "Titulo": $("#txtTitulo").val(),
        "DetalleConvocatoria": $("#txtDetalleConvocatoria").val(),
        "Direccion": $("#txtDireccion").val(),
        "Comentarios": $("#txtComentarios").val(),
        "SolicitanteID": G_IdUsuario_Logeado,
        "CoordinadorID": G_IdUsuario_Logeado,
        "ClienteID": $("#ClienteID").val(),
        "ModificadoPor": G_IdUsuario_Logeado,
        "ResponsableID": G_IdUsuario_Logeado,
        "Cerrado": true,
        "Estado": true,
        "TipoModalidadID": $("#cboTipoModalidad").val(),
        "SueldoMinimo": $("#txtSueldoMinimo").val(),
        "SueldoMaximo": $("#txtSueldoMaximo").val(),
        "DepartamentoID": $("#cboDepartamento").val(),
        "ProvinciaID": $("#cboProvincia").val(),
        "AnioExperiencia": $("#txtAnioExperiencia").val()
    };

    if (model.CargoID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Cargo es obligatorio`, false);
    }
    if (model.MotivoConvocatoriaID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Motivo es obligatorio`, false);
    }
    if (model.Vacante.length < 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Vacante es obligatorio`, false);
    }
    if (model.PrioridadConvocatoriaID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Prioridad es obligatorio`, false);
    }
    if (model.EmpresaID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Empresa es obligatorio`, false);
    }
    if (model.ClienteID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Cliente es obligatorio`, false);
    }
    if (model.FechaSolicitud < 1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Solicitud es obligatorio`, false);
    }
    if (model.FechaLimite.length < 1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Límite es obligatorio`, false);
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
                    url: `../Convocatoria/Update`,
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
    convocatoriaId = 0;
    cargoId = 0;
    distritoId = 0;
    $('#cboColaborador').val(null).trigger('change');
}