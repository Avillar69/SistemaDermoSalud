//Global
let ConvocatoriaID = $("#txtId").val();
let TablaPostulantes = $("#TablaPostulantes");
//let TablaPostulantes;
let ColaboradorID = 0;
let EvaluacionID = 0;
let G_FechaActual = $("#G_FechaActual").val();
let toteneval = 0;
let totapto = 0;
let totaptoobs = 0;
let totnoapto = 0;
let EvaluacionDetalleID = 0;
let rutaAdjunto = "";

$(document).ready(function () {
    $("#lightgallery").lightGallery();

    $(".tabpostulantes").click(function () {
        GetAllByConvocatoria(ConvocatoriaID);
    });

    //Combo
    ComboCargo("#cboCargo", "Seleccionar...");
    ComboMotivo("#cboMotivo", "Seleccionar...");
    ComboPrioridad("#cboPrioridad", "Seleccionar...");
    ComboEmpresa("#cboEmpresa", "Seleccionar...");
    ComboTipoModalidad("#cboTipoModalidad", "Seleccionar...");
    ComboDepartamento("#cboDepartamento", "Seleccionar...");
    ComboProvincia("#cboProvincia", "Seleccionar...");
    ComboDistrito("#cboDistrito", "Seleccionar...");
    ComboEstadoConvocatoria("#cboEstadoConvocatoria", "Seleccionar...")
    ComboCliente("#cboCliente", "Seleccionar...");
    ComboEtapaConvocatoria("#cboEtapaConvocatoria", "Seleccionar...");

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

    //IniciarCKEditor
    IniciarCKEditor();

    //Listado
    setTimeout(() => {
        Get(ConvocatoriaID);
        GetAllByConvocatoria(ConvocatoriaID);
    }, 1000)

    //Actualizar
    $("#BtnActualizar").click(function () {
        Update();
    });
    //Fin Actualizar

    //$("#TablaPostulantes").on("click", ".BtnEvaluar", function () {
    //    ColaboradorID = $(this).attr("data-colaboradorid");
    //    EvaluacionID = $(this).attr("data-evaluid");
    //    let postulante = $(this).attr("data-postulante");
    //    //$("#txtFechaEvaluacion").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
    //    $("#txtFechaEvaluacion").val(currentDate());
    //    ComboTipoEvaluacion("#cboTipoEvaluacion", "Seleccionar...");
    //    ComboStatus("#cboStatus", "Seleccionar...");
    //    $(".camposEdicion").fadeOut(0);
    //    $("#BtnGuardarEvaluación").fadeIn(0);
    //    $("#BtnActualizarEvaluación").fadeOut(0);
    //    $("#TituloModal").text(`Evaluación de candidato: ${postulante}`);
    //    $("#SubTituloModal").text(`Evaluador: Alexandra Lopez`);   
    //    $("#cboTipoEvaluacion").prop("disabled", false);
    //    $("#ModalEvaluacion").modal("show");
    //})

    //$("#TablaPostulantes").on("click", ".eneva, .apto, .noapto, .aptoobs", function () {
    //    let evadetid = $(this).attr("data-evadet-id");
    //    let postulante = $(this).attr("data-postulante");
    //    ColaboradorID = $(this).attr("data-colaborador-id");
    //    $(".camposEdicion").fadeIn(0);
    //    $("#BtnGuardarEvaluación").fadeOut(0);
    //    $("#BtnActualizarEvaluación").fadeIn(0);
    //    $("#TituloModal").text(`Editar evaluación de candidato: ${postulante}`);
    //    $("#cboTipoEvaluacion").prop("disabled", true);
    //    $("#SubTituloModal").text(`Evaluador: Alexandra Lopez`);
        
    //    GetEvaluacion(evadetid);
    //})

    //Evaluación
    $("#BtnGuardarEvaluación").click(function () {
        GuardarEvaluacion();
    });

    $("#BtnEnlaceAdjunto_url_img").click(function () {
        let Url_Adjunto = $("#BtnEnlaceAdjunto_url_img").attr("data-url-adjunto-img");

        if (Url_Adjunto.trim().length != 0) {
            let extensionAdjunto = ObtenerExtension(Url_Adjunto);

            if (extensionAdjunto == "jpg" || extensionAdjunto == "png" || extensionAdjunto == "gif" || extensionAdjunto == "jpeg") {
                //Imagen
                $("#btnImagenDefectoAdjunto").attr('href', Url_Adjunto);
                $("#btnImagenDefectoImg").attr('src', Url_Adjunto);
                $("#btnImagenDefectoAdjunto").click();
            } else {
                $("#BtnEnlaceAdjunto").attr('href', Url_Adjunto);
                document.getElementById("BtnEnlaceAdjunto").click();
            }
        }
    })

    $("#BtnActualizarEvaluación").click(function () {
        ActualizarEvaluacion();
    });
    //Fin Evaluación

    ComboTipoProceso("#cboTipoProceso", "Seleccionar...");
})

