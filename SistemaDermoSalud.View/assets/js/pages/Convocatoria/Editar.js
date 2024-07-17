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
let ObjCandidato;
let tipoPlanillaID = null;
let tipoEvaluacion = "";

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
    ComboTipoEvaluacionSelect2("/TipoEvaluacion/GetAll", "tipoEvaluacionID", "descripcion", "cboTipoEvaluacionE");
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

    $("#TablaPostulantes").on("click", ".BtnEvaluar", function () {
        ColaboradorID = $(this).attr("data-colaboradorid");
        EvaluacionID = $(this).attr("data-evaluid");
        let postulante = $(this).attr("data-postulante");
        //$("#txtFechaEvaluacion").datepicker("setDate", moment(G_FechaActual, "DD/MM/YYYY").toDate());
        $("#txtFechaEvaluacion").val(currentDate());
        //ComboTipoEvaluacion("#cboTipoEvaluacion", "Seleccionar...", -1, ConvocatoriaID);
        ComboTipoEvaluacionByConvocatoriaByEvaluacion("#cboTipoEvaluacion", "Seleccionar...", -1, ConvocatoriaID, EvaluacionID);
        ComboStatus("#cboStatus", "Seleccionar...");
        $(".camposEdicion").fadeOut(0);
        $("#BtnGuardarEvaluacion").fadeIn(0);
        $("#BtnActualizarEvaluacion").fadeOut(0);
        $("#BtnEliminarAdjunto").fadeOut(0);
        $("#TituloModal").text(`Evaluación de candidato: ${postulante}`);
        $("#SubTituloModal").text(`Evaluador: ` + G_NombreUsuario_Logeado);
        $("#cboTipoEvaluacion").prop("disabled", false);
        $("#ModalEvaluacion").modal("show");
    })

    $("#TablaPostulantes").on("click", ".eneva, .apto, .noapto, .aptoobs", function () {
        let evadetid = $(this).attr("data-evadet-id");
        let postulante = $(this).attr("data-postulante");
        ColaboradorID = $(this).attr("data-colaborador-id");
        $(".camposEdicion").fadeIn(0);
        $("#BtnGuardarEvaluacion").fadeOut(0);
        $("#BtnActualizarEvaluacion").fadeIn(0);
        $("#BtnEliminarAdjunto").fadeIn(0);
        $("#TituloModal").text(`Editar evaluación de candidato: ${postulante}`);
        $("#cboTipoEvaluacion").prop("disabled", true);
        $("#SubTituloModal").text(`Evaluador: ` + G_NombreUsuario_Logeado);

        GetEvaluacion(evadetid);
    })

    $("#TablaPostulantes").on("click", ".StatuEvaluar", function () {
        EvaluacionID = $(this).attr("data-ideva");
        let postulante = $(this).attr("data-postulante");
        ColaboradorID = $(this).attr("data-idcolaborador");
        ComboStatus("#cboStatusEvaluacion", "Seleccionar...");
        //$(".camposEdicion").fadeIn(0);
        $("#BtnActualizarStatusEvaluacion").fadeIn(0);
        $("#TituloModalStatus").text(`Resultado Entrevista del Cliente: ${postulante}`);
        $("#SubTituloModalStatus").text(`Evaluador: ` + G_NombreUsuario_Logeado);
        setTimeout(() => { GetEvaluacionStatus(EvaluacionID); }, 1200)
        
        $("#ModalEvaluacionStatus").modal("show");
    })

    //Evaluación
    $("#BtnGuardarEvaluacion").click(function () {
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

    $("#BtnActualizarEvaluacion").click(function () {
        ActualizarEvaluacion();
    });

    $("#BtnActualizarStatusEvaluacion").click(function () {
        ActualizarStatusEvaluacion();
    });
    //Fin Evaluación

    $("#TablaPostulantes").on("click", ".convertircolaborador", function () {
        let postulante = $(this).attr("data-postu");
        ColaboradorID = $(this).attr("data-colaid");
        GetCandidato(ColaboradorID);
        ComboTipoPlanilla("#cboTipoPlanilla", "Seleccionar...");
        $("#BtnMigrar").fadeIn(0);
        $("#TituloModalMigrar").text(`Migración de Candidato: ${postulante}` + ` a Colaborador`);
        $("#SubTituloModalMigrar").text(`Evaluador: ` + G_NombreUsuario_Logeado);
        $("#ModalMigrarCandidato").modal("show");
    });

    $("#BtnMigrar").click(function () {
        MigrarCandidato();
    });

    $("#BtnEliminarAdjunto").click(function () {
        DeleteAdjuntoEvaluacion();
    });

    $("#cboTipoEvaluacionE").change(function () {
        tipoEvaluacion = "";
        tipoEvaluacion += $(this).val() + ",";
    })

    ComboCanalPostulacion("#cboCanalPostulacion", "Seleccionar...");
    ComboTipoProceso("#cboTipoProceso", "Seleccionar...");

    $("#BtnCerrar").click(function () {
        CerrarConvocatoria();
    });
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
    trFirst += "<th></th>";
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
            tr += `<td class="StatuEvaluar" data-ideva="${value.evaluacionID}" data-idcolaborador="${value.colaboradorId}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-primary">${value.evaluacionEstado}</span></td>`;
            toteneval++;
        }
        else if (value.evaluacionEstadoCodigo == "APTO") {
            tr += `<td class="StatuEvaluar" data-ideva="${value.evaluacionID}" data-idcolaborador="${value.colaboradorId}" data-postulante="${value.postulante}"><span class=" badge rounded-pill bg-success">${value.evaluacionEstado}</span></td>`;
            totapto++;
        }
        else if (value.evaluacionEstadoCodigo == "APOBS") {
            tr += `<td class="StatuEvaluar" data-ideva="${value.evaluacionID}" data-idcolaborador="${value.colaboradorId}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-warning">${value.evaluacionEstado}</span></td>`;
            totaptoobs++;
        }
        else if (value.evaluacionEstadoCodigo == "NOAPTO") {
            tr += `<td class="StatuEvaluar" data-ideva="${value.evaluacionID}" data-idcolaborador="${value.colaboradorId}" data-postulante="${value.postulante}"><span class="badge rounded-pill bg-danger">${value.evaluacionEstado}</span></td>`;
            totnoapto++;
        }
        else {
            tr += `<td></td>`;
        }

        tr += `<td>
        <li class="list-inline-item">
                    <div class="dropdown">
                        <a href="javascript:void(0);" class="btn btn-light btn-icon" id="dropdownMenuLink3" data-bs-toggle="dropdown" aria-expanded="false">
                            <i class="ri-equalizer-fill"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-menu-end" style="">
                            <li>
                                <a href="javascript:void(0);" class="dropdown-item view-item-btn BtnEvaluar" data-colaboradorid="${value.colaboradorId}" 
                                data-evaluid="${value.evaluacionID}" data-postulante="${value.postulante}">
                                    <i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
                                    Evaluar
                                </a>
                            </li>
                            <li>
                                <a class="dropdown-item view-item-btn" href="/Postulacion/Detalle?postulacionID=${value.postulacionId}">
                                    <i class="ri-pencil-fill align-bottom me-2 text-muted"></i>
                                    Información General
                                </a>
                            </li>`;
        if (value.evaluacionEstadoCodigo == "APTO" && value.tipoColaborador == "POSTULANTE") {
            tr += `<li>
                        <a href="javascript:void(0);" class="dropdown-item view-item-btn convertircolaborador" data-colaid="${value.colaboradorId}" data-postu="${value.postulante}">
                            <i class="ri-information-fill me-2 align-middle text-muted"></i>
                            Convertir a colaborador
                        </a>
                   </li>`;
        }

        tr += `</ul></div></li></td>`

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
                    $("#txtFechaSolicitud").val(obj.fechaSolicitud.substring(0, 10));
                    $("#cboCargo").val(obj.cargoID);
                    $("#txtVacante").val(obj.vacante);
                    $("#cboPrioridad").val(obj.prioridadConvocatoriaID);
                    $("#txtFechaLimite").val(obj.fechaLimite.substring(0, 10));
                    $("#cboEmpresa").val(obj.empresaID);
                    $("#txtTitulo").val(obj.titulo);
                    CKEDITOR.instances.editor1.setData(obj.detalleConvocatoria);
                    $("#txtDireccion").val(obj.direccion);
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
                    $("#cboEstadoConvocatoria").val(obj.estadoConvocatoriaId);
                    $("#cboEtapaConvocatoria").val(obj.etapaConvocatoriaID);
                    $("#cboTipoEvaluacionE").val(obj.tipoEvaluacionID.split(",")).trigger("change");
                    $("#cboTipoProceso").val(obj.tipoProcesoSeleccionID);

                    if (obj.codigoEstadoConvocatoria == "CERRA" || obj.codigoEstadoConvocatoria == "CANCE") {
                        $("#BtnActualizar").fadeOut(0);
                        $("#BtnCerrar").fadeOut(0);
                    } else {
                        $("#BtnActualizar").fadeIn(0);
                        $("#BtnCerrar").fadeIn(0);
                    }
                    //ComboTipoEvaluacionSelect2("/TipoEvaluacion/GetAll", "tipoEvaluacionID", "descripcion", "cboTipoEvaluacion", obj.tipoEvaluacionIDS);
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
    let IdTipoEvaluacion = tipoEvaluacion.substring(0, tipoEvaluacion.length - 1);

    let model = {
        "ConvocatoriaID": ConvocatoriaID,
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
        //"SolicitanteID": $("#SolicitanteID").val(),
        //"CoordinadorID": $("#CoordinadorID").val(),
        "ClienteID": $("#cboCliente").val(),
        "ModificadoPor": G_IdUsuario_Logeado,
        "ResponsableID": G_IdUsuario_Logeado,
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
        "EstadoConvocatoriaId": $("#cboEstadoConvocatoria").val(),
        "EtapaConvocatoriaID": $("#cboEtapaConvocatoria").val(),
        "TipoEvaluacionID": IdTipoEvaluacion,
        "TipoProcesoSeleccionID": $("#cboTipoProceso").val()
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
    if (model.EstadoConvocatoriaId == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Estado Convocatoria es obligatorio`, false);
    }
    if (model.EtapaConvocatoriaID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Etapa Convocatoria es obligatorio`, false);
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

    if ($("#fileAdjunto").val() != "") {
        let File = $("#fileAdjunto").get(0).files;
        //if ((File[0].size / 1024 / 1024) > 10) {
        //    //Toastr_NotificacionError("¡Aviso del Sistema!", `El Archivo adjuntado supera los ${G_Menu_Adjunto_Size}MB`);
        //    Toastr_NotificacionError("¡Aviso del Sistema!", `El Archivo adjuntado supera los ${10}MB`);
        //    return false;
        //}
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("EvaluacionID", EvaluacionID);
    model.append("ColaboradorID", ColaboradorID);
    model.append("Fecha", $("#txtFechaEvaluacion").val());
    model.append("TipoEvaluacionID", $("#cboTipoEvaluacion").val());
    model.append("EvaluacionEstadoID", $("#cboStatus").val());

    if (ValidarIntNull(model.get('TipoEvaluacionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Evaluación es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('EvaluacionEstadoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Status es obligatorio`, false);
    }
    if (model.get("Fecha").length == 0 || model.get("Fecha") == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha es obligatorio`, false);
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
                if (obj != null) {
                    $("#txtId").val(obj.evaluacionDetalleID);
                    EvaluacionDetalleID = obj.evaluacionDetalleID;
                    EvaluacionID = obj.evaluacionID;
                    //$("#txtFechaEvaluacion").datepicker('setDate', obj.fechaView);
                    $("#txtFechaEvaluacion").val(obj.fecha.substring(0, 10).replaceAll("/", "-"));
                    ComboTipoEvaluacion("#cboTipoEvaluacion", "Seleccionar...", obj.tipoEvaluacionID, ConvocatoriaID);
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

                            $("#BtnEliminarAdjunto").fadeIn(0);
                        } else {
                            $(".divBtnAdjuntoView_url_img").fadeOut(0);
                            $("#BtnEliminarAdjunto").fadeOut(0);
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

    if ($("#fileAdjunto").val() != "") {
        let File = $("#fileAdjunto").get(0).files;
        //if ((File[0].size / 1024 / 1024) > 10) {
        //    Toastr_NotificacionError("¡Aviso del Sistema!", `El Archivo adjuntado supera los ${10}MB`);
        //    return false;
        //}
        model.append("Url_Adjunto_File", File[0]);
    }

    model.append("EvaluacionDetalleID", EvaluacionDetalleID);
    model.append("EvaluacionID", EvaluacionID);
    model.append("ColaboradorID", ColaboradorID);
    model.append("Fecha", $("#txtFechaEvaluacion").val());
    model.append("TipoEvaluacionID", $("#cboTipoEvaluacion").val());
    model.append("EvaluacionEstadoID", $("#cboStatus").val());
    model.append("UrlAdjunto", rutaAdjunto);

    if (ValidarIntNull(model.get('TipoEvaluacionID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Evaluación es obligatorio`, false);
    }
    if (ValidarIntNull(model.get('EvaluacionEstadoID')) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Status es obligatorio`, false);
    }
    if (model.get("Fecha").length == 0 || model.get("Fecha") == "") {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Fecha es obligatorio`, false);
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
function ActualizarStatusEvaluacion() {
    let validacion = true;

    let model = {
        "EvaluacionID": EvaluacionID,
        "ConvocatoriaID": ConvocatoriaID,
        "ColaboradorID": ColaboradorID,
        "EvaluacionEstadoID": $("#cboStatusEvaluacion").val(),
        "CanalPostulacionID": $("#cboCanalPostulacion").val()
    };

    if (ValidarIntNull(model.EvaluacionEstadoID) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Status es obligatorio`, false);
    }
    if (ValidarIntNull(model.CanalPostulacionID) == false) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Canal Postulación es obligatorio`, false);
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
                    url: `../EvaluacionDetalle/UpdateEvaluacion`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó el status de la evaluación correctamente.", "success");
                            $("#ModalEvaluacionStatus").modal("hide");
                            GetAllByConvocatoria(ConvocatoriaID);
                            LimpiarStatus();
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
function LimpiarStatus() {
    $("#cboStatusEvaluacion").val(-1);
    $("#cboCanalPostulacion").val(-1);
}
function GetCandidato(id) {
    $.ajax({
        type: 'GET',
        url: `../Candidato/Get?candidatoID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                //let obj = response.data;
                ObjCandidato = response.data;
                if (ObjCandidato != null) {
                    $("#txtId").val(ObjCandidato.candidatoID);
                    //$("#txtFechaSolicitud").val(obj.fechaSolicitud);
                    $("#txtNumDocumento").val(ObjCandidato.numeroDocumentoIdentidad);
                    $("#txtNombres").val(ObjCandidato.nombres);
                    $("#txtApellidoPaterno").val(ObjCandidato.apellidoParterno);
                    $("#txtApellidoMaterno").val(ObjCandidato.apellidoMarterno);
                    $("#txtDirección").val(ObjCandidato.direccion);
                    $("#cboDepartamento").val(ObjCandidato.departamentoID);
                    ComboProvincia("#cboProvincia", "Seleccionar", ObjCandidato.departamentoID, ObjCandidato.provinciaID);
                    ComboDistrito("#cboDistrito", "Seleccionar", ObjCandidato.provinciaID, ObjCandidato.distritoResidenciaID);
                    $("#txtTelefono").val(ObjCandidato.telelefono1);
                    $("#txtCorreo").val(ObjCandidato.direccionMail1);
                    $("#cboNivelEducativo").val(ObjCandidato.tipoEstudioID);
                    $("#cboProfesion").val(ObjCandidato.profesionID);
                    $("#cboSexo").val(ObjCandidato.sexo);
                    $("#txtAnioExperiencia").val(ObjCandidato.anioExperiencia);
                    $("#txtEmpresaActual").val(ObjCandidato.empresaActual);
                    $("#txtFechaInicio").datepicker('setDate', ObjCandidato.fechaInicialLaboralView);
                    $("#txtFechaFin").datepicker('setDate', ObjCandidato.fechaFinalLaboralView);
                } else {
                    Swal.fire("¡Aviso del Sistema!", "No se encontró el candidato", "warning")
                }
            } else {
                console.log(response);
            }
        },
        error: function () { },
        complete: function () { }
    });
}
function MigrarCandidato() {
    let validacion = true;
    let model = {
        "NumeroDocumentoIdentidad": ObjCandidato.numeroDocumentoIdentidad,
        "Nombres": ObjCandidato.nombres,
        "ApellidoParterno": ObjCandidato.apellidoParterno,
        "ApellidoMarterno": ObjCandidato.apellidoMarterno,
        "Direccion": ObjCandidato.direccion,
        "DistritoResidenciaID": ObjCandidato.distritoResidenciaID,
        "Telelefono1": ObjCandidato.telelefono1,
        "DireccionMail1": ObjCandidato.direccionMail1,
        "TipoEstudioID": ObjCandidato.tipoEstudioID,
        "ProfesionID": ObjCandidato.profesionID,
        "Sexo": ObjCandidato.sexo,
        "AnioExperiencia": ObjCandidato.anioExperiencia,
        "EmpresaActual": ObjCandidato.empresaActual,
        "FechaInicial_Laboral": ObjCandidato.fechaInicialLaboralView,
        "FechaFinal_Laboral": ObjCandidato.fechaFinalLaboralView,
        "TipoPlanillaID": $("#cboTipoPlanilla").val(),
        "CandidatoID": ObjCandidato.candidatoID,
        "EmpresaId": ObjCandidato.empresaId,
        "ClienteId": ObjCandidato.clienteId,
        "CreadoPor": G_IdUsuario_Logeado,
    };

    if (model.TipoPlanillaID == -1) {
        validacion = false;
        Toastr_NotificacionError("¡Aviso del Sistema!", `El campo Tipo Planilla es obligatorio`, false);
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
                            UpdateTipoColaborador()
                            GetAllByConvocatoria(ConvocatoriaID);
                            $("#ModalMigrarCandidato").modal("hide");
                            $("#InsertColaborador").modal("show");
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se realizó la migración.", "error")
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
function UpdateTipoColaborador() {
    let validacion = true;

    let model = {
        "CandidatoID": ObjCandidato.candidatoID,
        "ModificadoPor": G_IdUsuario_Logeado
    };

    if (validacion) {
        $.ajax({
            type: 'POST',
            url: `../Candidato/UpdateTipoColaborador`,
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            async: true,
            cache: false,
            beforeSend: function () {
            },
            success: function (response) {
                if (response.resultado) {
                } else {
                    if (response.codigoError == "ERROR_API") {
                        Swal.fire("¡Aviso del Sistema!", "Error API - UpdateTipoColaborador", "error")
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
}
function DeleteAdjuntoEvaluacion() {
    let validacion = true;
    var model = new FormData();

    model.append("EvaluacionDetalleID", EvaluacionDetalleID);
    model.append("EvaluacionID", EvaluacionID);
    model.append("ColaboradorID", ColaboradorID);

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
                    url: `../EvaluacionDetalle/DeleteAdjunto`,
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
                            Swal.fire("¡Aviso del Sistema!", "Se eliminó el adjunto de la evaluación correctamente.", "success");
                            $("#ModalEvaluacion").modal("hide");
                            GetAllByConvocatoria(ConvocatoriaID);
                            Limpiar();
                        }
                        else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "No se eliminó correctamente.", "error")
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
function GetEvaluacionStatus(id) {
    $.ajax({
        type: 'GET',
        url: `../Evaluacion/Get?EvaluacionID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () { },
        success: function (response) {
            if (response.resultado) {
                let obj = response.data;
                if (obj != null) { 
                    $("#cboStatusEvaluacion").val(obj.evaluacionEstadoID);
                    $("#cboCanalPostulacion").val(obj.canalPostulacionID);                    
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
function CerrarConvocatoria() {
    let validacion = true;

    let model = {
        "ConvocatoriaID": ConvocatoriaID,
        "EstadoConvocatoriaId": 4,
        "CerradoPor": G_IdUsuario_Logeado,        
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
                    url: `../Convocatoria/CerrarConvocatoria`,
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            Get(ConvocatoriaID)
                            Swal.fire("¡Aviso del Sistema!", "Se realizó el cierre de la convocatoria correctamente.", "success")
                            setTimeout(function () { location.href = `/Convocatoria`; }, 1800);
                        } else {
                            if (response.codigoError == "ERROR_API") {
                                Swal.fire("¡Aviso del Sistema!", "Error API - CerrarConvocatoria", "error")
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