let CandidatoID = $("#G_CandidatoID").val();
let rutaAdjunto = "";
let descripcionAdjunto = "";
let descripcionAdjuntoUrl = "";
var BaseUrl = $("#G_BaseUrl").val();

$(document).ready(function (e) {
    ComboDepartamento("#cboDepartamento", "Seleccionar...");
    ComboProvincia("#cboProvincia", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");
    ComboProfesion("#cboProfesion", "Seleccionar...");
    ComboTipoEstudio("#cboNivelEducativo", "Seleccionar...");
    ComboListaNegra("#cboListaNegra", "Seleccionar...");
    setTimeout(() => { Get(CandidatoID); }, 1500)

    $("#BtnGuardar").click(function () {
        Update();
    });

    $("#BtnUrlAdjunto").click(function () {
        //let Url_Adjunto = $("#BtnUrlAdjunto").attr("data-url-adjunto");
        //let TipoArchivo = ObtenerExtension(Url_Adjunto);

        //if (Url_Adjunto.trim().length != 0) {
        //    if (G_RetornaClaseFileByExtension(`.${TipoArchivo}`).IsImage == true) {
        //        //Imagen
        //        $("#btnImagenDefectoAdjunto").attr('href', `${Url_Adjunto}?${new Date().getTime()}`);
        //        $("#btnImagenDefectoImg").attr('src', `${Url_Adjunto}?${new Date().getTime()}`);
        //        document.getElementById("btnImagenDefectoAdjunto").click()
        //    } else {
        //        $("#BtnEnlaceAdjunto_Storage").attr('href', Url_Adjunto);
        //        document.getElementById("BtnEnlaceAdjunto_Storage").click()
        //    }
        //}
        axios.get(BaseUrl + 'UploadPortal/DownloadCV?name=' + descripcionAdjuntoUrl, { responseType: 'blob' })
            .then(res => {
                debugger
                if (res.status == 200) {
                    const blob = new Blob([res.data], { type: 'application/pdf' });
                    const url = window.URL.createObjectURL(blob);
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', descripcionAdjuntoUrl);
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                    window.URL.revokeObjectURL(url);
                    Swal.fire("¡Aviso del Sistema!", "Se realizó la descarga", "success")
                } else {
                    Swal.fire('¡Aviso del Sistema!', 'No se pudo obtener la URL de descarga', 'info');
                }
            })
            .catch(err => {
                console.error(err);
                Swal.fire('¡Aviso del Sistema!', 'Hubo un error al procesar su solicitud', 'error');

            });
    })

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
})

function Get(CandidatoID) {
    $.ajax({
        type: 'GET',
        url: `../Candidato/Get?candidatoID=${CandidatoID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) {
                    $("#txtId").val(obj.candidatoID);
                    $("#txtNumDocumento").val(obj.numeroDocumentoIdentidad);
                    $("#txtNombres").val(obj.nombres);
                    $("#txtApellidoPaterno").val(obj.apellidoParterno);
                    $("#txtApellidoMaterno").val(obj.apellidoMarterno);
                    $("#txtDireccion").val(obj.direccion);
                    $("#cboDepartamento").val(obj.departamentoID == null ? -1 : obj.departamentoID);
                    ComboProvincia("#cboProvincia", "Seleccionar...", obj.departamentoID, obj.provinciaID);
                    ComboDistrito("#cboDistrito", "Seleccionar...", obj.provinciaID, obj.distritoResidenciaID);

                    $("#txtTelefono").val(obj.telelefono1);
                    $("#txtCorreo").val(obj.direccionMail1);
                    $("#cboNivelEducativo").val(obj.tipoEstudioID);
                    $("#cboProfesion").val(obj.profesionID);
                    $("#cboSexo").val(obj.sexo);
                    $("#txtAnioExperiencia").val(obj.anioExperiencia);
                    $("#txtEmpresaActual").val(obj.empresaActual);
                    $("#txtFechaInicio").val(obj.fechaInicialLaboral == null ? null : obj.fechaInicialLaboral.substring(0, 10).replaceAll("/", "-"));
                    $("#txtFechaFin").val(obj.fechaFinalLaboral == null ? null : obj.fechaFinalLaboral.substring(0, 10).replaceAll("/", "-"));
                    $("#cboListaNegra").val(obj.listaNegraID);
                    $("#Nuevo_Password").val(obj.password);
                    $("#Nuevo_ConfirmarPassword").val(obj.password);


                    //ADJUNTO
                    $("#BtnUrlAdjunto").attr("data-url-adjunto", obj.urlAdjunto);

                    //$("#BtnUrlAdjunto").attr("data-url-adjunto", obj.url_Adjunto);

                    //if (obj.url_Adjunto.length != 0) {
                    //    $(".divBtnUrlAdjunto").fadeIn(0);
                    //    let extensionAdjunto = ObtenerExtension(obj.url_Adjunto);
                    //    $("#iconoUrlAdjunto").removeClass();
                    //    if (extensionAdjunto == "jpg" || extensionAdjunto == "png" || extensionAdjunto == "gif" || extensionAdjunto == "jpeg") {
                    //        $("#iconoUrlAdjunto").addClass("fa fa-file-image-o");
                    //    } else if (extensionAdjunto == "pdf") {
                    //        $("#iconoUrlAdjunto").addClass("fa fa-file-pdf-o");
                    //    } else {
                    //        $("#iconoUrlAdjunto").addClass("fa fa-download");
                    //    }
                    //} else {
                    //    $(".divBtnUrlAdjunto").fadeOut(0);
                    //}

                    $(".custom-file-label").text(obj.descripcionAdjunto);
                    rutaAdjunto = obj.urlAdjunto;
                    descripcionAdjunto = obj.descripcionAdjunto;
                    descripcionAdjuntoUrl = obj.descripcionAdjuntoUrl;

                    if (obj.urlAdjunto != null) {
                        $("#BtnUrlAdjunto").attr("data-url-adjunto", obj.urlAdjunto);
                        if (obj.urlAdjunto.trim().length != 0) {
                            $(".divBtnUrlAdjunto").fadeIn(0);

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
                            $(".divBtnUrlAdjunto").fadeOut(0);
                            $("#BtnEliminarAdjunto").fadeOut(0);
                        }
                    }
                    //FIN ADJUNTO
                } else {
                    Swal.fire("¡Aviso del Sistema!", "No se encontro el candidato", "warning")
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

    var model = new FormData();

    if ($("#fileAdjunto").val() != "") {
        let File = $("#fileAdjunto").get(0).files;
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("CandidatoID", CandidatoID);
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
    model.append("FechaInicial_Laboral", $("#txtFechaInicio").val());
    model.append("FechaFinal_Laboral", $("#txtFechaFin").val());
    model.append("ModificadoPor", G_IdUsuario_Logeado);
    model.append("Password", $("#Nuevo_Password").val());
    model.append("ConfirmPassword", $("#Nuevo_ConfirmarPassword").val());
    model.append("ListaNegraID", $("#cboListaNegra").val());
    model.append("UrlAdjunto", rutaAdjunto);
    model.append("DescripcionAdjunto", descripcionAdjunto);
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
    if (model.get('FechaInicial_Laboral') == null || model.get('FechaInicial_Laboral') == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Inicial Laboral es obligatorio`, false);
    }
    if (model.get('FechaFinal_Laboral') == null || model.get('FechaFinal_Laboral') == "") {
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
                    url: `../Candidato/Update`,
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
                            Swal.fire("¡Aviso del Sistema!", "Se actualizo correctamente.", "success")
                            setTimeout(function () { location.href = `/Candidato`; }, 1800);
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se actualizo correctamente.", "error")
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
function ObtenerExtension(filename) {
    return filename.split('.').pop();
}