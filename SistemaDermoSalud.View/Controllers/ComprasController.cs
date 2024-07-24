using SistemaDermoSalud.Business;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDermoSalud.View.Controllers
{
    public class ComprasController : Controller
    {
        // GET: Compras
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
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            //ingreso la lista de Orden Compra
            COM_OrdenCompraBL oCOM_OrdenCompraBL = new COM_OrdenCompraBL();

            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "P");
            ResultDTO<Ma_MonedaDTO> oListaMoneda = oMa_MonedaBL.ListarTodo(eSEGUsuario.idEmpresa);
            ResultDTO<Ma_FormaPagoDTO> oListaFormaPago = oMa_FormaPagoBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<Ma_TipoComprobanteDTO> oListaComprobantes = oMa_TipoComprobanteBL.ListarTodo();
            ResultDTO<Ma_Clase_ArticuloDTO> oListaClaseArticulo = oMa_Clase_ArticuloBL.ListarTodo(eSEGUsuario.idEmpresa, "A");
            ResultDTO<COM_DocumentoCompraDTO> oListaDocumentoCompras = oCOM_DocumentoCompraBL.ListarRangoFecha(eSEGUsuario.idEmpresa, fechaInicio, fechaFin);
            //ingreso la lista de Orden Compra
            // ResultDTO<AD_SocioNegocioDTO> oListaOrdenCOmpra = oCOM_OrdenCompraBL.ListarProv(eSEGUsuario.idEmpresa, "P");↔{9}

            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });
            string listaMoneda = Serializador.rSerializado(oListaMoneda.ListaResultado, new string[] { "idMoneda", "Descripcion" });
            string listaFormaPago = Serializador.rSerializado(oListaFormaPago.ListaResultado, new string[] { "idFormaPago", "Descripcion" });
            string listaComprobantes = Serializador.rSerializado(oListaComprobantes.ListaResultado, new string[] { "idTipoComprobante", "Descripcion" });
            string listaTipoCompra = Serializador.rSerializado(oListaClaseArticulo.ListaResultado, new string[] { "idClaseArticulo", "CodigoGenerado", "Descripcion" });
            string listaDocumentoCompra = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[]
            {"idDocumentoCompra","FechaDocumento","SerieDocumento", "NumDocumento","ProveedorRazon","SubTotalNacional","IGVNacional","TotalNacional" });
            string listaMa_BancoBL = "";
            Ma_BancoBL oMa_BancoBL = new Ma_BancoBL();
            ResultDTO<Ma_BancoDTO> oResultDTO = oMa_BancoBL.ListaBancos();
            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaMa_BancoBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] { "idBanco", "Descripcion" }, false);
            }
            AD_CuentaOrigenBL CuentaOrigen = new AD_CuentaOrigenBL();
            ResultDTO<AD_CuentaOrigenDTO> resultado = CuentaOrigen.ListarTodo();
            string listaAD_CuentaOrigen = Serializador.rSerializado(resultado.ListaResultado, new string[]
            {"idCuentaOrigen", "NumeroCuenta"});

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}↔{5}↔{6}↔{7}↔{8}↔{9}↔{10}", "OK", listaSocios, listaMoneda,
                listaFormaPago, listaComprobantes, listaTipoCompra, listaDocumentoCompra, fechaInicio.ToString("dd/MM/yyyy"), fechaFin.ToString("dd/MM/yyyy"), listaMa_BancoBL, listaAD_CuentaOrigen);
        }
        public string ObtenerDatosxID(int id)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            //Datos Cabecera de Documento
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            ResultDTO<COM_DocumentoCompraDTO> oListaDocumentoCompras = oCOM_DocumentoCompraBL.ListarxID(id);//ListarxIDNotas_Compras
            ResultDTO<COM_DocumentoCompraDTO> oListaDocumento_Nota = oCOM_DocumentoCompraBL.ListarxIDNotas_Compras(id);
            string listaDocumentoCompra = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[] { });
            string listaDocumento_Nota = Serializador.rSerializado(oListaDocumento_Nota.ListaResultado, new string[] { });
            //Direcciones de Proveedor
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            var a = oListaDocumentoCompras.ListaResultado[0].idProveedor;
            ResultDTO<AD_SocioNegocioDTO> oListaDirecciones = oAD_SocioNegocioBL.ListarxID(oListaDocumentoCompras.ListaResultado[0].idProveedor);
            string listaSocio_Direccion = oListaDirecciones.ListaResultado.Any() ?
                Serializador.rSerializado(oListaDirecciones.ListaResultado[0].oListaDireccion, new string[] { "idDireccion", "Direccion" }) : "";
            string listaDocumentoCompraDetalle = oListaDocumentoCompras.ListaResultado.Any() ?
                Serializador.rSerializado(oListaDocumentoCompras.ListaResultado[0].oListaDetalle, new string[] { }) : "";

            return String.Format("{0}↔{1}↔{2}↔{3}↔{4}", "OK", listaDocumentoCompra, listaSocio_Direccion, listaDocumentoCompraDetalle, listaDocumento_Nota);
        }
        public string ObtenerPorFecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            ResultDTO<COM_DocumentoCompraDTO> oListaDocumentoCompras = oCOM_DocumentoCompraBL.ListarRangoFecha(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));
            string listaDocumentoCompra = Serializador.rSerializado(oListaDocumentoCompras.ListaResultado, new string[] {"idDocumentoCompra", "FechaDocumento", "SerieDocumento", "NumDocumento",
                   "ProveedorRazon", "SubTotalNacional", "IGVNacional", "TotalNacional" });
            return String.Format("{0}↔{1}↔{2}", oListaDocumentoCompras.Resultado, oListaDocumentoCompras.MensajeError, listaDocumentoCompra);
        }
        public string Grabar(COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            if (oCOM_DocumentoCompraDTO.idDocumentoCompra == 0)
            {
                oCOM_DocumentoCompraDTO.UsuarioCreacion = eSEGUsuario.idUsuario;
            }
            oCOM_DocumentoCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;
            oCOM_DocumentoCompraDTO.UsuarioModificacion = eSEGUsuario.idUsuario;
            var a = oCOM_DocumentoCompraDTO.idDocumentoCompra;
            //obtendre si se repite o no para que se pueda grabar
            string oResultDTO2 = oCOM_DocumentoCompraBL.ValidarDocumento(oCOM_DocumentoCompraDTO);
            string validandoElDocumento = "";
            validandoElDocumento = oResultDTO2;
            if (validandoElDocumento == "" || oCOM_DocumentoCompraDTO.idDocumentoCompra != 0)
            {
                oResultDTO = oCOM_DocumentoCompraBL.UpdateInsert(oCOM_DocumentoCompraDTO, fechaInicio, fechaFin);
                string listaDocumentoCompra = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
                    {"idDocumentoCompra", "FechaDocumento", "SerieDocumento", "NumDocumento","ProveedorRazon", "SubTotalNacional", "IGVNacional", "TotalNacional" });
                return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, listaDocumentoCompra);
            }
            else
            {
                return string.Format("{0}↔{1}↔{2}", "REPITE", "ya hay un proveedor que tiene ese numero y serie", "no se guardo nada");
            }


        }
        public string Eliminar(COM_DocumentoCompraDTO oCOM_DocumentoCompraDTO)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            oCOM_DocumentoCompraDTO.idEmpresa = eSEGUsuario.idEmpresa;

            oResultDTO = oCOM_DocumentoCompraBL.Delete(oCOM_DocumentoCompraDTO, fechaInicio, fechaFin);
            string lista_Compra = Serializador.rSerializado(oResultDTO.ListaResultado, new string[]
            { "idDocumentoCompra", "FechaDocumento", "SerieDocumento", "NumDocumento","ProveedorRazon", "SubTotalNacional", "IGVNacional", "TotalNacional" });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_Compra);
        }

        public string fechaVencimiento(string fecha, int forma)
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            oResultDTO = oCOM_DocumentoCompraBL.FechaVencimiento(fecha, forma);
            string lista_fecha = Serializador.rSerializado(oResultDTO.ListaResultado, new string[] { });
            return string.Format("{0}↔{1}↔{2}", oResultDTO.Resultado, oResultDTO.MensajeError, lista_fecha);
        }

        public string ObtenerUnicamenteDatosCompra()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            COM_DocumentoCompraBL oCOM_DocumentoCompraBL = new COM_DocumentoCompraBL();
            ResultDTO<COM_DocumentoCompraDTO> oResultDTO = oCOM_DocumentoCompraBL.ListarTodo();
            string listaCOM_CompraBL = "";

            if (oResultDTO.ListaResultado != null && oResultDTO.ListaResultado.Count > 0)
            {
                listaCOM_CompraBL = Serializador.Serializar(oResultDTO.ListaResultado, '▲', '▼', new string[] {
            "idDocumentoCompra", "SerieDocumento", "NumDocumento","ProveedorDocumento"}, false);
            }
            return String.Format("{0}↔{1}↔{2}",
                oResultDTO.Resultado, oResultDTO.MensajeError, listaCOM_CompraBL);
        }


    }
}