let G_FechaActual = $("#G_FechaActual").val();
$(document).ready(function () {
    ComboDepartamento("#cboDepartamento", "Seleccionar...");
    ComboProvincia("#cboProvincia", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");
    ComboProfesion("#cboProfesion", "Seleccionar...");
    ComboTipoEstudio("#cboNivelEducativo", "Seleccionar...");
    ComboListaNegra("#cboListaNegra", "Seleccionar...");

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

    $("#txtFechaInicio").val(G_FechaActual.substring(0, 10));
    $("#txtFechaFin").val(G_FechaActual.substring(0, 10));

    $("#BtnGuardar").click(function () {
        Create();
    });
})
function Create() {
    let validacion = true;

    var model = new FormData();

    if ($("#fileAdjunto").val() != "") {
        let File = $("#fileAdjunto").get(0).files;
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("CodigoTipoDocumentoIdentidad", "DNI");
    model.append("NumeroDocumentoIdentidad", $("#txtNumDocumento").val());
    model.append("Nombres", $("#txtNombres").val());
    model.append("ApellidoParterno", $("#txtApellidoPaterno").val());
    model.append("ApellidoMarterno", $("#txtApellidoMaterno").val());
    model.append("Direccion", $("#txtDireccion").val());
    model.append("DistritoResidenciaID", $("#cboDistrito").val());
    model.append("Telelefono1", $("#txtTelefono").val());
    model.append("DireccionMail1", $("#txtCorreo").val());
    model.append("TipoEstudioID", $("#cboNivelEducativo").val());
    model.append("ProfesionID", $("#cboProfesion").val());
    model.append("Sexo", $("#cboSexo").val());
    model.append("AnioExperiencia", $("#txtAnioExperiencia").val());
    model.append("EmpresaActual", $("#txtEmpresaActual").val());
    //model.append("FechaInicial_Laboral", $("#txtFechaInicio").val());
    model.append("FechaInicialLaboral", $("#txtFechaInicio").val());
    //model.append("FechaFinal_Laboral", $("#txtFechaFin").val());
    model.append("FechaFinalLaboral", $("#txtFechaFin").val());
    model.append("CreadoPor", G_IdUsuario_Logeado);
    model.append("Password", $("#Nuevo_Password").val());
    model.append("ConfirmPassword", $("#Nuevo_ConfirmarPassword").val());
    model.append("ListaNegraID", $("#cboListaNegra").val());
    model.append("Estado", 1);
    model.append("TipoCandidato", 1);

    if (model.get('NumeroDocumentoIdentidad').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Numero Documento Identidad es obligatorio`, false);
    }
    if (model.get('Nombres').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Nombres es obligatorio`, false);
    }
    if (model.get('ApellidoParterno').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Apellido Paterno es obligatorio`, false);
    }
    if (model.get('ApellidoMarterno').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Apellido Materno es obligatorio`, false);
    }
    if (model.get('Direccion').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Dirección es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('DistritoResidenciaID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Distrito es obligatorio`, false);
    }   
    if (model.get('Telelefono1').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Teléfono es obligatorio`, false);
    }
    if (model.get('DireccionMail1').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Email es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('TipoEstudioID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo de Estudio es obligatorio`, false);
    }  
    if (ValidarIntNull(model.get('ProfesionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Profesión es obligatorio`, false);
    }  
    if (model.get('Sexo').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Sexo es obligatorio`, false);
    }      
    if (model.get('AnioExperiencia').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Año de Experiencia es obligatorio`, false);
    }
    if (model.get('EmpresaActual').length == 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Empresa Actual es obligatorio`, false);
    }
    //if(model.get('FechaInicial_Laboral') == null || model.get('FechaInicial_Laboral') == "") {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Inicial Laboral es obligatorio`, false);
    //}
    //if (model.get('FechaFinal_Laboral') == null || model.get('FechaFinal_Laboral') == "") {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Final Laboral es obligatorio`, false);
    //}
    if(model.get('FechaInicialLaboral') == null || model.get('FechaInicialLaboral') == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Inicial Laboral es obligatorio`, false);
    }
    if (model.get('FechaFinalLaboral') == null || model.get('FechaFinalLaboral') == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Final Laboral es obligatorio`, false);
    }
    if (model.get("Password").trim().length != 0) {
        if (model.get("Password").trim() != model.get("ConfirmPassword").trim()) {
            validacion = false;
            Toastr_NotificacionError("¡Aviso del Sistema!", `Las Contraseñas no coinciden`, false);
        }
    }
    if (ValidarIntNull(model.get('ListaNegraID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Lista Negra es obligatorio`, false);
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
                    url: `../Candidato/ExisteCandidato?Email=${model.get('DireccionMail1')}&Dni=${model.get('NumeroDocumentoIdentidad') }`,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            debugger
                            let obj = response.data;
                            if (response.data.length > 0) {
                                Swal.fire("¡Aviso del Sistema!", `Ya existe un candidato con el Email ${model.get('DireccionMail1')} o el DNI ${model.get('NumeroDocumentoIdentidad') }`, "info")
                            }
                            else {
                                $.ajax({
                                    type: 'POST',
                                    url: `../Candidato/Create`,
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
                                            Swal.fire("¡Aviso del Sistema!", "Se registro correctamente.", "success")
                                            setTimeout(function () { location.href = `/Candidato`; }, 1800);
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



                //Swal.fire({
                //    html: 'Espere por favor...',
                //    allowOutsideClick: false,
                //    allowEscapeKey: false,
                //    allowEnterKey: false,
                //    showConfirmButton: false,
                //    onBeforeOpen: () => {
                //        Swal.showLoading()
                //    }
                //});

                //$.ajax({
                //    type: 'POST',
                //    url: `../Candidato/Create`,
                //    contentType: "application/json",
                //    data: model,
                //    enctype: 'multipart/form-data',
                //    contentType: false,
                //    processData: false,
                //    async: true,
                //    cache: false,
                //    timeout: 600000,
                //    beforeSend: function () {
                //    },
                //    success: function (response) {
                //        if (response.resultado) {
                //            Swal.fire("¡Aviso del Sistema!", "Se registro correctamente.", "success")
                //            setTimeout(function () { location.href = `/Candidato`; }, 1800);
                //        } else {
                //            if (response.codigoError == "ERROR_API") {
                //                Swal.fire("¡Aviso del Sistema!", "No se registro correctamente.", "error")
                //            }
                //            else if (response.codigoError == "ERROR_CONECT_API") {
                //                Swal.fire("¡Aviso del Sistema!", "Error al conectar con la API!", "error")
                //            }
                //            else {
                //                console.log(response);
                //                Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                //            }
                //        }
                //    },
                //    error: function (xhr, status, error) {
                //        Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                //    },
                //    complete: function () {
                //    }
                //});
            }
        });
    }
}