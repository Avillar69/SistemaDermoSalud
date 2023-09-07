using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Finanzas;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Finanzas;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers.Finanzas
{
    public class PagoController : Controller
    {
        // GET: Pago
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
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            //COM_RequerimientoBL ocCOM_RequerimientoBL = new COM_RequerimientoBL();

            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "P");
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_FormaPagoDTO> oListaFormaPago = oMa_FormaPagoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<FN_PagosDTO> oListaPagos = oFN_PagoBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);

            //ResultDTO<COM_OrdenCompraDTO> oListaDocumentoCompras = ocCOM_RequerimientoBL.ListarRequerimiento();
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            // string listaLocal = Serializador.rSerializado(eSEGUsuario.oListaLocales, new string[] { "idLocal", "Descripcion" });
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string listaFormaPago = Serializador.rSerializado(oListaFormaPago.ListaResultado, new string[] { "idFormaPago", "Descripcion" });
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });
            string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            string listaPago = Serializador.rSerializado(oListaPagos.ListaResultado, new string[]
            { "idPago", "FechaPago", "idDocumentoCompraVenta","NumeroDcto", "RazonSocial", "MontoxPagar", "MontoxCobrar", "SaldoxAplicar" });

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}", "OK", listaSocios/*, listaLocal*/, listaMoneda, listaFormaPago, listaComprobantes, listaTipoCompra, listaPago, fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            ResultDTO<FN_PagosDTO> olistaPago = oFN_PagoBL.ListarRangoFecha(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaPago = Serializador.rSerializado(olistaPago.ListaResultado, new string[]
            { "idPago", "FechaPago", "idDocumentoCompraVenta","NumeroDcto", "RazonSocial", "MontoxPagar", "MontoxCobrar", "SaldoxAplicar" });
            return String.Format("{0}↔{1}↔{2}", olistaPago.Resultado, olistaPago.MensajeError, listaPago);
        }

        public string Grabar(FN_PagosDTO oCOM_OrdenCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<FN_PagosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            if (oCOM_OrdenCompraDTO.idPago == 0)
            {
                oCOM_OrdenCompraDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCOM_OrdenCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oCOM_OrdenCompraDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oFN_PagoBL.UpdateInsert(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);

            List<FN_PagosDTO> lstCOM_OrdenCompraDTO = oResultDTO.ListaResultado;
            string listaOrdenCompra = Serializador.rSerializado(lstCOM_OrdenCompraDTO, new string[]
            {"idPago", "FechaPago", "idDocumentoCompraVenta","NumeroDcto", "RazonSocial", "MontoxPagar", "MontoxCobrar", "SaldoxAplicar" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaOrdenCompra);
        }
        public string Eliminar(FN_PagosDTO oCOM_OrdenCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<FN_PagosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            oCOM_OrdenCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;

            oResultDTO = oFN_PagoBL.Delete(oCOM_OrdenCompraDTO, fechaInicio, fechaFin);
            string lista_Pago = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            {"idPago", "FechaPago", "idDocumentoCompraVenta","NumeroDcto", "RazonSocial", "MontoxPagar", "MontoxCobrar", "SaldoxAplicar"});
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Pago);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            ResultDTO<FN_PagosDTO> oListaPago = oFN_PagoBL.ListarxID(id);
            string listaPago = Serializador.rSerializado(oListaPago.ListaResultado, new string[] { });
            //Direcciones de Proveedor
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaPago.ListaResultado[0].idSocioNegocio);
            string listaSocio_Direccion = Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[] { "idDireccion", "Direccion" });
            string listaOrdenCompraDetalle = Serializador.rSerializado(oListaPago.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}", "OK", listaPago, listaSocio_Direccion, listaOrdenCompraDetalle);
        }

        public string ListarOrdenCompra(int ordenCompra)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            //listas
            ResultDTO<FN_PagosDTO> oResultDTO = oFN_PagoBL.ListarOrdenCompra(ordenCompra);
            //cadenas
            string listaFN_PagoBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaFN_PagoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
            "idPago", "FechaOrdenCompra", "NumOrdenCompra","Estado"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_PagoBL);
        }



        public string ListarPagos(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //BL
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            //listas
            ResultDTO<FN_PagosDTO> oResultDTO = oFN_PagoBL.ListarPagos(id);
            //cadenas
            string listaFN_PagoBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaFN_PagoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[]
                {"idPago", "FechaCreacion","RazonSocial","NumeroDcto" ,"SerieDcto" ,"SaldoxAplicar"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaFN_PagoBL);
        }
        /*metodo para actualizar el pago y grabar en el detalle los pagos echos*/
        public string ActPagoDetalle(FN_PagosDTO oFN_PagosDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<FN_PagosDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            if (oFN_PagosDTO.idPago == 0)
            {
                oFN_PagosDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oFN_PagosDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oFN_PagosDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            oResultDTO = oFN_PagoBL.UpdateInsert(oFN_PagosDTO, fechaInicio, fechaFin);

            List<FN_PagosDTO> lstCOM_OrdenCompraDTO = oResultDTO.ListaResultado;
            string listaOrdenCompra = Serializador.rSerializado(lstCOM_OrdenCompraDTO, new string[]
            {"idPago", "FechaPago", "idDocumentoCompraVenta","NumeroDcto", "RazonSocial", "MontoxPagar", "MontoxCobrar", "SaldoxAplicar" });
            return String.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaOrdenCompra);
        }

        public string ObtenerDatosxIDPagoCompras(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            ResultDTO<FN_PagosDTO> oListaPago = oFN_PagoBL.ListarxIDInnerCompras(id);
            string listaPago = Serializador.rSerializado(oListaPago.ListaResultado, new string[] { });
            string listaFN_PagoBL = "";
            ResultDTO<FN_PagosDTO> oResultDTO = oFN_PagoBL.ListarPagos(id);
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaFN_PagoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[]
                {"idPago", "FechaCreacion","RazonSocial","NumeroDcto" ,"SerieDcto" ,"SaldoxAplicar"}, false);
            }
            return String.Format("{0}↔{1}↔{2}", "OK", listaPago, listaFN_PagoBL);

            //AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            //ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaPago.ListaResultado[0].idSocioNegocio);
            //string listaSocio_Direccion = Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[] { "idDireccion", "Direccion" });
            //string listaOrdenCompraDetalle = Serializador.rSerializado(oListaPago.ListaResultado[0].oListaDetalle, new string[] { });
            //return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", "OK", listaPago, listaSocio_Direccion, listaOrdenCompraDetalle, listaFN_PagoBL);
        }

        public string ObtenerDatosxIDPagoVentas(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            FN_PagoBL oFN_PagoBL = new FN_PagoBL();
            ResultDTO<FN_PagosDTO> oListaPago = oFN_PagoBL.ListarxIDInnerVentas(id);
            string listaPago = Serializador.rSerializado(oListaPago.ListaResultado, new string[] { });
            string listaFN_PagoBL = "";
            ResultDTO<FN_PagosDTO> oResultDTO = oFN_PagoBL.ListarPagos(id);
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaFN_PagoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[]
                {"idPago", "FechaCreacion","RazonSocial","NumeroDcto" ,"SerieDcto" ,"SaldoxAplicar"}, false);
            }


            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaPago.ListaResultado[0].idSocioNegocio);
            string listaSocio_Direccion = Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[] { "idDireccion", "Direccion" });
            string listaOrdenCompraDetalle = Serializador.rSerializado(oListaPago.ListaResultado[0].oListaDetalle, new string[] { });
            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", "OK", listaPago, listaSocio_Direccion, listaOrdenCompraDetalle, listaFN_PagoBL);
        }


    }
}