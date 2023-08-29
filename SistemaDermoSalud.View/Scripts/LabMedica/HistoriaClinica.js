var cabeceras = ["idHistoria", "N° Historia", "Paciente", "Dni", "Fecha Nacimiento", "Edad"];
var cabeceraReceta = ["Medicamento", "Dosis", "Via", "Frecuencia", "Duracion"];
var posiciones = [0, 1, 2, 3];
var listaDatos;
var matriz = [];
var matrizReceta = [];
var NroHistoria;
var FechaActual;
console.log("HistoriaClinica");
var url = "HistoriaClinica/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();
var dbimg64 = "";
$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
$("#txtFechaNacimiento").datetimepicker({
    format: 'DD-MM-YYYY'
});
$("#txtFechaActual").datetimepicker({
    format: 'DD-MM-YYYY'
});
$("#txtFechaUltimaRegla").datetimepicker({
    format: 'DD-MM-YYYY'
});
$("#txtFechaEvolucion").datetimepicker({
    format: 'DD-MM-YYYY'
});
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
    limpiarTodo();
    switch (opcion) {
        case 1:
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nueva Historia Clinica";
            gbi("divBtn_ModalAtencion").style.display = "none";
            gbi("txtHistoria").value = NroHistoria;
            gbi("txtFechaActual").value = FechaActual;
            gbi("txtFechaUltimaRegla").value = FechaActual;
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Historia Clinica";
            gbi("divBtn_ModalAtencion").style.display = "";
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function configurarBotonesModal() {
    var btnModalPaciente = document.getElementById("btnModalPaciente");
    btnModalPaciente.onclick = function () {
        cbmu("paciente", "Paciente", "txtPaciente", null,
            ["idPaciente", "Dni", "Paciente", "Fecha Nacimiento", "Edad", "Sexo"], '/HistoriaClinica/ListaPacientesSinHistoria', cargarLista);
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        //if (validarFormulario()) {
        var url = "HistoriaClinica/Grabar";
        var frm = new FormData();
        frm.append("idHistoria", gbi("txtID").value.length == 0 ? "0" : gbi("txtID").value);
        frm.append("Codigo", gbi("txtHistoria").value.length == 0 ? "0" : gbi("txtHistoria").value);
        frm.append("idPaciente", gbi("txtPaciente").value.length == 0 ? "0" : gbi("txtPaciente").dataset.id);
        frm.append("NombrePaciente", gbi("txtPaciente").value.length == 0 ? " " : gbi("txtPaciente").value);
        frm.append("FechaNacimiento", gbi("txtFechaNacimiento").value.length == 0 ? " " : gbi("txtFechaNacimiento").value);
        frm.append("Edad", gbi("txtEdad").value.length == 0 ? " " : gbi("txtEdad").value);
        frm.append("Dni", gbi("txtDNI").value.length == 0 ? " " : gbi("txtDNI").value);
        frm.append("Sexo", gbi("txtSexo").value.length == 0 ? " " : gbi("txtSexo").value);
        frm.append("FechaActual", gbi("txtFechaActual").value.length == 0 ? " " : gbi("txtFechaActual").value);
        frm.append("AntecedentesFamiliares", gbi("chkAFamiliares").checked);
        frm.append("AF_Padres", gbi("txtPadres").value.length == 0 ? " " : gbi("txtPadres").value);
        frm.append("AF_Padres_Vivos", gbi("txtPadresVivos").value.length == 0 ? " " : gbi("txtPadresVivos").value);
        frm.append("AF_Padres_Fallecidos", gbi("txtPadresFallecidos").value.length == 0 ? " " : gbi("txtPadresFallecidos").value);
        frm.append("AF_Padres_Causas", gbi("txtPadresCausas").value.length == 0 ? " " : gbi("txtPadresCausas").value);
        frm.append("AF_Hermanos", gbi("txtHermanos").value.length == 0 ? " " : gbi("txtHermanos").value);
        frm.append("AF_Hermanos_Vivos", gbi("txtHermanosVivos").value.length == 0 ? " " : gbi("txtHermanosVivos").value);
        frm.append("AF_Hermanos_Fallecidos", gbi("txtHermanosFallecidos").value.length == 0 ? " " : gbi("txtHermanosFallecidos").value);
        frm.append("AF_Hermanos_Causas", gbi("txtHermanosCausas").value.length == 0 ? " " : gbi("txtHermanosCausas").value);
        frm.append("AF_Hijos", gbi("txtHijos").value.length == 0 ? " " : gbi("txtHijos").value);
        frm.append("AF_Hijos_Vivos", gbi("txtHijosVivos").value.length == 0 ? " " : gbi("txtHijosVivos").value);
        frm.append("AF_Hijos_Fallecidos", gbi("txtHijosFallecidos").value.length == 0 ? " " : gbi("txtHijosFallecidos").value);
        frm.append("AF_Hijos_Causas", gbi("txtHijosCausas").value.length == 0 ? " " : gbi("txtHijosCausas").value);
        frm.append("AP_Alcohol", gbi("chkAlcohol").checked);
        frm.append("AP_Tabaco", gbi("chkTabaco").checked);
        frm.append("AP_Drogas", gbi("chkDrogas").checked);
        frm.append("AP_Transfusiones", gbi("chkTransfusion").checked);
        frm.append("P_Diabetis", gbi("chkDiabetis").checked);
        frm.append("P_Hipertension", gbi("chkHipertension").checked);
        frm.append("P_Tubercolosis", gbi("chkTuberculosis").checked);
        frm.append("P_Hepatitis", gbi("chkHepatitis").checked);
        frm.append("P_Vih", gbi("chkVIH").checked);
        frm.append("P_Sifilis", gbi("chkSifilis").checked);
        frm.append("P_Asma", gbi("chkAsma").checked);
        frm.append("P_Gota", gbi("chkGota").checked);
        frm.append("P_Hipercolesterolemia", gbi("chkHipercolesterolemia").checked);
        frm.append("P_Migraña", gbi("chkMigraña").checked);
        frm.append("P_EnfVascular", gbi("chkVascular").checked);
        frm.append("P_EnfVascular_Cuando", gbi("txtVascular").value.length == 0 ? " " : gbi("txtVascular").value);
        frm.append("P_Infartos", gbi("chkInfartos").checked);
        frm.append("P_Infartos_Cuando", gbi("txtInfartos").value.length == 0 ? " " : gbi("txtInfartos").value);
        frm.append("P_Varices", gbi("chkVarices").checked);
        frm.append("P_Varices_Lugar", gbi("txtVarices").value.length == 0 ? " " : gbi("txtVarices").value);
        frm.append("P_Anticoagualantes", gbi("chkAnticoagulantes").checked);
        frm.append("P_Anticoagulantes_Dosis", gbi("txtAnticoagulantes").value.length == 0 ? " " : gbi("txtAnticoagulantes").value);
        frm.append("P_Aspirina", gbi("chkAspirina").checked);
        frm.append("P_Aspirina_Dosis", gbi("txtAspirina").value.length == 0 ? " " : gbi("txtAspirina").value);
        frm.append("P_Anticonceptivos", gbi("chkAnticonceptivos").checked);
        frm.append("P_Anticonceptivos_Dosis", gbi("txtAnticonceptivos").value.length == 0 ? " " : gbi("txtAnticonceptivos").value);
        frm.append("P_Colageno", gbi("chkColageno").checked);
        frm.append("P_Colageno_Cual", gbi("txtColageno").value.length == 0 ? " " : gbi("txtColageno").value);
        frm.append("P_Tiroides", gbi("chkTiroides").checked);
        frm.append("P_Tiroides_Medicacion", gbi("txtTiroides").value.length == 0 ? " " : gbi("txtTiroides").value);
        frm.append("P_Cancer", gbi("chkCancer").checked);
        frm.append("P_Cancer_Lugar", gbi("txtCancer").value.length == 0 ? " " : gbi("txtCancer").value);
        frm.append("P_Mentales", gbi("chkMentales").checked);
        frm.append("P_Mentales_Medicacion", gbi("txtMentales").value.length == 0 ? " " : gbi("txtMentales").value);
        frm.append("P_Convulsiones", gbi("chkConvulsiones").checked);
        frm.append("P_Convulsiones_Medicacion", gbi("txtConvulsiones").value.length == 0 ? " " : gbi("txtConvulsiones").value);
        frm.append("FechaUltimaRegla", gbi("txtFechaUltimaRegla").value.length == 0 ? " " : gbi("txtFechaUltimaRegla").value);
        frm.append("Embarazos", gbi("txtEmbarazos").value.length == 0 ? " " : gbi("txtEmbarazos").value);
        frm.append("AntecedentesQx", gbi("txtAntecedentesQx").value.length == 0 ? " " : gbi("txtAntecedentesQx").value);
        frm.append("Alergias", gbi("txtAlergias").value.length == 0 ? " " : gbi("txtAlergias").value);
        frm.append("MedicacionPaciente", gbi("txtMedicacion").value.length == 0 ? " " : gbi("txtMedicacion").value);
        frm.append("Otros", gbi("txtOtros").value.length == 0 ? " " : gbi("txtOtros").value);
        frm.append("EF_ImpresionGeneral", gbi("txtImpresionGeneral").value.length == 0 ? " " : gbi("txtImpresionGeneral").value);
        frm.append("EF_SignosVitales_FC", gbi("txtFC").value.length == 0 ? " " : gbi("txtFC").value);
        frm.append("EF_SignosVitales_TA", gbi("txtTA").value.length == 0 ? " " : gbi("txtTA").value);
        frm.append("EF_SignosVitales_FR", gbi("txtFR").value.length == 0 ? " " : gbi("txtFR").value);
        frm.append("EF_SignosVitales_Pulso", gbi("txtPulso").value.length == 0 ? " " : gbi("txtPulso").value);
        frm.append("EF_TAuxiliar", gbi("txtTAuxilar").value.length == 0 ? " " : gbi("txtTAuxilar").value);
        frm.append("EF_PesoHabitual", gbi("txtPesoHabitual").value.length == 0 ? " " : gbi("txtPesoHabitual").value);
        frm.append("EF_PesoActual", gbi("txtPesoActual").value.length == 0 ? " " : gbi("txtPesoActual").value);
        frm.append("EF_Talla", gbi("txtTalla").value.length == 0 ? " " : gbi("txtTalla").value);
        frm.append("EF_IMC", gbi("txtIMC").value.length == 0 ? " " : gbi("txtIMC").value);
        frm.append("Nutricion", gbi("chkNutricion").checked);
        frm.append("N_Calorias_Dia", gbi("txtCalorias").value.length == 0 ? " " : gbi("txtCalorias").value);
        frm.append("N_NroVecesAlimentacion", gbi("txtNroAlimentacion").value.length == 0 ? " " : gbi("txtNroAlimentacion").value);
        frm.append("N_PorcentajeGrasa", gbi("txtGrasa").value.length == 0 ? " " : gbi("txtGrasa").value);
        frm.append("N_DietaRegular", gbi("txtDieta").value.length == 0 ? " " : gbi("txtDieta").value);
        frm.append("N_PreferenciasAlimentarias", gbi("txtAlimientarias").value.length == 0 ? " " : gbi("txtAlimientarias").value);
        frm.append("N_Objetivo", gbi("txtObjetivo").value.length == 0 ? " " : gbi("txtObjetivo").value);
        frm.append("ExamenComplementario", gbi("txtExComplementarios").value.length == 0 ? " " : gbi("txtExComplementarios").value);
        frm.append("OtrosEstudios", gbi("txtOtrosEstudios").value.length == 0 ? " " : gbi("txtOtrosEstudios").value);
        frm.append("lista_Archivos", crearTabArchivos());
        enviarServidorPost(url, actualizarListar, frm);
        //}
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }

    gbi("chkVascular").onclick = function () {
        HabilitarChecked("chkVascular", "divVascular");
    }
    gbi("chkInfartos").onclick = function () {
        HabilitarChecked("chkInfartos", "divInfartos");
    }
    gbi("chkVarices").onclick = function () {
        HabilitarChecked("chkVarices", "divVarices");
    }
    gbi("chkAnticoagulantes").onclick = function () {
        HabilitarChecked("chkAnticoagulantes", "divAnticoagulantes");
    }
    gbi("chkAspirina").onclick = function () {
        HabilitarChecked("chkAspirina", "divAspirina");
    }
    gbi("chkAnticonceptivos").onclick = function () {
        HabilitarChecked("chkAnticonceptivos", "divAnticonceptivos");
    }
    gbi("chkColageno").onclick = function () {
        HabilitarChecked("chkColageno", "divColageno");
    }
    gbi("chkTiroides").onclick = function () {
        HabilitarChecked("chkTiroides", "divTiroides");
    }
    gbi("chkCancer").onclick = function () {
        HabilitarChecked("chkCancer", "divCancer");
    }
    gbi("chkMentales").onclick = function () {
        HabilitarChecked("chkMentales", "divMentales");
    }
    gbi("chkConvulsiones").onclick = function () {
        HabilitarChecked("chkConvulsiones", "divConvulsiones");
    }
    gbi("chkNutricion").onclick = function () {
        HabilitarChecked("chkNutricion", "divNutricion");
    }
    gbi("chkAFamiliares").onclick = function () {
        HabilitarChecked("chkAFamiliares", "divAFamiliares");
    }
    gbi("chkCitas").onclick = function () {
        HabilitarChecked("chkCitas", "tablaCitas");
    }

    var inputImage = document.getElementById("inpImage");
    inputImage.onchange = function (evt) {
        var nombre = gvt("inpImage");
        var b = nombre.split('.');
        var tgt = evt.target || window.event.srcElement,
            files = tgt.files;
        if (FileReader && files && files.length) {
            var fr = new FileReader();
            fr.onload = function () {
                var img = gbi("dpzfiles");
                dbimg64 = fr.result;
            }
            fr.readAsDataURL(files[0]);
        }

        if (b.length > 0) {
            addArchivos(0, [], dbimg64);
        }
        gbi("inpImage").value = "";
    }

    var btnAbrir_ModalAtencion = document.getElementById("btnAbrir_ModalAtencion");
    btnAbrir_ModalAtencion.onclick = function () {
        limpiarModalAtencion();
        gbi("tb_DetalleReceta").innerHTML = "";
        gbi("tb_CabeceraReceta").innerHTML = "";
        gbi("tb_DetalleCitas").innerHTML = "";
        var url = "/Atencion_Medica/ListaCitas?idPaciente=" + gbi("txtPaciente").dataset.id;
        enviarServidor(url, mostrarCitas);
        AbrirModal('modalAtencion');
    }
    var btnAgregarReceta = document.getElementById("btnAgregarReceta");
    btnAgregarReceta.onclick = function () {
        if (validarAgregarReceta()) {
            var tablaCabeceraReceta = gbi("tb_CabeceraReceta");
            var cantidadCabeceraReceta = tablaCabeceraReceta.children.length;
            var contNro = 0;
            if (cantidadCabeceraReceta == 0) {
                add_ItemRecetaCabecera(0, []);
            } else {
                for (var i = 0; i < tablaCabeceraReceta.children.length; i++) {
                    if (tablaCabeceraReceta.children[i].children[2].innerText == gbi("txtNroReceta").value)
                        contNro = contNro + 1;
                }
                if (contNro == 0) {
                    add_ItemRecetaCabecera(0, []);
                }
            }
            add_ItemReceta(0, []);
        }
    }
    var btnAgregarEvolucion = document.getElementById("btnAgregarEvolucion");
    btnAgregarEvolucion.onclick = function () {
        if (validarAgregarEvolucion()) {
            add_ItemEvolucion(0, []);
        }
    }
    var btnGrabarAtencionMedica = document.getElementById("btnGrabarAtencionMedica");
    btnGrabarAtencionMedica.onclick = function () {
        let esValido = true;
        if (validarControl("txtMotivoConsulta")) esValido = false;
        if (validarControl("txtCitaAtencion")) esValido = false;
        if (validarControl("txtMedicoAtencion")) esValido = false;


        if (esValido) {
            var url = "Atencion_Medica/Grabar";
            var frm = new FormData();
            frm.append("idAtencionMedica", gbi("txtIDAtencion").value.length == 0 ? "0" : gbi("txtIDAtencion").value);
            frm.append("idCita", gbi("txtIDCita").value.length == 0 ? "0" : gbi("txtIDCita").value);
            frm.append("idPaciente", gbi("txtPaciente").value.length == 0 ? "0" : gbi("txtPaciente").dataset.id);
            frm.append("idPersonal", gbi("txtMedicoAtencion").value.length == 0 ? " " : gbi("txtMedicoAtencion").dataset.id);
            frm.append("Personal", gbi("txtMedicoAtencion").value.length == 0 ? " " : gbi("txtMedicoAtencion").value);
            frm.append("PlanTerapeutico", gbi("txtPlanTerapeutico").value.length == 0 ? " " : gbi("txtPlanTerapeutico").value);
            frm.append("MotivoConsulta", gbi("txtMotivoConsulta").value.length == 0 ? " " : gbi("txtMotivoConsulta").value);
            frm.append("lista_Recetas", crearTabReceta());
            frm.append("lista_Evolucion", crearTabEvolucion());
            //Receta
            frm.append("idReceta", gbi("txtIDReceta").value.length == 0 ? "0" : gbi("txtIDReceta").value);
            frm.append("NroReceta", gbi("txtNroReceta").value.length == 0 ? "0" : gbi("txtNroReceta").value);
            enviarServidorPost(url, actualizarListarAM, frm);
        }
    };
    var btnImprimir = document.getElementById("btnImprimirReceta");
    btnImprimir.onclick = function () {
        var rdetalle = "";
        $(".rowDetReceta").each(function (obj) {
            rdetalle += $(".rowDetReceta")[obj].children[1].innerHTML;//Medicamento
            rdetalle += "|" + $(".rowDetReceta")[obj].children[2].innerHTML;//Dosis
            rdetalle += "|" + $(".rowDetReceta")[obj].children[3].innerHTML;//Via
            rdetalle += "|" + $(".rowDetReceta")[obj].children[4].innerHTML;//Frecuencia
            rdetalle += "|" + $(".rowDetReceta")[obj].children[5].innerHTML;//Duracion
            rdetalle += "¯";
        });
        var lstDetalle = rdetalle.split("¯");
        crearMatrizReceta(lstDetalle);
        ImprimirReceta("p", "Receta Medica", cabeceraReceta, matrizReceta, "Receta Medica", "a4", "i");
    }
    var btnReceta = document.getElementById("btnNuevaReceta");
    btnNuevaReceta.onclick = function () {
        var url = "Atencion_Medica/ObtenerNumeroReceta";
        enviarServidor(url, NumeroReceta);
    }
    var btnCancelarReceta = document.getElementById("btnCancelarReceta");
    btnCancelarReceta.onclick = function () {
        gbi("divRecetas").style.display = "none";
    }
    var btnGrabarReceta = document.getElementById("btnGrabarReceta");
    btnGrabarReceta.onclick = function () {
        var tablaCabeceraReceta = gbi("tb_CabeceraReceta");
        var idReceta;
        for (var i = 0; i < tablaCabeceraReceta.children.length; i++) {
            idReceta = parseInt(tablaCabeceraReceta.children[i].children[0].innerText);
            if (idReceta == 0) {
                var url = "Atencion_Medica/GrabarReceta";
                var frm = new FormData();
                frm.append("idReceta", gbi("txtIDReceta").value.length == 0 ? "0" : gbi("txtIDReceta").value);
                frm.append("idAtencionMedica", gbi("txtIDAtencion").value.length == 0 ? "0" : gbi("txtIDAtencion").value);
                frm.append("NroReceta", gbi("txtNroReceta").value.length == 0 ? "0" : gbi("txtNroReceta").value);
                frm.append("idCita", gbi("txtIDCita").value.length == 0 ? "0" : gbi("txtIDCita").value);
                frm.append("lista_Recetas", crearTabReceta());
                enviarServidorPost(url, actualizarListarReceta, frm);
            }
        }


    };
}
function NumeroReceta(rpta) {
    gbi("divRecetas").style.display = "";
    gbi("tb_DetalleReceta").innerHTML = "";
    gbi("txtNroReceta").value = rpta;
}
function validarAgregarReceta() {
    var error = true;
    if (validarControl("txtMedicamentoReceta")) error = false;
    if (validarControl("txtDosisReceta")) error = false;
    if (validarControl("txtViaReceta")) error = false;
    if (validarControl("txtFrecuenciaReceta")) error = false;
    if (validarControl("txtDuracionReceta")) error = false;
    return error;
}
function validarAgregarEvolucion() {
    var error = true;
    if (validarControl("txtFechaEvolucion")) error = false;
    if (validarControl("txtDescripcionEvolucion")) error = false;
    return error;
}
function mostrarCitas(rpta) {
    if (rpta != "") {
        gbi("txtIDAtencion").innerHTML += "";
        gbi("tb_CabeceraReceta").innerHTML += "";
        gbi("tb_DetalleCitas").innerHTML += "";
        var datos = rpta.split("↔");
        var lCitas = datos[2].split("▼");
        for (var i = 0; i < lCitas.length; i++) {
            var listasCitas = lCitas[i].split("▲");
            add_ItemCita(0, listasCitas);
        }
    }
}
function validarFormulario() {
    var error = true;
    var li = document.querySelectorAll(".vald");
    [].forEach.call(li, function (div) {
        console.log("Entro a validacion");

        if (validarControl(div.id)) { div.style.color = "red"; error = false; }
    });
    return error;
}
function actualizarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {
            if (res == "OK") {
                mensaje = "Se adicionó la Historia Clinica";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó la Historia Clinica";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar();
    }
}
function TraerDetalle(id) {
    var url = "HistoriaClinica/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        gbi("txtID").value = listaDetalle[0];
        gbi("txtHistoria").value = listaDetalle[1];
        gbi("txtPaciente").dataset.id = listaDetalle[2];
        gbi("txtPaciente").value = listaDetalle[3];
        gbi("txtFechaNacimiento").value = listaDetalle[6];
        gbi("txtEdad").value = listaDetalle[4];
        gbi("txtDNI").value = listaDetalle[7];
        gbi("txtSexo").value = listaDetalle[5];
        gbi("txtFechaActual").value = listaDetalle[8];
        gbi("txtMotivoConsulta").value = listaDetalle[9];
        gbi("chkAFamiliares").value = listaDetalle[10].toUpperCase() == "TRUE" ? true : false;
        gbi("txtPadres").value = listaDetalle[11];
        gbi("txtPadresVivos").value = listaDetalle[12];
        gbi("txtPadresFallecidos").value = listaDetalle[13];
        gbi("txtPadresCausas").value = listaDetalle[14];
        gbi("txtHermanos").value = listaDetalle[15];
        gbi("txtHermanosVivos").value = listaDetalle[16];
        gbi("txtHermanosFallecidos").value = listaDetalle[17];
        gbi("txtHermanosCausas").value = listaDetalle[18];
        gbi("txtHijos").value = listaDetalle[19];
        gbi("txtHijosVivos").value = listaDetalle[20];
        gbi("txtHijosFallecidos").value = listaDetalle[21];
        gbi("txtHijosCausas").value = listaDetalle[22];
        gbi("chkAlcohol").value = listaDetalle[23].toUpperCase() == "TRUE" ? true : false;
        gbi("chkTabaco").value = listaDetalle[24].toUpperCase() == "TRUE" ? true : false;
        gbi("chkDrogas").value = listaDetalle[25].toUpperCase() == "TRUE" ? true : false;
        gbi("chkTransfusion").value = listaDetalle[26].toUpperCase() == "TRUE" ? true : false;
        gbi("chkDiabetis").checked = listaDetalle[27].toUpperCase() == "TRUE" ? true : false;
        gbi("chkHipertension").checked = listaDetalle[28].toUpperCase() == "TRUE" ? true : false;
        gbi("chkTuberculosis").checked = listaDetalle[29].toUpperCase() == "TRUE" ? true : false;
        gbi("chkHepatitis").checked = listaDetalle[30].toUpperCase() == "TRUE" ? true : false;
        gbi("chkVIH").checked = listaDetalle[31].toUpperCase() == "TRUE" ? true : false;
        gbi("chkSifilis").checked = listaDetalle[32].toUpperCase() == "TRUE" ? true : false;
        gbi("chkAsma").checked = listaDetalle[33].toUpperCase() == "TRUE" ? true : false;
        gbi("chkGota").checked = listaDetalle[34].toUpperCase() == "TRUE" ? true : false;
        gbi("chkHipercolesterolemia").checked = listaDetalle[35].toUpperCase() == "TRUE" ? true : false;
        gbi("chkMigraña").checked = listaDetalle[36].toUpperCase() == "TRUE" ? true : false;
        gbi("chkVascular").checked = listaDetalle[37].toUpperCase() == "TRUE" ? true : false;
        gbi("txtVascular").value = listaDetalle[38];
        gbi("chkInfartos").checked = listaDetalle[39].toUpperCase() == "TRUE" ? true : false;
        gbi("txtInfartos").value = listaDetalle[40];
        gbi("chkVarices").checked = listaDetalle[41].toUpperCase() == "TRUE" ? true : false;
        gbi("txtVarices").value = listaDetalle[42];
        gbi("chkAnticoagulantes").checked = listaDetalle[43].toUpperCase() == "TRUE" ? true : false;
        gbi("txtAnticoagulantes").value = listaDetalle[44];
        gbi("chkAspirina").checked = listaDetalle[45].toUpperCase() == "TRUE" ? true : false;
        gbi("txtAspirina").value = listaDetalle[46];
        gbi("chkAnticonceptivos").checked = listaDetalle[47].toUpperCase() == "TRUE" ? true : false;
        gbi("txtAnticonceptivos").value = listaDetalle[48];
        gbi("chkColageno").checked = listaDetalle[49].toUpperCase() == "TRUE" ? true : false;
        gbi("txtColageno").value = listaDetalle[50];
        gbi("chkTiroides").checked = listaDetalle[51].toUpperCase() == "TRUE" ? true : false;
        gbi("txtTiroides").value = listaDetalle[52];
        gbi("chkCancer").checked = listaDetalle[53].toUpperCase() == "TRUE" ? true : false;
        gbi("txtCancer").value = listaDetalle[54];
        gbi("chkMentales").checked = listaDetalle[55].toUpperCase() == "TRUE" ? true : false;
        gbi("txtMentales").value = listaDetalle[56];
        gbi("chkConvulsiones").checked = listaDetalle[57].toUpperCase() == "TRUE" ? true : false;
        gbi("txtConvulsiones").value = listaDetalle[58];
        gbi("txtFechaUltimaRegla").value = listaDetalle[59];
        gbi("txtEmbarazos").value = listaDetalle[60];
        gbi("txtAntecedentesQx").value = listaDetalle[61];
        gbi("txtAlergias").value = listaDetalle[62];
        gbi("txtMedicacion").value = listaDetalle[63];
        gbi("txtOtros").value = listaDetalle[64];
        gbi("txtImpresionGeneral").value = listaDetalle[65];
        gbi("txtFC").value = listaDetalle[66];
        gbi("txtTA").value = listaDetalle[67];
        gbi("txtFR").value = listaDetalle[68];
        gbi("txtPulso").value = listaDetalle[69];
        gbi("txtTAuxilar").value = listaDetalle[70];
        gbi("txtPesoHabitual").value = listaDetalle[71];
        gbi("txtPesoActual").value = listaDetalle[72];
        gbi("txtTalla").value = listaDetalle[73];
        gbi("txtIMC").value = listaDetalle[74];
        gbi("chkNutricion").checked = listaDetalle[75].toUpperCase() == "TRUE" ? true : false;
        gbi("txtCalorias").value = listaDetalle[76];
        gbi("txtNroAlimentacion").value = listaDetalle[77];
        gbi("txtGrasa").value = listaDetalle[78];
        gbi("txtDieta").value = listaDetalle[79];
        gbi("txtAlimientarias").value = listaDetalle[80];
        gbi("txtObjetivo").value = listaDetalle[81];
        gbi("txtExComplementarios").value = listaDetalle[82];

        var ListaArchivos = datos[3].split('▼');
        if (ListaArchivos[0] != "") {
            if (ListaArchivos.length >= 1) {
                for (var i = 0; i < ListaArchivos.length; i++) {
                    addArchivos(1, ListaArchivos[i].split("▲"));
                }
            }
        }
        var ListaAtencionMedica = datos[4].split('▼');
        if (ListaAtencionMedica[0] != "") {
            if (ListaAtencionMedica.length >= 1) {
                for (var i = 0; i < ListaAtencionMedica.length; i++) {
                    add_ItemAtencionMedica(1, ListaAtencionMedica[i].split("▲"));
                }
            }
        }
    }
}
function limpiarTodo() {
    limpiarControl("txtID");
    limpiarControl("txtHistoria");
    limpiarControl("txtPaciente");
    limpiarControl("txtFechaNacimiento");
    limpiarControl("txtEdad");
    limpiarControl("txtDNI");
    limpiarControl("txtSexo");
    limpiarControl("txtFechaActual");
    limpiarControl("txtMotivoConsulta");
    limpiarControl("txtPadres");
    limpiarControl("txtPadresVivos");
    limpiarControl("txtPadresFallecidos");
    limpiarControl("txtPadresCausas");
    limpiarControl("txtHermanos");
    limpiarControl("txtHermanosVivos");
    limpiarControl("txtHermanosFallecidos");
    limpiarControl("txtHermanosCausas");
    limpiarControl("txtHijos");
    limpiarControl("txtHijosVivos");
    limpiarControl("txtHijosFallecidos");
    limpiarControl("txtHijosCausas");
    limpiarControl("txtAnticoagulantes");
    limpiarControl("txtAspirina");
    limpiarControl("txtAnticonceptivos");
    limpiarControl("txtColageno");
    limpiarControl("txtCancer");
    limpiarControl("txtMentales");
    limpiarControl("txtConvulsiones");
    limpiarControl("txtFechaUltimaRegla");
    limpiarControl("txtEmbarazos");
    limpiarControl("txtAntecedentesQx");
    limpiarControl("txtAlergias");
    limpiarControl("txtMedicacion");
    limpiarControl("txtOtros");
    limpiarControl("txtImpresionGeneral");
    limpiarControl("txtFC");
    limpiarControl("txtTA");
    limpiarControl("txtFR");
    limpiarControl("txtPulso");
    limpiarControl("txtTAuxilar");
    limpiarControl("txtPesoHabitual");
    limpiarControl("txtPesoActual");
    limpiarControl("txtTalla");
    limpiarControl("txtIMC");
    limpiarControl("txtCalorias");
    limpiarControl("txtNroAlimentacion");
    limpiarControl("txtGrasa");
    limpiarControl("txtDieta");
    limpiarControl("txtAlimientarias");
    limpiarControl("txtObjetivo");
    limpiarControl("txtExComplementarios");
    limpiarControl("txtOtrosEstudios");

    gbi("chkAlcohol").checked = false;
    gbi("chkTabaco").checked = false;
    gbi("chkDrogas").checked = false;
    gbi("chkTransfusion").checked = false;
    gbi("chkMentales").checked = false;
    gbi("chkConvulsiones").checked = false;
    gbi("chkCancer").checked = false;
    gbi("chkTiroides").checked = false;
    gbi("chkColageno").checked = false;
    gbi("chkAnticonceptivos").checked = false;
    gbi("chkAspirina").checked = false;
    gbi("chkTuberculosis").checked = false;
    gbi("chkHepatitis").checked = false;
    gbi("chkVIH").checked = false;
    gbi("chkSifilis").checked = false;
    gbi("chkAsma").checked = false;
    gbi("chkGota").checked = false;
    gbi("chkHipercolesterolemia").checked = false;
    gbi("chkMigraña").checked = false;
    gbi("chkVascular").checked = false;
    gbi("txtVascular").checked = false;
    gbi("chkInfartos").checked = false;
    gbi("txtInfartos").checked = false;
    gbi("chkVarices").checked = false;
    gbi("txtVarices").checked = false;
    gbi("chkAnticoagulantes").checked = false;
    gbi("chkDiabetis").checked = false;
    gbi("chkHipertension").checked = false;
    gbi("chkNutricion").checked = false;
    gbi("tb_DetalleArchivos").innerHTML = "";
    gbi("tb_DetalleAtencionMedica").innerHTML = "";
}
function limpiarModalAtencion() {
    limpiarControl("txtIDAtencion");
    limpiarControl("txtCitaAtencion");
    limpiarControl("txtMedicoAtencion");
    limpiarControl("txtPlanTerapeutico");
    limpiarControl("txtMedicamentoReceta");
    limpiarControl("txtDosisReceta");
    limpiarControl("txtViaReceta");
    limpiarControl("txtFrecuenciaReceta");
    limpiarControl("txtDuracionReceta");
    limpiarControl("txtFechaEvolucion");
    limpiarControl("txtDescripcionEvolucion");
    gbi("tb_DetalleReceta").innerHTML = "";
    gbi("tb_HC_Evolucion").innerHTML = "";
    gbi("tb_DetalleCitas").innerHTML = "";

    gbi("btnNuevaReceta").disabled = true;
    gbi("btnGrabarReceta").style.display = "none";
    gbi("btnImprimirReceta").style.display = "none";
}
function eliminar(id) {
    //if (confirm("¿Está seguro que desea eliminar?") == false) return false;
    swal({
        title: "Desea Eliminar este Medicamento?",
        text: "No se podrá recuperar los datos eliminados.",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Si, Eliminar",
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "Medicamento/Eliminar?idMedicamentos=" + id;
                enviarServidor(url, eliminarListar);
            } else {
                swal("Cancelado", "No se elminó al Personal", "error");
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        if (res == "OK") {
            mensaje = "Se eliminó al Medicamento";
            tipo = "success";
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
    } else {
        mensaje = "No hubo respuesta";
    }
    mostrarRespuesta(res, mensaje, tipo);
    listaDatos = data[2].split("▼");
    listar();
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtPaciente":
            return gbi("txtPersonal");
            break;
        case "txtFormaPago":
            return gbi("txtCategoria");
            break;
        default:
            break;
    }
}
function adt(v, ctrl) {
    gbi(ctrl).value = v;
}
function cbm(ds, t, tM, tM2, cab, dat, m) {
    comboConcepto = "";
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
    m(dat);
    if (ds == "conceptoCaja") {
        comboConcepto = ds;
    } else {
        combox = ds;
    }

}
//
function crearTablaHC(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-3'>";
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
function mostrarMatriz(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 2:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 5:
                            contenido2 += "col-md-1' style='padding-top:5px;'>";
                            break;
                        default:
                            contenido2 += "col-md-2' style='padding-top:5px;'>";
                            break;
                    }
                    contenido2 += "<span class='d-sm-none'>" + cabeceras[j] + " : </span><span id='tp" + i + "-" + j + "'>" + matriz[i][j] + "</span>";
                    contenido2 += "</div>";
                }
                contenido2 += "<div class='col-12 col-md-2'>";
                contenido2 += "<div class='row saltbtn'>";
                contenido2 += "<div class='col-12'>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o fs-11'></i> </button>";
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil fs-11'></i></button>";
                contenido2 += "</div>";
                contenido2 += "</div>";
                contenido2 += "</div>";
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
}
//Carga con botones de Modal desde URL
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
        case "paciente": gbi("txtFechaNacimiento").value = gbi("md" + num + "-3").innerHTML;
            gbi("txtEdad").value = gbi("md" + num + "-4").innerHTML; gbi("txtDNI").value = gbi("md" + num + "-1").innerHTML;
            gbi("txtSexo").value = gbi("md" + num + "-5").innerHTML;
        case "cita": gbi("txtCitaAtencion").value = gbi("md" + num + "-1").innerHTML;
            gbi("txtMedicoAtencion").value = gbi("md" + num + "-3").innerHTML;
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

