using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Compras
{
    public class OrdenCompraController : Controller
    {
        // GET: OrdenCompra
        public ActionResult Index()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
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
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();
            //COM_RequerimientoBL ocCOM_RequerimientoBL = new COM_RequerimientoBL();

            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "P");
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_FormaPagoDTO> oListaFormaPago = oMa_FormaPagoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<COM_OrdenCompraDTO> oListaOrdenCompras = oCOM_OrdenCompraBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);

            //ResultDTO<COM_OrdenCompraDTO> oListaDocumentoCompras = ocCOM_RequerimientoBL.ListarRequerimiento();
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            // string listaLocal = Serializador.rSerializado(eSEGUsuario.oListaLocales, new string[] { "idLocal", "Descripcion" });
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string listaFormaPago = Serializador.rSerializado(oListaFormaPago.ListaResultado, new string[] { "idFormaPago", "Descripcion" });
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });
            string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            string listaOrdenCompra = Serializador.rSerializado(oListaOrdenCompras.ListaResultado, new string[]
            { "idOrdenCompra", "FechaOrdenCompra", "NumOrdenCompra", "ProveedorDocumento", "ProveedorRazon", "TotalNacional", "Estado" });
            //string listaDocumentoCompra = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[] 
            // { "idDocumentoCompra","FechaDocumento","SerieDocumento", "NumDocumento","ProveedorRazon","SubTotalNacional","IGVNacional","TotalNacional" });

            //↔{9}
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}", "OK", listaSocios/*, listaLocal*/, listaMoneda, listaFormaPago, listaComprobantes, listaTipoCompra, listaOrdenCompra, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();
            ResultDTO<COM_OrdenCompraDTO> olistaOrdenCompras = oCOM_OrdenCompraBL.ListarRangoFecha(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaOrdenCompra = Serializador.rSerializado(olistaOrdenCompras.ListaResultado, new string[]
            { "idOrdenCompra", "FechaOrdenCompra", "NumOrdenCompra", "ProveedorDocumento", "ProveedorRazon", "TotalNacional", "Estado"});
            return String.Format("{0}↔{1}↔{2}", olistaOrdenCompras.Resultado, olistaOrdenCompras.MensajeError, listaOrdenCompra);
        }
        /*public string ObtenerAlmacen(int pL)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_LocalBL oMa_LocalBL = new Ma_LocalBL();
            ResultDTO<Ma_LocalDTO> oResultDTO_Local = oMa_LocalBL.ListarxID(pL);
            string listaAlmacen = Serializador.rSerializado(oResultDTO_Local.ListaResultado[0].oListaAlmacen, new string[] { "idAlmacen", "Descripcion" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO_Local.Resultado, oResultDTO_Local.MensajeError, listaAlmacen);
        }*/
        public string Grabar(COM_OrdenCompraDTO oCOM_OrdenCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_OrdenCompraDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();
            if (oCOM_OrdenCompraDTO.idOrdenCompra == 0)
            {
                oCOM_OrdenCompraDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCOM_OrdenCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oCOM_OrdenCompraDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oCOM_OrdenCompraBL.UpdateInsert(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);

            List<COM_OrdenCompraDTO> lstCOM_OrdenCompraDTO = oResultDTO.ListaResultado;
            string listaOrdenCompra = Serializador.rSerializado(lstCOM_OrdenCompraDTO, new string[]
            {"idOrdenCompra", "FechaOrdenCompra", "NumOrdenCompra", "ProveedorDocumento", "ProveedorRazon", "TotalNacional", "Estado" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaOrdenCompra);
        }
        public string Eliminar(COM_OrdenCompraDTO oCOM_OrdenCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_OrdenCompraDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();
            oCOM_OrdenCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;

            oResultDTO = oCOM_OrdenCompraBL.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);
            string lista_Compra = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idOrdenCompra", "FechaOrdenCompra", "NumOrdenCompra", "ProveedorDocumento", "ProveedorRazon", "TotalNacional", "Estado" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Compra);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();
            ResultDTO<COM_OrdenCompraDTO> oListaOrdenCompras = oCOM_OrdenCompraBL.ListarxID(id);
            string listaOrdenCompra = Serializador.rSerializado(oListaOrdenCompras.ListaResultado, new string[] { });
            //Direcciones de Proveedor
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaOrdenCompras.ListaResultado[0].idProveedor);
            string listaSocio_Direccion = Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[]
            { "idDireccion", "Direccion" });


            string listaOrdenCompraDetalle = Serializador.rSerializado(oListaOrdenCompras.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}", "OK", listaOrdenCompra, listaSocio_Direccion, listaOrdenCompraDetalle);
        }

        public string ListarOrdenCompra(int ordenCompra)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();
            //listas
            ResultDTO<COM_OrdenCompraDTO> oResultDTO = oCOM_OrdenCompraBL.ListarOrdenCompra(ordenCompra);
            //cadenas
            string listaCOM_OrdenCompraBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaCOM_OrdenCompraBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
            "idOrdenCompra", "FechaOrdenCompra", "NumOrdenCompra","Estado"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaCOM_OrdenCompraBL);
        }





    }
}