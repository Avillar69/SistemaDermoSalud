//Global
let ConvocatoriaID = $("#txtId").val();
let TablaTerna = $("#TablaTerna");
let G_FechaActual = $("#G_FechaActual").val();
let cargo = localStorage.getItem('CargoTerna');
let cliente = localStorage.getItem('Cliente');
$(document).ready(function () {
    $("#txtTerna").text(`Listado de ternas: Cargo de ${cargo} para cliente ${cliente}`);

    //Listado
    GetAllTernaByConvocatoria(ConvocatoriaID);
})

//Listado
function GetAllTernaByConvocatoria(id) {
    $.ajax({
        type: 'GET',
        url: `../Convocatoria/GetAllByConvocatoriaTerna?convocatoriaID=${id}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            if (TablaTerna instanceof $.fn.dataTable.Api) {
                $('#TablaTerna').DataTable().clear().destroy();
            }
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
    EstructuraHead(data);
}
function EstructuraHead(data) {
    if (data.head.headFirstFecha.length != 0) {
        data.head.headFirstFecha.forEach((valueFecha) => {
            let terna = `<h5>Candidatos de terna #${valueFecha.numeroTerna}</h5>`;
            let ternaFecha = `<h5>Fecha de entrevistas: ${valueFecha.fecha}</h5>`;

            let trFirst = `<tr>`;
            let th = `<th>Candidato</th>`;
            trFirst += `<th style="width:300px;">Candidato</th>`;

            //HEADER CABECERA
            if (data.head.headFirstStatic.length != 0) {
                data.head.headFirstStatic.forEach((value) => {
                    if (value.descripcionCorta != 'Resultado') {
                        let thTipoEvaluacion = `<th data-id="${value.tipoEvaluacionID}"><center>${value.descripcionCorta}</center></th>`;
                        trFirst += thTipoEvaluacion;
                    }                   
                })
            }

            trFirst += `<th><center>Status</center></th>`;
            trFirst += `</tr>`;

            let tablaBody = `<tr>`;

            data.body.data.forEach((value) => {
                let consu00 = value.listaDetalleEvaluacionTerna.find(x => x.colaboradorId == value.colaboradorID &&
                    valueFecha.fecha == value.fecha)
                if (consu00) {
                    tablaBody += `<td>${value.postulante}</td>`;

                    data.head.headFirstStatic.forEach((tipo) => {
                        if (tipo.descripcionCorta != 'Resultado') {
                            let consu = value.listaDetalleEvaluacionTerna.find(x => x.colaboradorId == value.colaboradorID &&
                                x.tipoEvaluacionID == tipo.tipoEvaluacionID && valueFecha.fecha == value.fecha)
                            if (consu) {
                                if (consu.evaluacionEstadoDetalleCodigo == "ENEVA") {
                                    tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-primary text-white p-2">${consu.evaluacionEstadoDetalle}</span></td>`;
                                }
                                else if (consu.evaluacionEstadoDetalleCodigo == "APTO") {
                                    tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-success text-white p-2">${consu.evaluacionEstadoDetalle}</span></td>`;
                                }
                                else if (consu.evaluacionEstadoDetalleCodigo == "APOBS") {
                                    tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-warning text-white p-2">${consu.evaluacionEstadoDetalle}</span></td>`;
                                }
                                else if (consu.evaluacionEstadoDetalleCodigo == "NOAPTO") {
                                    tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-danger text-white p-2">${consu.evaluacionEstadoDetalle}</span></td>`;
                                }
                            }
                            else {
                                tablaBody += `<td></td>`;
                            }
                        }                        
                    })

                    let consu01 = value.listaDetalleEvaluacionTerna.find(x => x.colaboradorId == value.colaboradorID &&
                        valueFecha.fecha == value.fecha)
                    if (consu01) {
                        if (consu01.evaluacionEstadoCodigo == "ENEVA") {
                            tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-primary text-white p-2">${value.evaluacionEstado}</span></td>`;
                        }
                        else if (consu01.evaluacionEstadoCodigo == "APTO") {
                            tablaBody += `<td class="text-center"><span class=" badge rounded-pill bg-success text-white p-2">${value.evaluacionEstado}</span></td>`;
                        }
                        else if (consu01.evaluacionEstadoCodigo == "APOBS") {
                            tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-warning text-white p-2">${value.evaluacionEstado}</span></td>`;
                        }
                        else if (consu01.evaluacionEstadoCodigo == "NOAPTO") {
                            tablaBody += `<td class="text-center"><span class="badge rounded-pill bg-danger text-white p-2">${value.evaluacionEstado}</span></td>`;
                        }
                        else {
                            tablaBody += `<td></td>`;
                        }
                    }               
                }                      

                tablaBody += `</tr>`;                
            })

            //PINTAR TABLA
            let tablaHeaderBody = `<div class="table table-responsive">
                                    <table class="table table-condensed table-bordered table-hover" id="TablaTerna-${valueFecha.numeroTerna}">
                                        <thead class="text-muted table-light">
                                        ${trFirst}
                                        </thead>
                                        <tbody>
                                        ${tablaBody}
                                        </tbody>
                                    </table>
                               </div>`;
            //tablaHeader += trFirst;
            //tablaHeader += `</thead>
            //            <tbody>`;

            //tablaHeader += tablaBody;
            //tablaHeader += `</tbody>
            //            </table>
            //          </div>`;
                       //PINTAR TABLA

            $("#terna").append(terna);
            $("#terna").append(ternaFecha);
            $("#terna").append(tablaHeaderBody);
        })
    }
}