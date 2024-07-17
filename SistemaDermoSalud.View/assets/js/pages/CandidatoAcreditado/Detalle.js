let CandidatoID = $("#G_CandidatoID").val();
let rutaAdjunto = "";
let descripcionAdjunto = "";
let descripcionAdjuntoUrl = "";
var BaseUrl = $("#G_BaseUrl").val();

$(document).ready(function () {
    ComboDepartamento("#cboDepartamento", "Seleccionar...");
    ComboProvincia("#cboProvincia", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");
    ComboProfesion("#cboProfesion", "Seleccionar...");
    ComboTipoEstudio("#cboNivelEducativo", "Seleccionar...");
    ComboListaNegra("#cboListaNegra", "Seleccionar...");
    setTimeout(() => { Get(CandidatoID); }, 1500)

    $("#BtnUrlAdjunto").click(function () {
        axios.get(BaseUrl + 'UploadPortal/DownloadCV?name=' + descripcionAdjuntoUrl, { responseType: 'blob' })
            .then(res => {
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
                    $("#txtFechaInicio").val(obj.fechaInicialLaboral == null ? null : obj.fechaInicialLaboral.substring(0, 10).replaceAll("/", "-"));
                    $("#txtFechaFin").val(obj.fechaFinalLaboral == null ? null : obj.fechaFinalLaboral.substring(0, 10).replaceAll("/", "-"));
                    $("#cboListaNegra").val(obj.listaNegraID);
                    $("#Nuevo_Password").val(obj.password);
                    $("#Nuevo_ConfirmarPassword").val(obj.password);

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
function ObtenerExtension(filename) {
    return filename.split('.').pop();
}