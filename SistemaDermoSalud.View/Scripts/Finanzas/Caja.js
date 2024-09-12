
var cabeceras = ["IdCaja", "Descripcion", "Fecha Apertura", "Fecha Cierre", "Inicio", "Saldo Final", "Estado"];
var listaDatos;
var matriz = [];
var orden = 0;
var registrosPagina = 1000000000;
var indiceActualPagina = 0;
var paginasBloque = 3;
var indiceActualBloque = 0;
var indicePagina = 0;
var textoExportar;
var excelExportar;
var NombreUsuario;
var Periodo; var Fecha; var NroCaja;
var OpcionCaja;
var listaDatosDetalle; var listaCaja;
//variables modal
var matrizModal = [];
var listaDatosModal;
//var cabecera_Modal = [];
var txtModal;//input para poner el valor
var txtValor;//input para obtener el valor
//idUsuario
var idUsuario;
var $table = $('table.demo1');
var listaConceptosCaja
var actualizarDetalle;
var countDetalle
var comboConcepto; var combox;
var listaConcepto; var listaDEmpleado; var listaDBanco;
var listaConcepto2;
var listaDetalleCajaReal = [];
var listaDetalleCajaPDF = [];

//$("#txtFilFecIn").datetimepicker({
//    format: 'DD-MM-YYYY',
//});
//$("#txtFilFecFn").datetimepicker({
//    format: 'DD-MM-YYYY',
//});
//$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
$('#collapseOne-2').on('shown.bs.collapse', function () {
    reziseTabla();
})
$('#collapseOne-2').on('hidden.bs.collapse', function () {
    reziseTabla();
})
//Inicializando    = 
var url = "/Caja/ObtenerDatos";
enviarServidor(url, mostrarLista);
configurarBotonesModal();
function mostrarLista(rpta) {
    //crearTablaCaja(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        listaCaja = listas;
        listaDatos = listas[2].split("▼");
        listaMoneda = listas[3].split("▼");
        idUsuario = listas[4];
        NombreUsuario = listas[5];
        gbi("txtPeriodo").value = listas[6];
        gbi("dtpFechaInicio").value = listas[7];
        gbi("txtNroCaja").value = listas[8];
        gbi("txtFilFecIn").value = listas[9];
        gbi("txtFilFecFn").value = listas[10];
        listaConcepto = listas[11].split("▼");
        listar(listaDatos);
        cargarDatosMonedas(listaMoneda);
        cargarDatosConceptos(listaConcepto);
    }
}
function listar(r) {

    if (r[0] !== '') {
        let newDatos = [];
        console.log(r);
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                idCaja: valor[0],
                descripcion: valor[1],
                fechaApertura: valor[2],
                horaApertura: valor[7],
                fechaCierre: valor[3],
                horaCierre: valor[8],
                montoInicio: valor[4],
                montoSaldo: valor[5],
                estadoCaja: valor[6],
                tipoCaja: valor[9]
            })
        });
        console.log(newDatos);
        let cols = ["tipoCaja","descripcion", "fechaApertura", "horaApertura", "fechaCierre", "horaCierre", "montoInicio", "montoSaldo", "estadoCaja"];
        loadDataTable(cols, newDatos, "idCaja", "tbDatos", cadButtonOptions(), false);
    }

}
function mostrarDetalle(opcion, idRow) {
    let id = 0;
    console.log(id);
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    var aperturado = 0;
    //var contentPrincipal = gbi("contentPrincipal");
    //for (var i = 0; i < contentPrincipal.children.length; i++) {
    //    if (contentPrincipal.children[i].children[6].innerText == "ABIERTA")
    //        aperturado++;
    //}
    limpiarTodo();
    OpcionCaja = "";
    switch (opcion) {
        case 1:
            if (aperturado == 0) {
                limpiarCabecera();
                show_hidden_Formulario(true);
                OpcionCaja = "1";
                lblTituloPanel.innerHTML = "Aperturar Caja";//Titulo Insertar
                txtUsuarioResp.value = NombreUsuario;
                gbi("divMovimientoCaja").style.display = "none";
                idUsuario = listaCaja[4];
                NombreUsuario = listaCaja[5];
                gbi("txtPeriodo").value = listaCaja[6];
                gbi("dtpFechaInicio").value = listaCaja[7];
                gbi("txtNroCaja").value = listaCaja[8];
                gbi("dtpFechaCierre").value = "-";
                gbi("txtSaldoInicial").value = 0.00;
                gbi("txtInicial").value = 0.00;
                gbi("txtMontoIngreso").value = 0.00;
                gbi("txtMontoSalida").value = 0.00;
                gbi("txtSaldoFinal").value = 0.00;
                gbi("txtIngresoTarjeta").value = 0.00;
                gbi("txtIngresoEfectivo").value = 0.00;
                gbi("txtSaldoInicial").disabled = false;
                gbi("btnGrabar").style.display = "";
                gbi("btnCerrarCaja").style.display = "none";
                $("#btnGrabar").show();
                gbi("divMovimientoCaja").querySelectorAll("input, select, button:not(.btn-info)").forEach(item => { item.disabled = false; });
            }
            else {
                swal("Error", "Deben estar cerradas todas las cajas para aperturar una nueva", "error")
            }
            break;
        case 2:
            OpcionCaja = "2";
            id = idRow.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
            lblTituloPanel.innerHTML = "Caja";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            gbi("divMovimientoCaja").style.display = "";
            gbi("txtSaldoInicial").disabled = true;
            gbi("btnGrabar").style.display = "none";
            gbi("btnCerrarCaja").style.display = "";
            break;
    }
}
function limpiarTodo() {
    limpiarControl("txtIDdetalle");
    limpiarControl("txtConcepto");
    limpiarControl("txtTipoOperacion");
    limpiarControl("txtAfectoIGV");
    limpiarControl("txtEmpleado");
    limpiarControl("txtSerieRecibo");
    limpiarControl("txtNroRecibo");
    limpiarControl("txtSerieVale");
    limpiarControl("txtNroVale");
    limpiarControl("txtNroCheque");
    limpiarControl("txtBanco");
    limpiarControl("txtListaDocumento");
    limpiarControl("txtRazonSocial");
    limpiarControl("txtRuc");
    limpiarControl("txtMontoPendiente");
    limpiarControl("txtObservacion");
    limpiarControl("txtIdCompraVenta");
    limpiarControl("txtNroOperacion");
    gbi("txtSubTotalSoles").value = 0.00;
    gbi("txtIgvSoles").value = 0.00;
    gbi("txtTotalSoles").value = 0.00;
    gbi("btnImprimirPDF").disabled = true;
}
function limpiarCabecera() {
    limpiarControl("txtID");
    limpiarControl("txtCodigo");
    limpiarControl("txtPeriodo");
    limpiarControl("txtNroCaja");
    limpiarControl("txtEmpleado");
    limpiarControl("dtpFechaInicio");
    limpiarControl("dtpFechaCierre");
    limpiarControl("txtSaldoInicial");
    limpiarControl("txtUsuarioResp");
    limpiarControl("txtInicial");
    limpiarControl("txtMontoIngreso");
    limpiarControl("txtMontoSalida");
    limpiarControl("txtSaldoFinal");
}
var txtSaldoInicial = document.getElementById("txtSaldoInicial");

