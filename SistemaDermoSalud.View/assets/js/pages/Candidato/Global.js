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
function ComboListaNegra(componente, texto, seleccionado = null) {
    $.ajax({
        type: 'GET',
        url: `../ListaNegra/GetAll`,
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
                    $(componente).append(`<option value="${value.listaNegraID}">${value.descripcion}</option>`);
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