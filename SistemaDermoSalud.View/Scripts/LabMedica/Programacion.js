var evt = [];
var idRol;
var CalFecha;
var listaPacientes;
var cabeceras = ["idCita", "Fecha", "Medico", "Tratamiento"];
var listaDatosBusqueda;
var url = "Citas/ObtenerEventos";
var OpcionCambio = 1;
enviarServidor(url, mostrarLista);

$("#txtFechaCita").datetimepicker({
    format: 'DD-MM-YYYY',

});
$(".datepicker").datetimepicker({
    inline: true,
    format: 'dd/MM/yyyy'
})
function configurarBotones() {
    var btnPersonal = document.getElementById("btnModalPersonal");
    btnPersonal.onclick = function () {
        cbmu("personal", "Médico", "txtPersonal", null, ["idPersonal", "DNI", "Médico"], "/Personal/ObtenerDatos", cargarLista);
    }
    var btnModalPaciente = document.getElementById("btnModalPaciente");
    btnModalPaciente.onclick = function () {
        cbmu("paciente", "Paciente", "txtPaciente", null,
            ["idPersonal", "DNI", "Personal"], "/Pacientes/ObtenerDatos", cargarLista);
    }
    var btnModalPacientes = document.getElementById("btnModalPacientes");
    btnModalPacientes.onclick = function () {
        cbmu("paciente", "Paciente", "txtPacientes", null,
            ["idPersonal", "DNI", "Personal"], "/Pacientes/ObtenerDatos", cargarLista);
    }
    var btnServicio = document.getElementById("btnModalServicio");
    btnServicio.onclick = function () {
        cbmu("servicio", "Tratamiento", "txtServicio", null, ["idServicio", "Código", "Tratamiento"], "/Servicios/ObtenerDatos", cargarLista);
    }
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario_Programacion(OpcionCambio);
    }
    var btnEliminar = document.getElementById("btnEliminar");
    btnEliminar.onclick = function () {
        eliminar(gvt("txtID"));
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var f = new Date();
            var fecha = (f.getDate() + "-" + (f.getMonth() + 1) + "-" + f.getFullYear());
            //if (gvt("txtFechaCita") >= fecha) {
            var url = "Citas/Grabar";
            var frm = new FormData();
            frm.append("idCita", gbi("txtID").value == 0 ? "0" : gbi("txtID").value);
            frm.append("FechaCita", gvt("txtFechaCita"));
            frm.append("idPaciente", gvc("txtPaciente"));
            frm.append("NombreCompleto", gvt("txtPaciente"));//
            if (gbi("cboTipoEvento").value === "1") {
                frm.append("idPersonal", gvc("txtPersonal"));//cita
            } else {
                frm.append("idPersonal", (gvc("txtPersonalOtros") || 0));//otros
            }
            frm.append("idServicio", 1);//gvc("txtServicio")
            frm.append("Observaciones", gvt("txtObservaciones"));
            frm.append("Hora", $("#txtHoraCitaI option:selected").text());
            frm.append("HoraF", $("#txtHoraCitaF option:selected").text());
            frm.append("idDocRef", 0);//
            frm.append("Estado", true);
            frm.append("Pago", gvt("txtPago"));//
            frm.append("EstadoCita", 1);
            frm.append("Tratamiento", gvt("txtTratamiento"));
            frm.append("cadDetalle", crearCadDetalle());
            //
            //Fecha
            var datosFecha = CalFecha.split("/");
            var MesFecha = datosFecha[0];
            var AñoFechaI = datosFecha[2];
            var AñoFechaF = parseInt(datosFecha[2]);
            var FechaIni = "01/" + MesFecha + "/" + AñoFechaI;
            //var FechaFin = "31/" + (parseInt(MesFecha)+3) + "/" + AñoFechaF;
            var date = new Date();
            var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            var FechaFin = lastDay.getDate() + "/" + MesFecha + "/" + AñoFechaF;
            frm.append("fi", FechaIni);
            frm.append("ff", FechaFin);
            swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarListar, frm);
            //} else{
            //    swal('Error de Fecha', 'No se puede grabar una cita para fechas pasadas', 'error');
            //}
        }
    };
    var btnDuplicar = document.getElementById("btnDuplicar");
    btnDuplicar.onclick = function () {
        gbi("txtID").value = "0";
        gbi("txtCodigo").value = "";
        gbi("txtCodigo").placeholder = "Autogenerado";
        gbi("btnEliminar").style.display = "none";
        gbi("btnDuplicar").style.display = "none";
        gbi("btnModalPaciente").style.display = "none";
        //gbi("btnModalPersonal").style.display = "none";
        //gbi("btnModalPersonalOtros").style.display = "none";
        //gbi("btnModalServicio").style.display = "none";
        gbi("btnConfirmar").style.display = "none";
        //gbi("txtPago").disabled = true;
        gbi("cboTipoEvento").disabled = true;
    }
    var btnConfirmar = document.getElementById("btnConfirmar");
    btnConfirmar.onclick = function () {
        var url = "Citas/Confirmar";
        var frm = new FormData();
        frm.append("idCita", gbi("txtID").value == 0 ? "0" : gbi("txtID").value);
        frm.append("EstadoCita", 1);
        swal({ title: "<div class='loader' style='margin:0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
        enviarServidorPost(url, actualizarConfirmar, frm);
    }

    var btnAgregarTratamiento = document.getElementById("btnAgregarTratamiento");
    btnAgregarTratamiento.onclick = function () {
        var error = true;
        error = validarAddTratamiento();
        if (error) {
            addItem();
        }

    }

    let cboTipoEvento = gbi("cboTipoEvento");
    cboTipoEvento.onchange = function () {
        if (this.value === "1") {//cita
            $("#divCita").show();
            $("#divOtro").hide();
        } else {//otro
            $("#divCita").hide();
            $("#divOtro").show();
        }
    }
    var btnModalPersonalOtros = document.getElementById("btnModalPersonalOtros");
    btnModalPersonalOtros.onclick = function () {
        cbmu("personal", "Médico", "txtPersonalOtros", null, ["idPersonal", "DNI", "Médico"], "/Personal/ObtenerDatos", cargarLista);
    }

    var btnBuscar = gbi("btnBuscar");
    btnBuscar.onclick = function () {
        var u = "Citas/ObtenerCitasxPaciente?idPaciente=" + gbi("txtPacientes").dataset.id;
        enviarServidor(u, mostrarCitas);
    }
    var btnCancelarBusqueda = document.getElementById("btnCancelarBusqueda");
    btnCancelarBusqueda.onclick = function () {
        gbi("txtPacientes").value = "";
        gbi("txtPacientes").dataset.id = "";
        show_hidden_Formulario_Programacion(2);
    }
}

function eliminar(id) {
    swal({
        title: 'Desea Eliminar la cita? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false,
    },
        function (isConfirm) {
            if (isConfirm) {
                IdEliminar = "";
                IdEliminar = id;
                var u = "Citas/Eliminar?idCita=" + id;
                enviarServidor(u, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó la cita', 'error');
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó la Cita';
            tipo = 'success';
            var datosFecha = CalFecha.split("/");
            var MesFecha = datosFecha[0];
            var AñoFechaI = datosFecha[2];
            var AñoFechaF = parseInt(datosFecha[2]) + 1;
            var FechaIni = "01/" + MesFecha + "/" + AñoFechaI;
            var FechaFin = "31/" + (parseInt(MesFecha) + 3) + "/" + AñoFechaF;
            var url = "Citas/ObtenerEventosxFecha/?fi=" + FechaIni + "&ff=" + FechaFin;
            //var url = "Citas/ObtenerEventosxFecha";
            enviarServidor(url, mostrarLista);
            show_hidden_Formulario_Programacion(1);
        }
        else {
            mensaje = data[1]
            tipo = 'error';
        }
    } else {
        mensaje = 'No hubo respuesta';
    }
    mostrarRespuesta(res, mensaje, tipo);
}
function validarFormulario() {
    var error = true;
    //if (validarcontrol("txtPaciente")) error = false;
    if (validarControl("txtFechaCita")) error = false;
    if (validarControl("txtHoraCitaI")) error = false;
    if (validarControl("txtHoraCitaF")) error = false;

    if (gbi("cboTipoEvento").value === "1") {
        if (validarControl("txtPaciente")) error = false;
        if (validarControl("txtPersonal")) error = false;
        if (validarControl("txtTratamiento")) error = false;
        if (validarControl("txtPago")) error = false;
    } else {
        if (validarControl("txtObservaciones")) error = false;
    }

    return error;
}
function actualizarListar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó  nueva Cita";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el Cita";
                tipo = "success";
                CerrarModal("modal-form");
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario_Programacion(1); }
        mostrarRespuesta(res, mensaje, tipo);
        ///////
        if (res == "OK") {
            var listaDatos = data[2].split("▼");
            var listaDr = [];
            evt = [];
            var d = "";
            for (var i = 0; i < listaDatos.length; i++) {

                var dato = listaDatos[i].split("▲");
                if (d != dato[4]) {
                    d = dato[4];
                    var fc = [dato[2], dato[4]];
                    listaDr.push(fc);
                }

                var evento = {
                    title: dato[1] + ' - ' + (dato[4] || dato[11]),//+ ' - ' + dato[5]
                    start: dato[9],
                    end: dato[10],
                    color: dato[2],
                    textColor: "#fff",
                    description: dato[11]
                };
                evt.push(evento);
            }
            mL(listaDr);
            $('#calendar').fullCalendar('removeEventSources');
            $('#calendar').fullCalendar('addEventSource', evt);
        }
    }
}
function actualizarConfirmar(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (res == "OK") {
            mensaje = "Se Confirmo la Cita";
            tipo = "success";
            CerrarModal("modal-form");
        }
        else {
            mensaje = data[1];
            tipo = "error";
        }
        if (res == "OK") { show_hidden_Formulario_Programacion(1); }
        mostrarRespuesta(res, mensaje, tipo);
        ///////
        if (res == "OK") {
            var listaDatos = data[2].split("▼");
            var listaDr = [];
            evt = [];
            var d = "";
            for (var i = 0; i < listaDatos.length; i++) {

                var dato = listaDatos[i].split("▲");
                if (d != dato[4]) {
                    d = dato[4];
                    var fc = [dato[2], dato[4]];
                    listaDr.push(fc);
                }

                var evento = {
                    title: dato[1] + ' - ' + (dato[4] || dato[11]),//+ ' - ' + dato[5]
                    start: dato[9],
                    end: dato[10],
                    color: dato[2],
                    textColor: "#fff",
                    description: dato[11]
                };
                evt.push(evento);
            }
            mL(listaDr);
            $('#calendar').fullCalendar('removeEventSources');
            $('#calendar').fullCalendar('addEventSource', evt);
        }
    }
}
function mostrarLista(rpta) {

    configurarBotones();
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            var listaDatos = listas[2].split("▼");
            listaPacientes = listas[4].split("▼");

            var listaDr = [];
            evt = [];
            var d = "";
            idRol = listas[3];
            VisualizarxRol();
            if (listaDatos.length > 0 && listaDatos[0] !== "") {
                for (var i = 0; i < listaDatos.length; i++) {

                    var dato = listaDatos[i].split("▲");
                    if (d != dato[4]) {
                        d = dato[4];
                        var fc = [dato[2], dato[4]];
                        listaDr.push(fc);
                    }
                    var evento = {
                        title: dato[1] + ' - ' + (dato[4] || dato[11]),// + ' - ' + dato[5],
                        start: dato[9],
                        end: dato[10],
                        color: dato[2],
                        textColor: "#fff",
                        description: dato[11]
                    };
                    evt.push(evento);

                }
            }
            mL(listaDr);
            $('#calendar').fullCalendar('removeEventSources');
            $('#calendar').fullCalendar('addEventSource', evt);
        }
    }
    gbi("cdb").style.height = (resizeCalendar("cdb") - 210) + "px";//210
    var h = resizeCalendar("calendar");
    $('#calendar').fullCalendar({
        height: h,
        locale: 'es',
        header: {
            left: '',
            center: 'title',
            right: '',

        },
        minTime: "08:00:00",
        maxTime: "20:00:00",
        defaultView: 'agendaWeek',
        scrollTime: "09:00:00",
        events: evt,
        eventClick: function (calEvent, jsEvent, view) {
            OpcionCambio = 1;
            var u = "Citas/detEvento?c=" + calEvent.title.substring(0, 10);
            enviarServidor(u, mostrarCita);
            gbi("cboTipoEvento").disabled = false;
        },
        dayClick: function (date, jsEvent, view) {
            if (idRol != "3") {
                show_hidden_Formulario_Programacion(1);
                lblTituloPanel.innerHTML = "Nueva Historia Clinica";
                var hora = date.format('HH:mm');
                var dia = date.format('DD-MM-YYYY');
                var mhora = hora.split(":");
                var mhoraf = parseInt(mhora[0]) + 1;
                var fmin = mhora[1];
                var fhora; var horaf;
                if (fmin == "00") { fhora = mhora[0] + ":30"; }
                else {
                    if (mhoraf.lenght < 2) { horaf = "0" + mhoraf; } else { horaf = mhoraf }
                    fhora = horaf + ":00";
                }
                gbi("txtFechaCita").value = dia;
                gbi("txtHoraCitaI").value = hora;
                gbi("txtHoraCitaF").value = fhora;
                limpiarCitaNueva();
            } else {
                mostrarRespuesta("Error", "Medico no puede crear Cita", "error");
            }
        },
        eventRender: function (eventObj, $el) {
            $el.popover({
                title: eventObj.title,
                content: eventObj.description,
                trigger: 'hover',
                placement: 'top',
                container: 'body'
            });
        },
    });

    CalFecha = document.getElementsByClassName("day active today")[0].dataset.day;
    var datosFecha = CalFecha.split("/");
    var MesFecha = datosFecha[0];
    var AñoFechaI = datosFecha[2];
    var AñoFechaF = parseInt(datosFecha[2]) + 1;
    var FechaIni = "01/" + MesFecha + "/" + AñoFechaI;
    var FechaFin = "31/" + (parseInt(MesFecha) + 3) + "/" + AñoFechaF;
    $('#dtPck').on('dp.change', function (event) {
        var url = "Citas/ObtenerEventosxFecha/?fi=" + FechaIni + "&ff=" + FechaFin;
        enviarServidor(url, mostrarLista);

        $('#calendar').fullCalendar(
            'gotoDate', $.fullCalendar.moment(event.date.format('YYYY/MM/DD')));

    })
    //$(window).resize(function () {
    //    reziseTablaCita();
    //});
}
function ccv(t) {
    switch (t) {
        case 1:
            $('#calendar').fullCalendar('changeView', 'month');
            break;
        case 2:
            $('#calendar').fullCalendar('changeView', 'agendaWeek');
            break;
        case 3:
            $('#calendar').fullCalendar('changeView', 'agendaDay');
            break;
        default:

    }
}
function mL(d) {
    var cont = "";
    for (var i = 0; i < d.length; i++) {
        cont += "<div class='col-12'>";
        cont += "<label style='background-color:" + d[i][0] + ";width:10px;height:10px;border-radius:5px;padding-top:5px;margin:0px;'></label> " + d[i][1];
        cont += "</div>";
    }
    gbi("dL").innerHTML = cont;
}
function mostrarCita(r) {
    limpiarTodo();
    if (r != "") {
        show_hidden_Formulario_Programacion(OpcionCambio);
        lblTituloPanel.innerHTML = "Reporte De Cita";
        var listas = r.split("↔");
        var resultado = listas[0];
        var mensaje = listas[1];
        if (resultado == "OK") {
            var listaDatos = listas[2].split("▲");
            var listaDet = listas[3].split("▼");
            gbi("btnEliminar").style.display = "";
            gbi("txtID").value = listaDatos[0];
            gbi("txtFechaCita").value = listaDatos[1];
            gbi("txtPaciente").dataset.id = listaDatos[2];
            gbi("txtPaciente").value = listaDatos[3];

            if (listaDatos[2] && parseInt(listaDatos[2]) > 0) {
                gbi("txtPersonal").dataset.id = listaDatos[4];
                gbi("txtPersonal").value = listaDatos[5];
                gbi("cboTipoEvento").value = 1;
            } else {
                gbi("txtPersonalOtros").dataset.id = listaDatos[4];
                gbi("txtPersonalOtros").value = listaDatos[5];
                gbi("cboTipoEvento").value = 2;
            }
            gbi("cboTipoEvento").onchange();

            gbi("txtServicio").dataset.id = listaDatos[6];
            gbi("txtServicio").value = listaDatos[7];
            gbi("txtHoraCitaI").value = listaDatos[9];
            gbi("txtCodigo").value = listaDatos[20];
            gbi("txtObservaciones").value = listaDatos[11];
            gbi("txtPago").value = listaDatos[21];
            gbi("txtHoraCitaF").value = listaDatos[23];
            gbi("txtTratamiento").value = listaDatos[40];

            //Detalle
            if (listaDet[0] != "") {
                if (listaDet.length >= 1) {
                    for (var i = 0; i < listaDet.length; i++) {
                        console.log(listaDet[i]);
                        addItem(1, listaDet[i].split("▲"));
                    }
                }
            }
            //

            AbrirModal("modalCita");
            VisualizarxRol();
            //estadoCita listaDatos[24];
            //1: pendiente
            //2: confirmado
            if (listaDatos[24] == 2) {
                gbi("btnConfirmar").style.display = "none";
                gbi("btnEliminar").style.display = "none";
            } else {
                gbi("btnConfirmar").style.display = "";

                if (parseInt(listaDatos[39]) > 0)
                    gbi("btnEliminar").style.display = "none";
                else
                    gbi("btnEliminar").style.display = "";
            }
            gbi("btnDuplicar").style.display = "";
        }
        else {
            mostrarRespuesta("Error", mensaje, "error");
        }
    }
}
function resizeCalendar(id) {
    var div = document.getElementById(id);
    var altoTotal = window.innerHeight;
    var puntoInicio = getPos(div).y;
    var altoFooter = 21;
    return (altoTotal - altoFooter - puntoInicio - 70);
}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    switch (opcion) {
        case 1:
            OpcionCambio = 1;
            limpiarTodo();
            gbi("btnConfirmar").style.display = "none";
            gbi("btnDuplicar").style.display = "none";
            gbi("cboTipoEvento").value = "1";
            $("#txtFechaCita").datetimepicker("date", new Date());
            $("#divCita").show();
            $("#divOtro").hide();
            show_hidden_Formulario_Programacion(1);
            lblTituloPanel.innerHTML = "Nueva Cita";
            VisualizarxRol();
            break;
        case 2:
            limpiarTodo();
            lblTituloPanel.innerHTML = "Editar Historia Clinica";
            TraerDetalle(id);
            show_hidden_Formulario_Programacion(1);
            break;
    }
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtPaciente":
            return gbi("txtPersonal");
            break;
        case "txtPersonal":
            return gbi("txtServicio");
            break;
        default:
            break;
    }
}
function limpiarTodo() {
    //gbi("txtFechaCita").checked = false;
    bDM("txtFechaCita");
    bDM("txtPaciente");
    bDM("txtPersonal");
    bDM("txtServicio");
    bDM('txtPago');
    bDM("txtObservaciones");
    bDM("txtHoraCitaI");
    gbi("btnEliminar").style.display = "none";
    for (let item of gbi("rowFrm").querySelectorAll("input")) {
        if (item.id) { limpiarControl(item.id); }
    }
    gbi("btnDuplicar").style.display = "";
    limpiarControl("txtHoraCitaI");
    gbi("contentTratamiento").innerHTML = "";
    gbi("txtTratamiento").value = "";
}
function limpiarCitaNueva() {
    bDM("txtPaciente");
    bDM("txtPersonal");
    bDM("txtServicio");
    bDM('txtPago');
    bDM("txtObservaciones");
    gbi("btnEliminar").style.display = "none";
    gbi("btnDuplicar").style.display = "none";
    gbi("btnConfirmar").style.display = "none";
    bDM("txtPersonalOtros");
    gbi("cboTipoEvento").disabled = false;
    for (let item of gbi("divOtro").querySelectorAll("button")) {
        item.style.display = "";
    }
    for (let item of gbi("divCita").querySelectorAll("button")) {
        item.style.display = "";
    }
    gbi("cboTipoEvento").value = "1";
    gbi("cboTipoEvento").onchange();
    gbi("contentTratamiento").innerHTML = "";
    gbi("txtTratamiento").value = "";
}
function funcionModal(tr) {
    var num = tr.id.replace("numMod", "");
    var id = gbi("md" + num + "-0").innerHTML;
    var value;
    if (tr.children.length === 2) {
        value = gbi("md" + num + "-1").innerHTML;
    }
    else {
        value = gbi("md" + num + "-2").innerHTML;
    }
    var value2 = gbi("md" + num + "-1").innerHTML;
    txtModal.value = value;
    txtModal.dataset.id = id;
    var next = accionModal2(url, tr, id);
    if (txtModal2) {
        txtModal2.value = value2;
    }
    CerrarModal("modal-Modal", next);
    gbi("txtFiltroMod").value = "";
}
function cbmu(ds, t, tM, tM2, cab, u, m) {
    document.getElementById("div_Frm_Modal").innerHTML = document.getElementById("div_Frm_Detalle").innerHTML;
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
}
function VisualizarxRol() {
    if (idRol == "3") {
        gbi("btnMostrarDetalle").style.display = "none";
        gbi("btnEliminar").style.display = "none";
        gbi("btnGrabar").style.display = "none";
        gbi("txtFechaCita").disabled = true;
        gbi("txtHoraCitaI").disabled = true;
        gbi("txtHoraCitaF").disabled = true;
        gbi("txtPago").disabled = true;
        gbi("txtObservaciones").disabled = true;
    } else {
    }
}

