//t = Tipo : GET / POST / DELETE , u = urlBase +  url del controller en el api , m = function callback () 
function axiosCon(t, u, m) {
    var axiosConfig = {
        method: t,
        url: u,

        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
        }
    };
    axios(axiosConfig)
        .then(res => {
            m(res);
        })
        .catch(err => {
            Swal.fire('Error', err.toString(), 'error');
        });
}


//Metodo GET simple . Parametros : URL , callback
async function GET(u, m) {
    var axiosConfig = {
        method: "GET",
        url: u,
        withCredentials: true,
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            "Authorization": "Bearer " + localStorage.token
        }
    };
    axios(axiosConfig)
        .then(res => {
            m(res.data);
        })
        .catch(err => {
            Swal.fire('Error', err.toString(), 'error');
        });
}
 

const swalDelete = Swal.mixin({
    customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
    },
    buttonsStyling: false
});

async function loadSelectMGClave(u, id, t, c, m) {
    let obj = {
        Tabla: t,
        Campo: c
    };
    axios.post(u, JSON.stringify(obj), {
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        let responseJson = res.data;
        let content = "";
        content += "<option value=''>Seleccione</option>";
        for (var i = 0; i < responseJson.data.length; i++) {
            content += "<option value='" + responseJson.data[i]["clave"] + "' " + ">" + responseJson.data[i]["valor"] + "</option>";
        }
        $("#" + id).html(content);
        if (m != null) {
            m();
        }
    }).catch(err => {
        Swal.fire('Error', err.toString(), 'error');
    });
}

async function loadSelectMGMaestroID(u, id, t, c, m) {
    let obj = {
        Tabla: t,
        Campo: c
    };
    axios.post(u, JSON.stringify(obj), {
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        let responseJson = res.data;
        let content = "";
        content += "<option value=''>Seleccione</option>";
        for (var i = 0; i < responseJson.data.length; i++) {
            content += "<option value='" + responseJson.data[i]["maestroId"] + "' " + ">" + responseJson.data[i]["valor"] + "</option>";
        }
        $("#" + id).html(content);
        if (m != null) {
            m();
        }
    }).catch(err => {
        Swal.fire('Error', err.toString(), 'error');
    });
}



async function loadSelectMG2(u, id, t, c, m) {
    let obj = {
        Tabla: t,
        Campo: c
    };
    axios.post(u, JSON.stringify(obj), {
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        let responseJson = res.data;
        let content = "";
        content += "<option value=''>Seleccione</option>";
        for (var i = 0; i < responseJson.data.length; i++) {
            content += "<option value='" + responseJson.data[i]["clave"] + "' " + ">" + responseJson.data[i]["valor"] + "</option>";
        }
        $("#" + id).html(content);
        if (m != null) {
            m();
        }
    }).catch(err => {
        Swal.fire('Error', err.toString(), 'error');
    });
}