txtSaldoInicial.onkeyup = function () {
    var mInicio = gbi("txtSaldoInicial").value;
    gbi("txtInicial").value = parseFloat(mInicio).toFixed(2);
    gbi("txtSaldoFinal").value = parseFloat(mInicio).toFixed(2);
}

function configurarBotonesModal() {
    let txtConcepto = gbi("txtConcepto");
    txtConcepto.onchange = function () {
        CambiarConcepto()
    }
    var btnModalEmpleado = document.getElementById("btnModalEmpleado");
    btnModalEmpleado.onclick = function () {
        cbmu("empleado", "Empleado", "txtEmpleado", null,
            ["idEmpleado", "Nombre"], ' /Caja/ListaEmpleados', cargarLista);
    }
    var btnModalBanco = document.getElementById("btnModalBanco");
    btnModalBanco.onclick = function () {
        cbmu("banco", "Banco", "txtBanco", null,
            ["idBanco", "Descripcion"], ' /Caja/ListaBancos', cargarLista);
    }
    var btnGrabar = document.getElementById("btnGrabar");
    btnGrabar.onclick = function () {
        if (validarFormularioCaja()) {
            var url = "Caja/Grabar";
            var frm = new FormData();
            frm.append("idCaja", txtID.value.length == 0 ? "0" : txtID.value);
            frm.append("PeriodoAno", gbi("txtPeriodo").value);
            frm.append("NroCaja", gbi("txtNroCaja").value);
            frm.append("FechaApertura", gbi("dtpFechaInicio").value);
            frm.append("idTipoCaja", gbi("cboTipoCaja").value);
            frm.append("idMoneda", gbi("txtMoneda").value);
            frm.append("MontoInicio", gbi("txtSaldoInicial").value);
            frm.append("MontoIngreso", gbi("txtMontoIngreso").value);
            frm.append("MontoSalida", gbi("txtMontoSalida").value);
            frm.append("MontoSaldo", gbi("txtSaldoFinal").value);
            frm.append("Opcion", OpcionCaja);
            frm.append("MontoEfectivo", gbi("txtIngresoEfectivo").value);
            frm.append("MontoTarjeta", gbi("txtIngresoTarjeta").value);
            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario(true);
    }
    var btnAgregarDetalle = document.getElementById("btnAgregarDetalle")
    btnAgregarDetalle.onclick = function () {
        if (validarFormularioDetalle()) {
            var url = "Caja/GrabarDetalle";
            var frm = new FormData();
            frm.append("idCajaDetalle", gbi("txtIDdetalle").value.length == 0 ? "0" : gbi("txtIDdetalle").value);
            frm.append("idCaja", gbi("txtID").value);
            frm.append("PeriodoAno", gbi("txtPeriodo").value);
            frm.append("NroCaja", gbi("txtNroCaja").value);
            frm.append("idConcepto", gbi("txtConcepto").value);
            frm.append("DescripcionConcepto", gbi("txtConcepto").options[gbi("txtConcepto").selectedIndex].text);
            frm.append("idMoneda", gbi("txtMoneda").value);
            frm.append("SubTotalNacional", gbi("txtSubTotalSoles").value);
            frm.append("IGVNacional", gbi("txtIgvSoles").value);
            frm.append("TotalNacional", gbi("txtTotalSoles").value);
            var idC = gbi("txtConcepto").value;
            switch (idC) {
                case "9": frm.append("idProvCliEmpl", gbi("txtEmpleado").value);
                    frm.append("NombreProvCliEmpl", gbi("txtRazonSocial").value.length == 0 ? "-" : gbi("txtRazonSocial").value);
                    break;
                case "10": frm.append("NombreProvCliEmpl", gbi("txtEmpleado").value.length == 0 ? "-" : gbi("txtEmpleado").value);
                    break;
            }
            frm.append("Observaciones", gbi("txtObservacion").value.length == 0 ? " " : gbi("txtObservacion").value);
            frm.append("idTipoDocumento", gbi("txtListaDocumento").value);
            var numeroDocumento = gbi("txtListaDocumento").value;
            var DatosDcto = numeroDocumento.split("-");
            frm.append("SerieDcto", DatosDcto.length == 0 ? " " : DatosDcto[0]);//gbi("txtSerieRecibo").value.length == 0 ? " " : gbi("txtSerieRecibo").value);
            frm.append("NroDcto", DatosDcto.length == 0 ? " " : DatosDcto[1]); // gbi("txtNroRecibo").value.length == 0 ? " " : gbi("txtNroRecibo").value);
            frm.append("NroOperacion", gbi("txtNroOperacion").value.length == 0 ? " " : gbi("txtNroOperacion").value);//frm.append("NroVale", gbi("txtNroVale").value.length == 0 ? " " : gbi("txtNroVale").value);
            frm.append("TipoOperacion", gbi("txtTipoOperacion").value);
            frm.append("Ruc", gbi("txtRuc").value.length == 0 ? " " : gbi("txtRuc").value);
            frm.append("MontoPendiente", gbi("txtMontoPendiente").value.length == 0 ? 0 : gbi("txtMontoPendiente").value);
            frm.append("idCompraVenta", gbi("txtIdCompraVenta").value.length == 0 ? 0 : gbi("txtIdCompraVenta").value);
            frm.append("idCita", gbi("txtCita").value.length == 0 ? 0 : gbi("txtCita").dataset.id);
            frm.append("NroCita", gbi("txtCita").value.length == 0 ? " " : gbi("txtCita").value);
            frm.append("Paciente", gbi("txtPaciente").value.length == " " ? 0 : gbi("txtPaciente").value);
            frm.append("CostoCita", gbi("txtCostoCita").value.length == 0 ? 0 : gbi("txtCostoCita").value);
            frm.append("TipoCambio", 1);
            frm.append("idTipoPago", gbi("cboTipoPago").value.length == 0 ? 0 : gbi("cboTipoPago").value);
            frm.append("TipoPago", gbi("cboTipoPago").value.length == 0 ? 0 : gbi("cboTipoPago").selectedOptions[0].innerHTML);
            frm.append("idTarjeta", gbi("cboTarjeta").value.length == 0 ? 0 : gbi("cboTarjeta").selectedOptions[0].innerHTML);
            frm.append("Tarjeta", gbi("cboTarjeta").value.length == 0 ? 0 : gbi("cboTarjeta").selectedOptions[0].innerHTML);
            frm.append("MontoInicio", txtSaldoInicial.value);
            frm.append("idTipoEmpleado", 1);
            frm.append("SerieRecibo", gbi("txtSerieRecibo").value.length == 0 ? " " : gbi("txtSerieRecibo").value);
            frm.append("NroRecibo", gbi("txtNroRecibo").value.length == 0 ? " " : gbi("txtNroRecibo").value);
            //Swal.fire({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
            enviarServidorPost(url, actualizarDetalle, frm);

        }
    }
    var btnCancelarDetalle = document.getElementById("btnCancelarDetalle");
    btnCancelarDetalle.onclick = function () {
        //btnCancelarDetalle.style.visibility = "hidden";
        btnAgregarDetalle.dataset.row = -1;
        btnAgregarDetalle.innerHTML = "Agregar";
        limpiarTodo();
    }
    var btnModalCita = document.getElementById("btnModalCita");
    btnModalCita.onclick = function () {
        cbmu("cita", "Citas", "txtCita", null,
            ["idCita", "Fecha", "Codigo", "Paciente", "Costo", "Observaciones"], ' /Caja/ListaCitas', cargarListaConcepto);
    }
    var cboTipoPago = gbi("cboTipoPago");
    cboTipoPago.onchange = function () {
        var TipoPago = gbi("cboTipoPago").value;
        if (TipoPago == "1") {
            gbi("divOperacion").style.display = "none";
            gbi("divTarjeta").style.display = "none";
        } else {
            gbi("divOperacion").style.display = "";
            gbi("divTarjeta").style.display = "";
        }
    }
    var btnModalListaDocumento = gbi("btnModalListaDocumento");
    btnModalListaDocumento.onclick = function () {
        var concepto = gbi("txtConcepto").value;
        let urlProducto;
        switch (concepto) {
            case "7":
                urlProducto = '/Caja/ListaCompras';
                enviarServidor(urlProducto, cargarListaDoc); break;
            case "9":
                urlProducto = '/Caja/ListaVentas';
                enviarServidor(urlProducto, cargarListaDoc); break;
        }
    }
    gbi("txtTotalSoles").onfocus = function () {
        CalcularMontos();
        this.select();
    }
    gbi("txtSaldoInicial").onfocus = function () { this.select(); }
    var btnBuscar = document.getElementById("btnBuscar");
    btnBuscar.onclick = function () {
        BuscarxFecha(gbi("txtFilFecIn").value, gbi("txtFilFecFn").value);
    }

    let txtSaldoInicial = gbi("txtSaldoInicial");
    txtSaldoInicial.onkeypress = function (e) {
        var reg = /^[0-9.]+$/;
        if (!reg.test(e.key)) return false;
    }

    var btnCerrarCaja = document.getElementById("btnCerrarCaja");
    btnCerrarCaja.onclick = function () {
        var url = "Caja/CerrarCaja";
        var frm = new FormData();
        frm.append("idCaja", txtID.value.length == 0 ? "0" : txtID.value);
        //swal({ title: "<div class='loader' style='margin: 0px 200px;'></div>Procesando información", html: true, showConfirmButton: false });
        enviarServidorPost(url, actualizarCerrarCaja, frm);
    };
}
function BuscarxFecha(f1, f2) {
    var url = 'Caja/ObtenerPorFecha?fechaInicio=' + f1 + '&fechaFin=' + f2;
    enviarServidor(url, mostrarBusqueda);
}
function mostrarBusqueda(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        listaDatos = listas[2].split('▼');
        matriz = crearMatriz(listaDatos);
        configurarFiltro(cabeceras);
        mostrarMatrizCaja(matriz, cabeceras, "divTabla", "contentPrincipal");
        reziseTabla();
    }
}
function validarFormularioCaja() {
    var error = true;
    //var sInicial = parseFloat(gbi("txtSaldoInicial").value);
    //if (sInicial == 0) {
    //    error = false;
    //    Swal.fire("Error", "Debe ingresar un saldo inicial mayor a 0", "error")
    //}
    if (validarControl("txtSaldoInicial")) error = false;
    if (validarControl("txtMoneda")) error = false;
    return error;
}
function validarFormularioDetalle() {
    var error = true;
    switch (gbi("txtConcepto").dataset.id) {
        case "10": case "2":
            if (validarControl("txtEmpleado")) error = false;
            if (validarControl("txtConcepto")) error = false;
            if (validarControl("txtSerieRecibo")) error = false;
            if (validarControl("txtNroRecibo")) error = false;
            if (validarControl("txtTotalSoles")) error = false;
            break;
        case "93": case "92":
            if (validarControl("txtTotalSoles")) error = false;
            if (validarControl("txtListaDocumento")) error = false;
            if (validarControl("txtRazonSocial")) error = false;
            if (validarControl("txtRuc")) error = false;
            if (validarControl("txtMontoPendiente")) error = false;
            if (validarControl("txtConcepto")) error = false;
            break;
        default:
            if (validarControl("txtTotalSoles")) error = false;
            if (validarControl("txtConcepto")) error = false;
    }
    return error;
}
function actualizarListar(rpta) {
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar(listaDatos);
    }
}
function TraerDetalle(id) {
    var url = "Caja/ObtenerDatosxID/?id=" + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == 'OK') {
            var listaCabecera = listas[2].split('▲');
            var listaDetalleCaja = listas[3].split('▲');
            //Asignar valores a controles
            gbi("txtID").value = listaCabecera[0];//idCaja
            gbi("txtCodigo").value = listaCabecera[1];//codigoGenerado            
            gbi("txtPeriodo").value = listaCabecera[2];//Periodo
            gbi("txtNroCaja").value = listaCabecera[3];//NroCaja
            gbi("dtpFechaInicio").value = listaCabecera[4]
            gbi("dtpFechaCierre").value = listaCabecera[5]
            gbi("txtSaldoInicial").value = listaCabecera[7];//Monto Inicio
            gbi("txtUsuarioResp").value = listaCabecera[8];//Usuario Responsable
            gbi("txtInicial").value = listaCabecera[7];//Saldo inicial
            gbi("txtMoneda").value = listaCabecera[6];
            gbi("txtMontoIngreso").value = listaCabecera[9];//Ingresp
            gbi("txtMontoSalida").value = listaCabecera[10];//Salida
            gbi("txtSaldoFinal").value = listaCabecera[11];//Saldo Final
            //adc(listaMoneda, listaCabecera[6], "txtMoneda", 1);
            //console.log(listaCabecera);
            gbi("cboTipoCaja").value = listaCabecera[14];//Saldo Final 

            gbi("cboTipoPago").value = "1";
            gbi("cboTipoPago").selectedOptions[0].innerHTML;
            gbi("cboTarjeta").value = "1";
            gbi("cboTarjeta").selectedOptions[0].innerHTML;
            //Detalles
            var detalleCaja = listas[3].split('▼');
            CargarDetalleCaja(detalleCaja);

            var estadoCaja = listaCabecera[13];
            if (estadoCaja == "C") {
                BloquearCaja();
            } else {
                //btnGrabar.innerHTML = "Cerrar Caja";
                DesbloquearCaja();
            }

            //lista detalle pdf
            listaDetalleCajaPDF = listas[5].split('▼');
            //bloquear boton imprimir cunado la caja no esta cerrada
            gbi("btnImprimirCajaPDF").disabled = estadoCaja != "C";
        }
        else {
            document.getElementById("error").innerHTML = mensaje;
        }
    }
}
function BloquearCaja() {
    gbi("txtIDdetalle").disabled = true;
    gbi("txtConcepto").disabled = true;
    gbi("txtTipoOperacion").disabled = true;
    gbi("txtAfectoIGV").disabled = true;
    gbi("txtSerieRecibo").disabled = true;
    gbi("txtNroRecibo").disabled = true;
    gbi("txtSerieVale").disabled = true;
    gbi("txtNroVale").disabled = true;
    gbi("txtObservacion").disabled = true;
    gbi("txtMontoPendiente").disabled = true;
    gbi("txtRuc").disabled = true;
    gbi("txtRazonSocial").disabled = true;
    gbi("txtListaDocumento").disabled = true;
    gbi("txtSubTotalSoles").disabled = true;
    gbi("txtIgvSoles").disabled = true;
    gbi("txtTotalSoles").disabled = true;
    gbi("txtBanco").disabled = true;
    gbi("txtEmpleado").disabled = true;
    gbi("txtNroCheque").disabled = true;
    //gbi("btnGrabar").innerHTML = "Aperturar Caja";
    //gbi("btnGrabar").style.display = "";
    //gbi("btnCerrarCaja").style.display = "none";
    gbi("btnAgregarDetalle").style.display = "none";
    gbi("btnCancelarDetalle").style.display = "none";
    document.getElementsByClassName("fa fa-trash-o").disabled = true;
    //$("#btnGrabar").hide();
    //gbi("divMovimientoCaja").querySelectorAll("input, select, button:not(.btn-info)").forEach(item => { item.disabled = true; });
}
function DesbloquearCaja() {
    gbi("txtIDdetalle").disabled = false;
    gbi("txtConcepto").disabled = false;
    gbi("txtTipoOperacion").disabled = false;
    gbi("txtAfectoIGV").disabled = false;
    gbi("txtSerieRecibo").disabled = false;
    gbi("txtNroRecibo").disabled = false;
    gbi("txtSerieVale").disabled = false;
    gbi("txtNroVale").disabled = false;
    gbi("txtObservacion").disabled = false;
    gbi("txtMontoPendiente").disabled = false;
    gbi("txtRuc").disabled = false;
    gbi("txtRazonSocial").disabled = false;
    gbi("txtListaDocumento").disabled = false;
    gbi("txtSubTotalSoles").disabled = false;
    gbi("txtIgvSoles").disabled = false;
    gbi("txtTotalSoles").disabled = false;
    gbi("txtBanco").disabled = false;
    gbi("txtEmpleado").disabled = false;
    gbi("txtNroCheque").disabled = false;
    gbi("btnAgregarDetalle").style.display = "";
    gbi("btnCancelarDetalle").style.display = "";

    //gbi("btnGrabar").style.display = "none";
    //gbi("btnCerrarCaja").style.display = "";
    //$("#btnGrabar").show();
    gbi("divMovimientoCaja").querySelectorAll("input, select, button:not(.btn-info)").forEach(item => { item.disabled = false; });
}
function CargarDetalleCaja(r) {

    var montoIngreso = 0; var montoIngresoEfectivo = 0; var montoIngresoTarjeta = 0; var montoIngresoYape = 0;
    var montoSalida = 0;
    if (r[0] !== '') {
        let newDatos = [];
        let indx = 0;
        r.forEach(function (e) {
            indx++;
            let valor = e.split("▲");
            newDatos.push({
                Index: indx,
                idCajaDetalle: valor[0],
                idCaja: valor[1],
                DescripcionConcepto: valor[2],
                SubTotalNacional: valor[3],
                IGVNacional: valor[4],
                TotalNacional: valor[5],
                TipoOperacion: valor[6],
                idTipoPago: valor[7],
                TipoPago: valor[8],
                idTarjeta: valor[9],
                Tarjeta: valor[10]
            })

            var ingreso; var salida; var ingresoEfectivo;
            ingreso = valor[5];
            ingresoEfectivo = valor[5];
            ingresoTarjeta = valor[5];
            salida = valor[5];
            if (valor[6] == "I") {
                console.log(valor[8]);
                switch (valor[8]) {
                    case "EFECTIVO":
                        montoIngreso = parseFloat(montoIngreso) + parseFloat(ingreso);
                        montoIngresoEfectivo = parseFloat(montoIngresoEfectivo) + parseFloat(ingresoEfectivo);
                        break;
                    case "TARJETA":
                        montoIngresoTarjeta = parseFloat(montoIngresoTarjeta) + parseFloat(ingresoEfectivo); break;
                    default: montoIngresoYape = parseFloat(montoIngresoYape) + parseFloat(ingresoEfectivo); break;
                }
            } else {
                montoSalida = parseFloat(montoSalida) + parseFloat(salida);
            }
            var montoInicial = parseFloat(txtSaldoInicial.value);
            txtMontoIngreso.value = montoIngreso.toFixed(2);
            txtMontoSalida.value = montoSalida.toFixed(2);
            txtSaldoFinal.value = (montoInicial + montoIngreso - montoSalida).toFixed(2);
            gbi("txtIngresoEfectivo").value = montoIngresoEfectivo.toFixed(2);
            gbi("txtIngresoTarjeta").value = montoIngresoTarjeta.toFixed(2);
            gbi("txtIngresoYape").value = montoIngresoYape.toFixed(2);

        });
        listaDetalleCajaReal = newDatos;
        let cols = ["Index", "DescripcionConcepto", "SubTotalNacional", "IGVNacional", "TotalNacional", "TipoPago"];
        loadDataTable(cols, newDatos, "idCajaDetalle", "tb_DetalleCaja", cadButtonOptions(), false);
    }

}
function actualizarDetalle(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se adicionó el detalle a la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó el detalle de la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { }//show_hidden_Formulario(); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        CargarDetalleCaja(listaDatos);
        limpiarTodo();
    }
}
function actualizarDeleteDetalle(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se eliminó el detalle da la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se eliminó el detalle de la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { }//show_hidden_Formulario(); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        CargarDetalleCaja(listaDatos);
        limpiarTodo();
    }
}

