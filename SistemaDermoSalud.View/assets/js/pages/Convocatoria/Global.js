let CargoTerna;
//Combo
function ComboCargo(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Cargo/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.cargoID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboMotivo(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../MotivoConvocatoria/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.motivoConvocatoriaID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboPrioridad(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Prioridad/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.prioridadConvocatoriaID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboEmpresa(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Empresa/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.tipoSituacionID == "ACT") {
                        $(componente).append(`<option value="${value.empresaID}">${value.razonSocial}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboTipoModalidad(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoModalidad/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.tipoModalidadID}">${value.descripcion}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboDepartamento(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Departamento/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.departamentoID}">${value.descripcion}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboProvincia(componente, texto, departamentoID, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Provincia/GetAll?DepartamentoID=${departamentoID}`,
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.provinciaID}">${value.descripcion}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboDistrito(componente, texto, provinciaID, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Distrito/GetAllByProvincia?ProvinciaID=${provinciaID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.distritoID}">${value.descripcion}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
async function ComboCandidato(componente, texto = "Buscar...", showTodos = false) {
    $(componente).select2({
        theme: "bootstrap",
        placeholder: texto,
        ajax: {
            url: `/Empresa/GetAll`,
            dataType: "json",
            delay: 250,
            data: function (e) {
                return {
                    Descripcion: e.term,
                    top: 20
                }
            },
            processResults: function (e, t) {
                if (showTodos == true) {
                    let objTodos = {
                        id: -1,
                        razonSocial: "Todos..."
                    }
                    e.data.push(objTodos)
                }
                return {
                    results: e.data
                }
            },
            cache: !0
        },
        escapeMarkup: function (e) {
            return e
        },
        minimumInputLength: 1,
        templateResult: function (e) {
            if (e.loading) return e.text;
            return `<div class="select2-result-repository clearfix">${e.razonSocial}</div>`;
        },
        templateSelection: function (e) {
            if (e.id.trim().length == 0) {
                return e.text;
            } else if (e.razonSocial != null) {
                return `${e.razonSocial}`;
            } else {
                return e.text;
            }
        }
    });

}
async function loadSelect2(u, v, t, c, m) {
    const response = await fetch(u,
        {
            method: 'GET',
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {

            let arr = [];
            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t]
                };
                arr.push(objChild);
            }
            if (m) {
                $("#" + c).select2({
                    placeholder: "Buscar candidato...",
                    data: arr,
                    dropdownParent: $('#' + m),
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: "Buscar candidato...",
                    data: arr,
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
        })
}
function ComboTipoEvaluacion(componente, texto, seleccionado = null, ConvocatoriaID) {
    $.ajax({
        type: 'GET',
        url: `../TipoEvaluacion/ListaTipoEvaluacionByConvocatoria?ConvocatoriaID=${ConvocatoriaID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.tipoEvaluacionID}">${value.descripcion}</option>`);
                    }                    
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboStatus(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../EvaluacionEstado/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.evaluacionEstadoID}">${value.descripcion}</option>`);
                    }                   
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboEstadoConvocatoria(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../EstadoConvocatoria/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.estadoConvocatoriaId}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboCliente(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Cliente/GetAll?TipoSocio=c`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.socioNegocioId}">${value.razonSocial}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
async function ComboEmpresaSelect2(u, v, t, c, m) {
    const response = await fetch(u,
        {
            method: 'GET',
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {

            let arr = [];
            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t]
                };
                arr.push(objChild);
            }
            if (m) {
                $("#" + c).select2({
                    placeholder: "Buscar empresa...",
                    data: arr,
                    dropdownParent: $('#' + m),
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: "Buscar empresa...",
                    data: arr,
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
        })
}
async function ComboCargoSelect2(u, v, t, c, m) {
    const response = await fetch(u,
        {
            method: 'GET',
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {

            let arr = [];
            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t]
                };
                arr.push(objChild);
            }
            if (m) {
                $("#" + c).select2({
                    placeholder: "Buscar cargo...",
                    data: arr,
                    dropdownParent: $('#' + m),
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: "Buscar cargo...",
                    data: arr,
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
        })
}
async function ComboTipoEvaluacionSelect2(u, v, t, c, m) {
    const response = await fetch(u,
        {
            method: 'GET',
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {

            let arr = [];
            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t]
                };
                arr.push(objChild);
            }
            if (m) {
                $("#" + c).select2({
                    placeholder: "Seleccionar Evaluación...",
                    multiple:true,
                    data: arr,
                    dropdownParent: $('#' + m),
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: "Seleccionar Evaluación...",
                    multiple: true,
                    data: arr,
                    escapeMarkup: function (m) {
                        return m;
                    }
                });
            }
        })
}
function ComboEtapaConvocatoria(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../EtapaConvocatoria/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.etapaConvocatoriaID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboTipoPlanilla(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoPlanilla/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.tipoPlanillaID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboClienteByEmpresa(componente, texto, seleccionado = null, IdEmpresa) {
    $.ajax({
        type: 'GET',
        url: `../Cliente/GetClienteByEmpresa?id=${IdEmpresa}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.socioNegocioID}">${value.razonSocial}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboEtapa(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../EtapaConvocatoria/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.etapaConvocatoriaID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboCanalPostulacion(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../CanalPostulacion/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.canalPostulacionID}">${value.descripcion}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboTipoProceso(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoProcesoSeleccion/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    $(componente).append(`<option value="${value.tipoProcesoSeleccionID}">${value.descripcion}</option>`);
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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
function ComboTipoEvaluacionByConvocatoriaByEvaluacion(componente, texto, seleccionado = null, ConvocatoriaID, EvaluacionID) {
    $.ajax({
        type: 'GET',
        url: `../TipoEvaluacion/ListaTipoEvaluacionByConvocatoriaByEvaluacion?ConvocatoriaID=${ConvocatoriaID}&EvaluacionID=${EvaluacionID}`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(componente).empty();
            $(componente).append(`<option value="-1">${texto}</option>`);
        },
        success: function (response) {
            if (response.resultado) {
                response.data.forEach(function (value, index) {
                    if (value.estado) {
                        $(componente).append(`<option value="${value.tipoEvaluacionID}">${value.descripcion}</option>`);
                    }
                })

                if (seleccionado != null) {
                    $(componente).val(seleccionado);
                }
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