async function loadSelectMG3(u, id, t, c, m) {
    let obj = {
        Tabla: t,
        Campo: c
    };
    axios.post(u, JSON.stringify(obj), {
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        let responseJson = res.data;

        let content = "";
        content += "<option value=''>Seleccione</option>";
        for (var i = 0; i < responseJson.data.length; i++) {
            content += "<option value='" + responseJson.data[i]["clave"] + "' " + ">" + responseJson.data[i]["valor"] + "</option>";
        }
        $("#" + id).html(content);

        cargarRubro(responseJson);

        if (m != null) {
            m();
        }

    }).catch(err => {
        Swal.fire('Error', err.toString(), 'error');
    });
}
async function loadSelectMG4(u, id, t, c, m) {
    let obj = {
        Tabla: t,
        Campo: c
    };

    axios.post(u, JSON.stringify(obj), {
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(res => {
        let responseJson = res.data;

        let filteredData = responseJson.data.filter(item => item.condicion !== 'T');

        let content = "";
        content += '<option value="" selected>Seleccione</option>';
        for (var i = 0; i < filteredData.length; i++) {
            content += "<option value='" + filteredData[i]["clave"] + "' " + ">" + filteredData[i]["clave"] + " - " + filteredData[i]["valor"] + "</option>";
        }
        $("#" + id).html(content);
        if (m != null) {
            m();
        }
    }).catch(err => {
        Swal.fire('Error', err.toString(), 'error');
    });
}


function cadButtonOptions() {
    let cad = '';
    cad += `<div class="dropdown">
      <a href="javascript:void(0);" class="btn btn-light btn-icon" id="dropdownMenuLink3" data-bs-toggle="dropdown" aria-expanded="false">
        <i class="ri-equalizer-fill"></i>
      </a>
      <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuLink3" style="">
        <li><a class="dropdown-item" href="javascript:void(0)" onclick="btnActions(this, \'view\');"><i class="ri-information-fill me-2 align-middle text-muted"></i>Ver detalle</a></li>
        <li><a class="dropdown-item" href="javascript:void(0)" onclick="btnActions(this, \'edit\');"><i class=" ri-edit-2-fill me-2 align-middle text-muted"></i>Editar</a></li>
        <li><a class="dropdown-item remove-item-btn" data-bs-toggle="modal" href="javascript:void(0)" onclick="btnActions(this, \'delete\');"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>'</li>
      </ul>
    </div>`;
    return cad;
}


function cadButtonOptions1() {
    let cad = '';
    cad += `<div class="dropdown">
      <a href="javascript:void(0);" class="btn btn-light btn-icon" id="dropdownMenuLink3" data-bs-toggle="dropdown" aria-expanded="false">
        <i class="ri-equalizer-fill"></i>
      </a>
      <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuLink3" style="">
        <li><a class="dropdown-item" href="javascript:void(0)" onclick="btnActions(this, \'view\');"><i class="ri-information-fill me-2 align-middle text-muted"></i>Ver detalle</a></li>
        <li><a class="dropdown-item remove-item-btn" data-bs-toggle="modal" href="javascript:void(0)" onclick="btnActions(this, \'delete\');"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>'</li>
      </ul>
    </div>`;
    return cad;
}

function cadButtonOptionsTarget() {
    let cad = '';
    cad += `<div class="dropdown">
      <a href="javascript:void(0);" class="btn btn-light btn-icon" id="dropdownMenuLink3" data-bs-toggle="dropdown" aria-expanded="false">
        <i class="ri-equalizer-fill"></i>
      </a>
      <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuLink3" style="">
        <li><a class="dropdown-item" href="javascript:void(0)" onclick="btnActions(this, \'subirdoc\');"><i class="ri-information-fill me-2 align-middle text-muted"></i>Ver detalle</a></li>
        <li><a class="dropdown-item" href="javascript:void(0)" onclick="btnActions(this, \'editar\');"><i class=" ri-edit-2-fill me-2 align-middle text-muted"></i>Editar</a></li>

      </ul>
    </div>`;
    return cad;
}

function cadButtonOptionsPortal(estadoID, targetID) {
    // Esta función debe retornar el contenido HTML que deseas agregar a la columna "actions"

    let cad = '';
    // Botón de vista siempre habilitado
    cad += '<div class="btn-group" style="margin-right: 5px;">';
    cad += '<a href="javascript:void(0)" class="btn btn-success btn-sm fs-13" onclick="btnActions(this, \'subirdoc\');"><i class="ri-eye-fill"></i></a>';
    cad += '&nbsp;';

    // Botón de solicitud habilitado o deshabilitado según el estado
    cad += '<a href="javascript:void(0)" class="btn btn-success btn-sm fs-13';
    if (estadoID >= 4 && estadoID <= 5) {
        cad += '"';
    } else {
        cad += ' disabled"';
    }
    cad += ' onclick="btnActions(this, \'solicitud\');"><i class="ri-lock-fill"></i></a>';

    cad += '</div>';

    return cad;
}

function cadButtonOptionsPortalAdmin(estadoID, estadoSolicitudID) {
    // Esta función debe retornar el contenido HTML que deseas agregar a la columna "actions"
    console.log("solicitud", estadoSolicitudID);
    let cad = '';
    // Botón de subir doc siempre habilitado
    cad += '<div class="btn-group" style="margin-right: 5px;">';
    cad += '<a href="javascript:void(0)" class="btn btn-success btn-sm fs-13" onclick="btnActions(this, \'subirdoc\');"><i class="ri-eye-fill"></i></a>';
    cad += '&nbsp;';

    // Condición para habilitar o deshabilitar el botón "Solicitud"
    if (estadoSolicitudID === 1) {
        cad += '<a href="javascript:void(0)" class="btn btn-success btn-sm fs-13" onclick="btnActions(this, \'solicitud\');"><i class="ri-eye-fill"></i></a>';
    } else {
        cad += '<a href="javascript:void(0)" class="btn btn-success btn-sm fs-13 disabled" onclick="btnActions(this, \'solicitud\');"><i class="ri-eye-fill"></i></a>';
    }

    cad += '</div>';

    return cad;
}

async function loadSelect(id, u, v, o, callback) {
    const response = await fetch(BaseUrl + u,
        {
            method: 'GET',
        }).then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            let content = "";
            content += "<option value=''>Seleccione</option>";
            for (var i = 0; i < responseJson.data.length; i++) {
                content += "<option value='" + responseJson.data[i][v] + "' " + ">" + responseJson.data[i][o] + "</option>";
            }
            $("#" + id).html(content);
            if (callback != null) {
                callback();
            }
        })
}


