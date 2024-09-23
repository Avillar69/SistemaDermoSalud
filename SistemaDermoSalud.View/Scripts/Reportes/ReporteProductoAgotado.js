var cabeceras = ["Fecha", "Concepto", "Observación", "Total", "Forma de Pago"];

var url = "/Reportes/ObtenerDatos_ProductosxAgotarse";
enviarServidor(url, mostrarLista);

function mostrarLista(rpta) {
    console.log(rpta);
    if (rpta != "") {
        var listas = rpta.split("↔");
        var Resultado = listas[0];
        if (Resultado == "OK") {
            listaDatos = listas[1].split("▼");
            listar(listaDatos);
        }
        else {
            mostrarRespuesta(Resultado, mensaje, "error");
        }
    }
}

function listar(r) {
    if (r[0] !== '') {
        let newDatos = [];
        r.forEach(function (e) {
            let valor = e.split("▲");
            newDatos.push({
                marca: valor[0],
                codigoProducto: valor[1],
                producto: valor[2],
                stock: valor[3]
            })
        });
        let cols = ["marca", "codigoProducto", "producto", "stock"];
        loadDataTable(cols, newDatos, "marca", "tbDatos", cadButtonOptions(), false);
    }

}