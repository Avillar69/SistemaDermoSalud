//Combo
function ComboTipoColaborador(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../TipoColaborador/GetAll`,
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
                        $(componente).append(`<option value="${value.tipoColaboradorID}">${value.descripcion}</option>`);
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
function ComboFuenteReclutamiento(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../FuenteReclutamiento/GetAll`,
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
                        $(componente).append(`<option value="${value.fuenteReclutamientoID}">${value.descripcion}</option>`);
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
function ComboCentroEstudio(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../CentroEstudio/GetAll`,
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
                        $(componente).append(`<option value="${value.centroEstudioID}">${value.descripcion}</option>`);
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
function ComboDistrito(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../Distrito/GetAll`,
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