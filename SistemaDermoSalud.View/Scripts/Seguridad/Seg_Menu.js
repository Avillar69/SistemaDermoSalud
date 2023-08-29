var cabeceras = ['idMenu', 'Descripcion', 'Vista', 'Controller', 'Fecha Mod.', 'Estado'];
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
//Inicializando
var url = 'Seg_Menu/ObtenerDatos';
enviarServidor(url, mostrarLista);
configurarBotonesModal();

//Declarar variables para controles var nombreVariable = document.getElementbyId(idControl);
function mostrarLista(rpta) {
    crearTablaCompras(cabeceras, "cabeTabla");
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == 'OK') {
            listaDatos = listas[2].split('▼');
            listar();
        }
        else {
            mostrarRespuesta(Resultado, mensaje, 'error');
        }
    }
}


function crearTablaCompras(cabeceras, div) {
    var contenido = "";
    nCampos = cabeceras.length;
    contenido += "";
    contenido += "          <div class='row panel bg-info d-none d-md-flex' style='color:white;margin-bottom:5px;padding:5px 20px 0px 20px;'>";
    for (var i = 0; i < nCampos; i++) {
        switch (i) {
            case 0:
                contenido += "              <div class='col-12 col-md-2' style='display:none;'>";
                break;
            case 3: case 1: case 5: case 6: case 7:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 4:
                contenido += "              <div class='col-12 col-md-2'>";
                break;
            case 2:
                contenido += "              <div class='col-12 col-md-3'>";
                break;
            default:
                contenido += "              <div class='col-12 col-md-1'>";
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

function listar() {
    configurarFiltro();
    matriz = crearMatriz(listaDatos);
    configurarFiltro(cabeceras);
    mostrarMatriz(matriz, cabeceras, "divTabla", "contentPrincipal");
    configurarBotonesModal();
    reziseTabla();
    $(window).resize(function () {
        reziseTabla();
    });
    //configurarFiltro();
    //matriz = crearMatriz(listaDatos);
    //configurarPaginacion(matriz, registrosPagina, paginasBloque, indiceActualBloque);
    //if (indiceActualPagina > 0) { mostrarMatrizMenu(indicePagina, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina); } else { mostrarMatrizMenu(indicePagina, registrosPagina, paginasBloque, indiceActualBloque, matriz, 0); }
    //configurarBotonesModal();
}

function configurarFiltro() {
    var texto = document.getElementById("txtFiltro");
    texto.onkeyup = function () {
        matriz = crearMatriz(listaDatos);
        configurarPaginacion(matriz, registrosPagina, paginasBloque, indiceActualBloque);
        indiceActualBloque = 0;
        indiceActualPagina = 0;;
        mostrarMatrizMenu(0, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina);
    };

}
function mostrarMatrizMenu(indicePagina, registrosPagina, paginasBloque, indiceActualBloque, matriz, indiceActualPagina) {
    var esBloque = false;
    var contenido = "";
    var tbTabla = document.getElementById('divTabla');
    var nRegistros = matriz.length;
    if (nRegistros > 0) {
        esBloque = (indicePagina < 0);
        var indiceUltimaPagina = Math.floor(nRegistros / registrosPagina);
        if (nRegistros % registrosPagina == 0) indiceUltimaPagina--;
        var indiceUltimoBloque = Math.floor(nRegistros / (registrosPagina * paginasBloque));
        if (nRegistros % (registrosPagina * paginasBloque) == 0) indiceUltimoBloque--;
        if (esBloque) {
            switch (indicePagina) {
                case -1:
                    indicePagina = 0;
                    indiceActualBloque = 0;
                    break;
                case -2:
                    if (indiceActualBloque > 0) {

                        indiceActualBloque--;
                        indicePagina = indiceActualBloque * paginasBloque;
                    }
                    break;
                case -3:
                    if (indiceActualBloque < indiceUltimoBloque) {
                        indiceActualBloque++;
                        indicePagina = indiceActualBloque * paginasBloque;
                    }
                    break;
                case -4:
                    indicePagina = indiceUltimaPagina;
                    indiceActualBloque = indiceUltimoBloque;
                    break;
            }
        }
        indiceActualPagina = indicePagina;
        nRegistros = matriz.length;
        var nCampos = matriz[0].length;
        var inicio = indicePagina * registrosPagina;
        var fin = inicio + registrosPagina;
        tbTabla.innerHTML = "";
        for (var i = inicio; i < fin; i++) {

            if (i < nRegistros) {

                contenido += "<div class='panel panel-default' style='margin-bottom:10px;'>";
                contenido += "  <div class='panel-heading' role='tab' id='heading" + i + "'>";
                contenido += "  <label>";
                contenido += "      <h5 class='panel-title'>";
                contenido += "          <a role='button' data-toggle='collapse' data-parent='#accordion' href='#collapse" + i + "' aria-expanded='false' aria-controls='collapse" + i + "'>";
                contenido += matriz[i][3];
                contenido += "          </a>";
                contenido += "      </h5>";
                contenido += "  </label>";
                contenido += "  </div>";
                contenido += "  <div id='collapse" + i + "' class='panel-collapse collapse in' role='tabpanel' aria-labelledby='heading" + i + "'>";
                contenido += "      <div class='panel-body'  style='padding: 10px 20px 20px 20px'>";
                contenido += "          <div class='row' style='padding-left:20px;padding-right:20px'>";
                contenido += "              <div class='col-xs-12 col-md-3'>";
                contenido += "                  <label style='padding-top:5px;padding-bottom:5px;'> Código : " + matriz[i][1] + "</label>";
                contenido += "              </div>";
                contenido += "              <div class='col-xs-12 col-md-3'>";
                contenido += "                  <label style='padding-top:5px;padding-bottom:5px;'> Rol : " + matriz[i][2] + "</label>";
                contenido += "              </div>";
                contenido += "              <div class='col-xs-12 col-md-4'>";
                contenido += "                  <label style='padding-top:5px;padding-bottom:5px;'> Usuario : " + matriz[i][3] + "</label>";
                contenido += "              </div>";
                contenido += "              <div class='col-xs-12 col-md-2'>";
                contenido += "                  <div class='row'>";
                contenido += "                      <div class='col-xs-12'>";
                contenido += "                          <button type='button' class='btn btn-sm waves-effect waves-light btn-danger pull-right m-l-10' onclick='eliminar(\"" + matriz[i][0] + "\")'> <i class='fa fa-trash-o'></i> </button>";
                contenido += "                          <button type='button' class='btn btn-sm waves-effect waves-light btn-success pull-right' onclick='mostrarDetalle(2, \"" + matriz[i][0] + "\")'> <i class='fa fa-pencil'></i></button>";
                contenido += "                      </div>";
                contenido += "                  </div>";
                contenido += "              </div>";
                contenido += "          </div>";
                contenido += "      </div>";
                contenido += "  </div>";
                contenido += "</div>";
            }
            else break;
        }
    }
    tbTabla.innerHTML = contenido;
    if (esBloque) configurarPaginacion(matriz, registrosPagina, paginasBloque, indiceActualBloque);

}
function mostrarDetalle(opcion, id) {
    var lblTituloPanel = document.getElementById('lblTituloPanel');
    limpiarTodo();
    switch (opcion) {
        case 1:
            //AbrirModal('modal-form');
            show_hidden_Formulario();
            lblTituloPanel.innerHTML = "Nuevo Menu";//Titulo Insertar
            break;
        case 2:
            lblTituloPanel.innerHTML = "Editar Menu";//Titulo Modificar
            TraerDetalle(id);
            show_hidden_Formulario();
            break;
    }
}
function TraerDetalle(id) {
    // url = Controlador / Action ?id= + id
    var url = 'Seg_Menu/ObtenerDatosxID?id=' + id;
    enviarServidor(url, CargarDetalles);
}
function CargarDetalles(rpta) {
    if (rpta != '') {
        var listas = rpta.split('↔');
        var Resultado = listas[0];
        var mensaje = listas[1];
        if (Resultado == 'OK') {
            //AbrirModal('modal-form');
            var listaDetalle = listas[2].split('▲');        
            adt(listaDetalle[2], "txtDescripcion");
            adt(listaDetalle[4], "txtidMenuPadre");
            adt(listaDetalle[5], "txtNivel");
            adt(listaDetalle[6], "txtPosicion");
            adt(listaDetalle[7], "txtAction");
            adt(listaDetalle[8], "txtController");
            adt(listaDetalle[3], "txtIcono");
            adt(listaDetalle[10], "txtFechaModificacion");
        }
        else {
            mostrarRespuesta(Resultado, mensaje, 'error');
        }
    }
}
function configurarBotonesModal() {
    var btnGrabar = document.getElementById('btnGrabar');
    btnGrabar.onclick = function () {
        if (validarFormulario()) {
            var url = '';
            var frm = new FormData();


            enviarServidorPost(url, actualizarListar, frm);
        }
    };
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () {
        show_hidden_Formulario();
    }
}
function limpiarTodo() {
    //Aqui Ingresar todos los campos a limpiar || validarCombo || validarTexto
}
function validarFormulario() {
    var error = true;
    //Aqui Ingresar todos los campos a validar || validarCombo || validarTexto
    return error;
}
function actualizarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        var tipo = '';
        var txtCodigo = document.getElementById('txtId');
        var codigo = txtCodigo.value;
        if (codigo.length == 0) {
            if (res == 'OK') {
                mensaje = 'Se adicionó el centro de costo';
                tipo = 'success';
                CerrarModal('modal-form');
            }
            else {
                mensaje = data[1];
                tipo = 'error';
            }
        }
        else {
            if (res == 'Exito') {
                mensaje = 'Se actualizó el centro de costo';
                tipo = 'success';
                CerrarModal('modal-form');
            }
            else {
                mensaje = data[1];
                tipo = 'error';
            }
        }
        mostrarRespuesta(res, mensaje, tipo);
        listaDatos = data[2].split('▼');
        listar();
    }
}
function eliminar(id) {
    swal({
        title: 'Desea Eliminar el Menu ? ',
        text: 'No podrá recuperar los datos eliminados.',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Si, Eliminalo!',
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {
                var url = '';
                enviarServidor(url, eliminarListar);
            } else {
                swal('Cancelado', 'No se eliminó el menu', 'error');
            }
        });
}
function eliminarListar(rpta) {
    if (rpta != '') {
        var data = rpta.split('↔');
        var res = data[0];
        var mensaje = '';
        if (res == 'OK') {
            mensaje = 'Se eliminó el Menu';
            tipo = 'success';
        }
        else {
            mensaje = data[1]
            tipo = 'error';
        }
    } else {
        mensaje = 'No hubo respuesta';
    }
    mostrarRespuesta(res, mensaje, tipo);
    listaDatos = data[2].split('▼');
    listar();
}