function cargarDatosMonedas(r) {
    let monedas = r
    $("#txtMoneda").empty();
    $("#txtMoneda").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        monedas.forEach(element => {
            $("#txtMoneda").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
    document.getElementById("txtMoneda").selectedIndex = "1";
} function cargarDatosConceptos(r) {
    let conceptos = r;
    $("#txtConcepto").empty();
    $("#txtConcepto").append(`<option value="">Seleccione</option>`);

    if (r && r.length > 0) {
        conceptos.forEach(element => {
            $("#txtConcepto").append(`<option value="${element.split('▲')[0]}">${element.split('▲')[1]}</option>`);
        });
    }
}
function CambiarConcepto() {
    var c1 = gbi("concepto1");
    var c2 = gbi("concepto2");
    var c3 = gbi("concepto3");
    var c4 = gbi("concepto4");
    var c5 = gbi("concepto5");
    var idConcepto = gbi("txtConcepto").value;
    switch (idConcepto) {
        case "10": case "2": c1.style.display = ""; c2.style.display = "none";
            c3.style.display = "none"; c4.style.display = "none"; c5.style.display = "none";
            break;
        case "92": case "93": c1.style.display = "none"; c2.style.display = "";
            c3.style.display = "none"; c4.style.display = "none"; c5.style.display = "none";
            break;
        case "9": case "7": c1.style.display = "none"; c2.style.display = "none";
            c3.style.display = "none"; c4.style.display = ""; c5.style.display = "none";
            break;
        case "96": c1.style.display = "none"; c2.style.display = "none";
            c3.style.display = "none"; c4.style.display = ""; c5.style.display = "none";
            break;
        case "83": c1.style.display = "none"; c2.style.display = "none";
            c3.style.display = "none"; c4.style.display = "none"; c5.style.display = "";
            break;
        default: c1.style.display = "none"; c2.style.display = "none";
            c3.style.display = "none"; c4.style.display = "none"; c5.style.display = "none";
            break;
    }
    for (var i = 0; i < listaConcepto.length; i++) {
        var tipoConcepto = listaConcepto[i].split("▲");
        if (idConcepto == tipoConcepto[0]) {
            txtTipoOperacion.value = tipoConcepto[3];
            txtAfectoIGV.value = tipoConcepto[2];
            switch (tipoConcepto[3]) {
                case "INGRESO": gbi("cboTipoPago").disabled = false; gbi("divTarjeta").display = ""; break;
                case "SALIDA": gbi("cboTipoPago").disabled = true; gbi("divTarjeta").display = "none"; gbi("cboTipoPago").value = "1"; break;
            }
        }
    }

    var TipoPago = gbi("cboTipoPago").value;
    if (TipoPago == "1") {
        gbi("divOperacion").style.display = "none";
        gbi("divTarjeta").style.display = "none";
    } else {
        gbi("divOperacion").style.display = "";
        gbi("divTarjeta").style.display = "";
    }
}

txtTotalSoles.onkeyup = function () {
    var mtotal = txtTotalSoles.value;
    var msubtotal = mtotal / 1.18;
    txtSubTotalSoles.value = (mtotal / 1.18).toFixed(2);
    txtIgvSoles.value = (mtotal - msubtotal).toFixed(2);
}
//Eliminar  
function eliminar(id) {
    swal({
        title: 'Desea Eliminar la Caja ? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "Caja/Eliminar?idCaja=" + id;
                enviarServidor(url, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó la Caja', 'error');
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó la Caja';
            tipo = 'success';
            listaDatos = data[2].split('▼');
            listar();
        }
        else {
            mensaje = data[1];
            tipo = 'error';
        }
    } else {
        mensaje = 'No hubo respuesta';
    }
    mostrarRespuesta(res, mensaje, tipo);
}

//DETALLE CAJA
function mostrarDetalleCaja(opcion, idDetalle) {
    var url = "Caja/ObtenerDatosCajaDetallexID/?id=" + idDetalle;
    enviarServidor(url, CargarCajaDetalles);
}
function CargarCajaDetalles(rpta) {
    if (rpta != "") {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == 'OK') {
            var listaDCaja = listas[2].split('▲');
            listaConceptosCaja = listas[3].split('▼');
            listaDEmpleado = listas[4].split('▼');
            listaDBanco = listas[5].split('▼');

            //Asignar valores a controles
            gbi("txtConcepto").value = listaDCaja[1];//Concepto            
            adc(listaConceptosCaja, listaDCaja[1], "txtConcepto", 1);
            CambiarConcepto();
            gbi("txtIDdetalle").value = listaDCaja[0];//idCajaDetalle            
            gbi("txtTipoOperacion").value = listaDCaja[2];//TipoOperacion
            //gbi("txtAfectoIGV").value = listaDCaja[3];//AfectoIgv

            gbi("txtSerieRecibo").value = listaDCaja[6]//SerieReci
            gbi("txtNroRecibo").value = listaDCaja[8];//NroRecibo
            //gbi("txtListaDocumento").dataset.id = listaDCaja[10];//Documento 

            switch (listaDCaja[1]) {
                case "9": gbi("txtRazonSocial").value = listaDCaja[12];//RazonSocial
                    break;
                case "10": gbi("txtEmpleado").value = listaDCaja[12];//RazonSocial
                    break;
            }
            gbi("txtIdCompraVenta").dataset.id = listaDCaja[11];//CompraVenta
            gbi("txtRazonSocial").value = listaDCaja[12];//RazonSocial
            gbi("txtRuc").value = listaDCaja[13];//Ruc
            gbi("txtMontoPendiente").value = listaDCaja[14];//MontoPendient
            gbi("txtObservacion").value = listaDCaja[15];//Observacion
            gbi("txtSubTotalSoles").value = listaDCaja[16];//SubTotal
            gbi("txtIgvSoles").value = listaDCaja[17];//Igv
            gbi("txtTotalSoles").value = listaDCaja[18];//Total
            gbi("txtNroOperacion").value = listaDCaja[7];//NroOperacion
            gbi("txtListaDocumento").value = listaDCaja[21];//Documento 
            switch (listaDCaja[1]) {
                case "7": case "8": case "14": adc(listaDEmpleado, listaDCaja[3], "txtEmpleado", 1); break;//gbi("txtEmpleado").dataset.id = listaDCaja[3]//Empleado
                case "51": adc(listaDBanco, listaDCaja[9], "txtBanco", 1); //gbi("txtBanco").dataset.id = listaDCaja[9];//Banco
                default:
            }
            gbi("cboTipoPago").value = listaDCaja[19];
            if (listaDCaja[19] == "1") {
                gbi("divOperacion").style.display = "none"; gbi("divTarjeta").style.display = "none";
            } else { gbi("divOperacion").style.display = ""; gbi("divTarjeta").style.display = ""; }
            gbi("cboTarjeta").value = listaDCaja[22];
            gbi("btnAgregarDetalle").innerHTML = "Actualizar";
        }
        else {
            document.getElementById("error").innerHTML = mensaje;
        }
    }
}
function eliminarDetalle(id, idDetalle) {
    swal({
        title: 'Desea Eliminar el Movimiento de Caja ? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = "Caja/EliminarDetalle?idCaja=" + id + "&idCajaDetalle=" + idDetalle;
                enviarServidor(url, eliminarListarDetalle);
            } else {
                swal('Cancelado', 'No se eliminó el Movimiento de Caja', 'error');
            }
        });
}
function eliminarListarDetalle(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó el Movimiento de Caja';
            tipo = 'success';
            listaDatosDetalle = data[2].split('▼');
        }
        else {
            mensaje = data[1];
            tipo = 'error';
        }
    } else {
        mensaje = 'No hubo respuesta';
    }
    CargarDetalleCaja(listaDatosDetalle);
    mostrarRespuesta(res, mensaje, tipo);
    var DetalleCaja = gbi("tb_DetalleCaja").innerHTML;
    if (DetalleCaja == "") {
        var MonInicio = gbi("txtInicial").value;
        gbi("txtIngresoEfectivo").value = 0.00;
        gbi("txtSaldoFinal").value = MonInicio;
    }
}

