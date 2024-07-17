//Global
let G_FechaActual = $("#G_FechaActual").val();
let ConvocatoriaID = 0;
let tipoEvaluacion = "";

$(document).ready(function () {
    //Combo
    ComboCargo("#cboCargo", "Seleccionar...");
    ComboMotivo("#cboMotivo", "Seleccionar...");
    ComboPrioridad("#cboPrioridad", "Seleccionar...");
    ComboEmpresa("#cboEmpresa", "Seleccionar...");
    ComboTipoModalidad("#cboTipoModalidad", "Seleccionar...");
    ComboDepartamento("#cboDepartamento", "Seleccionar...");
    ComboProvincia("#cboProvincia", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");
    ComboClienteByEmpresa("#cboCliente", "Seleccionar...");
    ComboTipoEvaluacionSelect2("/TipoEvaluacion/GetAll", "tipoEvaluacionID", "descripcion", "cboTipoEvaluacion");

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

    let hoy = new Date();
    let dia = hoy.getDate();
    let mes = hoy.getMonth() + 1;
    let anio = hoy.getFullYear();
        
    dia = ('0' + dia).slice(-2);
    mes = ('0' + mes).slice(-2);

    // DD/MM/AAAA
    let format = `${dia}/${mes}/${anio}`;

    //$("#txtFechaSolicitud").val(G_FechaActual);
    //$("#txtFechaLimite").val(G_FechaActual);

    $("#txtFechaSolicitud").val(G_FechaActual.substring(0, 10));
    $("#txtFechaLimite").val(G_FechaActual.substring(0, 10));

    //RangoFechaInicia();
    //IniciarCKEditor
    IniciarCKEditor();
    $("#BtnGuardar").click(function () {
        Create();
    });
    //Fin Nuevo

    //Cancelar
    $("#BtnCancelar").click(function () {
        Limpiar();
    });
    //Fin Cancelar

    $("#cboTipoEvaluacion").change(function () {
        tipoEvaluacion = "";
        tipoEvaluacion += $(this).val() + ",";
    })

    $("#cboEmpresa").change(function () {
        $("#cboCliente").val(-1);
        ComboClienteByEmpresa("#cboCliente", 'Seleccionar...', -1, this.value);
    })

    ComboTipoProceso("#cboTipoProceso", "Seleccionar...");
})

function RangoFechaInicia() {
    var start = moment();
    var end = moment();

    $('#reportrange').daterangepicker({
        opens: App.isRTL() ? "left" : "right",
        format: "DD/MM/YYYY",
        startDate: start,
        endDate: end,
        linkedCalendars: false,
        ranges: {
            'Hoy': [moment(), moment()],
            'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Los últimos 7 días': [moment().subtract(6, 'days'), moment()],
            'Últimos 30 días': [moment().subtract(29, 'days'), moment()],
            'Este mes': [moment().startOf('month'), moment().endOf('month')],
            'El mes pasado': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        locale: {
            format: "DD/MM/YYYY",
            applyLabel: "Aplicar",
            cancelLabel: "Cancelar",
            fromLabel: "De",
            toLabel: "Hasta",
            customRangeLabel: "Rango Personalizado",
            daysOfWeek: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
            monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Novimbre", "Diciembre"],
            firstDay: 1
        }
    }, cb);

    cb(start, end);

}
//Listado
function GetAll() {

    $.ajax({
        type: 'GET',
        url: `../Convocatoria/GetAllFilter`,
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
        columns: [
            { data: 'convocatoriaId' },
            { data: 'solicitante' },
            { data: 'fechaSolicitudView' },
            { data: 'motivo' },
            { data: 'titulo' },
            { data: 'cliente' },
            { data: 'vacante' },
            { data: 'responsable' },
            { data: 'fechaLimiteView' },
            { data: 'avance' },
            { data: 'cerrado' },
            { data: '' }
        ],
        columnDefs: [
            {
                targets: 0,
                render: function (data, type, full, meta) {
                    return `<center><a href="javascript:void(0);" class="BtnEditar" data-id="${data}" title="Editar">${data}</a></center>`
                },
            },
            {
                targets: 1,
                render: function (data, type, full, meta) {
                    return `<center><a href="javascript:void(0);" class="BtnEditar" data-id="${full.convocatoriaId}" title="Editar">${data}</a></center>`
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
                    return `<center><a>${data}</a></center>`;
                },
            },
            {
                targets: -1,
                render: function (data, type, full, meta) {
                    return `<center>
                         <a href="/Convocatoria/Detalle?convocatoriaId=${full.convocatoriaId}" class="text-primary d-inline-block" title="Editar/Detalle">
                                                    <i class="ri-eye-fill fs-16"></i>
                                                </a>
                         <a href="@Url.Action("OrderDetails", "Ecommerce")" class="text-primary d-inline-block">
                                                    <i class="ri-eye-fill fs-16"></i>
                                                </a>
                         <a href="@Url.Action("OrderDetails", "Ecommerce")" class="text-primary d-inline-block">
                                                    <i class="ri-eye-fill fs-16"></i>
                                                </a>
                    </center>`;
                },
            }
        ],
        "ordering": true,
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
    let IdTipoEvaluacion = tipoEvaluacion.substring(0, tipoEvaluacion.length - 1);;

    let model = {
        //"Fecha_Solicitud": $("#txtFechaSolicitud").val(),
        "Fecha_Solicitud": $("#txtFechaSolicitud").val(),
        "CargoID": $("#cboCargo").val(),
        "MotivoConvocatoriaID": 1,
        "Vacante": $("#txtVacante").val(),
        "PrioridadConvocatoriaID": $("#cboPrioridad").val(),
        //"Fecha_Limite": $("#txtFechaLimite").val(),
        "Fecha_Limite": $("#txtFechaLimite").val(),
        "EmpresaID": $("#cboEmpresa").val(),
        "Titulo": $("#txtTitulo").val(),
        "DetalleConvocatoria": CKEDITOR.instances['editor1'].getData(),
        "Direccion": $("#txtDireccion").val(),
        "Comentarios": "",
        "SolicitanteID": G_IdUsuario_Logeado,
        "CoordinadorID": G_IdUsuario_Logeado,
        "ClienteID": $("#cboCliente").val(),
        "CreadoPor": G_IdUsuario_Logeado,
        "ResponsableID": G_IdUsuario_Logeado,
        "Cerrado": false,
        "Estado": true,
        "FechaCreacion": $("#FechaCreacion").val(),
        "TipoModalidadID": $("#cboTipoModalidad").val(),
        "SueldoMinimo": $("#txtSueldoMinimo").val(),
        "SueldoMaximo": $("#txtSueldoMaximo").val(),
        "DepartamentoID": $("#cboDepartamento").val(),
        "ProvinciaID": $("#cboProvincia").val(),
        "DistritoID": $("#cboDistrito").val(),
        "AnioExperiencia": $("#txtAnioExperiencia").val(),
        "LinkVideo": $("#txtLinkVideo").val(),
        "LinkUbicacion": $("#txtLinkUbicacion").val(),
        "TipoEvaluacionID": IdTipoEvaluacion,
        "TipoProcesoSeleccionID": $("#cboTipoProceso").val(),
    };

    if (model.CargoID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Cargo es obligatorio`, false);
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
    if (model.Fecha_Solicitud == null || model.Fecha_Solicitud == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Solicitud es obligatorio`, false);
    }
    if (model.Fecha_Limite == null || model.Fecha_Limite == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha Límite es obligatorio`, false);
    }
    if (model.Titulo.length < 1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Título es obligatorio`, false);
    }
    if (model.DetalleConvocatoria.length < 1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Detalle de Convocatoria es obligatorio`, false);
    }
    //if (model.Direccion.length < 1) {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Dirección es obligatorio`, false);
    //}
    if (model.TipoModalidadID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Modalidad es obligatorio`, false);
    }
    if (model.sueldoMinimo <= 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Sueldo Mínimo tiene que ser mayor a 0`, false);
    }
    if (model.SueldoMaximo <= 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Sueldo Máximo tiene que ser mayor a 0`, false);
    }
    if (model.DepartamentoID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Departamento es obligatorio`, false);
    }
    if (model.ProvinciaID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Provincia es obligatorio`, false);
    }
    if (model.DistritoID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Distrito es obligatorio`, false);
    }
    if (model.TipoEvaluacionID.length <= 0) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Evaluación es obligatorio`, false);
    }
    if (model.TipoProcesoSeleccionID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Proceso es obligatorio`, false);
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
                    url: `../Convocatoria/Create`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            //GetAll();
                            Swal.fire("¡Aviso del Sistema!", "Se registro correctamente.", "success")
                            //location.href = `/Convocatoria/Index`;
                            setTimeout(function () { location.href = `/Convocatoria`; }, 1800);
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
function Limpiar() {
    ConvocatoriaID = 0;
    $("#txtFechaSolicitud").val(G_FechaActual);
    $("#cboCargo").val(-1);
    $("#cboMotivo").val(-1);
    $("#txtVacante").val("");
    $("#cboPrioridad").val(-1);
    $("#txtFechaLimite").val(G_FechaActual);
    $("#cboEmpresa").val(-1);
    $("#txtTitulo").val("");
    //$("#txtDetalleConvocatoria").val("");
    $("#txtDireccion").val("");
    $("#txtComentarios").val("");
    $("#cboTipoModalidad").val(-1);
    $("#cboDepartamento").val(-1);
    $("#cboProvincia").val(-1);
    $("#txtAnioExperiencia").val("");
    $("#txtSueldoMinimo").val("");
    $("#txtSueldoMaximo").val("");

    CKEDITOR.instances.editor1.setData('');
    CKEDITOR.instances.editor1.destroy();
    CKEDITOR.replace('editor1', {
        height: '500px',
        width: '100%',
        contentsCss: ['/assets/libs/ckeditor4/mystyles.css'],
        customConfig: ['/config.js'],
        bodyClass: "document-editor"
    });      

    $("#ModalMantenimiento").modal("hide");
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
function addCssCK(ancho, items) {
    CKEDITOR.addCss('body{background:blue;}');
}