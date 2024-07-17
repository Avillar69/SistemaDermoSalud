//Combo
function ComboProfesion(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Profesion/GetAll`,
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
                        $(componente).append(`<option value="${value.profesionID}">${value.descripcion}</option>`);
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
function ComboTipoEstudio(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoEstudio/GetAll`,
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
                    $(componente).append(`<option value="${value.tipoEstudioID}">${value.descripcion}</option>`);
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
function ComboCodigoSancion(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../CodigoSancion/GetAll`,
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
                        $(componente).append(`<option value="${value.codigoSancionID}">${value.codigo} - ${value.descripcion}</option>`);
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
function ComboTipoSancion(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoSancion/GetAll`,
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
                        $(componente).append(`<option value="${value.tipoSancionID}">${value.codigo} - ${value.descripcion}</option>`);
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
function ComboSsomaSede(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Sede/GetAll`,
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
                        $(componente).append(`<option value="${value.sedeID}">${value.descripcion}</option>`);
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
function ComboSsomaArea(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Area/GetAll`,
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
                        $(componente).append(`<option value="${value.areaID}">${value.descripcion}</option>`);
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
function ComboTipoEvento(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoEvento/GetAll`,
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
                        $(componente).append(`<option value="${value.tipoEventoID}">${value.descripcion}</option>`);
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
function ComboNivelAccidente(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../NivelAccidente/GetAll`,
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
                        $(componente).append(`<option value="${value.nivelAccidenteID}">${value.descripcion}</option>`);
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
function ComboAccidentadoPor(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../AccidentadoPor/GetAll`,
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
                        $(componente).append(`<option value="${value.accidentadoPorID}">${value.descripcion}</option>`);
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
function ComboAccidenteIncapacitado(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../AccidenteIncapacitado/GetAll`,
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
                        $(componente).append(`<option value="${value.accidenteIncapacitadoID}">${value.descripcion}</option>`);
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
function ComboTurno(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Turno/GetAll`,
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
                        $(componente).append(`<option value="${value.turnoID}">${value.descripcion}</option>`);
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
function ComboPlanta(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Planta/GetAll`,
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
                        $(componente).append(`<option value="${value.plantaID}">${value.descripcion}</option>`);
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
function ComboTipoContacto(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoContacto/GetAll`,
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
                        $(componente).append(`<option value="${value.tipoContactoID}">${value.descripcion}</option>`);
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
function ComboSituacion(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Situacion/GetAll`,
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
                        $(componente).append(`<option value="${value.situacionID}">${value.descripcion}</option>`);
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