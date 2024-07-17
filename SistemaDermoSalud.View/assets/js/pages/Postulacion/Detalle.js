//Global
let PostulacionID = $("#txtIdPostulacion").val();

$(document).ready(function () {
    //Combo
    ComboTipoColaborador("#cboTipoColaborador", "Seleccionar...");
    ComboFuenteReclutamiento("#cboFuenteReclutamiento", "Seleccionar...");
    ComboCentroEstudio("#cboCentroEstudio", "Seleccionar...");
    ComboProfesion("#cboProfesion", "Seleccionar...");
    ComboTipoEstudio("#cboNivelEducativo", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");

    setTimeout(() => {
        GetInfoPostulacion(PostulacionID);
    }, 2000)

    $("#lista").click(function () {
        location.href = `/Convocatoria/Detalle?convocatoriaId=${$("#txtConvocatoriaPersonalId").val()}`;
    })
    
})

function GetInfoPostulacion(id) {
    $.ajax({
        type: 'GET',
        url: `../Postulacion/GetInfoPostulacion?postulacionID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                let cabecera = "";
                if (obj != null) {
                    $("#txtId").val(obj.colaboradorID);
                    cabecera = `${obj.numeroDocumentoIdentidad} | ${obj.nombreCompletoView}`;
                    $(".txtCabecera").text(cabecera);
                    $("#txtCodigoDocumento").val(obj.codigoTipoDocumentoIdentidad);
                    $("#txtNumDocumento").val(obj.numeroDocumentoIdentidad);
                    $("#txtFechaPostulacion").val(obj.fechaPostulacionView);
                    $("#txtCargo").val(obj.cargo);
                    $("#txtDistritoConvocatoria").val(obj.distritoConvocatoria);
                    $("#txtCliente").val(obj.empresa);
                    $("#txtNombres").val(obj.nombres);
                    $("#txtApellidoPaterno").val(obj.apellidoParterno);
                    $("#txtApellidoMaterno").val(obj.apellidoMarterno);
                    $("#txtFechaNacimiento").val(obj.fechaNacimiento);
                    $("#txtTelefono").val(obj.telelefono1);
                    $("#txtEmailPersonal").val(obj.direccionMail1);
                    $("#cboTipoColaborador").val(obj.tipoColaboradorID);
                    $("#cboFuenteReclutamiento").val(obj.fuenteReclutamientoID);
                    $("#cboNivelEducativo").val(obj.tipoEstudioID);
                    $("#cboProfesion").val(obj.profesionID);
                    $("#cboCentroEstudio").val(obj.centroEstudioID);
                    $("#cboDistrito").val(obj.distritoResidenciaID);
                    $("#txtEmpresaActual").val(obj.empresaActual);
                    $("#txtPuestoActual").val(obj.puestoActual);
                    $("#txtFechaIngreso").val(obj.fechaInicialLaboralView);
                    $("#txtFechaFinal").val(obj.fechaFinalLaboralView);
                    $("#txtConvocatoriaPersonalId").val(obj.convocatoriaPersonalID);
                } else {
                    swal("No se encontró la postulación", "", "warning");
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
        "ClienteID": $("#cboCliente").val(),
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
                    url: `../Colaborador/Update`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó correctamente.", "success")
                            location.href = `/Convocatoria/Index`;
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