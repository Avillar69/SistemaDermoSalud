var cabeceras = ["idHistoria", "N° Historia", "Paciente", "Dni", "Fecha Nacimiento", "Edad"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var matriz = [];
var NroHistoria;
var FechaActual;
var url = "Atencion_Medica/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();

function mostrarLista(rpta) {
    crearTablaHC(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatos = listas[2].split("▼");
            NroHistoria = listas[3];
            FechaActual = listas[4];
            listar();
        }
    }
}
function listar() {
    configurarFiltro();
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    //limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nueva Atencion Medica";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Atencion Medica";
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function configurarBotonesModal() {
    var btnModalCita = gbi("btnModalCita");
    btnModalCita.onclick = function () {
        cbmu("cita", "Citas", "txtCita", null,
           ["idCita", "NroCita", "Fecha", "Paciente", "Medico"], '/Atencion_Medica/ListaCitas', cargarLista);
    }
}

function cbmu(ds, t, tM, tM2, cab, u, m) {
    document.getElementById("div_Frm_Modals").innerHTML = document.getElementById("div_Frm_Detalles").innerHTML;
    document.getElementById("btnGrabar_Modal").dataset.grabar = ds;
    document.getElementById("lblTituloModal").innerHTML = t;
    var txtCodigo_FormaPago = document.getElementById("txtCodigo_Detalle");
    txtCodigo_FormaPago.disabled = true;
    txtCodigo_FormaPago.placeholder = "Autogenerado";
    var txtM1 = document.getElementById(tM);
    txtModal = txtM1;
    tM2 == null ? txtModal2 = tM2 : txtModal2 = document.getElementById(tM2);
    cabecera_Modal = cab;
    enviarServidor(u, m);
    combox = ds;
}
function funcionModal(tr) {
    var num = tr.id.replace("numMod", "");
    var id = gbi("md" + num + "-0").innerHTML;
    if (tr.children.length == 2) {
        var value = gbi("md" + num + "-1").innerHTML;
    } else {

        var value = gbi("md" + num + "-2").innerHTML;
    }

    var value2 = gbi("md" + num + "-1").innerHTML;
    //var azx = gbi("md" + num + "-3").innerHTML;
    txtModal.value = value;
    txtModal.dataset.id = id;

    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";

    switch (combox) {
        case "cita": gbi("txtCita").value = gbi("md" + num + "-1").innerHTML;
            gbi("txtMedico").value = gbi("md" + num + "-4").innerHTML; gbi("txtPaciente").value = gbi("md" + num + "-3").innerHTML;
            gbi("txtEdad").value = gbi("md" + num + "-2").innerHTML;
            break;
    }

}
function CerrarModal(idModal, te) {
    ventanaActual = 1;
    $('#' + idModal).modal('hide');
    if (te) {
        te.focus();
    }
}
function cargarModal(id, TM, valor) {
    var txtText = document.getElementById(TM);
    txtText.value = valor;
    txtText.dataset.id = id;
    conceptoId = id;
    Conceptos();
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtCita":
            return gbi("txtMotivoConsulta");
            break;
        case "txtMedico":
            return gbi("txtMotivoConsulta");
            break;
        default:
            break;
    }
}
//Matriz Modal                    
function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 3: case 4:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
        }
        contenido += "                  <label>" + cabeceras[i] + "</label>";
        contenido += "              </div>";
    }
    contenido += "          </div>";

    var divTabla = gbi(div);
    divTabla.innerHTML = contenido;
}
function mostrarMatrizModal(matriz, cabeceras, tabId, contentID, confdbc) {
    if (matriz.length == 0) {

    } else {
        var esBloque = false;
        var contenido = "";
        var nRegistros = matriz.length;
        if (nRegistros > 0) {
            nRegistros = matriz.length;
            var nCampos = matriz[0].length;
            var tbTabla = document.getElementById(tabId);
            tbTabla.style.cursor = "pointer";
            var tipocol = "";
            var tipoColDes = "";
            var dat = [];
            for (var i = 0; i < nRegistros; i++) {
                if (i < nRegistros) {
                    var contenido2 = "<div class='row panel salt' id='numMod" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:1px;cursor:pointer;'>";
                    for (var j = 0; j < cabeceras.length; j++) {
                        switch (j) {
                            case 0: 
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 3: case 4:
                                contenido2 += ("<div class='col-12 col-md-4'>");
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-2'>");
                                break;
                        }
                        contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='md" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                        contenido2 += "</div>";
                    }
                    contenido2 += "</div>";
                    dat.push(contenido2);
                }
                else break;
            }
            var clusterize = new Clusterize({
                rows: dat,
                scrollId: tabId,
                contentId: contentID
            });
        }
        confdbc();
    }
}