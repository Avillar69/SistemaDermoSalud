using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDermoSalud.Business;
using SistemaDermoSalud.Business.Mantenimiento;
using SistemaDermoSalud.Entities;
using SistemaDermoSalud.Entities.Mantenimiento;
using SistemaDermoSalud.Entities.Nubefact;
using SistemaDermoSalud.Entities.Reportes;
using SistemaDermoSalud.Helpers;

namespace SistemaDermoSalud.View.Controllers.Reportes
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult RegistroVentas()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteUtilidades()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteVentas()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteComprobantePago()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteGastosxDia()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteProductoAgotado()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteProductoSinVenta()
        {
            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public ActionResult ReporteDetalleVentaCliente() {

            if (Session["Config"] == null) return RedirectToAction("Login", "Home");
            else
            {
                return PartialView();
            }
        }
        public string ObtenerDatos_ProductosxAgotarse()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ProductoBL oAD_SocioNegocioBL = new Ma_ProductoBL();
            ResultDTO<RepProductoAgotarseDTO> oListaSocios = oAD_SocioNegocioBL.ReporteProductoxAgotarse();
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { });


            return String.Format("{0}↔{1}", "OK", listaSocios);
        }
        public string ObtenerDatos_DetalleVentaCliente()
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            AD_SocioNegocioBL oAD_SocioNegocioBL = new AD_SocioNegocioBL();
            ResultDTO<AD_SocioNegocioDTO> oListaSocios = oAD_SocioNegocioBL.ListarProv(eSEGUsuario.idEmpresa, "C");
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today; 
            string listaSocios = Serializador.rSerializado(oListaSocios.ListaResultado, new string[] { "idSocioNegocio", "RazonSocial", "Documento" });


            return String.Format("{0}↔{1}↔{2}↔{3}", "OK", fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"),listaSocios);
        }
        public string ObtenerDatos_RegistroVenta()
        {
            DateTime fechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime fechaFin = DateTime.Today;
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
           
            return String.Format("{0}↔{1}↔{2}", "OK",  fechaInicio.ToString("dd-MM-yyyy"), fechaFin.ToString("dd-MM-yyyy"));
        }
        public string ObtenerPorFecha_DetalleVentaCliente(string fechaInicio, string fechaFin, int idCliente)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<Rep_DocumentoVentaDetalleCliente> oListaDocumentoVentas = new ResultDTO<Rep_DocumentoVentaDetalleCliente>();

            oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarDetalleVentaCliente(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), idCliente);

            string listaDocumentoVenta = Serializador.rSerializado(oListaDocumentoVentas.ListaResultado, new string[]{ });

            return String.Format("{0}↔{1}↔{2}", oListaDocumentoVentas.Resultado, oListaDocumentoVentas.MensajeError, listaDocumentoVenta);
        }

        public string ObtenerPorFecha_RegistroVentas(string fechaInicio, string fechaFin, int idTipoDctos)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VN_DocumentoVentaBL oVN_DocumentoVentaBL = new VN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDTO> oListaDocumentoVentas = new ResultDTO<VEN_DocumentoVentaDTO>();
            //string[] TipoDctos = { "1", "2", "3" };
            //string[] Dctos = { "true", "true", "true" };
            //for (int i = 0; i < Dctos.Length; i++)
            //{
            //    if (Dctos[i] == Documentos.Split('-')[i])
            //    {
            //        oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRegistroVenta(oListaDocumentoVentas,eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), TipoDctos[i]);                   
            //    }
            //}
            oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRegistroVenta(eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), idTipoDctos);

            string listaDocumentoVenta = Serializador.rSerializado(oListaDocumentoVentas.ListaResultado, new string[]
            { "idDocumentoVenta", "FechaDocumento", "SerieDocumento", "NumDocumento", "ClienteRazon", "SubTotalNacional", "IGVNacional", "TotalNacional", "Estado", "Enlace" });
            
            return String.Format("{0}↔{1}↔{2}", oListaDocumentoVentas.Resultado, oListaDocumentoVentas.MensajeError, listaDocumentoVenta);
        }

        public string ReporteVenta_Fecha(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oListaReporteVentas = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            //string[] TipoDctos = { "1", "2", "3" };
            //string[] Dctos = { "true", "true", "true" };
            //for (int i = 0; i < Dctos.Length; i++)
            //{
            //    if (Dctos[i] == Documentos.Split('-')[i])
            //    {
            //        oListaDocumentoVentas = oVN_DocumentoVentaBL.ListarRegistroVenta(oListaDocumentoVentas,eSEGUsuario.idEmpresa, Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), TipoDctos[i]);                   
            //    }
            //}
            oListaReporteVentas = oVEN_DocumentoVentaBL.ReporteVentaFecha(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));

            string ListaReporteVentas = Serializador.rSerializado(oListaReporteVentas.ListaResultado, new string[]
            { "idArticulo", "Laboratorio", "DescripcionArticulo", "Cantidad", "PrecioMedicamento", "TotalNacional"});

            return String.Format("{0}↔{1}↔{2}", oListaReporteVentas.Resultado, oListaReporteVentas.MensajeError, ListaReporteVentas);
        }


        public string ReporteVenta_GastosxDia(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            FN_CajaBL oVEN_DocumentoVentaBL = new FN_CajaBL();
            ResultDTO<RepGastosxDiaDTO> oListaReporteVentas = new ResultDTO<RepGastosxDiaDTO>();
            oListaReporteVentas = oVEN_DocumentoVentaBL.RepGastosxDia(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));

            string ListaReporteVentas = Serializador.rSerializado(oListaReporteVentas.ListaResultado, new string[]
            {});

            return String.Format("{0}↔{1}↔{2}", oListaReporteVentas.Resultado, oListaReporteVentas.MensajeError, ListaReporteVentas);
        }
        public string ReporteVenta_ProductoSinVenta(string fechaInicio, string fechaFin)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            Ma_ProductoBL oVEN_DocumentoVentaBL = new Ma_ProductoBL();
            ResultDTO<RepProductoAgotarseDTO> oListaReporteVentas = new ResultDTO<RepProductoAgotarseDTO>();
            oListaReporteVentas = oVEN_DocumentoVentaBL.ReporteProductoSinVenta(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin));

            string ListaReporteVentas = Serializador.rSerializado(oListaReporteVentas.ListaResultado, new string[]
            {});

            return String.Format("{0}↔{1}↔{2}", oListaReporteVentas.Resultado, oListaReporteVentas.MensajeError, ListaReporteVentas);
        }


        public string ReporteVenta_Fecha_Medicamento(string fechaInicio, string fechaFin, string idMedicamento)
        {
            Seg_UsuarioDTO eSEGUsuario = ((ObjSesionDTO)Session["Config"]).SessionUsuario;
            VEN_DocumentoVentaBL oVEN_DocumentoVentaBL = new VEN_DocumentoVentaBL();
            ResultDTO<VEN_DocumentoVentaDetalleDTO> oListaReporteVentas = new ResultDTO<VEN_DocumentoVentaDetalleDTO>();
            oListaReporteVentas = oVEN_DocumentoVentaBL.ReporteVentaFechaMedicamento(Convert.ToDateTime(fechaInicio), Convert.ToDateTime(fechaFin), Convert.ToInt32(idMedicamento));

            string ListaReporteVentas = Serializador.rSerializado(oListaReporteVentas.ListaResultado, new string[]
            { "FechaCreacion", "Factura", "Cliente", "DescripcionArticulo", "Cantidad", "PrecioMedicamento","TotalNacional"});

            return String.Format("{0}↔{1}↔{2}", oListaReporteVentas.Resultado, oListaReporteVentas.MensajeError, ListaReporteVentas);
        }

    }
}