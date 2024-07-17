//Variables Gloables
let G_IdUsuario_Logeado = $("#Menu_Id_Colaborador_Logeado").val();
let G_NombreUsuario_Logeado = $("#Menu_Colaborador_NombreCompleto_Logeado").val();
let G_IdPerfil_Logeado = $("#Menu_Id_Perfil_Logeado").val();
let G_DescPerfil_Logeado = $("#Menu_Desc_Perfil_Logeado").val();
let G_Menu_FechaActual = $("#Menu_FechaActual").val();
let G_Menu_Id_Usuario_Logeado = $("#Menu_Id_Usuario_Logeado").val();

//Funciones Globales
function ValidarIntNull(numero) {
    let resultado = true;

    numero = G_FormatoNumero_NotEval(numero);

    if (numero == null || numero == "" || numero == "-1" || numero == "0") {
        resultado = false;
    }

    return resultado;
}
function G_FormatoNumero_NotEval(numero) {
    if (numero == null) {
        numero = 0;
    }

    let Rpt = Number(numero.toString().replace(/,/g, ""));

    if (isNaN(Rpt)) {
        Rpt = 0;
    }

    if (!isFinite(Rpt)) {
        Rpt = 0;
    }

    return Number(Rpt);
}
function G_RetornaClaseFileByExtension(extensionAdjunto) {

    let respuesta = {
        Clase: "",
        Color: "",
        IsImage: false,
        IsValidExtension: false
    };

    if (extensionAdjunto != null) {

        extensionAdjunto = extensionAdjunto.toLowerCase();

        if (extensionAdjunto == ".jpg" || extensionAdjunto == ".png" || extensionAdjunto == ".gif" || extensionAdjunto == ".jpeg") {
            respuesta.Clase = "fa fa-file-image-o";
            respuesta.IsImage = true;
            respuesta.Color = "lightgreen";
            respuesta.IsValidExtension = true;
        } else if (extensionAdjunto == ".pdf") {
            respuesta.Clase = "fa fa-file-pdf-o";
            respuesta.Color = "red";
            respuesta.IsValidExtension = true;
        } else if (extensionAdjunto == ".xlsx" || extensionAdjunto == ".xls" || extensionAdjunto == ".xlsm" || extensionAdjunto == ".xlsb") {
            respuesta.Clase = "fa fa-file-excel-o";
            respuesta.Color = "green";
            respuesta.IsValidExtension = false;
        } else if (extensionAdjunto == ".docx" || extensionAdjunto == ".docm" || extensionAdjunto == ".dotx" || extensionAdjunto == ".dotm") {
            respuesta.Clase = "fa fa-file-word-o";
            respuesta.Color = "blue";
            respuesta.IsValidExtension = true;
        } else {
            respuesta.Clase = "fa fa-download";
            respuesta.Color = "blue";
            respuesta.IsValidExtension = false;
        }
    }

    return respuesta;

}
function G_RetornaClaseByUrlBase64(file, isBase64 = null) {

    let extensionAdjunto = "";

    let respuesta = {
        Clase: "",
        Color: "",
        IsImage: false,
        IsPdf: false,
        IsBase64: false,
        IsWord: false, //para validar las extensiones de los adjuntos word
        IsExcel: false //para validar las extensiones de los adjuntos excel
    };

    if (isBase64 != null) {
        if (isBase64) {
            extensionAdjunto = "." + GetBase64Extension(file).toLowerCase();
            respuesta.IsBase64 = true;
        } else { // G_ValidateUrl(file)
            extensionAdjunto = "." + G_ObtenerExtension(file).toLowerCase();
        }
    } else {
        if (G_Base64IsValid(file)) {
            extensionAdjunto = "." + GetBase64Extension(file).toLowerCase();
            respuesta.IsBase64 = true;
        } else { // G_ValidateUrl(file)
            extensionAdjunto = "." + G_ObtenerExtension(file).toLowerCase();
        }
    }

    if (extensionAdjunto == ".jpg" || extensionAdjunto == ".png" || extensionAdjunto == ".gif" || extensionAdjunto == ".jpeg") {
        respuesta.Clase = "fa fa-file-image-o";
        respuesta.IsImage = true;
        respuesta.Color = "lightgreen";
    } else if (extensionAdjunto == ".pdf" || extensionAdjunto == ".-stream") {
        respuesta.Clase = "fa fa-file-pdf-o";
        respuesta.Color = "red";
        respuesta.IsPdf = true;
    } else if (extensionAdjunto == ".xlsx" || extensionAdjunto == ".xls" || extensionAdjunto == ".xlsm" || extensionAdjunto == ".xlsb" || extensionAdjunto == "..sheet") {
        respuesta.Clase = "fa fa-file-excel-o";
        respuesta.Color = "green";
        respuesta.IsValidExtension = true;
        respuesta.IsExcel = true;
    } else if (extensionAdjunto == ".docx" || extensionAdjunto == ".docm" || extensionAdjunto == ".dotx" || extensionAdjunto == ".dotm" || extensionAdjunto == "..document") {
        respuesta.Clase = "fa fa-file-word-o";
        respuesta.Color = "blue";
        respuesta.IsValidExtension = true;
        respuesta.IsWord = true;
    } else {
        respuesta.Clase = "fa fa-download";
        respuesta.Color = "blue";
        respuesta.IsValidExtension = false;
    }

    return respuesta;

}
function G_GenerarEnlaceAdjuntoTabla(file, fileName, isBase64 = null) {
    let rpt = "";
    let Href = "";
    let Class = "";
    let Attrs = "";

    if (ValidaStringIsNull(file) == false) {

        let Obj_Adjunto = G_RetornaClaseByUrlBase64(file, isBase64);

        if (Obj_Adjunto.IsImage) {
            Href = "javascript:;";
            Class = "VerAdjunto";
            Attrs = `data-url-adjunto=${file}`;
        } else {
            Href = file;
        }

        rpt = `<a download="${fileName}"
                  style="font-size: 20px;"
                  href="${Href}"
                  class="${Class}"
                  data-is-base64="${isBase64}"
                  ${Attrs}
                  style="color: ${Obj_Adjunto.Color} !important;font-size: 20px;">
                  <span style="color: ${Obj_Adjunto.Color} !important;font-size: 20px;" class="${Obj_Adjunto.Clase}"></span></a>`;

    }

    return rpt;
}
function G_GenerarEnlaceAdjunto(componente, file, fileName, isBase64 = null) {

    $(componente).empty();
    let rpt = "";
    let Href = "";
    let Class = "";
    let Attrs = "";

    if (ValidaStringIsNull(file) == false) {

        let Obj_Adjunto = G_RetornaClaseByUrlBase64(file, isBase64);

        if (Obj_Adjunto.IsImage) {
            Href = "javascript:;";
            Class = "VerAdjunto";
            Attrs = `data-url-adjunto=${file}`;
        } else {
            Href = file;
        }

        rpt = `<a download="${fileName}"
                  style="font-size: 20px;"
                  href="${Href}"
                  class="${Class}"
                  data-is-base64="${isBase64}"
                  ${Attrs}
                  style="font-size: 20px;color:${Obj_Adjunto.Color}">
                  <span class="${Obj_Adjunto.Clase}"></span></a>`;

    }

    $(componente).append(rpt);
}
function G_GenerarEventoAdjuntoImage() {

    $(document).on("click", ".VerAdjunto", function () {

        let Url_Adjunto = $(this).attr("data-url-adjunto");
        let Url_Is_Base64 = JSON.parse($(this).attr("data-is-base64"));
        let Obj_Adjunto = G_RetornaClaseByUrlBase64(Url_Adjunto, Url_Is_Base64);

        if (ValidaStringIsNull(Url_Adjunto) == false) {

            if (Obj_Adjunto.IsImage == true) {

                if (Obj_Adjunto.IsBase64 == false) {
                    $("#btnImagenDefectoAdjunto").attr('href', `${Url_Adjunto}?${new Date().getTime()}`);
                    $("#btnImagenDefectoImg").attr('src', `${Url_Adjunto}?${new Date().getTime()}`);
                } else {
                    $("#btnImagenDefectoAdjunto").attr('href', `${Url_Adjunto}`);
                    $("#btnImagenDefectoImg").attr('src', `${Url_Adjunto}`);
                }

                document.getElementById("btnImagenDefectoAdjunto").click()

            }
        }
    })
}
function G_GenerarEnlaceAdjunto(componente, file, fileName, isBase64 = null) {
    $(componente).empty();
    let rpt = "";
    let Href = "";
    let Class = "";
    let Attrs = "";

    if (ValidaStringIsNull(file) == false) {
        let Obj_Adjunto = G_RetornaClaseByUrlBase64(file);

        if (Obj_Adjunto.IsImage) {
            Href = "javascript:;";
            Class = "VerAdjunto";
            Attrs = `data-url-adjunto=${file}`;
        } else {
            Href = file;
        }

        rpt = `<a download="${fileName}"
                  style="font-size: 20px;"
                  href="${Href}"
                  class="${Class}"
                  data-is-base64="${isBase64}"
                  ${Attrs}
                  style="font-size: 20px;color:${Obj_Adjunto.Color}">
                  <span class="${Obj_Adjunto.Clase}"></span></a>`;
    }

    $(componente).append(rpt);
}
function G_GenerarEventoAdjuntoImage() {
    $(document).on("click", ".VerAdjunto", function () {

        let Url_Adjunto = $(this).attr("data-url-adjunto");
        let Url_Is_Base64 = JSON.parse($(this).attr("data-is-base64"));
        let Obj_Adjunto = G_RetornaClaseByUrlBase64(Url_Adjunto, Url_Is_Base64);

        if (ValidaStringIsNull(Url_Adjunto) == false) {

            if (Obj_Adjunto.IsImage == true) {

                if (Obj_Adjunto.IsBase64 == false) {
                    $("#btnImagenDefectoAdjunto").attr('href', `${Url_Adjunto}?${new Date().getTime()}`);
                    $("#btnImagenDefectoImg").attr('src', `${Url_Adjunto}?${new Date().getTime()}`);
                } else {
                    $("#btnImagenDefectoAdjunto").attr('href', `${Url_Adjunto}`);
                    $("#btnImagenDefectoImg").attr('src', `${Url_Adjunto}`);
                }

                document.getElementById("btnImagenDefectoAdjunto").click()
            }
        }
    })
}
function G_RetornaClaseByUrlBase64(file, isBase64 = null) {
    let extensionAdjunto = "";

    let respuesta = {
        Clase: "",
        Color: "",
        IsImage: false,
        IsPdf: false,
        IsBase64: false,
        IsWord: false, //para validar las extensiones de los adjuntos word
        IsExcel: false //para validar las extensiones de los adjuntos excel
    };

    if (isBase64 != null) {
        if (isBase64) {
            extensionAdjunto = "." + GetBase64Extension(file).toLowerCase();
            respuesta.IsBase64 = true;
        } else { // G_ValidateUrl(file)
            extensionAdjunto = "." + G_ObtenerExtension(file).toLowerCase();
        }
    } else {
        if (G_Base64IsValid(file)) {
            extensionAdjunto = "." + GetBase64Extension(file).toLowerCase();
            respuesta.IsBase64 = true;
        } else { // G_ValidateUrl(file)
            extensionAdjunto = "." + G_ObtenerExtension(file).toLowerCase();
        }
    }

    if (extensionAdjunto == ".jpg" || extensionAdjunto == ".png" || extensionAdjunto == ".gif" || extensionAdjunto == ".jpeg") {
        respuesta.Clase = "fa fa-file-image-o";
        respuesta.IsImage = true;
        respuesta.Color = "lightgreen";
    } else if (extensionAdjunto == ".pdf") {
        respuesta.Clase = "fa fa-file-pdf-o";
        respuesta.Color = "red";
        respuesta.IsPdf = true;
    } else if (extensionAdjunto == ".xlsx" || extensionAdjunto == ".xls" || extensionAdjunto == ".xlsm" || extensionAdjunto == ".xlsb" || extensionAdjunto == "..sheet") {
        respuesta.Clase = "fa fa-file-excel-o";
        respuesta.Color = "green";
        respuesta.IsValidExtension = true;
        respuesta.IsExcel = true;
    } else if (extensionAdjunto == ".docx" || extensionAdjunto == ".docm" || extensionAdjunto == ".dotx" || extensionAdjunto == ".dotm" || extensionAdjunto == "..document") {
        respuesta.Clase = "fa fa-file-word-o";
        respuesta.Color = "blue";
        respuesta.IsValidExtension = true;
        respuesta.IsWord = true;
    } else {
        respuesta.Clase = "fa fa-download";
        respuesta.Color = "blue";
        respuesta.IsValidExtension = false;
    }

    return respuesta;
}
function GetBase64Extension(encoded) {
    var result = null;

    if (typeof encoded !== 'string') {
        return result;
    }

    result = encoded.match(/[^:/]\w+(?=;|,)/)[0];

    return result;
}
function G_Base64IsValid(encoded) {
    var encodedStringBtoA = btoa(encoded);
    var decodedStringAtoB = atob(encodedStringBtoA);

    let regex = /^([0-9a-zA-Z+/]{4})*(([0-9a-zA-Z+/]{2}==)|([0-9a-zA-Z+/]{3}=))?$/;
    return regex.test(G_Base64GetCode(encoded));
}
function G_Base64GetCode(encoded) {
    let rpt = encoded.split(",");

    try {
        return rpt[1];

    } catch (err) {
        return undefined;
    }
}
function G_ObtenerExtension(filename) {
    filename = ValidaStringIsNull(filename) == true ? "" : filename;
    return filename.split('.').pop().split("?").shift();
}
function ValidaStringIsNull(data) {
    let rpt = false;

    if (data == null || data == undefined || String(data).trim().length == 0) {
        rpt = true;
    }

    return rpt;
}
function EmptyValueIsNull(data) {
    if (ValidaStringIsNull(data)) {
        return "";
    } else {
        return data;
    }
}
function IsValidEmail(valor) {
    if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(valor)) {
        return true;
    } else {
        return false;
    }
}
function G_Clone(source) {
    var result = source, i, len;
    if (!source
        || source instanceof Number
        || source instanceof String
        || source instanceof Boolean) {
        return result;
    } else if (Object.prototype.toString.call(source).slice(8, -1) === 'Array') {
        result = [];
        var resultLen = 0;
        for (i = 0, len = source.length; i < len; i++) {
            result[resultLen++] = G_Clone(source[i]);
        }
    } else if (typeof source == 'object') {
        result = {};
        for (i in source) {
            if (source.hasOwnProperty(i)) {
                result[i] = G_Clone(source[i]);
            }
        }
    }
    return result;
};
function OrdenarArrayPorPropiedad(list, key) {
    function compare(a, b) {
        a = a[key];
        b = b[key];
        var type = (typeof (a) === 'string' ||
            typeof (b) === 'string') ? 'string' : 'number';
        var result;
        if (type === 'string') result = a.localeCompare(b);
        else result = a - b;
        return result;
    }
    return list.sort(compare);
}
function G_FormatoDecimales_Por_Numero_Decimales(numero, decimales) {

    let Rpt = Number(eval(numero));

    if (isNaN(Rpt)) {
        Rpt = 0;
    }

    if (!isFinite(Rpt)) {
        Rpt = 0;
    }

    return parseFloat(Rpt).toFixed(decimales);

}