function loadSelect2(id, u, v, o, callback) {
    let axiosConfig = {
        method: "GET",
        url: BaseUrl + u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let content = "";

            content += "<option value=''>Seleccione</option>";
            for (var i = 0; i < responseJson.data.length; i++) {

                content += "<option value='" + responseJson.data[i][v] + "' " + ">" + responseJson.data[i][o] + "</option>";
            }
            $("#" + id).html(content);
            if (callback != null) {
                callback();
            }
        })
        .catch(err => {
            Swal.fire('Error', err.toString(), 'error');
        });
}


async function loadSelect3(id, u, valueProperty, textProperty, callback) {

    try {

        const response = await axios.get(BaseUrl + u);

        if (response.status === 200) {
            const responseData = response.data.data;
            dataTemp = responseData;

            let content = "<option value=''>Seleccione</option>";

            for (let i = 0; i < responseData.length; i++) {
                content += `<option value='${responseData[i][valueProperty]}'>${responseData[i][valueProperty]} - ${responseData[i][textProperty]}</option>`;
            }

            $("#" + id).html(content);

            if (callback) {
                callback();
            }
        } else {
            console.error('Error al obtener los datos:', response.status);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}
async function loadSelect4(id) {
    if (typeof dataTemp === 'object' && dataTemp !== null) {
        const dataArray = Object.values(dataTemp);
        const datos = dataArray.filter(obj => obj.clasificacionCuenta === 'G' || obj.clasificacionCuenta === 'I' || obj.clasificacionCuenta === 'C');

        let content = "<option value=''>Seleccione</option>";

        for (var i = 0; i < datos.length; i++) {
            content += "<option value='" + datos[i]['cuenta'] + "'>" + datos[i]['cuenta'] + " - " + datos[i]['descripcion'] + "</option>";
        }
        $("#" + id).html(content);
    } else {
        console.error("dataTemp no es un objeto");
    }
}


function validarControl(id) {
    var textbox = document.getElementById(id);
    if (textbox.value === null || textbox.value.length === 0 || /^\s+$/.test(textbox.value)) {
        textbox.style.border = "1px solid red";
        var labels = document.getElementsByTagName("LABEL");
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].htmlFor == id) {
                labels[i].style.color = "red";
                break;
            }
        }
        textbox.focus();
        return false;
    } else {
        textbox.style.border = "";
        var labels = document.getElementsByTagName("LABEL");
        for (var i = 0; i < labels.length; i++) {
            if (labels[i].htmlFor == id) {
                labels[i].style.color = "";
                break;
            }
        }
        return true;
    }
}


function validate(css) {
    var value = true;
    $("." + css).each(function (index) {
        value = value & validarControl($(this)[0].id);
    });
    return value;
}


function camelize(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
        if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
        return index === 0 ? match.toLowerCase() : match.toUpperCase();
    });
}


function cleanControl(css) {
    $("." + css).each(function (index) {
        switch ($(this)[0].localName) {
            case "select":
                $(this).val(0);
                break;
            case "input": case "textarea":
                $(this).val("");
                break;
            case "img":
                $(this).attr("src", "/assets/images/image-placeholder.jpg");
                break;
            default:
                break;
        }
    });
}


function validarSelect2(id) {
    var select = $('#' + id);

    if (select.val() === '') {
        select.next('.select2-container').addClass('select2-error');
        select.next('.select2-container').find('.select2-selection__rendered').addClass('select2-error-text');
        select.next('.select2-container').find('.select2-selection').css({
            'border': '1px solid red', // A�ade el borde rojo al contenedor
            'border-color': 'red' // Cambia el color del borde a rojo
        });
        return false;
    } else {
        select.next('.select2-container').removeClass('select2-error');
        select.next('.select2-container').find('.select2-selection__rendered').removeClass('select2-error-text');
        select.next('.select2-container').find('.select2-selection').css({
            'border': '', // Borra el borde personalizado
            'border-color': '' // Restaura el color del borde por defecto
        });
        return true;
    }
}


function firstDayMonth() {
    let y = new Date().getFullYear();
    let m = new Date().getMonth() + 1
    let d = "01";
    return d + "-" + (m.length == 1 ? ("0" + m) : m) + "-" + y;
}


function lastDayMonth() {
    let y = new Date().getFullYear();
    let m = new Date().getMonth() + 1
    let today = new Date();
    let lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);
    return lastDayOfMonth.getDate() + "-" + (m.length == 1 ? ("0" + m) : m) + "-" + y;
}


function formatFecha(f) {
    return f.split("-")[1] + "-" + f.split("-")[0] + "-" + f.split("-")[2]
}


function firstUpper(str) {
    return str.charAt(0).toUpperCase() + str.slice(1);
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

    console.log("depa:", departamentoID);
    console.log("depa:", seleccionado);

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