function validarAddTratamiento() {
    var error = true;
    if (validarControl("txtServicio")) { error = false; }

    return error;
}

function addItem(tipo, data) {
    var contenido = "";
    var div = document.getElementById("contentTratamiento");
    var cadena = "<div class='art row panel salt' id='gd" + (tipo == 1 ? data[0] : document.getElementsByClassName("art").length + 1) + "' data-id='0' tabindex='100' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
    cadena += "<div class='col-12 col-md-1' style='display:none' >" + (tipo == 1 ? data[1] : '0') + "</div>";
    cadena += "<div class='col-12 col-md-1'>" + (document.getElementsByClassName("art").length + 1) + "</div>";
    cadena += "<div class='col-12 col-md-3' data-id='" + (tipo == 1 ? data[2] : gbi("txtServicio").dataset.id) + "'>" + (tipo == 1 ? data[3] : gvt("txtServicio")) + "</div>";
    cadena += "<div class='col-12 col-md-2'>" + (tipo == 1 ? data[4] : gvt("txtMonto")) + "</div>";
    cadena += "<div class='col-12 col-md-1' style='display:none' >" + (tipo == 1 ? data[0] : '0') + "</div>";
    cadena += "<div class='row saltbtn'>";
    cadena += "<div class='col-12'>";
    cadena += "<button type='button' onclick='borrarDetalle(this);'  class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' style='padding:3px 10px;' > <i class='fa fa-trash-o fs-11'></i> </button>"
    cadena += "</div>";
    cadena += "</div>";
    cadena += "</div>";
    cadena += "</div>";
    div.innerHTML += cadena;
    cancel_AddTratamiento();
}
function cancel_AddTratamiento() {
    var btnAgregarTratamiento = document.getElementById("btnAgregarTratamiento");
    var btnCancelarTratamiento = document.getElementById("btnCancelarTratamiento");
    //btnCancelarArticulo.style.visibility = "hidden";
    btnAgregarTratamiento.dataset.row = -1;
    limpiarControl("txtServicio");
    gbi("txtMonto").value = "0.00";
    btnAgregarTratamiento.innerHTML = "Agregar";
    gbi("btnActualizarTratamiento").style.display = "none";
    gbi("btnCancelarTratamiento").style.display = "none";
    gbi("btnAgregarTratamiento").style.display = "";

    var rows = gbi("contentTratamiento").querySelectorAll(".art");
    var listaTratamiento = "";

    var TotalMonto = 0;
    for (var i = 0; i < rows.length; i++) {
        listaTratamiento += rows[i].children[2].innerHTML + " + ";//idArticulo
        TotalMonto = (parseFloat(TotalMonto) + parseFloat(rows[i].children[3].innerHTML)).toFixed(2);
    }
    gbi("txtTratamiento").value = listaTratamiento;
    gbi("txtPago").value = TotalMonto;
}
function borrarDetalle(elem) {
    var p = elem.parentNode.parentNode.parentNode.remove();
    var rows = gbi("contentTratamiento").querySelectorAll(".art");
    var listaTratamiento = "";
    for (var i = 0; i < rows.length; i++) {
        listaTratamiento += rows[i].children[2].innerHTML + " + ";//idArticulo
    }
    gbi("txtTratamiento").value = listaTratamiento;
}

