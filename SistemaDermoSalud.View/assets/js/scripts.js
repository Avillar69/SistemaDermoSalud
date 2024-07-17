//t = Tipo : GET / POST / DELETE , u = urlBase +  url del controller en el api , m = function callback () 
function axiosCon(t, u, m) {
    var axiosConfig = {
        method: t,
        url: u,
        //withCredentials: true,
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            //"Authorization": "Bearer " + localStorage.token
        }
    };
    axios(axiosConfig)
        .then(res => {
            m(res);
        })
        .catch(err => {
            console.log(err);
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
//Metodo GET simple . Parametros : URL , callback
let PermisoPaginaEliminar = $("#PermisoPaginaEliminar").val();
let PermisoPaginaModificar = $("#PermisoPaginaModificar").val();
let PermisoPaginaDetalle = $("#PermisoPaginaDetalle").val();
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
//Cargar un select deste la tabla Global.Maestro , Parametros : URL : urlbase + url controller api , id= Id del select , T : tabla en tabla global.Maestro , c : campo en tabla global.maestro , m = funcioon callback
async function loadSelectMG(u, id, t, c, m) {
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
        content += '<option value="" selected>Seleccione</option>';
        for (var i = 0; i < responseJson.data.length; i++) {
            content += "<option value='" + responseJson.data[i]["maestroId"] + "' " + ">" + responseJson.data[i]["valor"] + "</option>";
        }
        $("#" + id).html(content);
        if (m != null) {
            m();
        }
    }).catch(err => {
        console.log(err);
        Swal.fire('Error', err.toString(), 'error');
    });
}
//Función para cargar el datatable de Inicio, Parametros : array de columnas (según json),json con datos, campo ID, id de Table, botones a agregar
//arrOrder : [[1, 'asc']] primer campo numero de columna, 2do campo tipo de orden, asc o desc
function loadDataTable(cols, datos, rid, tid, btns,arrOrder, showFirstField) {
    var columnas = [];
    for (var i = 0; i < cols.length; i++) {
        let item = {
            data: cols[i]
        };
        columnas.push(item);
    }
    let itemBtn = {
        "data": null,
        "defaultContent": "<center>" + btns + "</center>"
    };
    columnas.push(itemBtn);
    tbDatos = $('#' + tid).DataTable({
        data: datos,
        columns: columnas,
        rowId: rid,
        order: arrOrder,
        columnDefs:
            [
                {
                    "targets": 0,
                    "visible": showFirstField,
                },
                {
                    "targets": columnas.length - 1,
                    "width": "10%"
                }],
        searching: !0,
        bLengthChange: !0,
        destroy: !0,
        pagingType: "full_numbers",
        info: !1,
        paging: !0,
        pageLength: 50,
        responsive: !0,
        footer: false,
        deferRender: !1,
        language: {
            "decimal": "",
            "emptyTable": "No existen registros a mostrar.",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Registros",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total registros)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Registros",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            search: "_INPUT_",
            searchPlaceholder: "Buscar ",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "<<",
                "last": ">>",
                "next": ">",
                "previous": "<"
            }
        }
    });
}
//Función que retorna el html para botón de opciones de tabla , ver , editar y eliminar
function cadButtonOptions() {
    let cad = "";
    cad += '<ul class="list-inline" style="margin-bottom: 0px;">';
    cad += '<li class="list-inline-item">';
    cad += '<div class="dropdown">';
    cad += '<button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">';
    cad += '<i class="ri-more-fill align-middle"></i>';
    cad += '</button>';
    cad += '<ul class="dropdown-menu dropdown-menu-end" style="">';
    //if (PermisoPaginaDetalle == 1) {
    cad += '<li>';
    cad += '<a class="dropdown-item" href="javascript:void(0)" onclick="btnActions(this, \'view\');"><i class="ri-eye-fill align-bottom me-2 text-muted"></i>Ver detalle</a>';
    cad += '</li>';
    //}    
    //if (PermisoPaginaModificar == 1) {
    cad += '<li>';
    cad += '<a class="dropdown-item edit-item-btn" href="javascript:void(0)" onclick="btnActions(this, \'edit\');" ><i class="ri-pencil-fill align-bottom me-2 text-muted"></i>Editar</a>';
    cad += '</li>';
    //}
    //if (PermisoPaginaEliminar == 1) {
    cad += '<li>';
    cad += '<a class="dropdown-item remove-item-btn" data-bs-toggle="modal" href="javascript:void(0)" onclick="btnActions(this, \'delete\');"><i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Eliminar</a>';
    cad += '</li>';
    //}    
    cad += '</ul>';
    cad += '</div>';
    cad += '</li>';
    cad += ' </ul>';
    return cad;
}
//Get Select , (Id , URL Api , Value , Option , Metodo callback)
function loadSelect(id, u, v, o, callback) {
    let axiosConfig = {
        method: "GET",
        url: BaseUrl + u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            //"Authorization": "Bearer " + localStorage.token
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let content = "";
            content += "<option value='0'>Seleccione</option>";
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
//Get Select , (Id , URL Api , Value , Option , Metodo callback)
function loadSelectPlace(id, u, v, o, placeholder, callback) {
    let axiosConfig = {
        method: "GET",
        url: BaseUrl + u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            //"Authorization": "Bearer " + localStorage.token
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let content = "";
            content += "<option value=''>" + placeholder + "</option>";
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
async function loadSelect2(u, v, t, c, modal, callback) {
    let axiosConfig = {
        method: "GET",
        url: u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let arr = [{
                id: 0,
                text: "Seleccione",
            }];
            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t]
                };
                arr.push(objChild);
            }
            if (modal) {
                $("#" + c).select2({
                    placeholder: "Seleccione",
                    data: arr, allowClear: true,
                    dropdownParent: $('#' + modal),
                    escapeMarkup: function (e) {
                        return e;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: "Seleccione",
                    data: arr, allowClear: true,
                    escapeMarkup: function (e) {
                        return e;
                    }
                });
            }

            if (callback != null) {
                callback();
            }
        })
        .catch(err => {
            Swal.fire('Error', err.toString(), 'error');
        });
}
async function loadSelect2V(u, v, t, t2, c, modal, callback) {
    let axiosConfig = {
        method: "GET",
        url: u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let arr = [{
                id: 0,
                text: "Seleccione",
            }];
            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t] + '-' + responseJson.data[j][t2]
                };
                arr.push(objChild);
            }
            if (modal) {
                $("#" + c).select2({
                    placeholder: "Seleccione",
                    data: arr, allowClear: true,
                    dropdownParent: $('#' + modal),
                    escapeMarkup: function (e) {
                        return e;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: "Seleccione",
                    data: arr, allowClear: true,
                    escapeMarkup: function (e) {
                        return e;
                    }
                });
            }

            if (callback != null) {
                callback();
            }
        })
        .catch(err => {
            Swal.fire('Error', err.toString(), 'error');
        });
}
async function loadSelect2Placeholder(u, v, t, c, placeholder, modal, callback) {
    let axiosConfig = {
        method: "GET",
        url: u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let arr = [];

            for (var j = 0; j < responseJson.data.length; j++) {
                let objChild = {
                    id: responseJson.data[j][v],
                    text: responseJson.data[j][t]
                };
                arr.push(objChild);
            }
            if (modal) {
                $("#" + c).select2({
                    placeholder: placeholder,
                    data: arr, allowClear: true,
                    dropdownParent: $('#' + modal),
                    escapeMarkup: function (e) {
                        return e;
                    }
                });
            }
            else {
                $("#" + c).select2({
                    placeholder: placeholder,
                    data: arr, allowClear: true,
                    escapeMarkup: function (e) {
                        return e;
                    }
                });
            }

            if (callback != null) {
                callback();
            }
        })
        .catch(err => {
            Swal.fire('Error', err.toString(), 'error');
        });
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
function camelizeLow(str) {
    return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
        if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
        return index != 0 ? match.toLowerCase() : match.toUpperCase();
    });
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
                $(this).val("");
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
function firstDayMonth() {
    let y = new Date().getFullYear();
    let m = new Date().getMonth() + 1
    let d = "01";
    return d + "/" + (m.length == 1 ? ("0" + m) : m) + "/" + y;
}
function currentDate() {
    let y = new Date().getFullYear();
    let m = new Date().getMonth() + 1;
    let d = new Date().getDate();
    return (d.toString().length == 1 ? ("0" + d.toString()) : d.toString()) + "/" + (m.toString().length == 1 ? ("0" + m.toString()) : m.toString()) + "/" + y;
}
function lastDayMonth() {
    let y = new Date().getFullYear();
    let m = new Date().getMonth() + 1
    let today = new Date();
    let lastDayOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);
    return lastDayOfMonth.getDate() + "/" + (m.length == 1 ? ("0" + m) : m) + "/" + y;
}
function formatFecha(f) {
    return f.split("/")[1] + "/" + f.split("/")[0] + "/" + f.split("/")[2]
}
function firstUpper(str) {
    return str.charAt(0).toUpperCase() + str.slice(1);
}
function getDateFormatString(f) {
    let dia = f.split("/")[0];
    let mes = f.split("/")[1];
    let anio = f.split("/")[2];
    // Creamos array con los meses del año
    const meses = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
    // Creamos array con los días de la semana
    const dias_semana = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];
    // Construimos el formato de salida
    return dia + ' de ' + meses[parseInt(mes) - 1] + ' de ' + anio;
}
function getQueryParams(url) {
    const paramArr = url.slice(url.indexOf('?') + 1).split('&');
    const params = {};
    paramArr.map(param => {
        const [key, val] = param.split('=');
        params[key] = decodeURIComponent(val);
    })
    return params;
}
function loadSelectParam(id, u, v, o, callback, param) {
    let axiosConfig = {
        method: "GET",
        url: BaseUrl + u,
        withCredentials: false,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            //"Authorization": "Bearer " + localStorage.token
        }
    };
    axios(axiosConfig)
        .then(res => {
            let responseJson = res.data;
            let content = "";
            //content += "<option value=''>Seleccione</option>";
            for (var i = 0; i < responseJson.data.length; i++) {
                if (param == parseInt(responseJson.data[i][v])) {
                    //$(componente).append(`<option value="${value.num_Ahno}"selected>${value.num_Ahno}</option>`);
                    content += "<option value='" + responseJson.data[i][v] + "' " + " selected>" + responseJson.data[i][o] + "</option>";
                } else {
                    //$(componente).append(`<option value="${value.num_Ahno}">${value.num_Ahno}</option>`);
                    content += "<option value='" + responseJson.data[i][v] + "' " + ">" + responseJson.data[i][o] + "</option>";
                }

                //content += "<option value='" + responseJson.data[i][v] + "' " + ">" + responseJson.data[i][o] + "</option>";
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