//Matriz Modal                    
function crearTablaModal(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0: case 4: case 5:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-4'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-3'>";
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
                            case 0: case 4: case 5:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 2:
                                contenido2 += ("<div class='col-12 col-md-4'>");
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-3'>");
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
function HabilitarChecked(ch, div) {
    var w = gbi(ch).checked;
    if (w == true) {
        gbi(div).style.display = "";
    } else {
        gbi(div).style.display = "none";
    }
}
//ARCHIVOS
function addArchivos(tipo, data, dbimg) {
    var contenido = "";
    var nombre = gvt("inpImage");
    var b = nombre.split('.');
    contenido += '<div class="row rowDet" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + '" style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-right: 0px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[1] : '0') + '</div>';
    contenido += '  <div class="col-md-2" data-id="' + (tipo == 1 ? data[3] : FechaActual) + '">' + (tipo == 1 ? data[3] : FechaActual) + '</div>';
    contenido += '  <div class="col-md-4" data-id="' + (tipo == 1 ? data[2] : b[0]) + '">' + (tipo == 1 ? data[2] : gbi("inpImage").files[0].name) + '</div>';
    contenido += '  <div class="col-md-1" data-id="' + (tipo == 1 ? data[4] : b[1]) + '">' + (tipo == 1 ? data[4] : b[1]) + '</div>';
    contenido += '  <div class="col-md-2" data-id="' + (tipo == 1 ? data[5] : dbimg) + '">' + '<a target="_blank" href="HistoriaClinica/Download?iHC=' + data[0] + '">Descargar</a></div>';
    contenido += '  <div class="col-md-1">';
    contenido += '      <div class="row rowDetbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalle(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '  <div class="col-sm-1"></div>';
    contenido += '</div>';
    gbi("tb_DetalleArchivos").innerHTML += contenido;
}
function crearTabArchivos() {
    var cdet = "";
    $(".rowDet").each(function (obj) {
        cdet += $(".rowDet")[obj].children[0].innerHTML;//idhistoriaArchivo
        cdet += "|" + $(".rowDet")[obj].children[1].innerHTML;//idHistoria
        cdet += "|" + $(".rowDet")[obj].children[3].innerHTML;//NombreArchivo
        cdet += "|" + $(".rowDet")[obj].children[2].innerHTML;//fecha
        cdet += "|" + $(".rowDet")[obj].children[4].dataset.id;//extension
        cdet += "|" + $(".rowDet")[obj].children[5].dataset.id;//archivo 
        cdet += "|01-01-2000|1";
        cdet += "¯";
    });
    return cdet;
}

//ATENCION MEDICA
function actualizarListarAM(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {
            if (res == "OK") {
                mensaje = "Se adicionó la Atención Medica";
                tipo = "success";
                CerrarModal("modal-form");

                gbi("tb_DetalleAtencionMedica").innerHTML = "";
                var ListaAtencionMedica = data[2].split('▼');
                if (ListaAtencionMedica.length > 0 && ListaAtencionMedica[0] != "") {
                    for (var i = 0; i < ListaAtencionMedica.length; i++) {
                        add_ItemAtencionMedica(1, ListaAtencionMedica[i].split("▲"));
                    }
                }
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se actualizó la Atención Medica";
                tipo = "success";
                CerrarModal("modal-form");

                gbi("tb_DetalleAtencionMedica").innerHTML = "";
                var ListaAtencionMedica = data[2].split('▼');
                if (ListaAtencionMedica.length > 0 && ListaAtencionMedica[0] != "") {
                    for (var i = 0; i < ListaAtencionMedica.length; i++) {
                        add_ItemAtencionMedica(1, ListaAtencionMedica[i].split("▲"));
                    }
                }
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { CerrarModal('modalAtencion') }
        mostrarRespuesta(res, mensaje, tipo);
        //listaDatos = data[2].split("▼");
        //listar();
    }
}
function add_ItemAtencionMedica(tipo, data) {
    if (data[0] != "") {
        var medico = data[3];
        var contenido = "";
        contenido += "<div class='row rowDetAM' id='gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDetAM").length + 1) + "' style='margin-bottom:2px;padding:0px 20px;padding-top:2px;margin-right: 0px;'>";
        contenido += "  <div class='col-md-4' style='display:none;'>" + data[0] + "</div>";
        contenido += "  <div class='col-md-1'><button type='button' id='btnCitaAdd'  onclick='MostrarAM(\"" + data[0] + "\")' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10 ft-eye' style='padding:4px 8px;'></div>";
        contenido += "  <div class='col-md-2'>" + data[1] + "</div>";
        contenido += "  <div class='col-md-4'>" + data[2] + "</div>";
        contenido += "  <div class='col-md-5' id=" + (tipo == 1 ? data[4] : data[4]) + " >" + data[3] + "</div>";
        contenido += "</div>";
        gbi("tb_DetalleAtencionMedica").innerHTML += contenido;
    } else {
        gbi("tb_DetalleAtencionMedica").innerHTML += "";
    }
}
function MostrarAM(id) {
    gbi("tb_DetalleReceta").innerHTML = "";
    gbi("tb_HC_Evolucion").innerHTML = "";
    gbi("tb_CabeceraReceta").innerHTML = "";

    gbi("btnNuevaReceta").disabled = false;
    gbi("btnGrabarReceta").style.display = "";
    gbi("btnImprimirReceta").style.display = "";

    var url = "Atencion_Medica/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetallesAM);
}
function CargarDetallesAM(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var listaDetalle = datos[2].split("▲");
        gbi("txtIDAtencion").value = listaDetalle[0];
        gbi("txtCitaAtencion").value = listaDetalle[1];
        gbi("txtMedicoAtencion").dataset.id = listaDetalle[2];
        gbi("txtMedicoAtencion").value = listaDetalle[3];
        gbi("txtPlanTerapeutico").value = listaDetalle[4];
        gbi("txtIDCita").value = listaDetalle[6];
        var ListaReceta = datos[3].split('▼');
        if (ListaReceta[0] != "") {
            if (ListaReceta.length >= 1) {
                for (var i = 0; i < ListaReceta.length; i++) {
                    add_ItemRecetaCabecera(1, ListaReceta[i].split("▲"));
                }
            }
        }
        var ListaEvolucion = datos[4].split('▼');
        if (ListaEvolucion[0] != "") {
            if (ListaEvolucion.length >= 1) {
                for (var i = 0; i < ListaEvolucion.length; i++) {
                    add_ItemEvolucion(1, ListaEvolucion[i].split("▲"));
                }
            }
        }
        AbrirModal('modalAtencion');
    }
}
//CITAS
function add_ItemCita(tipo, data) {
    if (data[0] != "") {
        var medico = data[3];
        var contenido = "";
        contenido += "<div class='row rowDet' id='gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDet").length + 1) + "' style='margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-right: 0px;'>";
        contenido += "  <div class='col-md-4' style='display:none;'>" + data[0] + "</div>";
        contenido += "  <div class='col-md-1'><button type='button' id='btnCitaAdd'  onclick='MostrarCitaAM(\"" + data[1] + "\"," + "\"" + medico + "\"," + "\"" + data[0] + "\"," + "\"" + data[4] + "\")' class='btn btn-sm waves-effect waves-light btn-info pull-right m-l-10 ft-check-square' style='padding:5px 5px;'></div>";
        contenido += "  <div class='col-md-2'>" + data[1] + "</div>";
        contenido += "  <div class='col-md-2'>" + data[2] + "</div>";
        contenido += "  <div class='col-md-4' id=" + (tipo == 1 ? data[4] : data[4]) + " >" + data[3] + "</div>";
        contenido += "</div>";
        gbi("tb_DetalleCitas").innerHTML += contenido;
    } else {
        gbi("tb_DetalleCitas").innerHTML += "";
    }

}
function MostrarCitaAM(cita, medico, idCita, idPersonal) {
    gbi("chkCitas").checked = false;
    gbi("tablaCitas").style.display = "none";
    gbi("txtIDCita").value = idCita;
    gbi("txtCitaAtencion").value = cita;
    gbi("txtMedicoAtencion").value = medico;
    gbi("txtMedicoAtencion").dataset.id = idPersonal;

}
//RECETAS
function Grabar_Receta() {
    var url = "Atencion_Medica/GrabarReceta";
    var frm = new FormData();
    frm.append("idReceta", gbi("txtIDReceta").value.length == 0 ? "0" : gbi("txtIDReceta").value);
    frm.append("idAtencionMedica", gbi("txtIDAtencion").value.length == 0 ? "0" : gbi("txtIDAtencion").value);
    frm.append("NroReceta", gbi("txtNroReceta").value.length == 0 ? "0" : gbi("txtNroReceta").value);
    frm.append("idCita", gbi("txtIDCita").value.length == 0 ? "0" : gbi("txtIDCita").value);
    frm.append("lista_Recetas", crearTabReceta());
    enviarServidorPost(url, actualizarListarReceta, frm);
}
function add_ItemRecetaCabecera(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowCabReceta" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDetReceta").length + 1) + '" style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-right: 0px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-3">' + (tipo == 1 ? data[1] : FechaActual) + '</div>';
    contenido += '  <div class="col-md-3">' + (tipo == 1 ? data[2] : gbi("txtNroReceta").value) + '</div>';
    contenido += '  <div class="col-md-3" style="display:none">' + (tipo == 1 ? data[3] : gbi("txtIDAtencion").value) + '</div>';
    contenido += '  <div class="col-md-2">';
    contenido += '      <div class="row rowCabRecetabtn">';
    contenido += '          <div class="col-xs-12"><button type="button" onclick="ObtenerReceta(' + (tipo == 1 ? data[0] : 0) + ');" class="btn btn-sm waves-effect waves-light btn-info pull-right m-l-10" style="padding:2px 10px;" > <i class="fa fa-eye"></i> </button>';
    contenido += '          </div>';
    contenido += '          <div class="col-xs-12">';
    contenido += "            " + (tipo == 1 ? "" : "<button type='button' onclick='borrarCabeceraReceta(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>");
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '</div>';
    gbi("tb_CabeceraReceta").innerHTML += contenido;
    //limpiarReceta();
}
function add_ItemReceta(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDetReceta" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDetReceta").length + 1) + '" style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-right: 0px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-3">' + (tipo == 1 ? data[1] : gbi("txtMedicamentoReceta").value.trim()) + '</div>';
    contenido += '  <div class="col-md-2">' + (tipo == 1 ? data[2] : gbi("txtDosisReceta").value) + '</div>';
    contenido += '  <div class="col-md-2">' + (tipo == 1 ? data[3] : gbi("txtViaReceta").value) + '</div>';
    contenido += '  <div class="col-md-2">' + (tipo == 1 ? data[4] : gbi("txtFrecuenciaReceta").value) + '</div>';
    contenido += '  <div class="col-md-2">' + (tipo == 1 ? data[5] : gbi("txtDuracionReceta").value) + '</div>';
    contenido += '  <div class="col-md-2" style="display:none;">' + (tipo == 1 ? data[6] : "0") + '</div>';
    contenido += '  <div class="col-md-1">';
    contenido += '      <div class="row rowDetRecetabtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "            " + (tipo == 1 ? "" : "<button type='button' onclick='borrarDetalleReceta(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>");
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '</div>';
    gbi("tb_DetalleReceta").innerHTML += contenido;
    limpiarReceta();
}
function borrarDetalleReceta(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function limpiarReceta() {
    limpiarControl("txtMedicamentoReceta");
    limpiarControl("txtDosisReceta");
    limpiarControl("txtViaReceta");
    limpiarControl("txtFrecuenciaReceta");
    limpiarControl("txtDuracionReceta");
}
function crearTabReceta() {
    var cdet = "";
    $(".rowDetReceta").each(function (obj) {
        cdet += $(".rowDetReceta")[obj].children[0].innerHTML;//idReceta
        cdet += "|" + gbi("txtIDCita").value;
        cdet += "|" + $(".rowDetReceta")[obj].children[6].innerHTML;//"|0";
        cdet += "|" + $(".rowDetReceta")[obj].children[1].innerHTML;//Medicamento
        cdet += "|" + $(".rowDetReceta")[obj].children[2].innerHTML;//Dosis
        cdet += "|" + $(".rowDetReceta")[obj].children[3].innerHTML;//Via
        cdet += "|" + $(".rowDetReceta")[obj].children[4].innerHTML;//Frecuencia
        cdet += "|" + $(".rowDetReceta")[obj].children[5].innerHTML;//Duracion
        cdet += "|01-01-2000|01-01-2000|1|1|1|0";
        cdet += "¯";
    });
    return cdet;
}
function actualizarListarReceta(rpta) {
    if (rpta != "") {
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {
            if (res == "OK") {
                mensaje = "Se guardo la Receta";
                tipo = "success";
                CerrarModal("modal-form");

                let boton = gbi("tb_CabeceraReceta").lastChild.querySelector("button");
                if (boton) {
                    boton.outerHTML = '<button type="button" onclick="ObtenerReceta(' + data[3] + ');" class="btn btn-sm waves-effect waves-light btn-info pull-right m-l-10" style="padding:2px 10px;" > <i class="fa fa-eye"></i> </button>';
                }

                let listaBotones = gbi("tb_CabeceraReceta").lastChild.querySelectorAll("button");
                listaBotones[listaBotones.length - 1].remove();

                for (let item of gbi("tb_DetalleReceta").querySelectorAll("button")) {
                    item.remove();
                }
                gbi("btnCancelarReceta").onclick();
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {
            if (res == "OK") {
                mensaje = "Se guardo la Receta";
                tipo = "success";
                CerrarModal("modal-form");

                let boton = gbi("tb_CabeceraReceta").lastChild.querySelector("button");
                if (boton) {
                    boton.outerHTML = '<button type="button" onclick="ObtenerReceta(' + data[3] + ');" class="btn btn-sm waves-effect waves-light btn-info pull-right m-l-10" style="padding:2px 10px;" > <i class="fa fa-eye"></i> </button>';
                }

                let listaBotones = gbi("tb_CabeceraReceta").lastChild.querySelectorAll("button");
                listaBotones[listaBotones.length - 1].remove();

                for (let item of gbi("tb_DetalleReceta").querySelectorAll("button")) {
                    item.remove();
                }
                gbi("btnCancelarReceta").onclick();
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        ////if (res == "OK") { CerrarModal('modalAtencion') }
        mostrarRespuesta(res, mensaje, tipo);

    }
}
function crearMatrizReceta(listaDatosReceta) {
    var nRegistros = listaDatosReceta.length;
    var nCampos;
    var campos;
    var c = 0;
    var textos = document.getElementById("txtFiltro").value.trim();
    matrizReceta = [];
    var exito;
    if (listaDatosReceta != "") {

        for (var i = 0; i < nRegistros; i++) {
            campos = listaDatosReceta[i].split("|");
            nCampos = campos.length;
            exito = true;
            if (textos.trim() != "") {
                for (var l = 1; l < nCampos; l++) {
                    exito = true;
                    exito = exito && campos[l].toLowerCase().indexOf(textos.toLowerCase()) != -1;
                    if (exito) break;
                }
            }
            if (exito) {
                matrizReceta[c] = [];
                for (var j = 0; j < nCampos; j++) {
                    matrizReceta[c][j] = campos[j];
                }
                c++;
            }
        }
    } else {
        document.getElementById("contentPrincipal").innerHTML = "";
    }
    return matrizReceta;
}
function ImprimirReceta(orientation, titulo, cabecerasReceta, matrizReceta, nombre, tipo, v) {
    var texto = "";
    var columns = [];
    for (var i = 0; i < cabecerasReceta.length; i++) {
        columns[i] = cabecerasReceta[i];
    }

    var data = [];
    for (var i = 0; i < matrizReceta.length; i++) {
        data[i] = [];
        for (var j = 0; j < matrizReceta[i].length; j++) {
            data[i][j] = matrizReceta[i][j];
        }
    }
    var doc = new jsPDF(orientation, 'pt', (tipo == undefined ? "a5" : "a5"));
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    var fec = new Date();
    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
    var y = fec.getFullYear();

    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
    doc.setFont('helvetica')
    doc.setFontSize(14);
    doc.text(titulo, width / 2 - 80, 95);
    doc.line(30, 125, width - 30, 125);
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 90, 40)
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50)

    doc.autoTable(columns, data, {
        theme: 'plain',
        startY: 110, showHeader: 'firstPage',
        headerStyles: { styles: { overflow: 'linebreak', halign: 'center' }, fontSize: 9, },
        bodyStyles: { fontSize: 8, valign: 'middle', cellPadding: 2, columnWidt: 'wrap' },
        columnStyles: {},

    });
    if (v == "e") {
        doc.save((nombre != undefined ? nombre : "table.pdf"));
    }
    else if (v == "i") {
        doc.autoPrint();
        var iframe = document.getElementById('iframePDF');
        iframe.src = doc.output('dataurlstring');
    }
}
function ObtenerReceta(id) {
    gbi("tb_DetalleReceta").innerHTML = "";
    var url = "Atencion_Medica/ObtenerRecetaxID/?id=" + id;
    enviarServidor(url, CargarDetallesReceta);
}
function CargarDetallesReceta(rpta) {
    if (rpta != "") {
        var datos = rpta.split("↔");
        var ListaRecetaDetalle = datos[2].split('▼');
        if (ListaRecetaDetalle[0] != "") {
            if (ListaRecetaDetalle.length >= 1) {
                for (var i = 0; i < ListaRecetaDetalle.length; i++) {
                    add_ItemReceta(1, ListaRecetaDetalle[i].split("▲"));
                }
            }
        }
    }
}
// EVOLUCION
function add_ItemEvolucion(tipo, data) {
    var contenido = "";
    contenido += '<div class="row rowDetEvolucion" id="gd' + (tipo == 1 ? data[0] : document.getElementsByClassName("rowDetEvolucion").length + 1) + '" style="margin-bottom:2px;padding:0px 20px;padding-top:4px;margin-right: 0px;">';
    contenido += '  <div class="col-md-1" style="display:none;">' + (tipo == 1 ? data[0] : '0') + '</div>';
    contenido += '  <div class="col-md-2">' + (tipo == 1 ? data[1] : gbi("txtFechaEvolucion").value) + '</div>';
    contenido += '  <div class="col-md-8">' + (tipo == 1 ? data[2] : gbi("txtDescripcionEvolucion").value) + '</div>';
    contenido += '  <div class="col-md-2" style="display:none;">' + (tipo == 1 ? data[3] : "0") + '</div>';
    contenido += '  <div class="col-md-1">';
    contenido += '      <div class="row rowDetEvolucionbtn">';
    contenido += '          <div class="col-xs-12">';
    contenido += "              <button type='button' onclick='borrarDetalleEvolucion(this);' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:2px 10px;' > <i class='fa fa-trash-o'></i> </button>";
    contenido += '          </div>';
    contenido += '      </div>';
    contenido += '  </div>';
    contenido += '</div>';
    gbi("tb_HC_Evolucion").innerHTML += contenido;
    limpiarEvolucion();
}
function borrarDetalleEvolucion(elem) {
    var p = elem.parentNode.parentNode.parentNode.parentNode.remove();
}
function limpiarEvolucion() {
    limpiarControl("txtFechaEvolucion");
    limpiarControl("txtDescripcionEvolucion");
}
function crearTabEvolucion() {

    var cdet = "";
    $(".rowDetEvolucion").each(function (obj) {
        var descripcionEvolucion = $(".rowDetEvolucion")[obj].children[2].innerHTML
        cdet += $(".rowDetEvolucion")[obj].children[0].innerHTML;//idevolucion
        cdet += "|" + gbi("txtIDCita").value;
        cdet += "|" + $(".rowDetEvolucion")[obj].children[3].innerHTML;//fecha//"|0";
        cdet += "|" + $(".rowDetEvolucion")[obj].children[1].innerHTML;//fecha
        cdet += "|" + descripcionEvolucion;//Descripcion
        cdet += "|01-01-2000|01-01-2000|1|1|1";
        cdet += "¯";
    });
    return cdet;
}

function borrarCabeceraReceta(elemento) {
    gbi("tb_DetalleReceta").innerHTML = "";
    elemento.parentElement.parentElement.parentElement.parentElement.remove();
    gbi("btnCancelarReceta").onclick();
}