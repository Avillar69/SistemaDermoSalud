var cabeceras = ["idReceta", "Fecha", "N° Receta", "Cliente"];
var listaDatos;
var matriz = [];
let count = 30;
var url = "Recetas/ObtenerDatos";
enviarServidor(url, mostrarLista);
configBM();
$("#txtFilFecIn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$("#txtFilFecFn").datetimepicker({
    format: 'DD-MM-YYYY',
});
$('#datepicker-range').datetimepicker({ format: 'DD-MM-YYYY' });
var fechaFiltro = new Date();
$('#txtFilFecIn').data("DateTimePicker").date(new Date(fechaFiltro.getFullYear(), fechaFiltro.getMonth(), 1));
$('#txtFilFecFn').data("DateTimePicker").date(fechaFiltro);

function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            listaSocios = listas[1].split("▼");            
            listaDatos = listas[2].split("▼");
            var fechaInicio = listas[3];
            var fechaFin = listas[4];                       
            gbi("txtFilFecIn").value = fechaInicio;
            gbi("txtFilFecFn").value = fechaFin;
            gbi("txtFecha").value = fechaFin;
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
        listar();
    }
    reziseTabla();
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
            show_hidden_Formulario(true);
            lblTituloPanel.innerHTML = "Nueva Receta";
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Documento de Venta";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario(true);
            gbi("btnEnviarSunat").style.display = "";
            break;
    }
}
function TraerDetalle(id) {
    var url = 'Recetas/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function configBM() {

}