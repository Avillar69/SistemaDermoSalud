using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Ventas
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            return PartialView();
        }
        public string ObtenerDatos()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;

            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            Ma_MonedaBL oMa_MonedaBL = new Ma_MonedaBL();
            Ma_FormaPagoBL oMa_FormaPagoBL = new Ma_FormaPagoBL();
            Ma_TipoComprobanteBL oMa_TipoComprobanteBL = new Ma_TipoComprobanteBL();
            Ma_Clase_ArticuloBL oMa_Clase_ArticuloBL = new Ma_Clase_ArticuloBL();
            VN_PedidosBL oVN_PedidosBL = new VN_PedidosBL();
            //COM_RequerimientoBL ocCOM_RequerimientoBL = new COM_RequerimientoBL();

            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "C");
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_FormaPagoDTO> oListaFormaPago = oMa_FormaPagoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<VEN_PedidosDTO> oListaPedido = oVN_PedidosBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);

            //ResultDTO<COM_OrdenCompraDTO> oListaDocumentoCompras = ocCOM_RequerimientoBL.ListarRequerimiento();
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            // string listaLocal = Serializador.rSerializado(eSEGUsuario.oListaLocales, new string[] { "idLocal", "Descripcion" });
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string listaFormaPago = Serializador.rSerializado(oListaFormaPago.ListaResultado, new string[] { "idFormaPago", "Descripcion" });
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });
            string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            string listaPedido = Serializador.rSerializado(oListaPedido.ListaResultado, new string[]
            {"idPedido","FechaOrdenCompra","NumPedido","ProveedorDocumento","ProveedorRazon","Estado"});
            //string listaDocumentoCompra = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[] 
            // { "idDocumentoCompra","FechaDocumento","SerieDocumento", "NumDocumento","ProveedorRazon","SubTotalNacional","IGVNacional","TotalNacional" });

            //↔{9}
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}", "OK", listaSocios, listaMoneda, listaFormaPago, listaComprobantes, listaTipoCompra, listaPedido, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_PedidosBL oVN_PedidosBL = new VN_PedidosBL();
            ResultDTO<VEN_PedidosDTO> olistaPedido = oVN_PedidosBL.ListarRangoFecha(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaPedido = Serializador.rSerializado(olistaPedido.ListaResultado, new string[]
            { "idPedido","FechaOrdenCompra","NumPedido","ProveedorDocumento","ProveedorRazon", "TotalNacional","Estado" });
            return String.Format("{0}↔{1}↔{2}", olistaPedido.Resultado, olistaPedido.MensajeError, listaPedido);
        }

        public string Grabar(VEN_PedidosDTO oCOM_OrdenCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_PedidosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_PedidosBL oVN_PedidosBL = new VN_PedidosBL();
            if (oCOM_OrdenCompraDTO.idPedido == 0)
            {
                oCOM_OrdenCompraDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCOM_OrdenCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oCOM_OrdenCompraDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oVN_PedidosBL.UpdateInsert(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);

            List<VEN_PedidosDTO> lstCOM_OrdenCompraDTO = oResultDTO.ListaResultado;
            string listaOrdenCompra = Serializador.rSerializado(lstCOM_OrdenCompraDTO, new string[]
            {"idPedido","FechaOrdenCompra","NumPedido","ProveedorDocumento","ProveedorRazon", "TotalNacional","Estado"});
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaOrdenCompra);
        }
        public string Eliminar(VEN_PedidosDTO oCOM_OrdenCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<VEN_PedidosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_PedidosBL oVN_PedidosBL = new VN_PedidosBL();
            oCOM_OrdenCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;

            oResultDTO = oVN_PedidosBL.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);
            string lista_Pedido = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idPedido","FechaOrdenCompra","NumPedido","ProveedorDocumento","ProveedorRazon", "TotalNacional","Estado"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Pedido);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            VN_PedidosBL oVN_PedidosBL = new VN_PedidosBL();
            ResultDTO<VEN_PedidosDTO> oListaPedido = oVN_PedidosBL.ListarxID(id);
            //Direcciones de Proveedor
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaPedido.ListaResultado[0].idProveedor);
            string listaPedido = Serializador.rSerializado(oListaPedido.ListaResultado, new string[] { });
            string listaSocio_Direccion = Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[] { "idDireccion", "Direccion" });
            string listaOrdenCompraDetalle = Serializador.rSerializado(oListaPedido.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}", "OK", listaPedido, listaSocio_Direccion, listaOrdenCompraDetalle);
        }

        public string ListarOrdenCompra(int ordenCompra)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            VN_PedidosBL oVN_PedidosBL = new VN_PedidosBL();
            //listas
            ResultDTO<VEN_PedidosDTO> oResultDTO = oVN_PedidosBL.ListarOrdenCompra(ordenCompra);
            //cadenas
            string listaCOM_OrdenCompraBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaCOM_OrdenCompraBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
            "idPedido", "FechaOrdenCompra", "NumPedido","Estado"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaCOM_OrdenCompraBL);
        }
    }
}