//function reziseTablaCita() {
//    var div = document.getElementById("calendar");
//    var altoTotal = window.innerHeight;
//    var puntoInicio = getPos(div).y;
//    var altoFooter = 21;
//    div.style.height = "" + (altoTotal - altoFooter - puntoInicio - 100) + "px";
//}

function crearCadDetalle() {
    var cdet = "";
    $(".art").each(function (obj) {
        cdet += $(".art")[obj].children[0].innerHTML;//idDetalle
        cdet += "|0|" + $(".art")[obj].children[2].dataset.id;//idtratamiento
        cdet += "|" + $(".art")[obj].children[2].innerHTML;//tratamiento
        cdet += "|" + $(".art")[obj].children[3].innerHTML;//Monto Tratamiento
        cdet += "|01-01-2000|01-01-2000|1|1";
        cdet += "¯";
    });
    return cdet;
    //"00|86|CONSULTA|80|01-01-2000|01-01-2000|1|1¯"
}

function show_hidden_Formulario_Programacion(busq, filtro) {
    var frm = document.getElementById("rowFrm");
    var frmFilt = document.getElementById("rowFilter");
    var frmBusqueda = document.getElementById("rowBusqueda");
    switch (busq) {
        case 1:
            if (frm.style.display == "none") {
                document.getElementById("rowTabla").style.display = "none";
                if (filtro) {
                    frmFilt.style.display = "none";
                    $('.content-header-right').hide();
                }
                $("#rowFrm").fadeIn();
            } else {
                frm.style.display = "none";
                if (filtro) {
                    $("#rowFilter").fadeIn();
                }
                $("#rowTabla").fadeIn();
            }
            break;
        case 2:
            if (frmBusqueda.style.display == "none") {
                document.getElementById("rowTabla").style.display = "none";
                if (filtro) {
                    frmBusqueda.style.display = "none";
                    $('.content-header-right').hide();
                }
                $("#rowBusqueda").fadeIn();
            } else {
                frmBusqueda.style.display = "none";
                if (filtro) {
                    $("#rowBusqueda").fadeIn();
                }
                $("#rowTabla").fadeIn();
            }
            break;
        case 3:
            if (frm.style.display == "none") {
                document.getElementById("rowBusqueda").style.display = "none";
                $("#rowFrm").fadeIn();
            } else {
                frm.style.display = "none";
                $("#rowBusqueda").fadeIn();
            }
            break;
    }


    //if (busq == 1) {
    //    if (frm.style.display == "none") {
    //        document.getElementById("rowTabla").style.display = "none";
    //        if (filtro) {
    //            frmFilt.style.display = "none";
    //            $('.content-header-right').hide();
    //        }
    //        $("#rowFrm").fadeIn();
    //    } else {
    //        frm.style.display = "none";
    //        if (filtro) {
    //            $("#rowFilter").fadeIn();
    //        }
    //        $("#rowTabla").fadeIn();
    //    }
    //} else {
    //    if (frmBusqueda.style.display == "none") {
    //        document.getElementById("rowTabla").style.display = "none";
    //        if (filtro) {
    //            frmBusqueda.style.display = "none";
    //            $('.content-header-right').hide();
    //        }
    //        $("#rowBusqueda").fadeIn();
    //    } else {
    //        frmBusqueda.style.display = "none";
    //        if (filtro) {
    //            $("#rowBusqueda").fadeIn();
    //        }
    //        $("#rowTabla").fadeIn();
    //    }
    //}


    if ($('.content-header-right')) {
        $('.content-header-right').fadeIn();
    }
    //reziseTabla();
}
function mostrarCitas(rpta) {
    show_hidden_Formulario_Programacion(2);
    crearTablaBusqueda(cabeceras, "cabeTablaBusqueda");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var resultado = listas[0];
        if (resultado == "OK") {
            listaDatosBusqueda = listas[2].split("▼");
            listarBusqueda();
        }
    }
}
function crearTablaBusqueda(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 1:
                contenido += "              <div class='col-12 col-md-1'>";
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
function listarBusqueda() {
    matriz = crearMatriz(listaDatosBusqueda);
    configurarFiltroBusqueda(cabeceras);
    mostrarMatrizBusqueda(matriz, cabeceras, "divTablaBusqueda", "ContPrincipalBusqueda");
}
function configurarFiltroBusqueda(cabe) {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatosBusqueda);
        mostrarMatrizBusqueda(matriz, cabe, "divTablaBusqueda", "ContPrincipalBusqueda");
    };
}
function mostrarMatrizBusqueda(matriz, cabeceras, tabId, contentID) {
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        nRegistros = matriz.length;
        var dat = [];
        for (var i = 0; i < nRegistros; i++) {
            if (i < nRegistros) {
                var contenido2 = "<div class='row panel salt' id='num" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:2px;cursor:pointer;'>";
                for (var j = 0; j < cabeceras.length; j++) {
                    contenido2 += "<div class='col-12 ";
                    switch (j) {
                        case 0:
                            contenido2 += "col-md-2' style='display:none;'>";
                            break;
                        case 1:
                            contenido2 += "col-md-1'>";
                            break;
                        case 2:
                            contenido2 += "col-md-3' style='padding-top:5px;'>";
                            break;
                        case 3:
                            contenido2 += "col-md-6' style='padding-top:5px;'>";
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
                contenido2 += "<button type='button' class='btn btn-sm waves-effect waves-light btn-info pull-right' style='padding:3px 10px;' onclick='mostrarDetalleCita(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-eye fs-11'></i></button>";
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

function mostrarDetalleCita(opcion, codigo) {
    OpcionCambio = 3;
    var u = "Citas/detEvento?c=" + codigo;
    enviarServidor(u, mostrarCita);

}