//Carga con botones de Modal sin URL (Con datos dat[])
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
function cbmuDoc(ds, t, tM, tM2, cab, u, m) {
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

    if (comboConcepto == "conceptoCaja") {
        conceptoId = id;
        Conceptos();
    } else {
        switch (combox) {
            case "cargo": tipoEmpleadoId = id; break;
            case "venta": txtIdCompraVenta.value = gbi("md" + num + "-0").innerHTML;
                txtMontoPendiente.value = gbi("md" + num + "-4").innerHTML; txtRuc.value = gbi("md" + num + "-1").innerHTML;
                txtTotalSoles.value = gbi("md" + num + "-4").innerHTML; txtListaDocumento.value = gbi("md" + num + "-3").innerHTML;
                CalcularMontos();
                break;
            case "cita": gbi("txtCita").value = gbi("md" + num + "-2").innerHTML;
                gbi("txtCostoCita").value = gbi("md" + num + "-4").innerHTML; gbi("txtPaciente").value = gbi("md" + num + "-3").innerHTML;
                gbi("txtObservacion").value = gbi("md" + num + "-5").innerHTML; gbi("txtTotalSoles").value = gbi("md" + num + "-4").innerHTML;
                break;
        }
    }
}
function accionModal2(url, tr, id) {
    switch (txtModal.id) {
        case "txtEmpleado":
            return gbi("txtEmpleado");
            break;
        case "txtConcepto":
            CambiarConcepto();
            break;
        case "txtRazonSocial":
            CalcularMontos();
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
function cargarListaConcepto(rpta) {
    if (rpta != "") {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = data[1];
        listaDatosModal = data[2].split("▼");
        listaConceptosCaja = listaDatosModal;
        mostrarModal(cabecera_Modal, listaDatosModal);
    }
}
//
function adc(l, id, ctrl, c) {
    var ind;
    for (var i = 0; i < l.length; i++) {
        if (l[i].split('▲')[0] == id) {
            ind = i;
            break;
        }
    }
    gbi(ctrl).value = l[ind].split('▲')[c];
    gbi(ctrl).dataset.id = id;
}
//Tabla principal
function crearTablaCaja(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-1'>";
                break;
            case 6:
                contenido += "              <div class='col-12 col-md-1'>";
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
function mostrarMatrizCaja(matriz, cabeceras, tabId, contentID) {
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
                        case 4:
                            contenido2 += "col-md-1'>";
                            break;
                        case 6:
                            contenido2 += "col-md-1'>";
                            break;
                        default:
                            contenido2 += "col-md-2'>";
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
//Modal
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
            case 3:
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
            switch (cabeceras.length) {
                case 2:
                    tipoColDes = 12;
                    break;
                case 3:
                    tipocol = 4;
                    tipoColDes = 8;
                    break;

                case 4:
                    tipocol = 3
                    tipoColDes = 4
                    break
                case 5:
                    tipocol = 2
                    tipoColDes = 4;
                    break;
                case 6:
                    tipocol = 2
                    tipoColDes = 4;
                    break;
                default:

            }
            var dat = [];
            for (var i = 0; i < nRegistros; i++) {
                if (i < nRegistros) {
                    var contenido2 = "<div class='row panel salt' id='numMod" + i + "' tabindex='" + (100 + i) + "' style='padding:3px 20px;margin-bottom:1px;cursor:pointer;'>";
                    for (var j = 0; j < cabeceras.length; j++) {
                        switch (j) {
                            case 0:
                                contenido2 += "<div class='col-12 col-md-2' style='display:none;'>";
                                break;
                            case 1:
                                contenido2 += ("<div class='col-12 col-md-" + (cabeceras.length == 2 ? tipoColDes : tipocol) + "'>");
                                break;
                            case 3:
                                contenido2 += "<div class='col-12 col-md-3'>";
                                break;
                            default:
                                contenido2 += ("<div class='col-12 col-md-" + tipocol + "'>");
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
function CalcularMontos() {
    var mtotal = parseFloat(gbi("txtTotalSoles").value).toFixed(2);
    var msubtotal = mtotal / 1.18;
    gbi("txtSubTotalSoles").value = (mtotal / 1.18).toFixed(2);
    gbi("txtIgvSoles").value = (mtotal - msubtotal).toFixed(2);
}
function cajasDescargarPDF() {
    var texto = "";
    var columns = cabeceras;
    var data = [];
    for (var i = 0; i < matriz.length; i++) {
        data[i] = matriz[i];
    }
    var doc = new jsPDF('l', 'pt', "a4");
    var width = doc.internal.pageSize.width;
    var height = doc.internal.pageSize.height;
    var fec = new Date();
    var d = fec.getDate().toString().length == 2 ? fec.getDate() : ("0" + fec.getDate());
    var m = (fec.getMonth() + 1).length == 2 ? (fec.getMonth() + 1) : ("0" + (fec.getMonth() + 1));
    var y = fec.getFullYear();
    //Datos Cabecera de Página
    var h = fec.getHours().toString().length == 2 ? fec.getHours() : ("0" + fec.getHours());
    var mm = fec.getMinutes().toString().length == 2 ? fec.getMinutes() : ("0" + fec.getMinutes());
    var s = fec.getSeconds().toString().length == 2 ? fec.getSeconds() : ("0" + fec.getSeconds());
    var fechaImpresion = d + '-' + m + '-' + y + ' ' + h + ':' + mm + ':' + s;
    doc.setFont('helvetica');
    doc.setFontSize(10);
    doc.setFontType("bold");
    doc.text("Dermosalud S.A.C", 30, 30);
    doc.setFontSize(8);
    doc.setFontType("normal");
    doc.text("Ruc:", 30, 40);
    doc.text("20565643143", 50, 40);
    doc.text("Dirección:", 30, 50);
    doc.text("Avenida Manuel Cipriano Dulanto 1009, Cercado de Lima", 70, 50);
    doc.setFontType("bold");
    doc.text("Fecha Impresión", width - 90, 40)
    doc.setFontType("normal");
    doc.setFontSize(7);
    doc.text(fechaImpresion, width - 90, 50)
    doc.setFontSize(14);
    doc.setFontType("bold");
    //Titulo de Documento y datos
    doc.text("REPORTE DE CAJA", width / 2, 95, "center");// + gbi("txtRequerimiento").value
    var xic = 140;
    var altc = 12;
    doc.setFontType("bold");
    doc.setFontSize(7);
    doc.text("USUARIO: " + gbi("txtUsuarioResp").value, 30, xic);
    doc.text("FECHA INICIO: " + gbi("dtpFechaInicio").value, 160, xic);
    doc.text("FECHA CIERRE: " + gbi("dtpFechaCierre").value, 260, xic);

    doc.text("SALDO INICIAL: " + gbi("txtSaldoInicial").value, 30, xic + altc * 1);
    doc.text("MONTO INICIAL: " + gbi("txtInicial").value, 160, xic + altc * 1);
    doc.text("EFECTIVO: " + gbi("txtIngresoEfectivo").value, 260, xic + altc * 1);
    doc.text("TARJETA: " + gbi("txtIngresoTarjeta").value, 360, xic + altc * 1);
    doc.text("SALIDA: " + gbi("txtMontoSalida").value, 460, xic + altc * 1);
    doc.text("SALDO FINAL: " + gbi("txtSaldoFinal").value, 560, xic + altc * 1);

    doc.roundedRect(26, xic - altc, width - 52, altc * 3, 3, 3);

    //Inicio de Detalle
    //Crear Cabecera
    var xid = 190;
    var xad = 12;
    agregarCabeceras();
    function agregarCabeceras() {
        doc.setFontType("bold");
        doc.setFontSize(7);
        doc.text("ITEM", 30, xid);
        doc.text("DOCUMENTO", 70, xid);
        doc.text("CONCEPTO", 150, xid);
        doc.text("CLIENTE", 300, xid);
        doc.text("FORMA PAGO", 500, xid);
        doc.text("SALIDA", width - 198, xid, 'right');
        doc.text("INGRESO", width - 98, xid, 'right');
        doc.line(30, xid + 3, width - 30, xid + 1);
        doc.setFontType("normal");
        doc.setFontSize(6.5);
    }

    //Crear Detalle
    var n = 0;
    listaDetalleCajaPDF.forEach((item, index) => {
        var valores = item.split("▲");
        doc.text((index + 1).toString(), 30, xid + xad + (n * xad));//item
        doc.text((valores[24] + "-" + valores[25]), 70, xid + xad + (n * xad));//documento
        doc.text(valores[8], 150, xid + xad + (n * xad));//concepto
        doc.text(valores[20], 300, xid + xad + (n * xad));//cliente
        doc.text(valores[38], 500, xid + xad + (n * xad));//forma pago

        if (valores[33] == "I") {//ingreso
            doc.text(parseFloat(valores[41]).toFixed(2), width - 200, xid + xad + (n * xad), 'right');//salida
            doc.text(parseFloat(valores[15]).toFixed(2), width - 100, xid + xad + (n * xad), 'right');//ingreso
        } else {//salida
            doc.text(parseFloat(valores[15]).toFixed(2), width - 200, xid + xad + (n * xad), 'right');//salida
            doc.text(parseFloat(valores[40]).toFixed(2), width - 100, xid + xad + (n * xad), 'right');//ingreso
        }

        if (xid + xad + (n * xad) > height - 30) {
            doc.addPage();
            n = 0;
            xid = 30;
            agregarCabeceras();
            xid = 35;
        }
        n += 1;
    });
    doc.autoPrint();
    var iframe = document.getElementById('iframePDF');
    iframe.src = doc.output('dataurlstring');
}
//cerrarcja
function actualizarCerrarCaja(rpta) { //rpta es mi lista de colores
    if (rpta != "") { //validar cuando respuesta sea vacio
        var data = rpta.split("↔");
        var res = data[0];
        var mensaje = "";
        var tipo = "";
        var txtCodigo = document.getElementById("txtID");
        var codigo = txtCodigo.value;

        if (codigo.length == 0) {//ADICIONAR
            if (res == "OK") {
                mensaje = "Se cerró la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        else {//ACTUALIZAR
            if (res == "OK") {
                mensaje = "Se actualizó la Caja";
                tipo = "success";
            }
            else {
                mensaje = data[1];
                tipo = "error";
            }
        }
        if (res == "OK") { show_hidden_Formulario(true); }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split("▼");
        listar(listaDatos);
    }
}