//Listado
function GetAllByConvocatoria(id) {
    $.ajax({
        type: 'GET',
        url: `../Convocatoria/GetAllByConvocatoria?convocatoriaID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            toteneval = 0; totapto = 0; totaptoobs = 0; totnoapto = 0;
            $("#TablaPostulantes tbody").empty();
            //if (TablaPostulantes instanceof $.fn.dataTable.Api) {
            //    $('#TablaPostulantes').DataTable().clear().destroy();
            //}
        },
        success: function (response) {
            if (response.resultado) {
                PintaTabla(response.data);
                $("#txtEnEval").val(toteneval);
                $("#txtApto").val(totapto);
                $("#txtNoApto").val(totnoapto);
                $("#txtAptoObs").val(totaptoobs);
            } else {
                console.log(response);
            }
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
            //IniciarDataTable();
        }
    });

}
function PintaTabla(data) {
    EstructuraHead(data.head);

    if (data.body.data.length > 0) {
        EstructuraBody(data);
    }
    else {
        $("#TablaPostulantes tbody").empty();
    }
}
function EstructuraHead(data) {
    $("#TablaPostulantes thead").empty();

    let trFirst = `<tr class="info">`;

    let th = `<th>Candidato</th>`;
    trFirst += th;

    //HEADER CABECERA
    if (data.headFirstStatic.length != 0) {
        data.headFirstStatic.forEach((value) => {
            let thTipoEvaluacion = `<th data-id="${value.tipoEvaluacionID}">${value.descripcionCorta}</th>`;
            trFirst += thTipoEvaluacion;
        })
    }

    trFirst += "<th>Status</th>";
    /*trFirst += "<th></th>";*/
    trFirst += "</tr>";

    $("#TablaPostulantes thead").append(trFirst);
}
function EstructuraBody(data) {
    $("#TablaPostulantes tbody").empty();

    data.body.data.forEach((value) => {
        let tr = `<tr>
		    <td>${value.postulante}</td>`;

        data.head.headFirstStatic.forEach((tipo) => {
            let consu = value.listaDetalleEvaluacion.find(x => x.colaboradorId == value.colaboradorId && x.tipoEvaluacionID == tipo.tipoEvaluacionID)
            if (consu) {
                if (consu.evaluacionEstadoDetalleCodigo == "ENEVA") {
                    tr += `<td class="eneva" data-colaborador-id="${consu.colaboradorId}" data-evadet-id="${consu.evaluacionDetalleID}" data-eva-id="${consu.evaluacionID}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-primary">${consu.evaluacionEstadoDetalle}</span></td>`;
                }
                else if (consu.evaluacionEstadoDetalleCodigo == "APTO") {
                    tr += `<td class="apto" data-colaborador-id="${consu.colaboradorId}" data-evadet-id="${consu.evaluacionDetalleID}" data-eva-id="${consu.evaluacionID}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-success">${consu.evaluacionEstadoDetalle}</span></td>`;
                }
                else if (consu.evaluacionEstadoDetalleCodigo == "APOBS") {
                    tr += `<td class="aptoobs" data-colaborador-id="${consu.colaboradorId}" data-evadet-id="${consu.evaluacionDetalleID}" data-eva-id="${consu.evaluacionID}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-warning">${consu.evaluacionEstadoDetalle}</span></td>`;
                }
                else if (consu.evaluacionEstadoDetalleCodigo == "NOAPTO") {
                    tr += `<td class="noapto" data-colaborador-id="${consu.colaboradorId}" data-evadet-id="${consu.evaluacionDetalleID}" data-eva-id="${consu.evaluacionID}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-danger">${consu.evaluacionEstadoDetalle}</span></td>`;
                }
            }
            else {
                tr += `<td> </td>`;
            }
        })

        if (value.evaluacionEstadoCodigo == "ENEVA") {
            tr += `<td><span class="badge rounded-pill bg-primary">${value.evaluacionEstado}</span></td>`;
            toteneval++;
        }
        else if (value.evaluacionEstadoCodigo == "APTO") {
            tr += `<td><span class=" badge rounded-pill bg-success">${value.evaluacionEstado}</span></td>`;
            totapto++;
        }
        else if (value.evaluacionEstadoCodigo == "APOBS") {
            tr += `<td><span class="badge rounded-pill bg-warning">${value.evaluacionEstado}</span></td>`;
            totaptoobs++;
        }
        else if (value.evaluacionEstadoCodigo == "NOAPTO") {
            tr += `<td><span class="badge rounded-pill bg-danger">${value.evaluacionEstado}</span></td>`;
            totnoapto++;
        }
        else {
            tr += `<td></td>`;
        }

        //tr += `<td>
        //            <div class="dropdown">
        //                <a href="javascript:void(0);" class="btn btn-light btn-icon" id="dropdownMenuLink3" data-bs-toggle="dropdown" aria-expanded="false">
        //                    <i class="ri-equalizer-fill"></i>
        //                </a>
        //                <ul class="dropdown-menu dropdown-menu-end" style="">
        //                    <li>
        //                        <a href="javascript:void(0);" class="dropdown-item view-item-btn BtnEvaluar" data-colaboradorid="${value.colaboradorId}" 
        //                        data-evaluid="${value.evaluacionID}" data-postulante="${value.postulante}">
        //                            <i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
        //                            Evaluar
        //                        </a>
        //                    </li>
        //                    <li>
        //                        <a class="dropdown-item view-item-btn" href="/Postulacion/Detalle?postulacionID=${value.postulacionId}">
        //                            <i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
        //                            Información General
        //                        </a>
        //                    </li>
        //                </ul>
        //            </div> 
        //       </td>`

        tr += `</tr>`;
        $("#TablaPostulantes tbody").append(tr);
    })
}
function PintaTabla_(data) {
    TablaPostulantes.DataTable({
        responsive: true,
        pagingType: "full_numbers",
        data: data,
        destroy: true,
        "order": [[0, "desc"]],
        columns: [
            { data: 'postulacionId' },
            { data: 'postulante' },
            { data: 'fechaPostulacionView' },
            /*{ data: 'fuenteReclutamiento' },*/
            { data: 'reclutador' },
            { data: 'etapaPostulacion' },
            { data: 'estadoProceso' },
            { data: 'aprobador' },
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
            //{
            //    targets: 3,
            //    render: function (data, type, full, meta) {
            //        return `<center><a>${data}</a></center>`;
            //    },
            //},
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
                                                    <a class="dropdown-item view-item-btn" href="/Convocatoria/Detalle?convocatoriaId=${full.postulacionId}">
                                                        <i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
                                                        Evaluar
                                                    </a>
                                                </li>
                                                <li>
                                                    <a class="dropdown-item view-item-btn" href="/Postulacion/Detalle?postulacionID=${full.postulacionId}">
                                                        <i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
                                                        Información General
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </li>
                                </ul>
                            </center>`;
                }
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
                let cabecera = "";
                if (obj != null) {
                    $("#txtId").val(obj.convocatoriaId);
                    cabecera = `${obj.convocatoriaID} | ${obj.titulo} | ${obj.fechaSolicitudView}`;
                    $(".txtCabecera").text(cabecera);
                    //$("#txtFechaSolicitud").datepicker('setDate', obj.fechaSolicitudView);
                    $("#txtFechaSolicitud").val(obj.fechaSolicitudView.substring(0, 10).replaceAll("/", "-"));
                    $("#cboCargo").val(obj.cargoID);
                    //$("#cboMotivo").val(obj.motivoConvocatoriaID);
                    $("#txtVacante").val(obj.vacante);
                    $("#cboPrioridad").val(obj.prioridadConvocatoriaID);
                    //$("#txtFechaLimite").datepicker('setDate', obj.fechaLimiteView);
                    $("#txtFechaLimite").val(obj.fechaLimiteView.substring(0, 10).replaceAll("/", "-"));
                    $("#cboEmpresa").val(obj.empresaID);
                    $("#txtTitulo").val(obj.titulo);
                    CKEDITOR.instances.editor1.setData(obj.detalleConvocatoria);
                    $("#txtDireccion").val(obj.direccion);
                    //$("#txtComentarios").val(obj.comentarios);
                    $("#SolicitanteID").val(obj.solicitanteID);
                    $("#CoordinadorID").val(obj.coordinadorID);
                    $("#cboCliente").val(obj.clienteID);
                    $("#cboTipoModalidad").val(obj.tipoModalidadID);
                    $("#cboDepartamento").val(obj.departamentoID);
                    ComboProvincia("#cboProvincia", "Seleccionar", obj.departamentoID, obj.provinciaID);
                    ComboDistrito("#cboDistrito", "Seleccionar", obj.provinciaID, obj.distritoID);
                    $("#txtAnioExperiencia").val(obj.anioExperiencia);
                    $("#txtSueldoMinimo").val(obj.sueldoMinimo);
                    $("#txtSueldoMaximo").val(obj.sueldoMaximo);
                    $("#txtLinkVideo").val(obj.linkVideo);
                    $("#txtLinkUbicacion").val(obj.linkUbicacion);
                    //$("#chkEstado").prop("checked", obj.estado);
                    $("#cboEstadoConvocatoria").val(obj.estadoConvocatoriaId);
                    $("#cboEtapaConvocatoria").val(obj.etapaConvocatoriaID);
                    $("#cboTipoProceso").val(obj.tipoProcesoSeleccionID);
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
        //"Fecha_Solicitud": $("#txtFechaSolicitud").val(),
        "Fecha_Solicitud": new Date(formatFecha($("#txtFechaSolicitud").val())),
        "CargoID": $("#cboCargo").val(),
        "MotivoConvocatoriaID": 1,
        "Vacante": $("#txtVacante").val(),
        "PrioridadConvocatoriaID": $("#cboPrioridad").val(),
        //"Fecha_Limite": $("#txtFechaLimite").val(),
        "Fecha_Limite": new Date(formatFecha($("#txtFechaLimite").val())),
        "EmpresaID": $("#cboEmpresa").val(),
        "Titulo": $("#txtTitulo").val(),
        "DetalleConvocatoria": CKEDITOR.instances['editor1'].getData(),
        "Direccion": $("#txtDireccion").val(),
        "Comentarios": "",
        "SolicitanteID": $("#SolicitanteID").val(),
        "CoordinadorID": $("#CoordinadorID").val(),
        "ClienteID": $("#cboCliente").val(),
        "ModificadoPor": $("#ModificadoPor").val(),
        "ResponsableID": $("#ResponsableID").val(),
        //"Estado": $("#chkEstado").is(":checked"),
        "TipoModalidadID": $("#cboTipoModalidad").val(),
        "SueldoMinimo": $("#txtSueldoMinimo").val(),
        "SueldoMaximo": $("#txtSueldoMaximo").val(),
        "DepartamentoID": $("#cboDepartamento").val(),
        "ProvinciaID": $("#cboProvincia").val(),
        "DistritoID": $("#cboDistrito").val(),
        "AnioExperiencia": $("#txtAnioExperiencia").val(),
        "LinkVideo": $("#txtLinkVideo").val(),
        "LinkUbicacion": $("#txtLinkUbicacion").val(),
        "EstadoConvocatoriaId": $("#cboEstadoConvocatoria").val()
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
                    url: `../Convocatoria/Update`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Get(ConvocatoriaID)
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó correctamente.", "success")
                            //GetAll();
                            //setTimeout(function () { location.href = `/Convocatoria`; }, 1800);
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
function GuardarEvaluacion() {
    let validacion = true;
    var model = new FormData();

    //if ($("#fileAdjunto").val() != "") {
    //    let File = $("#fileAdjunto").get(0).files;
    //    if ((File[0].size / 1024 / 1024) > 10) {
    //        //Toastr_NotificacionError("¡Aviso del Sistema!", `El Archivo adjuntado supera los ${G_Menu_Adjunto_Size}MB`);
    //        Toastr_NotificacionError("¡Aviso del Sistema!", `El Archivo adjuntado supera los ${10}MB`);
    //        return false;
    //    }
    //    model.append("Url_Adjunto_File", File[0]);
    //}

    model.append("EvaluacionID", EvaluacionID);
    model.append("ColaboradorID", ColaboradorID);
    model.append("Fecha", $("#txtFechaEvaluacion").val());
    model.append("TipoEvaluacionID", $("#cboTipoEvaluacion").val());
    model.append("EvaluacionEstadoID", $("#cboStatus").val());

    //if ($("#fileAdjunto").val() == "") {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Adjunto es obligatorio`, false);
    //}
    if (ValidarIntNull(model.get('TipoEvaluacionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Evaluación es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('EvaluacionEstadoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Status es obligatorio`, false);
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
            if (result.isConfirmed) {
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
                    url: `../EvaluacionDetalle/Create`,
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
                            Swal.fire("¡Aviso del Sistema!", "Se registro la evaluación correctamente.", "success");
                            $("#ModalEvaluacion").modal("hide");
                            GetAllByConvocatoria(ConvocatoriaID);
                            Limpiar();
                        }
                        else {
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
function GetEvaluacion(id) {
    $.ajax({
        type: 'GET',
        url: `../EvaluacionDetalle/Get?evaluacionDetalleID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                console.log(obj)
                if (obj != null) {
                    $("#txtId").val(obj.evaluacionDetalleID);
                    EvaluacionDetalleID = obj.evaluacionDetalleID;
                    EvaluacionID = obj.evaluacionID;
                    //$("#txtFechaEvaluacion").datepicker('setDate', obj.fechaView);
                    $("#txtFechaEvaluacion").val(obj.fechaView.substring(0, 10).replaceAll("/", "-"));
                    ComboTipoEvaluacion("#cboTipoEvaluacion", "Seleccionar...", obj.tipoEvaluacionID);
                    //G_GenerarEnlaceAdjunto(".divBtnAdjuntoView_Adjunto", obj.urladjunto, obj.fileName_Adjunto)
                    ComboStatus("#cboStatus", "Seleccionar...", obj.evaluacionEstadoId);

                    //Adjunto
                    $("#BtnEnlaceAdjunto_url_img").attr("data-url-adjunto-img", obj.urlAdjunto);
                    rutaAdjunto = obj.urlAdjunto;

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
                        } else {
                            $(".divBtnAdjuntoView_url_img").fadeOut(0);
                        }
                    }

                    $("#ModalEvaluacion").modal("show");
                } else {
                    swal("No se encontro la evaluación", "", "warning");
                }
            } else {
                console.log(response);
            }
        },
        error: function () { },
        complete: function () { }
    });
}
function ActualizarEvaluacion() {
    let validacion = true;
    var model = new FormData();

    //if ($("#fileAdjunto").val() != "") {
    //    let File = $("#fileAdjunto").get(0).files;
    //    if ((File[0].size / 1024 / 1024) > 10) {
    //        Toastr_NotificacionError("¡Aviso del Sistema!", `El Archivo adjuntado supera los ${10}MB`);
    //        return false;
    //    }
    //    model.append("Url_Adjunto_File", File[0]);
    //}

    model.append("EvaluacionDetalleID", EvaluacionDetalleID);
    model.append("EvaluacionID", EvaluacionID);
    model.append("ColaboradorID", ColaboradorID);
    model.append("Fecha", $("#txtFechaEvaluacion").val());
    model.append("TipoEvaluacionID", $("#cboTipoEvaluacion").val());
    model.append("EvaluacionEstadoID", $("#cboStatus").val());
    model.append("UrlAdjunto", rutaAdjunto);

    //if ($("#fileAdjunto").val() == "") {
    //    validacion = false;
    //    Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Adjunto es obligatorio`, false);
    //}
    if (ValidarIntNull(model.get('TipoEvaluacionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Evaluación es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('EvaluacionEstadoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Status es obligatorio`, false);
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
            if (result.isConfirmed) {
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
                    url: `../EvaluacionDetalle/Update`,
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
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó la evaluación correctamente.", "success");
                            $("#ModalEvaluacion").modal("hide");
                            GetAllByConvocatoria(ConvocatoriaID);
                            Limpiar();
                        }
                        else {
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
    $("#txtFechaEvaluacion").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
    $("#cboTipoEvaluacion").val(-1);
    document.getElementById("fileAdjunto").value = "";
    $("#cboStatus").val(-1);
}
function IniciarDataTable() {
    TablaPostulantes = $("#TablaPostulantes").DataTable({
        responsive: true,
        pagingType: "full_numbers",
        destroy: true,
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
            extend: "excel", footer: true,
            className: "btn yellow btn-outline "
        }, {
            extend: "csv",
            className: "btn purple btn-outline "
        }]
    });
}
function ObtenerExtension(filename) {
    return filename.split('.').pop();
}